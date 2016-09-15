namespace AutoDealerAdmin
{
    partial class frmCategory
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
            this.btnOK = new System.Windows.Forms.Button();
            this.tbCatPageDesc = new System.Windows.Forms.TextBox();
            this.tbCatDesc = new System.Windows.Forms.TextBox();
            this.tbCatName = new System.Windows.Forms.TextBox();
            this.tbCatId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCatSeq = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 292);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(419, 292);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Add/Update";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbCatPageDesc
            // 
            this.tbCatPageDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCatPageDesc.Location = new System.Drawing.Point(134, 171);
            this.tbCatPageDesc.MaxLength = 512;
            this.tbCatPageDesc.Multiline = true;
            this.tbCatPageDesc.Name = "tbCatPageDesc";
            this.tbCatPageDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCatPageDesc.Size = new System.Drawing.Size(360, 104);
            this.tbCatPageDesc.TabIndex = 4;
            // 
            // tbCatDesc
            // 
            this.tbCatDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCatDesc.Location = new System.Drawing.Point(134, 83);
            this.tbCatDesc.MaxLength = 256;
            this.tbCatDesc.Multiline = true;
            this.tbCatDesc.Name = "tbCatDesc";
            this.tbCatDesc.Size = new System.Drawing.Size(360, 44);
            this.tbCatDesc.TabIndex = 2;
            // 
            // tbCatName
            // 
            this.tbCatName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCatName.Location = new System.Drawing.Point(134, 51);
            this.tbCatName.MaxLength = 45;
            this.tbCatName.Name = "tbCatName";
            this.tbCatName.Size = new System.Drawing.Size(360, 20);
            this.tbCatName.TabIndex = 1;
            // 
            // tbCatId
            // 
            this.tbCatId.Location = new System.Drawing.Point(134, 19);
            this.tbCatId.Name = "tbCatId";
            this.tbCatId.ReadOnly = true;
            this.tbCatId.Size = new System.Drawing.Size(31, 20);
            this.tbCatId.TabIndex = 14;
            this.tbCatId.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Web Page Description:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Category Description:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Category Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Category ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Sequence:";
            // 
            // tbCatSeq
            // 
            this.tbCatSeq.Location = new System.Drawing.Point(134, 140);
            this.tbCatSeq.Name = "tbCatSeq";
            this.tbCatSeq.Size = new System.Drawing.Size(31, 20);
            this.tbCatSeq.TabIndex = 3;
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 327);
            this.Controls.Add(this.tbCatSeq);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbCatPageDesc);
            this.Controls.Add(this.tbCatDesc);
            this.Controls.Add(this.tbCatName);
            this.Controls.Add(this.tbCatId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmCategory";
            this.Text = "frmCategory";
            this.Load += new System.EventHandler(this.frmCategory_Load);
            this.Shown += new System.EventHandler(this.frmCategory_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbCatPageDesc;
        private System.Windows.Forms.TextBox tbCatDesc;
        private System.Windows.Forms.TextBox tbCatName;
        private System.Windows.Forms.TextBox tbCatId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCatSeq;
    }
}