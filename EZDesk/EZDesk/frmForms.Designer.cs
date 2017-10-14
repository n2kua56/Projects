namespace EZDesk
{
    partial class frmForms
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
            this.dgvForms = new System.Windows.Forms.DataGridView();
            this.cmdCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvForms)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvForms
            // 
            this.dgvForms.AllowUserToAddRows = false;
            this.dgvForms.AllowUserToDeleteRows = false;
            this.dgvForms.AllowUserToResizeColumns = false;
            this.dgvForms.AllowUserToResizeRows = false;
            this.dgvForms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvForms.Location = new System.Drawing.Point(12, 53);
            this.dgvForms.MultiSelect = false;
            this.dgvForms.Name = "dgvForms";
            this.dgvForms.ReadOnly = true;
            this.dgvForms.RowHeadersVisible = false;
            this.dgvForms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvForms.Size = new System.Drawing.Size(501, 289);
            this.dgvForms.TabIndex = 0;
            this.dgvForms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvForms_CellDoubleClick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(438, 360);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // frmForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(525, 395);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dgvForms);
            this.Name = "frmForms";
            this.Text = "EZDesk Forms";
            this.Load += new System.EventHandler(this.frmForms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvForms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvForms;
        private System.Windows.Forms.Button cmdCancel;
    }
}