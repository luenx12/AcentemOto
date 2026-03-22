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
            this.components = new System.ComponentModel.Container();
            this.tabControl = new MaterialSkin.Controls.MaterialTabControl();
            this.tabGönderim = new System.Windows.Forms.TabPage();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.tabKategori = new System.Windows.Forms.TabPage();
            
            this.dgvNumbers = new System.Windows.Forms.DataGridView();
            this.btnLoadExcel = new MaterialSkin.Controls.MaterialButton();
            this.btnExport = new MaterialSkin.Controls.MaterialButton();
            this.cmbProfile = new MaterialSkin.Controls.MaterialComboBox();
            this.btnConnect = new MaterialSkin.Controls.MaterialButton();
            this.btnStartSending = new MaterialSkin.Controls.MaterialButton();
            this.btnStop = new MaterialSkin.Controls.MaterialButton();
            this.chkHeadless = new MaterialSkin.Controls.MaterialCheckbox();
            
            this.btnAttachment = new MaterialSkin.Controls.MaterialButton();
            this.lblAttachment = new MaterialSkin.Controls.MaterialLabel();
            this.chkSchedule = new MaterialSkin.Controls.MaterialCheckbox();
            this.dtpSchedule = new System.Windows.Forms.DateTimePicker();
            
            this.txtMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.progressBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.lblStatus = new MaterialSkin.Controls.MaterialLabel();
            this.label1 = new MaterialSkin.Controls.MaterialLabel();
            
            this.lblTotal = new MaterialSkin.Controls.MaterialLabel();
            this.lblPending = new MaterialSkin.Controls.MaterialLabel();
            this.lblSuccess = new MaterialSkin.Controls.MaterialLabel();
            this.lblFailed = new MaterialSkin.Controls.MaterialLabel();
            
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.tmrSchedule = new System.Windows.Forms.Timer(this.components);

            // Kategori Gönderim tab kontrolleri
            this.cmbCategory = new MaterialSkin.Controls.MaterialComboBox();
            this.lblCategoryTitle = new MaterialSkin.Controls.MaterialLabel();
            this.dtpCatStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCatEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblCatStartDate = new MaterialSkin.Controls.MaterialLabel();
            this.lblCatEndDate = new MaterialSkin.Controls.MaterialLabel();
            this.cmbFilterValue = new MaterialSkin.Controls.MaterialComboBox();
            this.txtSingleNumber = new MaterialSkin.Controls.MaterialTextBox2();
            this.btnFilter = new MaterialSkin.Controls.MaterialButton();
            this.dgvFiltered = new System.Windows.Forms.DataGridView();
            this.btnSendFiltered = new MaterialSkin.Controls.MaterialButton();
            this.lblFilteredCount = new MaterialSkin.Controls.MaterialLabel();
            this.lblCatMessage = new MaterialSkin.Controls.MaterialLabel();
            this.txtCatMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            this.btnCatAttachment = new MaterialSkin.Controls.MaterialButton();
            this.lblCatAttachment = new MaterialSkin.Controls.MaterialLabel();
            this.rtbCatLog = new System.Windows.Forms.RichTextBox();
            this.progressBarCat = new MaterialSkin.Controls.MaterialProgressBar();
            this.lblCatStatus = new MaterialSkin.Controls.MaterialLabel();
            this.pnlFilterBar = new System.Windows.Forms.Panel();
            this.rtbHelp = new System.Windows.Forms.RichTextBox();
            this.btnStopCat = new MaterialSkin.Controls.MaterialButton();
            this.btnRemoveAttachment = new MaterialSkin.Controls.MaterialButton();
            this.btnRemoveCatAttachment = new MaterialSkin.Controls.MaterialButton();
            this.cmbCatTypeFilter = new MaterialSkin.Controls.MaterialComboBox();

            this.tabControl.SuspendLayout();
            this.tabGönderim.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.tabKategori.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // 
            // picLogo
            // 
            this.picLogo.Image = System.Drawing.Image.FromFile("logo.png");
            this.picLogo.Location = new System.Drawing.Point(12, 70);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 65);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;

            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGönderim);
            this.tabControl.Controls.Add(this.tabKategori);
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Depth = 0;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(10, 145);
            this.tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1180, 645);
            this.tabControl.TabIndex = 1;

            // 
            // tabGönderim
            // 
            this.tabGönderim.BackColor = System.Drawing.Color.White;
            this.tabGönderim.Controls.Add(this.cmbProfile);
            this.tabGönderim.Controls.Add(this.btnLoadExcel);
            this.tabGönderim.Controls.Add(this.btnExport);
            this.tabGönderim.Controls.Add(this.btnConnect);
            this.tabGönderim.Controls.Add(this.chkHeadless);
            this.tabGönderim.Controls.Add(this.btnStartSending);
            this.tabGönderim.Controls.Add(this.btnStop);
            this.tabGönderim.Controls.Add(this.chkSchedule);
            this.tabGönderim.Controls.Add(this.dtpSchedule);
            this.tabGönderim.Controls.Add(this.btnAttachment);
            this.tabGönderim.Controls.Add(this.lblAttachment);
            this.tabGönderim.Controls.Add(this.btnRemoveAttachment);
            this.tabGönderim.Controls.Add(this.label1);
            this.tabGönderim.Controls.Add(this.txtMessage);
            this.tabGönderim.Controls.Add(this.rtbLog);
            this.tabGönderim.Controls.Add(this.dgvNumbers);
            this.tabGönderim.Controls.Add(this.progressBar);
            this.tabGönderim.Controls.Add(this.lblStatus);
            this.tabGönderim.Location = new System.Drawing.Point(4, 26);
            this.tabGönderim.Name = "tabGönderim";
            this.tabGönderim.Padding = new System.Windows.Forms.Padding(3);
            this.tabGönderim.Size = new System.Drawing.Size(1172, 615);
            this.tabGönderim.TabIndex = 0;
            this.tabGönderim.Text = "Gönderim Ekranı";

            // 
            // tabDashboard
            // 
            this.tabDashboard.BackColor = System.Drawing.Color.White;
            this.tabDashboard.Controls.Add(this.lblTotal);
            this.tabDashboard.Controls.Add(this.lblPending);
            this.tabDashboard.Controls.Add(this.lblSuccess);
            this.tabDashboard.Controls.Add(this.lblFailed);
            this.tabDashboard.Location = new System.Drawing.Point(4, 26);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(1172, 615);
            this.tabDashboard.TabIndex = 1;
            this.tabDashboard.Text = "Canlı İstatistikler";

            // 
            // dgvNumbers
            // 
            this.dgvNumbers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvNumbers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNumbers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            headerStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNumbers.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dgvNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNumbers.EnableHeadersVisualStyles = false;
            this.dgvNumbers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvNumbers.Location = new System.Drawing.Point(15, 15);
            this.dgvNumbers.Name = "dgvNumbers";
            this.dgvNumbers.RowTemplate.Height = 25;
            this.dgvNumbers.Size = new System.Drawing.Size(630, 360);
            this.dgvNumbers.TabIndex = 0;

            // 
            // cmbProfile
            // 
            this.cmbProfile.AutoResize = false;
            this.cmbProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbProfile.Depth = 0;
            this.cmbProfile.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbProfile.DropDownHeight = 174;
            this.cmbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfile.DropDownWidth = 121;
            this.cmbProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmbProfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbProfile.FormattingEnabled = true;
            this.cmbProfile.Hint = "WhatsApp Profili Seçin";
            this.cmbProfile.IntegralHeight = false;
            this.cmbProfile.ItemHeight = 43;
            this.cmbProfile.Items.AddRange(new object[] { "Varsayılan (Default)", "Satış Hattı", "Destek Hattı", "Profil 4" });
            this.cmbProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.cmbProfile.Location = new System.Drawing.Point(15, 390);
            this.cmbProfile.MaxDropDownItems = 4;
            this.cmbProfile.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbProfile.Name = "cmbProfile";
            this.cmbProfile.Size = new System.Drawing.Size(280, 49);
            this.cmbProfile.StartIndex = 0;
            this.cmbProfile.TabIndex = 20;

            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadExcel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLoadExcel.Depth = 0;
            this.btnLoadExcel.HighEmphasis = true;
            this.btnLoadExcel.Icon = null;
            this.btnLoadExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnLoadExcel.Location = new System.Drawing.Point(15, 500);
            this.btnLoadExcel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLoadExcel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLoadExcel.Size = new System.Drawing.Size(130, 36);
            this.btnLoadExcel.TabIndex = 1;
            this.btnLoadExcel.Text = " Excel Yükle ";
            this.btnLoadExcel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLoadExcel.UseAccentColor = false;
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.BtnLoadExcel_Click);

            // 
            // btnExport
            // 
            this.btnExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExport.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnExport.Depth = 0;
            this.btnExport.HighEmphasis = true;
            this.btnExport.Icon = null;
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnExport.Location = new System.Drawing.Point(165, 500);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnExport.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnExport.Name = "btnExport";
            this.btnExport.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnExport.Size = new System.Drawing.Size(130, 36);
            this.btnExport.TabIndex = 21;
            this.btnExport.Text = " Rapor Al ";
            this.btnExport.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnExport.UseAccentColor = false;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            // 
            // btnConnect
            // 
            this.btnConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConnect.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnConnect.Depth = 0;
            this.btnConnect.HighEmphasis = true;
            this.btnConnect.Icon = null;
            this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnConnect.Location = new System.Drawing.Point(15, 450);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnConnect.Size = new System.Drawing.Size(280, 36);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "WhatsApp Bağlan";
            this.btnConnect.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnConnect.UseAccentColor = false;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);

            // 
            // chkHeadless
            // 
            this.chkHeadless.AutoSize = true;
            this.chkHeadless.Depth = 0;
            this.chkHeadless.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.chkHeadless.Location = new System.Drawing.Point(320, 396);
            this.chkHeadless.Margin = new System.Windows.Forms.Padding(0);
            this.chkHeadless.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkHeadless.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkHeadless.Name = "chkHeadless";
            this.chkHeadless.ReadOnly = false;
            this.chkHeadless.Ripple = true;
            this.chkHeadless.Size = new System.Drawing.Size(185, 37);
            this.chkHeadless.TabIndex = 5;
            this.chkHeadless.Text = "Headless Mod (Gizli)";
            this.chkHeadless.UseVisualStyleBackColor = true;

            // 
            // btnStartSending
            // 
            this.btnStartSending.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartSending.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStartSending.Depth = 0;
            this.btnStartSending.HighEmphasis = true;
            this.btnStartSending.Icon = null;
            this.btnStartSending.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnStartSending.Location = new System.Drawing.Point(15, 550);
            this.btnStartSending.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStartSending.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStartSending.Name = "btnStartSending";
            this.btnStartSending.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStartSending.Size = new System.Drawing.Size(155, 36);
            this.btnStartSending.TabIndex = 3;
            this.btnStartSending.Text = " Gönderimi Başlat ";
            this.btnStartSending.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStartSending.UseAccentColor = false;
            this.btnStartSending.UseVisualStyleBackColor = true;
            this.btnStartSending.Click += new System.EventHandler(this.BtnStartSending_Click);

            // 
            // btnStop
            // 
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStop.Depth = 0;
            this.btnStop.HighEmphasis = true;
            this.btnStop.Icon = null;
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnStop.Location = new System.Drawing.Point(185, 550);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStop.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStop.Name = "btnStop";
            this.btnStop.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStop.Size = new System.Drawing.Size(110, 36);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = " Durdur ";
            this.btnStop.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStop.UseAccentColor = true;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            this.btnStop.Enabled = false;

            // 
            // chkSchedule
            // 
            this.chkSchedule.AutoSize = true;
            this.chkSchedule.Depth = 0;
            this.chkSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.chkSchedule.Location = new System.Drawing.Point(320, 499);
            this.chkSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.chkSchedule.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSchedule.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSchedule.Name = "chkSchedule";
            this.chkSchedule.ReadOnly = false;
            this.chkSchedule.Ripple = true;
            this.chkSchedule.Size = new System.Drawing.Size(125, 37);
            this.chkSchedule.TabIndex = 22;
            this.chkSchedule.Text = "Zamanlayıcı:";
            this.chkSchedule.UseVisualStyleBackColor = true;
            this.chkSchedule.CheckedChanged += new System.EventHandler(this.ChkSchedule_CheckedChanged);

            // 
            // dtpSchedule
            // 
            this.dtpSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.dtpSchedule.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpSchedule.Location = new System.Drawing.Point(460, 504);
            this.dtpSchedule.Name = "dtpSchedule";
            this.dtpSchedule.ShowUpDown = true;
            this.dtpSchedule.Size = new System.Drawing.Size(120, 29);
            this.dtpSchedule.TabIndex = 23;
            this.dtpSchedule.Enabled = false;

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Depth = 0;
            this.label1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.label1.Location = new System.Drawing.Point(670, 15);
            this.label1.MouseState = MaterialSkin.MouseState.HOVER;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gönderilecek Mesaj:";

            // 
            // txtMessage
            // 
            this.txtMessage.AnimateReadOnly = false;
            this.txtMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtMessage.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessage.Depth = 0;
            this.txtMessage.HideSelection = true;
            this.txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.txtMessage.Location = new System.Drawing.Point(670, 45);
            this.txtMessage.MaxLength = 32767;
            this.txtMessage.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PasswordChar = '\0';
            this.txtMessage.ReadOnly = false;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.SelectedText = "";
            this.txtMessage.SelectionLength = 0;
            this.txtMessage.SelectionStart = 0;
            this.txtMessage.ShortcutsEnabled = true;
            this.txtMessage.Size = new System.Drawing.Size(480, 240);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.TabStop = false;
            this.txtMessage.Text = "Sayın {İsim}, {Plaka} plakalı aracınızın trafik sigortası bitmektedir.";
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMessage.UseSystemPasswordChar = false;

            // 
            // btnAttachment
            // 
            this.btnAttachment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnAttachment.Depth = 0;
            this.btnAttachment.HighEmphasis = true;
            this.btnAttachment.Icon = null;
            this.btnAttachment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnAttachment.Location = new System.Drawing.Point(670, 295);
            this.btnAttachment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnAttachment.Name = "btnAttachment";
            this.btnAttachment.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnAttachment.Size = new System.Drawing.Size(113, 36);
            this.btnAttachment.TabIndex = 24;
            this.btnAttachment.Text = "Görsel Seç";
            this.btnAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnAttachment.UseAccentColor = false;
            this.btnAttachment.UseVisualStyleBackColor = true;
            this.btnAttachment.Click += new System.EventHandler(this.BtnAttachment_Click);

            // 
            // btnRemoveAttachment
            // 
            this.btnRemoveAttachment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemoveAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRemoveAttachment.Depth = 0;
            this.btnRemoveAttachment.HighEmphasis = true;
            this.btnRemoveAttachment.Icon = null;
            this.btnRemoveAttachment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnRemoveAttachment.Location = new System.Drawing.Point(790, 295);
            this.btnRemoveAttachment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRemoveAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRemoveAttachment.Name = "btnRemoveAttachment";
            this.btnRemoveAttachment.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRemoveAttachment.Size = new System.Drawing.Size(70, 36);
            this.btnRemoveAttachment.TabIndex = 26;
            this.btnRemoveAttachment.Text = " Kaldır ";
            this.btnRemoveAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnRemoveAttachment.UseAccentColor = true;
            this.btnRemoveAttachment.UseVisualStyleBackColor = true;
            this.btnRemoveAttachment.Visible = false;
            this.btnRemoveAttachment.Click += new System.EventHandler(this.BtnRemoveAttachment_Click);

            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Depth = 0;
            this.lblAttachment.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblAttachment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.lblAttachment.AutoEllipsis = true;
            this.lblAttachment.Location = new System.Drawing.Point(870, 305);
            this.lblAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(126, 19);
            this.lblAttachment.TabIndex = 25;
            this.lblAttachment.Text = "Dosya Seçilmedi.";

            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.Color.LightGreen;
            this.rtbLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.rtbLog.Location = new System.Drawing.Point(670, 345);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(480, 240);
            this.rtbLog.TabIndex = 8;
            this.rtbLog.Text = "";

            // 
            // progressBar
            // 
            this.progressBar.Depth = 0;
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.progressBar.Location = new System.Drawing.Point(15, 595);
            this.progressBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1135, 5);
            this.progressBar.TabIndex = 9;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Depth = 0;
            this.lblStatus.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.Location = new System.Drawing.Point(15, 605);
            this.lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(121, 17);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Durum: Bekliyor...";

            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Depth = 0;
            this.lblTotal.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblTotal.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.lblTotal.Location = new System.Drawing.Point(60, 60);
            this.lblTotal.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(325, 41);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Toplam Yüklenen: 0";

            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Depth = 0;
            this.lblPending.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblPending.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.lblPending.Location = new System.Drawing.Point(60, 150);
            this.lblPending.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(175, 41);
            this.lblPending.TabIndex = 1;
            this.lblPending.Text = "Bekleyen: 0";

            // 
            // lblSuccess
            // 
            this.lblSuccess.AutoSize = true;
            this.lblSuccess.Depth = 0;
            this.lblSuccess.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblSuccess.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.lblSuccess.ForeColor = System.Drawing.Color.Green;
            this.lblSuccess.Location = new System.Drawing.Point(60, 240);
            this.lblSuccess.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(164, 41);
            this.lblSuccess.TabIndex = 2;
            this.lblSuccess.Text = "Başarılı: 0";

            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.Depth = 0;
            this.lblFailed.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblFailed.FontType = MaterialSkin.MaterialSkinManager.fontType.H4;
            this.lblFailed.ForeColor = System.Drawing.Color.Red;
            this.lblFailed.Location = new System.Drawing.Point(60, 330);
            this.lblFailed.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(126, 41);
            this.lblFailed.TabIndex = 3;
            this.lblFailed.Text = "Hatalı: 0";
            
            // 
            // tmrSchedule
            // 
            this.tmrSchedule.Interval = 1000;
            this.tmrSchedule.Tick += new System.EventHandler(this.TmrSchedule_Tick);

            // =============================================
            // tabKategori - MODERN LAYOUT
            // =============================================
            this.tabKategori.BackColor = System.Drawing.Color.FromArgb(250, 250, 252);
            this.tabKategori.Controls.Add(this.pnlFilterBar);
            this.tabKategori.Controls.Add(this.dgvFiltered);
            this.tabKategori.Controls.Add(this.btnSendFiltered);
            this.tabKategori.Controls.Add(this.btnStopCat);
            this.tabKategori.Controls.Add(this.btnRemoveCatAttachment);
            this.tabKategori.Controls.Add(this.lblFilteredCount);
            this.tabKategori.Controls.Add(this.lblCatMessage);
            this.tabKategori.Controls.Add(this.txtCatMessage);
            this.tabKategori.Controls.Add(this.btnCatAttachment);
            this.tabKategori.Controls.Add(this.lblCatAttachment);
            this.tabKategori.Controls.Add(this.rtbHelp);
            this.tabKategori.Controls.Add(this.rtbCatLog);
            this.tabKategori.Controls.Add(this.progressBarCat);
            this.tabKategori.Controls.Add(this.lblCatStatus);
            this.tabKategori.Location = new System.Drawing.Point(4, 26);
            this.tabKategori.Name = "tabKategori";
            this.tabKategori.Padding = new System.Windows.Forms.Padding(3);
            this.tabKategori.Size = new System.Drawing.Size(1172, 615);
            this.tabKategori.TabIndex = 2;
            this.tabKategori.Text = "Kategori Gönderim";

            // ─── FILTER BAR PANEL ───
            this.pnlFilterBar.BackColor = System.Drawing.Color.FromArgb(240, 240, 245);
            this.pnlFilterBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlFilterBar.Controls.Add(this.cmbCategory);
            this.pnlFilterBar.Controls.Add(this.cmbCatTypeFilter);
            this.pnlFilterBar.Controls.Add(this.lblCatStartDate);
            this.pnlFilterBar.Controls.Add(this.dtpCatStartDate);
            this.pnlFilterBar.Controls.Add(this.lblCatEndDate);
            this.pnlFilterBar.Controls.Add(this.dtpCatEndDate);
            this.pnlFilterBar.Controls.Add(this.cmbFilterValue);
            this.pnlFilterBar.Controls.Add(this.txtSingleNumber);
            this.pnlFilterBar.Controls.Add(this.btnFilter);
            this.pnlFilterBar.Location = new System.Drawing.Point(10, 8);
            this.pnlFilterBar.Name = "pnlFilterBar";
            this.pnlFilterBar.Size = new System.Drawing.Size(1145, 80);
            this.pnlFilterBar.TabIndex = 50;

            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Depth = 0;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCategoryTitle.Location = new System.Drawing.Point(10, 5);
            this.lblCategoryTitle.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(150, 19);
            this.lblCategoryTitle.TabIndex = 30;
            this.lblCategoryTitle.Text = "📋 Filtre Kategorisi:";

            // 
            // cmbCategory
            // 
            this.cmbCategory.AutoResize = false;
            this.cmbCategory.BackColor = System.Drawing.Color.White;
            this.cmbCategory.Depth = 0;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCategory.DropDownHeight = 200;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.DropDownWidth = 230;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbCategory.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Hint = "Kategori Seçin";
            this.cmbCategory.IntegralHeight = false;
            this.cmbCategory.ItemHeight = 43;
            this.cmbCategory.Items.AddRange(new object[] { "📅 Sigorta Tarihi (Aralık)", "📊 Durum", "🏢 Şirket", "📁 Türü (Trafik/Dask/Kasko)", "📱 Tek Numara Gönderim" });
            this.cmbCategory.Location = new System.Drawing.Point(10, 25);
            this.cmbCategory.MaxDropDownItems = 5;
            this.cmbCategory.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(280, 49);
            this.cmbCategory.StartIndex = 0;
            this.cmbCategory.TabIndex = 31;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.CmbCategory_SelectedIndexChanged);

            // 
            // lblCatStartDate
            // 
            this.lblCatStartDate.AutoSize = true;
            this.lblCatStartDate.Depth = 0;
            this.lblCatStartDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatStartDate.Location = new System.Drawing.Point(310, 5);
            this.lblCatStartDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCatStartDate.Name = "lblCatStartDate";
            this.lblCatStartDate.Size = new System.Drawing.Size(100, 19);
            this.lblCatStartDate.TabIndex = 32;
            this.lblCatStartDate.Text = "Başlangıç Tarihi:";
            this.lblCatStartDate.Visible = false;

            // 
            // dtpCatStartDate
            // 
            this.dtpCatStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCatStartDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCatStartDate.Location = new System.Drawing.Point(310, 32);
            this.dtpCatStartDate.Name = "dtpCatStartDate";
            this.dtpCatStartDate.Size = new System.Drawing.Size(155, 29);
            this.dtpCatStartDate.TabIndex = 33;
            this.dtpCatStartDate.Visible = false;

            // 
            // lblCatEndDate
            // 
            this.lblCatEndDate.AutoSize = true;
            this.lblCatEndDate.Depth = 0;
            this.lblCatEndDate.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatEndDate.Location = new System.Drawing.Point(480, 5);
            this.lblCatEndDate.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCatEndDate.Name = "lblCatEndDate";
            this.lblCatEndDate.Size = new System.Drawing.Size(50, 19);
            this.lblCatEndDate.TabIndex = 34;
            this.lblCatEndDate.Text = "Bitiş Tarihi:";
            this.lblCatEndDate.Visible = false;

            // 
            // dtpCatEndDate
            // 
            this.dtpCatEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCatEndDate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpCatEndDate.Location = new System.Drawing.Point(480, 32);
            this.dtpCatEndDate.Name = "dtpCatEndDate";
            this.dtpCatEndDate.Size = new System.Drawing.Size(155, 29);
            this.dtpCatEndDate.TabIndex = 35;
            this.dtpCatEndDate.Visible = false;

            // 
            // cmbCatTypeFilter
            // 
            this.cmbCatTypeFilter.AutoResize = false;
            this.cmbCatTypeFilter.BackColor = System.Drawing.Color.White;
            this.cmbCatTypeFilter.Depth = 0;
            this.cmbCatTypeFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCatTypeFilter.DropDownHeight = 200;
            this.cmbCatTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCatTypeFilter.DropDownWidth = 200;
            this.cmbCatTypeFilter.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbCatTypeFilter.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            this.cmbCatTypeFilter.FormattingEnabled = true;
            this.cmbCatTypeFilter.Hint = "Tür Seçin (Opsiyonel)";
            this.cmbCatTypeFilter.IntegralHeight = false;
            this.cmbCatTypeFilter.ItemHeight = 43;
            this.cmbCatTypeFilter.Location = new System.Drawing.Point(660, 25);
            this.cmbCatTypeFilter.MaxDropDownItems = 8;
            this.cmbCatTypeFilter.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCatTypeFilter.Name = "cmbCatTypeFilter";
            this.cmbCatTypeFilter.Size = new System.Drawing.Size(230, 49);
            this.cmbCatTypeFilter.StartIndex = -1;
            this.cmbCatTypeFilter.TabIndex = 36;
            this.cmbCatTypeFilter.Visible = false;

            // 
            // cmbFilterValue
            // 
            this.cmbFilterValue.AutoResize = false;
            this.cmbFilterValue.BackColor = System.Drawing.Color.White;
            this.cmbFilterValue.Depth = 0;
            this.cmbFilterValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFilterValue.DropDownHeight = 200;
            this.cmbFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterValue.DropDownWidth = 200;
            this.cmbFilterValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.cmbFilterValue.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            this.cmbFilterValue.FormattingEnabled = true;
            this.cmbFilterValue.Hint = "Değer Seçin";
            this.cmbFilterValue.IntegralHeight = false;
            this.cmbFilterValue.ItemHeight = 43;
            this.cmbFilterValue.Location = new System.Drawing.Point(310, 25);
            this.cmbFilterValue.MaxDropDownItems = 8;
            this.cmbFilterValue.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbFilterValue.Name = "cmbFilterValue";
            this.cmbFilterValue.Size = new System.Drawing.Size(280, 49);
            this.cmbFilterValue.StartIndex = -1;
            this.cmbFilterValue.TabIndex = 36;
            this.cmbFilterValue.Visible = false;

            // 
            // txtSingleNumber
            // 
            this.txtSingleNumber.AnimateReadOnly = false;
            this.txtSingleNumber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtSingleNumber.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtSingleNumber.Depth = 0;
            this.txtSingleNumber.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.txtSingleNumber.Hint = "Örn: 05551234567";
            this.txtSingleNumber.LeadingIcon = null;
            this.txtSingleNumber.Location = new System.Drawing.Point(310, 25);
            this.txtSingleNumber.MaxLength = 15;
            this.txtSingleNumber.MouseState = MaterialSkin.MouseState.OUT;
            this.txtSingleNumber.Name = "txtSingleNumber";
            this.txtSingleNumber.Size = new System.Drawing.Size(280, 48);
            this.txtSingleNumber.TabIndex = 37;
            this.txtSingleNumber.Text = "";
            this.txtSingleNumber.TrailingIcon = null;
            this.txtSingleNumber.Visible = false;

            // 
            // btnFilter
            // 
            this.btnFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFilter.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnFilter.Depth = 0;
            this.btnFilter.HighEmphasis = true;
            this.btnFilter.Icon = null;
            this.btnFilter.Location = new System.Drawing.Point(920, 30);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnFilter.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnFilter.Size = new System.Drawing.Size(145, 36);
            this.btnFilter.TabIndex = 38;
            this.btnFilter.Text = " 🔍 Filtrele ";
            this.btnFilter.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnFilter.UseAccentColor = false;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilter_Click);

            // ─── CONTENT AREA ───

            // 
            // lblFilteredCount
            // 
            this.lblFilteredCount.AutoSize = true;
            this.lblFilteredCount.Depth = 0;
            this.lblFilteredCount.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblFilteredCount.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            this.lblFilteredCount.ForeColor = System.Drawing.Color.FromArgb(211, 21, 21);
            this.lblFilteredCount.Location = new System.Drawing.Point(15, 95);
            this.lblFilteredCount.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblFilteredCount.Name = "lblFilteredCount";
            this.lblFilteredCount.Size = new System.Drawing.Size(180, 17);
            this.lblFilteredCount.TabIndex = 39;
            this.lblFilteredCount.Text = "📋 Filtrelenen: 0 kayıt";

            // 
            // dgvFiltered
            // 
            this.dgvFiltered.BackgroundColor = System.Drawing.Color.White;
            this.dgvFiltered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvFiltered.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            System.Windows.Forms.DataGridViewCellStyle catHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            catHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            catHeaderStyle.BackColor = System.Drawing.Color.FromArgb(183, 28, 28);
            catHeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            catHeaderStyle.ForeColor = System.Drawing.Color.White;
            catHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(211, 47, 47);
            catHeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            catHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFiltered.ColumnHeadersDefaultCellStyle = catHeaderStyle;
            this.dgvFiltered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiltered.EnableHeadersVisualStyles = false;
            this.dgvFiltered.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            System.Windows.Forms.DataGridViewCellStyle catRowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            catRowStyle.BackColor = System.Drawing.Color.White;
            catRowStyle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            catRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 235, 238);
            catRowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(183, 28, 28);
            catRowStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dgvFiltered.DefaultCellStyle = catRowStyle;
            System.Windows.Forms.DataGridViewCellStyle catAltRowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            catAltRowStyle.BackColor = System.Drawing.Color.FromArgb(248, 248, 252);
            this.dgvFiltered.AlternatingRowsDefaultCellStyle = catAltRowStyle;
            this.dgvFiltered.Location = new System.Drawing.Point(10, 115);
            this.dgvFiltered.Name = "dgvFiltered";
            this.dgvFiltered.RowTemplate.Height = 28;
            this.dgvFiltered.Size = new System.Drawing.Size(645, 350);
            this.dgvFiltered.TabIndex = 40;

            // ─── RIGHT SIDE: MESSAGE + HELP + LOG ───

            // 
            // lblCatMessage
            // 
            this.lblCatMessage.AutoSize = true;
            this.lblCatMessage.Depth = 0;
            this.lblCatMessage.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatMessage.Location = new System.Drawing.Point(670, 95);
            this.lblCatMessage.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCatMessage.Name = "lblCatMessage";
            this.lblCatMessage.Size = new System.Drawing.Size(200, 19);
            this.lblCatMessage.TabIndex = 41;
            this.lblCatMessage.Text = "✏️ Gönderilecek Mesaj:";

            // 
            // txtCatMessage
            // 
            this.txtCatMessage.AnimateReadOnly = false;
            this.txtCatMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtCatMessage.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtCatMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCatMessage.Depth = 0;
            this.txtCatMessage.HideSelection = true;
            this.txtCatMessage.Location = new System.Drawing.Point(670, 118);
            this.txtCatMessage.MaxLength = 32767;
            this.txtCatMessage.MouseState = MaterialSkin.MouseState.OUT;
            this.txtCatMessage.Name = "txtCatMessage";
            this.txtCatMessage.PasswordChar = '\0';
            this.txtCatMessage.ReadOnly = false;
            this.txtCatMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCatMessage.SelectedText = "";
            this.txtCatMessage.SelectionLength = 0;
            this.txtCatMessage.SelectionStart = 0;
            this.txtCatMessage.ShortcutsEnabled = true;
            this.txtCatMessage.Size = new System.Drawing.Size(488, 120);
            this.txtCatMessage.TabIndex = 42;
            this.txtCatMessage.TabStop = false;
            this.txtCatMessage.Text = "Sayın {İsim}, {Plaka} plakalı aracınızın trafik sigortası bitmektedir. En uygun teklifimiz {EnUygunSirket} firmasından {EnUygunFiyat} fiyatıdır.";
            this.txtCatMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCatMessage.UseSystemPasswordChar = false;

            // 
            // btnCatAttachment
            // 
            this.btnCatAttachment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCatAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnCatAttachment.Depth = 0;
            this.btnCatAttachment.HighEmphasis = true;
            this.btnCatAttachment.Icon = null;
            this.btnCatAttachment.Location = new System.Drawing.Point(670, 245);
            this.btnCatAttachment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnCatAttachment.Name = "btnCatAttachment";
            this.btnCatAttachment.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnCatAttachment.Size = new System.Drawing.Size(130, 36);
            this.btnCatAttachment.TabIndex = 43;
            this.btnCatAttachment.Text = " 📎 Görsel Seç ";
            this.btnCatAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Outlined;
            this.btnCatAttachment.UseAccentColor = false;
            this.btnCatAttachment.UseVisualStyleBackColor = true;
            this.btnCatAttachment.Click += new System.EventHandler(this.BtnCatAttachment_Click);

            // 
            // btnRemoveCatAttachment
            // 
            this.btnRemoveCatAttachment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemoveCatAttachment.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnRemoveCatAttachment.Depth = 0;
            this.btnRemoveCatAttachment.HighEmphasis = true;
            this.btnRemoveCatAttachment.Icon = null;
            this.btnRemoveCatAttachment.Location = new System.Drawing.Point(810, 245);
            this.btnRemoveCatAttachment.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRemoveCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnRemoveCatAttachment.Name = "btnRemoveCatAttachment";
            this.btnRemoveCatAttachment.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnRemoveCatAttachment.Size = new System.Drawing.Size(70, 36);
            this.btnRemoveCatAttachment.TabIndex = 53;
            this.btnRemoveCatAttachment.Text = " Kaldır ";
            this.btnRemoveCatAttachment.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnRemoveCatAttachment.UseAccentColor = true;
            this.btnRemoveCatAttachment.UseVisualStyleBackColor = true;
            this.btnRemoveCatAttachment.Visible = false;
            this.btnRemoveCatAttachment.Click += new System.EventHandler(this.BtnRemoveCatAttachment_Click);

            // 
            // lblCatAttachment
            // 
            this.lblCatAttachment.AutoSize = true;
            this.lblCatAttachment.Depth = 0;
            this.lblCatAttachment.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatAttachment.AutoEllipsis = true;
            this.lblCatAttachment.Location = new System.Drawing.Point(890, 253);
            this.lblCatAttachment.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCatAttachment.Name = "lblCatAttachment";
            this.lblCatAttachment.Size = new System.Drawing.Size(126, 19);
            this.lblCatAttachment.TabIndex = 44;
            this.lblCatAttachment.Text = "Dosya Seçilmedi.";

            // 
            // rtbHelp - PARAMETRELERİ AÇIKLAYAN YARDIM PANELİ
            // 
            this.rtbHelp.BackColor = System.Drawing.Color.FromArgb(255, 253, 231);
            this.rtbHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbHelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbHelp.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.rtbHelp.Location = new System.Drawing.Point(670, 290);
            this.rtbHelp.Name = "rtbHelp";
            this.rtbHelp.ReadOnly = true;
            this.rtbHelp.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbHelp.Size = new System.Drawing.Size(488, 130);
            this.rtbHelp.TabIndex = 51;
            this.rtbHelp.Text = "💡 KULLANILABILIR PARAMETRELER (Süslü parantez ile yazın)\n\n" +
                "  {İsim}  → Müşteri adı/soyadı\n" +
                "  {Plaka}  → Araç plakası\n" +
                "  {Sigorta Tar}  → Sigorta bitiş tarihi\n" +
                "  {Türü}  → Poliçe türü (Trafik, Dask, Kasko)\n" +
                "  {Marka}  → Araç markası\n" +
                "  {TC / VRG}  → TC veya Vergi No\n" +
                "  {Belge No}  → Belge numarası\n" +
                "  {Durum}  → Poliçe durumu\n" +
                "  {Şirket}  → Sigorta şirketi\n" +
                "  {Teklif No}  → Teklif numarası\n\n" +
                "📊 OTOMATİK HESAPLANAN:\n" +
                "  {EnUygunFiyat}  → En düşük teklif fiyatı\n" +
                "  {EnUygunSirket}  → En düşük fiyatı veren şirket\n" +
                "  {EnUygunTeklif}  → Şirket: Fiyat formatında\n" +
                "  {Teklifler}  → Tüm teklifler listesi\n\n" +
                "📋 KATEGORİLER:\n" +
                "  📅 Sigorta Tarihi → Tarih aralığındaki poliçeleri filtreler\n" +
                "  📊 Durum → KESİLDİ, SATILMIŞ, YENİLENMEMİŞ vb.\n" +
                "  🏢 Şirket → Mapfre, Quick, Anadolu vb.\n" +
                "  📁 Türü → Trafik, Dask, Kasko\n" +
                "  📱 Tek Numara → Direkt bir numaraya mesaj atar";

            // ─── BOTTOM LEFT: ACTION BUTTONS ───

            // 
            // btnSendFiltered
            // 
            this.btnSendFiltered.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSendFiltered.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnSendFiltered.Depth = 0;
            this.btnSendFiltered.HighEmphasis = true;
            this.btnSendFiltered.Icon = null;
            this.btnSendFiltered.Location = new System.Drawing.Point(10, 475);
            this.btnSendFiltered.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSendFiltered.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnSendFiltered.Name = "btnSendFiltered";
            this.btnSendFiltered.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnSendFiltered.Size = new System.Drawing.Size(280, 36);
            this.btnSendFiltered.TabIndex = 45;
            this.btnSendFiltered.Text = " 🚀 Filtrelenen Listeye Gönder ";
            this.btnSendFiltered.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnSendFiltered.UseAccentColor = false;
            this.btnSendFiltered.UseVisualStyleBackColor = true;
            this.btnSendFiltered.Click += new System.EventHandler(this.BtnSendFiltered_Click);

            // 
            // btnStopCat
            // 
            this.btnStopCat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStopCat.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStopCat.Depth = 0;
            this.btnStopCat.HighEmphasis = true;
            this.btnStopCat.Icon = null;
            this.btnStopCat.Location = new System.Drawing.Point(305, 475);
            this.btnStopCat.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStopCat.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStopCat.Name = "btnStopCat";
            this.btnStopCat.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStopCat.Size = new System.Drawing.Size(130, 36);
            this.btnStopCat.TabIndex = 52;
            this.btnStopCat.Text = " ⏹ Durdur ";
            this.btnStopCat.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStopCat.UseAccentColor = true;
            this.btnStopCat.UseVisualStyleBackColor = true;
            this.btnStopCat.Click += new System.EventHandler(this.BtnStopCat_Click);
            this.btnStopCat.Enabled = false;

            // ─── BOTTOM: LOG + STATUS ───

            // 
            // rtbCatLog
            // 
            this.rtbCatLog.BackColor = System.Drawing.Color.FromArgb(30, 30, 35);
            this.rtbCatLog.Font = new System.Drawing.Font("Cascadia Code", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbCatLog.ForeColor = System.Drawing.Color.FromArgb(100, 255, 100);
            this.rtbCatLog.Location = new System.Drawing.Point(670, 430);
            this.rtbCatLog.Name = "rtbCatLog";
            this.rtbCatLog.ReadOnly = true;
            this.rtbCatLog.Size = new System.Drawing.Size(488, 150);
            this.rtbCatLog.TabIndex = 46;
            this.rtbCatLog.Text = "";

            // 
            // progressBarCat
            // 
            this.progressBarCat.Depth = 0;
            this.progressBarCat.Location = new System.Drawing.Point(10, 590);
            this.progressBarCat.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBarCat.Name = "progressBarCat";
            this.progressBarCat.Size = new System.Drawing.Size(1148, 5);
            this.progressBarCat.TabIndex = 47;

            // 
            // lblCatStatus
            // 
            this.lblCatStatus.AutoSize = true;
            this.lblCatStatus.Depth = 0;
            this.lblCatStatus.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblCatStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            this.lblCatStatus.Location = new System.Drawing.Point(10, 598);
            this.lblCatStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblCatStatus.Name = "lblCatStatus";
            this.lblCatStatus.Size = new System.Drawing.Size(121, 17);
            this.lblCatStatus.TabIndex = 48;
            this.lblCatStatus.Text = "Durum: Hazır.";

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.tabControl);
            this.DrawerShowIconsWhenHidden = true;
            this.DrawerTabControl = this.tabControl;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İncir Sigorta - WhatsApp Otomasyonu (CRM)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            
            this.tabControl.ResumeLayout(false);
            this.tabGönderim.ResumeLayout(false);
            this.tabGönderim.PerformLayout();
            this.tabKategori.ResumeLayout(false);
            this.tabKategori.PerformLayout();
            this.tabDashboard.ResumeLayout(false);
            this.tabDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
