import re

file_path = "Forms/MainForm.Designer.cs"

with open(file_path, "r", encoding="utf-8") as f:
    content = f.read()

# Replace Form Size
content = content.replace("this.ClientSize = new System.Drawing.Size(1000, 680);", "this.ClientSize = new System.Drawing.Size(1200, 800);")

# Replace Logo
content = content.replace("this.picLogo.Size = new System.Drawing.Size(250, 75);", "this.picLogo.Size = new System.Drawing.Size(200, 65);")

# Replace tabControl
content = content.replace("this.tabControl.Location = new System.Drawing.Point(0, 150);", "this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));\n            this.tabControl.Location = new System.Drawing.Point(10, 145);")
content = content.replace("this.tabControl.Size = new System.Drawing.Size(1000, 520);", "this.tabControl.Size = new System.Drawing.Size(1180, 645);")

# Replace tab pages size
content = content.replace("this.tabGönderim.Size = new System.Drawing.Size(992, 490);", "this.tabGönderim.Size = new System.Drawing.Size(1172, 615);")
content = content.replace("this.tabDashboard.Size = new System.Drawing.Size(992, 490);", "this.tabDashboard.Size = new System.Drawing.Size(1172, 615);")

# DataGridView
content = content.replace("this.dgvNumbers.Location = new System.Drawing.Point(12, 10);", "this.dgvNumbers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;\n            this.dgvNumbers.Location = new System.Drawing.Point(15, 15);")
content = content.replace("this.dgvNumbers.Size = new System.Drawing.Size(520, 240);", "this.dgvNumbers.Size = new System.Drawing.Size(630, 360);")

# Profile Dropdown
content = content.replace("this.cmbProfile.Location = new System.Drawing.Point(12, 260);", "this.cmbProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.cmbProfile.Location = new System.Drawing.Point(15, 390);")
content = content.replace("this.cmbProfile.Size = new System.Drawing.Size(250, 49);", "this.cmbProfile.Size = new System.Drawing.Size(280, 49);")

# Connect Button
content = content.replace("this.btnConnect.Location = new System.Drawing.Point(280, 265);", "this.btnConnect.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.btnConnect.Location = new System.Drawing.Point(15, 450);")
content = content.replace("this.btnConnect.Size = new System.Drawing.Size(161, 36);", "this.btnConnect.Size = new System.Drawing.Size(280, 36);")

# Headless Checkbox
content = content.replace("this.chkHeadless.Location = new System.Drawing.Point(450, 265);", "this.chkHeadless.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.chkHeadless.Location = new System.Drawing.Point(320, 396);")

# Excel Button
content = content.replace("this.btnLoadExcel.Location = new System.Drawing.Point(12, 320);", "this.btnLoadExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.btnLoadExcel.Location = new System.Drawing.Point(15, 500);")
content = content.replace("this.btnLoadExcel.Size = new System.Drawing.Size(117, 36);", "this.btnLoadExcel.Size = new System.Drawing.Size(130, 36);")
content = content.replace("this.btnLoadExcel.Text = \"Excel Yükle\";", "this.btnLoadExcel.Text = \" Excel Yükle \";")

# Export Button
content = content.replace("this.btnExport.Location = new System.Drawing.Point(140, 320);", "this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.btnExport.Location = new System.Drawing.Point(165, 500);")
content = content.replace("this.btnExport.Size = new System.Drawing.Size(93, 36);", "this.btnExport.Size = new System.Drawing.Size(130, 36);")
content = content.replace("this.btnExport.Text = \"Rapor Al\";", "this.btnExport.Text = \" Rapor Al \";")

# Schedule Checkbox
content = content.replace("this.chkSchedule.Location = new System.Drawing.Point(280, 319);", "this.chkSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.chkSchedule.Location = new System.Drawing.Point(320, 499);")

# Schedule DateTimePicker
content = content.replace("this.dtpSchedule.Location = new System.Drawing.Point(415, 326);", "this.dtpSchedule.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.dtpSchedule.Font = new System.Drawing.Font(\"Segoe UI\", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);\n            this.dtpSchedule.Location = new System.Drawing.Point(460, 504);")
content = content.replace("this.dtpSchedule.Size = new System.Drawing.Size(140, 25);", "this.dtpSchedule.Size = new System.Drawing.Size(120, 29);")

# Start Sending Button
content = content.replace("this.btnStartSending.Location = new System.Drawing.Point(12, 380);", "this.btnStartSending.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.btnStartSending.Location = new System.Drawing.Point(15, 550);")
content = content.replace("this.btnStartSending.Text = \"Gönderimi Başlat\";", "this.btnStartSending.Text = \" Gönderimi Başlat \";")

# Stop Button
content = content.replace("this.btnStop.Location = new System.Drawing.Point(180, 380);", "this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.btnStop.Location = new System.Drawing.Point(185, 550);")
content = content.replace("this.btnStop.Size = new System.Drawing.Size(83, 36);", "this.btnStop.Size = new System.Drawing.Size(110, 36);")
content = content.replace("this.btnStop.Text = \"Durdur\";", "this.btnStop.Text = \" Durdur \";")

# Message Textbox & Label
content = content.replace("this.label1.Location = new System.Drawing.Point(550, 10);", "this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;\n            this.label1.Location = new System.Drawing.Point(670, 15);")

content = content.replace("this.txtMessage.Location = new System.Drawing.Point(550, 35);", "this.txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;\n            this.txtMessage.Location = new System.Drawing.Point(670, 45);")
content = content.replace("this.txtMessage.Size = new System.Drawing.Size(420, 160);", "this.txtMessage.Size = new System.Drawing.Size(480, 240);")
content = content.replace("this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;", "this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;")

# Attachment
content = content.replace("this.btnAttachment.Location = new System.Drawing.Point(550, 205);", "this.btnAttachment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;\n            this.btnAttachment.Location = new System.Drawing.Point(670, 295);")
content = content.replace("this.lblAttachment.Location = new System.Drawing.Point(680, 215);", "this.lblAttachment.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;\n            this.lblAttachment.AutoEllipsis = true;\n            this.lblAttachment.Location = new System.Drawing.Point(795, 305);")

# Log Textbox
content = content.replace("this.rtbLog.Location = new System.Drawing.Point(550, 260);", "this.rtbLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;\n            this.rtbLog.Location = new System.Drawing.Point(670, 345);")
content = content.replace("this.rtbLog.Size = new System.Drawing.Size(420, 160);", "this.rtbLog.Size = new System.Drawing.Size(480, 240);")
content = content.replace("this.rtbLog.Font = new System.Drawing.Font(\"Consolas\", 9F,", "this.rtbLog.Font = new System.Drawing.Font(\"Consolas\", 10F,")

# Progress Bar
content = content.replace("this.progressBar.Location = new System.Drawing.Point(12, 450);", "this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;\n            this.progressBar.Location = new System.Drawing.Point(15, 595);")
content = content.replace("this.progressBar.Size = new System.Drawing.Size(958, 5);", "this.progressBar.Size = new System.Drawing.Size(1135, 5);")

# Status Label
content = content.replace("this.lblStatus.Location = new System.Drawing.Point(12, 465);", "this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;\n            this.lblStatus.Location = new System.Drawing.Point(15, 605);")

# Dashboard Tab Labels
content = content.replace("this.lblTotal.Location = new System.Drawing.Point(30, 30);", "this.lblTotal.Location = new System.Drawing.Point(60, 60);")
content = content.replace("this.lblPending.Location = new System.Drawing.Point(30, 90);", "this.lblPending.Location = new System.Drawing.Point(60, 150);")
content = content.replace("this.lblSuccess.Location = new System.Drawing.Point(30, 150);", "this.lblSuccess.Location = new System.Drawing.Point(60, 240);")
content = content.replace("this.lblFailed.Location = new System.Drawing.Point(30, 210);", "this.lblFailed.Location = new System.Drawing.Point(60, 330);")


with open(file_path, "w", encoding="utf-8") as f:
    f.write(content)

print("Done replacing bounds.")
