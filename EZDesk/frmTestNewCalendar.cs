using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZDesk
{
    public partial class frmTestNewCalendar : Form
    {
        public frmTestNewCalendar()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDayView.Checked)
            {
                ezCalendar1.CalendarView = Calendar.NET.myCalendarViews.Day;
            }
            if (rbMonthView.Checked)
            {
                ezCalendar1.CalendarView = Calendar.NET.myCalendarViews.Month;
            }
        }

        /// <summary>
        /// CHange the "Show Date".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton3_Click(object sender, EventArgs e)
        {
            ezCalendar1.ShowDateInHeader = rbShowTheDate.Checked;
        }

        /// <summary>
        /// Change the ShowTodayButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton5_Click(object sender, EventArgs e)
        {
            ezCalendar1.ShowTodayButton = rbShowTodayButton.Checked;
        }

        /// <summary>
        /// Change the ShowArrowControls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton7_Click(object sender, EventArgs e)
        {
            ezCalendar1.ShowArrowControls = rbShowArrowButtons.Checked;
        }

        private void frmTestNewCalendar_Load(object sender, EventArgs e)
        {
            dtpCurrentDate.Value = DateTime.Now;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            ezCalendar1.CalendarDate = dtpCurrentDate.Value;
        }

        private void btnDateHeaderFont_Click(object sender, EventArgs e)
        {
            Font f = ezCalendar1.DateHeaderFont;
            fontDialog1.Font = f;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ezCalendar1.DateHeaderFont = fontDialog1.Font; 
            }
        }

        private void btnDayViewTimeFont_Click(object sender, EventArgs e)
        {
            Font f = ezCalendar1.DayViewTimeFont;
            fontDialog1.Font = f;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ezCalendar1.DayViewTimeFont = fontDialog1.Font;
            }
        }

        private void btnDayOfWeekFont_Click(object sender, EventArgs e)
        {
            Font f = ezCalendar1.DayOfWeekFont;
            fontDialog1.Font = f;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ezCalendar1.DayOfWeekFont = fontDialog1.Font;
            }
        }

        private void btnTodayFont_Click(object sender, EventArgs e)
        {
            Font f = ezCalendar1.TodayFont;
            fontDialog1.Font = f;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ezCalendar1.TodayFont = fontDialog1.Font;
            }
        }

        private void btnDaysFont_Click(object sender, EventArgs e)
        {
            Font f = ezCalendar1.DaysFont;
            fontDialog1.Font = f;
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                ezCalendar1.DaysFont = fontDialog1.Font;
            }
        }

    }
}
