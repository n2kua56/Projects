namespace EZUtils
{
    partial class ExceptionDialog
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxErrorMessage = new System.Windows.Forms.TextBox();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panelDetails = new System.Windows.Forms.Panel();
            this.textBoxEZMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSysErrorMsg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSysErrorType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Location = new System.Drawing.Point(13, 29);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(34, 35);
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            // 
            // textBoxErrorMessage
            // 
            this.textBoxErrorMessage.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxErrorMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxErrorMessage.Location = new System.Drawing.Point(60, 11);
            this.textBoxErrorMessage.Multiline = true;
            this.textBoxErrorMessage.Name = "textBoxErrorMessage";
            this.textBoxErrorMessage.ReadOnly = true;
            this.textBoxErrorMessage.Size = new System.Drawing.Size(450, 67);
            this.textBoxErrorMessage.TabIndex = 10;
            this.textBoxErrorMessage.TabStop = false;
            // 
            // buttonDetails
            // 
            this.buttonDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDetails.Location = new System.Drawing.Point(9, 87);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new System.Drawing.Size(75, 23);
            this.buttonDetails.TabIndex = 1;
            this.buttonDetails.Text = "Details";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panelDetails
            // 
            this.panelDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetails.BackColor = System.Drawing.SystemColors.Control;
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.textBoxEZMessage);
            this.panelDetails.Controls.Add(this.label3);
            this.panelDetails.Controls.Add(this.textBoxSysErrorMsg);
            this.panelDetails.Controls.Add(this.label2);
            this.panelDetails.Controls.Add(this.textBoxSysErrorType);
            this.panelDetails.Controls.Add(this.label1);
            this.panelDetails.Controls.Add(this.buttonCopy);
            this.panelDetails.Location = new System.Drawing.Point(9, 121);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(498, 96);
            this.panelDetails.TabIndex = 6;
            // 
            // textBoxEZMessage
            // 
            this.textBoxEZMessage.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxEZMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxEZMessage.Location = new System.Drawing.Point(172, 45);
            this.textBoxEZMessage.Name = "textBoxEZMessage";
            this.textBoxEZMessage.Size = new System.Drawing.Size(312, 13);
            this.textBoxEZMessage.TabIndex = 7;
            this.textBoxEZMessage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Srs Library Error Message:";
            // 
            // textBoxSysErrorMsg
            // 
            this.textBoxSysErrorMsg.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSysErrorMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSysErrorMsg.Location = new System.Drawing.Point(172, 27);
            this.textBoxSysErrorMsg.Name = "textBoxSysErrorMsg";
            this.textBoxSysErrorMsg.Size = new System.Drawing.Size(312, 13);
            this.textBoxSysErrorMsg.TabIndex = 5;
            this.textBoxSysErrorMsg.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "System Level Error Message:";
            // 
            // textBoxSysErrorType
            // 
            this.textBoxSysErrorType.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxSysErrorType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSysErrorType.Location = new System.Drawing.Point(172, 9);
            this.textBoxSysErrorType.Name = "textBoxSysErrorType";
            this.textBoxSysErrorType.Size = new System.Drawing.Size(312, 13);
            this.textBoxSysErrorType.TabIndex = 3;
            this.textBoxSysErrorType.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "System Level Error Type:";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(409, 64);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(75, 23);
            this.buttonCopy.TabIndex = 2;
            this.buttonCopy.Text = "Copy Report";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonQuit.Location = new System.Drawing.Point(351, 87);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonQuit.TabIndex = 8;
            this.buttonQuit.TabStop = false;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(432, 87);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(75, 23);
            this.buttonContinue.TabIndex = 12;
            this.buttonContinue.TabStop = false;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // ExceptionDialog
            // 
            this.AcceptButton = this.buttonContinue;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonQuit;
            this.ClientSize = new System.Drawing.Size(516, 229);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.textBoxErrorMessage);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.panelDetails);
            this.Controls.Add(this.buttonQuit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionDialog";
            this.ShowInTaskbar = false;
            this.Text = "ExceptionDialog";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox textBoxErrorMessage;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.TextBox textBoxEZMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSysErrorMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSysErrorType;
        private System.Windows.Forms.Label label1;
    }
}