using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.User;
using EZUtils;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;
using pModels = EZDeskDataLayer.Person.Models;

namespace EZDesk
{
    public partial class frmUser : Form
    {
        private MySqlConnection mConn = null;
        private int mPersonID = -1;
        private int mUserID = -1;
        private UserTypes mUserType = UserTypes.Undefined;
        private object mTPDemographics = null;
        private object mTPUser = null;
        private string mModName = "frmUser";
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mPerson = null;
        private EZDeskDataLayer.User.Models.UserDetails mUser = null;

        EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        EZDeskDataLayer.Address.AddressCtrl aCtrl = null;
        EZDeskDataLayer.Communications.CommunicationCtrl cCtrl = null;
        EZDeskDataLayer.User.UserController uCtrl = null;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="Conn"></param>
        /// <param name="typ"></param>
        public frmUser(MySqlConnection Conn, UserTypes typ, int personid)
        {
            InitializeComponent();
            mPersonID = personid;
            mUserType = typ;
            mConn = Conn;

            pCtrl = new EZDeskDataLayer.Person.PersonCtrl(mConn);
            aCtrl = new EZDeskDataLayer.Address.AddressCtrl(mConn);
            cCtrl = new EZDeskDataLayer.Communications.CommunicationCtrl(mConn);
            uCtrl = new UserController(mConn);
    
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "frmUser constructor"));

            try
            {
                step = "Fill comboboxes";
                zFillinLanguageDropDown();
                zFillinRaceDropDown();
                zFillinEthnicityDropDown();

                //Now that InitializeComponent has run, set the global objects
                //  to remember the tab pages.
                step = "Build ddControl1";
                ddControl1.Text = "New";
                List<string> lstSting = new List<string>();
                foreach (aModels.Address.AddressTypeEnum a in
                        Enum.GetValues(typeof(aModels.Address.AddressTypeEnum)).Cast<EZDeskDataLayer.Address.Models.Address.AddressTypeEnum>())
                {
                    if (a.ToString().ToLower() != "udefined")
                    {
                        lstSting.Add(a.ToString());
                    }
                }
                ddControl1.FillControlList(lstSting);

                step = "Save tab pages";
                mTPDemographics = tpDemographics;
                mTPUser = tpUser;

                //Remove all tabs.
                step = "Remove tab pages";
                foreach (TabPage page in tabControl1.TabPages)
                {
                    tabControl1.TabPages.Remove(page);
                }

                //Fill-in the form
                step = "Fill-in form";
                zFillInForm();

                //Build the text for the title bar.
                step = "Build the title bar";
                string title = "";
                if (mPersonID == -1)
                {
                    title = "New ";
                }
                title += mUserType.ToString();
                this.Text = title;
            }

            catch (Exception ex)
            {
                //Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "frmUser constructor"));
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmUser constructor"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInForm()
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zFillInForm"));

            try
            {
                //Depending on the person type being passed in, make
                //  the appropriate tab pages visible (add them back in)
                //  and fill them in if the patientid > -1.
                switch (mUserType)
                {
                    case UserTypes.Doctor:
                    case UserTypes.Staff:
                    case UserTypes.ClinicalStaff:
                    case UserTypes.ExtCarePerson:
                    case UserTypes.LabDoctor:
                    case UserTypes.ReferringDoctor:
                        step = "Fill-in Staff";
                        zFillinDemographicsPage();
                        zFillinUserPage();
                        break;

                    case UserTypes.Company:
                    case UserTypes.LabFacility:
                        step = "Fill-in Company";
                        zFillinDemographicsPage();
                        break;

                    case UserTypes.Patient:
                    case UserTypes.InsuranceGuarentor:
                        step = "Fill-in Patient";
                        zFillinDemographicsPage();
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillInForm"));
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillInForm"));
            }
        }

        private bool mDontChange = false;

        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
        {
            mDontChange = true;
            cbDecline.Checked = false;
            mDontChange = false;
        }

        private void cbDecline_CheckedChanged(object sender, EventArgs e)
        {
            if (!mDontChange)
            {
                if (cbDecline.Checked)
                {
                    dtpBirthDay.Value = dtpBirthDay.MinDate;
                    cmbSex.Text = "";
                }
            }
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            mDontChange = true;
            cbDecline.Checked = false;
            mDontChange = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            MySqlTransaction trans = null;
            bool transComplete = false;

            Trace.Enter(Trace.RtnName(mModName, "cmdSave_Click"));

            try
            {
                msg = zValidateForm();
                if (msg.Length == 0)
                {
                    //Start a transaction
                    trans = mConn.BeginTransaction();
                    transComplete = true;
                    switch (mUserType)
                    {
                        case UserTypes.Doctor:
                        case UserTypes.Staff:
                        case UserTypes.ClinicalStaff:
                        case UserTypes.ExtCarePerson:
                        case UserTypes.LabDoctor:
                        case UserTypes.ReferringDoctor:
                            transComplete = zAddUpdatePatient(trans);
                            if (transComplete)
                            {
                                transComplete = zAddUpdateUser(trans);
                            }
                            break;

                        case UserTypes.Company:
                        case UserTypes.LabFacility:
                            transComplete = zAddUpdatePatient(trans);
                            break;

                        case UserTypes.Patient:
                        case UserTypes.InsuranceGuarentor:
                            transComplete = zAddUpdatePatient(trans);
                            break;

                        default:
                            trans.Rollback();
                            transComplete = false;
                            MessageBox.Show("Unsupported user type", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }

                    if (transComplete)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

                //There was some error, display the messages
                else
                {
                    msg = msg.Replace("|", "\n");
                    MessageBox.Show("Errors found:\n\n" + msg, "Errors",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                msg = "cmdSave_Click failed: " + ex.Message;
                try
                {
                    trans.Rollback();
                }
                catch { }
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "cmdSave_Click"));
                //#Trace.ExceptionDataKeyAdd(ex.Data, "msg", msg);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdSave_Click"));
            }
        }

        //=================================================================================
        // Methods for the User Page
        #region Demographics Page

        //=================================================================================
        // Some controls need filling in before the selected person data
        // can be applied, like drop down (combo boxes).
        #region Fill-in Demographics page combo boxes etc.

        /// <summary>
        /// Fill-in the Language combo box on the demographics page
        /// </summary>
        private void zFillinLanguageDropDown()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinLanguage"));

            try
            {
                EZDeskDataLayer.LanguageList.LanguageController ctrl =
                    new EZDeskDataLayer.LanguageList.LanguageController(mConn);

                DataTable LanguageTable = ctrl.GetLanguageList();
                cmbLanguage.DataSource = LanguageTable;
                cmbLanguage.DisplayMember = "Language";
                cmbLanguage.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinLanguage"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinLanguage"));
            }
        }

        /// <summary>
        /// Fill-in the Race combo box on the Demographics page
        /// </summary>
        private void zFillinRaceDropDown()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinRace"));

            try
            {
                EZDeskDataLayer.RaceList.RaceController ctrl =
                        new EZDeskDataLayer.RaceList.RaceController(mConn);

                DataTable RaceTable = ctrl.GetRaceList();
                cmbRace.DataSource = RaceTable;
                cmbRace.DisplayMember = "Race";
                cmbRace.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinRace"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinRace"));
            }
        }

        /// <summary>
        /// Fill-in the Ethnicity combo box on the Demographics page
        /// </summary>
        private void zFillinEthnicityDropDown()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinEthnicity"));

            try
            {
                EZDeskDataLayer.EthnicityList.EthnicityController ctrl =
                    new EZDeskDataLayer.EthnicityList.EthnicityController(mConn);

                DataTable EthnicityTable = ctrl.GetEthnicityList();
                cmbEthnicity.DataSource = EthnicityTable;
                cmbEthnicity.DisplayMember = "Ethnicity";
                cmbEthnicity.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinEthnicity"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinEthnicity"));
            }
        }

        #endregion

        //================================================================
        // Fill-in the Demographics Page with data from the passed
        // in user.
        #region Fill-in the Demographics Page

        /// <summary>
        /// Make the Demographics page visible and fill-in if we have
        /// a personid to get for.
        /// </summary>
        private void zFillinDemographicsPage()
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zFillinDemographics"));

            try
            {
                step = "Add the Demographics tab back in";
                tabControl1.TabPages.Add((TabPage)mTPDemographics);
                zClearPage(tpDemographics.Controls);

                if (mPersonID > -1)
                {
                    step = "Get the person to fill-in";
                    mPerson = pCtrl.GetPersonByID(mPersonID);

                    if (mPerson != null)
                    {
                        step = "Fill-in controls";
                        zFillInDemographics();
                        step = "zFillInAddress";
                        zFillInAddress(mPerson.addresses);
                        step = "zFillinCommunication";
                        zFillinCommunication(mPerson.comms);
                    }
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", mModName + ".zFillinDemographics");
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinDemographics"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInDemographics()
        {
            tbPersonID.Text = mPerson.PersonID.ToString();
            tbSharedID.Text = mPerson.SharedID;
            tbSSN.Text = mPerson.SSNO;
            tbPrefix.Text = mPerson.Prefix;
            tbFirstName.Text = mPerson.FirstName;
            tbMiddleName.Text = mPerson.MiddleName;
            tbLastName.Text = mPerson.LastName;
            tbSuffix.Text = mPerson.Suffix;
            dtpBirthDay.Value = (mPerson.BirthDate != null) ?
                    (DateTime)mPerson.BirthDate : DateTime.MinValue;

            zFillInSex();
            zFillInRace();
            zFillInEthnicity();
            zFillInLanguage();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInLanguage()
        {
            int LanguageIdx = -1;
            for (int rIdx = 0; ((rIdx < cmbLanguage.Items.Count) && (LanguageIdx < 0)); rIdx++)
            {
                DataRowView dr = (DataRowView)cmbLanguage.Items[rIdx];
                if (mPerson.LanguageTypeID == Convert.ToInt32(dr["Id"].ToString()))
                {
                    LanguageIdx = rIdx;
                }
            }
            if (LanguageIdx < 0)
            {
                LanguageIdx = 0;
            }
            cmbLanguage.SelectedIndex = LanguageIdx;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInEthnicity()
        {
            int EthnicityIdx = -1;
            for (int rIdx = 0; ((rIdx < cmbEthnicity.Items.Count) && (EthnicityIdx < 0)); rIdx++)
            {
                DataRowView dr = (DataRowView)cmbEthnicity.Items[rIdx];
                if (mPerson.EthnicityTypeID == Convert.ToInt32(dr["Id"].ToString()))
                {
                    EthnicityIdx = rIdx;
                }
            }
            if (EthnicityIdx < 0)
            {
                EthnicityIdx = 0;
            }
            cmbEthnicity.SelectedIndex = EthnicityIdx;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInRace()
        {
            int raceIdx = -1;
            for (int rIdx = 0; ((rIdx < cmbRace.Items.Count) && (raceIdx < 0)); rIdx++)
            {
                DataRowView dr = (DataRowView)cmbRace.Items[rIdx];
                if (mPerson.RaceTypeID == Convert.ToInt32(dr["Id"].ToString()))
                {
                    raceIdx = rIdx;
                }
            }
            if (raceIdx < 0)
            {
                raceIdx = 0;
            }
            cmbRace.SelectedIndex = raceIdx;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInSex()
        {
            string key = mPerson.Sex;
            int idx = cmbSex.Items.IndexOf(key);
            if (idx > -1)
            {
                cmbSex.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// Fill-in the Communications group box.
        /// </summary>
        private void zFillinCommunication(List<EZDeskDataLayer.Communications.Models.Communication> comms)
        {
            string[] flds = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zFillinCommunication"));

            try
            {
                tbHome.Tag = "";
                tbWork.Tag = "";
                tbFax.Tag = "";
                tbCell.Tag = "";
                tbEmail.Tag = "";

                if (comms != null)
                {
                    foreach (EZDeskDataLayer.Communications.Models.Communication com in comms)
                    {
                 
                        switch ((int)com.CommunicationType)
                        {
                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone):
                                step = "Fill-in Home Phone";
                                flds = com.CommunicationCode.Split('x');
                                if (flds.Length > 0) { tbHome.Text = flds[0]; }
                                if (flds.Length > 1) { tbHomeExt.Text = flds[1]; }
                                tbHome.Tag = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone):
                                step = "Fill-in Work Phone";
                                flds = com.CommunicationCode.Split('x');
                                if (flds.Length > 0) { tbWork.Text = flds[0]; }
                                if (flds.Length > 1) { tbWorkExt.Text = flds[1]; }
                                tbWork.Tag = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.CellPhone):
                                step = "Fill-in Cell Phone";
                                tbCell.Text = com.CommunicationCode;
                                tbCell.Tag = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.Fax):
                                step = "Fill-in Fax";
                                tbFax.Text = com.CommunicationCode;
                                tbFax.Tag = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.EMAIL):
                                step = "Fill-in EMail";
                                tbEmail.Text = com.CommunicationCode;
                                tbEmail.Tag = com.CommunicationID.ToString();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinCommunication")); 
                ex.Data.Add("step", step);
                throw ex;
            }

            finally 
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinCommunication"));
            }
        }

        /// <summary>
        /// Fill-in the Address group box on the demographics form
        /// </summary>
        /// <remarks>
        /// For now, just the home address.
        /// </remarks>
        private void zFillInAddress(List<EZDeskDataLayer.Address.Models.Address> addrs)
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zFillInAddress"));

            try
            {
                lblBusinessId.Text = "";
                lblHomeId.Text = "";
                lblWorkId.Text = "";

                if (addrs != null)
                {
                    foreach (EZDeskDataLayer.Address.Models.Address addr in addrs)
                    {
                        if (addr.AddressType == EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home)
                        {
                            switch (addr.AddressType)
                            {
                                case aModels.Address.AddressTypeEnum.Home:
                                    step = "Fill-in Home address";
                                    tbAddress1.Text = addr.Address1;
                                    tbAddress2.Text = addr.Address2;
                                    tbCity.Text = addr.City;
                                    tbState.Text = addr.State;
                                    tbZip.Text = addr.Zip;
                                    lblHomeId.Text = addr.AddressID.ToString();
                                    break;
                                case aModels.Address.AddressTypeEnum.Work:
                                    step = "Fill-in Work address";
                                    tbWorkStreet1.Text = addr.Address1;
                                    tbWorkStreet2.Text = addr.Address2;
                                    tbWorkCity.Text = addr.City;
                                    tbWorkState.Text = addr.State;
                                    tbWorkZip.Text = addr.Zip;
                                    lblWorkId.Text = addr.AddressID.ToString();
                                    break;
                                case aModels.Address.AddressTypeEnum.Business:
                                    step = "Fill-in Business address";
                                    tbBusStreet1.Text = addr.Address1;
                                    tbBusStreet2.Text = addr.Address2;
                                    tbBusCity.Text = addr.City;
                                    tbBusState.Text = addr.State;
                                    tbBusZip.Text = addr.Zip;
                                    lblBusinessId.Text = addr.AddressID.ToString();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, ".zFillInAddress"));
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillInAddress"));
            }
        }

        #endregion

        //=================================================================
        // Insert or Update the entries on the Demographics page
        #region save demographics

        /// <summary>
        /// Validate the Demographics page
        /// </summary>
        /// <returns></returns>
        private string zValidDemographics()
        {
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "zValidDemographics"));

            try
            {
                if (tbLastName.Text.Trim().Length == 0)
                {
                    rtn = "You must specify a Last Name|";
                }
                if (!cbDecline.Checked)
                {
                    if (cmbSex.Text.Trim().Length == 0)
                    {
                        rtn += "You must specify Sex|";
                    }
                    //// Birthday should NOT be required.
                    ////if (dtpBirthDay.Value == dtpBirthDay.MinDate)
                    ////{
                    ////    rtn += "You must specify Birthday|";
                    ////}
                }

                return rtn;
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zValidDemographics"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidDemographics"));
            }
        }

        /// <summary>
        /// Add or Update a Patient
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool zAddUpdatePatient(MySqlTransaction trans)
        {
            bool transComplete = true;

            Trace.Enter(Trace.RtnName(mModName, "zAddUpdatePatient"));

            try
            {
                if (mPersonID == -1)
                {
                    mPersonID = zAddDemographics(trans);
                }
                else
                {
                    zUpdateDemographics(trans);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error adding/updating " + mUserType.ToString() + "\n" + ex.Message,
                                "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transComplete = false;
                trans.Rollback();
                //TODO: Clear the screen?
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddUpdatePatient"));
            }

            return transComplete;
        }

        /// <summary>
        /// INSERT a demographic entry (Person).
        /// </summary>
        private int zAddDemographics(MySqlTransaction transaction)
        {
            string step = "";
            DataTable tbl = new DataTable();
            int personID = -1;

            Trace.Enter(Trace.RtnName(mModName, "zAddDemographics"));

            try
            {
                step = "Pull demographic data from controls";
                mPerson = new EZDeskDataLayer.Person.Models.PersonFormGetDemographics();

                // Get Demographic data...
                step = "Get Demographic data";
                zPullDemographics(mPerson);
                string temp = mUserType.ToString();
                mPerson.PersonType = (pModels.PersonFormGetDemographics.PersonTypeEnum)pCtrl.GetPersonTypeByName(temp);

                // Add the address if any...
                step = "Get Address data";
                mPerson.addresses = 
                    new List<EZDeskDataLayer.Address.Models.Address>();
                zPullAddresses(mPerson);

                // Add the phone numbers...
                step = "Get Phone data";
                mPerson.comms = 
                    new List<EZDeskDataLayer.Communications.Models.Communication>();
                zPullPhoneNumbers(mPerson);

                step = "Update person";
                personID = pCtrl.UpDatePerson(mPerson, transaction);
                return personID;
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zAddDemographics"));
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddDemographics"));
            }
        }

        /// <summary>
        /// UPDATE a demographic entry (Person).
        /// </summary>
        private void zUpdateDemographics(MySqlTransaction transaction)
        {
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "zUpdateDemographics"));

            try
            {
                zPullDemographics(mPerson);
                zPullAddresses(mPerson);
                zPullPhoneNumbers(mPerson);
                pCtrl.UpDatePerson(mPerson, transaction);
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zUpdateDemographics"));
                ex.Data.Add("SQL", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zUpdateDemographics"));
            }
        }

        /// <summary>
        /// Pull the Communications info from the screen controls and
        /// add them to the person structure.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="temp"></param>
        private void zPullPhoneNumbers(EZDeskDataLayer.Person.Models.PersonFormGetDemographics person)
        {
            string temp = "";

            if (tbHome.Text.Trim().Length > 0)
            {
                temp = tbHome.Text.Trim();
                if (tbHomeExt.Text.Trim().Length > 0)
                {
                    temp += "x" + tbHomeExt.Text.Trim();
                }
                EZDeskDataLayer.Communications.Models.Communication home =
                    new EZDeskDataLayer.Communications.Models.Communication();
                home.CommunicationCode = temp;
                home.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone;
                home.PersonID = person.PersonID;
                
                home.CommunicationID = (tbHome.Tag.ToString().Length > 0) ?
                    Convert.ToInt32(tbHome.Tag.ToString()) : -1;

                if (person.comms == null)
                {
                    person.comms = new List<cModels.Communication>();
                }
                person.comms.Add(home);
            }

            if (tbFax.Text.Trim().Length > 0)
            {
                EZDeskDataLayer.Communications.Models.Communication fax =
                    new EZDeskDataLayer.Communications.Models.Communication();
                fax.CommunicationCode = tbFax.Text.Trim();
                fax.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.Fax;

                fax.CommunicationID = (tbFax.Tag.ToString().Length > 0) ?
                    Convert.ToInt32(tbFax.Tag.ToString()) : -1;

                fax.PersonID = person.PersonID;
                if (person.comms == null)
                {
                    person.comms = new List<cModels.Communication>();
                }
                person.comms.Add(fax);
            }

            if (tbWork.Text.Trim().Length > 0)
            {
                temp = tbWork.Text.Trim();
                if (tbWorkExt.Text.Trim().Length > 0)
                {
                    temp += "x" + tbWorkExt.Text.Trim();
                }
                EZDeskDataLayer.Communications.Models.Communication work =
                    new EZDeskDataLayer.Communications.Models.Communication();
                work.CommunicationCode = temp;
                work.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone;

                work.CommunicationID = (tbWork.Tag.ToString().Length > 0) ?
                    Convert.ToInt32(tbWork.Tag.ToString()) : -1;

                work.PersonID = person.PersonID;
                if (person.comms == null)
                {
                    person.comms = new List<cModels.Communication>();
                }
                person.comms.Add(work);
            }

            if (tbCell.Text.Trim().Length > 0)
            {
                EZDeskDataLayer.Communications.Models.Communication cell =
                    new EZDeskDataLayer.Communications.Models.Communication();
                cell.CommunicationCode = tbCell.Text.Trim();
                cell.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.CellPhone;

                cell.CommunicationID = (tbCell.Tag.ToString().Length > 0) ?
                    Convert.ToInt32(tbCell.Tag.ToString()) : -1;

                cell.PersonID = person.PersonID;
                if (person.comms == null)
                {
                    person.comms = new List<cModels.Communication>();
                }
                person.comms.Add(cell);
            }

            if (tbEmail.Text.Trim().Length > 0)
            {
                EZDeskDataLayer.Communications.Models.Communication email =
                    new EZDeskDataLayer.Communications.Models.Communication();
                email.CommunicationCode = tbEmail.Text.Trim();
                email.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.EMAIL;

                email.CommunicationID = (tbEmail.Tag.ToString().Length > 0) ?
                    Convert.ToInt32(tbEmail.Tag.ToString()) : -1;

                email.PersonID = person.PersonID;
                if (person.comms == null)
                {
                    person.comms = new List<cModels.Communication>();
                }
                person.comms.Add(email);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <param name="addr"></param>
        private void zPullAddresses(EZDeskDataLayer.Person.Models.PersonFormGetDemographics person)
        {
            EZDeskDataLayer.Address.Models.Address addr = null;
                
            if ((tbAddress1.Text.Trim().Length > 0) ||
                (tbAddress2.Text.Trim().Length > 0) ||
                (tbCity.Text.Trim().Length > 0) ||
                (tbZip.Text.Trim().Length > 0) ||
                (tbState.Text.Trim().Length > 0))
            {
                addr = new EZDeskDataLayer.Address.Models.Address();
                addr.Address1 = tbAddress1.Text.Trim();
                addr.Address2 = tbAddress2.Text.Trim();
                addr.City = tbCity.Text.Trim();
                addr.State = tbState.Text.Trim();
                addr.Zip = tbZip.Text.Trim();
                addr.PersonID = person.PersonID;

                addr.AddressID = (lblHomeId.Text.Length > 0) ?
                    Convert.ToInt32(lblHomeId.Text) : -1;

                addr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home;
                if (person.addresses == null)
                {
                    person.addresses = new List<aModels.Address>();
                }
                person.addresses.Add(addr);
            }

            if ((tbWorkStreet1.Text.Trim().Length > 0) ||
                (tbWorkStreet2.Text.Trim().Length > 0) ||
                (tbWorkCity.Text.Trim().Length > 0) ||
                (tbWorkZip.Text.Trim().Length > 0) ||
                (tbWorkState.Text.Trim().Length > 0))
            {
                addr = new EZDeskDataLayer.Address.Models.Address();
                addr.Address1 = tbWorkStreet1.Text.Trim();
                addr.Address2 = tbWorkStreet2.Text.Trim();
                addr.City = tbWorkCity.Text.Trim();
                addr.State = tbWorkState.Text.Trim();
                addr.Zip = tbWorkZip.Text.Trim();
                addr.PersonID = person.PersonID;

                addr.AddressID = (lblWorkId.Text.Length > 0) ?
                    Convert.ToInt32(lblWorkId.Text) : -1;

                addr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Work;
                if (person.addresses == null)
                {
                    person.addresses = new List<aModels.Address>();
                }
                person.addresses.Add(addr);
            }

            if ((tbBusStreet1.Text.Trim().Length > 0) ||
                (tbBusStreet2.Text.Trim().Length > 0) ||
                (tbBusCity.Text.Trim().Length > 0) ||
                (tbBusZip.Text.Trim().Length > 0) ||
                (tbBusState.Text.Trim().Length > 0))
            {
                addr = new EZDeskDataLayer.Address.Models.Address();
                addr.Address1 = tbBusStreet1.Text.Trim();
                addr.Address2 = tbBusStreet2.Text.Trim();
                addr.City = tbBusCity.Text.Trim();
                addr.State = tbBusState.Text.Trim();
                addr.Zip = tbBusZip.Text.Trim();
                addr.PersonID = person.PersonID;

                addr.AddressID = (lblBusinessId.Text.Length > 0) ?
                    Convert.ToInt32(lblBusinessId.Text) : -1;

                addr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Business;
                if (person.addresses == null)
                {
                    person.addresses = new List<aModels.Address>();
                }
                person.addresses.Add(addr);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        private void zPullDemographics(EZDeskDataLayer.Person.Models.PersonFormGetDemographics person)
        {
            //Get the person data (name, birthday, sex...)
            person.SharedID = tbSharedID.Text.Trim();
            person.SSNO = tbSSN.Text.Trim();
            person.Prefix = tbPrefix.Text.Trim();
            person.FirstName = tbFirstName.Text.Trim();
            person.MiddleName = tbMiddleName.Text.Trim();
            person.LastName = tbLastName.Text.Trim();
            person.Suffix = tbSuffix.Text.Trim();
            person.BirthDate = dtpBirthDay.Value;
            person.Sex = cmbSex.Text;

            DataRowView dr = null;
            dr = (cmbRace.SelectedIndex > -1) ?
                (DataRowView)cmbRace.Items[cmbRace.SelectedIndex] :
                (DataRowView)cmbRace.Items[cmbRace.Items.Count - 1];
            person.RaceTypeID = Convert.ToInt32(dr["Id"].ToString());

            dr = (cmbLanguage.SelectedIndex > -1) ?
                (DataRowView)cmbLanguage.Items[cmbLanguage.SelectedIndex] :
                (DataRowView)cmbLanguage.Items[cmbLanguage.Items.Count - 1];
            person.LanguageTypeID = Convert.ToInt32(dr["Id"].ToString());

            dr = (cmbEthnicity.SelectedIndex > -1) ?
                (DataRowView)cmbEthnicity.Items[cmbEthnicity.SelectedIndex] :
                (DataRowView)cmbEthnicity.Items[cmbEthnicity.Items.Count - 1];
            person.EthnicityTypeID = Convert.ToInt32(dr["Id"].ToString());

            person.Note = "";
            person.UDF1 = "";
            person.UDF2 = "";
            person.UDF3 = "";
            person.UDF4 = "";
            person.UDF5 = "";
            person.UDF6 = "";
            person.UDF7 = "";
            person.UDF8 = "";
            person.UDF9 = "";
            person.UDF10 = "";
        }

        #endregion
        #endregion

        //=================================================================================
        // Methods for the User Page
        #region User Page

        /// <summary>
        /// Make the User page visible and fill-in if we have a personID
        /// </summary>
        private void zFillinUserPage()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinUser"));

            try
            {
                tabControl1.TabPages.Add((TabPage)mTPUser);
                zClearPage(tpUser.Controls);
                if (mPersonID > -1)
                {
                    mUser = uCtrl.GetUserDetails(mPersonID);
                    if (mUser != null)
                    {
                        tbUserName.Text = mUser.UserName;
                        cbActive.Checked = mUser.IsActive;
                        cbSendMessages.Checked = mUser.CanSendMessages;
                        cbReceiveMessages.Checked = mUser.CanRcvdSignMessages;
                        tbPassword1.Text = mUser.UserPassWord;
                        mUserID = mUser.UserSecurityID;
                    }
                }

                else
                {
                    mUserID = -1;
                    mUser = new EZDeskDataLayer.User.Models.UserDetails();
                    mUser.UserSecurityID = -1;
                    mUser.PersonID = mPerson.PersonID;
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinUser"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinUser"));
            }
        }

        /// <summary>
        /// Validate the Users page
        /// </summary>
        /// <returns></returns>
        private string zValidUser()
        {
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "zValidUser"));

            try
            {
                if (tbUserName.Text.Trim().Length == 0)
                {
                    rtn = "You must specify a user|";
                }
                if (tbPassword1.Text.Length == 0)
                {
                    rtn += "You must specify a Password|";
                }
                if (tbPassword2.Text.Length == 0)
                {
                    rtn += "You must specify a 'Confirm' Password|";
                }

                return rtn;
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zValidUser"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidUser"));
            }
        }

        /// <summary>
        /// INSERT/Update a usersecurity record
        /// </summary>
        private bool zAddUpdateUser(MySqlTransaction trans)
        {
            bool transComplete = true;

            Trace.Enter(Trace.RtnName(mModName, "zAddUpdateStaff"));

            try
            {
                if (mUserID == -1)
                {
                    transComplete = zAddUser(trans);
                }
                else
                {
                    transComplete = zUpdateUser(trans);
                }
                return transComplete;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error adding/updating " + mUserType.ToString() + "\n" + ex.Message,
                                "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transComplete = false;
                trans.Rollback();
                //TODO: Clear the screen?
                return transComplete;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddUpdateStaff"));
            }
        }

        /// <summary>
        /// UPDATE usersecurity record
        /// </summary>
        /// <param name="transaction"></param>
        private bool zUpdateUser(MySqlTransaction transaction)
        {
            try
            {
                zPullUser();
                uCtrl.SaveUserDetails(mUser, transaction);
                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool zAddUser(MySqlTransaction trans)
        {
            try
            {
                zPullUser();
                uCtrl.SaveUserDetails(mUser, trans);
                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gather the user data from the window controls and update
        /// the user object with this data.
        /// </summary>
        private void zPullUser()
        {
            mUser.UserName = tbUserName.Text;
            mUser.IsActive = cbActive.Checked;
            mUser.CanSendMessages = cbSendMessages.Checked;
            mUser.CanRcvdSignMessages = cbReceiveMessages.Checked;
            mUser.UserPassWord = tbPassword1.Text;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrls"></param>
        private void zClearPage(Control.ControlCollection ctrls)
        {
            Trace.Enter(Trace.RtnName(mModName, "zClearPage"));

            try
            {
                foreach (Control ctrl in ctrls)
                {
                    Type ctrlType = ctrl.GetType();

                    if (ctrlType == typeof(TextBox))
                    {
                        ((TextBox)ctrl).Text = "";
                    }

                    if (ctrlType == typeof(CheckBox))
                    {
                        ((CheckBox)ctrl).Checked = false;
                    }

                    if (ctrlType == typeof(DateTimePicker))
                    {
                        //((DateTimePicker)ctrl).Value = DateTime.MinValue;
                        ((DateTimePicker)ctrl).Value = ((DateTimePicker)ctrl).MinDate;
                    }

                    if (ctrlType == typeof(GroupBox))
                    {
                        zClearPage(((GroupBox)ctrl).Controls);
                    }
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zClearPage") + " failed");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zClearPage"));
            }
        }

        /// <summary>
        /// Validate the form specific to the persontype
        /// </summary>
        /// <returns></returns>
        private string zValidateForm()
        {
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "zValidateForm"));

            try
            {
                switch (mUserType)
                {
                    case UserTypes.Staff:
                        rtn += zValidDemographics();
                        rtn += zValidUser();
                        break;

                    case UserTypes.Company:
                        rtn += zValidDemographics();
                        break;

                    case UserTypes.Patient:
                        rtn += zValidDemographics();
                        break;

                    default:
                        break;
                }
                return rtn;
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zValidateForm"));
                string msg = "zValidateForm failed: " + ex.Message;
                //#Trace.ExceptionDataKeyAdd(ex.Data, "msg", msg);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidateForm"));
            }
        }

    }
}
