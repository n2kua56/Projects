using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Calendar.NET;

namespace Calendar.NET
{
    /// <summary>
    /// An enumeration describing various ways to view the calendar
    /// </summary>
    public enum myCalendarViews
    {
        /// <summary>
        /// Renders the Calendar in a month view
        /// </summary>
        Month = 1,
        /// <summary>
        /// Renders the Calendar in a day view
        /// </summary>
        Day = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class ezCalendar : UserControl
    {
        private DateTime mCalendarDate;
        private Font mDayOfWeekFont;
        private Font mDaysFont;
        private Font mTodayFont;
        private Font mDateHeaderFont;
        private Font mDayViewTimeFont;
        private bool mShowArrowControls = true;
        private bool mShowTodayButton;
        private bool mShowDateInHeader;
        private bool mShowingToolTip;
        private bool mShowEventTooltips;
        private bool mLoadPresetHolidays;
        private bool mShowDisabledEvents;
        private bool mShowDashedBorderOnDisabledEvents;
        private bool mDimDisabledEvents;
        private bool mHighlightCurrentDay;
        private myCalendarViews mCalendarView;
        private CalendarEvent mClickedEvent;

        private const int margin = 4;

        //==================================================================================
        // ezCalendar properties.
        #region Properties

        //==================================================================================
        // These settings will change the layout of the ezCalendar control.  After the 
        // setting has been set the control must be adjust to represent those settings
        #region Settings that affect layout

        /// <summary>
        /// Indicates the type of calendar to render, Month or Day view
        /// </summary>
        public myCalendarViews CalendarView
        {
            get { return mCalendarView; }
            set
            {
                mCalendarView = value;
                
                //By default, going to month view will also show the date in header 
                //  and arrow buttons.
                switch (mCalendarView)
                {
                    case myCalendarViews.Day:
                        mShowDateInHeader = false;
                        ShowArrowControls = false;
                        mShowTodayButton = false;
                        break;

                    case myCalendarViews.Month:
                        mShowDateInHeader = true;
                        ShowArrowControls = true;
                        mShowTodayButton = true;
                        break;

                    default:
                        break;
                }

                lblCurrentDay.Visible = mShowDateInHeader;
                btnLeft.Visible = mShowArrowControls;
                btnRight.Visible = mShowArrowControls;
                btnToday.Visible = mShowTodayButton;

                zResize();
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether the date should be displayed in the upper left hand corner 
        /// of the calendar control.
        /// </summary>
        public bool ShowDateInHeader
        {
            get { return mShowDateInHeader; }
            set
            {
                mShowDateInHeader = value;
                lblCurrentDay.Visible = ShowDateInHeader;

                //#if (mCalendarView == myCalendarViews.Day)
                //#{
                //#    //#ResizeScrollPanel();  <== not sure if this will ever be needed in new code!
                //#}

                zResize();
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether the calendar control should render the previous/next 
        /// month buttons
        /// </summary>
        public bool ShowArrowControls
        {
            get { return mShowArrowControls; }
            set
            {
                mShowArrowControls = value;
                btnLeft.Visible = mShowArrowControls;
                btnRight.Visible = mShowArrowControls;

                //#if (mCalendarView == myCalendarViews.Day)
                //#{
                //#    //#ResizeScrollPanel();
                //#}

                zResize();
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether the calendar control should render the Today button
        /// </summary>
        public bool ShowTodayButton
        {
            get { return mShowTodayButton; }
            set
            {
                mShowTodayButton = value;
                btnToday.Visible = value;

                //#if (mCalendarView == myCalendarViews.Day)
                //#{
                //#    //#ResizeScrollPanel();
                //#}

                zResize();
                Refresh();
            }
        }

        #endregion

        //==================================================================================
        // These setting will change the various fonts used by the ezCalendar control.
        #region fontSettings

        /// <summary>
        /// Indicates the font for the times on the day view
        /// </summary>
        public Font DayViewTimeFont
        {
            get { return mDayViewTimeFont; }
            set
            {
                mDayViewTimeFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get or Set this value to the Font you wish to use to render the date in the upper right corner
        /// </summary>
        public Font DateHeaderFont
        {
            get { return mDateHeaderFont; }
            set
            {
                mDateHeaderFont = value;
                lblCurrentDay.Font = mDateHeaderFont;
                Refresh();
            }
        }

        /// <summary>
        /// The font used to render the days of the week text
        /// </summary>
        public Font DayOfWeekFont
        {
            get { return mDayOfWeekFont; }
            set
            {
                mDayOfWeekFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// The font used to render the Today button
        /// </summary>
        public Font TodayFont
        {
            get { return mTodayFont; }
            set
            {
                mTodayFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// The font used to render the number days on the calendar
        /// </summary>
        public Font DaysFont
        {
            get { return mDaysFont; }
            set
            {
                mDaysFont = value;
                Refresh();
            }
        }

        #endregion

        //==================================================================================
        // These setting control the display of events.
        #region Events

        /// <summary>
        /// Indicates whether events can be right-clicked and edited
        /// </summary>
        public bool AllowEditingEvents
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether disabled events will appear as "dimmed".
        /// This property is only used if <see cref="ShowDisabledEvents"/> is set to true.
        /// </summary>
        public bool DimDisabledEvents
        {
            get { return mDimDisabledEvents; }
            set
            {
                mDimDisabledEvents = value;
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether disabled events should show up on the calendar control
        /// </summary>
        public bool ShowDisabledEvents
        {
            get { return mShowDisabledEvents; }
            set
            {
                mShowDisabledEvents = value;
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether hovering over an event will display a tooltip of the event
        /// </summary>
        public bool ShowEventTooltips
        {
            get { return mShowEventTooltips; }
            set
            {
                mShowEventTooltips = value;
                //#mEventTip.Visible = false; 
            }
        }

        #endregion

        //==================================================================================
        // Misc properties.
        #region misc properties

        /// <summary>
        /// Indicates whether today's date should be highlighted
        /// </summary>
        public bool HighlightCurrentDay
        {
            get { return mHighlightCurrentDay; }
            set
            {
                mHighlightCurrentDay = value;
                Refresh();
            }
        }

        /// <summary>
        /// Indicates whether Federal Holidays are automatically preloaded onto the calendar
        /// </summary>
        public bool LoadPresetHolidays
        {
            get { return mLoadPresetHolidays; }
            set
            {
                mLoadPresetHolidays = value;
                if (mLoadPresetHolidays)
                {
                    mEvents.Clear();
                    //#PresetHolidays();
                    Refresh();
                }
                else
                {
                    mEvents.Clear();
                    Refresh();
                }
            }
        }

        /// <summary>
        /// The Date that the calendar is currently showing
        /// </summary>
        public DateTime CalendarDate
        {
            get { return mCalendarDate; }
            set
            {
                mCalendarDate = value;
                lblCurrentDay.Text = mCalendarDate.ToString("dd MMM yyyy");
                Refresh();
            }
        }

        /// <summary>
        /// Indicates if a dashed border should show up around disabled events.
        /// This property is only used if <see cref="ShowDisabledEvents"/> is set to true.
        /// TODO: What is this?
        /// </summary>
        public bool ShowDashedBorderOnDisabledEvents
        {
            get { return mShowDashedBorderOnDisabledEvents; }
            set
            {
                mShowDashedBorderOnDisabledEvents = value;
                Refresh();
            }
        }

        #endregion
        #endregion

        private readonly List<IEvent> mEvents;

        /// <summary>
        /// 
        /// </summary>
        public ezCalendar()
        {
            InitializeComponent();

            mCalendarDate = DateTime.Now;
            mDayOfWeekFont = new Font("Arial", 10, FontStyle.Regular);
            mDaysFont = new Font("Arial", 10, FontStyle.Regular);
            mTodayFont = new Font("Arial", 10, FontStyle.Bold);
            mDateHeaderFont = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            mDayViewTimeFont = new Font("Arial", 10, FontStyle.Bold);
            mShowArrowControls = true;
            mShowDateInHeader = true;
            mShowTodayButton = true;
            mShowingToolTip = false;
            mClickedEvent = null;
            mShowDisabledEvents = false;
            mShowDashedBorderOnDisabledEvents = true;
            mDimDisabledEvents = true;
            AllowEditingEvents = true;
            mHighlightCurrentDay = true;
            mCalendarView = myCalendarViews.Month;
            //#_scrollPanel = new ScrollPanel();

            //#_scrollPanel.RightButtonClicked += ScrollPanelRightButtonClicked;

            mEvents = new List<IEvent>();
            //#mRectangles = new List<Rectangle>();
            //#mCalendarDays = new Dictionary<int, Point>();
            //#mCalendarEvents = new List<CalendarEvent>();
            mShowEventTooltips = true;
            //#_eventTip = new EventToolTip { Visible = false };

            zResize();
        }

        /// <summary>
        /// Adds an event to the calendar
        /// </summary>
        /// <param name="calendarEvent">The <see cref="IEvent"/> to add to the calendar</param>
        public void AddEvent(IEvent calendarEvent)
        {
            //#_events.Add(calendarEvent);
            Refresh();
        }

        /// <summary>
        /// Removes an event from the calendar
        /// </summary>
        /// <param name="calendarEvent">The <see cref="IEvent"/> to remove to the calendar</param>
        public void RemoveEvent(IEvent calendarEvent)
        {
            //#_events.Remove(calendarEvent);
            Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResize()
        {
            //Changing the display of the date header changes where the top
            //  of the listbox will display.
            lblCurrentDay.Visible = ShowDateInHeader;
            btnToday.Visible = ShowTodayButton;
            btnLeft.Visible = ShowArrowControls;
            btnRight.Visible = ShowArrowControls;

            //Adjust the top of the dayevents display listbox based on
            // the visibility of current date visible and button visibility.
            if (ShowDateInHeader || ShowArrowControls || ShowTodayButton)
            {
                listBox1.Top = lblCurrentDay.Top + lblCurrentDay.Height + 4;
            }
            else
            {
                listBox1.Top = lblCurrentDay.Top;
            }

            //NOW based on the type of display mode (day/month) we can adjust
            //  the control positions and sizes.
            switch (mCalendarView)
            {
                case myCalendarViews.Day:
                    monthCalendar1.Top = btnToday.Top;
                    monthCalendar1.Visible = true;

                    panel1.Top = monthCalendar1.Top + monthCalendar1.Height + margin;
                    panel1.Height = this.Height - panel1.Top - panel1.Left - margin;

                    listBox1.Width = monthCalendar1.Left - listBox1.Left - margin;
                    listBox1.Height = panel1.Top - listBox1.Top - margin;

                    vScrollBar1.Top = panel1.Top;
                    vScrollBar1.Height = panel1.Height;
                    break;

                case myCalendarViews.Month:
                    monthCalendar1.Visible = false;

                    panel1.Top = listBox1.Top + listBox1.Height + margin;
                    panel1.Height = this.Height - panel1.Top - margin;

                    listBox1.Width = panel1.Width;
                    listBox1.Height = panel1.Top - listBox1.Top - margin;

                    vScrollBar1.Top = panel1.Top;
                    vScrollBar1.Height = panel1.Height;
                    break;

                default:
                    break;
            }
            Refresh();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            CalendarDate = e.Start;
        }

    }
}
