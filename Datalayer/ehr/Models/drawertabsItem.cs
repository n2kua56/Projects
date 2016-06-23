using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.ehr.Models
{
    /// <summary>
    /// Maps a tab to a drawer.
    /// </summary>
    public class drawertabsItem
    {
        private int mId;
        private int mDrawerId;
        private int mTabId;
        private DateTime mCreated;

        /// <summary>
        /// gets/sets this record id mapping a drawer to a tab
        /// </summary>
        public int Id 
        {
            get { return mId; }
            set { mId = value; }
        }

        /// <summary>
        /// gets/sets the `ehr`.`drawers` row id that the tab is being added to
        /// </summary>
        public int DrawerId
        {
            get { return mDrawerId; }
            set { mDrawerId = value; }
        }

        /// <summary>
        /// gets/sets the `ehr`.`tabs` row id that is being mapped to the drawer
        /// </summary>
        public int TabId
        {
            get { return mTabId; }
            set { mTabId = value; }
        }

        /// <summary>
        /// gets the date/time this row was created
        /// </summary>
        public DateTime Created
        {
            get { return mCreated; }
            set { mCreated = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public drawertabsItem()
        {
        }

        /// <summary>
        /// Constructor by specifying the drawer and the tab.
        /// </summary>
        /// <param name="drawer">`ehr`.`drawers` row id</param>
        /// <param name="tab">`ehr`.`tabs` row id</param>
        public drawertabsItem(int drawer, int tab)
        {
            mDrawerId = drawer;
            mTabId = tab;
        }
    }
}
