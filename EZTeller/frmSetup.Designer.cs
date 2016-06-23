namespace EZTeller
{
    partial class frmSetup
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ckbLogWarning = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbResetAbove = new System.Windows.Forms.TextBox();
            this.cmdReset = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbEnvLast = new System.Windows.Forms.TextBox();
            this.tbEnvFirst = new System.Windows.Forms.TextBox();
            this.tbSpecial = new System.Windows.Forms.TextBox();
            this.tbCash = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdChange = new System.Windows.Forms.Button();
            this.tbMySQLpassword = new System.Windows.Forms.TextBox();
            this.tbMySQLuserid = new System.Windows.Forms.TextBox();
            this.tbMySQLaddress = new System.Windows.Forms.TextBox();
            this.tbMySQLdatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tbInitials = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Location = new System.Drawing.Point(616, 341);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.TabIndex = 18;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(342, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(349, 18);
            this.label13.TabIndex = 16;
            this.label13.Text = "Select Local Zip Codes";
            this.label13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(342, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(349, 304);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.TabStop = false;
            // 
            // ckbLogWarning
            // 
            this.ckbLogWarning.AutoSize = true;
            this.ckbLogWarning.Location = new System.Drawing.Point(180, 113);
            this.ckbLogWarning.Name = "ckbLogWarning";
            this.ckbLogWarning.Size = new System.Drawing.Size(87, 17);
            this.ckbLogWarning.TabIndex = 6;
            this.ckbLogWarning.Text = "Log Warning";
            this.ckbLogWarning.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(50, 163);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(277, 20);
            this.tbName.TabIndex = 7;
            this.tbName.Tag = "Teller_ChurchName";
            this.tbName.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.tbResetAbove);
            this.groupBox3.Controls.Add(this.cmdReset);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(167, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 80);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Reset to 0";
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(6, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 28);
            this.label12.TabIndex = 3;
            this.label12.Text = "Use CAREFULLY";
            // 
            // tbResetAbove
            // 
            this.tbResetAbove.Location = new System.Drawing.Point(59, 25);
            this.tbResetAbove.Name = "tbResetAbove";
            this.tbResetAbove.Size = new System.Drawing.Size(82, 20);
            this.tbResetAbove.TabIndex = 10;
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(87, 51);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(54, 23);
            this.cmdReset.TabIndex = 11;
            this.cmdReset.Text = "Reset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Above:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbEnvLast);
            this.groupBox2.Controls.Add(this.tbEnvFirst);
            this.groupBox2.Controls.Add(this.tbSpecial);
            this.groupBox2.Controls.Add(this.tbCash);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(9, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(152, 148);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Special Numbers";
            // 
            // tbEnvLast
            // 
            this.tbEnvLast.Location = new System.Drawing.Point(79, 116);
            this.tbEnvLast.Name = "tbEnvLast";
            this.tbEnvLast.Size = new System.Drawing.Size(58, 20);
            this.tbEnvLast.TabIndex = 5;
            this.tbEnvLast.Tag = "eztellerCELast";
            this.tbEnvLast.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            this.tbEnvLast.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCash_KeyPress);
            // 
            // tbEnvFirst
            // 
            this.tbEnvFirst.Location = new System.Drawing.Point(79, 90);
            this.tbEnvFirst.Name = "tbEnvFirst";
            this.tbEnvFirst.Size = new System.Drawing.Size(58, 20);
            this.tbEnvFirst.TabIndex = 4;
            this.tbEnvFirst.Tag = "eztellerCEFirst";
            this.tbEnvFirst.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            this.tbEnvFirst.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCash_KeyPress);
            // 
            // tbSpecial
            // 
            this.tbSpecial.Location = new System.Drawing.Point(79, 46);
            this.tbSpecial.Name = "tbSpecial";
            this.tbSpecial.Size = new System.Drawing.Size(58, 20);
            this.tbSpecial.TabIndex = 3;
            this.tbSpecial.Tag = "eztellerSpecial2";
            this.tbSpecial.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            this.tbSpecial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCash_KeyPress);
            // 
            // tbCash
            // 
            this.tbCash.Location = new System.Drawing.Point(79, 20);
            this.tbCash.Name = "tbCash";
            this.tbCash.Size = new System.Drawing.Size(58, 20);
            this.tbCash.TabIndex = 2;
            this.tbCash.Tag = "eztellerSpecial1";
            this.tbCash.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            this.tbCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCash_KeyPress);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 23);
            this.label9.TabIndex = 4;
            this.label9.Text = "Last:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 23);
            this.label8.TabIndex = 3;
            this.label8.Text = "First:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Envelope Numbers:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 23);
            this.label6.TabIndex = 1;
            this.label6.Text = "Special:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(9, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cash:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdChange);
            this.groupBox1.Controls.Add(this.tbMySQLpassword);
            this.groupBox1.Controls.Add(this.tbMySQLuserid);
            this.groupBox1.Controls.Add(this.tbMySQLaddress);
            this.groupBox1.Controls.Add(this.tbMySQLdatabase);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 150);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // cmdChange
            // 
            this.cmdChange.Location = new System.Drawing.Point(228, 122);
            this.cmdChange.Name = "cmdChange";
            this.cmdChange.Size = new System.Drawing.Size(75, 23);
            this.cmdChange.TabIndex = 8;
            this.cmdChange.TabStop = false;
            this.cmdChange.Text = "Change";
            this.cmdChange.UseVisualStyleBackColor = true;
            this.cmdChange.Click += new System.EventHandler(this.cmdChange_Click);
            // 
            // tbMySQLpassword
            // 
            this.tbMySQLpassword.Location = new System.Drawing.Point(68, 96);
            this.tbMySQLpassword.Name = "tbMySQLpassword";
            this.tbMySQLpassword.ReadOnly = true;
            this.tbMySQLpassword.Size = new System.Drawing.Size(235, 20);
            this.tbMySQLpassword.TabIndex = 16;
            this.tbMySQLpassword.TabStop = false;
            // 
            // tbMySQLuserid
            // 
            this.tbMySQLuserid.Location = new System.Drawing.Point(68, 70);
            this.tbMySQLuserid.Name = "tbMySQLuserid";
            this.tbMySQLuserid.ReadOnly = true;
            this.tbMySQLuserid.Size = new System.Drawing.Size(235, 20);
            this.tbMySQLuserid.TabIndex = 15;
            this.tbMySQLuserid.TabStop = false;
            // 
            // tbMySQLaddress
            // 
            this.tbMySQLaddress.Location = new System.Drawing.Point(68, 44);
            this.tbMySQLaddress.Name = "tbMySQLaddress";
            this.tbMySQLaddress.ReadOnly = true;
            this.tbMySQLaddress.Size = new System.Drawing.Size(235, 20);
            this.tbMySQLaddress.TabIndex = 14;
            this.tbMySQLaddress.TabStop = false;
            // 
            // tbMySQLdatabase
            // 
            this.tbMySQLdatabase.Location = new System.Drawing.Point(68, 18);
            this.tbMySQLdatabase.Name = "tbMySQLdatabase";
            this.tbMySQLdatabase.ReadOnly = true;
            this.tbMySQLdatabase.Size = new System.Drawing.Size(235, 20);
            this.tbMySQLdatabase.TabIndex = 13;
            this.tbMySQLdatabase.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 189);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Initials:";
            // 
            // tbInitials
            // 
            this.tbInitials.Location = new System.Drawing.Point(50, 189);
            this.tbInitials.Name = "tbInitials";
            this.tbInitials.Size = new System.Drawing.Size(100, 20);
            this.tbInitials.TabIndex = 8;
            this.tbInitials.Tag = "ezTellerInitials";
            this.tbInitials.TextChanged += new System.EventHandler(this.tbCash_TextChanged);
            // 
            // frmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 377);
            this.Controls.Add(this.tbInitials);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ckbLogWarning);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmSetup";
            this.Text = "frmSetup";
            this.Load += new System.EventHandler(this.frmSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox ckbLogWarning;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbResetAbove;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbEnvLast;
        private System.Windows.Forms.TextBox tbEnvFirst;
        private System.Windows.Forms.TextBox tbSpecial;
        private System.Windows.Forms.TextBox tbCash;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdChange;
        private System.Windows.Forms.TextBox tbMySQLpassword;
        private System.Windows.Forms.TextBox tbMySQLuserid;
        private System.Windows.Forms.TextBox tbMySQLaddress;
        private System.Windows.Forms.TextBox tbMySQLdatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbInitials;
    }
}