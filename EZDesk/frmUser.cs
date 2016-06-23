using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.User;
using EZUtils;
//using System.Diagnostics;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;
using pModels = EZDeskDataLayer.Person.Models;

namespace EZDesk
{
    //TODO: Get rid of void zUpdateDemographics(MySqlTransaction transaction)

    public partial class frmUser : Form
    {
        private int mPersonID = -1;
        private int mUserID = -1;
        private UserTypes mUserType = UserTypes.Undefined;
        private object mTPDemographics = null;
        private object mTPUser = null;
        private string mModName = "frmUser";
        private EZDeskCommon mCommon;
        private EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        private EZDeskDataLayer.User.Models.UserDetails mItem = null;

        private int mLeft;
        private int mTop;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        /// <param name="typ"></param>
        public frmUser(EZDeskCommon common, UserTypes typ, int personid, int left, int top, int width, int height)
        {
            InitializeComponent();
            panel1.AllowDrop = true;
            mPersonID = personid;
            mUserType = typ;
            mCommon = common;
            pCtrl = new EZDeskDataLayer.Person.PersonCtrl(mCommon);
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "frmUser constructor"));

            try
            {
                step = "Center";
                
                //mLeft = left + ((width - this.Width) / 2);
                //mTop = top + ((height - this.Height) / 2);
                mLeft = 0;
                mTop = 0;
                mCommon.SetFormLeftTop(left, top, width, height, this.Width, this.Height, out mLeft, out mTop);

                if (typ == UserTypes.Company)
                {
                    tbPrefix.Visible = false;
                    tbPrefix.Enabled = false;
                    label1.Visible = false;

                    tbMiddleName.Visible = false;
                    tbMiddleName.Enabled = false;
                    label3.Visible = false;

                    tbLastName.Visible = false;
                    tbLastName.Enabled = false;
                    label4.Visible = false;

                    tbSuffix.Visible = false;
                    tbSuffix.Enabled = false;
                    label5.Visible = false;

                    tbFirstName.Width = (tbSuffix.Left + tbSuffix.Width) - tbFirstName.Left;
                    label2.Text = "Company Name";

                    dtpBirthDay.Enabled = false;
                    dtpBirthDay.Visible = false;
                    label6.Visible = false;

                    tbSSN.Enabled = false;
                    tbSSN.Visible = false;
                    label7.Visible = false;

                    cmbSex.Enabled = false;
                    cmbSex.Visible = false;
                    label8.Visible = false;

                    cbDecline.Enabled = false;
                    cbDecline.Visible = false;

                    cmbRace.Enabled = false;
                    cmbRace.Visible = false;
                    label31.Visible = false;

                    cmbRace.Enabled = false;
                    cmbRace.Visible = false;
                    label32.Visible = false;
                }
                else
                {
                    tbPrefix.Visible = true;
                    tbPrefix.Enabled = true;
                    label1.Visible = true;

                    tbMiddleName.Visible = true;
                    tbMiddleName.Enabled = true;
                    label3.Visible = true;

                    tbLastName.Visible = true;
                    tbLastName.Enabled = true;
                    label4.Visible = true;

                    tbSuffix.Visible = true;
                    tbSuffix.Enabled = true;
                    label5.Visible = true;

                    tbFirstName.Width = tbSharedID.Width;
                    label2.Text = "First Name";

                    dtpBirthDay.Enabled = true;
                    dtpBirthDay.Visible = true;
                    label6.Visible = true;

                    tbSSN.Enabled = true;
                    tbSSN.Visible = true;
                    label7.Visible = true;

                    cmbSex.Enabled = true;
                    cmbSex.Visible = true;
                    label8.Visible = true;

                    cbDecline.Enabled = true;
                    cbDecline.Visible = true;

                    cmbRace.Enabled = true;
                    cmbRace.Visible = true;
                    label31.Visible = true;

                    cmbRace.Enabled = true;
                    cmbRace.Visible = true;
                    label32.Visible = true;
                }

                step = "Fill comboboxes";
                zFillinLanguage();
                zFillinRace();
                zFillinEthnicity();

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
                EZException eze = new EZException("frmUser failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmUser constructor"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_Shown(object sender, EventArgs e)
        {
            this.Left = mLeft;
            this.Top = mTop;
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
                    //case UserTypes.Doctor:
                    //case UserTypes.ClinicalStaff:
                    //    step = "Fill-in Doctor/ClinicalStaff";
                    //    zFillinDemographics();
                    //    zFillinUser();
                    //    break;

                    case UserTypes.Staff:
                        step = "Fill-in Staff";
                        zFillinDemographics();
                        zFillinUser();
                        break;

                    case UserTypes.Company:
                        step = "Fill-in Company";
                        zFillinDemographics();
                        break;

                    //case UserTypes.ExtCarePerson:
                    //    step = "Fill-in ExtCarePerson";
                    //    zFillinDemographics();
                    //    break;

                    //case UserTypes.InsuranceGuarentor:
                    //    step = "Fill-in InsuranceGuarentor";
                    //    zFillinDemographics();
                    //    break;

                    //case UserTypes.LabDoctor:
                    //    step = "Fill-in LabDoctor";
                    //    zFillinDemographics();
                    //    break;

                    //case UserTypes.LabFacility:
                    //    step = "Fill-in LabFacility";
                    //    zFillinDemographics();
                    //    break;

                    //case UserTypes.ReferringDoctor:
                    //    step = "Fill-in Referring Doctor";
                    //    zFillinDemographics();
                    //    break;

                    case UserTypes.Patient:
                        step = "Fill-in Patient";
                        zFillinDemographics();
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillInForm failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillInForm"));
            }
        }

        //=================================================================================
        // Methods for the User Page
        #region Demographics Page

        /// <summary>
        /// Make the Demographics page visible and fill-in if we have
        /// a personid to get for.
        /// </summary>
        private void zFillinDemographics()
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
                    EZDeskDataLayer.Person.Models.PersonFormGetDemographics person = null;
                    person = mCommon.pCtrl.GetPersonByID(mPersonID);

                    if (person != null)
                    {
                        step = "Fill-in controls";
                        tbSharedID.Text = person.SharedID;
                        tbSSN.Text = person.SSNO;
                        tbPersonID.Text = person.PersonID.ToString();
                        tbPrefix.Text = person.Prefix;
                        tbFirstName.Text = person.FirstName;
                        tbMiddleName.Text = person.MiddleName;
                        tbLastName.Text = person.LastName;
                        dtpBirthDay.Value = (person.BirthDate != null) ?
                                (DateTime)person.BirthDate : DateTime.MinValue;

                        string key = person.Sex;
                        int idx = cmbSex.Items.IndexOf(key);
                        if (idx > -1)
                        {
                            cmbSex.SelectedIndex = idx;
                        }

                        int raceIdx = -1;
                        for (int rIdx = 0; ((rIdx < cmbRace.Items.Count) && (raceIdx < 0)); rIdx++)
                        {
                            DataRowView dr = (DataRowView)cmbRace.Items[rIdx];
                            if (person.RaceTypeID == Convert.ToInt32(dr["Id"].ToString()))
                            {
                                raceIdx = rIdx;
                            }
                        }
                        if (raceIdx < 0)
                        {
                            raceIdx = 0;
                        }
                        cmbRace.SelectedIndex = raceIdx;

                        int EthnicityIdx = -1;
                        for (int rIdx = 0; ((rIdx < cmbEthnicity.Items.Count) && (EthnicityIdx < 0)); rIdx++)
                        {
                            DataRowView dr = (DataRowView)cmbEthnicity.Items[rIdx];
                            if (person.EthnicityTypeID == Convert.ToInt32(dr["Id"].ToString()))
                            {
                                EthnicityIdx = rIdx;
                            }
                        }
                        if (EthnicityIdx < 0)
                        {
                            EthnicityIdx = 0;
                        }
                        cmbEthnicity.SelectedIndex = EthnicityIdx;

                        cmbLanguage.SelectedIndex = zSetLanguage(person.LanguageTypeID);

                        step = "zFillInAddress";
                        zFillInAddress(person.addresses);
                        step = "zFillinCommunication";
                        zFillinCommunication(person.comms);
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinDemographics failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinDemographics"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private int zSetLanguage(int personLanguageIdx)
        {
            int LanguageIdx = -1;
            int rIdx = -1;

            Trace.Enter(Trace.RtnName(mModName, "zSetLanguage"));

            try
            {

                for (rIdx = 0; ((rIdx < cmbLanguage.Items.Count) && (LanguageIdx < 0)); rIdx++)
                {
                    DataRowView dr = (DataRowView)cmbLanguage.Items[rIdx];
                    if (personLanguageIdx == Convert.ToInt32(dr["Id"].ToString()))
                    {
                        LanguageIdx = rIdx;
                        string temp = dr["Language"].ToString();
                    }
                }
                if (LanguageIdx < 0)
                {
                    LanguageIdx = 0;
                }
                return LanguageIdx;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSetLanguage failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetLanguage"));
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
                lblCellRec.Text = "";
                lblEmailRec.Text = "";
                lblFaxRec.Text = "";
                lblHomeRec.Text = "";
                lblWorkRec.Text = "";
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
                                lblHomeRec.Text = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone):
                                step = "Fill-in Work Phone";
                                flds = com.CommunicationCode.Split('x');
                                if (flds.Length > 0) { tbWork.Text = flds[0]; }
                                if (flds.Length > 1) { tbWorkExt.Text = flds[1]; }
                                lblWorkRec.Text = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.CellPhone):
                                step = "Fill-in Cell Phone";
                                tbCell.Text = com.CommunicationCode;
                                lblCellRec.Text = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.Fax):
                                step = "Fill-in Fax";
                                tbFax.Text = com.CommunicationCode;
                                lblFaxRec.Text = com.CommunicationID.ToString();
                                break;

                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.EMAIL):
                                step = "Fill-in EMail";
                                tbEmail.Text = com.CommunicationCode;
                                lblEmailRec.Text = com.CommunicationID.ToString();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinCommunication failed", ex);
                eze.Add("step", step);
                throw eze;
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
                lblRec1.Text = "";
                lblRec2.Text = "";
                lblRec3.Text = "";
                tbZip.Text = "";
                
                if (addrs != null)
                {
                    foreach (EZDeskDataLayer.Address.Models.Address addr in addrs)
                    {
                        if (addr.AddressType == EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home)
                        {
                            step = "Fill-in Home address";
                            tbAddress1.Text = addr.Address1;
                            tbAddress2.Text = addr.Address2;
                            tbCity.Text = addr.City;
                            tbState.Text = addr.State;
                            tbZip.Text = addr.Zip;
                            lblRec1.Text = addr.AddressID.ToString();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinAddress failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillInAddress"));
            }
        }

        /// <summary>
        /// Fill-in the Language combo box on the demographics page
        /// </summary>
        private void zFillinLanguage()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinLanguage"));

            try
            {
                EZDeskDataLayer.LanguageList.LanguageController ctrl =
                    new EZDeskDataLayer.LanguageList.LanguageController(mCommon.Connection);

                DataTable LanguageTable = ctrl.GetLanguageList();
                cmbLanguage.DataSource = LanguageTable;
                cmbLanguage.DisplayMember = "Language";
                cmbLanguage.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinLanguage failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinLanguage"));
            }
        }

        /// <summary>
        /// Fill-in the Race combo box on the Demographics page
        /// </summary>
        private void zFillinRace()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinRace"));

            try
            {
                EZDeskDataLayer.RaceList.RaceController ctrl =
                        new EZDeskDataLayer.RaceList.RaceController(mCommon.Connection);

                DataTable RaceTable = ctrl.GetRaceList();
                cmbRace.DataSource = RaceTable;
                cmbRace.DisplayMember = "Race";
                cmbRace.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinRace failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinRace"));
            }
        }

        /// <summary>
        /// Fill-in the Ethnicity combo box on the Demographics page
        /// </summary>
        private void zFillinEthnicity()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinEthnicity"));

            try
            {
                EZDeskDataLayer.EthnicityList.EthnicityController ctrl =
                    new EZDeskDataLayer.EthnicityList.EthnicityController(mCommon.Connection);

                DataTable EthnicityTable = ctrl.GetEthnicityList();
                cmbEthnicity.DataSource = EthnicityTable;
                cmbEthnicity.DisplayMember = "Ethnicity";
                cmbEthnicity.ValueMember = "ID";
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinEthnicity failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinEthnicity"));
            }
        }

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
                EZException eze = new EZException("zValidDemographics failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidDemographics"));
            }
        }

        /// <summary>
        /// INSERT a demographic entry (Person).
        /// </summary>
        private int zAddDemographics(MySqlTransaction transaction)
        {
            EZDeskDataLayer.Person.Models.PersonFormGetDemographics person = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zAddDemographics"));

            try
            {
                step = "Pull demographic data from controls";
                person = zPullDemographics();
                person.PersonID = -1;

                step = "Update person";
                mCommon.pCtrl.UpDatePerson(person, transaction);
                return person.PersonID;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zAddDemographics failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddDemographics"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics zPullDemographics()
        {
            string step = "";
            EZDeskDataLayer.Person.Models.PersonFormGetDemographics person = null;

            Trace.Enter(Trace.RtnName(mModName, "zPullDemographics"));

            try
            {
                step = "Pull personal data";
                person = zCollectPersonData();
                person.PersonID = mPersonID;

                // Add the address if any.
                step = "Pull address data from controls";
                person.addresses = zCollectAddresses(person.PersonID);

                // Add the phone numbers.
                step = "Pull communications data from controls";
                person.comms = zCollectPhoneNumbers(person.PersonID);

                return person;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zPullDemographics failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zPullDemographics"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<EZDeskDataLayer.Communications.Models.Communication> zCollectPhoneNumbers(int id)
        {
            int recid = -1;
            List<EZDeskDataLayer.Communications.Models.Communication> comms = 
                new List<EZDeskDataLayer.Communications.Models.Communication>();

            Trace.Enter(Trace.RtnName(mModName, "zCollectPhoneNumbers"));

            try
            {
                if (tbHome.Text.Trim().Length > 0)
                {
                    recid = (lblHomeRec.Text.Length > 0) ? Convert.ToInt32(lblHomeRec.Text) : -1;
                    comms.Add(zBuildCommunicationObject(tbHome.Text, tbHomeExt.Text, id, recid,
                        EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone));
                }

                if (tbFax.Text.Trim().Length > 0)
                {
                    recid = (lblFaxRec.Text.Length > 0) ? Convert.ToInt32(lblFaxRec.Text) : -1;
                    comms.Add(zBuildCommunicationObject(tbFax.Text, "", id, recid,
                        EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.Fax));
                }

                if (tbWork.Text.Trim().Length > 0)
                {
                    recid = (lblWorkRec.Text.Length > 0) ? Convert.ToInt32(lblWorkRec.Text) : -1;
                    comms.Add(zBuildCommunicationObject(tbWork.Text, tbWorkExt.Text, id, recid,
                        EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone));
                }

                if (tbCell.Text.Trim().Length > 0)
                {
                    recid = (lblCellRec.Text.Length > 0) ? Convert.ToInt32(lblCellRec.Text) : -1;
                    comms.Add(zBuildCommunicationObject(tbCell.Text, "", id, recid,
                        EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.CellPhone));
                }

                if (tbEmail.Text.Trim().Length > 0)
                {
                    recid = (lblEmailRec.Text.Length > 0) ? Convert.ToInt32(lblEmailRec.Text) : -1;
                    comms.Add(zBuildCommunicationObject(tbEmail.Text, "", id, recid,
                        EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.EMAIL));
                }

                return comms;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zCollectPhoneNumbers failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCollectPhoneNumbers"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="ext"></param>
        /// <param name="commType"></param>
        /// <returns></returns>
        private EZDeskDataLayer.Communications.Models.Communication zBuildCommunicationObject(
                string number, string ext, int id, int recid,
                EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum commType)
        {
            string temp = "";
            EZDeskDataLayer.Communications.Models.Communication commObj =
                new EZDeskDataLayer.Communications.Models.Communication();

            Trace.Enter(Trace.RtnName(mModName, "zBuildCommunicationObject"));

            try
            {
                temp = number.Trim();
                if (ext.Trim().Length > 0)
                {
                    temp += "x" + ext.Trim();
                }

                commObj.CommunicationCode = temp;
                commObj.CommunicationType = commType;
                commObj.PersonID = id;
                commObj.CommunicationID = recid;
                return commObj;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zBuildCommunicationObject failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zBuildCommunicationObject"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        private List<EZDeskDataLayer.Address.Models.Address> zCollectAddresses(int id)
        {
            EZDeskDataLayer.Address.Models.Address addr = null;
            List<EZDeskDataLayer.Address.Models.Address> addresses =
                       new List<EZDeskDataLayer.Address.Models.Address>();
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zCollectAddresses"));

            try
            {
                if ((tbAddress1.Text.Trim().Length > 0) ||
                    (tbAddress2.Text.Trim().Length > 0) ||
                    (tbCity.Text.Trim().Length > 0) ||
                    (tbZip.Text.Trim().Length > 0) ||
                    (tbState.Text.Trim().Length > 0))
                {
                    step = "Home Address";
                    addr = new EZDeskDataLayer.Address.Models.Address();
                    addr.PersonID = id;
                    if (lblRec1.Text.Length > 0)
                    {
                        addr.AddressID = Convert.ToInt32(lblRec1.Text);
                    }
                    else
                    {
                        addr.AddressID = -1;
                    }
                    addr.Address1 = tbAddress1.Text.Trim();
                    addr.Address2 = tbAddress2.Text.Trim();
                    addr.City = tbCity.Text.Trim();
                    addr.State = tbState.Text.Trim();
                    addr.Zip = tbZip.Text.Trim();
                    addr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home;

                    addresses.Add(addr);
                }

                return addresses;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zCollectAddresses failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCollectAddresses"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics zCollectPersonData()
        {
            string temp = "";
            DataRow dr = null;

            Trace.Enter(Trace.RtnName(mModName, "zCollectPersonData"));

            try
            {
                EZDeskDataLayer.Person.Models.PersonFormGetDemographics person =
                    new EZDeskDataLayer.Person.Models.PersonFormGetDemographics();

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

                dr = ((DataTable)cmbRace.DataSource).Rows[cmbRace.SelectedIndex];
                person.RaceTypeID = Convert.ToInt32(dr["Id"].ToString());

                dr = ((DataTable)cmbLanguage.DataSource).Rows[cmbLanguage.SelectedIndex];
                person.LanguageTypeID = Convert.ToInt32(dr["Id"].ToString());

                dr = ((DataTable)cmbEthnicity.DataSource).Rows[cmbEthnicity.SelectedIndex];
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
                temp = mUserType.ToString();
                person.PersonType = (pModels.PersonFormGetDemographics.PersonTypeEnum)mCommon.pCtrl.GetPersonTypeByName(temp);
                return person;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zCollectPersonData failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCollectPersonData"));
            }
        }

        /// <summary>
        /// UPDATE a demographic entry (Person).
        /// </summary>
        private void zUpdateDemographics(MySqlTransaction transaction)
        {
            EZDeskDataLayer.Person.Models.PersonFormGetDemographics person = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zUpdateDemographics"));

            try
            {
                step = "Pull demographic data from controls";
                person = zPullDemographics();
                person.PersonID = mPersonID;

                step = "Update person";
                mCommon.pCtrl.UpDatePerson(person, transaction);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zUpdateDemographics failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zUpdateDemographics"));
            }
        }

        #endregion

        //=================================================================================
        // Methods for the User Page
        #region User Page

        /// <summary>
        /// Make the User page visible and fill-in if we have a personID
        /// </summary>
        private void zFillinUser()
        {
            //#EZDeskDataLayer.User.UserController uCtrl =
            //#        new UserController(mCommon.Connection);

            Trace.Enter(Trace.RtnName(mModName, "zFillinUser"));

            try
            {
                tabControl1.TabPages.Add((TabPage)mTPUser);
                zClearPage(tpUser.Controls);
                if (mPersonID > -1)
                {
                    mItem = mCommon.uCtrl.GetUserDetails(mPersonID);
                    if (mItem != null)
                    {
                        tbPassword1.Text = mItem.UserPassWord;
                        tbPassword2.Text = mItem.UserPassWord;
                        tbUserName.Text = mItem.UserName;
                        cbActive.Checked = mItem.IsActive;
                        cbSendMessages.Checked = mItem.CanSendMessages;
                        tbDirectEmail.Text = mItem.DirectEmail;
                        mUserID = mItem.UserSecurityID;
                        tbMailUserName.Text = mItem.MailUserName;
                        tbMailPassword.Text = mItem.MailPassword;
                        tbUDF1.Text = mItem.UDF1;
                        tbUDF2.Text = mItem.UDF2;
                        tbUDF3.Text = mItem.UDF3;
                        tbUDF4.Text = mItem.UDF4;
                        tbUDF5.Text = mItem.UDF5;
                        tbUDF6.Text = mItem.UDF6;
                        tbUDF7.Text = mItem.UDF7;
                        tbUDF8.Text = mItem.UDF8;
                        tbUDF9.Text = mItem.UDF9;
                        tbUDF10.Text = mItem.UDF10;
                    }
                }

                else
                {
                    mUserID = -1;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFillinUser failed", ex);
                throw eze;
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
                EZException eze = new EZException("zValidUser failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidUser"));
            }
        }

        /// <summary>
        /// INSERT a usersecurity record
        /// </summary>
        private void zAddUser(MySqlTransaction transaction)
        {
            mCommon.uCtrl.SaveUserDetails(mItem, transaction);
        }

        /// <summary>
        /// UPDATE usersecurity record
        /// </summary>
        private void zUpdateUser(MySqlTransaction transaction)
        {
            mCommon.uCtrl.SaveUserDetails(mItem, transaction);
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
                EZException eze = new EZException("zClearPage failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zClearPage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string msg = "";

            Trace.Enter(Trace.RtnName(mModName, "cmdSave_Click"));

            try
            {
                msg = zValidateForm();
                if (msg.Length == 0)
                {
                    bool transComplete = true;
                    //Start a transaction
                    MySqlTransaction trans;
                    trans = mCommon.Connection.BeginTransaction();

                    switch (mUserType)
                    {
                        //case UserTypes.Doctor:
                        //case UserTypes.ClinicalStaff:
                        case UserTypes.Staff:
                            transComplete = zAddUpdateStaff(trans);
                            break;

                        case UserTypes.Company:
                        //case UserTypes.LabFacility:
                            transComplete = zAddUpdateCompany(trans);
                            break;

                        //case UserTypes.ExtCarePerson:
                        //case UserTypes.InsuranceGuarentor:
                        //case UserTypes.LabDoctor:
                        //case UserTypes.ReferringDoctor:
                        case UserTypes.Patient:
                            transComplete = zAddUpdatePerson(trans);
                            break;

                        default:
                            trans.Rollback();
                            transComplete = false;
                            break;
                    }

                    if (transComplete)
                    {
                        trans.Commit();
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
                EZException eze = new EZException("cmdSave_Click failed", ex);
                msg = "cmdSave_Click failed: " + ex.Message;
                eze.Add("msg", msg);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdSave_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zBuildName()
        {
            string rtn = "";
            if (tbLastName.Text.Trim().Length > 0) { rtn += tbLastName.Text.Trim(); }
            if (((tbFirstName.Text.Trim().Length > 0) || (tbMiddleName.Text.Trim().Length > 0)) && (rtn.Length > 0))
            { rtn += ", "; }
            if (tbFirstName.Text.Trim().Length > 0) { rtn += tbFirstName.Text.Trim(); }
            if ((tbFirstName.Text.Trim().Length > 0) && (tbMiddleName.Text.Trim().Length > 0)) { rtn += " "; }
            if (tbMiddleName.Text.Trim().Length > 0) { rtn += tbMiddleName.Text.Trim(); }
            return rtn;
        }

        /// <summary>
        /// Add or Update a Patient
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool zAddUpdatePerson(MySqlTransaction trans)
        {
            bool transComplete = true;
            EZDeskDataLayer.ehr.Models.AuditItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "zAddUpdatePerson"));

            try
            {
                if (mPersonID == -1)
                {
                    mPersonID = zAddDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Add, 
                                "Person: " + zBuildName());
                }
                else
                {
                    zUpdateDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Edit,
                                "Person: " + zBuildName());
                }
                mCommon.eCtrl.WriteAuditRecord(item);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error adding/updating " + mUserType.ToString() + "\n" + ex.Message,
                                "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                transComplete = false;
                trans.Rollback();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddUpdatePerson"));
            }

            return transComplete;
        }

        /// <summary>
        /// Add or Update Company
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool zAddUpdateCompany(MySqlTransaction trans)
        {
            bool transComplete = true;
            EZDeskDataLayer.ehr.Models.AuditItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "zAddUpdateCompany"));

            try
            {
                if (mPersonID == -1)
                {
                    mPersonID = zAddDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.Company,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Add,
                                "Company: " + zBuildName());
                }
                else
                {
                    zUpdateDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.Company,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Edit,
                                "Company: " + zBuildName());
                }
                mCommon.eCtrl.WriteAuditRecord(item);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Error adding/updating " + mUserType.ToString(), ex);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Error adding/updating");
                transComplete = false;
                trans.Rollback();
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddUpdateCompany"));
            }

            return transComplete;
        }

        /// <summary>
        /// Add or Update Staff (non-clinical)
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool zAddUpdateStaff(MySqlTransaction trans)
        {
            bool transComplete = true;
            EZDeskDataLayer.ehr.Models.AuditItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "zAddUpdateStaff"));

            try
            {
                if (mPersonID == -1)
                {
                    mPersonID = zAddDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.User,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Add,
                                "Staff: " + zBuildName());
                }
                else
                {
                    zUpdateDemographics(trans);
                    item = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                                mPersonID,
                                EZDeskDataLayer.ehr.Models.AuditAreas.User,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Edit,
                                "Staff: " + zBuildName());
                }
                mCommon.eCtrl.WriteAuditRecord(item);

                if (mUserID == -1)
                {
                    zAddUser(trans);
                }
                else
                {
                    zUpdateUser(trans);
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
                Trace.Exit(Trace.RtnName(mModName, "zAddUpdateStaff"));
            }

            return transComplete;
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
                    //case UserTypes.Doctor:
                    //case UserTypes.ClinicalStaff:
                    //    rtn += zValidDemographics();
                    //    rtn += zValidUser();
                    //    ////rtn += zValidRegistry();  //???
                    //    break;

                    case UserTypes.Staff:
                        rtn += zValidDemographics();
                        rtn += zValidUser();
                        break;

                    case UserTypes.Company:
                        rtn += zValidDemographics();
                        break;

                    //case UserTypes.ExtCarePerson:
                    //    rtn += zValidDemographics();
                    //    break;

                    //case UserTypes.InsuranceGuarentor:
                    //    rtn += zValidDemographics();
                    //    break;

                    //case UserTypes.LabDoctor:
                    //    rtn += zValidDemographics();
                    //    break;

                    //case UserTypes.LabFacility:
                    //    rtn += zValidDemographics();
                    //    break;

                    //case UserTypes.ReferringDoctor:
                    //    rtn += zValidDemographics();
                    //    break;

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
                EZException eze = new EZException("zValidateForm failed", ex);
                string msg = "zValidateForm failed: " + ex.Message;
                eze.Add("msg", msg);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zValidateForm"));
            }
        }

        private bool mDontChange = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSex_SelectedIndexChanged(object sender, EventArgs e)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            mDontChange = true;
            cbDecline.Checked = false;
            mDontChange = false;
        }

        string log = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            int i = 1;
            log += "panel1_DragEnter\n";

            if ((e.Data.GetDataPresent(DataFormats.Bitmap)) || (e.Data.GetDataPresent(DataFormats.FileDrop)))
            {
                log += "panel1_DragEnter - Copy\n";
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                log += "panel1_DragEnter - NONE\n";
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string step = "";
            string fileName = "";
            string temp = "";

            Trace.Enter(Trace.RtnName(mModName, "panel1_DragDrop"));

            try
            {
                step = "Build filename";
                fileName = mCommon.eCtrl.GetProperty("ServerPath");
                temp = mCommon.eCtrl.GetProperty("PatientImagePath");
                if ((fileName.Length > 0) && (fileName.Length > 0))
                {
                    fileName = Path.Combine(fileName, temp);
                    fileName = Path.Combine(fileName, mCommon.Person.PersonID.ToString());
                }

                if (e.Data.GetDataPresent(DataFormats.Bitmap))
                {
                    step = "  panel1_DragDrop Bitmap\n";
                    //TODO: copy the image to the picturebox
                    //TODO: Write the image data to a file in the right place
                    //TODO: Fill-in the picture path
                }

                else if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    step = "  panel1_DragDrop FileDrop\n";

                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                    if (files.Length != 1)
                    {
                        MessageBox.Show("You may only drag one file here", "Drag Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {
                            step = "Load and Save image file";
                            pictureBox1.BackgroundImage = Image.FromFile(files[0]);
                            FileInfo fi = new FileInfo(files[0]);
                            fileName += "." + fi.Extension;
                            pictureBox1.BackgroundImage.Save(fileName);
                            mCommon.Person.PicturePath = fileName;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("There was an error assigning or saving the image. " + ex.Message,
                                        "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("panel1_DragDrop failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "panel1_DragDrop"));
            }
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            int i = 1;
            log += "pictureBox1_DragEnter\n";
            if ((e.Data.GetDataPresent(DataFormats.Bitmap)) || (e.Data.GetDataPresent(DataFormats.FileDrop)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            int i = 1;
            log += "panel1_DragDrop\n";
        }

        private void label42_DoubleClick(object sender, EventArgs e)
        {
            int i = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbUserName_Leave(object sender, EventArgs e)
        {
            string value = "";

            value = ((TextBox)sender).Text.Trim();

            if (sender == tbUserName) { mItem.UserName = value; }
            if (sender == tbPassword1) { mItem.UserPassWord = value; }
            if (sender == tbDirectEmail) { mItem.DirectEmail = value; }
            if (sender == tbMailUserName) { mItem.MailUserName = value; }
            if (sender == tbMailPassword) { mItem.MailPassword = value; }
            if (sender == tbUDF1) { mItem.UDF1 = value; }
            if (sender == tbUDF2) { mItem.UDF2 = value; }
            if (sender == tbUDF3) { mItem.UDF3 = value; }
            if (sender == tbUDF4) { mItem.UDF4 = value; }
            if (sender == tbUDF5) { mItem.UDF5 = value; }
            if (sender == tbUDF6) { mItem.UDF6 = value; }
            if (sender == tbUDF7) { mItem.UDF7 = value; }
            if (sender == tbUDF8) { mItem.UDF8 = value; }
            if (sender == tbUDF9) { mItem.UDF9 = value; }
            if (sender == tbUDF10) { mItem.UDF10 = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSendMessages_CheckedChanged(object sender, EventArgs e)
        {
            tbMailPassword.Enabled = cbSendMessages.Checked;
            tbMailUserName.Enabled = cbSendMessages.Checked;
            mItem.CanSendMessages = cbSendMessages.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbActive_CheckedChanged(object sender, EventArgs e)
        {
            mItem.IsActive = cbActive.Checked;
        }

    }
}
