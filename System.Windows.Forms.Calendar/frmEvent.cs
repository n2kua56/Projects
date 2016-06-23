using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using EZDeskDataLayer;

namespace System.Windows.Forms.Calendar
{
    public partial class frmEvent : Form
    {
        private int minHeight = 0;
        private int minWidth = 0;
        private string mModName = "frmEvent";

        /// <summary>
        /// Gets or Sets the event Start Date Time
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the event End Date Time
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Gets or Sets the event name
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or Sets the event location
        /// </summary>
        public string EventLocation { get; set; }

        /// <summary>
        /// Gets or Sets the All Day switch
        /// </summary>
        public bool EventAllDay { get; set; }

        /// <summary>
        /// Displays the event Add/Edit form.
        /// </summary>
        /// <param name="start">Date and Time of the start of the event.</param>
        /// <param name="end">Date and Time of the end of the event.</param>
        /// <param name="name">Name of the event.</param>
        /// <param name="location">Location of the event.</param>
        /// <param name="allDay">true if this is an all day event.</param>
        public frmEvent(DateTime start, DateTime end, string name, string location,
                                   bool allDay)
        {
            InitializeComponent();
            StartDateTime = start;
            EndDateTime = end;
            EventName = name;
            EventLocation = location;
            EventAllDay = allDay;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEvent_Load(object sender, EventArgs e)
        {
            minHeight = this.Height;
            minWidth = this.Width;

            Trace.Enter(Trace.RtnName(mModName, "frmEvent_Load"));

            tbEventName.Text = EventName;
            tbLocation.Text = EventLocation;
            if (StartDateTime != DateTime.MinValue)
            {
                dtpFromDate.Value = StartDateTime;
                dtpFromTime.Value = StartDateTime;
            }
            if (EndDateTime != DateTime.MinValue)
            {
                dtpToDate.Value = StartDateTime;
                dtpToDate.Value = StartDateTime;
            }
            cbAllDay.Checked = EventAllDay;
        }

        /// <summary>
        /// Can't let the for get too small.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEvent_Resize(object sender, EventArgs e)
        {
            if (minHeight > this.Height) { this.Height = minHeight; }
            if (minWidth > this.Width) { this.Width = minWidth; }
        }

        /// <summary>
        /// The user is happy with the event, return to the calling
        /// program to save the event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cbAllDay_CheckedChanged(object sender, EventArgs e)
        {
            EventAllDay = cbAllDay.Checked;
        }

        private void tbEventName_TextChanged(object sender, EventArgs e)
        {
            EventName = tbEventName.Text;
        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {
            EventLocation = tbLocation.Text;
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            StartDateTime = dtpFromDate.Value.Date + dtpFromTime.Value.TimeOfDay;
        }

    }
}
