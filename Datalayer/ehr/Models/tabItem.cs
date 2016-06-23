using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.ehr.Models
{
    public class tabItem
    {
        /// <summary>
        /// The database id for this tab
        /// </summary>
        public int TabId { get; set; }

        /// <summary>
        /// The tab display name for this tab
        /// </summary>
        public string TabName { get; set; }

        /// <summary>
        /// The description for this tab
        /// </summary>
        public string TabDesc { get; set; }

        /// <summary>
        /// True the tab is active, false the tab is no longer used
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// An integer used to sort the tabs for display, both in
        /// the settings form and in the tabs on the main form.
        /// </summary>
        public int DisplaySeq { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public tabItem()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        public tabItem(string name, string desc)
        {
            TabName = name;
            TabDesc = desc;
        }
    }
}
