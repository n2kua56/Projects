namespace EZDesk
{
    partial class frmSetupDocuments
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdNew = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdDrawersNew = new System.Windows.Forms.Button();
            this.dgvDrawers = new System.Windows.Forms.DataGridView();
            this.MenuStripDrawers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addDrawerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editDrawerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDrawerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.dgvAssignedTabs = new System.Windows.Forms.DataGridView();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.dgvAvailableTabs = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.MenuStripTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditTab = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawers)).BeginInit();
            this.MenuStripDrawers.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedTabs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTabs)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.MenuStripTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(20, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 364);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 338);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Document Tabs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(286, 197);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(309, 135);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "User Defined Fields Default Values";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Location = new System.Drawing.Point(286, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 176);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tab Properties";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdNew);
            this.groupBox1.Controls.Add(this.cmdDelete);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(6, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tabs";
            // 
            // cmdNew
            // 
            this.cmdNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNew.Location = new System.Drawing.Point(193, 293);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(75, 23);
            this.cmdNew.TabIndex = 2;
            this.cmdNew.Text = "New";
            this.cmdNew.UseVisualStyleBackColor = true;
            this.cmdNew.Click += new System.EventHandler(this.cmdNew_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDelete.Location = new System.Drawing.Point(108, 293);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 1;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.MenuStripTabs;
            this.dataGridView1.Location = new System.Drawing.Point(6, 28);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(262, 254);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.Resize += new System.EventHandler(this.dataGridView1_Resize);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 338);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File Drawers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 7);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmdDrawersNew);
            this.splitContainer1.Panel1.Controls.Add(this.dgvDrawers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Size = new System.Drawing.Size(579, 325);
            this.splitContainer1.SplitterDistance = 269;
            this.splitContainer1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Drawers";
            // 
            // cmdDrawersNew
            // 
            this.cmdDrawersNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDrawersNew.Location = new System.Drawing.Point(187, 293);
            this.cmdDrawersNew.Name = "cmdDrawersNew";
            this.cmdDrawersNew.Size = new System.Drawing.Size(75, 23);
            this.cmdDrawersNew.TabIndex = 5;
            this.cmdDrawersNew.Text = "New";
            this.cmdDrawersNew.UseVisualStyleBackColor = true;
            this.cmdDrawersNew.Click += new System.EventHandler(this.cmdDrawersNew_Click);
            // 
            // dgvDrawers
            // 
            this.dgvDrawers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrawers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrawers.ContextMenuStrip = this.MenuStripDrawers;
            this.dgvDrawers.Location = new System.Drawing.Point(8, 24);
            this.dgvDrawers.Name = "dgvDrawers";
            this.dgvDrawers.Size = new System.Drawing.Size(254, 263);
            this.dgvDrawers.TabIndex = 3;
            this.dgvDrawers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDrawers_CellDoubleClick);
            this.dgvDrawers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDrawers_CellMouseDown);
            this.dgvDrawers.SelectionChanged += new System.EventHandler(this.dgvDrawers_SelectionChanged);
            this.dgvDrawers.Resize += new System.EventHandler(this.dgvDrawers_Resize);
            // 
            // MenuStripDrawers
            // 
            this.MenuStripDrawers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDrawerToolStripMenuItem,
            this.editDrawerToolStripMenuItem,
            this.deleteDrawerToolStripMenuItem});
            this.MenuStripDrawers.Name = "MenuStripDrawers";
            this.MenuStripDrawers.Size = new System.Drawing.Size(148, 70);
            // 
            // addDrawerToolStripMenuItem
            // 
            this.addDrawerToolStripMenuItem.Name = "addDrawerToolStripMenuItem";
            this.addDrawerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addDrawerToolStripMenuItem.Text = "Add Drawer";
            this.addDrawerToolStripMenuItem.Click += new System.EventHandler(this.addDrawerToolStripMenuItem_Click);
            // 
            // editDrawerToolStripMenuItem
            // 
            this.editDrawerToolStripMenuItem.Name = "editDrawerToolStripMenuItem";
            this.editDrawerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.editDrawerToolStripMenuItem.Text = "Edit Drawer";
            this.editDrawerToolStripMenuItem.Click += new System.EventHandler(this.editDrawerToolStripMenuItem_Click);
            // 
            // deleteDrawerToolStripMenuItem
            // 
            this.deleteDrawerToolStripMenuItem.Name = "deleteDrawerToolStripMenuItem";
            this.deleteDrawerToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.deleteDrawerToolStripMenuItem.Text = "Delete Drawer";
            this.deleteDrawerToolStripMenuItem.Click += new System.EventHandler(this.deleteDrawerToolStripMenuItem_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnLeft);
            this.groupBox4.Controls.Add(this.btnRight);
            this.groupBox4.Controls.Add(this.dgvAssignedTabs);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.dgvAvailableTabs);
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(303, 322);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Resize += new System.EventHandler(this.groupBox4_Resize);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Selected Tabs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Available Tabs:";
            // 
            // btnLeft
            // 
            this.btnLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLeft.Location = new System.Drawing.Point(138, 107);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(27, 23);
            this.btnLeft.TabIndex = 19;
            this.btnLeft.Text = "<<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRight.Location = new System.Drawing.Point(138, 77);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(27, 23);
            this.btnRight.TabIndex = 18;
            this.btnRight.Text = ">>";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // dgvAssignedTabs
            // 
            this.dgvAssignedTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAssignedTabs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssignedTabs.Location = new System.Drawing.Point(170, 24);
            this.dgvAssignedTabs.Name = "dgvAssignedTabs";
            this.dgvAssignedTabs.Size = new System.Drawing.Size(125, 263);
            this.dgvAssignedTabs.TabIndex = 17;
            this.dgvAssignedTabs.Resize += new System.EventHandler(this.dgvAssignedTabs_Resize);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(99, 296);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(8, 296);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // dgvAvailableTabs
            // 
            this.dgvAvailableTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAvailableTabs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableTabs.Location = new System.Drawing.Point(8, 24);
            this.dgvAvailableTabs.Name = "dgvAvailableTabs";
            this.dgvAvailableTabs.Size = new System.Drawing.Size(125, 263);
            this.dgvAvailableTabs.TabIndex = 14;
            this.dgvAvailableTabs.Resize += new System.EventHandler(this.dgvAvailableTabs_Resize);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(601, 338);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "File Types";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(6, 6);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(589, 284);
            this.dataGridView4.TabIndex = 0;
            // 
            // MenuStripTabs
            // 
            this.MenuStripTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewTab,
            this.mnuEditTab,
            this.mnuDeleteTab});
            this.MenuStripTabs.Name = "MenuStripTabs";
            this.MenuStripTabs.Size = new System.Drawing.Size(131, 70);
            // 
            // mnuNewTab
            // 
            this.mnuNewTab.Name = "mnuNewTab";
            this.mnuNewTab.Size = new System.Drawing.Size(130, 22);
            this.mnuNewTab.Text = "New Tab";
            this.mnuNewTab.Click += new System.EventHandler(this.mnuNewTab_Click);
            // 
            // mnuEditTab
            // 
            this.mnuEditTab.Name = "mnuEditTab";
            this.mnuEditTab.Size = new System.Drawing.Size(130, 22);
            this.mnuEditTab.Text = "Edit Tab";
            this.mnuEditTab.Click += new System.EventHandler(this.mnuEditTab_Click);
            // 
            // mnuDeleteTab
            // 
            this.mnuDeleteTab.Name = "mnuDeleteTab";
            this.mnuDeleteTab.Size = new System.Drawing.Size(130, 22);
            this.mnuDeleteTab.Text = "Delete Tab";
            this.mnuDeleteTab.Click += new System.EventHandler(this.mnuDeleteTab_Click);
            // 
            // frmSetupDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 388);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSetupDocuments";
            this.Text = "frmSetupDocuments";
            this.Load += new System.EventHandler(this.frmSetupDocuments_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrawers)).EndInit();
            this.MenuStripDrawers.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignedTabs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTabs)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.MenuStripTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.ContextMenuStrip MenuStripTabs;
        private System.Windows.Forms.ToolStripMenuItem mnuNewTab;
        private System.Windows.Forms.ToolStripMenuItem mnuEditTab;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdDrawersNew;
        private System.Windows.Forms.DataGridView dgvDrawers;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.DataGridView dgvAssignedTabs;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.DataGridView dgvAvailableTabs;
        private System.Windows.Forms.ContextMenuStrip MenuStripDrawers;
        private System.Windows.Forms.ToolStripMenuItem addDrawerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editDrawerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDrawerToolStripMenuItem;
    }
}