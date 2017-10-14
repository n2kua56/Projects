namespace EZDesk
{
    partial class frmTestNewCalendar
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
            this.rbDayView = new System.Windows.Forms.RadioButton();
            this.rbMonthView = new System.Windows.Forms.RadioButton();
            this.gbCalendarView = new System.Windows.Forms.GroupBox();
            this.gbShowDateInHeader = new System.Windows.Forms.GroupBox();
            this.rbDontShowDate = new System.Windows.Forms.RadioButton();
            this.rbShowTheDate = new System.Windows.Forms.RadioButton();
            this.gbShowTodayButton = new System.Windows.Forms.GroupBox();
            this.rbDontShowTodayButton = new System.Windows.Forms.RadioButton();
            this.rbShowTodayButton = new System.Windows.Forms.RadioButton();
            this.gbShowArrowControls = new System.Windows.Forms.GroupBox();
            this.rbDontShowArrowControls = new System.Windows.Forms.RadioButton();
            this.rbShowArrowButtons = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpCurrentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDateHeaderFont = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.ezCalendar1 = new Calendar.NET.ezCalendar();
            this.btnDayViewTimeFont = new System.Windows.Forms.Button();
            this.btnDayOfWeekFont = new System.Windows.Forms.Button();
            this.btnTodayFont = new System.Windows.Forms.Button();
            this.btnDaysFont = new System.Windows.Forms.Button();
            this.gbCalendarView.SuspendLayout();
            this.gbShowDateInHeader.SuspendLayout();
            this.gbShowTodayButton.SuspendLayout();
            this.gbShowArrowControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbDayView
            // 
            this.rbDayView.AutoSize = true;
            this.rbDayView.Location = new System.Drawing.Point(20, 19);
            this.rbDayView.Name = "rbDayView";
            this.rbDayView.Size = new System.Drawing.Size(44, 17);
            this.rbDayView.TabIndex = 1;
            this.rbDayView.Text = "Day";
            this.rbDayView.UseVisualStyleBackColor = true;
            this.rbDayView.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rbMonthView
            // 
            this.rbMonthView.AutoSize = true;
            this.rbMonthView.Checked = true;
            this.rbMonthView.Location = new System.Drawing.Point(20, 42);
            this.rbMonthView.Name = "rbMonthView";
            this.rbMonthView.Size = new System.Drawing.Size(55, 17);
            this.rbMonthView.TabIndex = 2;
            this.rbMonthView.TabStop = true;
            this.rbMonthView.Text = "Month";
            this.rbMonthView.UseVisualStyleBackColor = true;
            this.rbMonthView.Click += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // gbCalendarView
            // 
            this.gbCalendarView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalendarView.Controls.Add(this.rbDayView);
            this.gbCalendarView.Controls.Add(this.rbMonthView);
            this.gbCalendarView.Location = new System.Drawing.Point(564, 59);
            this.gbCalendarView.Name = "gbCalendarView";
            this.gbCalendarView.Size = new System.Drawing.Size(142, 64);
            this.gbCalendarView.TabIndex = 3;
            this.gbCalendarView.TabStop = false;
            this.gbCalendarView.Text = "CalendarView";
            // 
            // gbShowDateInHeader
            // 
            this.gbShowDateInHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShowDateInHeader.Controls.Add(this.rbDontShowDate);
            this.gbShowDateInHeader.Controls.Add(this.rbShowTheDate);
            this.gbShowDateInHeader.Location = new System.Drawing.Point(564, 133);
            this.gbShowDateInHeader.Name = "gbShowDateInHeader";
            this.gbShowDateInHeader.Size = new System.Drawing.Size(142, 69);
            this.gbShowDateInHeader.TabIndex = 4;
            this.gbShowDateInHeader.TabStop = false;
            this.gbShowDateInHeader.Text = "ShowDateInHeader";
            // 
            // rbDontShowDate
            // 
            this.rbDontShowDate.AutoSize = true;
            this.rbDontShowDate.Location = new System.Drawing.Point(20, 42);
            this.rbDontShowDate.Name = "rbDontShowDate";
            this.rbDontShowDate.Size = new System.Drawing.Size(102, 17);
            this.rbDontShowDate.TabIndex = 1;
            this.rbDontShowDate.Text = "Don\'t show date";
            this.rbDontShowDate.UseVisualStyleBackColor = true;
            this.rbDontShowDate.Click += new System.EventHandler(this.radioButton3_Click);
            // 
            // rbShowTheDate
            // 
            this.rbShowTheDate.AutoSize = true;
            this.rbShowTheDate.Checked = true;
            this.rbShowTheDate.Location = new System.Drawing.Point(20, 19);
            this.rbShowTheDate.Name = "rbShowTheDate";
            this.rbShowTheDate.Size = new System.Drawing.Size(94, 17);
            this.rbShowTheDate.TabIndex = 0;
            this.rbShowTheDate.TabStop = true;
            this.rbShowTheDate.Text = "Show the date";
            this.rbShowTheDate.UseVisualStyleBackColor = true;
            this.rbShowTheDate.Click += new System.EventHandler(this.radioButton3_Click);
            // 
            // gbShowTodayButton
            // 
            this.gbShowTodayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShowTodayButton.Controls.Add(this.rbDontShowTodayButton);
            this.gbShowTodayButton.Controls.Add(this.rbShowTodayButton);
            this.gbShowTodayButton.Location = new System.Drawing.Point(564, 208);
            this.gbShowTodayButton.Name = "gbShowTodayButton";
            this.gbShowTodayButton.Size = new System.Drawing.Size(142, 69);
            this.gbShowTodayButton.TabIndex = 5;
            this.gbShowTodayButton.TabStop = false;
            this.gbShowTodayButton.Text = "ShowTodayButton";
            // 
            // rbDontShowTodayButton
            // 
            this.rbDontShowTodayButton.AutoSize = true;
            this.rbDontShowTodayButton.Location = new System.Drawing.Point(20, 42);
            this.rbDontShowTodayButton.Name = "rbDontShowTodayButton";
            this.rbDontShowTodayButton.Size = new System.Drawing.Size(78, 17);
            this.rbDontShowTodayButton.TabIndex = 1;
            this.rbDontShowTodayButton.Text = "Don\'t show";
            this.rbDontShowTodayButton.UseVisualStyleBackColor = true;
            this.rbDontShowTodayButton.Click += new System.EventHandler(this.radioButton5_Click);
            // 
            // rbShowTodayButton
            // 
            this.rbShowTodayButton.AutoSize = true;
            this.rbShowTodayButton.Checked = true;
            this.rbShowTodayButton.Location = new System.Drawing.Point(20, 19);
            this.rbShowTodayButton.Name = "rbShowTodayButton";
            this.rbShowTodayButton.Size = new System.Drawing.Size(119, 17);
            this.rbShowTodayButton.TabIndex = 0;
            this.rbShowTodayButton.TabStop = true;
            this.rbShowTodayButton.Text = "Show Today Button";
            this.rbShowTodayButton.UseVisualStyleBackColor = true;
            this.rbShowTodayButton.Click += new System.EventHandler(this.radioButton5_Click);
            // 
            // gbShowArrowControls
            // 
            this.gbShowArrowControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShowArrowControls.Controls.Add(this.rbDontShowArrowControls);
            this.gbShowArrowControls.Controls.Add(this.rbShowArrowButtons);
            this.gbShowArrowControls.Location = new System.Drawing.Point(564, 283);
            this.gbShowArrowControls.Name = "gbShowArrowControls";
            this.gbShowArrowControls.Size = new System.Drawing.Size(142, 71);
            this.gbShowArrowControls.TabIndex = 6;
            this.gbShowArrowControls.TabStop = false;
            this.gbShowArrowControls.Text = "ShowArrowControls";
            // 
            // rbDontShowArrowControls
            // 
            this.rbDontShowArrowControls.AutoSize = true;
            this.rbDontShowArrowControls.Location = new System.Drawing.Point(20, 42);
            this.rbDontShowArrowControls.Name = "rbDontShowArrowControls";
            this.rbDontShowArrowControls.Size = new System.Drawing.Size(80, 17);
            this.rbDontShowArrowControls.TabIndex = 1;
            this.rbDontShowArrowControls.Text = "Don\'t Show";
            this.rbDontShowArrowControls.UseVisualStyleBackColor = true;
            this.rbDontShowArrowControls.Click += new System.EventHandler(this.radioButton7_Click);
            // 
            // rbShowArrowButtons
            // 
            this.rbShowArrowButtons.AutoSize = true;
            this.rbShowArrowButtons.Checked = true;
            this.rbShowArrowButtons.Location = new System.Drawing.Point(20, 19);
            this.rbShowArrowButtons.Name = "rbShowArrowButtons";
            this.rbShowArrowButtons.Size = new System.Drawing.Size(91, 17);
            this.rbShowArrowButtons.TabIndex = 0;
            this.rbShowArrowButtons.TabStop = true;
            this.rbShowArrowButtons.Text = "Show Buttons";
            this.rbShowArrowButtons.UseVisualStyleBackColor = true;
            this.rbShowArrowButtons.Click += new System.EventHandler(this.radioButton7_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(566, 357);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Current date:";
            // 
            // dtpCurrentDate
            // 
            this.dtpCurrentDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpCurrentDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCurrentDate.Location = new System.Drawing.Point(569, 373);
            this.dtpCurrentDate.Name = "dtpCurrentDate";
            this.dtpCurrentDate.Size = new System.Drawing.Size(137, 20);
            this.dtpCurrentDate.TabIndex = 8;
            this.dtpCurrentDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnDateHeaderFont
            // 
            this.btnDateHeaderFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDateHeaderFont.Location = new System.Drawing.Point(12, 458);
            this.btnDateHeaderFont.Name = "btnDateHeaderFont";
            this.btnDateHeaderFont.Size = new System.Drawing.Size(100, 23);
            this.btnDateHeaderFont.TabIndex = 9;
            this.btnDateHeaderFont.Text = "DateHeaderFont";
            this.btnDateHeaderFont.UseVisualStyleBackColor = true;
            this.btnDateHeaderFont.Click += new System.EventHandler(this.btnDateHeaderFont_Click);
            // 
            // ezCalendar1
            // 
            this.ezCalendar1.AllowEditingEvents = true;
            this.ezCalendar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ezCalendar1.CalendarDate = new System.DateTime(2014, 9, 27, 11, 0, 35, 720);
            this.ezCalendar1.CalendarView = Calendar.NET.myCalendarViews.Month;
            this.ezCalendar1.DateHeaderFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.ezCalendar1.DayOfWeekFont = new System.Drawing.Font("Arial", 10F);
            this.ezCalendar1.DaysFont = new System.Drawing.Font("Arial", 10F);
            this.ezCalendar1.DayViewTimeFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.ezCalendar1.DimDisabledEvents = true;
            this.ezCalendar1.HighlightCurrentDay = true;
            this.ezCalendar1.LoadPresetHolidays = false;
            this.ezCalendar1.Location = new System.Drawing.Point(12, 12);
            this.ezCalendar1.Name = "ezCalendar1";
            this.ezCalendar1.ShowArrowControls = true;
            this.ezCalendar1.ShowDashedBorderOnDisabledEvents = true;
            this.ezCalendar1.ShowDateInHeader = true;
            this.ezCalendar1.ShowDisabledEvents = false;
            this.ezCalendar1.ShowEventTooltips = true;
            this.ezCalendar1.ShowTodayButton = true;
            this.ezCalendar1.Size = new System.Drawing.Size(548, 432);
            this.ezCalendar1.TabIndex = 0;
            this.ezCalendar1.TodayFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            // 
            // btnDayViewTimeFont
            // 
            this.btnDayViewTimeFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDayViewTimeFont.Location = new System.Drawing.Point(124, 458);
            this.btnDayViewTimeFont.Name = "btnDayViewTimeFont";
            this.btnDayViewTimeFont.Size = new System.Drawing.Size(100, 23);
            this.btnDayViewTimeFont.TabIndex = 10;
            this.btnDayViewTimeFont.Text = "DayViewTimeFont";
            this.btnDayViewTimeFont.UseVisualStyleBackColor = true;
            this.btnDayViewTimeFont.Click += new System.EventHandler(this.btnDayViewTimeFont_Click);
            // 
            // btnDayOfWeekFont
            // 
            this.btnDayOfWeekFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDayOfWeekFont.Location = new System.Drawing.Point(236, 458);
            this.btnDayOfWeekFont.Name = "btnDayOfWeekFont";
            this.btnDayOfWeekFont.Size = new System.Drawing.Size(100, 23);
            this.btnDayOfWeekFont.TabIndex = 11;
            this.btnDayOfWeekFont.Text = "DayOfWeekFont";
            this.btnDayOfWeekFont.UseVisualStyleBackColor = true;
            this.btnDayOfWeekFont.Click += new System.EventHandler(this.btnDayOfWeekFont_Click);
            // 
            // btnTodayFont
            // 
            this.btnTodayFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTodayFont.Location = new System.Drawing.Point(348, 458);
            this.btnTodayFont.Name = "btnTodayFont";
            this.btnTodayFont.Size = new System.Drawing.Size(100, 23);
            this.btnTodayFont.TabIndex = 12;
            this.btnTodayFont.Text = "TodayFont";
            this.btnTodayFont.UseVisualStyleBackColor = true;
            this.btnTodayFont.Click += new System.EventHandler(this.btnTodayFont_Click);
            // 
            // btnDaysFont
            // 
            this.btnDaysFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDaysFont.Location = new System.Drawing.Point(460, 458);
            this.btnDaysFont.Name = "btnDaysFont";
            this.btnDaysFont.Size = new System.Drawing.Size(100, 23);
            this.btnDaysFont.TabIndex = 13;
            this.btnDaysFont.Text = "DaysFont";
            this.btnDaysFont.UseVisualStyleBackColor = true;
            this.btnDaysFont.Click += new System.EventHandler(this.btnDaysFont_Click);
            // 
            // frmTestNewCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 493);
            this.Controls.Add(this.btnDaysFont);
            this.Controls.Add(this.btnTodayFont);
            this.Controls.Add(this.btnDayOfWeekFont);
            this.Controls.Add(this.btnDayViewTimeFont);
            this.Controls.Add(this.btnDateHeaderFont);
            this.Controls.Add(this.dtpCurrentDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbShowArrowControls);
            this.Controls.Add(this.gbShowTodayButton);
            this.Controls.Add(this.gbShowDateInHeader);
            this.Controls.Add(this.gbCalendarView);
            this.Controls.Add(this.ezCalendar1);
            this.Name = "frmTestNewCalendar";
            this.Text = "Test ezCalendar";
            this.Load += new System.EventHandler(this.frmTestNewCalendar_Load);
            this.gbCalendarView.ResumeLayout(false);
            this.gbCalendarView.PerformLayout();
            this.gbShowDateInHeader.ResumeLayout(false);
            this.gbShowDateInHeader.PerformLayout();
            this.gbShowTodayButton.ResumeLayout(false);
            this.gbShowTodayButton.PerformLayout();
            this.gbShowArrowControls.ResumeLayout(false);
            this.gbShowArrowControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Calendar.NET.ezCalendar ezCalendar1;
        private System.Windows.Forms.RadioButton rbDayView;
        private System.Windows.Forms.RadioButton rbMonthView;
        private System.Windows.Forms.GroupBox gbCalendarView;
        private System.Windows.Forms.GroupBox gbShowDateInHeader;
        private System.Windows.Forms.RadioButton rbDontShowDate;
        private System.Windows.Forms.RadioButton rbShowTheDate;
        private System.Windows.Forms.GroupBox gbShowTodayButton;
        private System.Windows.Forms.RadioButton rbDontShowTodayButton;
        private System.Windows.Forms.RadioButton rbShowTodayButton;
        private System.Windows.Forms.GroupBox gbShowArrowControls;
        private System.Windows.Forms.RadioButton rbDontShowArrowControls;
        private System.Windows.Forms.RadioButton rbShowArrowButtons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpCurrentDate;
        private System.Windows.Forms.Button btnDateHeaderFont;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button btnDayViewTimeFont;
        private System.Windows.Forms.Button btnDayOfWeekFont;
        private System.Windows.Forms.Button btnTodayFont;
        private System.Windows.Forms.Button btnDaysFont;
    }
}