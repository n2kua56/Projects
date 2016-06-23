namespace MessageCenter
{
    partial class SendMailNew
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSignature = new System.Windows.Forms.CheckBox();
            this.chkHtml = new System.Windows.Forms.CheckBox();
            this.textBody = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textAttachments = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textSubject = new System.Windows.Forms.TextBox();
            this.textCc = new System.Windows.Forms.TextBox();
            this.textTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pgSending = new System.Windows.Forms.ToolStripProgressBar();
            this.attachmentDlg = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(584, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkSignature
            // 
            this.chkSignature.Location = new System.Drawing.Point(128, 94);
            this.chkSignature.Name = "chkSignature";
            this.chkSignature.Size = new System.Drawing.Size(128, 21);
            this.chkSignature.TabIndex = 35;
            this.chkSignature.Text = "Digitial Signature";
            // 
            // chkHtml
            // 
            this.chkHtml.Location = new System.Drawing.Point(10, 94);
            this.chkHtml.Name = "chkHtml";
            this.chkHtml.Size = new System.Drawing.Size(107, 21);
            this.chkHtml.TabIndex = 34;
            this.chkHtml.Text = "HTML Body";
            // 
            // textBody
            // 
            this.textBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBody.Location = new System.Drawing.Point(10, 148);
            this.textBody.Name = "textBody";
            this.textBody.Size = new System.Drawing.Size(663, 273);
            this.textBody.TabIndex = 32;
            this.textBody.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(624, 118);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(49, 23);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(578, 118);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 23);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textAttachments
            // 
            this.textAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textAttachments.BackColor = System.Drawing.SystemColors.Info;
            this.textAttachments.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textAttachments.Location = new System.Drawing.Point(90, 120);
            this.textAttachments.Name = "textAttachments";
            this.textAttachments.ReadOnly = true;
            this.textAttachments.Size = new System.Drawing.Size(482, 20);
            this.textAttachments.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Attachments";
            // 
            // textSubject
            // 
            this.textSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSubject.Location = new System.Drawing.Point(66, 66);
            this.textSubject.Name = "textSubject";
            this.textSubject.Size = new System.Drawing.Size(607, 20);
            this.textSubject.TabIndex = 27;
            this.textSubject.Text = "Test subject";
            // 
            // textCc
            // 
            this.textCc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textCc.Location = new System.Drawing.Point(66, 37);
            this.textCc.Name = "textCc";
            this.textCc.Size = new System.Drawing.Size(607, 20);
            this.textCc.TabIndex = 26;
            // 
            // textTo
            // 
            this.textTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textTo.Location = new System.Drawing.Point(66, 9);
            this.textTo.Name = "textTo";
            this.textTo.Size = new System.Drawing.Size(607, 20);
            this.textTo.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Cc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "To";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(488, 427);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(88, 23);
            this.btnSend.TabIndex = 33;
            this.btnSend.TabStop = false;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbStatus,
            this.pgSending});
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(689, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sbStatus
            // 
            this.sbStatus.AutoSize = false;
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(300, 17);
            this.sbStatus.Text = "toolStripStatusLabel1";
            this.sbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pgSending
            // 
            this.pgSending.AutoSize = false;
            this.pgSending.Name = "pgSending";
            this.pgSending.Size = new System.Drawing.Size(300, 16);
            // 
            // SendMailNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 479);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkSignature);
            this.Controls.Add(this.chkHtml);
            this.Controls.Add(this.textBody);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textAttachments);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textSubject);
            this.Controls.Add(this.textCc);
            this.Controls.Add(this.textTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSend);
            this.Name = "SendMailNew";
            this.Text = "SendMailNew";
            this.Load += new System.EventHandler(this.SendMailNew_Load);
            this.Resize += new System.EventHandler(this.SendMailNew_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkSignature;
        private System.Windows.Forms.CheckBox chkHtml;
        private System.Windows.Forms.RichTextBox textBody;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textAttachments;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textSubject;
        private System.Windows.Forms.TextBox textCc;
        private System.Windows.Forms.TextBox textTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sbStatus;
        private System.Windows.Forms.ToolStripProgressBar pgSending;
        private System.Windows.Forms.OpenFileDialog attachmentDlg;

    }
}