namespace EZDesk
{
    partial class frmDocumentEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbDocId = new System.Windows.Forms.TextBox();
            this.tbPersonId = new System.Windows.Forms.TextBox();
            this.tbTabId = new System.Windows.Forms.TextBox();
            this.dtpCreated = new System.Windows.Forms.DateTimePicker();
            this.tbDocName = new System.Windows.Forms.TextBox();
            this.tbDocFullPathName = new System.Windows.Forms.TextBox();
            this.tbGroupRestriction = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Doc Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Person Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tab Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Created:";
            // 
            // cbIsActive
            // 
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new System.Drawing.Point(104, 121);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(56, 17);
            this.cbIsActive.TabIndex = 4;
            this.cbIsActive.Text = "Active";
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "File Path Name:";
            // 
            // tbDocId
            // 
            this.tbDocId.Location = new System.Drawing.Point(104, 17);
            this.tbDocId.Name = "tbDocId";
            this.tbDocId.ReadOnly = true;
            this.tbDocId.Size = new System.Drawing.Size(100, 20);
            this.tbDocId.TabIndex = 7;
            // 
            // tbPersonId
            // 
            this.tbPersonId.Location = new System.Drawing.Point(104, 43);
            this.tbPersonId.Name = "tbPersonId";
            this.tbPersonId.ReadOnly = true;
            this.tbPersonId.Size = new System.Drawing.Size(100, 20);
            this.tbPersonId.TabIndex = 8;
            // 
            // tbTabId
            // 
            this.tbTabId.Location = new System.Drawing.Point(104, 69);
            this.tbTabId.Name = "tbTabId";
            this.tbTabId.ReadOnly = true;
            this.tbTabId.Size = new System.Drawing.Size(100, 20);
            this.tbTabId.TabIndex = 9;
            // 
            // dtpCreated
            // 
            this.dtpCreated.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpCreated.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreated.Location = new System.Drawing.Point(104, 95);
            this.dtpCreated.Name = "dtpCreated";
            this.dtpCreated.Size = new System.Drawing.Size(200, 20);
            this.dtpCreated.TabIndex = 10;
            // 
            // tbDocName
            // 
            this.tbDocName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDocName.Location = new System.Drawing.Point(104, 144);
            this.tbDocName.Name = "tbDocName";
            this.tbDocName.Size = new System.Drawing.Size(200, 20);
            this.tbDocName.TabIndex = 11;
            // 
            // tbDocFullPathName
            // 
            this.tbDocFullPathName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDocFullPathName.Location = new System.Drawing.Point(104, 170);
            this.tbDocFullPathName.Name = "tbDocFullPathName";
            this.tbDocFullPathName.ReadOnly = true;
            this.tbDocFullPathName.Size = new System.Drawing.Size(200, 20);
            this.tbDocFullPathName.TabIndex = 12;
            // 
            // tbGroupRestriction
            // 
            this.tbGroupRestriction.Location = new System.Drawing.Point(104, 196);
            this.tbGroupRestriction.Name = "tbGroupRestriction";
            this.tbGroupRestriction.ReadOnly = true;
            this.tbGroupRestriction.Size = new System.Drawing.Size(100, 20);
            this.tbGroupRestriction.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Group Restriction:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(129, 240);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(229, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmDocumentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(317, 273);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbGroupRestriction);
            this.Controls.Add(this.tbDocFullPathName);
            this.Controls.Add(this.tbDocName);
            this.Controls.Add(this.dtpCreated);
            this.Controls.Add(this.tbTabId);
            this.Controls.Add(this.tbPersonId);
            this.Controls.Add(this.tbDocId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmDocumentEdit";
            this.Text = "Edit Document";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbDocId;
        private System.Windows.Forms.TextBox tbPersonId;
        private System.Windows.Forms.TextBox tbTabId;
        private System.Windows.Forms.DateTimePicker dtpCreated;
        private System.Windows.Forms.TextBox tbDocName;
        private System.Windows.Forms.TextBox tbDocFullPathName;
        private System.Windows.Forms.TextBox tbGroupRestriction;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}