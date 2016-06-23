using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.ehr.Models
{
    public class AvailablePropertyItem
    {
        private int mPropID;
        private DateTime mCreated;
        private bool mIsActive;
        private DateTime mModified;
        private string mPropertyName;
        private string mDescription;
        private string mPropertyValue;
        //VISIBILITY

        /// <summary>
        /// Row ID in the ehr_availableproperties table
        /// </summary>
        public int PropID 
        {
            get { return mPropID; }
            set { mPropID = value; }
        }

        /// <summary>
        /// gets/sets theDate/Time the property was added
        /// </summary>
        public DateTime Created 
        {
            get { return mCreated; }
            set { mCreated = value; } 
        }

        /// <summary>
        /// gets/sets the active flag.
        /// </summary>
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        /// <summary>
        /// gets the Date/Time the property was last modified
        /// </summary>
        public DateTime Modified
        {
            get { return mModified; }
            set { mModified = value; }
        }

        /// <summary>
        /// gets/Sets the name of the property
        /// </summary>
        public string PropertyName 
        {
            get { return mPropertyName; }
            set { mPropertyName = value; }
        }

        /// <summary>
        /// gets/sets the description of the property
        /// </summary>
        public string Description 
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        
        /// <summary>
        /// gets/sets the property value
        /// </summary>
        public string PropertyValue 
        {
            get { return mPropertyValue; }
            set { mPropertyValue = value; }
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public AvailablePropertyItem()
        {
        }

        /// <summary>
        /// Constructor that fills in the relative values
        /// </summary>
        /// <param name="propname"></param>
        /// <param name="description"></param>
        /// <param name="propvalue"></param>
        public AvailablePropertyItem(string propname, string description, string propvalue)
        {
            PropertyName = propname;
            Description = description;
            PropertyValue = propvalue;
            IsActive = true;
        }
    }
}
