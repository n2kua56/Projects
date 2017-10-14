namespace ToDo
{
    partial class frmToDoSetDate
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.cmdClearDate = new System.Windows.Forms.Button();
            this.cmdSetDate = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(69, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(93, 20);
            this.dtpDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Time:";
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(69, 68);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(93, 20);
            this.dtpTime.TabIndex = 3;
            // 
            // cmdClearDate
            // 
            this.cmdClearDate.Location = new System.Drawing.Point(183, 27);
            this.cmdClearDate.Name = "cmdClearDate";
            this.cmdClearDate.Size = new System.Drawing.Size(75, 23);
            this.cmdClearDate.TabIndex = 4;
            this.cmdClearDate.Text = "Clear Date";
            this.cmdClearDate.UseVisualStyleBackColor = true;
            this.cmdClearDate.Click += new System.EventHandler(this.cmdClearDate_Click);
            // 
            // cmdSetDate
            // 
            this.cmdSetDate.Location = new System.Drawing.Point(183, 68);
            this.cmdSetDate.Name = "cmdSetDate";
            this.cmdSetDate.Size = new System.Drawing.Size(75, 23);
            this.cmdSetDate.TabIndex = 5;
            this.cmdSetDate.Text = "Set Date";
            this.cmdSetDate.UseVisualStyleBackColor = true;
            this.cmdSetDate.Click += new System.EventHandler(this.cmdSetDate_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(183, 109);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // frmToDoSetDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSetDate);
            this.Controls.Add(this.cmdClearDate);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Name = "frmToDoSetDate";
            this.Text = "Set Task Expected Completion";
            this.Load += new System.EventHandler(this.frmToDoSetDate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Button cmdClearDate;
        private System.Windows.Forms.Button cmdSetDate;
        private System.Windows.Forms.Button cmdCancel;
    }
}