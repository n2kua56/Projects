namespace HamLogBook
{
    partial class HLBInputBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl = new System.Windows.Forms.Label();
            this.tb1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl
            // 
            this.lbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(1, 1);
            this.lbl.MaximumSize = new System.Drawing.Size(1000, 23);
            this.lbl.MinimumSize = new System.Drawing.Size(50, 23);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(280, 23);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "label1";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl.TextChanged += new System.EventHandler(this.lbl_TextChanged);
            // 
            // tb1
            // 
            this.tb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb1.Location = new System.Drawing.Point(1, 24);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(281, 20);
            this.tb1.TabIndex = 1;
            // 
            // HLBInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb1);
            this.Controls.Add(this.lbl);
            this.Name = "HLBInputBox";
            this.Size = new System.Drawing.Size(282, 45);
            this.Load += new System.EventHandler(this.HLBInputBox_Load);
            this.Resize += new System.EventHandler(this.HLBInputBox_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.TextBox tb1;
    }
}
