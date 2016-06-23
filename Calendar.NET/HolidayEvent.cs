using System;
using System.Drawing;

namespace Calendar.NET
{
    /// <summary>
    /// An event that defines a holiday
    /// </summary>
    public class HolidayEvent : IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public int Rank
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public float EventLengthInHours
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public CustomRecurringFrequenciesHandler CustomRecurringFunction
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IgnoreTimeComponent
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ReadOnlyEvent
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Color EventColor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Font EventFont
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EventText
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Color EventTextColor
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public RecurringFrequencies RecurringFrequency
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool TooltipEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool ThisDayForwardOnly
        {
            get;
            set;
        }

        /// <summary>
        /// HolidayEvent Constructor
        /// </summary>
        public HolidayEvent()
        {
            EventColor = Color.FromArgb(80, 170, 255);
            EventFont = new Font("Arial", 8, FontStyle.Bold);
            EventTextColor = Color.FromArgb(255, 255, 255);
            Rank = 1;
            EventLengthInHours = 24;
            ReadOnlyEvent = true;
            Enabled = true;
            IgnoreTimeComponent = true;
            TooltipEnabled = true;
            ThisDayForwardOnly = false;
            RecurringFrequency = RecurringFrequencies.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEvent Clone()
        {
            return new HolidayEvent
                         {
                             CustomRecurringFunction = CustomRecurringFunction,
                             Date = Date,
                             Enabled = Enabled,
                             EventColor = EventColor,
                             EventFont = EventFont,
                             EventText = EventText,
                             EventTextColor = EventTextColor,
                             IgnoreTimeComponent = IgnoreTimeComponent,
                             Rank = Rank,
                             ReadOnlyEvent = ReadOnlyEvent,
                             RecurringFrequency = RecurringFrequency,
                             ThisDayForwardOnly = ThisDayForwardOnly,
                             EventLengthInHours = EventLengthInHours,
                             TooltipEnabled = TooltipEnabled
                         };
        }
    }
}
