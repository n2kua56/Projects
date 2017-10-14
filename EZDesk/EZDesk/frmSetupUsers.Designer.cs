namespace EZDesk
{
    partial class frmSetupUsers
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpUsers = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbProfileGroup = new System.Windows.Forms.TextBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSuffix = new System.Windows.Forms.TextBox();
            this.tbMI = new System.Windows.Forms.TextBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdMoveLeft = new System.Windows.Forms.Button();
            this.cmdMoveRight = new System.Windows.Forms.Button();
            this.dgvUserDrawers = new System.Windows.Forms.DataGridView();
            this.rbAlphabetically = new System.Windows.Forms.RadioButton();
            this.rbBySortOrder = new System.Windows.Forms.RadioButton();
            this.dgvAvailableDrawers = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ddControl1 = new Dropdown_Button.DDControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.doctorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clinicalStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUserNames = new System.Windows.Forms.DataGridView();
            this.tpProfiles = new System.Windows.Forms.TabPage();
            this.tpProfileGroups = new System.Windows.Forms.TabPage();
            this.tpCurrentProfile = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tpUsers.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserDrawers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableDrawers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserNames)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpUsers);
            this.tabControl1.Controls.Add(this.tpProfiles);
            this.tabControl1.Controls.Add(this.tpProfileGroups);
            this.tabControl1.Controls.Add(this.tpCurrentProfile);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(717, 379);
            this.tabControl1.TabIndex = 0;
            // 
            // tpUsers
            // 
            this.tpUsers.Controls.Add(this.groupBox3);
            this.tpUsers.Controls.Add(this.groupBox2);
            this.tpUsers.Controls.Add(this.groupBox1);
            this.tpUsers.Location = new System.Drawing.Point(4, 22);
            this.tpUsers.Name = "tpUsers";
            this.tpUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tpUsers.Size = new System.Drawing.Size(709, 353);
            this.tpUsers.TabIndex = 0;
            this.tpUsers.Text = "Users";
            this.tpUsers.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbProfileGroup);
            this.groupBox3.Controls.Add(this.tbLastName);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tbSuffix);
            this.groupBox3.Controls.Add(this.tbMI);
            this.groupBox3.Controls.Add(this.tbFirstName);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(287, 196);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(414, 151);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Name, Password and Profile Group";
            this.groupBox3.Resize += new System.EventHandler(this.groupBox3_Resize);
            // 
            // tbProfileGroup
            // 
            this.tbProfileGroup.Location = new System.Drawing.Point(282, 43);
            this.tbProfileGroup.Name = "tbProfileGroup";
            this.tbProfileGroup.Size = new System.Drawing.Size(115, 20);
            this.tbProfileGroup.TabIndex = 15;
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(282, 17);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(115, 20);
            this.tbLastName.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Profile Group";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Last Name";
            // 
            // tbSuffix
            // 
            this.tbSuffix.Location = new System.Drawing.Point(71, 69);
            this.tbSuffix.Name = "tbSuffix";
            this.tbSuffix.Size = new System.Drawing.Size(115, 20);
            this.tbSuffix.TabIndex = 7;
            // 
            // tbMI
            // 
            this.tbMI.Location = new System.Drawing.Point(71, 43);
            this.tbMI.Name = "tbMI";
            this.tbMI.Size = new System.Drawing.Size(115, 20);
            this.tbMI.TabIndex = 6;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(71, 17);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(115, 20);
            this.tbFirstName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Suffix";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "MI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmdMoveLeft);
            this.groupBox2.Controls.Add(this.cmdMoveRight);
            this.groupBox2.Controls.Add(this.dgvUserDrawers);
            this.groupBox2.Controls.Add(this.rbAlphabetically);
            this.groupBox2.Controls.Add(this.rbBySortOrder);
            this.groupBox2.Controls.Add(this.dgvAvailableDrawers);
            this.groupBox2.Location = new System.Drawing.Point(288, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 184);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Drawers for nnnn";
            this.groupBox2.Resize += new System.EventHandler(this.groupBox2_Resize);
            // 
            // cmdMoveLeft
            // 
            this.cmdMoveLeft.Location = new System.Drawing.Point(192, 78);
            this.cmdMoveLeft.Name = "cmdMoveLeft";
            this.cmdMoveLeft.Size = new System.Drawing.Size(27, 23);
            this.cmdMoveLeft.TabIndex = 5;
            this.cmdMoveLeft.Text = ">>";
            this.cmdMoveLeft.UseVisualStyleBackColor = true;
            // 
            // cmdMoveRight
            // 
            this.cmdMoveRight.Location = new System.Drawing.Point(192, 39);
            this.cmdMoveRight.Name = "cmdMoveRight";
            this.cmdMoveRight.Size = new System.Drawing.Size(27, 23);
            this.cmdMoveRight.TabIndex = 4;
            this.cmdMoveRight.Text = ">>";
            this.cmdMoveRight.UseVisualStyleBackColor = true;
            // 
            // dgvUserDrawers
            // 
            this.dgvUserDrawers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserDrawers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserDrawers.Location = new System.Drawing.Point(224, 19);
            this.dgvUserDrawers.Name = "dgvUserDrawers";
            this.dgvUserDrawers.Size = new System.Drawing.Size(181, 120);
            this.dgvUserDrawers.TabIndex = 3;
            // 
            // rbAlphabetically
            // 
            this.rbAlphabetically.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbAlphabetically.AutoSize = true;
            this.rbAlphabetically.Checked = true;
            this.rbAlphabetically.Location = new System.Drawing.Point(6, 162);
            this.rbAlphabetically.Name = "rbAlphabetically";
            this.rbAlphabetically.Size = new System.Drawing.Size(109, 17);
            this.rbAlphabetically.TabIndex = 2;
            this.rbAlphabetically.TabStop = true;
            this.rbAlphabetically.Text = "List Alphabetically";
            this.rbAlphabetically.UseVisualStyleBackColor = true;
            // 
            // rbBySortOrder
            // 
            this.rbBySortOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbBySortOrder.AutoSize = true;
            this.rbBySortOrder.Location = new System.Drawing.Point(6, 145);
            this.rbBySortOrder.Name = "rbBySortOrder";
            this.rbBySortOrder.Size = new System.Drawing.Size(106, 17);
            this.rbBySortOrder.TabIndex = 1;
            this.rbBySortOrder.Text = "List by Sort Order";
            this.rbBySortOrder.UseVisualStyleBackColor = true;
            // 
            // dgvAvailableDrawers
            // 
            this.dgvAvailableDrawers.AllowUserToAddRows = false;
            this.dgvAvailableDrawers.AllowUserToDeleteRows = false;
            this.dgvAvailableDrawers.AllowUserToResizeColumns = false;
            this.dgvAvailableDrawers.AllowUserToResizeRows = false;
            this.dgvAvailableDrawers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAvailableDrawers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableDrawers.Location = new System.Drawing.Point(6, 19);
            this.dgvAvailableDrawers.MultiSelect = false;
            this.dgvAvailableDrawers.Name = "dgvAvailableDrawers";
            this.dgvAvailableDrawers.RowHeadersVisible = false;
            this.dgvAvailableDrawers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableDrawers.Size = new System.Drawing.Size(181, 120);
            this.dgvAvailableDrawers.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.ddControl1);
            this.groupBox1.Controls.Add(this.dgvUserNames);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Names";
            this.groupBox1.Resize += new System.EventHandler(this.groupBox1_Resize);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(183, 316);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "label11";
            // 
            // ddControl1
            // 
            this.ddControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ddControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.ddControl1.Location = new System.Drawing.Point(2, 312);
            this.ddControl1.Name = "ddControl1";
            this.ddControl1.Size = new System.Drawing.Size(66, 24);
            this.ddControl1.TabIndex = 3;
            this.ddControl1.ItemClickedEvent += new Dropdown_Button.ItemClickedDelegate(this.ddControl1_ItemClickedEvent);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doctorToolStripMenuItem,
            this.clinicalStaffToolStripMenuItem,
            this.staffToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(134, 70);
            // 
            // doctorToolStripMenuItem
            // 
            this.doctorToolStripMenuItem.Name = "doctorToolStripMenuItem";
            this.doctorToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.doctorToolStripMenuItem.Text = "Doctor";
            // 
            // clinicalStaffToolStripMenuItem
            // 
            this.clinicalStaffToolStripMenuItem.Name = "clinicalStaffToolStripMenuItem";
            this.clinicalStaffToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.clinicalStaffToolStripMenuItem.Text = "Clinical Staff";
            // 
            // staffToolStripMenuItem
            // 
            this.staffToolStripMenuItem.Name = "staffToolStripMenuItem";
            this.staffToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.staffToolStripMenuItem.Text = "Staff";
            // 
            // dgvUserNames
            // 
            this.dgvUserNames.AllowUserToAddRows = false;
            this.dgvUserNames.AllowUserToDeleteRows = false;
            this.dgvUserNames.AllowUserToResizeRows = false;
            this.dgvUserNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUserNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserNames.Location = new System.Drawing.Point(6, 19);
            this.dgvUserNames.MultiSelect = false;
            this.dgvUserNames.Name = "dgvUserNames";
            this.dgvUserNames.ReadOnly = true;
            this.dgvUserNames.RowHeadersVisible = false;
            this.dgvUserNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserNames.Size = new System.Drawing.Size(265, 289);
            this.dgvUserNames.TabIndex = 0;
            this.dgvUserNames.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvUserNames_CellMouseDoubleClick);
            this.dgvUserNames.SelectionChanged += new System.EventHandler(this.dgvUserNames_SelectionChanged);
            // 
            // tpProfiles
            // 
            this.tpProfiles.Location = new System.Drawing.Point(4, 22);
            this.tpProfiles.Name = "tpProfiles";
            this.tpProfiles.Size = new System.Drawing.Size(709, 353);
            this.tpProfiles.TabIndex = 4;
            this.tpProfiles.Text = "Profiles";
            this.tpProfiles.UseVisualStyleBackColor = true;
            // 
            // tpProfileGroups
            // 
            this.tpProfileGroups.Location = new System.Drawing.Point(4, 22);
            this.tpProfileGroups.Name = "tpProfileGroups";
            this.tpProfileGroups.Size = new System.Drawing.Size(709, 353);
            this.tpProfileGroups.TabIndex = 5;
            this.tpProfileGroups.Text = "Profile Groups";
            this.tpProfileGroups.UseVisualStyleBackColor = true;
            // 
            // tpCurrentProfile
            // 
            this.tpCurrentProfile.Location = new System.Drawing.Point(4, 22);
            this.tpCurrentProfile.Name = "tpCurrentProfile";
            this.tpCurrentProfile.Size = new System.Drawing.Size(709, 353);
            this.tpCurrentProfile.TabIndex = 6;
            this.tpCurrentProfile.Text = "Current Profile";
            this.tpCurrentProfile.UseVisualStyleBackColor = true;
            // 
            // frmSetupUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 403);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSetupUsers";
            this.Text = "frmSetupUsers";
            this.Load += new System.EventHandler(this.frmSetupUsers_Load);
            this.Resize += new System.EventHandler(this.frmSetupUsers_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tpUsers.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserDrawers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableDrawers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserNames)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvUserNames;
        private System.Windows.Forms.TabPage tpProfiles;
        private System.Windows.Forms.TabPage tpProfileGroups;
        private System.Windows.Forms.TabPage tpCurrentProfile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbProfileGroup;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSuffix;
        private System.Windows.Forms.TextBox tbMI;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdMoveLeft;
        private System.Windows.Forms.Button cmdMoveRight;
        private System.Windows.Forms.DataGridView dgvUserDrawers;
        private System.Windows.Forms.RadioButton rbAlphabetically;
        private System.Windows.Forms.RadioButton rbBySortOrder;
        private System.Windows.Forms.DataGridView dgvAvailableDrawers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem doctorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clinicalStaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffToolStripMenuItem;
        private Dropdown_Button.DDControl ddControl1;
        private System.Windows.Forms.Label label11;
    }
}