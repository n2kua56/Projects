namespace EZDesk
{
    partial class frmSetupSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetupSystem));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpProperties = new System.Windows.Forms.TabPage();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.tpCustomText = new System.Windows.Forms.TabPage();
            this.tpUserDefinedFields = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpProperties);
            this.tabControl1.Controls.Add(this.tpCustomText);
            this.tabControl1.Controls.Add(this.tpUserDefinedFields);
            this.tabControl1.Location = new System.Drawing.Point(1, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 335);
            this.tabControl1.TabIndex = 0;
            // 
            // tpProperties
            // 
            this.tpProperties.Controls.Add(this.dgvProperties);
            this.tpProperties.Location = new System.Drawing.Point(4, 22);
            this.tpProperties.Name = "tpProperties";
            this.tpProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tpProperties.Size = new System.Drawing.Size(561, 309);
            this.tpProperties.TabIndex = 0;
            this.tpProperties.Text = "Properties";
            this.tpProperties.UseVisualStyleBackColor = true;
            // 
            // dgvProperties
            // 
            this.dgvProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.Location = new System.Drawing.Point(7, 6);
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProperties.Size = new System.Drawing.Size(548, 297);
            this.dgvProperties.TabIndex = 0;
            this.dgvProperties.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProperties_CellMouseDoubleClick);
            this.dgvProperties.Resize += new System.EventHandler(this.dgvProperties_Resize);
            // 
            // tpCustomText
            // 
            this.tpCustomText.Location = new System.Drawing.Point(4, 22);
            this.tpCustomText.Name = "tpCustomText";
            this.tpCustomText.Padding = new System.Windows.Forms.Padding(3);
            this.tpCustomText.Size = new System.Drawing.Size(561, 309);
            this.tpCustomText.TabIndex = 1;
            this.tpCustomText.Text = "Custom Text";
            this.tpCustomText.UseVisualStyleBackColor = true;
            // 
            // tpUserDefinedFields
            // 
            this.tpUserDefinedFields.Location = new System.Drawing.Point(4, 22);
            this.tpUserDefinedFields.Name = "tpUserDefinedFields";
            this.tpUserDefinedFields.Size = new System.Drawing.Size(561, 309);
            this.tpUserDefinedFields.TabIndex = 2;
            this.tpUserDefinedFields.Text = "User Defined Fields";
            this.tpUserDefinedFields.UseVisualStyleBackColor = true;
            // 
            // frmSetupSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 345);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetupSystem";
            this.Text = "frmSetupSystem";
            this.Load += new System.EventHandler(this.frmSetupSystem_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpProperties;
        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.TabPage tpCustomText;
        private System.Windows.Forms.TabPage tpUserDefinedFields;
    }
}