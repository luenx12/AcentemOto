using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcentemOto.Data;
using AcentemOto.Models;
using AcentemOto.Services;

using MaterialSkin;
using MaterialSkin.Controls;

namespace AcentemOto.Forms
{
    public partial class MainForm : MaterialForm
    {
        private readonly IMessageLogRepository _repository;
        private readonly ExcelService _excelService;
        private WhatsAppAutomationService? _whatsAppService;

        private BindingList<MessageLog> _messageLogs;
        private CancellationTokenSource? _cancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();
            _repository = new MessageLogRepository();
            _excelService = new ExcelService();
            _messageLogs = new BindingList<MessageLog>();
            dgvNumbers.DataSource = _messageLogs;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red800, Primary.Red900,
                Primary.Red500, Accent.Red200, TextShade.WHITE
            );
            
            // Yükleme anında varsa eski listeyi veritabanından getir (KALDIRILDI - Sadece yeni Excel'dekiler gösterilecek)
            // _ = LoadExistingLogsAsync();
        }

        private async Task LoadExistingLogsAsync()
        {
            try
            {
                var existingLogs = await _repository.GetAllLogsAsync();
                foreach (var log in existingLogs)
                {
                    _messageLogs.Add(log);
                }
                UpdateStatus($"Veritabanından {existingLogs.Count} kayıt yüklendi.");
            }
            catch (Exception ex)
            {
                Log($"Veritabanı yükleme hatası: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => Log(message)));
                return;
            }

            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        private void UpdateStatus(string status)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(() => UpdateStatus(status)));
                return;
            }
            lblStatus.Text = $"Durum: {status}";
        }

        private async void BtnLoadExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Dosyaları|*.xlsx";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        btnLoadExcel.Enabled = false;
                        UpdateStatus("Excel okunuyor...");

                        // Eski yüklenenleri ekrandan temizle
                        _messageLogs.Clear();
                        
                        var loadedLogs = await _excelService.ReadPhoneNumbersAsync(ofd.FileName);
                        
                        // Olanların üzerine ekle, veritabanına kaydet
                        foreach (var log in loadedLogs)
                        {
                            if (!_messageLogs.Any(x => x.PhoneNumber == log.PhoneNumber))
                            {
                                await _repository.AddLogAsync(log);
                                _messageLogs.Add(log);
                            }
                        }

                        dgvNumbers.Refresh();
                        Log($"{loadedLogs.Count} numara başarıyla yüklendi.");
                        UpdateStatus("Hazır.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Excel okuma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log($"Hata: {ex.Message}");
                        UpdateStatus("Hata oluştu.");
                    }
                    finally
                    {
                        btnLoadExcel.Enabled = true;
                    }
                }
            }
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                UpdateStatus("WhatsApp Web'e bağlanılıyor...");
                Log("Tarayıcı başlatılıyor...");

                _whatsAppService?.Dispose(); // Varsa eskisini kapat
                _whatsAppService = new WhatsAppAutomationService(_repository);
                
                bool isHeadless = chkHeadless.Checked;
                _whatsAppService.InitializeDriver(isHeadless);

                Log("Lütfen açılan tarayıcıdan QR kodu taratın (gerekliyse).");
                UpdateStatus("WhatsApp bağlantısı bekleniyor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log($"Bağlantı Hatası: {ex.Message}");
                UpdateStatus("Hata oluştu.");
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private async void BtnStartSending_Click(object sender, EventArgs e)
        {
            if (_whatsAppService == null)
            {
                MessageBox.Show("Lütfen önce WhatsApp'a bağlanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Lütfen bir mesaj metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pendingLogs = _messageLogs.Where(x => x.Status == MessageStatus.Pending || x.Status == MessageStatus.Failed).ToList();
            if (!pendingLogs.Any())
            {
                MessageBox.Show("Gönderilecek numara bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            
            btnStartSending.Enabled = false;
            btnStop.Enabled = true;
            progressBar.Maximum = pendingLogs.Count;
            progressBar.Value = 0;
            
            Log("Gönderim başlatıldı...");
            UpdateStatus("Gönderim devam ediyor.");

            var progress = new Progress<string>(msg =>
            {
                Log(msg);
                if (msg.StartsWith("Başarılı") || msg.StartsWith("Hata"))
                {
                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                    dgvNumbers.Refresh();
                }
            });

            try
            {
                await _whatsAppService.SendMessageAsync(pendingLogs, txtMessage.Text, progress, _cancellationTokenSource.Token);
                Log("Tüm gönderim işlemleri tamamlandı.");
                UpdateStatus("Tamamlandı.");
            }
            catch (Exception ex)
            {
                Log($"Gönderim işlemi iptal edildi veya hata oluştu: {ex.Message}");
                UpdateStatus("Durduruldu / Hata.");
            }
            finally
            {
                btnStartSending.Enabled = true;
                btnStop.Enabled = false;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                dgvNumbers.Refresh();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                Log("Durdurma talebi gönderildi, mevcut işlem tamamlandıktan sonra duracak...");
                _cancellationTokenSource.Cancel();
                btnStop.Enabled = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _whatsAppService?.Dispose();
        }
    }
}
