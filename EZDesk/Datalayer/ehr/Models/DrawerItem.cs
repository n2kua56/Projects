using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datalayer.ehr.Models
{
    public class DrawerItem
    {
        private int mDrawerId = -1;
        private string mDrawerName = "";
        private string mDrawerDesc = "";
        private int mSeq = 0;
        private bool mIsActive = true;
        private DateTime mCreated = DateTime.MinValue;

        /// <summary>
        /// The database ID for this Drawer
        /// </summary>
        public int DrawerId
        {
            get { return mDrawerId; }
            set { mDrawerId = value; }
        }

        /// <summary>
        /// Gets/Sets the Drawer Name for this Drawer
        /// </summary>
        public string DrawerName 
        {
            get { return mDrawerName; }
            set { mDrawerName = value; }
        }

        /// <summary>
        /// Gets/Sets the description of what this drawer is used for.
        /// </summary>
        public string DrawerDesc 
        {
            get { return mDrawerDesc; }
            set { mDrawerDesc = value; }
        }

        /// <summary>
        /// Gets/Sets the display sequence for this drawer.
        /// </summary>
        public int Seq 
        {
            get { return mSeq; }
            set { mSeq = value; }
        }

        /// <summary>
        /// Gets/Sets the flag if this drawer is active
        /// </summary>
        public bool IsActive 
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        /// <summary>
        /// Gets/Sets the DateTime this drawer was created
        /// </summary>
        public DateTime Created 
        {
            get { return mCreated; }
            set { mCreated = value; }
        }

        public DrawerItem()
        {
        }

    }
}
