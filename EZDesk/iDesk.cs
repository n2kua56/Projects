using System;
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

namespace EZDesk
{
    public partial class iDesk : Form
    {
        //TODO: How to fill-in iDesk small and large items.
        //TODO: Need to be able to handle pictures
        //TODO: Go through this module... using mCommon may not need mPerson or mPersonId below.

        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        //# private EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        //# private EZDeskDataLayer.Address.AddressCtrl aCtrl = null;
        //# private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskDataLayer.Person.Models.PersonFormGetDemographics mPerson = null;
        private int mPersonId = -1;
        private MySqlConnection mConn;

        public int PersonId 
        {
            get { return mPersonId; }
            set
            {
                mPersonId = value;
                zFillinPerson();
            }
        }

        public event EventHandler PersonSelected;
        public delegate void EventHandler(frmPerson m, EZDeskDataLayer.Person.Models.PersonSelectedArguments e);

        public iDesk(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;

            this.Text = "";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// The PersonId has been set, update the iDesk demographics section to
        /// show the currently selected person.
        /// </summary>
        private void zFillinPerson()
        {
            string bd = "";
            string address = "";
            string phones = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zFillinPerson"));

            try
            {
                if (mPersonId > -1)
                {
                    mPerson = mCommon.pCtrl.GetPersonByID(mPersonId);
                    mPerson.addresses = mCommon.aCtrl.GetAddressListByPersonID(mPersonId);

                    // Diaplay the person's name.
                    lblName.Text = zBuildPersonName();

                    //Display the person's address
                    address = zBuildAddress();
                    phones = zBuildPhones();
                    if ((address.Trim().Length > 0) && (phones.Trim().Length > 0))
                    {
                        address += "\n\n";
                    }
                    address += phones;
                    lblAddress.Text = address;

                    if (mPerson.BirthDate != null)
                    {
                        bd = ((DateTime)mPerson.BirthDate).ToString("MM/dd/yyyy");
                        TimeSpan dif = (TimeSpan)(DateTime.Today - mPerson.BirthDate);
                        DateTime zeroTime = new DateTime(1, 1, 1);

                        // because we start at year 1 for the Gregorian 
                        // calendar, we must subtract a year here.
                        int years = (zeroTime + dif).Year - 1;
                        textBox1.Text = years.ToString() + " years";
                    }
                    tbDateOfBirth.Text = bd;
                    tbGender.Text = mPerson.Sex;

                    tbPersonType.Text = mPerson.PersonType.ToString();

                    if (mPerson.PicturePath.Trim().Length == 0)
                    {
                        pictureBox1.BackgroundImage = zGetGenericPic();
                    }

                    else
                    {
                        pictureBox1.BackgroundImage = zGetPatientPic();
                    }
                }

                else
                {
                    lblName.Text = "";
                    lblAddress.Text = "";
                    tbDateOfBirth.Text = "";
                    tbGender.Text = "";
                    pictureBox1.BackgroundImage = null;
                }

                // ////////////////////////////////////////////////////////// //
                // Auditing is done back in Form1 where the PersonId was set. //
                // ////////////////////////////////////////////////////////// //
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zFillinPerson failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zFillinPerson"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Image zGetPatientPic()
        {
            Image img = null;
            string picPath = "";
            string temp = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zGetPatientPic"));

            try
            {
                picPath = mCommon.eCtrl.GetProperty("ServerPath");
                temp = mCommon.eCtrl.GetProperty("PatientImagePath");
                if ((picPath.Length > 0) && (temp.Length > 0))
                {
                    picPath = Path.Combine(picPath, temp);
                    picPath = Path.Combine(picPath, mPerson.PicturePath);
                    FileInfo fi = new FileInfo(picPath);
                    if (fi.Exists)
                    {
                        img = Image.FromFile(picPath);
                    }
                    else
                    {
                        img = zGetGenericPic();
                    }
                }
                else
                {
                    img = zGetGenericPic();
                }
                return img;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zGetPatientPic failed", ex);
                throw eze;
            }

            finally 
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zGetPatientPic"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picPath"></param>
        /// <param name="temp"></param>
        private Image zGetGenericPic()
        {
            string picPath = "";
            string temp = "";
            Image img = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zGetGenericPic"));

            try
            {
                picPath = mCommon.eCtrl.GetProperty("PicturePath");
                //temp = mCommon.eCtrl.GetProperty("PatientImagePath");
                //if ((picPath.Length > 0) && (temp.Length > 0))
                //{
                    if (picPath.Trim().Length == 0)
                    {
                        picPath = zGetFilePath();
                    }
                    //#btnDropDown.Image = Image.FromFile(Path.Combine(imgPath, @"arrow.png"));
                    //picPath = Path.Combine(picPath, temp);
                    if (mPerson.Sex.ToLower() == "f") { picPath = Path.Combine(picPath, "female.jpg"); }
                    else { picPath = Path.Combine(picPath, "male.jpg"); }
                    FileInfo fi = new FileInfo(picPath);
                    if (fi.Exists)
                    {
                        img = Image.FromFile(picPath);
                    }
                //}
                return img;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zGetGenericPic failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zGetGenericPic"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetFilePath() 
        {
            string value = Application.StartupPath.Substring(Application.StartupPath.IndexOf(@"bin", System.StringComparison.Ordinal));
            value = Application.StartupPath;
            value = Path.Combine(value, "Images");
            return value;
            //return Application.StartupPath.Replace(value, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string zBuildPersonName()
        {
            string name = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zBuildPersonName"));

            try
            {
                name = mPerson.Prefix.Trim();
                if (name.Length > 0) { name += " "; }
                name += mPerson.FirstName.Trim();
                if (mPerson.FirstName.Trim().Length > 0) { name += " "; }
                name += mPerson.MiddleName.Trim();
                if (mPerson.MiddleName.Trim().Length > 0) { name += " "; }
                name += mPerson.LastName.Trim();
                if (mPerson.LastName.Trim().Length > 0) { name += " "; }
                name += mPerson.Suffix;
                return name;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zBuildPersonName failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zBuildPersonName"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zBuildPhones()
        {
            string phones = "";
            string hPhone = "";
            string wPhone = "";
            string cPhone = "";
            string fPhone = "";
            string email = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zBuildPhones"));

            try
            {
                if (mPerson.comms != null)
                {
                    foreach (EZDeskDataLayer.Communications.Models.Communication p in mPerson.comms)
                    {
                        switch (p.CommunicationType)
                        {
                            case EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone:
                                hPhone = "H: " + p.CommunicationCode;
                                break;

                            case EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone:
                                wPhone = "W: " + p.CommunicationCode;
                                break;

                            case EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.CellPhone:
                                cPhone = "C: " + p.CommunicationCode;
                                break;

                            case EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.Fax:
                                fPhone = "F: " + p.CommunicationCode;
                                break;

                            case EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.EMAIL:
                                email = p.CommunicationCode;
                                break;
                        }
                    }
                    if (hPhone.Trim().Length > 0) { phones += hPhone + "\n"; }
                    if (wPhone.Trim().Length > 0) { phones += wPhone + "\n"; }
                    if (cPhone.Trim().Length > 0) { phones += cPhone + "\n"; }
                    if (fPhone.Trim().Length > 0) { phones += fPhone + "\n"; }
                    if (email.Trim().Length > 0) { phones += email; }
                }

                return phones;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zBuildPhones failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zBuildPhones"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private string zBuildAddress()
        {
            string address = "";
            EZDeskDataLayer.Address.Models.Address addr = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName("iDesk", "zBuildAddress"));

            try
            {
                if (mPerson.addresses != null)
                {
                    foreach (EZDeskDataLayer.Address.Models.Address a in mPerson.addresses)
                    {
                        if (a.AddressType == EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home)
                        {
                            addr = a;
                            break;
                        }
                    }
                }
                if (addr == null)
                {
                    lblAddress.Text = "No home address";
                }
                else
                {
                    if (addr.Address1.Trim().Length > 0) { address += addr.Address1.Trim() + "\n"; }
                    if (addr.Address2.Trim().Length > 0) { address += addr.Address2.Trim() + "\n"; }
                    if (addr.City.Trim().Length > 0) { address += addr.City.Trim(); }
                    if ((addr.City.Trim().Length > 0) && (addr.State.Trim().Length > 0)) { address += ", "; }
                    if (addr.State.Trim().Length > 0) { address += addr.State.Trim(); }
                    if ((addr.City.Trim().Length > 0) || (addr.State.Trim().Length > 0)) { address += " "; }
                    address += addr.Zip;
                }
                return address;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zBuildAddress failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName("iDesk", "zBuildAddress"));
            }
        }

        private void iDesk_Load(object sender, EventArgs e)
        {
            lblAddress.Text = "Address";
            lblName.Text = "Name";
        }
    }
}
