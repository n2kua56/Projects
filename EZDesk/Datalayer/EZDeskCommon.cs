using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer
{
    public class EZDeskCommon
    {
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mUserPerson = null;
        private EZDeskDataLayer.User.Models.UserDetails mUser = null;
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mPerson = null;
        
        /// <summary>
        /// The Person data for the person currently signed on.
        /// </summary>
        public EZDeskDataLayer.Person.Models.PersonFormGetDemographics Staff 
        {
            get { return mUserPerson; }
            set { mUserPerson = value; } 
        }

        /// <summary>
        /// The User data for the user currently signed on.
        /// </summary>
        public EZDeskDataLayer.User.Models.UserDetails User
        {
            get { return mUser; }
            set { mUser = value; }
        }

        /// <summary>
        /// The Person data for the currently selected person.
        /// </summary>
        public EZDeskDataLayer.Person.Models.PersonFormGetDemographics Person
        {
            get { return mPerson; }
            set { mPerson = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public EZDeskCommon()
        {
            Staff = null;
            User = null;
            Person = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="u"></param>
        public EZDeskCommon(EZDeskDataLayer.Person.Models.PersonFormGetDemographics p,
            EZDeskDataLayer.User.Models.UserDetails u)
        {
            Staff = p;
            User = u;
            Person = null;
        }

    }
}
