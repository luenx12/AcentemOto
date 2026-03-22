using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        private string? _attachmentPath;
        private bool _isSending = false;

        // Kategori Gönderim tab alanları
        private BindingList<MessageLog> _filteredLogs = new BindingList<MessageLog>();
        private string? _catAttachmentPath;
        private CancellationTokenSource? _catCancellationTokenSource;

        public MainForm()
        {
            InitializeComponent();
            _repository = new MessageLogRepository();
            _excelService = new ExcelService();
            _messageLogs = new BindingList<MessageLog>();
            dgvNumbers.DataSource = _messageLogs;

            try 
            {
                btnLoadExcel.Icon = System.Drawing.Image.FromFile(@"Icons\upload.png");
                btnExport.Icon = System.Drawing.Image.FromFile(@"Icons\download.png");
                btnConnect.Icon = System.Drawing.Image.FromFile(@"Icons\connect.png");
                btnStartSending.Icon = System.Drawing.Image.FromFile(@"Icons\send.png");
                btnStop.Icon = System.Drawing.Image.FromFile(@"Icons\stop.png");
                btnAttachment.Icon = System.Drawing.Image.FromFile(@"Icons\photo.png");
            } 
            catch { }

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Red800, Primary.Red900,
                Primary.Red500, Accent.Red200, TextShade.WHITE
            );

            // Veritabanının ilk açılışta oluşturulmasını sağla
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            UpdateDashboard();
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

        private void UpdateDashboard()
        {
            if (lblTotal.InvokeRequired)
            {
                lblTotal.Invoke(new Action(UpdateDashboard));
                return;
            }

            int total = _messageLogs.Count;
            int pending = _messageLogs.Count(x => x.Status == MessageStatus.Pending);
            int success = _messageLogs.Count(x => x.Status == MessageStatus.Sent);
            int failed = _messageLogs.Count(x => x.Status == MessageStatus.Failed);

            lblTotal.Text = $"Toplam Yüklenen: {total}";
            lblPending.Text = $"Bekleyen: {pending}";
            lblSuccess.Text = $"Başarılı: {success}";
            lblFailed.Text = $"Hatalı: {failed}";
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

                        _messageLogs.Clear();
                        
                        var loadedLogs = await _excelService.ReadPhoneNumbersAsync(ofd.FileName);
                        
                        var newLogs = new List<MessageLog>();
                        foreach (var log in loadedLogs)
                        {
                            if (!_messageLogs.Any(x => x.PhoneNumber == log.PhoneNumber))
                            {
                                newLogs.Add(log);
                                _messageLogs.Add(log);
                            }
                        }

                        if (newLogs.Any())
                        {
                            await _repository.AddLogsBulkAsync(newLogs);
                        }

                        dgvNumbers.Refresh();
                        Log($"{loadedLogs.Count} numara başarıyla yüklendi.");
                        UpdateStatus("Hazır.");
                        UpdateDashboard();
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

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            if (_messageLogs.Count == 0)
            {
                MessageBox.Show("Dışa aktarılacak veri bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Dosyası|*.xlsx";
                sfd.FileName = $"Rapor_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        btnExport.Enabled = false;
                        UpdateStatus("Excel oluşturuluyor...");
                        await _excelService.ExportToExcelAsync(_messageLogs.ToList(), sfd.FileName);
                        Log("Rapor başarıyla kaydedildi.");
                        UpdateStatus("Rapor kaydedildi.");
                        MessageBox.Show("Rapor başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Dışa aktarma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log($"Hata: {ex.Message}");
                    }
                    finally
                    {
                        btnExport.Enabled = true;
                    }
                }
            }
        }

        private void BtnAttachment_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim/Video Dosyaları|*.jpg;*.jpeg;*.png;*.mp4;*.3gp|Tüm Dosyalar|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _attachmentPath = ofd.FileName;
                    lblAttachment.Text = "Eklenti: " + Path.GetFileName(_attachmentPath);
                    btnRemoveAttachment.Visible = true;
                    Log($"Eklenti seçildi: {lblAttachment.Text}");
                }
            }
        }

        private void BtnRemoveAttachment_Click(object sender, EventArgs e)
        {
            _attachmentPath = null;
            lblAttachment.Text = "Dosya Seçilmedi.";
            btnRemoveAttachment.Visible = false;
            Log("Eklenti kaldırıldı.");
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnect.Enabled = false;
                UpdateStatus("WhatsApp Web'e bağlanılıyor...");
                Log("Tarayıcı başlatılıyor...");

                _whatsAppService?.Dispose();
                _whatsAppService = new WhatsAppAutomationService(_repository);
                
                bool isHeadless = chkHeadless.Checked;
                string profileName = string.IsNullOrWhiteSpace(cmbProfile.Text) ? "DefaultProfile" : cmbProfile.Text;
                
                _whatsAppService.InitializeDriver(isHeadless, profileName);

                Log($"Kullanılan Profil: {profileName}");
                Log("Lütfen açılan tarayıcıdan QR kodu taratın (gerekliyse).");
                UpdateStatus("WhatsApp bağlantısı sağlandı/bekleniyor.");
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

        private void ChkSchedule_CheckedChanged(object sender, EventArgs e)
        {
            dtpSchedule.Enabled = chkSchedule.Checked;
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
                MessageBox.Show("Lütfen bir parametrik şablon veya mesaj metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pendingLogs = _messageLogs.Where(x => x.Status == MessageStatus.Pending || x.Status == MessageStatus.Failed).ToList();
            if (!pendingLogs.Any())
            {
                MessageBox.Show("Gönderilecek numara bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Zamanlayıcı kontrolü
            if (chkSchedule.Checked)
            {
                if (dtpSchedule.Value <= DateTime.Now)
                {
                    MessageBox.Show("Lütfen gelecekteki bir zaman seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                Log($"Gönderim işlemi saat {dtpSchedule.Value:HH:mm}'a zamanlandı. Programı açık bırakın.");
                UpdateStatus($"Zamanlandı: {dtpSchedule.Value:HH:mm}");
                btnStartSending.Enabled = false;
                btnConnect.Enabled = false;
                btnLoadExcel.Enabled = false;
                btnStop.Enabled = true;
                
                tmrSchedule.Start();
                return;
            }

            await StartAutomationProcess(pendingLogs);
        }

        private void TmrSchedule_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= dtpSchedule.Value && !_isSending)
            {
                tmrSchedule.Stop();
                chkSchedule.Checked = false;
                Log("Zamanı geldi! Gönderim otomatik olarak başlatılıyor...");
                
                var pendingLogs = _messageLogs.Where(x => x.Status == MessageStatus.Pending || x.Status == MessageStatus.Failed).ToList();
                _ = StartAutomationProcess(pendingLogs);
            }
        }

        private async Task StartAutomationProcess(List<MessageLog> pendingLogs)
        {
            _isSending = true;
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
                    UpdateDashboard();
                }
            });

            try
            {
                await _whatsAppService!.SendMessageAsync(pendingLogs, txtMessage.Text, _attachmentPath, progress, _cancellationTokenSource.Token);
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
                _isSending = false;
                btnStartSending.Enabled = true;
                btnStop.Enabled = false;
                btnConnect.Enabled = true;
                btnLoadExcel.Enabled = true;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
                dgvNumbers.Refresh();
                UpdateDashboard();
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (tmrSchedule.Enabled)
            {
                tmrSchedule.Stop();
                chkSchedule.Checked = false;
                Log("Zamanlanmış gönderim iptal edildi.");
                btnStartSending.Enabled = true;
                btnConnect.Enabled = true;
                btnLoadExcel.Enabled = true;
                btnStop.Enabled = false;
                UpdateStatus("Zamanlayıcı iptal edildi.");
                return;
            }

            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                Log("Durdurma talebi gönderildi, mevcut işlem tamamlandıktan sonra duracak...");
                _cancellationTokenSource.Cancel();
                btnStop.Enabled = false;
            }
        }

        // ============================================================
        // KATEGORİ GÖNDERİM TAB - EVENT HANDLERS
        // ============================================================

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tüm dinamik kontrolleri gizle
            dtpCatStartDate.Visible = false;
            lblCatEndDate.Visible = false;
            cmbFilterValue.Visible = false;
            txtSingleNumber.Visible = false;
            cmbCatTypeFilter.Visible = false;

            if (cmbCategory.SelectedIndex < 0) return;

            string selected = cmbCategory.SelectedItem?.ToString() ?? "";

            if (selected.Contains("Sigorta Tarihi"))
            {
                // Tarih aralığı seçimi göster
                dtpCatStartDate.Visible = true;
                dtpCatEndDate.Visible = true;
                lblCatStartDate.Visible = true;
                lblCatEndDate.Visible = true;

                // Tür filtresini de göster
                cmbCatTypeFilter.Visible = true;
                cmbCatTypeFilter.Items.Clear();
                var types = _excelService.GetDistinctValues(_messageLogs.ToList(), "Tür");
                if (types.Count == 0) types = _excelService.GetDistinctValues(_messageLogs.ToList(), "Tur");
                foreach (var t in types) cmbCatTypeFilter.Items.Add(t);
            }
            else if (selected.Contains("Durum"))
            {
                // Durum değerlerini ComboBox'a doldur
                cmbFilterValue.Visible = true;
                cmbFilterValue.Items.Clear();
                var values = _excelService.GetDistinctValues(_messageLogs.ToList(), "Durum");
                foreach (var v in values) cmbFilterValue.Items.Add(v);
                cmbFilterValue.Hint = "Durum Seçin";
            }
            else if (selected.Contains("Şirket"))
            {
                cmbFilterValue.Visible = true;
                cmbFilterValue.Items.Clear();
                var values = _excelService.GetDistinctValues(_messageLogs.ToList(), "Şirket");
                if (values.Count == 0) values = _excelService.GetDistinctValues(_messageLogs.ToList(), "Sirket");
                foreach (var v in values) cmbFilterValue.Items.Add(v);
                cmbFilterValue.Hint = "Şirket Seçin";
            }
            else if (selected.Contains("Türü"))
            {
                cmbFilterValue.Visible = true;
                cmbFilterValue.Items.Clear();
                var values = _excelService.GetDistinctValues(_messageLogs.ToList(), "Tür");
                if (values.Count == 0) values = _excelService.GetDistinctValues(_messageLogs.ToList(), "Tur");
                foreach (var v in values) cmbFilterValue.Items.Add(v);
                cmbFilterValue.Hint = "Tür Seçin";
            }
            else if (selected.Contains("Tek Numara"))
            {
                txtSingleNumber.Visible = true;
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            if (_messageLogs.Count == 0)
            {
                MessageBox.Show("Lütfen önce 'Gönderim Ekranı' sekmesinden Excel yükleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategory.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir kategori seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selected = cmbCategory.SelectedItem?.ToString() ?? "";
            List<MessageLog> result;

            if (selected.Contains("Sigorta Tarihi"))
            {
                result = _excelService.FilterByCategory(
                    _messageLogs.ToList(),
                    FilterCategory.SigortaTarihi,
                    startDate: dtpCatStartDate.Value,
                    endDate: dtpCatEndDate.Value,
                    typeFilter: cmbCatTypeFilter.SelectedItem?.ToString());
            }
            else if (selected.Contains("Durum"))
            {
                if (cmbFilterValue.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen bir durum değeri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                result = _excelService.FilterByCategory(
                    _messageLogs.ToList(),
                    FilterCategory.Durum,
                    filterValue: cmbFilterValue.SelectedItem?.ToString());
            }
            else if (selected.Contains("Şirket"))
            {
                if (cmbFilterValue.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen bir şirket seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                result = _excelService.FilterByCategory(
                    _messageLogs.ToList(),
                    FilterCategory.Sirket,
                    filterValue: cmbFilterValue.SelectedItem?.ToString());
            }
            else if (selected.Contains("Türü"))
            {
                if (cmbFilterValue.SelectedIndex < 0)
                {
                    MessageBox.Show("Lütfen bir tür seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                result = _excelService.FilterByCategory(
                    _messageLogs.ToList(),
                    FilterCategory.Turu,
                    filterValue: cmbFilterValue.SelectedItem?.ToString());
                
                // Eğer tarih filtresi seçilirse Tür filtresini de beraber kullanmak istiyorsa diye opsiyonel bırakıyoruz ama direkt Tür seçildiyse zaten result dönüyor.
            }
            else if (selected.Contains("Tek Numara"))
            {
                // Tek numara modunda: filtre yok, doğrudan gönderim olacak
                if (string.IsNullOrWhiteSpace(txtSingleNumber.Text))
                {
                    MessageBox.Show("Lütfen bir telefon numarası girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string formattedNumber = AcentemOto.Extensions.PhoneNumberExtensions.FormatTurkishPhone(txtSingleNumber.Text.Trim());
                var singleLog = new MessageLog
                {
                    PhoneNumber = formattedNumber,
                    Status = MessageStatus.Pending
                };
                result = new List<MessageLog> { singleLog };
            }
            else
            {
                result = new List<MessageLog>();
            }

            _filteredLogs.Clear();
            foreach (var log in result)
            {
                _filteredLogs.Add(log);
            }

            dgvFiltered.DataSource = null;
            dgvFiltered.DataSource = _filteredLogs;
            dgvFiltered.Refresh();

            lblFilteredCount.Text = $"📋 Filtrelenen: {_filteredLogs.Count} kayıt";
            CatLog($"{_filteredLogs.Count} kayıt filtrelendi.");
        }

        private async void BtnSendFiltered_Click(object sender, EventArgs e)
        {
            if (_whatsAppService == null)
            {
                MessageBox.Show("Lütfen önce 'Gönderim Ekranı' sekmesinden WhatsApp'a bağlanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCatMessage.Text))
            {
                MessageBox.Show("Lütfen bir mesaj metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pendingLogs = _filteredLogs.Where(x => x.Status == MessageStatus.Pending || x.Status == MessageStatus.Failed).ToList();
            if (!pendingLogs.Any())
            {
                MessageBox.Show("Gönderilecek numara bulunamadı. Önce filtreleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _catCancellationTokenSource = new CancellationTokenSource();
            btnSendFiltered.Enabled = false;
            btnFilter.Enabled = false;
            btnStopCat.Enabled = true;
            progressBarCat.Maximum = pendingLogs.Count;
            progressBarCat.Value = 0;

            CatLog("Kategori gönderimi başlatıldı...");
            UpdateCatStatus("Gönderim devam ediyor.");

            var progress = new Progress<string>(msg =>
            {
                CatLog(msg);
                if (msg.StartsWith("Başarılı") || msg.StartsWith("Hata"))
                {
                    progressBarCat.Value = Math.Min(progressBarCat.Value + 1, progressBarCat.Maximum);
                    dgvFiltered.Refresh();
                }
            });

            try
            {
                await _whatsAppService.SendMessageAsync(pendingLogs, txtCatMessage.Text, _catAttachmentPath, progress, _catCancellationTokenSource.Token);
                CatLog("Tüm gönderim işlemleri tamamlandı.");
                UpdateCatStatus("Tamamlandı.");
            }
            catch (Exception ex)
            {
                CatLog($"Gönderim hatası: {ex.Message}");
                UpdateCatStatus("Hata.");
            }
            finally
            {
                btnSendFiltered.Enabled = true;
                btnFilter.Enabled = true;
                btnStopCat.Enabled = false;
                _catCancellationTokenSource?.Dispose();
                _catCancellationTokenSource = null;
                dgvFiltered.Refresh();
            }
        }

        private void BtnCatAttachment_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim/Video Dosyaları|*.jpg;*.jpeg;*.png;*.mp4;*.3gp|Tüm Dosyalar|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _catAttachmentPath = ofd.FileName;
                    lblCatAttachment.Text = "Eklenti: " + Path.GetFileName(_catAttachmentPath);
                    btnRemoveCatAttachment.Visible = true;
                    CatLog($"Eklenti seçildi: {lblCatAttachment.Text}");
                }
            }
        }

        private void BtnRemoveCatAttachment_Click(object sender, EventArgs e)
        {
            _catAttachmentPath = null;
            lblCatAttachment.Text = "Dosya Seçilmedi.";
            btnRemoveCatAttachment.Visible = false;
            CatLog("Eklenti kaldırıldı.");
        }

        private void CatLog(string message)
        {
            if (rtbCatLog.InvokeRequired)
            {
                rtbCatLog.Invoke(new Action(() => CatLog(message)));
                return;
            }

            rtbCatLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbCatLog.SelectionStart = rtbCatLog.Text.Length;
            rtbCatLog.ScrollToCaret();
        }

        private void UpdateCatStatus(string status)
        {
            if (lblCatStatus.InvokeRequired)
            {
                lblCatStatus.Invoke(new Action(() => UpdateCatStatus(status)));
                return;
            }
            lblCatStatus.Text = $"Durum: {status}";
        }

        private void BtnStopCat_Click(object sender, EventArgs e)
        {
            _catCancellationTokenSource?.Cancel();
            CatLog("Gönderim durduruldu.");
            UpdateCatStatus("Durduruldu.");
            btnStopCat.Enabled = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }

                if (_catCancellationTokenSource != null && !_catCancellationTokenSource.IsCancellationRequested)
                {
                    _catCancellationTokenSource.Cancel();
                    _catCancellationTokenSource.Dispose();
                }

                _whatsAppService?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kapanırken hata oluştu: {ex.Message}");
            }
        }
    }
}
