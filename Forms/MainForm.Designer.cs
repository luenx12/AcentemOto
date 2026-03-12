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
            this.dgvNumbers = new System.Windows.Forms.DataGridView();
            this.btnLoadExcel = new MaterialSkin.Controls.MaterialButton();
            this.btnConnect = new MaterialSkin.Controls.MaterialButton();
            this.btnStartSending = new MaterialSkin.Controls.MaterialButton();
            this.btnStop = new MaterialSkin.Controls.MaterialButton();
            this.chkHeadless = new MaterialSkin.Controls.MaterialCheckbox();
            this.txtMessage = new MaterialSkin.Controls.MaterialMultiLineTextBox2();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.progressBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.lblStatus = new MaterialSkin.Controls.MaterialLabel();
            this.label1 = new MaterialSkin.Controls.MaterialLabel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            
            // picLogo
            this.picLogo.Location = new System.Drawing.Point(12, 70);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(250, 75);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            this.picLogo.Image = System.Drawing.Image.FromFile("logo.png");

            // dgvNumbers
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
            this.dgvNumbers.Location = new System.Drawing.Point(12, 160);
            this.dgvNumbers.Name = "dgvNumbers";
            this.dgvNumbers.RowTemplate.Height = 25;
            this.dgvNumbers.Size = new System.Drawing.Size(500, 240);
            this.dgvNumbers.TabIndex = 0;
            
            // btnLoadExcel
            this.btnLoadExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoadExcel.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLoadExcel.Depth = 0;
            this.btnLoadExcel.HighEmphasis = true;
            this.btnLoadExcel.Icon = null;
            this.btnLoadExcel.Location = new System.Drawing.Point(12, 410);
            this.btnLoadExcel.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLoadExcel.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLoadExcel.Size = new System.Drawing.Size(117, 36);
            this.btnLoadExcel.TabIndex = 1;
            this.btnLoadExcel.Text = "Excel Yükle";
            this.btnLoadExcel.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLoadExcel.UseAccentColor = false;
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.BtnLoadExcel_Click);
            
            // btnConnect
            this.btnConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConnect.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnConnect.Depth = 0;
            this.btnConnect.HighEmphasis = true;
            this.btnConnect.Icon = null;
            this.btnConnect.Location = new System.Drawing.Point(135, 410);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnConnect.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnConnect.Size = new System.Drawing.Size(161, 36);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "WhatsApp Bağlan";
            this.btnConnect.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnConnect.UseAccentColor = false;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            
            // btnStartSending
            this.btnStartSending.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartSending.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStartSending.Depth = 0;
            this.btnStartSending.HighEmphasis = true;
            this.btnStartSending.Icon = null;
            this.btnStartSending.Location = new System.Drawing.Point(302, 410);
            this.btnStartSending.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStartSending.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStartSending.Name = "btnStartSending";
            this.btnStartSending.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStartSending.Size = new System.Drawing.Size(155, 36);
            this.btnStartSending.TabIndex = 3;
            this.btnStartSending.Text = "Gönderimi Başlat";
            this.btnStartSending.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStartSending.UseAccentColor = false;
            this.btnStartSending.UseVisualStyleBackColor = true;
            this.btnStartSending.Click += new System.EventHandler(this.BtnStartSending_Click);
            
            // btnStop
            this.btnStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStop.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnStop.Depth = 0;
            this.btnStop.HighEmphasis = true;
            this.btnStop.Icon = null;
            this.btnStop.Location = new System.Drawing.Point(463, 410);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnStop.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnStop.Name = "btnStop";
            this.btnStop.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnStop.Size = new System.Drawing.Size(83, 36);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Durdur";
            this.btnStop.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnStop.UseAccentColor = true;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            this.btnStop.Enabled = false;
            
            // chkHeadless
            this.chkHeadless.AutoSize = true;
            this.chkHeadless.Depth = 0;
            this.chkHeadless.Location = new System.Drawing.Point(580, 410);
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
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Depth = 0;
            this.label1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(530, 80);
            this.label1.MouseState = MaterialSkin.MouseState.HOVER;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Gönderilecek Mesaj:";
            
            // txtMessage
            this.txtMessage.AnimateReadOnly = false;
            this.txtMessage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.txtMessage.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.txtMessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMessage.Depth = 0;
            this.txtMessage.HideSelection = true;
            this.txtMessage.Location = new System.Drawing.Point(530, 95);
            this.txtMessage.MaxLength = 32767;
            this.txtMessage.MouseState = MaterialSkin.MouseState.OUT;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.PasswordChar = '\0';
            this.txtMessage.ReadOnly = false;
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMessage.SelectedText = "";
            this.txtMessage.SelectionLength = 0;
            this.txtMessage.SelectionStart = 0;
            this.txtMessage.ShortcutsEnabled = true;
            this.txtMessage.Size = new System.Drawing.Size(400, 145);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.TabStop = false;
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtMessage.UseSystemPasswordChar = false;
            
            // rtbLog
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbLog.ForeColor = System.Drawing.Color.LightGreen;
            this.rtbLog.Location = new System.Drawing.Point(530, 260);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(400, 140);
            this.rtbLog.TabIndex = 8;
            this.rtbLog.Text = "";
            
            // progressBar
            this.progressBar.Depth = 0;
            this.progressBar.Location = new System.Drawing.Point(12, 460);
            this.progressBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(918, 5);
            this.progressBar.TabIndex = 9;
            
            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Depth = 0;
            this.lblStatus.Font = new System.Drawing.Font("Roboto Medium", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblStatus.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle2;
            this.lblStatus.Location = new System.Drawing.Point(12, 480);
            this.lblStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(121, 17);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Durum: Bekliyor...";
            
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 540);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkHeadless);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStartSending);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnLoadExcel);
            this.Controls.Add(this.dgvNumbers);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İncir Sigorta - WhatsApp Otomasyonu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvNumbers;
        private MaterialSkin.Controls.MaterialButton btnLoadExcel;
        private MaterialSkin.Controls.MaterialButton btnConnect;
        private MaterialSkin.Controls.MaterialButton btnStartSending;
        private MaterialSkin.Controls.MaterialButton btnStop;
        private MaterialSkin.Controls.MaterialCheckbox chkHeadless;
        private MaterialSkin.Controls.MaterialMultiLineTextBox2 txtMessage;
        private System.Windows.Forms.RichTextBox rtbLog;
        private MaterialSkin.Controls.MaterialProgressBar progressBar;
        private MaterialSkin.Controls.MaterialLabel lblStatus;
        private MaterialSkin.Controls.MaterialLabel label1;
        private System.Windows.Forms.PictureBox picLogo;
    }
}
