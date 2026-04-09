namespace AcentemOto.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tabControl = new MaterialSkin.Controls.MaterialTabControl();
            tabGönderim = new TabPage();
            cmbProfile = new MaterialSkin.Controls.MaterialComboBox();
            btnLoadExcel = new MaterialSkin.Controls.MaterialButton();
            btnExport = new MaterialSkin.Controls.MaterialButton();
            btnConnect = new MaterialSkin.Controls.MaterialButton();
            chkHeadless = new MaterialSkin.Controls.MaterialCheckbox();
            cmbSpeed = new MaterialSkin.Controls.MaterialComboBox();
            chkHash = new MaterialSkin.Controls.MaterialCheckbox();
            btnStartSending = new MaterialSkin.Controls.MaterialButton();
            btnStop = new MaterialSkin.Controls.MaterialButton();
            chkSchedule = new MaterialSkin.Controls.MaterialCheckbox();
            dtpSchedule = new DateTimePicker();
            btnAttachment = new MaterialSkin.Controls.MaterialButton();
            lblAttachment = new MaterialSkin.Controls.MaterialLabel();
            btnRemoveAttachment = new MaterialSkin.Controls.MaterialButton();
            label1 = new MaterialSkin.Controls.MaterialLabel();
            txtMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            rtbLog = new RichTextBox();
            dgvNumbers = new DataGridView();
            progressBar = new MaterialSkin.Controls.MaterialProgressBar();
            lblStatus = new MaterialSkin.Controls.MaterialLabel();
            tabKategori = new TabPage();
            pnlFilterBar = new Panel();
            cmbCategory = new MaterialSkin.Controls.MaterialComboBox();
            cmbCatTypeFilter = new MaterialSkin.Controls.MaterialComboBox();
            lblCatStartDate = new MaterialSkin.Controls.MaterialLabel();
            dtpCatStartDate = new DateTimePicker();
            lblCatEndDate = new MaterialSkin.Controls.MaterialLabel();
            dtpCatEndDate = new DateTimePicker();
            cmbFilterValue = new MaterialSkin.Controls.MaterialComboBox();
            txtSingleNumber = new MaterialSkin.Controls.MaterialTextBox2();
            btnFilter = new MaterialSkin.Controls.MaterialButton();
            dgvFiltered = new DataGridView();
            btnSendFiltered = new MaterialSkin.Controls.MaterialButton();
            btnStopCat = new MaterialSkin.Controls.MaterialButton();
            btnRemoveCatAttachment = new MaterialSkin.Controls.MaterialButton();
            lblFilteredCount = new MaterialSkin.Controls.MaterialLabel();
            lblCatMessage = new MaterialSkin.Controls.MaterialLabel();
            txtCatMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            btnCatAttachment = new MaterialSkin.Controls.MaterialButton();
            lblCatAttachment = new MaterialSkin.Controls.MaterialLabel();
            rtbHelp = new RichTextBox();
            rtbCatLog = new RichTextBox();
            progressBarCat = new MaterialSkin.Controls.MaterialProgressBar();
            lblCatStatus = new MaterialSkin.Controls.MaterialLabel();
            tabDashboard = new TabPage();
            lblTotal = new MaterialSkin.Controls.MaterialLabel();
            lblPending = new MaterialSkin.Controls.MaterialLabel();
            lblSuccess = new MaterialSkin.Controls.MaterialLabel();
            lblFailed = new MaterialSkin.Controls.MaterialLabel();
            picLogo = new PictureBox();
            tmrSchedule = new System.Windows.Forms.Timer(components);
            lblCategoryTitle = new MaterialSkin.Controls.MaterialLabel();
            tabControl.SuspendLayout();
            tabGönderim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNumbers).BeginInit();
            tabKategori.SuspendLayout();
            pnlFilterBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiltered).BeginInit();
            tabDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(tabGönderim);
            tabControl.Controls.Add(tabKategori);
            tabControl.Controls.Add(tabDashboard);
            tabControl.Depth = 0;
            tabControl.Location = new Point(12, 149);
            tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1244, 801);
            tabControl.TabIndex = 1;
            // 
            // tabGönderim
            // 
            tabGönderim.BackColor = Color.White;
            tabGönderim.Controls.Add(cmbProfile);
            tabGönderim.Controls.Add(btnLoadExcel);
            tabGönderim.Controls.Add(btnExport);
            tabGönderim.Controls.Add(btnConnect);
            tabGönderim.Controls.Add(chkHeadless);
            tabGönderim.Controls.Add(cmbSpeed);
            tabGönderim.Controls.Add(chkHash);
            tabGönderim.Controls.Add(btnStartSending);
            tabGönderim.Controls.Add(btnStop);
            tabGönderim.Controls.Add(chkSchedule);
            tabGönderim.Controls.Add(dtpSchedule);
            tabGönderim.Controls.Add(btnAttachment);
            tabGönderim.Controls.Add(lblAttachment);
            tabGönderim.Controls.Add(btnRemoveAttachment);
            tabGönderim.Controls.Add(label1);
            tabGönderim.Controls.Add(txtMessage);
            tabGönderim.Controls.Add(rtbLog);
            tabGönderim.Controls.Add(dgvNumbers);
            tabGönderim.Controls.Add(progressBar);
            tabGönderim.Controls.Add(lblStatus);
            tabGönderim.Location = new Point(4, 26);
            tabGönderim.Name = "tabGönderim";
            tabGönderim.Padding = new Padding(3);
            tabGönderim.Size = new Size(1236, 771);
            tabGönderim.TabIndex = 0;
            tabGönderim.Text = "Gönderim Ekranı";
            // 
            // cmbProfile
            // 
            cmbProfile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbProfile.AutoResize = false;
            cmbProfile.BackColor = Color.FromArgb(255, 255, 255);
            cmbProfile.Depth = 0;
            cmbProfile.DrawMode = DrawMode.OwnerDrawVariable;
            cmbProfile.DropDownHeight = 174;
            cmbProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProfile.DropDownWidth = 121;
            cmbProfile.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbProfile.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbProfile.FormattingEnabled = true;
            cmbProfile.Hint = "WhatsApp Profili Seçin";
            cmbProfile.IntegralHeight = false;
            cmbProfile.ItemHeight = 43;
            cmbProfile.Items.AddRange(new object[] { "Varsayılan (Default)", "Satış Hattı", "Destek Hattı", "Profil 4" });
            cmbProfile.Location = new Point(15, 546);
            cmbProfile.MaxDropDownItems = 4;
            cmbProfile.MouseState = MaterialSkin.MouseState.OUT;
            cmbProfile.Name = "cmbProfile";
            cmbProfile.Size = new Size(280, 49);
            cmbProfile.StartIndex = 0;
            cmbProfile.TabIndex = 20;
            // 
            // btnLoadExcel
            // 
            btnLoadExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLoadExcel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLoadExcel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnLoadExcel.Depth = 0;
            btnLoadExcel.HighEmphasis = true;
            btnLoadExcel.Icon = null;
            btnLoadExcel.Location = new Point(15, 656);
            btnLoadExcel.Margin = new Padding(4, 6, 4, 6);
            btnLoadExcel.MouseState = MaterialSkin.MouseState.HOVER;
            btnLoadExcel.Name = "btnLoadExcel";
            btnLoadExcel.NoAccentTextColor = Color.Empty;
            btnLoadExcel.Size = new Size(117, 36);
            btnLoadExcel.TabIndex = 1;
            btnLoadExcel.Text = " Excel Yükle ";
            btnLoadExcel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnLoadExcel.UseAccentColor = false;
            btnLoadExcel.UseVisualStyleBackColor = true;
            btnLoadExcel.Click += BtnLoadExcel_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnExport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExport.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnExport.Depth = 0;
            btnExport.HighEmphasis = true;
            btnExport.Icon = null;
            btnExport.Location = new Point(165, 656);
            btnExport.Margin = new Padding(4, 6, 4, 6);
            btnExport.MouseState = MaterialSkin.MouseState.HOVER;
            btnExport.Name = "btnExport";
            btnExport.NoAccentTextColor = Color.Empty;
            btnExport.Size = new Size(94, 36);
            btnExport.TabIndex = 21;
            btnExport.Text = " Rapor Al ";
            btnExport.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnExport.UseAccentColor = false;
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnConnect.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnConnect.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnConnect.Depth = 0;
            btnConnect.HighEmphasis = true;
            btnConnect.Icon = null;
            btnConnect.Location = new Point(15, 606);
            btnConnect.Margin = new Padding(4, 6, 4, 6);
            btnConnect.MouseState = MaterialSkin.MouseState.HOVER;
            btnConnect.Name = "btnConnect";
            btnConnect.NoAccentTextColor = Color.Empty;
            btnConnect.Size = new Size(162, 36);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "WhatsApp Bağlan";
            btnConnect.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnConnect.UseAccentColor = false;
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;
            // 
            // chkHeadless
            // 
            chkHeadless.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkHeadless.AutoSize = true;
            chkHeadless.Depth = 0;
            chkHeadless.Location = new Point(320, 552);
            chkHeadless.Margin = new Padding(0);
            chkHeadless.MouseLocation = new Point(-1, -1);
            chkHeadless.MouseState = MaterialSkin.MouseState.HOVER;
            chkHeadless.Name = "chkHeadless";
            chkHeadless.ReadOnly = false;
            chkHeadless.Ripple = true;
            chkHeadless.Size = new Size(182, 37);
            chkHeadless.TabIndex = 5;
            chkHeadless.Text = "Headless Mod (Gizli)";
            chkHeadless.UseVisualStyleBackColor = true;
            // 
            // cmbSpeed
            // 
            cmbSpeed.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cmbSpeed.AutoResize = false;
            cmbSpeed.BackColor = Color.FromArgb(255, 255, 255);
            cmbSpeed.Depth = 0;
            cmbSpeed.DrawMode = DrawMode.OwnerDrawVariable;
            cmbSpeed.DropDownHeight = 174;
            cmbSpeed.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSpeed.DropDownWidth = 121;
            cmbSpeed.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            cmbSpeed.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbSpeed.FormattingEnabled = true;
            cmbSpeed.Hint = "Gönderim Hızı Seçin";
            cmbSpeed.IntegralHeight = false;
            cmbSpeed.ItemHeight = 43;
            cmbSpeed.Items.AddRange(new object[] { "Hızlı (5-11 sn)", "Orta (12-29 sn) - Önerilen", "Yavaş (25-41 sn)" });
            cmbSpeed.Location = new Point(320, 595);
            cmbSpeed.MaxDropDownItems = 4;
            cmbSpeed.MouseState = MaterialSkin.MouseState.OUT;
            cmbSpeed.Name = "cmbSpeed";
            cmbSpeed.Size = new Size(260, 49);
            cmbSpeed.StartIndex = 1;
            cmbSpeed.TabIndex = 21;
            // 
            // chkHash
            // 
            chkHash.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkHash.AutoSize = true;
            chkHash.Depth = 0;
            chkHash.Location = new Point(320, 706);
            chkHash.Margin = new Padding(0);
            chkHash.MouseLocation = new Point(-1, -1);
            chkHash.MouseState = MaterialSkin.MouseState.HOVER;
            chkHash.Name = "chkHash";
            chkHash.ReadOnly = false;
            chkHash.Ripple = true;
            chkHash.Size = new Size(370, 37);
            chkHash.TabIndex = 22;
            chkHash.Text = "Mesajın sonuna benzersiz kod ekle (Anti-Spam)";
            chkHash.UseVisualStyleBackColor = true;
            // 
            // btnStartSending
            // 
            btnStartSending.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStartSending.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStartSending.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnStartSending.Depth = 0;
            btnStartSending.HighEmphasis = true;
            btnStartSending.Icon = null;
            btnStartSending.Location = new Point(15, 706);
            btnStartSending.Margin = new Padding(4, 6, 4, 6);
            btnStartSending.MouseState = MaterialSkin.MouseState.HOVER;
            btnStartSending.Name = "btnStartSending";
            btnStartSending.NoAccentTextColor = Color.Empty;
            btnStartSending.Size = new Size(162, 36);
            btnStartSending.TabIndex = 3;
            btnStartSending.Text = " Gönderimi Başlat ";
            btnStartSending.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnStartSending.UseAccentColor = false;
            btnStartSending.UseVisualStyleBackColor = true;
            btnStartSending.Click += BtnStartSending_Click;
            // 
            // btnStop
            // 
            btnStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStop.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStop.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnStop.Depth = 0;
            btnStop.Enabled = false;
            btnStop.HighEmphasis = true;
            btnStop.Icon = null;
            btnStop.Location = new Point(185, 706);
            btnStop.Margin = new Padding(4, 6, 4, 6);
            btnStop.MouseState = MaterialSkin.MouseState.HOVER;
            btnStop.Name = "btnStop";
            btnStop.NoAccentTextColor = Color.Empty;
            btnStop.Size = new Size(82, 36);
            btnStop.TabIndex = 4;
            btnStop.Text = " Durdur ";
            btnStop.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnStop.UseAccentColor = true;
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += BtnStop_Click;
            // 
            // chkSchedule
            // 
            chkSchedule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            chkSchedule.AutoSize = true;
            chkSchedule.Depth = 0;
            chkSchedule.Location = new Point(320, 655);
            chkSchedule.Margin = new Padding(0);
            chkSchedule.MouseLocation = new Point(-1, -1);
            chkSchedule.MouseState = MaterialSkin.MouseState.HOVER;
            chkSchedule.Name = "chkSchedule";
            chkSchedule.ReadOnly = false;
            chkSchedule.Ripple = true;
            chkSchedule.Size = new Size(127, 37);
            chkSchedule.TabIndex = 22;
            chkSchedule.Text = "Zamanlayıcı:";
            chkSchedule.UseVisualStyleBackColor = true;
            chkSchedule.CheckedChanged += ChkSchedule_CheckedChanged;
            // 
            // dtpSchedule
            // 
            dtpSchedule.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            dtpSchedule.Enabled = false;
            dtpSchedule.Font = new Font("Segoe UI", 12F);
            dtpSchedule.Format = DateTimePickerFormat.Time;
            dtpSchedule.Location = new Point(460, 660);
            dtpSchedule.Name = "dtpSchedule";
            dtpSchedule.ShowUpDown = true;
            dtpSchedule.Size = new Size(120, 29);
            dtpSchedule.TabIndex = 23;
            // 
            // btnAttachment
            // 
            btnAttachment.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAttachment.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnAttachment.Depth = 0;
            btnAttachment.HighEmphasis = true;
            btnAttachment.Icon = null;
            btnAttachment.Location = new Point(740, 451);
            btnAttachment.Margin = new Padding(4, 6, 4, 6);
            btnAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            btnAttachment.Name = "btnAttachment";
            btnAttachment.NoAccentTextColor = Color.Empty;
            btnAttachment.Size = new Size(107, 36);
            btnAttachment.TabIndex = 24;
            btnAttachment.Text = "Görsel Seç";
            btnAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnAttachment.UseAccentColor = false;
            btnAttachment.UseVisualStyleBackColor = true;
            btnAttachment.Click += BtnAttachment_Click;
            // 
            // lblAttachment
            // 
            lblAttachment.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lblAttachment.AutoEllipsis = true;
            lblAttachment.AutoSize = true;
            lblAttachment.Depth = 0;
            lblAttachment.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblAttachment.Location = new Point(934, 461);
            lblAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            lblAttachment.Name = "lblAttachment";
            lblAttachment.Size = new Size(123, 19);
            lblAttachment.TabIndex = 25;
            lblAttachment.Text = "Dosya Seçilmedi.";
            // 
            // btnRemoveAttachment
            // 
            btnRemoveAttachment.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRemoveAttachment.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRemoveAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRemoveAttachment.Depth = 0;
            btnRemoveAttachment.HighEmphasis = true;
            btnRemoveAttachment.Icon = null;
            btnRemoveAttachment.Location = new Point(849, 451);
            btnRemoveAttachment.Margin = new Padding(4, 6, 4, 6);
            btnRemoveAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            btnRemoveAttachment.Name = "btnRemoveAttachment";
            btnRemoveAttachment.NoAccentTextColor = Color.Empty;
            btnRemoveAttachment.Size = new Size(75, 36);
            btnRemoveAttachment.TabIndex = 26;
            btnRemoveAttachment.Text = " Kaldır ";
            btnRemoveAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRemoveAttachment.UseAccentColor = true;
            btnRemoveAttachment.UseVisualStyleBackColor = true;
            btnRemoveAttachment.Visible = false;
            btnRemoveAttachment.Click += BtnRemoveAttachment_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Depth = 0;
            label1.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            label1.Location = new Point(734, 15);
            label1.MouseState = MaterialSkin.MouseState.HOVER;
            label1.Name = "label1";
            label1.Size = new Size(143, 19);
            label1.TabIndex = 6;
            label1.Text = "Gönderilecek Mesaj:";
            // 
            // txtMessage
            // 
            txtMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            txtMessage.AnimateReadOnly = false;
            txtMessage.BackgroundImageLayout = ImageLayout.None;
            txtMessage.CharacterCasing = CharacterCasing.Normal;
            txtMessage.Cursor = Cursors.IBeam;
            txtMessage.Depth = 0;
            txtMessage.HideSelection = true;
            txtMessage.Location = new Point(734, 45);
            txtMessage.MaxLength = 32767;
            txtMessage.MouseState = MaterialSkin.MouseState.OUT;
            txtMessage.Name = "txtMessage";
            txtMessage.PasswordChar = '\0';
            txtMessage.ReadOnly = false;
            txtMessage.ScrollBars = ScrollBars.Vertical;
            txtMessage.SelectedText = "";
            txtMessage.SelectionLength = 0;
            txtMessage.SelectionStart = 0;
            txtMessage.ShortcutsEnabled = true;
            txtMessage.Size = new Size(480, 396);
            txtMessage.TabIndex = 7;
            txtMessage.TabStop = false;
            txtMessage.Text = "Sayın {İsim}, {Plaka} plakalı aracınızın trafik sigortası bitmektedir.";
            txtMessage.TextAlign = HorizontalAlignment.Left;
            txtMessage.UseSystemPasswordChar = false;
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            rtbLog.BackColor = Color.FromArgb(40, 40, 40);
            rtbLog.Font = new Font("Consolas", 10F);
            rtbLog.ForeColor = Color.LightGreen;
            rtbLog.Location = new Point(734, 501);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(480, 240);
            rtbLog.TabIndex = 8;
            rtbLog.Text = "";
            // 
            // dgvNumbers
            // 
            dgvNumbers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvNumbers.BackgroundColor = Color.WhiteSmoke;
            dgvNumbers.BorderStyle = BorderStyle.None;
            dgvNumbers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(211, 21, 21);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvNumbers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvNumbers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNumbers.EnableHeadersVisualStyles = false;
            dgvNumbers.Location = new Point(15, 15);
            dgvNumbers.Name = "dgvNumbers";
            dgvNumbers.Size = new Size(694, 516);
            dgvNumbers.TabIndex = 0;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Depth = 0;
            progressBar.Location = new Point(15, 751);
            progressBar.MouseState = MaterialSkin.MouseState.HOVER;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1199, 5);
            progressBar.TabIndex = 9;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Depth = 0;
            lblStatus.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            lblStatus.Location = new Point(15, 761);
            lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(114, 17);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Durum: Bekliyor...";
            // 
            // tabKategori
            // 
            tabKategori.BackColor = Color.FromArgb(250, 250, 252);
            tabKategori.Controls.Add(pnlFilterBar);
            tabKategori.Controls.Add(dgvFiltered);
            tabKategori.Controls.Add(btnSendFiltered);
            tabKategori.Controls.Add(btnStopCat);
            tabKategori.Controls.Add(btnRemoveCatAttachment);
            tabKategori.Controls.Add(lblFilteredCount);
            tabKategori.Controls.Add(lblCatMessage);
            tabKategori.Controls.Add(txtCatMessage);
            tabKategori.Controls.Add(btnCatAttachment);
            tabKategori.Controls.Add(lblCatAttachment);
            tabKategori.Controls.Add(rtbHelp);
            tabKategori.Controls.Add(rtbCatLog);
            tabKategori.Controls.Add(progressBarCat);
            tabKategori.Controls.Add(lblCatStatus);
            tabKategori.Location = new Point(4, 24);
            tabKategori.Name = "tabKategori";
            tabKategori.Padding = new Padding(3);
            tabKategori.Size = new Size(1220, 734);
            tabKategori.TabIndex = 2;
            tabKategori.Text = "Kategori Gönderim";
            // 
            // pnlFilterBar
            // 
            pnlFilterBar.BackColor = Color.FromArgb(240, 240, 245);
            pnlFilterBar.Controls.Add(cmbCategory);
            pnlFilterBar.Controls.Add(cmbCatTypeFilter);
            pnlFilterBar.Controls.Add(lblCatStartDate);
            pnlFilterBar.Controls.Add(dtpCatStartDate);
            pnlFilterBar.Controls.Add(lblCatEndDate);
            pnlFilterBar.Controls.Add(dtpCatEndDate);
            pnlFilterBar.Controls.Add(cmbFilterValue);
            pnlFilterBar.Controls.Add(txtSingleNumber);
            pnlFilterBar.Controls.Add(btnFilter);
            pnlFilterBar.Location = new Point(10, 8);
            pnlFilterBar.Name = "pnlFilterBar";
            pnlFilterBar.Size = new Size(1145, 80);
            pnlFilterBar.TabIndex = 50;
            // 
            // cmbCategory
            // 
            cmbCategory.AutoResize = false;
            cmbCategory.BackColor = Color.White;
            cmbCategory.Depth = 0;
            cmbCategory.DrawMode = DrawMode.OwnerDrawVariable;
            cmbCategory.DropDownHeight = 217;
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.DropDownWidth = 230;
            cmbCategory.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            cmbCategory.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Hint = "Kategori Seçin";
            cmbCategory.IntegralHeight = false;
            cmbCategory.ItemHeight = 43;
            cmbCategory.Items.AddRange(new object[] { "📅 Sigorta Tarihi (Aralık)", "📊 Durum", "🏢 Şirket", "📁 Türü (Trafik/Dask/Kasko)", "📱 Tek Numara Gönderim" });
            cmbCategory.Location = new Point(10, 25);
            cmbCategory.MaxDropDownItems = 5;
            cmbCategory.MouseState = MaterialSkin.MouseState.OUT;
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(280, 49);
            cmbCategory.StartIndex = 0;
            cmbCategory.TabIndex = 31;
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            // 
            // cmbCatTypeFilter
            // 
            cmbCatTypeFilter.AutoResize = false;
            cmbCatTypeFilter.BackColor = Color.White;
            cmbCatTypeFilter.Depth = 0;
            cmbCatTypeFilter.DrawMode = DrawMode.OwnerDrawVariable;
            cmbCatTypeFilter.DropDownHeight = 174;
            cmbCatTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCatTypeFilter.DropDownWidth = 200;
            cmbCatTypeFilter.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            cmbCatTypeFilter.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbCatTypeFilter.FormattingEnabled = true;
            cmbCatTypeFilter.Hint = "Tür Seçin (Opsiyonel)";
            cmbCatTypeFilter.IntegralHeight = false;
            cmbCatTypeFilter.ItemHeight = 43;
            cmbCatTypeFilter.Location = new Point(660, 25);
            cmbCatTypeFilter.MaxDropDownItems = 4;
            cmbCatTypeFilter.MouseState = MaterialSkin.MouseState.OUT;
            cmbCatTypeFilter.Name = "cmbCatTypeFilter";
            cmbCatTypeFilter.Size = new Size(230, 49);
            cmbCatTypeFilter.StartIndex = -1;
            cmbCatTypeFilter.TabIndex = 36;
            cmbCatTypeFilter.Visible = false;
            // 
            // lblCatStartDate
            // 
            lblCatStartDate.AutoSize = true;
            lblCatStartDate.Depth = 0;
            lblCatStartDate.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblCatStartDate.Location = new Point(310, 5);
            lblCatStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblCatStartDate.Name = "lblCatStartDate";
            lblCatStartDate.Size = new Size(120, 19);
            lblCatStartDate.TabIndex = 32;
            lblCatStartDate.Text = "Başlangıç Tarihi:";
            lblCatStartDate.Visible = false;
            // 
            // dtpCatStartDate
            // 
            dtpCatStartDate.Font = new Font("Segoe UI", 12F);
            dtpCatStartDate.Format = DateTimePickerFormat.Short;
            dtpCatStartDate.Location = new Point(310, 32);
            dtpCatStartDate.Name = "dtpCatStartDate";
            dtpCatStartDate.Size = new Size(155, 29);
            dtpCatStartDate.TabIndex = 33;
            dtpCatStartDate.Visible = false;
            // 
            // lblCatEndDate
            // 
            lblCatEndDate.AutoSize = true;
            lblCatEndDate.Depth = 0;
            lblCatEndDate.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblCatEndDate.Location = new Point(480, 5);
            lblCatEndDate.MouseState = MaterialSkin.MouseState.HOVER;
            lblCatEndDate.Name = "lblCatEndDate";
            lblCatEndDate.Size = new Size(81, 19);
            lblCatEndDate.TabIndex = 34;
            lblCatEndDate.Text = "Bitiş Tarihi:";
            lblCatEndDate.Visible = false;
            // 
            // dtpCatEndDate
            // 
            dtpCatEndDate.Font = new Font("Segoe UI", 12F);
            dtpCatEndDate.Format = DateTimePickerFormat.Short;
            dtpCatEndDate.Location = new Point(480, 32);
            dtpCatEndDate.Name = "dtpCatEndDate";
            dtpCatEndDate.Size = new Size(155, 29);
            dtpCatEndDate.TabIndex = 35;
            dtpCatEndDate.Visible = false;
            // 
            // cmbFilterValue
            // 
            cmbFilterValue.AutoResize = false;
            cmbFilterValue.BackColor = Color.White;
            cmbFilterValue.Depth = 0;
            cmbFilterValue.DrawMode = DrawMode.OwnerDrawVariable;
            cmbFilterValue.DropDownHeight = 174;
            cmbFilterValue.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterValue.DropDownWidth = 200;
            cmbFilterValue.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            cmbFilterValue.ForeColor = Color.FromArgb(222, 0, 0, 0);
            cmbFilterValue.FormattingEnabled = true;
            cmbFilterValue.Hint = "Değer Seçin";
            cmbFilterValue.IntegralHeight = false;
            cmbFilterValue.ItemHeight = 43;
            cmbFilterValue.Location = new Point(310, 25);
            cmbFilterValue.MaxDropDownItems = 4;
            cmbFilterValue.MouseState = MaterialSkin.MouseState.OUT;
            cmbFilterValue.Name = "cmbFilterValue";
            cmbFilterValue.Size = new Size(280, 49);
            cmbFilterValue.StartIndex = -1;
            cmbFilterValue.TabIndex = 36;
            cmbFilterValue.Visible = false;
            // 
            // txtSingleNumber
            // 
            txtSingleNumber.AnimateReadOnly = false;
            txtSingleNumber.BackgroundImageLayout = ImageLayout.None;
            txtSingleNumber.CharacterCasing = CharacterCasing.Normal;
            txtSingleNumber.Depth = 0;
            txtSingleNumber.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            txtSingleNumber.HideSelection = true;
            txtSingleNumber.Hint = "Örn: 05551234567";
            txtSingleNumber.LeadingIcon = null;
            txtSingleNumber.Location = new Point(310, 25);
            txtSingleNumber.MaxLength = 15;
            txtSingleNumber.MouseState = MaterialSkin.MouseState.OUT;
            txtSingleNumber.Name = "txtSingleNumber";
            txtSingleNumber.PasswordChar = '\0';
            txtSingleNumber.PrefixSuffixText = null;
            txtSingleNumber.ReadOnly = false;
            txtSingleNumber.RightToLeft = RightToLeft.No;
            txtSingleNumber.SelectedText = "";
            txtSingleNumber.SelectionLength = 0;
            txtSingleNumber.SelectionStart = 0;
            txtSingleNumber.ShortcutsEnabled = true;
            txtSingleNumber.Size = new Size(280, 48);
            txtSingleNumber.TabIndex = 37;
            txtSingleNumber.TabStop = false;
            txtSingleNumber.TextAlign = HorizontalAlignment.Left;
            txtSingleNumber.TrailingIcon = null;
            txtSingleNumber.UseSystemPasswordChar = false;
            txtSingleNumber.Visible = false;
            // 
            // btnFilter
            // 
            btnFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnFilter.Depth = 0;
            btnFilter.HighEmphasis = true;
            btnFilter.Icon = null;
            btnFilter.Location = new Point(920, 30);
            btnFilter.Margin = new Padding(4, 6, 4, 6);
            btnFilter.MouseState = MaterialSkin.MouseState.HOVER;
            btnFilter.Name = "btnFilter";
            btnFilter.NoAccentTextColor = Color.Empty;
            btnFilter.Size = new Size(96, 36);
            btnFilter.TabIndex = 38;
            btnFilter.Text = " 🔍 Filtrele ";
            btnFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnFilter.UseAccentColor = false;
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += BtnFilter_Click;
            // 
            // dgvFiltered
            // 
            dataGridViewCellStyle2.BackColor = Color.FromArgb(248, 248, 252);
            dgvFiltered.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            dgvFiltered.BackgroundColor = Color.White;
            dgvFiltered.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(183, 28, 28);
            dataGridViewCellStyle3.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(211, 47, 47);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvFiltered.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvFiltered.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(255, 235, 238);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(183, 28, 28);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvFiltered.DefaultCellStyle = dataGridViewCellStyle4;
            dgvFiltered.EnableHeadersVisualStyles = false;
            dgvFiltered.GridColor = Color.FromArgb(230, 230, 230);
            dgvFiltered.Location = new Point(10, 115);
            dgvFiltered.Name = "dgvFiltered";
            dgvFiltered.RowTemplate.Height = 28;
            dgvFiltered.Size = new Size(645, 350);
            dgvFiltered.TabIndex = 40;
            // 
            // btnSendFiltered
            // 
            btnSendFiltered.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSendFiltered.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnSendFiltered.Depth = 0;
            btnSendFiltered.HighEmphasis = true;
            btnSendFiltered.Icon = null;
            btnSendFiltered.Location = new Point(10, 475);
            btnSendFiltered.Margin = new Padding(4, 6, 4, 6);
            btnSendFiltered.MouseState = MaterialSkin.MouseState.HOVER;
            btnSendFiltered.Name = "btnSendFiltered";
            btnSendFiltered.NoAccentTextColor = Color.Empty;
            btnSendFiltered.Size = new Size(247, 36);
            btnSendFiltered.TabIndex = 45;
            btnSendFiltered.Text = " 🚀 Filtrelenen Listeye Gönder ";
            btnSendFiltered.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnSendFiltered.UseAccentColor = false;
            btnSendFiltered.UseVisualStyleBackColor = true;
            btnSendFiltered.Click += BtnSendFiltered_Click;
            // 
            // btnStopCat
            // 
            btnStopCat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnStopCat.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnStopCat.Depth = 0;
            btnStopCat.Enabled = false;
            btnStopCat.HighEmphasis = true;
            btnStopCat.Icon = null;
            btnStopCat.Location = new Point(305, 475);
            btnStopCat.Margin = new Padding(4, 6, 4, 6);
            btnStopCat.MouseState = MaterialSkin.MouseState.HOVER;
            btnStopCat.Name = "btnStopCat";
            btnStopCat.NoAccentTextColor = Color.Empty;
            btnStopCat.Size = new Size(98, 36);
            btnStopCat.TabIndex = 52;
            btnStopCat.Text = " ⏹ Durdur ";
            btnStopCat.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnStopCat.UseAccentColor = true;
            btnStopCat.UseVisualStyleBackColor = true;
            btnStopCat.Click += BtnStopCat_Click;
            // 
            // btnRemoveCatAttachment
            // 
            btnRemoveCatAttachment.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnRemoveCatAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnRemoveCatAttachment.Depth = 0;
            btnRemoveCatAttachment.HighEmphasis = true;
            btnRemoveCatAttachment.Icon = null;
            btnRemoveCatAttachment.Location = new Point(810, 245);
            btnRemoveCatAttachment.Margin = new Padding(4, 6, 4, 6);
            btnRemoveCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            btnRemoveCatAttachment.Name = "btnRemoveCatAttachment";
            btnRemoveCatAttachment.NoAccentTextColor = Color.Empty;
            btnRemoveCatAttachment.Size = new Size(75, 36);
            btnRemoveCatAttachment.TabIndex = 53;
            btnRemoveCatAttachment.Text = " Kaldır ";
            btnRemoveCatAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnRemoveCatAttachment.UseAccentColor = true;
            btnRemoveCatAttachment.UseVisualStyleBackColor = true;
            btnRemoveCatAttachment.Visible = false;
            btnRemoveCatAttachment.Click += BtnRemoveCatAttachment_Click;
            // 
            // lblFilteredCount
            // 
            lblFilteredCount.AutoSize = true;
            lblFilteredCount.Depth = 0;
            lblFilteredCount.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblFilteredCount.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            lblFilteredCount.ForeColor = Color.FromArgb(211, 21, 21);
            lblFilteredCount.Location = new Point(15, 95);
            lblFilteredCount.MouseState = MaterialSkin.MouseState.HOVER;
            lblFilteredCount.Name = "lblFilteredCount";
            lblFilteredCount.Size = new Size(135, 17);
            lblFilteredCount.TabIndex = 39;
            lblFilteredCount.Text = "📋 Filtrelenen: 0 kayıt";
            // 
            // lblCatMessage
            // 
            lblCatMessage.AutoSize = true;
            lblCatMessage.Depth = 0;
            lblCatMessage.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblCatMessage.Location = new Point(670, 95);
            lblCatMessage.MouseState = MaterialSkin.MouseState.HOVER;
            lblCatMessage.Name = "lblCatMessage";
            lblCatMessage.Size = new Size(163, 19);
            lblCatMessage.TabIndex = 41;
            lblCatMessage.Text = "✏️ Gönderilecek Mesaj:";
            // 
            // txtCatMessage
            // 
            txtCatMessage.AnimateReadOnly = false;
            txtCatMessage.BackgroundImageLayout = ImageLayout.None;
            txtCatMessage.CharacterCasing = CharacterCasing.Normal;
            txtCatMessage.Cursor = Cursors.IBeam;
            txtCatMessage.Depth = 0;
            txtCatMessage.HideSelection = true;
            txtCatMessage.Location = new Point(670, 118);
            txtCatMessage.MaxLength = 32767;
            txtCatMessage.MouseState = MaterialSkin.MouseState.OUT;
            txtCatMessage.Name = "txtCatMessage";
            txtCatMessage.PasswordChar = '\0';
            txtCatMessage.ReadOnly = false;
            txtCatMessage.ScrollBars = ScrollBars.Vertical;
            txtCatMessage.SelectedText = "";
            txtCatMessage.SelectionLength = 0;
            txtCatMessage.SelectionStart = 0;
            txtCatMessage.ShortcutsEnabled = true;
            txtCatMessage.Size = new Size(488, 120);
            txtCatMessage.TabIndex = 42;
            txtCatMessage.TabStop = false;
            txtCatMessage.Text = "Sayın {İsim}, {Plaka} plakalı aracınızın trafik sigortası bitmektedir. En uygun teklifimiz {EnUygunSirket} firmasından {EnUygunFiyat} fiyatıdır.";
            txtCatMessage.TextAlign = HorizontalAlignment.Left;
            txtCatMessage.UseSystemPasswordChar = false;
            // 
            // btnCatAttachment
            // 
            btnCatAttachment.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCatAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnCatAttachment.Depth = 0;
            btnCatAttachment.HighEmphasis = true;
            btnCatAttachment.Icon = null;
            btnCatAttachment.Location = new Point(670, 245);
            btnCatAttachment.Margin = new Padding(4, 6, 4, 6);
            btnCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            btnCatAttachment.Name = "btnCatAttachment";
            btnCatAttachment.NoAccentTextColor = Color.Empty;
            btnCatAttachment.Size = new Size(120, 36);
            btnCatAttachment.TabIndex = 43;
            btnCatAttachment.Text = " 📎 Görsel Seç ";
            btnCatAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            btnCatAttachment.UseAccentColor = false;
            btnCatAttachment.UseVisualStyleBackColor = true;
            btnCatAttachment.Click += BtnCatAttachment_Click;
            // 
            // lblCatAttachment
            // 
            lblCatAttachment.AutoEllipsis = true;
            lblCatAttachment.AutoSize = true;
            lblCatAttachment.Depth = 0;
            lblCatAttachment.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            lblCatAttachment.Location = new Point(890, 253);
            lblCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            lblCatAttachment.Name = "lblCatAttachment";
            lblCatAttachment.Size = new Size(123, 19);
            lblCatAttachment.TabIndex = 44;
            lblCatAttachment.Text = "Dosya Seçilmedi.";
            // 
            // rtbHelp
            // 
            rtbHelp.BackColor = Color.FromArgb(255, 253, 231);
            rtbHelp.BorderStyle = BorderStyle.FixedSingle;
            rtbHelp.Font = new Font("Segoe UI", 9F);
            rtbHelp.ForeColor = Color.FromArgb(60, 60, 60);
            rtbHelp.Location = new Point(670, 290);
            rtbHelp.Name = "rtbHelp";
            rtbHelp.ReadOnly = true;
            rtbHelp.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbHelp.Size = new Size(488, 130);
            rtbHelp.TabIndex = 51;
            rtbHelp.Text = resources.GetString("rtbHelp.Text");
            // 
            // rtbCatLog
            // 
            rtbCatLog.BackColor = Color.FromArgb(30, 30, 35);
            rtbCatLog.Font = new Font("Cascadia Code", 9.5F);
            rtbCatLog.ForeColor = Color.FromArgb(100, 255, 100);
            rtbCatLog.Location = new Point(670, 430);
            rtbCatLog.Name = "rtbCatLog";
            rtbCatLog.ReadOnly = true;
            rtbCatLog.Size = new Size(488, 150);
            rtbCatLog.TabIndex = 46;
            rtbCatLog.Text = "";
            // 
            // progressBarCat
            // 
            progressBarCat.Depth = 0;
            progressBarCat.Location = new Point(10, 590);
            progressBarCat.MouseState = MaterialSkin.MouseState.HOVER;
            progressBarCat.Name = "progressBarCat";
            progressBarCat.Size = new Size(1148, 5);
            progressBarCat.TabIndex = 47;
            // 
            // lblCatStatus
            // 
            lblCatStatus.AutoSize = true;
            lblCatStatus.Depth = 0;
            lblCatStatus.Font = new Font("Roboto Medium", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblCatStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            lblCatStatus.Location = new Point(10, 598);
            lblCatStatus.MouseState = MaterialSkin.MouseState.HOVER;
            lblCatStatus.Name = "lblCatStatus";
            lblCatStatus.Size = new Size(88, 17);
            lblCatStatus.TabIndex = 48;
            lblCatStatus.Text = "Durum: Hazır.";
            // 
            // tabDashboard
            // 
            tabDashboard.BackColor = Color.White;
            tabDashboard.Controls.Add(lblTotal);
            tabDashboard.Controls.Add(lblPending);
            tabDashboard.Controls.Add(lblSuccess);
            tabDashboard.Controls.Add(lblFailed);
            tabDashboard.Location = new Point(4, 24);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(3);
            tabDashboard.Size = new Size(1220, 734);
            tabDashboard.TabIndex = 1;
            tabDashboard.Text = "Canlı İstatistikler";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Depth = 0;
            lblTotal.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblTotal.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblTotal.Location = new Point(60, 60);
            lblTotal.MouseState = MaterialSkin.MouseState.HOVER;
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(297, 41);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Toplam Yüklenen: 0";
            // 
            // lblPending
            // 
            lblPending.AutoSize = true;
            lblPending.Depth = 0;
            lblPending.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblPending.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblPending.Location = new Point(60, 150);
            lblPending.MouseState = MaterialSkin.MouseState.HOVER;
            lblPending.Name = "lblPending";
            lblPending.Size = new Size(171, 41);
            lblPending.TabIndex = 1;
            lblPending.Text = "Bekleyen: 0";
            // 
            // lblSuccess
            // 
            lblSuccess.AutoSize = true;
            lblSuccess.Depth = 0;
            lblSuccess.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblSuccess.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblSuccess.ForeColor = Color.Green;
            lblSuccess.Location = new Point(60, 240);
            lblSuccess.MouseState = MaterialSkin.MouseState.HOVER;
            lblSuccess.Name = "lblSuccess";
            lblSuccess.Size = new Size(149, 41);
            lblSuccess.TabIndex = 2;
            lblSuccess.Text = "Başarılı: 0";
            // 
            // lblFailed
            // 
            lblFailed.AutoSize = true;
            lblFailed.Depth = 0;
            lblFailed.Font = new Font("Roboto", 34F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblFailed.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            lblFailed.ForeColor = Color.Red;
            lblFailed.Location = new Point(60, 330);
            lblFailed.MouseState = MaterialSkin.MouseState.HOVER;
            lblFailed.Name = "lblFailed";
            lblFailed.Size = new Size(125, 41);
            lblFailed.TabIndex = 3;
            lblFailed.Text = "Hatalı: 0";
            // 
            // picLogo
            // 
            picLogo.Image = (Image)resources.GetObject("picLogo.Image");
            picLogo.Location = new Point(12, 70);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(200, 65);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // tmrSchedule
            // 
            tmrSchedule.Interval = 1000;
            tmrSchedule.Tick += TmrSchedule_Tick;
            // 
            // lblCategoryTitle
            // 
            lblCategoryTitle.AutoSize = true;
            lblCategoryTitle.Depth = 0;
            lblCategoryTitle.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            lblCategoryTitle.Location = new Point(10, 5);
            lblCategoryTitle.MouseState = MaterialSkin.MouseState.HOVER;
            lblCategoryTitle.Name = "lblCategoryTitle";
            lblCategoryTitle.Size = new Size(150, 19);
            lblCategoryTitle.TabIndex = 30;
            lblCategoryTitle.Text = "📋 Filtre Kategorisi:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 956);
            Controls.Add(picLogo);
            Controls.Add(tabControl);
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = tabControl;
            Font = new Font("Segoe UI", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "İncir Sigorta - WhatsApp Otomasyonu (CRM)";
            FormClosing += MainForm_FormClosing;
            tabControl.ResumeLayout(false);
            tabGönderim.ResumeLayout(false);
            tabGönderim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNumbers).EndInit();
            tabKategori.ResumeLayout(false);
            tabKategori.PerformLayout();
            pnlFilterBar.ResumeLayout(false);
            pnlFilterBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFiltered).EndInit();
            tabDashboard.ResumeLayout(false);
            tabDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
        }

        private MaterialSkin.Controls.MaterialTabControl tabControl;
        private System.Windows.Forms.TabPage tabGönderim;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.TabPage tabKategori;
        
        private System.Windows.Forms.DataGridView dgvNumbers;
        private MaterialSkin.Controls.MaterialButton btnLoadExcel;
        private MaterialSkin.Controls.MaterialButton btnExport;
        private MaterialSkin.Controls.MaterialComboBox cmbProfile;
        private MaterialSkin.Controls.MaterialButton btnConnect;
        private MaterialSkin.Controls.MaterialButton btnStartSending;
        private MaterialSkin.Controls.MaterialButton btnStop;
        private MaterialSkin.Controls.MaterialCheckbox chkHeadless;
        private MaterialSkin.Controls.MaterialComboBox cmbSpeed;
        private MaterialSkin.Controls.MaterialCheckbox chkHash;
        
        private MaterialSkin.Controls.MaterialButton btnAttachment;
        private MaterialSkin.Controls.MaterialLabel lblAttachment;
        private MaterialSkin.Controls.MaterialCheckbox chkSchedule;
        private System.Windows.Forms.DateTimePicker dtpSchedule;

        private MaterialSkin.Controls.MaterialMultiLineTextBox2 txtMessage;
        private System.Windows.Forms.RichTextBox rtbLog;
        private MaterialSkin.Controls.MaterialProgressBar progressBar;
        private MaterialSkin.Controls.MaterialLabel lblStatus;
        private MaterialSkin.Controls.MaterialLabel label1;
        
        private MaterialSkin.Controls.MaterialLabel lblTotal;
        private MaterialSkin.Controls.MaterialLabel lblPending;
        private MaterialSkin.Controls.MaterialLabel lblSuccess;
        private MaterialSkin.Controls.MaterialLabel lblFailed;
        
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Timer tmrSchedule;

        // Kategori Gönderim tab kontrolleri
        private MaterialSkin.Controls.MaterialComboBox cmbCategory;
        private MaterialSkin.Controls.MaterialLabel lblCategoryTitle;
        private System.Windows.Forms.DateTimePicker dtpCatStartDate;
        private System.Windows.Forms.DateTimePicker dtpCatEndDate;
        private MaterialSkin.Controls.MaterialLabel lblCatStartDate;
        private MaterialSkin.Controls.MaterialLabel lblCatEndDate;
        private MaterialSkin.Controls.MaterialComboBox cmbFilterValue;
        private MaterialSkin.Controls.MaterialTextBox2 txtSingleNumber;
        private MaterialSkin.Controls.MaterialButton btnFilter;
        private System.Windows.Forms.DataGridView dgvFiltered;
        private MaterialSkin.Controls.MaterialButton btnSendFiltered;
        private MaterialSkin.Controls.MaterialLabel lblFilteredCount;
        private MaterialSkin.Controls.MaterialLabel lblCatMessage;
        private MaterialSkin.Controls.MaterialMultiLineTextBox2 txtCatMessage;
        private MaterialSkin.Controls.MaterialButton btnCatAttachment;
        private MaterialSkin.Controls.MaterialLabel lblCatAttachment;
        private System.Windows.Forms.RichTextBox rtbCatLog;
        private MaterialSkin.Controls.MaterialProgressBar progressBarCat;
        private MaterialSkin.Controls.MaterialLabel lblCatStatus;
        private System.Windows.Forms.Panel pnlFilterBar;
        private System.Windows.Forms.RichTextBox rtbHelp;
        private MaterialSkin.Controls.MaterialButton btnStopCat;
        private MaterialSkin.Controls.MaterialButton btnRemoveAttachment;
        private MaterialSkin.Controls.MaterialButton btnRemoveCatAttachment;
        private MaterialSkin.Controls.MaterialComboBox cmbCatTypeFilter;
    }
}
