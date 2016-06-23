﻿namespace ToDo
{
    partial class frmToDo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmToDo));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdDone = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.cmsListMode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.archiveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdBack = new System.Windows.Forms.Button();
            this.cmsTaskMode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.setTargetDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbShowArchived = new System.Windows.Forms.CheckBox();
            this.cbShowDeleted = new System.Windows.Forms.CheckBox();
            this.cbShowCompleted = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cmsListMode.SuspendLayout();
            this.cmsTaskMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(448, 196);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.Resize += new System.EventHandler(this.dataGridView1_Resize);
            // 
            // cmdDone
            // 
            this.cmdDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDone.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdDone.Location = new System.Drawing.Point(385, 245);
            this.cmdDone.Name = "cmdDone";
            this.cmdDone.Size = new System.Drawing.Size(75, 23);
            this.cmdDone.TabIndex = 1;
            this.cmdDone.Text = "Done";
            this.cmdDone.UseVisualStyleBackColor = true;
            this.cmdDone.Click += new System.EventHandler(this.cmdDone_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAdd.Location = new System.Drawing.Point(409, 12);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(51, 23);
            this.cmdAdd.TabIndex = 2;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(12, 15);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(334, 20);
            this.tbTitle.TabIndex = 3;
            this.tbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTitle_KeyDown);
            // 
            // cmsListMode
            // 
            this.cmsListMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.archiveListToolStripMenuItem});
            this.cmsListMode.Name = "cmsListMode";
            this.cmsListMode.Size = new System.Drawing.Size(130, 76);
            this.cmsListMode.Opening += new System.ComponentModel.CancelEventHandler(this.cmsListMode_Opening);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameListToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteListToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(126, 6);
            // 
            // archiveListToolStripMenuItem
            // 
            this.archiveListToolStripMenuItem.Enabled = false;
            this.archiveListToolStripMenuItem.Name = "archiveListToolStripMenuItem";
            this.archiveListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.archiveListToolStripMenuItem.Text = "Archive List";
            // 
            // cmdBack
            // 
            this.cmdBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBack.Location = new System.Drawing.Point(352, 12);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(51, 23);
            this.cmdBack.TabIndex = 4;
            this.cmdBack.Text = "Back";
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // cmsTaskMode
            // 
            this.cmsTaskMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.setTargetDateToolStripMenuItem,
            this.toolStripMenuItem1,
            this.archiveToolStripMenuItem});
            this.cmsTaskMode.Name = "cmsTaskMode";
            this.cmsTaskMode.Size = new System.Drawing.Size(152, 98);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.renameToolStripMenuItem1.Text = "Rename";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameTaskToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteTaskToolStripMenuItem1_Click);
            // 
            // setTargetDateToolStripMenuItem
            // 
            this.setTargetDateToolStripMenuItem.Name = "setTargetDateToolStripMenuItem";
            this.setTargetDateToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.setTargetDateToolStripMenuItem.Text = "Set Target Date";
            this.setTargetDateToolStripMenuItem.Click += new System.EventHandler(this.setTaskTargetDateToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.Enabled = false;
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.archiveToolStripMenuItem.Text = "Archive";
            // 
            // cbShowArchived
            // 
            this.cbShowArchived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowArchived.AutoSize = true;
            this.cbShowArchived.Enabled = false;
            this.cbShowArchived.Location = new System.Drawing.Point(12, 246);
            this.cbShowArchived.Name = "cbShowArchived";
            this.cbShowArchived.Size = new System.Drawing.Size(98, 17);
            this.cbShowArchived.TabIndex = 5;
            this.cbShowArchived.Text = "Show Archived";
            this.cbShowArchived.UseVisualStyleBackColor = true;
            // 
            // cbShowDeleted
            // 
            this.cbShowDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowDeleted.AutoSize = true;
            this.cbShowDeleted.Location = new System.Drawing.Point(119, 246);
            this.cbShowDeleted.Name = "cbShowDeleted";
            this.cbShowDeleted.Size = new System.Drawing.Size(93, 17);
            this.cbShowDeleted.TabIndex = 6;
            this.cbShowDeleted.Text = "Show Deleted";
            this.cbShowDeleted.UseVisualStyleBackColor = true;
            // 
            // cbShowCompleted
            // 
            this.cbShowCompleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbShowCompleted.AutoSize = true;
            this.cbShowCompleted.Checked = true;
            this.cbShowCompleted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowCompleted.Location = new System.Drawing.Point(219, 246);
            this.cbShowCompleted.Name = "cbShowCompleted";
            this.cbShowCompleted.Size = new System.Drawing.Size(106, 17);
            this.cbShowCompleted.TabIndex = 7;
            this.cbShowCompleted.Text = "Show Completed";
            this.cbShowCompleted.UseVisualStyleBackColor = true;
            this.cbShowCompleted.Click += new System.EventHandler(this.cbShowCompleted_Click);
            // 
            // frmToDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdDone;
            this.ClientSize = new System.Drawing.Size(472, 273);
            this.Controls.Add(this.cbShowCompleted);
            this.Controls.Add(this.cbShowDeleted);
            this.Controls.Add(this.cbShowArchived);
            this.Controls.Add(this.cmdBack);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdDone);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmToDo";
            this.Text = "TO DO List";
            this.Load += new System.EventHandler(this.frmToDo_Load);
            this.Shown += new System.EventHandler(this.frmToDo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cmsListMode.ResumeLayout(false);
            this.cmsTaskMode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdDone;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.ContextMenuStrip cmsListMode;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.ContextMenuStrip cmsTaskMode;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem setTargetDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem archiveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbShowArchived;
        private System.Windows.Forms.CheckBox cbShowDeleted;
        private System.Windows.Forms.CheckBox cbShowCompleted;
    }
}