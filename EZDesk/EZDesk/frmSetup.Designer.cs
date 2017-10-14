namespace EZDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetup));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdDocuments = new System.Windows.Forms.ToolStripButton();
            this.cmdUsers = new System.Windows.Forms.ToolStripButton();
            this.cmdSystem = new System.Windows.Forms.ToolStripButton();
            this.cmdMessaging = new System.Windows.Forms.ToolStripButton();
            this.cmdMisc = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.pnlHosting = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.cmdExit);
            this.splitContainer1.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer1.Panel2.Controls.Add(this.cmdSave);
            this.splitContainer1.Panel2.Controls.Add(this.pnlHosting);
            this.splitContainer1.Size = new System.Drawing.Size(854, 509);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdDocuments,
            this.cmdUsers,
            this.cmdSystem,
            this.cmdMessaging,
            this.cmdMisc});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(7, 10);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(93, 499);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdDocuments
            // 
            this.cmdDocuments.Image = ((System.Drawing.Image)(resources.GetObject("cmdDocuments.Image")));
            this.cmdDocuments.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdDocuments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDocuments.Name = "cmdDocuments";
            this.cmdDocuments.Size = new System.Drawing.Size(91, 54);
            this.cmdDocuments.Text = "Documents";
            this.cmdDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdDocuments.Click += new System.EventHandler(this.cmdDocuments_Click);
            // 
            // cmdUsers
            // 
            this.cmdUsers.Image = ((System.Drawing.Image)(resources.GetObject("cmdUsers.Image")));
            this.cmdUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUsers.Name = "cmdUsers";
            this.cmdUsers.Size = new System.Drawing.Size(91, 52);
            this.cmdUsers.Text = "Users";
            this.cmdUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdUsers.Click += new System.EventHandler(this.cmdUsers_Click);
            // 
            // cmdSystem
            // 
            this.cmdSystem.Image = ((System.Drawing.Image)(resources.GetObject("cmdSystem.Image")));
            this.cmdSystem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSystem.Name = "cmdSystem";
            this.cmdSystem.Size = new System.Drawing.Size(91, 56);
            this.cmdSystem.Text = "System";
            this.cmdSystem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdSystem.Click += new System.EventHandler(this.cmdSystem_Click);
            // 
            // cmdMessaging
            // 
            this.cmdMessaging.Image = ((System.Drawing.Image)(resources.GetObject("cmdMessaging.Image")));
            this.cmdMessaging.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMessaging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdMessaging.Name = "cmdMessaging";
            this.cmdMessaging.Size = new System.Drawing.Size(91, 54);
            this.cmdMessaging.Text = "Messaging";
            this.cmdMessaging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdMessaging.Click += new System.EventHandler(this.cmdMessaging_Click);
            // 
            // cmdMisc
            // 
            this.cmdMisc.Image = ((System.Drawing.Image)(resources.GetObject("cmdMisc.Image")));
            this.cmdMisc.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdMisc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdMisc.Name = "cmdMisc";
            this.cmdMisc.Size = new System.Drawing.Size(91, 49);
            this.cmdMisc.Text = "Misc";
            this.cmdMisc.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cmdMisc.Click += new System.EventHandler(this.cmdMisc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 481);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(532, 477);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Location = new System.Drawing.Point(329, 476);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.TabIndex = 3;
            this.cmdExit.Text = "Exit";
            this.cmdExit.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdCancel.Location = new System.Drawing.Point(230, 476);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSave.Location = new System.Drawing.Point(135, 476);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 1;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            // 
            // pnlHosting
            // 
            this.pnlHosting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHosting.Location = new System.Drawing.Point(0, 0);
            this.pnlHosting.Name = "pnlHosting";
            this.pnlHosting.Size = new System.Drawing.Size(741, 467);
            this.pnlHosting.TabIndex = 0;
            // 
            // frmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(854, 509);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetup";
            this.Text = "frmSetup";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdDocuments;
        private System.Windows.Forms.ToolStripButton cmdUsers;
        private System.Windows.Forms.ToolStripButton cmdSystem;
        private System.Windows.Forms.ToolStripButton cmdMessaging;
        private System.Windows.Forms.ToolStripButton cmdMisc;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Panel pnlHosting;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
    }
}