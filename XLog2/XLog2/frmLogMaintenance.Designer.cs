namespace XLog2
{
    partial class frmLogMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogMaintenance));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mergeLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renumberLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.tbRename = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 47);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(147, 212);
            this.listBox1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listBox1, "Right Click on the Log you want to work on.");
            this.listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeLogsToolStripMenuItem,
            this.deleteLogToolStripMenuItem,
            this.renumberLogToolStripMenuItem,
            this.renameLogToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // mergeLogsToolStripMenuItem
            // 
            this.mergeLogsToolStripMenuItem.Name = "mergeLogsToolStripMenuItem";
            this.mergeLogsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mergeLogsToolStripMenuItem.Text = "Merge Logs";
            this.mergeLogsToolStripMenuItem.Click += new System.EventHandler(this.mergeLogsToolStripMenuItem_Click);
            // 
            // deleteLogToolStripMenuItem
            // 
            this.deleteLogToolStripMenuItem.Name = "deleteLogToolStripMenuItem";
            this.deleteLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteLogToolStripMenuItem.Text = "Delete Log";
            this.deleteLogToolStripMenuItem.Click += new System.EventHandler(this.deleteLogToolStripMenuItem_Click);
            // 
            // renumberLogToolStripMenuItem
            // 
            this.renumberLogToolStripMenuItem.Name = "renumberLogToolStripMenuItem";
            this.renumberLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renumberLogToolStripMenuItem.Text = "Renumber Log";
            this.renumberLogToolStripMenuItem.Click += new System.EventHandler(this.renumberLogToolStripMenuItem_Click);
            // 
            // renameLogToolStripMenuItem
            // 
            this.renameLogToolStripMenuItem.Name = "renameLogToolStripMenuItem";
            this.renameLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameLogToolStripMenuItem.Text = "Rename Log";
            this.renameLogToolStripMenuItem.Click += new System.EventHandler(this.renameLogToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Logs";
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.Enabled = false;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(191, 47);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(147, 212);
            this.listBox2.TabIndex = 3;
            this.listBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(187, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Merge With:";
            this.label2.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(190, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(273, 274);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(65, 23);
            this.btnMerge.TabIndex = 6;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Visible = false;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // tbRename
            // 
            this.tbRename.Enabled = false;
            this.tbRename.Location = new System.Drawing.Point(191, 47);
            this.tbRename.Name = "tbRename";
            this.tbRename.Size = new System.Drawing.Size(148, 20);
            this.tbRename.TabIndex = 7;
            this.tbRename.Visible = false;
            // 
            // frmLogMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 350);
            this.Controls.Add(this.tbRename);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogMaintenance";
            this.Text = "Log Mainenence";
            this.Load += new System.EventHandler(this.frmLogMaintenance_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem mergeLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renumberLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameLogToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.TextBox tbRename;
    }
}