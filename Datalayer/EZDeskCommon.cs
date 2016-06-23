using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace EZDeskDataLayer
{
    public class EZDeskCommon
    {
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mUserPerson = null;
        private EZDeskDataLayer.User.Models.UserDetails mUser = null;
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mPerson = null;

        public EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        public EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        public EZDeskDataLayer.User.UserController uCtrl = null;
        public EZDeskDataLayer.Address.AddressCtrl aCtrl = null;
        public EZDeskDataLayer.Communications.CommunicationCtrl cCtrl = null;

        public DataGridViewCellStyle AltRowGrey = new DataGridViewCellStyle();

        /// <summary>
        /// Gets/Sets the MySql connection string
        /// </summary>
        private string mConnStr = "";
        public string ConnStr
        {
            get { return mConnStr; }
            set { mConnStr = value; }
        }

        /// <summary>
        /// Gets/Sets the MySqlConnection
        /// </summary>
        private MySqlConnection mMySqlConnection = null;
        public MySqlConnection Connection
        {
            get { return mMySqlConnection; }
            set { mMySqlConnection = value; }
        }

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
            AltRowGrey.BackColor = Color.LightGray;
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
            AltRowGrey.BackColor = Color.LightGray;
        }

        public void SetControllers()
        {
            eCtrl = new ehr.ehrCtrl(this);
            pCtrl = new Person.PersonCtrl(this);
            uCtrl = new User.UserController(this);
            aCtrl = new Address.AddressCtrl(this);
            cCtrl = new Communications.CommunicationCtrl(this);
        }

        /// <summary>
        /// Calculate the new forms left and top position, centered on the form
        /// that is lauching the new form.  Don't let the new form go off the 
        /// screen.
        /// </summary>
        /// <param name="launchFormLeft">Launching forms current Left</param>
        /// <param name="launchFormTop">Launching forms current Top</param>
        /// <param name="launchFormWidth">Launching forms Width</param>
        /// <param name="launchFormHeight">Launching forms Height</param>
        /// <param name="newFormWidth">New forms Width</param>
        /// <param name="newFormHeight">New forms Height</param>
        /// <param name="newFormLeft">Calculated new forms Left</param>
        /// <param name="newFormTop">Calculated new forms Top</param>
        public void SetFormLeftTop(int launchFormLeft, int launchFormTop, int launchFormWidth, int launchFormHeight,
                                    int newFormWidth, int newFormHeight, out int newFormLeft, out int newFormTop)
        {
            newFormLeft = launchFormLeft + ((launchFormWidth - newFormWidth) / 2);
            newFormTop = launchFormTop + ((launchFormHeight - newFormHeight) / 2);

            if (newFormTop < 0) { newFormTop = 0; }
            if (newFormLeft < 0) { newFormLeft = 0; }
            
            if ((newFormTop + newFormHeight) > Screen.PrimaryScreen.Bounds.Height)
            {
                newFormTop = Screen.PrimaryScreen.Bounds.Height - newFormHeight;
            }

            if ((newFormLeft + newFormWidth) > Screen.PrimaryScreen.Bounds.Width)
            {
                newFormLeft = Screen.PrimaryScreen.Bounds.Width - newFormWidth;
            }
        }

    }
}
