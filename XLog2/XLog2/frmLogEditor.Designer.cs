namespace XLog2
{
    partial class frmLogEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogEditor));
            this.label1 = new System.Windows.Forms.Label();
            this.cbRemarks = new System.Windows.Forms.CheckBox();
            this.cbUnknown2 = new System.Windows.Forms.CheckBox();
            this.cbUnknown1 = new System.Windows.Forms.CheckBox();
            this.cbLocator = new System.Windows.Forms.CheckBox();
            this.cbQTH = new System.Windows.Forms.CheckBox();
            this.cbName = new System.Windows.Forms.CheckBox();
            this.cbPower = new System.Windows.Forms.CheckBox();
            this.cbQslIn = new System.Windows.Forms.CheckBox();
            this.cbQslOut = new System.Windows.Forms.CheckBox();
            this.cbAwards = new System.Windows.Forms.CheckBox();
            this.cbRX = new System.Windows.Forms.CheckBox();
            this.cbTX = new System.Windows.Forms.CheckBox();
            this.cbMode = new System.Windows.Forms.CheckBox();
            this.cbFrequency = new System.Windows.Forms.CheckBox();
            this.cbCall = new System.Windows.Forms.CheckBox();
            this.cbUTCend = new System.Windows.Forms.CheckBox();
            this.cbUTC = new System.Windows.Forms.CheckBox();
            this.cbDate = new System.Windows.Forms.CheckBox();
            this.cbQSONumber = new System.Windows.Forms.CheckBox();
            this.cbLogName = new System.Windows.Forms.CheckBox();
            this.lblLogName = new System.Windows.Forms.Label();
            this.lblUnknown1 = new System.Windows.Forms.Label();
            this.lblUnknown2 = new System.Windows.Forms.Label();
            this.tbUnknown1 = new System.Windows.Forms.TextBox();
            this.tbUnknown2 = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cbCountry = new System.Windows.Forms.CheckBox();
            this.cbState = new System.Windows.Forms.CheckBox();
            this.cbCounty = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 23);
            this.label1.TabIndex = 41;
            this.label1.Text = "Select fields to display for the new log";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbRemarks
            // 
            this.cbRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRemarks.AutoSize = true;
            this.cbRemarks.Location = new System.Drawing.Point(219, 134);
            this.cbRemarks.Name = "cbRemarks";
            this.cbRemarks.Size = new System.Drawing.Size(68, 17);
            this.cbRemarks.TabIndex = 40;
            this.cbRemarks.Text = "Remarks";
            this.cbRemarks.UseVisualStyleBackColor = true;
            // 
            // cbUnknown2
            // 
            this.cbUnknown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnknown2.AutoSize = true;
            this.cbUnknown2.Location = new System.Drawing.Point(219, 111);
            this.cbUnknown2.Name = "cbUnknown2";
            this.cbUnknown2.Size = new System.Drawing.Size(84, 17);
            this.cbUnknown2.TabIndex = 39;
            this.cbUnknown2.Text = "UNKNOWN";
            this.cbUnknown2.UseVisualStyleBackColor = true;
            this.cbUnknown2.CheckedChanged += new System.EventHandler(this.cbUnknown2_CheckedChanged);
            // 
            // cbUnknown1
            // 
            this.cbUnknown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbUnknown1.AutoSize = true;
            this.cbUnknown1.Location = new System.Drawing.Point(219, 88);
            this.cbUnknown1.Name = "cbUnknown1";
            this.cbUnknown1.Size = new System.Drawing.Size(84, 17);
            this.cbUnknown1.TabIndex = 38;
            this.cbUnknown1.Text = "UNKNOWN";
            this.cbUnknown1.UseVisualStyleBackColor = true;
            this.cbUnknown1.CheckedChanged += new System.EventHandler(this.cbUnknown1_CheckedChanged);
            // 
            // cbLocator
            // 
            this.cbLocator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLocator.AutoSize = true;
            this.cbLocator.Location = new System.Drawing.Point(219, 65);
            this.cbLocator.Name = "cbLocator";
            this.cbLocator.Size = new System.Drawing.Size(62, 17);
            this.cbLocator.TabIndex = 37;
            this.cbLocator.Text = "Locator";
            this.cbLocator.UseVisualStyleBackColor = true;
            // 
            // cbQTH
            // 
            this.cbQTH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbQTH.AutoSize = true;
            this.cbQTH.Location = new System.Drawing.Point(115, 224);
            this.cbQTH.Name = "cbQTH";
            this.cbQTH.Size = new System.Drawing.Size(49, 17);
            this.cbQTH.TabIndex = 36;
            this.cbQTH.Text = "QTH";
            this.cbQTH.UseVisualStyleBackColor = true;
            // 
            // cbName
            // 
            this.cbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbName.AutoSize = true;
            this.cbName.Location = new System.Drawing.Point(115, 203);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(54, 17);
            this.cbName.TabIndex = 35;
            this.cbName.Text = "Name";
            this.cbName.UseVisualStyleBackColor = true;
            // 
            // cbPower
            // 
            this.cbPower.AutoSize = true;
            this.cbPower.Location = new System.Drawing.Point(115, 180);
            this.cbPower.Name = "cbPower";
            this.cbPower.Size = new System.Drawing.Size(56, 17);
            this.cbPower.TabIndex = 34;
            this.cbPower.Text = "Power";
            this.cbPower.UseVisualStyleBackColor = true;
            // 
            // cbQslIn
            // 
            this.cbQslIn.AutoSize = true;
            this.cbQslIn.Location = new System.Drawing.Point(115, 157);
            this.cbQslIn.Name = "cbQslIn";
            this.cbQslIn.Size = new System.Drawing.Size(53, 17);
            this.cbQslIn.TabIndex = 33;
            this.cbQslIn.Text = "Qsl In";
            this.cbQslIn.UseVisualStyleBackColor = true;
            // 
            // cbQslOut
            // 
            this.cbQslOut.AutoSize = true;
            this.cbQslOut.Location = new System.Drawing.Point(115, 134);
            this.cbQslOut.Name = "cbQslOut";
            this.cbQslOut.Size = new System.Drawing.Size(61, 17);
            this.cbQslOut.TabIndex = 32;
            this.cbQslOut.Text = "Qsl Out";
            this.cbQslOut.UseVisualStyleBackColor = true;
            // 
            // cbAwards
            // 
            this.cbAwards.AutoSize = true;
            this.cbAwards.Location = new System.Drawing.Point(115, 111);
            this.cbAwards.Name = "cbAwards";
            this.cbAwards.Size = new System.Drawing.Size(61, 17);
            this.cbAwards.TabIndex = 31;
            this.cbAwards.Text = "Awards";
            this.cbAwards.UseVisualStyleBackColor = true;
            // 
            // cbRX
            // 
            this.cbRX.AutoSize = true;
            this.cbRX.Location = new System.Drawing.Point(115, 88);
            this.cbRX.Name = "cbRX";
            this.cbRX.Size = new System.Drawing.Size(69, 17);
            this.cbRX.TabIndex = 30;
            this.cbRX.Text = "RX(RST)";
            this.cbRX.UseVisualStyleBackColor = true;
            // 
            // cbTX
            // 
            this.cbTX.AutoSize = true;
            this.cbTX.Location = new System.Drawing.Point(115, 65);
            this.cbTX.Name = "cbTX";
            this.cbTX.Size = new System.Drawing.Size(68, 17);
            this.cbTX.TabIndex = 29;
            this.cbTX.Text = "TX(RST)";
            this.cbTX.UseVisualStyleBackColor = true;
            // 
            // cbMode
            // 
            this.cbMode.AutoSize = true;
            this.cbMode.Checked = true;
            this.cbMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMode.Enabled = false;
            this.cbMode.Location = new System.Drawing.Point(21, 224);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(53, 17);
            this.cbMode.TabIndex = 28;
            this.cbMode.Text = "Mode";
            this.cbMode.UseVisualStyleBackColor = true;
            // 
            // cbFrequency
            // 
            this.cbFrequency.AutoSize = true;
            this.cbFrequency.Checked = true;
            this.cbFrequency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFrequency.Enabled = false;
            this.cbFrequency.Location = new System.Drawing.Point(20, 203);
            this.cbFrequency.Name = "cbFrequency";
            this.cbFrequency.Size = new System.Drawing.Size(76, 17);
            this.cbFrequency.TabIndex = 27;
            this.cbFrequency.Text = "Frequency";
            this.cbFrequency.UseVisualStyleBackColor = true;
            // 
            // cbCall
            // 
            this.cbCall.AutoSize = true;
            this.cbCall.Checked = true;
            this.cbCall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCall.Enabled = false;
            this.cbCall.Location = new System.Drawing.Point(20, 180);
            this.cbCall.Name = "cbCall";
            this.cbCall.Size = new System.Drawing.Size(43, 17);
            this.cbCall.TabIndex = 26;
            this.cbCall.Text = "Call";
            this.cbCall.UseVisualStyleBackColor = true;
            // 
            // cbUTCend
            // 
            this.cbUTCend.AutoSize = true;
            this.cbUTCend.Location = new System.Drawing.Point(20, 157);
            this.cbUTCend.Name = "cbUTCend";
            this.cbUTCend.Size = new System.Drawing.Size(75, 17);
            this.cbUTCend.TabIndex = 25;
            this.cbUTCend.Text = "UTC - end";
            this.cbUTCend.UseVisualStyleBackColor = true;
            // 
            // cbUTC
            // 
            this.cbUTC.AutoSize = true;
            this.cbUTC.Checked = true;
            this.cbUTC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUTC.Enabled = false;
            this.cbUTC.Location = new System.Drawing.Point(20, 134);
            this.cbUTC.Name = "cbUTC";
            this.cbUTC.Size = new System.Drawing.Size(48, 17);
            this.cbUTC.TabIndex = 24;
            this.cbUTC.Text = "UTC";
            this.cbUTC.UseVisualStyleBackColor = true;
            // 
            // cbDate
            // 
            this.cbDate.AutoSize = true;
            this.cbDate.Checked = true;
            this.cbDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDate.Enabled = false;
            this.cbDate.Location = new System.Drawing.Point(20, 111);
            this.cbDate.Name = "cbDate";
            this.cbDate.Size = new System.Drawing.Size(49, 17);
            this.cbDate.TabIndex = 23;
            this.cbDate.Text = "Date";
            this.cbDate.UseVisualStyleBackColor = true;
            // 
            // cbQSONumber
            // 
            this.cbQSONumber.AutoSize = true;
            this.cbQSONumber.Location = new System.Drawing.Point(20, 88);
            this.cbQSONumber.Name = "cbQSONumber";
            this.cbQSONumber.Size = new System.Drawing.Size(89, 17);
            this.cbQSONumber.TabIndex = 22;
            this.cbQSONumber.Text = "QSO Number";
            this.cbQSONumber.UseVisualStyleBackColor = true;
            // 
            // cbLogName
            // 
            this.cbLogName.AutoSize = true;
            this.cbLogName.Location = new System.Drawing.Point(20, 65);
            this.cbLogName.Name = "cbLogName";
            this.cbLogName.Size = new System.Drawing.Size(70, 17);
            this.cbLogName.TabIndex = 21;
            this.cbLogName.Text = "Logname";
            this.cbLogName.UseVisualStyleBackColor = true;
            // 
            // lblLogName
            // 
            this.lblLogName.Location = new System.Drawing.Point(17, 9);
            this.lblLogName.Name = "lblLogName";
            this.lblLogName.Size = new System.Drawing.Size(276, 23);
            this.lblLogName.TabIndex = 42;
            this.lblLogName.Text = "Log: ";
            this.lblLogName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnknown1
            // 
            this.lblUnknown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnknown1.AutoSize = true;
            this.lblUnknown1.Location = new System.Drawing.Point(18, 251);
            this.lblUnknown1.Name = "lblUnknown1";
            this.lblUnknown1.Size = new System.Drawing.Size(74, 13);
            this.lblUnknown1.TabIndex = 43;
            this.lblUnknown1.Text = "UNKNOWN1:";
            // 
            // lblUnknown2
            // 
            this.lblUnknown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUnknown2.AutoSize = true;
            this.lblUnknown2.Location = new System.Drawing.Point(18, 277);
            this.lblUnknown2.Name = "lblUnknown2";
            this.lblUnknown2.Size = new System.Drawing.Size(74, 13);
            this.lblUnknown2.TabIndex = 44;
            this.lblUnknown2.Text = "UNKNOWN2:";
            // 
            // tbUnknown1
            // 
            this.tbUnknown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbUnknown1.Location = new System.Drawing.Point(98, 251);
            this.tbUnknown1.Name = "tbUnknown1";
            this.tbUnknown1.Size = new System.Drawing.Size(100, 20);
            this.tbUnknown1.TabIndex = 45;
            // 
            // tbUnknown2
            // 
            this.tbUnknown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbUnknown2.Location = new System.Drawing.Point(98, 277);
            this.tbUnknown2.Name = "tbUnknown2";
            this.tbUnknown2.Size = new System.Drawing.Size(100, 20);
            this.tbUnknown2.TabIndex = 46;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(136, 304);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 47;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(230, 304);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 48;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cbCountry
            // 
            this.cbCountry.AutoSize = true;
            this.cbCountry.Location = new System.Drawing.Point(219, 157);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(62, 17);
            this.cbCountry.TabIndex = 49;
            this.cbCountry.Text = "Country";
            this.cbCountry.UseVisualStyleBackColor = true;
            // 
            // cbState
            // 
            this.cbState.AutoSize = true;
            this.cbState.Location = new System.Drawing.Point(219, 180);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(51, 17);
            this.cbState.TabIndex = 50;
            this.cbState.Text = "State";
            this.cbState.UseVisualStyleBackColor = true;
            // 
            // cbCounty
            // 
            this.cbCounty.AutoSize = true;
            this.cbCounty.Location = new System.Drawing.Point(219, 203);
            this.cbCounty.Name = "cbCounty";
            this.cbCounty.Size = new System.Drawing.Size(59, 17);
            this.cbCounty.TabIndex = 51;
            this.cbCounty.Text = "County";
            this.cbCounty.UseVisualStyleBackColor = true;
            // 
            // frmLogEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(327, 344);
            this.Controls.Add(this.cbCounty);
            this.Controls.Add(this.cbState);
            this.Controls.Add(this.cbCountry);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.tbUnknown2);
            this.Controls.Add(this.tbUnknown1);
            this.Controls.Add(this.lblUnknown2);
            this.Controls.Add(this.lblUnknown1);
            this.Controls.Add(this.lblLogName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRemarks);
            this.Controls.Add(this.cbUnknown2);
            this.Controls.Add(this.cbUnknown1);
            this.Controls.Add(this.cbLocator);
            this.Controls.Add(this.cbQTH);
            this.Controls.Add(this.cbName);
            this.Controls.Add(this.cbPower);
            this.Controls.Add(this.cbQslIn);
            this.Controls.Add(this.cbQslOut);
            this.Controls.Add(this.cbAwards);
            this.Controls.Add(this.cbRX);
            this.Controls.Add(this.cbTX);
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.cbFrequency);
            this.Controls.Add(this.cbCall);
            this.Controls.Add(this.cbUTCend);
            this.Controls.Add(this.cbUTC);
            this.Controls.Add(this.cbDate);
            this.Controls.Add(this.cbQSONumber);
            this.Controls.Add(this.cbLogName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLogEditor";
            this.Text = "XLog2 - Log Editor";
            this.Load += new System.EventHandler(this.frmLogEditor_Load);
            this.Resize += new System.EventHandler(this.frmLogEditor_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.CheckBox cbQSONumber;
        internal System.Windows.Forms.CheckBox cbLogName;
        internal System.Windows.Forms.CheckBox cbRemarks;
        internal System.Windows.Forms.CheckBox cbUnknown2;
        internal System.Windows.Forms.CheckBox cbUnknown1;
        internal System.Windows.Forms.CheckBox cbLocator;
        internal System.Windows.Forms.CheckBox cbQTH;
        internal System.Windows.Forms.CheckBox cbName;
        internal System.Windows.Forms.CheckBox cbPower;
        internal System.Windows.Forms.CheckBox cbQslIn;
        internal System.Windows.Forms.CheckBox cbQslOut;
        internal System.Windows.Forms.CheckBox cbAwards;
        internal System.Windows.Forms.CheckBox cbRX;
        internal System.Windows.Forms.CheckBox cbTX;
        internal System.Windows.Forms.CheckBox cbMode;
        internal System.Windows.Forms.CheckBox cbFrequency;
        internal System.Windows.Forms.CheckBox cbCall;
        internal System.Windows.Forms.CheckBox cbUTCend;
        internal System.Windows.Forms.CheckBox cbUTC;
        internal System.Windows.Forms.CheckBox cbDate;
        internal System.Windows.Forms.Label lblLogName;
        internal System.Windows.Forms.TextBox tbUnknown1;
        internal System.Windows.Forms.TextBox tbUnknown2;
        internal System.Windows.Forms.Label lblUnknown1;
        internal System.Windows.Forms.Label lblUnknown2;
        internal System.Windows.Forms.CheckBox cbCountry;
        internal System.Windows.Forms.CheckBox cbState;
        internal System.Windows.Forms.CheckBox cbCounty;
    }
}