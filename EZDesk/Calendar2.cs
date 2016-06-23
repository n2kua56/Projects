using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;
using System.Xml.Serialization;
using System.IO;
using EZDeskDataLayer;

namespace EZDesk
{
    /// <summary>
    /// Stolen from:
    ///   http://www.codeproject.com/Articles/38699/A-Professional-Calendar-Agenda-View-That-You-Will
    /// </summary>
    public partial class Calendar2 : Form
    {
        List<CalendarItem> _items = new List<CalendarItem>();
        CalendarItem contextItem = null;
        DateTime mDayFound = DateTime.MinValue;
        private EZDeskCommon mCommon = null;

        public FileInfo ItemsFile
        {
            get
            {
                return new FileInfo(Path.Combine(Application.StartupPath, "items.xml"));
            }
        }

        public Calendar2(EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            //Monthview colors
            monthView1.MonthTitleColor = monthView1.MonthTitleColorInactive = CalendarColorTable.FromHex("#C2DAFC");
            monthView1.ArrowsColor = CalendarColorTable.FromHex("#77A1D3");
            monthView1.DaySelectedBackgroundColor = CalendarColorTable.FromHex("#F4CC52");
            monthView1.DaySelectedTextColor = monthView1.ForeColor;
        }

        private void Calendar2_Load(object sender, EventArgs e)
        {
            if (ItemsFile.Exists)
            {
                List<ItemInfo> lst = new List<ItemInfo>();

                XmlSerializer xml = new XmlSerializer(lst.GetType());

                using (Stream s = ItemsFile.OpenRead())
                {
                    lst = xml.Deserialize(s) as List<ItemInfo>;
                }

                foreach (ItemInfo item in lst)
                {
                    CalendarItem cal = new CalendarItem(calendar1, item.StartTime, item.EndTime, item.Text);

                    if (!(item.R == 0 && item.G == 0 && item.B == 0))
                    {
                        cal.ApplyColor(Color.FromArgb(item.A, item.R, item.G, item.B));
                    }

                    _items.Add(cal);
                }

                PlaceItems();
            }

            //TODO: Trying to adjust the "view" to position today and time visible.
            //calendar1.ViewEnd = DateTime.Now.AddDays(2);
            int off = calendar1.TimeUnitsOffset;
            CalendarTimeScale tscale = calendar1.TimeScale;
            calendar1.VerticalScroll.Enabled = true;
            //CalendarTimeScaleUnit unit = new CalendarTimeScaleUnit();

            calendar1.TimeUnitsOffset = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar1_LoadItems(object sender, System.Windows.Forms.Calendar.CalendarLoadEventArgs e)
        {
            PlaceItems();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PlaceItems()
        {
            foreach (CalendarItem item in _items)
            {
                if (calendar1.ViewIntersects(item))
                {
                    calendar1.Items.Add(item);
                }
            }
        }

        private void calendar1_ItemCreated(object sender, CalendarItemCancelEventArgs e)
        {
            _items.Add(e.Item);
        }

        private void calendar1_ItemMouseHover(object sender, CalendarItemEventArgs e)
        {
            Text = e.Item.Text;
        }

        private void calendar1_ItemClick(object sender, CalendarItemEventArgs e)
        {
            //MessageBox.Show(e.Item.Text);
        }

        private void Calendar2_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<ItemInfo> lst = new List<ItemInfo>();

            foreach (CalendarItem item in _items)
            {
                lst.Add(new ItemInfo(item.StartDate, item.EndDate, item.Text, item.BackgroundColor));
            }

            XmlSerializer xmls = new XmlSerializer(lst.GetType());

            if (ItemsFile.Exists)
            {
                ItemsFile.Delete();
            }

            using (Stream s = ItemsFile.OpenWrite())
            {
                xmls.Serialize(s, lst);
                s.Close();
            }
        }

        private void calendar1_ItemDoubleClick(object sender, CalendarItemEventArgs e)
        {
            DateTime eventDate = e.Item.Date;
            int i = e.Item.MinuteStartTop;          //If 0 then this is a day event.
            bool selected = e.Item.Selected;
            DateTime startDate = e.Item.StartDate;

            zAddEditEvent(startDate, startDate, "", "", false);
            MessageBox.Show("Double click: " + e.Item.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="allDay"></param>
        private void zAddEditEvent(DateTime start, DateTime end, string name, string loc,
                                   bool allDay)
        {
            try
            {
                this.Enabled = false;
                frmEvent frm = new frmEvent(start, end, name, loc, allDay);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string eventName = frm.EventName;
                    string eventLocation = frm.EventLocation;
                    DateTime eventStart = frm.StartDateTime;
                    DateTime eventEnd = frm.EndDateTime;
                    bool eventAllDay = frm.EventAllDay;
                    //TODO: Add the event.
                    
                }
            }

            catch (Exception ex)
            {
            }

            finally
            {
                this.Enabled = true;
            }
        }

        private void calendar1_ItemDeleted(object sender, CalendarItemEventArgs e)
        {
            _items.Remove(e.Item);
        }

        private void calendar1_DayHeaderClick(object sender, CalendarDayEventArgs e)
        {
            calendar1.SetViewRange(e.CalendarDay.Date, e.CalendarDay.Date);
        }

        private void monthView1_SelectionChanged(object sender, EventArgs e)
        {
            calendar1.SetViewRange(monthView1.SelectionStart, monthView1.SelectionEnd);
        }

        //The context menu is opening up
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            contextItem = calendar1.ItemAt(contextMenuStrip1.Bounds.Location);
            CalendarDaysMode daysmode = calendar1.DaysMode;
            DateTime start = calendar1.SelectionStart;
            DateTime end = calendar1.SelectionEnd;
            DateTime day = calendar1.Days[0].Date;
            Rectangle r = calendar1.Days[0].Bounds;
            int X = contextMenuStrip1.Left;
            int x1 = calendar1.Left;
        }

        /// <summary>
        /// The user is changing the Calendar view to 1 hour slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.SixtyMinutes;
        }

        /// <summary>
        /// The user is changing the Calendar view to 30 minute slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.ThirtyMinutes;
        }

        /// <summary>
        /// The user is changing the Calendar view to 15 minute slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.FifteenMinutes;
        }

        /// <summary>
        /// The user is changing the Calendar view to 10 minute slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.TenMinutes;
        }

        /// <summary>
        /// The user is changing the Calenar view to 6 (1/10 an hour) slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.SixMinutes;
        }

        /// <summary>
        /// The user is changing the Calendar view to 5 minute slots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minutesToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            calendar1.TimeScale = CalendarTimeScale.FiveMinutes;
        }

        /// <summary>
        /// The user is going into day view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mDayFound > DateTime.MinValue)
            {
                calendar1.SetViewRange(mDayFound.Date, mDayFound.Date);
            }
        }

        private void weekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mDayFound > DateTime.MinValue)
            {
                DayOfWeek dow = mDayFound.DayOfWeek;
                int daysBack = (int)dow;
                DateTime start = mDayFound.AddDays(-1 * daysBack);
                DateTime end = start.AddDays(6);
                calendar1.SetViewRange(start.Date, end.Date);
            }
        }

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mDayFound > DateTime.MinValue)
            {
                int daysBack = mDayFound.Day - 1;
                DateTime start = mDayFound.AddDays(-1 * daysBack);
                DateTime end = start.AddDays(DateTime.DaysInMonth(mDayFound.Year, mDayFound.Month) - 1);
                calendar1.SetViewRange(start.Date, end.Date);
            }
        }

        /// <summary>
        /// The mouse button has been pressed. In case we are bringing up the 
        /// context menu it would be nice to know what day it is in.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int x = e.X;
                int y = e.Y;

                mDayFound = DateTime.MinValue;
                foreach (CalendarDay cd in calendar1.Days)
                {
                    if ((x >= cd.Bounds.Left) && (x <= cd.Bounds.Right) &&
                        (y >= cd.Bounds.Top) && (y <= cd.Bounds.Bottom))
                    {
                        mDayFound = cd.Date;
                    }
                }
                if (mDayFound == DateTime.MinValue)
                {
                    x = x;
                }
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.SplitterDistance > 223) { splitContainer1.SplitterDistance = 223; }
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Adding an appointment
            ICalendarSelectableElement selectionEnd = calendar1.SelectedElementEnd;
            ICalendarSelectableElement selectionStart = calendar1.SelectedElementStart;
            DateTime selEnd = calendar1.SelectionEnd;
            DateTime selStart = calendar1.SelectionStart;
            //calendar1.ActivateEditMode();
            //calendar1.CreateItemOnSelection();
            zAddEditEvent(selectionStart.Date, selectionEnd.Date, "", "", false);
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Delete an appointment
        }

    }
}
