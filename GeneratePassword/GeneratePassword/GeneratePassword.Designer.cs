namespace GeneratePassword
{
    partial class GeneratePassword
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
            this.label2 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.cmdGenerate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbEncryptedPassword = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSQL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(96, 68);
            this.tbPassword.MaxLength = 8;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(241, 20);
            this.tbPassword.TabIndex = 3;
            this.toolTip1.SetToolTip(this.tbPassword, "This is the plain text \'Password\' that will be used\r\nby the above \'User Name\' to " +
        "log into the \r\neRxAdmin tool. It MUST be 8 characters or less\r\nand have no blank" +
        "s and no special characters.");
            this.tbPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserName_KeyPress);
            // 
            // cmdGenerate
            // 
            this.cmdGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGenerate.Location = new System.Drawing.Point(262, 109);
            this.cmdGenerate.Name = "cmdGenerate";
            this.cmdGenerate.Size = new System.Drawing.Size(75, 23);
            this.cmdGenerate.TabIndex = 4;
            this.cmdGenerate.Text = "Generate";
            this.cmdGenerate.UseVisualStyleBackColor = true;
            this.cmdGenerate.Click += new System.EventHandler(this.cmdGenerate_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "User Name:";
            // 
            // tbUserName
            // 
            this.tbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUserName.Location = new System.Drawing.Point(96, 24);
            this.tbUserName.MaxLength = 8;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(241, 20);
            this.tbUserName.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tbUserName, "This is the \'User Name\' the user will supply to\r\nlog into the erxAdmin tool. MUST" +
        " be 8 or fewer \r\ncharacters, no spaces and no special characters.");
            this.tbUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbUserName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Encrypted Password:";
            // 
            // tbEncryptedPassword
            // 
            this.tbEncryptedPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEncryptedPassword.Location = new System.Drawing.Point(37, 157);
            this.tbEncryptedPassword.Name = "tbEncryptedPassword";
            this.tbEncryptedPassword.ReadOnly = true;
            this.tbEncryptedPassword.Size = new System.Drawing.Size(300, 20);
            this.tbEncryptedPassword.TabIndex = 6;
            this.tbEncryptedPassword.TabStop = false;
            this.toolTip1.SetToolTip(this.tbEncryptedPassword, "This is the Encrypted Password that should be placed\r\ninto the Authentication tab" +
        "le with the \'User Name\'.");
            // 
            // cmdSQL
            // 
            this.cmdSQL.Location = new System.Drawing.Point(262, 200);
            this.cmdSQL.Name = "cmdSQL";
            this.cmdSQL.Size = new System.Drawing.Size(75, 23);
            this.cmdSQL.TabIndex = 7;
            this.cmdSQL.Text = "SQL";
            this.cmdSQL.UseVisualStyleBackColor = true;
            this.cmdSQL.Click += new System.EventHandler(this.cmdSQL_Click);
            // 
            // GeneratePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 235);
            this.Controls.Add(this.cmdSQL);
            this.Controls.Add(this.tbEncryptedPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdGenerate);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "GeneratePassword";
            this.Text = "eRxAdmin Generate Encrypted Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button cmdGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbEncryptedPassword;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button cmdSQL;
    }
}

