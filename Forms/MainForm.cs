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
            InitializeCrossSellTab();

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

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }
            
            UpdateDashboard();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Tasarımı ezen dinamik yerleşimi iptal ettik, böylece
            // Visual Studio ekranındaki form (Image 1) çalışma anında da (Image 2) birebir aynı görünecektir.
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
                        
                        List<MessageLog>? loadedLogs = null;
                        bool isLoaded = false;
                        string? currentPassword = null;
                        
                        while (!isLoaded)
                        {
                            try
                            {
                                loadedLogs = await _excelService.ReadPhoneNumbersAsync(ofd.FileName, currentPassword);
                                isLoaded = true;
                            }
                            catch (Exception ex) when (ex.Message.Contains("encrypted") || ex.Message.Contains("password") || ex.Message.Contains("şifre") || ex.Message.Contains("şifrelenmiş"))
                            {
                                if (currentPassword != null)
                                {
                                    MessageBox.Show("Giriş başarısız. Girdiğiniz şifre hatalı olabilir. Lütfen şifrenin başında veya sonunda boşluk bırakmadığınıza emin olunuz.", "Hatalı Şifre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                currentPassword = PromptForPassword();
                                if (string.IsNullOrEmpty(currentPassword))
                                {
                                    Log("Şifre girişi iptal edildi.");
                                    UpdateStatus("İptal edildi.");
                                    btnLoadExcel.Enabled = true;
                                    return;
                                }
                            }
                        }

                        if (loadedLogs == null) 
                        {
                            btnLoadExcel.Enabled = true;
                            return;
                        }
                        
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

        private string PromptForPassword()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 450;
                prompt.Height = 220;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = "Şifreli Excel";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.MaximizeBox = false;

                Label textLabel = new Label() { Left = 50, Top = 20, Width = 350, Text = "Excel dosyası şifreli. Lütfen şifreyi giriniz:" };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 280, PasswordChar = '*' };
                
                CheckBox chkShowPass = new CheckBox() { Left = 340, Top = 50, Width = 80, Text = "Göster" };
                chkShowPass.CheckedChanged += (s, e) => { textBox.PasswordChar = chkShowPass.Checked ? '\0' : '*'; };

                Button confirmation = new Button() { Text = "Tamam", Left = 160, Width = 100, Top = 100, DialogResult = DialogResult.OK };
                
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(chkShowPass);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : string.Empty;
            }
        }

        private async void BtnExport_Click(object? sender, EventArgs e)
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
                if (cmbSpeed.SelectedIndex == 0) _whatsAppService!.AntiSpam.SeciliHiz = GonderimHizi.Hizli;
                else if (cmbSpeed.SelectedIndex == 2) _whatsAppService!.AntiSpam.SeciliHiz = GonderimHizi.Yavas;
                else _whatsAppService!.AntiSpam.SeciliHiz = GonderimHizi.Orta;

                await _whatsAppService!.SendMessageAsync(pendingLogs, txtMessage.Text, _attachmentPath, progress, _cancellationTokenSource.Token, useUniqueHash: chkHash.Checked);
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
                if (cmbSpeed.SelectedIndex == 0) _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Hizli;
                else if (cmbSpeed.SelectedIndex == 2) _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Yavas;
                else _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Orta;

                await _whatsAppService.SendMessageAsync(pendingLogs, txtCatMessage.Text, _catAttachmentPath, progress, _catCancellationTokenSource.Token, useUniqueHash: chkHash.Checked);
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

        // ============================================================
        // ÇAPRAZ SATIŞ VE KAMPANYA TAB - PROGRAMMATIC UI
        // ============================================================
        
        private TabPage? tabKampanya;
        private MaterialSkin.Controls.MaterialComboBox? cmbCampaignType;
        private MaterialSkin.Controls.MaterialTextBox2? txtCampaignAd;
        private MaterialSkin.Controls.MaterialMultiLineTextBox2? txtCrossSellMessage;
        private DataGridView? dgvCrossSell;
        private MaterialSkin.Controls.MaterialButton? btnKampanyaFiltrele;
        private MaterialSkin.Controls.MaterialButton? btnKampanyaGonder;
        
        private BindingList<MessageLog> _crossSellLogs = new BindingList<MessageLog>();
        private CancellationTokenSource? _crossSellCancellationTokenSource;

        private void InitializeCrossSellTab()
        {
            tabKampanya = new TabPage("Kampanya && Çapraz Satış");
            tabKampanya.BackColor = System.Drawing.Color.White;

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 100, Padding = new Padding(10) };
            
            cmbCampaignType = new MaterialSkin.Controls.MaterialComboBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 250,
                Hint = "Kampanya Türü Seçin"
            };
            cmbCampaignType.Items.AddRange(new object[] { "Trafik'ten İMM'ye", "Trafik'ten Kasko'ya", "TSS Kampanyası" });

            btnKampanyaFiltrele = new MaterialSkin.Controls.MaterialButton
            {
                Text = "Filtrele",
                Location = new System.Drawing.Point(280, 15),
                Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
            };
            btnKampanyaFiltrele.Click += BtnKampanyaFiltrele_Click;

            pnlTop.Controls.Add(cmbCampaignType);
            pnlTop.Controls.Add(btnKampanyaFiltrele);

            var pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 250, Padding = new Padding(10) };
            
            var lblAd = new Label { Text = "Sabit Reklam Metni:", Location = new System.Drawing.Point(10, 10), AutoSize = true };
            txtCampaignAd = new MaterialSkin.Controls.MaterialTextBox2
            {
                Location = new System.Drawing.Point(10, 30),
                Width = 400,
                Hint = "Örn: 5M İMM 1000TL, 3M İMM 700TL"
            };

            var lblMsg = new Label { Text = "Şablon Mesaj:", Location = new System.Drawing.Point(430, 10), AutoSize = true };
            txtCrossSellMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2
            {
                Location = new System.Drawing.Point(430, 30),
                Width = 500,
                Height = 150,
                Text = "Sayın müşterimiz, en uygun fiyat {EnUygunFiyat} ile {EnUygunSirket} şirketindendir."
            };

            btnKampanyaGonder = new MaterialSkin.Controls.MaterialButton
            {
                Text = "Kampanya Gönder",
                Location = new System.Drawing.Point(10, 100),
                Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
            };
            btnKampanyaGonder.Click += BtnKampanyaGonder_Click;

            pnlBottom.Controls.Add(lblAd);
            pnlBottom.Controls.Add(txtCampaignAd);
            pnlBottom.Controls.Add(lblMsg);
            pnlBottom.Controls.Add(txtCrossSellMessage);
            pnlBottom.Controls.Add(btnKampanyaGonder);

            dgvCrossSell = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = System.Drawing.Color.WhiteSmoke,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = _crossSellLogs
            };

            tabKampanya.Controls.Add(dgvCrossSell);
            tabKampanya.Controls.Add(pnlTop);
            tabKampanya.Controls.Add(pnlBottom);

            this.tabControl.TabPages.Add(tabKampanya);
        }

        private void BtnKampanyaFiltrele_Click(object? sender, EventArgs e)
        {
            if (_messageLogs.Count == 0)
            {
                MessageBox.Show("Lütfen önce 'Gönderim Ekranı' sekmesinden Excel yükleyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCampaignType!.SelectedIndex < 0)
            {
                MessageBox.Show("Lütfen bir kampanya türü (örn: Trafik'ten İMM'ye) seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedCampaign = cmbCampaignType.SelectedItem?.ToString() ?? "";
            
            var targets = _excelService.GetCrossSellTargets(_messageLogs.ToList(), selectedCampaign);

            _crossSellLogs.Clear();
            foreach (var log in targets)
            {
                _crossSellLogs.Add(log);
            }

            dgvCrossSell!.DataSource = null;
            dgvCrossSell.DataSource = _crossSellLogs;
            dgvCrossSell.Refresh();

            Log($"{_crossSellLogs.Count} müşteri '{selectedCampaign}' kampanyası için hedeflendi.");
        }

        private async void BtnKampanyaGonder_Click(object? sender, EventArgs e)
        {
            if (_whatsAppService == null)
            {
                MessageBox.Show("Lütfen önce 'Gönderim Ekranı' sekmesinden WhatsApp'a bağlanın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCrossSellMessage!.Text))
            {
                MessageBox.Show("Lütfen şablon mesaj kutusuna kampanya mesajınızı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pendingLogs = _crossSellLogs.Where(x => x.Status == MessageStatus.Pending || x.Status == MessageStatus.Failed).ToList();
            if (!pendingLogs.Any())
            {
                MessageBox.Show("Gönderilecek müşteri bulunamadı. Önce filtreleme yapmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string finalMessage = txtCrossSellMessage!.Text;
            if (!string.IsNullOrWhiteSpace(txtCampaignAd!.Text))
            {
                finalMessage += "\n\n" + txtCampaignAd.Text;
            }

            _crossSellCancellationTokenSource = new CancellationTokenSource();
            btnKampanyaGonder!.Enabled = false;
            btnKampanyaFiltrele!.Enabled = false;

            Log("Kampanya gönderimi başlatılıyor...");
            
            var progress = new Progress<string>(msg =>
            {
                Log(msg);
                if (msg.StartsWith("Başarılı") || msg.StartsWith("Hata"))
                {
                    dgvCrossSell!.Refresh();
                }
            });

            try
            {
                if (cmbSpeed.SelectedIndex == 0) _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Hizli;
                else if (cmbSpeed.SelectedIndex == 2) _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Yavas;
                else _whatsAppService.AntiSpam.SeciliHiz = GonderimHizi.Orta;

                await _whatsAppService.SendMessageAsync(
                    pendingLogs, 
                    finalMessage, 
                    null, 
                    progress, 
                    _crossSellCancellationTokenSource.Token, 
                    useUniqueHash: chkHash.Checked
                );
                
                Log("Kampanya gönderimi başarıyla tamamlandı!");
            }
            catch (Exception ex)
            {
                Log($"Gönderim işlemi sırasında hata alındı: {ex.Message}");
            }
            finally
            {
                btnKampanyaGonder!.Enabled = true;
                btnKampanyaFiltrele!.Enabled = true;
                _crossSellCancellationTokenSource?.Dispose();
                _crossSellCancellationTokenSource = null;
                dgvCrossSell!.Refresh();
            }
        }

        private void SetupModernLayout()
        {
            // 1. GÖNDERİM EKRANI (Split Container)
            var splitContainer = new System.Windows.Forms.SplitContainer
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Orientation = System.Windows.Forms.Orientation.Vertical,
                SplitterDistance = (int)(this.Width * 0.6),
                BackColor = System.Drawing.Color.White
            };
            
            // Mevcut kontrolleri listeye al
            var tab1Controls = new System.Collections.Generic.List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control c in tabGönderim.Controls) tab1Controls.Add(c);
            tabGönderim.Controls.Clear();
            tabGönderim.Controls.Add(splitContainer);

            // Sol Panel Alt Kısım (TableLayoutPanel)
            var pnlLeftSettings = new System.Windows.Forms.TableLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Bottom,
                ColumnCount = 2,
                AutoSize = true,
                Padding = new System.Windows.Forms.Padding(10)
            };
            pnlLeftSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            pnlLeftSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            pnlLeftSettings.Controls.Add(cmbProfile, 0, 0);
            pnlLeftSettings.Controls.Add(chkHeadless, 1, 0);
            pnlLeftSettings.Controls.Add(btnLoadExcel, 0, 1);
            pnlLeftSettings.Controls.Add(btnExport, 1, 1);
            pnlLeftSettings.Controls.Add(btnConnect, 0, 2);
            pnlLeftSettings.Controls.Add(cmbSpeed, 1, 2);
            pnlLeftSettings.Controls.Add(chkSchedule, 0, 3);
            pnlLeftSettings.Controls.Add(dtpSchedule, 1, 3);
            
            chkHash.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            pnlLeftSettings.Controls.Add(chkHash, 0, 4);
            pnlLeftSettings.SetColumnSpan(chkHash, 2);

            var pnlStartStop = new System.Windows.Forms.FlowLayoutPanel { AutoSize = true, Dock = System.Windows.Forms.DockStyle.Fill };
            pnlStartStop.Controls.Add(btnStartSending);
            pnlStartStop.Controls.Add(btnStop);
            pnlLeftSettings.Controls.Add(pnlStartStop, 0, 5);
            pnlLeftSettings.SetColumnSpan(pnlStartStop, 2);

            // Durum ve Progress
            var pnlStatus = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 60 };
            lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlStatus.Controls.Add(progressBar);
            pnlStatus.Controls.Add(lblStatus);

            dgvNumbers.Dock = System.Windows.Forms.DockStyle.Fill;

            splitContainer.Panel1.Controls.Add(dgvNumbers);
            splitContainer.Panel1.Controls.Add(pnlLeftSettings);
            splitContainer.Panel1.Controls.Add(pnlStatus);

            // Sağ Panel (Mesaj Alanı)
            var pnlRightBottom = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 300 };
            
            var flpAttachments = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Top, AutoSize = true, Padding = new System.Windows.Forms.Padding(5) };
            flpAttachments.Controls.Add(btnAttachment);
            flpAttachments.Controls.Add(btnRemoveAttachment);
            flpAttachments.Controls.Add(lblAttachment);

            rtbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            rtbLog.Height = 200;

            pnlRightBottom.Controls.Add(rtbLog);
            pnlRightBottom.Controls.Add(flpAttachments);

            txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Dock = System.Windows.Forms.DockStyle.Top;

            splitContainer.Panel2.Controls.Add(txtMessage);
            splitContainer.Panel2.Controls.Add(label1);
            splitContainer.Panel2.Controls.Add(pnlRightBottom);

            // 2. KATEGORİ GÖNDERİM TAB (Tab 2)
            var flpCatFilters = new System.Windows.Forms.FlowLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Top,
                AutoSize = true,
                Padding = new System.Windows.Forms.Padding(10),
                WrapContents = true
            };
            
            if (pnlFilterBar != null)
            {
                var filterControls = new System.Collections.Generic.List<System.Windows.Forms.Control>();
                foreach (System.Windows.Forms.Control c in pnlFilterBar.Controls) filterControls.Add(c);
                foreach (var c in filterControls) flpCatFilters.Controls.Add(c);
            }
            
            // En alt Panel (Mesaj ve İşlemler)
            var pnlCatBottom = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 350 };
            
            var pnlCatMessage = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Top, Height = 100 };
            lblCatMessage.Dock = System.Windows.Forms.DockStyle.Top;
            txtCatMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlCatMessage.Controls.Add(txtCatMessage);
            pnlCatMessage.Controls.Add(lblCatMessage);

            var flpCatAttachments = new System.Windows.Forms.FlowLayoutPanel { Dock = System.Windows.Forms.DockStyle.Top, AutoSize = true, Padding = new System.Windows.Forms.Padding(5) };
            flpCatAttachments.Controls.Add(btnCatAttachment);
            flpCatAttachments.Controls.Add(btnRemoveCatAttachment);
            flpCatAttachments.Controls.Add(lblCatAttachment);
            flpCatAttachments.Controls.Add(btnSendFiltered);
            flpCatAttachments.Controls.Add(btnStopCat);

            var pnlCatStatus = new System.Windows.Forms.Panel { Dock = System.Windows.Forms.DockStyle.Bottom, Height = 180 };
            rtbCatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            progressBarCat.Dock = System.Windows.Forms.DockStyle.Bottom;
            lblCatStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            pnlCatStatus.Controls.Add(rtbCatLog);
            pnlCatStatus.Controls.Add(progressBarCat);
            pnlCatStatus.Controls.Add(lblCatStatus);

            pnlCatBottom.Controls.Add(pnlCatMessage);
            pnlCatBottom.Controls.Add(flpCatAttachments);
            pnlCatBottom.Controls.Add(pnlCatStatus);
            pnlCatBottom.Controls.Add(rtbHelp); // Sağda
            rtbHelp.Dock = System.Windows.Forms.DockStyle.Right;
            rtbHelp.Width = 350;

            dgvFiltered.Dock = System.Windows.Forms.DockStyle.Fill;

            tabKategori.Controls.Clear();
            tabKategori.Controls.Add(dgvFiltered);
            tabKategori.Controls.Add(flpCatFilters);
            tabKategori.Controls.Add(pnlCatBottom);

            // 3. İSTATİSTİKLER (Dashboard)
            tabDashboard.Controls.Clear();
            var tlpDashboard = new System.Windows.Forms.TableLayoutPanel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Padding = new System.Windows.Forms.Padding(50)
            };
            tlpDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpDashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpDashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tlpDashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));

            tlpDashboard.Controls.Add(CreateCard(lblTotal, System.Drawing.Color.DarkBlue), 0, 0);
            tlpDashboard.Controls.Add(CreateCard(lblPending, System.Drawing.Color.DarkOrange), 1, 0);
            tlpDashboard.Controls.Add(CreateCard(lblSuccess, System.Drawing.Color.ForestGreen), 0, 1);
            tlpDashboard.Controls.Add(CreateCard(lblFailed, System.Drawing.Color.Crimson), 1, 1);

            tabDashboard.Controls.Add(tlpDashboard);

            // 4. DATAGRIDVIEW AYARLARI
            FormatGrid(dgvNumbers);
            FormatGrid(dgvFiltered);
        }

        private System.Windows.Forms.Panel CreateCard(System.Windows.Forms.Control lbl, System.Drawing.Color color)
        {
            var pnl = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Margin = new System.Windows.Forms.Padding(20),
                BackColor = System.Drawing.Color.White,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };
            lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            lbl.ForeColor = color;
            pnl.Controls.Add(lbl);
            return pnl;
        }

        private void FormatGrid(System.Windows.Forms.DataGridView dgv)
        {
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        }

    }
}
