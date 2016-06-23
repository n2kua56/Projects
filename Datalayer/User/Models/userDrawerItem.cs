using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.User.Models
{
    public class userDrawerItem
    {
        private int mId;
        private int mUserId;
        private int mDrawerId;
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
        /// gets/sets the user that the drawers are being added to
        /// </summary>
        public int UserId
        {
            get { return mUserId; }
            set { mUserId = value; }
        }

        /// <summary>
        /// gets/sets the `ehr`.`drawers` row id that is being added to the user
        /// </summary>
        public int DrawerId
        {
            get { return mDrawerId; }
            set { mDrawerId = value; }
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
        public userDrawerItem()
        {
        }

        /// <summary>
        /// Constructor by specifying the drawer and the tab.
        /// </summary>
        /// <param name="drawer">`ehr`.`drawers` row id</param>
        /// <param name="tab">`ehr`.`tabs` row id</param>
        public userDrawerItem(int user, int drawer)
        {
            mUserId = user;
            mDrawerId = drawer;
        }
    }
}
