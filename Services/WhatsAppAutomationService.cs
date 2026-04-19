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
using System.Text.RegularExpressions;

namespace AcentemOto.Services
{
    /// <summary>Gönderim oturumunun özet sonucu.</summary>
    public record SendResult(int Success, int Failed, int Skipped, TimeSpan Duration);

    public class WhatsAppAutomationService : IDisposable
    {
        private IWebDriver? _driver;
        private readonly AntiSpamEngine _antiSpamEngine;
        public AntiSpamEngine AntiSpam => _antiSpamEngine;
        private readonly IMessageLogRepository _repository;
        private bool _isDisposed = false;

        public WhatsAppAutomationService(IMessageLogRepository repository)
        {
            _repository = repository;
            _antiSpamEngine = new AntiSpamEngine();
            KillZombieChromeDrivers();
        }

        public void InitializeDriver(bool isHeadless = false, string profileName = "DefaultProfile")
        {
            var options = new ChromeOptions();

            // User Data directory -> AppData\Roaming\AcentemOto\ChromeProfiles\{profileName}
            string userDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AcentemOto", "ChromeProfiles", profileName);
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

        public async Task<SendResult> SendMessageAsync(List<MessageLog> messageLogs, string messageTemplate, string? attachmentPath, IProgress<string> progress, CancellationToken cancellationToken, bool useUniqueHash = false)
        {
            int successCount = 0;
            int failedCount  = 0;
            int skippedCount = 0;
            var startTime    = DateTime.Now;

            foreach (var log in messageLogs)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    progress.Report("Gönderim kullanıcı tarafından durduruldu.");
                    break;
                }

                if (log.Status == MessageStatus.Sent)
                {
                    skippedCount++;
                    continue; // Zaten gönderilmişleri atla
                }

                if (string.IsNullOrWhiteSpace(log.PhoneNumber))
                {
                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = "Eksik Numara / Telefon Yok";
                    await _repository.UpdateLogAsync(log);
                    progress.Report($"Hata: Kayıtta telefon numarası bulunamadı, atlandı.");
                    failedCount++;
                    continue;
                }

                try
                {
                    progress.Report($"İşleniyor: {log.PhoneNumber}");

                    // 1. Prepare paramatric message: replace {Header} with value
                    string finalMessageText = messageTemplate;
                    foreach (var param in log.Parameters)
                    {
                        finalMessageText = Regex.Replace(finalMessageText, Regex.Escape($"{{{param.Key}}}"), param.Value ?? "", RegexOptions.IgnoreCase);
                    }

                    // Mesaj Benzersizleştirme (Hash Buster)
                    if (useUniqueHash)
                    {
                        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        string hash = new string(Enumerable.Repeat(chars, 6).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
                        finalMessageText += $"\n[Ref: {hash}]";
                    }

                    // URL encoded message and phone number
                    // Metni kopyalamak bazen başarısız olursa diye URL'e de ekliyoruz (yedek olarak).
                    string encodedMessage = Uri.EscapeDataString(finalMessageText);
                    string url = $"https://web.whatsapp.com/send?phone={log.PhoneNumber}&text={encodedMessage}";

                    if (_driver == null) throw new InvalidOperationException("Tarayıcı başlatılmamış.");

                    // SPA Navigasyon (Sayfa Yenilenmesini Engelleme)
                    ((IJavaScriptExecutor)_driver).ExecuteScript(
                        "const a = document.createElement('a');" +
                        "a.href = arguments[0];" +
                        "document.body.appendChild(a);" +
                        "a.click();" +
                        "document.body.removeChild(a);", url);

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

                    // --- METİN GÖNDERİMİ ---
                    var txtBoxes = _driver.FindElements(By.CssSelector("div[aria-placeholder='Bir mesaj yazın'], div[title='Bir mesaj yazın'], div[title='Type a message'], div[contenteditable='true'][data-tab='10'], div[contenteditable='true'][data-tab='6']"));
                    if (txtBoxes.Count > 0)
                    {
                        // URL üzerinden metin zaten kutuya dolmuş olabilir. Eğer boşsa clipboard ile yapıştır.
                        if (string.IsNullOrWhiteSpace(txtBoxes[0].Text))
                        {
                            try
                            {
                                // Javascript kullanarak div'e yapıştırmak daha güvenlidir
                                ((IJavaScriptExecutor)_driver).ExecuteScript(
                                    "const text = arguments[0];" +
                                    "const dataTransfer = new DataTransfer();" +
                                    "dataTransfer.setData('text', text);" +
                                    "const event = new ClipboardEvent('paste', { clipboardData: dataTransfer, bubbles: true });" +
                                    "arguments[1].dispatchEvent(event);",
                                    finalMessageText, txtBoxes[0]);

                                await Task.Delay(500, cancellationToken);
                            }
                            catch { /* Ignore paste error, URL text might be there */ }
                        }

                        // İnsan Simülasyonu (Humanize)
                        await Task.Delay(new Random().Next(1000, 3001), cancellationToken);

                        txtBoxes[0].SendKeys(OpenQA.Selenium.Keys.Enter);
                    }
                    else
                    {
                        throw new WebDriverTimeoutException("Mesaj gönderme butonu veya alanları bulunamadı.");
                    }

                    await Task.Delay(1000, cancellationToken);

                    // --- DOSYA/GÖRSEL EKLENTİ GÖNDERİMİ ---
                    if (!string.IsNullOrWhiteSpace(attachmentPath) && File.Exists(attachmentPath))
                    {
                        await Task.Delay(1000, cancellationToken); // Metin gönderildikten sonra kısa bekleme

                        try
                        {
                            // 1. Eklenti (Artı / Ataş) butonuna tıkla ki input DOM'a yüklensin
                            var attachButtons = _driver.FindElements(By.CssSelector("div[title='Ekle'], span[data-icon='plus'], span[data-icon='clip']"));
                            if (attachButtons.Count > 0)
                            {
                                attachButtons[0].Click();
                                await Task.Delay(1000, cancellationToken); // Menünün açılmasını bekle
                            }

                            // 2. Gizli input[type='file'] elemanlarını bul
                            var fileInputs = _driver.FindElements(By.CssSelector("input[type='file']"));
                            if (fileInputs.Count > 0)
                            {
                                // Resimler/Videolar için olan genellikle 2. inputtur, ancak hepsini denemek mantıklı
                                // Eklenen hata çözümü: WhatsApp son güncellemesinde "Sticker" yükleme butonu da eklediği için "image" içeren iki input var.
                                // Resim/Video input'u her zaman "video/mp4" içerir, Sticker input'u içermez.

                                // Yeni WhatsApp Web yapısında "accept" parametresi gizlenmeye başlandı veya etiketler değiştirildi. 
                                // Ancak input sıraları sabittir: 
                                // [0] -> Document (Belge)
                                // [1] -> Image/Video (GaleriMedya)
                                // [2] -> Sticker

                                bool isImageOrVideo = attachmentPath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                      attachmentPath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                                      attachmentPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                                      attachmentPath.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ||
                                                      attachmentPath.EndsWith(".3gp", StringComparison.OrdinalIgnoreCase);

                                IWebElement? targetInput = null;

                                if (isImageOrVideo && fileInputs.Count >= 2)
                                {
                                    // Resim/Video ise kesinlikle 2. inputu (Index 1) kullan (Galeri)
                                    targetInput = fileInputs[1];
                                }
                                else if (!isImageOrVideo && fileInputs.Count >= 1)
                                {
                                    // Belge ise 1. inputu (Index 0) kullan (Document)
                                    targetInput = fileInputs[0];
                                }
                                else if (fileInputs.Count > 0)
                                {
                                    // Fallback
                                    targetInput = fileInputs[0];
                                }

                                if (targetInput != null)
                                {
                                    targetInput.SendKeys(attachmentPath);
                                }

                                // 3. Ön izleme ekranındaki Gönder butonunu bekle ve tıkla
                                WebDriverWait waitAttach = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
                                var attachSendBtn = waitAttach.Until(driver =>
                                {
                                    var btns = driver.FindElements(By.CssSelector("div[aria-label='Gönder'], span[data-icon='send'], div[role='button'] span[data-icon='send']"));
                                    foreach (var b in btns)
                                    {
                                        if (b.Displayed) return b;
                                    }
                                    return null;
                                });
                                attachSendBtn.Click();

                                await Task.Delay(3000, cancellationToken); // Dosya animasyonunun tamamlanması için ek süre
                            }
                        }
                        catch (Exception ex)
                        {
                            progress.Report($"Uyarı: Görsel/Dosya gönderilemedi ({ex.Message})");
                        }
                    }

                    // Animasyon ve ağ gecikmesi için bekle
                    await Task.Delay(4000, cancellationToken);

                    log.Status = MessageStatus.Sent;
                    log.SentAt = DateTime.Now;
                    await _repository.UpdateLogAsync(log);

                    successCount++;
                    progress.Report($"Başarılı: {log.PhoneNumber}");

                    // Saatlik/günlük limit kontrolü
                    bool canSend = await _antiSpamEngine.CheckAndWaitForLimitsAsync(progress, cancellationToken);
                    if (!canSend) break; // Günlük limit doldu, döngüden çık


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
                    failedCount++;
                }
                catch (WebDriverException ex) when (ex.Message.Contains("net::ERR_INTERNET_DISCONNECTED"))
                {
                    progress.Report("İnternet bağlantısı kesildi. Yeniden bağlanması bekleniyor...");
                    await WaitUntilInternetRestoredAsync(progress, cancellationToken);

                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = "İnternet kesintisi";
                    await _repository.UpdateLogAsync(log);
                    failedCount++;
                }
                catch (Exception ex)
                {
                    log.Status = MessageStatus.Failed;
                    log.ErrorMessage = ex.Message;
                    await _repository.UpdateLogAsync(log);
                    progress.Report($"Beklenmeyen Hata: {log.PhoneNumber} ({log.ErrorMessage})");
                    failedCount++;
                }
            }

            return new SendResult(successCount, failedCount, skippedCount, DateTime.Now - startTime);
        }

        private async Task<bool> WaitForMessageBoxOrErrorAsync(WebDriverWait wait, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                try
                {
                    bool isInvalid = false;
                    var element = wait.Until(driver =>
                    {
                        // 1. Sohbet kutusunu kontrol et
                        var msgBox = driver.FindElements(By.CssSelector("div[aria-placeholder='Bir mesaj yazın'], div[title='Bir mesaj yazın'], div[title='Type a message'], div[contenteditable='true'][data-tab='10'], div[contenteditable='true'][data-tab='6']"));
                        if (msgBox.Count > 0 && msgBox[0].Displayed) return msgBox[0];

                        // 2. Hata popup'ını kontrol et ("Telefon numarası paylaşılan url üzerinden..." veya "geçersiz")
                        var popups = driver.FindElements(By.CssSelector("div[data-animate-modal-popup='true']"));
                        if (popups.Count > 0 && popups[0].Displayed)
                        {
                            string popupText = popups[0].Text.ToLower();
                            if (popupText.Contains("geçersiz") || popupText.Contains("invalid") || popupText.Contains("url"))
                            {
                                isInvalid = true;
                                var btn = popups[0].FindElement(By.CssSelector("button"));
                                return btn;
                            }
                        }

                        return null;
                    });

                    if (isInvalid)
                    {
                        try { element.Click(); } catch { } // 'Tamam' butonuna basıp popup'ı kapat
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

        private async Task WaitUntilInternetRestoredAsync(IProgress<string> progress, CancellationToken cancellationToken)
        {
            progress.Report("İnternet bağlantısı bekleniyor...");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var ping = new System.Net.NetworkInformation.Ping();
                    var reply = await Task.Run(() => ping.Send("8.8.8.8", 3000), cancellationToken);
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        progress.Report("İnternet bağlantısı yeniden sağlandı.");
                        break;
                    }
                }
                catch { /* Bağlantı yok, beklemeye devam */ }

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
