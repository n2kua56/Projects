namespace System.Windows.Forms.Calendar
{
    partial class frmEvent
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromTime = new System.Windows.Forms.DateTimePicker();
            this.tbEventName = new System.Windows.Forms.TextBox();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToTime = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.cbAllDay = new System.Windows.Forms.CheckBox();
            this.cmbRepeat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbWho = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnWhoAdd = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbReminder1 = new System.Windows.Forms.ComboBox();
            this.cmbReminder1Type = new System.Windows.Forms.ComboBox();
            this.btnReminder1Del = new System.Windows.Forms.Button();
            this.btnReminderAdd = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Event name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "From:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(89, 67);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(88, 20);
            this.dtpFromDate.TabIndex = 3;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpFromTime.Location = new System.Drawing.Point(183, 67);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.Size = new System.Drawing.Size(86, 20);
            this.dtpFromTime.TabIndex = 4;
            // 
            // tbEventName
            // 
            this.tbEventName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEventName.Location = new System.Drawing.Point(89, 15);
            this.tbEventName.Name = "tbEventName";
            this.tbEventName.Size = new System.Drawing.Size(219, 20);
            this.tbEventName.TabIndex = 5;
            this.tbEventName.TextChanged += new System.EventHandler(this.tbEventName_TextChanged);
            // 
            // tbLocation
            // 
            this.tbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLocation.Location = new System.Drawing.Point(89, 41);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(219, 20);
            this.tbLocation.TabIndex = 6;
            this.tbLocation.TextChanged += new System.EventHandler(this.tbLocation_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "To:";
            // 
            // dtpToTime
            // 
            this.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpToTime.Location = new System.Drawing.Point(183, 93);
            this.dtpToTime.Name = "dtpToTime";
            this.dtpToTime.Size = new System.Drawing.Size(86, 20);
            this.dtpToTime.TabIndex = 9;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(89, 93);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(88, 20);
            this.dtpToDate.TabIndex = 8;
            // 
            // cbAllDay
            // 
            this.cbAllDay.AutoSize = true;
            this.cbAllDay.Location = new System.Drawing.Point(89, 119);
            this.cbAllDay.Name = "cbAllDay";
            this.cbAllDay.Size = new System.Drawing.Size(57, 17);
            this.cbAllDay.TabIndex = 10;
            this.cbAllDay.Text = "All day";
            this.cbAllDay.UseVisualStyleBackColor = true;
            this.cbAllDay.CheckedChanged += new System.EventHandler(this.cbAllDay_CheckedChanged);
            // 
            // cmbRepeat
            // 
            this.cmbRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRepeat.Enabled = false;
            this.cmbRepeat.FormattingEnabled = true;
            this.cmbRepeat.Items.AddRange(new object[] {
            "None",
            "Daily",
            "Every weekday (Mon-Fri)",
            "Every 2 weeks ({dayname})",
            "Monthly (on day {daynum})",
            "Yearly (on {monthname} {daynum})"});
            this.cmbRepeat.Location = new System.Drawing.Point(89, 142);
            this.cmbRepeat.Name = "cmbRepeat";
            this.cmbRepeat.Size = new System.Drawing.Size(219, 21);
            this.cmbRepeat.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Repeat:";
            // 
            // tbWho
            // 
            this.tbWho.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbWho.Enabled = false;
            this.tbWho.Location = new System.Drawing.Point(89, 169);
            this.tbWho.Name = "tbWho";
            this.tbWho.Size = new System.Drawing.Size(180, 20);
            this.tbWho.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Who:";
            // 
            // btnWhoAdd
            // 
            this.btnWhoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWhoAdd.Enabled = false;
            this.btnWhoAdd.Location = new System.Drawing.Point(270, 169);
            this.btnWhoAdd.Name = "btnWhoAdd";
            this.btnWhoAdd.Size = new System.Drawing.Size(39, 20);
            this.btnWhoAdd.TabIndex = 15;
            this.btnWhoAdd.Text = "Add";
            this.btnWhoAdd.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Reminder:";
            // 
            // cmbReminder1
            // 
            this.cmbReminder1.Enabled = false;
            this.cmbReminder1.FormattingEnabled = true;
            this.cmbReminder1.Items.AddRange(new object[] {
            "On time",
            "1 min before",
            "5 min before",
            "10 min before",
            "15 min before",
            "20 min before",
            "25 min before",
            "30 min before",
            "45 min before",
            "1 hour before",
            "2 hour before",
            "3 hour before",
            "12 hour before",
            "1 day before",
            "2 days before",
            "1 weel before"});
            this.cmbReminder1.Location = new System.Drawing.Point(89, 195);
            this.cmbReminder1.Name = "cmbReminder1";
            this.cmbReminder1.Size = new System.Drawing.Size(103, 21);
            this.cmbReminder1.TabIndex = 17;
            // 
            // cmbReminder1Type
            // 
            this.cmbReminder1Type.Enabled = false;
            this.cmbReminder1Type.FormattingEnabled = true;
            this.cmbReminder1Type.Items.AddRange(new object[] {
            "Notification",
            "Email"});
            this.cmbReminder1Type.Location = new System.Drawing.Point(198, 195);
            this.cmbReminder1Type.Name = "cmbReminder1Type";
            this.cmbReminder1Type.Size = new System.Drawing.Size(71, 21);
            this.cmbReminder1Type.TabIndex = 18;
            // 
            // btnReminder1Del
            // 
            this.btnReminder1Del.Enabled = false;
            this.btnReminder1Del.Location = new System.Drawing.Point(270, 196);
            this.btnReminder1Del.Name = "btnReminder1Del";
            this.btnReminder1Del.Size = new System.Drawing.Size(39, 20);
            this.btnReminder1Del.TabIndex = 19;
            this.btnReminder1Del.Text = "Del";
            this.btnReminder1Del.UseVisualStyleBackColor = true;
            // 
            // btnReminderAdd
            // 
            this.btnReminderAdd.Enabled = false;
            this.btnReminderAdd.Location = new System.Drawing.Point(89, 222);
            this.btnReminderAdd.Name = "btnReminderAdd";
            this.btnReminderAdd.Size = new System.Drawing.Size(88, 20);
            this.btnReminderAdd.TabIndex = 20;
            this.btnReminderAdd.Text = "Add Reminder";
            this.btnReminderAdd.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(146, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(234, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(330, 285);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnReminderAdd);
            this.Controls.Add(this.btnReminder1Del);
            this.Controls.Add(this.cmbReminder1Type);
            this.Controls.Add(this.cmbReminder1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnWhoAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbWho);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbRepeat);
            this.Controls.Add(this.cbAllDay);
            this.Controls.Add(this.dtpToTime);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbLocation);
            this.Controls.Add(this.tbEventName);
            this.Controls.Add(this.dtpFromTime);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmEvent";
            this.Text = "frmEvent";
            this.Load += new System.EventHandler(this.frmEvent_Load);
            this.Resize += new System.EventHandler(this.frmEvent_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpFromTime;
        private TextBox tbEventName;
        private TextBox tbLocation;
        private Label label4;
        private DateTimePicker dtpToTime;
        private DateTimePicker dtpToDate;
        private CheckBox cbAllDay;
        private ComboBox cmbRepeat;
        private Label label5;
        private TextBox tbWho;
        private Label label6;
        private Button btnWhoAdd;
        private Label label7;
        private ComboBox cmbReminder1;
        private ComboBox cmbReminder1Type;
        private Button btnReminder1Del;
        private Button btnReminderAdd;
        private Button btnOK;
        private Button btnCancel;
    }
}