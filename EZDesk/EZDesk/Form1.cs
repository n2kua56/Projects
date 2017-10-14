using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using EZUtils;
using EZDeskDataLayer;

namespace EZDesk
{
    public partial class Form1 : Form
    {
        private EZDeskCommon mCommon = null;

        private iDash frmIDash = null;
        private frmCalendar fCalendar = null;
        private frmViewRx frmRx = null;
        private frmPerson frmPer = null;
        private frmExternalProgram pfrm = null;

        private int mSplitterDistance = 0;
        private string mConnStr = "";
        private MySqlConnection mConn = null;
        private MySqlCommand mCmd = null;

        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskDataLayer.Person.PersonCtrl pCtrl = null;

        private string mModName = "Form1";

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "Form1_Load"));

            try
            {
                zSetConnectionString();
                mConn = new MySqlConnection(mConnStr);
                try
                {
                    mConn.Open();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }

                mCmd = new MySqlCommand();
                mCmd.Connection = mConn;

                eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mConn);
                pCtrl = new EZDeskDataLayer.Person.PersonCtrl(mConn);
        
                iDashLoad();
                mSplitterDistance = splitContainer2.SplitterDistance;
                
                zPersonPanelLoad();

                zSetTabs(0);

                mCommon = new EZDeskCommon();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Form1_Load failed", ex);
                Trace.WriteEventEntry(Trace.RtnName(mModName, "Form1_Load"), ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Load"));
            }
        }

        /// <summary>
        /// After the main form has been shown, require the user
        /// to login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "Form1_Shown"));

            try
            {
                frmPassword frm = new frmPassword(mConn);
                DialogResult dr = frm.ShowDialog();
                switch (dr)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        Application.Exit();
                        break;
                    case System.Windows.Forms.DialogResult.OK:
                        mCommon.User = frm.User;
                        mCommon.Staff = pCtrl.GetPersonByID(mCommon.User.PersonID);
                        this.Text = "EZDesk - " + mCommon.User.UserName;
                        break;
                    default:
                        Application.Exit();
                        break;
                }

                if (mCommon.User != null)
                {
                    zSetTabs(mCommon.User.UserSecurityID);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Form1_Shown failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Shown"));
            }
        }

        /// <summary>
        /// At startup and on returning from Setup... rebuild the
        /// list of "Tabs" as buttons in the flowLayoutPanel.
        /// </summary>
        private void zSetTabs(int userId)
        {
            Trace.Enter(Trace.RtnName(mModName, "zSetTabs"));

            try
            {
                flowLayoutPanel1.Controls.Clear();
                //TODO: Change following to new routine GetTabsForUser
                DataTable tabs = eCtrl.GetAllTabs();
                foreach (DataRow dr in tabs.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr["tabName"].ToString();
                    btn.Tag = dr["tabId"].ToString();
                    btn.BackColor = ((bool)dr["IsActive"]) ?
                        SystemColors.Control : Color.Red;
                    btn.AllowDrop = (bool)dr["IsActive"];
                    btn.Click += new System.EventHandler(this.tabButton_Click);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSetTabs failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetTabs"));
            }
        }

        /// <summary>
        /// All "Tab Buttons" in the flowLayoutPanel will get here when
        /// the user clicks on a button indicating they want to open
        /// the "Tab Folder".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabButton_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "tabButton_Click"));

            try
            {
                int tabId = Convert.ToInt32(((Button)sender).Tag.ToString());
                //TODO: bring up the tab folder form in the work area
                MessageBox.Show("This will eventually bring up the Tab Folder: " + tabId.ToString(),
                    "Info Message");
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("tabButton_Click failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tabButton_Click"));
            }
        }

        /// <summary>
        /// Load the Person Panel
        /// </summary>
        private void zPersonPanelLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "zPersonPanelLoad"));

            try
            {
                if (frmPer == null)
                {
                    frmPer = new frmPerson();
                    frmPer.TopLevel = false;
                    frmPer.Visible = true;
                    frmPer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    frmPer.ControlBox = false;
                    frmPer.Text = String.Empty;

                    frmPer.Width = pnlPeople.Width;
                    frmPer.Height = pnlPeople.Height;
                    frmPer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlPeople.Controls.Add(frmPer);
                }

                //pnlPeople.Controls["frmPer"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zPersonPanelLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zPersonPanelLoad"));
            }
        }

        /// <summary>
        /// Setup the connection string
        /// </summary>
        private void zSetConnectionString()
        {
            Trace.Enter(Trace.RtnName(mModName, "zSetConnectionString"));

            try
            {
                mConnStr = Properties.Settings.Default.connStr;
                mConnStr = mConnStr.Replace("{server}", Properties.Settings.Default.server.Trim());
                mConnStr = mConnStr.Replace("{db}", Properties.Settings.Default.db.Trim());
                mConnStr = mConnStr.Replace("{uid}", Properties.Settings.Default.uid.Trim());
                mConnStr = mConnStr.Replace("{password}", Properties.Settings.Default.password.Trim());
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSetConnectionString failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetConnectionString"));
            }
        }

        /// <summary>
        /// Load the iDash form into the hosting panel
        /// </summary>
        private void iDashLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "iDashLoad"));

            try
            {
                if (frmIDash == null)
                {
                    frmIDash = new iDash();
                    frmIDash.TopLevel = false;
                    frmIDash.Visible = true;
                    frmIDash.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    frmIDash.ControlBox = false;

                    frmIDash.Width = pnlHosting.Width;
                    frmIDash.Height = pnlHosting.Height;
                    frmIDash.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmIDash);
                }

                splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls["iDash"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("iDashLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "iDashLoad"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void frmRxLoad()
        {
            //TODO: Remove this method - remove the frmRx

            Trace.Enter(Trace.RtnName(mModName, "frmRxLoad"));

            try
            {
                if (frmRx == null)
                {
                    frmRx = new frmViewRx();
                    frmRx.TopLevel = false;
                    frmRx.Visible = true;
                    frmRx.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    frmRx.ControlBox = false;

                    frmRx.Width = pnlHosting.Width;
                    frmRx.Height = pnlHosting.Height;
                    frmRx.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmRx);
                }

                splitContainer2.Panel1Collapsed = true;
                pnlHosting.Controls["frmViewRx"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("frmRxLoad failed", ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmRxLoad"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdRx_Click(object sender, EventArgs e)
        {
            //TODO: Remove this and the button

            Trace.Enter(Trace.RtnName(mModName, "cmdRx_Click"));

            try
            {
                frmRxLoad();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("cmdRx_Click failed", ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDesktop_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdDesktop_Click"));

            try
            {
                iDashLoad();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("cmdDesktop_Click failed", ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "settingsToolStripMenuItem_Click"));

            try
            {
                frmSetup frm = new frmSetup(mConn);
                DialogResult dr = frm.ShowDialog();
                zSetTabs(mCommon.User.UserSecurityID);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("settingsToolStripMenuItem_Click failed", ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "settingsToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdForms_Click(object sender, EventArgs e)
        {
            string formFileName = "";
            string calcFileName = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "cmdForms_Click"));

            try
            {
                step = "Build and call Forms dialog";
                frmForms frm = new frmForms(mConn);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    step = "Get the FormFileName from the Forms dialog";
                    formFileName = frm.FormFileName;
                }
                frm.Dispose();

                if (formFileName.Length > 0)
                {
                    step = "Get the OpenOffice calc program file name";
                    calcFileName = eCtrl.GetProperty("FormsProgram");
                    if (calcFileName.Length > 0)
                    {
                        if (pfrm == null)
                        {
                            step = "Bring up the form panel";
                            pfrm = new frmExternalProgram();

                            pfrm.TopLevel = false;
                            pfrm.Visible = true;
                            pfrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                            pfrm.ControlBox = false;
                            pfrm.Width = pnlHosting.Width;
                            pfrm.Height = pnlHosting.Height;
                            pfrm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                            pnlHosting.Controls.Add(pfrm);
                        }
                        pnlHosting.Controls["frmExternalProgram"].BringToFront();

                        step = "Tell frmExternalProgram to run the calc program for the selected form.";
                        pfrm.StartExternal(calcFileName, formFileName);
                    }
                    else
                    {
                        MessageBox.Show("You must initialize the 'FormsProgram' property", "Configuration Error");
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("cmdForms_Click failed", ex);
                eze.Add("step", step);
                eze.Add("formFileName", formFileName);
                eze.Add("calcFileName", calcFileName);

                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdForms_Click"));
            }
        }

        private void tsbToDo_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "tsbToDo_Click"));

            try
            {
                ToDo.frmToDo frm = new ToDo.frmToDo(mConn);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("To Do List failed to open\n" +
                                ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tsbToDo_Click"));
            }
        }

        /// <summary>
        /// Load the iDash form into the hosting panel
        /// </summary>
        private void CalendarLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "CalendarLoad"));

            try
            {
                if (fCalendar == null)
                {
                    fCalendar = new frmCalendar();
                    fCalendar.TopLevel = false;
                    fCalendar.Visible = true;
                    fCalendar.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    fCalendar.ControlBox = false;
                    fCalendar.ShowIcon = false;

                    fCalendar.Width = pnlHosting.Width;
                    fCalendar.Height = pnlHosting.Height;
                    fCalendar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(fCalendar);
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls["frmCalendar"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("CalendarLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "CalendarLoad"));
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CalendarLoad();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            iDashLoad();
        }

        private void testCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestNewCalendar frm = new frmTestNewCalendar();
            frm.ShowDialog();
        }

        private void cmdCalendar_Click(object sender, EventArgs e)
        {
            CalendarLoad();
        }

    }
}
