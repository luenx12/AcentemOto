using AcentemOto.Data;
using AcentemOto.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public class WhatsAppAutomationService : IDisposable
    {
        private IWebDriver? _driver;
        private readonly AntiSpamEngine _antiSpamEngine;
        private readonly IMessageLogRepository _repository;
        private bool _isDisposed = false;

        public WhatsAppAutomationService(IMessageLogRepository repository)
        {
            _repository = repository;
            _antiSpamEngine = new AntiSpamEngine();
            KillZombieChromeDrivers();
        }

        public void InitializeDriver(bool isHeadless = false)
        {
            var options = new ChromeOptions();

            // User Data directory -> AppData\Roaming\AcentemOto\ChromeProfile
            string userDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AcentemOto", "ChromeProfile");
            options.AddArgument($"user-data-dir={userDataDir}");

            // Anti-detect arguments
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("--window-size=1920,1080");

            if (isHeadless)
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            }

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            _driver = new ChromeDriver(service, options);

            // WhatsApp Web ilk yükleme
            _driver.Navigate().GoToUrl("https://web.whatsapp.com/");
        }

        public async Task SendMessageAsync(List<MessageLog> messageLogs, string messageText, IProgress<string> progress, CancellationToken cancellationToken)
        {
            int successCount = 0;

            foreach (var log in messageLogs)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    progress.Report("Gönderim kullanıcı tarafından durduruldu.");
                    break;
                }

                if (log.Status == MessageStatus.Sent)
                {
                    continue; // Zaten gönderilmişleri atla
                }

                try
                {
                    progress.Report($"İşleniyor: {log.PhoneNumber}");

                    // URL encoded message and phone number
                    string encodedMessage = Uri.EscapeDataString(messageText);
                    string url = $"https://web.whatsapp.com/send?phone={log.PhoneNumber.TrimStart('+')}&text={encodedMessage}";

                    if (_driver == null) throw new InvalidOperationException("Tarayıcı başlatılmamış.");

                    _driver.Navigate().GoToUrl(url);

                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

                    // Check for invalid number popup or the message input box
                    bool isValidNumber = await WaitForMessageBoxOrErrorAsync(wait, cancellationToken);

                    if (!isValidNumber)
                    {
                        log.Status = MessageStatus.Failed;
                        log.ErrorMessage = "Geçersiz numara.";
                        await _repository.UpdateLogAsync(log);
                        progress.Report($"Hata: {log.PhoneNumber} (Geçersiz numara)");
                        continue;
                    }

                    // Sohbetin tam olarak yüklenmesi ve mesaj kutusunun hazır olması için bekle
                    await Task.Delay(3000, cancellationToken);

                    bool messageSent = false;
                    var sendButtons = _driver.FindElements(By.CssSelector("span[data-icon='send'], button[aria-label='Gönder'], button[aria-label='Send']"));
                    foreach (var btn in sendButtons)
                    {
                        try
                        {
                            btn.Click();
                            messageSent = true;
                            break;
                        }
                        catch { }
                    }

                    if (!messageSent)
                    {
                        var msgBoxes = _driver.FindElements(By.CssSelector("div[aria-placeholder='Bir mesaj yazın'], div[title='Bir mesaj yazın'], div[title='Type a message'], div[contenteditable='true'][data-tab='10']"));
                        if (msgBoxes.Count > 0)
                        {
                            msgBoxes[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                        }
                        else
                        {
                            throw new WebDriverTimeoutException("Mesaj gönderme butonu veya alanları bulunamadı.");
                        }
                    }

                    // Mesajın gerçekten iletilmesi ve animasyonunun tamamlanması için ek bekleme (ÇOK ÖNEMLİ: URL hemen değişirse mesaj gitmez)
                    await Task.Delay(4000, cancellationToken);

                    log.Status = MessageStatus.Sent;
                    log.SentAt = DateTime.Now;
                    await _repository.UpdateLogAsync(log);

                    successCount++;
                    progress.Report($"Başarılı: {log.PhoneNumber}");

                    // Anti-spam mola
                    progress.Report("Anti-spam beklemesi devrede...");
                    await _antiSpamEngine.WaitBetweenMessagesAsync();

                    await _antiSpamEngine.CooldownIfNecessaryAsync(successCount);
                }
                catch (WebDriverTimeoutException)
                {
                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = "Zaman aşımı veya element bulunamadı.";
                    await _repository.UpdateLogAsync(log);
                    progress.Report($"Hata: {log.PhoneNumber} ({log.ErrorMessage})");
                }
                catch (WebDriverException ex) when (ex.Message.Contains("net::ERR_INTERNET_DISCONNECTED"))
                {
                    progress.Report("İnternet bağlantısı kesildi. Yeniden bağlanması bekleniyor...");
                    await WaitUntilInternetRestoredAsync(cancellationToken);
                    
                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = "İnternet kesintisi";
                    await _repository.UpdateLogAsync(log);
                }
                catch (Exception ex)
                {
                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = ex.Message;
                    await _repository.UpdateLogAsync(log);
                    progress.Report($"Beklenmeyen Hata: {log.PhoneNumber} ({log.ErrorMessage})");
                }
            }
        }

        private async Task<bool> WaitForMessageBoxOrErrorAsync(WebDriverWait wait, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Message box: div[aria-placeholder='Bir mesaj yazın'] or div[title='Bir mesaj yazın']
                    // Invalid popup: div[data-animate-modal-popup="true"] -> "Telefon numarası URL üzerinden paylaşıldı ancak geçersiz."
                    
                    var element = wait.Until(driver => {
                        var msgBox = driver.FindElements(By.CssSelector("div[aria-placeholder='Bir mesaj yazın'], div[title='Bir mesaj yazın'], div[title='Type a message'], div[contenteditable='true'][data-tab='10']"));
                        if (msgBox.Count > 0 && msgBox[0].Displayed) return msgBox[0];
                        
                        var errorPopup = driver.FindElements(By.CssSelector("div[data-animate-modal-popup='true'] button"));
                        if (errorPopup.Count > 0 && errorPopup[0].Displayed) return errorPopup[0];
                        
                        return null;
                    });
                    
                    // If it's a popup (button), it's invalid number
                    if (element.TagName.ToLower() == "button" || element.GetAttribute("data-animate-modal-popup") != null)
                    {
                        return false;
                    }
                    
                    return true;
                }
                catch
                {
                    return false;
                }
            }, cancellationToken);
        }

        private async Task WaitUntilInternetRestoredAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var title = _driver?.Title;
                    if (!string.IsNullOrEmpty(title))
                        break;
                }
                catch
                {
                    // Still disconnected
                }
                await Task.Delay(5000, cancellationToken);
            }
        }

        private void KillZombieChromeDrivers()
        {
            try
            {
                var processes = Process.GetProcessesByName("chromedriver");
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
            catch
            {
                // Ignore permission errors
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    try
                    {
                        if (_driver != null)
                        {
                            _driver.Quit();
                            _driver.Dispose();
                        }
                    }
                    catch { }
                }

                KillZombieChromeDrivers();
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
