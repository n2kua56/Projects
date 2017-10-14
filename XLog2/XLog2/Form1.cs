using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.Xml;

namespace XLog2
{
    //TODO: 1) Modify LogEditor
    //TODO: 2) Implement ARRL 10meter contest
    //TODO: 3) Menu item for database backup
    //TODO: 4) Problems with the QSL boolean field.
    //TODO: 5) UNKNOWN1 and UNKNOWN2 titles
    //TODO: 6) XLog2 view Modes/Bands comboboxes if specified
    // --------------------------------------------
    //TODO: 7) DXCC List form (Awards?)

    public partial class Form1 : Form
    {
        private string mQRZKey = "";
        private string mView = "";                              //Might not need this anymore

        internal string ProgramName = "XLog2";
        internal string mHamLogLogName = "";
        internal int m_QSONumber = 0;                           //QSO number being edited OR 0 for new
        internal DataAccess mDac = null;                        //Datalayer routines

        private frmViewHamLog HamLogView = null;
        private frmViewXLog2 Xlog2View = null;
        private frmContestARRL10M ARRL10m = null;

        internal frmWorkedBefore WorkedBefore = null;
        internal Size HamLogViewSize = new Size(0, 0);
        internal Size XLog2ViewSize = new Size(0, 0);
        internal Size ARRL10mSize = new Size(0, 0);

        public Form1()
        {
            InitializeComponent();
            foreach (Control c in pnlViewHost.Controls)
            {
                pnlViewHost.Controls.Remove(c);
            }
        }

        #region FormEvents

        //////////////////////////////////////////////////////////////////////////////////
        // Form Events.                                                                 //
        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            string logName = "";
            Form viewForm = null;

            mDac = new DataAccess(this);
            if (1 != mDac.ConnectToHamLog())
            {
                Application.DoEvents();
                MessageBox.Show("Could not connect to the HamLog database.");
                Application.Exit();
            }

            zSetStatusMessage("Starting");

            zFixPnlViewHost();

            mHamLogLogName = zGetLogName();

            DataGridView dgv = null;
            mView = mDac.GetProperty("XLOG.LastView");
            switch (mView)
            {
                case "frmViewXLog2":
                    zShowXLog2(mHamLogLogName);
                    xLog2ToolStripMenuItem1.Enabled = false;
                    hamLogToolStripMenuItem1.Enabled = true;
                    aRRL10MContestToolStripMenuItem.Enabled = true;
                    toolStripMenuItem8.Enabled = true;
                    viewForm = zFindViewForm("frmViewXLog2");
                    if (viewForm != null)
                    {
                        ((frmViewXLog2)viewForm).btnSave.Text = "Save";
                        ((frmViewXLog2)viewForm).dtpQSODate.Focus();
                    }
                    break;

                case "frmViewHamLog":
                    zShowHamLog(mHamLogLogName);
                    xLog2ToolStripMenuItem1.Enabled = true;
                    hamLogToolStripMenuItem1.Enabled = false;
                    aRRL10MContestToolStripMenuItem.Enabled = true;
                    toolStripMenuItem8.Enabled = false;
                    viewForm = zFindViewForm("frmViewHamLog");
                    if (viewForm != null)
                    {
                        ((frmViewHamLog)viewForm).btnHRAddUpdate.Text = "Save";
                        ((frmViewHamLog)viewForm).tbHRCall.Focus();
                    }
                    break;

                case "frmContestARRL10M":
                    zShowARRL10Meter(mHamLogLogName);
                    xLog2ToolStripMenuItem1.Enabled = true;
                    hamLogToolStripMenuItem1.Enabled = true;
                    aRRL10MContestToolStripMenuItem.Enabled = false;
                    toolStripMenuItem8.Enabled = false;
                    viewForm = zFindViewForm("frmContestARRL10M");
                    if (viewForm != null)
                    {
                        ((frmContestARRL10M)viewForm).btnSaveUpdate.Text = "Save";
                        ((frmContestARRL10M)viewForm).tbCall.Focus();
                    }
                    break;

                default:
                    break;
            }
            
            toolStripStatusLabel1.Text = "Ready";
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Form zFindViewForm(string name)
        {
            Form frm = null;
            foreach (Control c in pnlViewHost.Controls)
            {
                if (((Form)c).Name.ToLower() == name.ToLower())
                {
                    frm =(Form)c;
                }
            }
            return frm;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFixPnlViewHost()
        {
            pnlViewHost.Top = 25;
            pnlViewHost.Left = 0;
            pnlViewHost.Width = this.Width;
            pnlViewHost.Height = this.Height - (25 + 64);
            pnlViewHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void maintenanceStripMenuItem12_Click(object sender, EventArgs e)
        {
            int i = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetLogName()
        {
            string logName = "";
            DialogResult dr = DialogResult.None;

            zSetStatusMessage("Selecting Log");
            logName = mDac.GetProperty("XLOG2.LastLogViewed");
            if ((logName.Trim().Length == 0) || (logName.Trim().ToUpper() == "TABPAGE1"))
            {
                frmSelectLog frm = new frmSelectLog(this, false);
                dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    logName = frm.mSelectedLog;
                }
            }

            zSetStatusMessage("Starting");
            return logName;
        }

        /// <summary>
        /// Load the ComboBox controls.
        /// </summary>
        /// <remarks>
        /// to be filed in
        /// </remarks>
        private void zLoadComboBoxes()
        {
        }

        #endregion

        #region Menu Events

        //////////////////////////////////////////////////////////////////////////////////
        // Menu Events.                                                                 //
        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The user wants to view the "Defaults" setup dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////frmDefaults form = new XLog2.frmDefaults(this, tabControl1.SelectedTab.Text);
            //form.ShowDialog();
        }

        /// <summary>
        /// The user wants to view the "Dialogs" setup dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dialogsWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDialogs form = new XLog2.frmDialogs(this);
            form.ShowDialog();
        }

        /// <summary>
        /// The user wants to view the "Preferences" setup dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences form = new XLog2.frmPreferences(this);
            form.ShowDialog();
        }

        /// <summary>
        /// Open another log tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zOpenLog("");
        }

        /// <summary>
        /// 
        /// </summary>
        private void zOpenLog(string openLogName)
        {
            string msg = "";
            string logName = "";
            DataGridView dgv = null;
            RadioButton rb100 = null;
            RadioButton rb500 = null;
            RadioButton rbAll = null;
            CheckBox dateRange = null;
            DateTimePicker startDate = null;
            DateTimePicker endDate = null;
            int cont = 1;

            toolStripStatusLabel1.Text = "Selecting Log";
            Application.DoEvents();

            logName = openLogName;
            if (logName.Length == 0)
            {
                frmSelectLog frm = new XLog2.frmSelectLog(this, true);
                DialogResult dr = frm.ShowDialog();
                if (dr != DialogResult.OK)
                {
                     cont = 0;
                }
                else
                {
                    logName = frm.mSelectedLog;
                }
                frm.Dispose();
            }

            if (cont == 1)
            { 
                toolStripStatusLabel1.Text = "Opening Log";
                Application.DoEvents();
                
                switch (mView)
                {
                    //If the current view is XLog2, then either select the tab page
                    // for the selected log file or create a new tab page for the
                    // log file.
                    case "frmViewXLog2":
                        //Find the XLog2 form in the controls of pnlViewHost
                        frmViewXLog2 XLog2Frm = null;
                        foreach (Control c in pnlViewHost.Controls)
                        {
                            if (c.Name == "frmViewXLog2")
                            {
                                XLog2Frm = (frmViewXLog2)c;
                            }
                        }

                        //Try to find the tab page for this log file
                        int found = 0;
                        foreach (TabPage p in XLog2Frm.tabControl1.TabPages)
                        {
                            if (p.Text == logName)
                            {
                                found = 1;
                                XLog2Frm.tabControl1.SelectTab(p);
                                msg = "Selected tab. ";
                            }
                        }

                        //If the tab page for this log was not found, then add a new tab page.
                        if (found == 0)
                        {
                            TabPage page = new TabPage();
                            page.Text = logName;
                            DataGridView dataGridView99 = new DataGridView();
                            dataGridView99.Name = "dataGridView" + logName;
                            page.Controls.Add(dataGridView99);
                            dataGridView99.Top = 0;
                            dataGridView99.Left = 0;
                            dataGridView99.Width = page.Width - 2;
                            dataGridView99.Height = page.Height - 2;
                            dataGridView99.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top 
                                | System.Windows.Forms.AnchorStyles.Bottom)
                                | System.Windows.Forms.AnchorStyles.Left)
                                | System.Windows.Forms.AnchorStyles.Right)));
                            XLog2Frm.tabControl1.TabPages.Add(page);

                            XLog2Frm.tabControl1.SelectTab(page);
                            XLog2Frm.BuildQSOEntryForm(logName);
                        }

                        //And finally display the Log in the grid, and set the Save button to "Save".
                        dgv = (DataGridView)XLog2Frm.tabControl1.SelectedTab.Controls[0];
                        rb100 = XLog2Frm.rb100;
                        rb500 = XLog2Frm.rb500;
                        rbAll = XLog2Frm.rbAll;
                        dateRange = XLog2Frm.cbDateRange;
                        startDate = XLog2Frm.dateTimePicker1;
                        endDate = XLog2Frm.dateTimePicker2;
                        FillLogGrid(dgv, rb100.Checked, rb500.Checked, rbAll.Checked,
                                    dateRange.Checked, startDate.Value, endDate.Value,
                                    logName);
                        XLog2Frm.btnSave.Text = "Save";

                        break;

                    //If the current view is HamLog, just refresh the log file grid.
                    case "frmViewHamLog":
                        frmViewHamLog HamLogFrm = null;
                        foreach(Control c in pnlViewHost.Controls)
                        {
                            if (c.Name == "frmViewHamLog")
                            {
                                HamLogFrm = (frmViewHamLog)c;
                            }
                        }
                        dgv = HamLogFrm.dataGridView2;
                        rb100 = HamLogFrm.rbHR100;
                        rb500 = HamLogFrm.rbHR500;
                        rbAll = HamLogFrm.rbHRAll;
                        dateRange = HamLogFrm.cbHRDateRange;
                        startDate = HamLogFrm.dtpHRStartDate;
                        endDate = HamLogFrm.dtpHRStopDate;
                        FillLogGrid(dgv, rb100.Checked, rb500.Checked, rbAll.Checked,
                                    dateRange.Checked, startDate.Value, endDate.Value,
                                    logName);
                        HamLogFrm.lblHRTitle.Text = "Recent Contacts - " + mHamLogLogName;
                        HamLogFrm.lblHRCount.Text =
                                HamLogFrm.dataGridView2.RowCount.ToString() + " Listed";
                        HamLogFrm.btnHRAddUpdate.Text = "Save";
                        break;

                    case "frmContestARRL10M":
                        //TO DO: Load the file in the ContestARRL10M form
                        break;

                    default:
                        break;
                }
                mDac.SaveProperty("XLOG2.LastLogViewed", logName);
                mHamLogLogName = logName;
                this.Text = ProgramName + " - " + mHamLogLogName;
            }
            toolStripStatusLabel1.Text = msg + "Ready";
            Application.DoEvents();
        }

        /// <summary>
        /// Edit the log layout for the currently selected log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = mHamLogLogName;

            frmLogEditor frm = new XLog2.frmLogEditor(logName, this);

            frm.cbUTCend.Checked = (0 != mDac.GetLogFields(logName, "DateEnd"));
            frm.cbTX.Checked = (0 != mDac.GetLogFields(logName, "TX"));
            frm.cbRX.Checked = (0 != mDac.GetLogFields(logName, "RX"));
            frm.cbAwards.Checked = (0 != mDac.GetLogFields(logName, "Awards"));
            frm.cbQslOut.Checked = (0 != mDac.GetLogFields(logName, "QSLOut"));
            frm.cbQslIn.Checked = (0 != mDac.GetLogFields(logName, "QSLIn"));
            frm.cbPower.Checked = (0 != mDac.GetLogFields(logName, "Power"));
            frm.cbName.Checked = (0 != mDac.GetLogFields(logName, "Name"));
            frm.cbQTH.Checked = (0 != mDac.GetLogFields(logName, "QTH"));
            frm.cbLocator.Checked = (0 != mDac.GetLogFields(logName, "Locator"));

            frm.cbUnknown1.Checked = (0 != mDac.GetLogFields(logName, "UNKNOWN1"));
            frm.cbUnknown2.Checked = (0 != mDac.GetLogFields(logName, "UNKNOWN2"));
            frm.tbUnknown1.Text = mDac.GetDefault(mHamLogLogName, "Unknown1Label");
            frm.tbUnknown2.Text = mDac.GetDefault(mHamLogLogName, "Unknown2Label");

            frm.cbRemarks.Checked = (0 != mDac.GetLogFields(logName, "Remarks"));
            frm.cbCountry.Checked = (0 != mDac.GetLogFields(logName, "Country"));
            frm.cbState.Checked = (0 != mDac.GetLogFields(logName, "State"));
            frm.cbCounty.Checked = (0 != mDac.GetLogFields(logName, "County"));

            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                mDac.ClearLogFields(logName);

                zSaveLogFields(logName, frm.cbLogName.Checked, frm.cbQSONumber.Checked, frm.cbDate.Checked,
                            frm.cbUTC.Checked, frm.cbUTCend.Checked, frm.cbCall.Checked, frm.cbFrequency.Checked,
                            frm.cbMode.Checked, frm.cbTX.Checked, frm.cbRX.Checked, frm.cbAwards.Checked,
                            frm.cbQslOut.Checked, frm.cbQslIn.Checked, frm.cbPower.Checked,
                            frm.cbName.Checked, frm.cbQTH.Checked, frm.cbLocator.Checked,
                            frm.cbUnknown1.Checked, frm.tbUnknown1.Text.Trim(), frm.cbUnknown2.Checked,
                            frm.tbUnknown2.Text.Trim(), frm.cbRemarks.Checked, frm.cbCountry.Checked,
                            frm.cbState.Checked, frm.cbCounty.Checked);
            }
        }

        /// <summary>
        /// The user has selected the File->New log option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logFileName = "";

            frmNewLog frm = new frmNewLog(this);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                logFileName = frm.tbLogName.Text.Trim();
                if (1 == mDac.SaveLogName(logFileName))
                {
                    zSaveLogFields(logFileName, frm.logName, frm.QSONumber, frm.Date, frm.UTC, frm.UTCend,
                                frm.Call, frm.Frequency, frm.Mode, frm.TX, frm.RX, frm.Awards, frm.QslOut,
                                frm.QslIn, frm.Power, frm.contactName, frm.QTH, frm.Locator, frm.Unknown1,
                                frm.Unknown1Name, frm.Unknown2, frm.Unknown2Name, frm.Remarks, frm.Country,
                                frm.State, frm.County);
                }
                zOpenLog(frm.tbLogName.Text.Trim());
            }
            frm.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countryMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrefixMap frm = new XLog2.frmPrefixMap();
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: DELAYED Implement Keyer
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scoringWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: DELAYED Implement Scoring
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workedBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WorkedBefore == null)
            {
                WorkedBefore = new frmWorkedBefore(this);
                WorkedBefore.Show();
            }
            if (!WorkedBefore.IsAccessible)
            {
                WorkedBefore.Activate();
            }
        }

        /// <summary>
        /// frmWorkedBefore has been closed.
        /// </summary>
        internal void WorkedBeforeClosing()
        {
           //// WorkedBefore = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: DELAYED Implement Awards
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExport frm = new frmExport(this, mHamLogLogName);
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Implement Import
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Implement Page Setup
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Implement Print
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// The user selected the File->Exit optin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ((frmViewXLog2)pnlViewHost.Controls["frmViewXLog2"]).RemoveCurrentTab();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bugsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            frmBrowser frm = new frmBrowser(String.Format(@"file:\\{0}\documentation\Bugs.html", curDir), ProgramName + " - Bugs");
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            frmBrowser frm = new frmBrowser(String.Format(@"file:\\{0}\documentation\ChangeLog.html", curDir), ProgramName + " - Change Log");
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void todoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            frmBrowser frm = new frmBrowser(String.Format(@"file:\\{0}\documentation\Todo.html", curDir), ProgramName + " - To Do");
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            frmBrowser frm = new frmBrowser(String.Format(@"file:\\{0}\documentation\XLog2 Manual.html", curDir), ProgramName + " - Manual");
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            frmUSMap frm = new XLog2.frmUSMap();
            frm.Show();
        }

        #endregion

        #region QRZ

        /// <summary>
        /// Send a request to the QRZ Server and return the 
        /// XML Document response
        /// </summary>
        /// <param name="req">The URL request, usually logon or call sign lookup</param>
        /// <returns>XML Document returned by QRZ or null</returns>
        private XmlDocument zGetQRZ(string req)
        {
            string httpRtn = "";
            XmlDocument rtn = null;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(req);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                httpRtn = sr.ReadToEnd();
                if (httpRtn.Length > 0)
                {
                    rtn = new XmlDocument();
                    rtn.LoadXml(httpRtn);
                }
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callSign"></param>
        private XmlDocument zQRZlookup(string callSign)
        {
            XmlDocument res = null;

            //Do we already have a QRZ Session?
            if (mQRZKey.Length == 0)
            {
                //No Session, sign-on.
                string user = mDac.GetProperty("XLOG.Callsign");
                string pw = mDac.GetProperty("XLOG.QRZPassword");
                string req = "http://xmldata.qrz.com/xml/?username=" +
                                user + ";password=" + pw;
                res = zGetQRZ(req);

                //Did we get a response from QRZ?
                if (res != null)
                {
                    //Extract the Session Key
                    XmlNodeList keys = res.GetElementsByTagName("Key");
                    if (keys.Count > 0)
                    {
                        mQRZKey = keys[0].InnerText;
                    }

                    else
                    {
                        MessageBox.Show("The KEY tag was not found in the QRZ response",
                                        ProgramName + " - QRZ Error");
                        res = null;
                    }
                }

                //NO RESPONSE FROM QRZ
                else
                {
                    MessageBox.Show("There was no response from QRZ", ProgramName + " - QRZ Error");
                    res = null;
                }
            }

            //Do we have a Session Key?
            if (mQRZKey.Length > 0)
            {
                string qry = "http://xmldata.qrz.com/xml/current/?s=" +
                    mQRZKey + ";callsign=" + callSign;
                res = zGetQRZ(qry);
                if (res != null)
                {
                    string err = zGetNodeText(res, "Error");
                    if (err.Trim().Length != 0)
                    {
                        MessageBox.Show(err, ProgramName + " Error");
                        res = null;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private string zGetNodeText(XmlDocument doc, string nodeName)
        {
            string ret = "";

            XmlNodeList keys = doc.GetElementsByTagName(nodeName);
            if (keys.Count > 0)
            {
                ret = keys[0].InnerText;
            }
            return ret;
        }
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        public void ShowMessage(string msg, string title)
        {
            MessageBox.Show(msg, title);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        internal void zCenterDialog(Form box)
        {
            if (box.Height > this.Height)
            {
                box.Top = this.Top - ((box.Height - this.Height) / 2);
            }
            else
            {
                box.Top = this.Top + ((this.Height - box.Height) / 2);
            }

            if (box.Width > this.Width)
            {
                box.Left = this.Left - ((box.Width - this.Width) / 2);
            }
            else
            {
                box.Left = this.Left + ((this.Width - box.Width) / 2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1(this);
            frm.ShowDialog();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileName"></param>
        /// <param name="logName"></param>
        /// <param name="QSONumber"></param>
        /// <param name="Date"></param>
        /// <param name="UTC"></param>
        /// <param name="UTCend"></param>
        /// <param name="Call"></param>
        /// <param name="Frequency"></param>
        /// <param name="Mode"></param>
        /// <param name="TX"></param>
        /// <param name="RX"></param>
        /// <param name="Awards"></param>
        /// <param name="QslOut"></param>
        /// <param name="QslIn"></param>
        /// <param name="Power"></param>
        /// <param name="contactName"></param>
        /// <param name="QTH"></param>
        /// <param name="Locator"></param>
        /// <param name="Unknown1"></param>
        /// <param name="Unknown1Name"></param>
        /// <param name="Unknown2"></param>
        /// <param name="Unknown2Name"></param>
        /// <param name="Remarks"></param>
        private void zSaveLogFields(string logFileName, bool logName, bool QSONumber, bool Date,
                        bool UTC, bool UTCend, bool Call, bool Frequency, bool Mode,
                        bool TX, bool RX, bool Awards, bool QslOut, bool QslIn, bool Power,
                        bool contactName, bool QTH, bool Locator, bool Unknown1,
                        string Unknown1Name, bool Unknown2, string Unknown2Name, bool Remarks,
                        bool Country, bool State, bool County)
        {
            int logFileID = mDac.zGetLogFileID(logFileName);
            if (logFileID > 0)
            {
                if (logName) { mDac.SaveLogFields(logFileID, "Logname"); }
                if (QSONumber) { mDac.SaveLogFields(logFileID, "QSO Number"); }
                if (Date) { mDac.SaveLogFields(logFileID, "Date"); }
                if (UTC) { mDac.SaveLogFields(logFileID, "UTC"); }
                if (UTCend) { mDac.SaveLogFields(logFileID, "DateEnd"); }
                if (Call) { mDac.SaveLogFields(logFileID, "Call"); }

                if (Frequency) { mDac.SaveLogFields(logFileID, "Frequency"); }
                if (Mode) { mDac.SaveLogFields(logFileID, "Mode"); }
                if (TX) { mDac.SaveLogFields(logFileID, "TX"); }
                if (RX) { mDac.SaveLogFields(logFileID, "RX"); }
                if (Awards) { mDac.SaveLogFields(logFileID, "Awards"); }
                if (QslOut) { mDac.SaveLogFields(logFileID, "QSLOut"); }
                if (QslIn) { mDac.SaveLogFields(logFileID, "QSLIn"); }
                if (Power) { mDac.SaveLogFields(logFileID, "Power"); }
                if (contactName) { mDac.SaveLogFields(logFileID, "Name"); }
                if (QTH) { mDac.SaveLogFields(logFileID, "QTH"); }
                if (Locator) { mDac.SaveLogFields(logFileID, "Locator"); }

                if (Unknown1) { mDac.SaveLogFields(logFileID, "UNKNOWN1"); }
                string unk1Name = "UNKNOWN1";
                if (Unknown1Name.Trim().Length > 0) { unk1Name = Unknown1Name.Trim(); }
                mDac.AddUpdateDefault(logFileName, "Unknown1Label", unk1Name.Trim());

                if (Unknown2) { mDac.SaveLogFields(logFileID, "UNKNOWN2"); }
                string unk2Name = "UNKNOWN2";
                if (Unknown1Name.Trim().Length > 0) { unk1Name = Unknown1Name.Trim(); }
                mDac.AddUpdateDefault(logFileName, "Unknown2Label", unk2Name.Trim());

                if (Remarks) { mDac.SaveLogFields(logFileID, "Remarks"); }
                if (Country) { mDac.SaveLogFields(logFileID, "Country"); }
                if (State) { mDac.SaveLogFields(logFileID, "State"); }
                if (County) { mDac.SaveLogFields(logFileID, "County"); }
            }
        }

        /// <summary>
        /// The XLog2 main form is closing, make sure the connection to
        /// MySQL is closed.
        /// </summary>
        /// <remarks>
        /// Too late to worry about errors. Just close the MySQL connection.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mDac.IsConnectedToHamLog() == 1)
            {
                mDac.DisConnectFromHamLog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="fldName"></param>
        /// <param name="gridColumnName"></param>
        /// <param name="width"></param>
        private void zFormatColumnHeader(DataGridView dgv, string logName, string fldName, string gridColumnName, int width)
        {
            switch (mView)
            {
                case "XLog":
                    width = (mDac.GetLogFields(logName, fldName) > 0) ? width : 0;
                    break;
                default:
                    break;
            }
            dgv.Columns[gridColumnName].Width = width;
            if (width == 0) { dgv.Columns[gridColumnName].Visible = false; }
        }
        
        /// <summary>
        /// This routine will set the status message in the lower
        /// left corner of the form.  It also calls a DoEvent() to
        /// make sure the message is displayed.
        /// </summary>
        /// <remarks>
        /// This routine is too simple and difficult to see a failure 
        /// that no exception handler is included.  The calling
        /// function may or may not handle any exception that could
        /// possibly show up, but difficult to imagine any exception
        /// could be thrown.
        /// </remarks>
        /// <param name="msg"></param>
        private void zSetStatusMessage(string msg)
        {
            toolStripStatusLabel1.Text = msg;
            Application.DoEvents();
        }

        private void toolStripBackupDatabase_Click(object sender, EventArgs e)
        {
            //cls
            //echo off
            //set mydate =% date:~10,4 %% date:~4,2 %% date:~7,2 %
            //set captime =% TIME %
            //set h =% captime:~0,2 %
            //set m =% captime:~3,2 %
            //set mytime =% h %% m %
            //set dumpfile = backup.hamradio.% mydate %.% mytime %.sql
            //echo % dumpfile %
            //"C:\Program Files (x86)\MySQL\MySQL Server 5.7\bin\mysqldump.exe" - u root - ppassword hamradio >% dumpfile %
        }

        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////
        //                                                                           //
        //                     New QSO Form Interface routines.                      //
        //                                                                           //                                     
        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////

        #region QSOFormInterface

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hamLogViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zShowHamLog(mHamLogLogName);
            xLog2ToolStripMenuItem1.Enabled = true;
            hamLogToolStripMenuItem1.Enabled = false;
            aRRL10MContestToolStripMenuItem.Enabled = true;
            toolStripMenuItem8.Enabled = false;
            Form viewForm = zFindViewForm("frmViewHamLog");
            if (viewForm != null)
            {
                if (((frmViewHamLog)viewForm).btnHRAddUpdate.Text == "Add/Update")
                {
                    ((frmViewHamLog)viewForm).btnHRAddUpdate.Text = "Add";
                    ((frmViewHamLog)viewForm).tbHRCall.Focus();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        private void zShowHamLog(string logName)
        {
            if (HamLogView == null)
            {
                HamLogView = new frmViewHamLog(this);
            }
            zSwitchView2(HamLogView, HamLogViewSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xLog2ViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zShowXLog2(mHamLogLogName);
            xLog2ToolStripMenuItem1.Enabled = false;
            hamLogToolStripMenuItem1.Enabled = true;
            aRRL10MContestToolStripMenuItem.Enabled = true;
            toolStripMenuItem8.Enabled = true;
        }
        private void zShowXLog2(string logName)
        {
            if (Xlog2View == null)
            {
                Xlog2View = new frmViewXLog2(this);
            }
            zSwitchView2(Xlog2View, XLog2ViewSize);
            Form viewForm = zFindViewForm("frmXLog2");
            if (viewForm != null)
            {
                ((frmViewXLog2)viewForm).btnDate.Focus();
                if (((frmViewXLog2)viewForm).btnSave.Text == "Save/Update")
                {
                    ((frmViewXLog2)viewForm).btnSave.Text = "Save";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aRRL10MeterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zShowARRL10Meter(mHamLogLogName);
            xLog2ToolStripMenuItem1.Enabled = true;
            hamLogToolStripMenuItem1.Enabled = true;
            aRRL10MContestToolStripMenuItem.Enabled = false;
            toolStripMenuItem8.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        private void zShowARRL10Meter(string logName)
        {
            if (ARRL10m == null)
            {
                ARRL10m = new frmContestARRL10M(this);
            }
            zSwitchView2(ARRL10m, ARRL10mSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myForm"></param>
        /// <param name="myFormSize"></param>
        private void zSwitchView2(Form myForm, Size myFormSize)
        {
            int loadedAlready = 0;

            // Location: 0, 25
            // Size: 200, 374
            // Form Size: 1007, 463

            //First lets cleanup the pnlViewHost, remove any control
            //that is not a form.  'Hide' all forms.
            foreach (Control foundControl in pnlViewHost.Controls)
            {
                Type abc = foundControl.GetType();
                switch (abc.Name)
                {
                    case "frmViewHamLog":
                    case "frmViewXLog2":
                    case "frmContestARRL10M":
                        foundControl.Enabled = false;
                        foundControl.Visible = false;
                        if (abc.Name == myForm.Name)
                        {
                            loadedAlready = 1;
                        }
                        break;

                    default:
                        foundControl.Dispose();
                        break;
                }
            }

            myForm.TopLevel = false;
            myForm.AutoScroll = true;

            //Now adjust the size of the form to accomadate the embeded form size.
            if ((pnlViewHost.Height < myForm.Height) || (pnlViewHost.Width < myForm.Width))
            {
                this.Width = myForm.Width;
                this.Height = myForm.Height + (25 + 64);
            }

            if (loadedAlready == 0)
            {
                pnlViewHost.Controls.Add(myForm);
            }

            myForm.Top = 0;
            myForm.Left = 0;
            myForm.Height = pnlViewHost.Height;
            myForm.Width = pnlViewHost.Width - 20;
            myForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
           | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right)));

            myForm.Enabled = true;
            myForm.Visible = true;
            myForm.Show();
            mView = myForm.Name;
            mDac.SaveProperty("XLOG.LastView", mView);

            pnlViewHost.Enabled = true;
            pnlViewHost.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="rb100"></param>
        /// <param name="rb500"></param>
        /// <param name="rbAll"></param>
        /// <param name="dateRange"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="logname"></param>
        internal void FillLogGrid(DataGridView dgv, bool rb100, bool rb500, bool rbAll,
                                    bool dateRange, DateTime startDate, DateTime endDate,
                                    string view, string logname = "")
        {
            int count = 0;
            string logName = logname;

            if (rb100) { count = 100; }
            if (rb500) { count = 500; }
            if (logName == "") { logName = mHamLogLogName; }
            if (dgv != null)
            {
                dgv.DataSource = mDac.GetQSOs(logName, count);
                ///<FMEA>If MySQL has an error, the DataSource will be null. Issue an error message and continue.</FMEA>
                if (dgv.DataSource == null)
                {
                    MessageBox.Show("There was an error reading the log.", ProgramName + " System Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dgv.Columns["ID"].Width = 0;
                    dgv.Columns["ID"].Visible = false;
                    dgv.Columns["Number"].Width = 50;

                    FormatLogGrid(dgv, logName, view);
                }
            }
            this.Text = "XLog2 - " + logName;
            mHamLogLogName = logName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        internal void FormatLogGrid(DataGridView dgv, string logName, string view)
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.ScrollBars = ScrollBars.Both;

            dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 =
                    new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;

            dgv.ColumnHeadersHeightSizeMode =
                    System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            ////dgv.CellDoubleClick +=
            ////            new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            ////dgv.MouseDown +=
            ////            new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            ////string logName = tabControl1.SelectedTab.Text;

            zFormatColumnHeader(dgv, logName, "Date", "StartDate", 100);
            zFormatColumnHeader(dgv, logName, "DateEnd", "EndDate", 100);
            zFormatColumnHeader(dgv, logName, "Call", "Call", 80);
            zFormatColumnHeader(dgv, logName, "Frequency", "Frequency", 60);
            zFormatColumnHeader(dgv, logName, "Mode", "Mode", 40);
            zFormatColumnHeader(dgv, logName, "TX", "TXrst", 40);
            zFormatColumnHeader(dgv, logName, "RX", "RXrst", 40);
            zFormatColumnHeader(dgv, logName, "QSLOut", "QSLOut", 50);
            zFormatColumnHeader(dgv, logName, "QSLIn", "QSLIn", 50);
            zFormatColumnHeader(dgv, logName, "Power", "Power", 40);
            zFormatColumnHeader(dgv, logName, "Name", "Name", 90);
            zFormatColumnHeader(dgv, logName, "Remarks", "Remarks", 300);
            dgv.Columns["BandID"].Width = 0;
            dgv.Columns["BandID"].Visible = false;
            dgv.Columns["ModeID"].Width = 0;
            dgv.Columns["ModeID"].Visible = false;

            switch (view)
            {
                case "XLog2":
                    ////dgv.Top = 1;
                    ////dgv.Left = 2;
                    ////dgv.Width = tabControl1.Width - 12;             //Not HamLog view
                    ////dgv.Height = tabControl1.Height - 27;           //Not HamLog view
                    ////dgv.Anchor =
                    ////     (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
                    zFormatColumnHeader(dgv, logName, "UNKNOWN1", "UNKNOWN1", 120); //Other in HamLog
                    zFormatColumnHeader(dgv, logName, "Awards", "Awards", 50);      //not in HamLog
                    zFormatColumnHeader(dgv, logName, "QTH", "QTH", 100);           //not in HamLog
                    zFormatColumnHeader(dgv, logName, "Locator", "Locator", 55);    //not in HamLog
                    zFormatColumnHeader(dgv, logName, "UNKNOWN2", "UNKNOWN2", 120); //not in HamLog
                    break;

                case "HamLog":
                    zFormatColumnHeader(dgv, logName, "OTHER", "UNKNOWN1", 120); //Other in HamLog
                    dgv.Columns["UNKNOWN1"].HeaderText = "Other";
                    dgv.Columns["Locator"].Width = 0;
                    dgv.Columns["Locator"].Visible = false;
                    dgv.Columns["QTH"].Width = 0;
                    dgv.Columns["QTH"].Visible = false;
                    dgv.Columns["UNKNOWN2"].Width = 0;
                    dgv.Columns["UNKNOWN2"].Visible = false;
                    break;

                default:
                    break;
            }

            //These are hidden in all views
            List<String> hideCols = new List<String> { "UNKNOWN1Label", "UNKNOWN2Label", "CountryCode", "StateCode", "CountyCode" };
            foreach (string name in hideCols)
            {
                dgv.Columns[name].Width = 0;
                dgv.Columns[name].Visible = false;
            }
        }

        /// <summary>
        /// If we are using the Worked before form, and if we have at 
        /// lease 3 characters, search the log for possible worked before
        /// situations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CallTextChanged(string call)
        {
            if (call.Length > 2)
            {
                if (WorkedBefore != null)
                {
                    WorkedBefore.CallSignChanged(call);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void AddUpdate(QSO qso)
        {

            if ((qso.LogName == "ALL") && (qso.LogID == 0))
            {
                MessageBox.Show("While in the 'ALL' log you may only UPDATE", ProgramName + " Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            else
            {
                ///< FMEA > The user may not have filled in required filds Band | Frequency and Call. 
                /// Action Issue a message and continue without saving.</ FMEA >
                if ((qso.Call.Trim().Length == 0) ||
                    ((qso.BandID == -1) && (qso.Frequency.Trim().Length == 0)))
                {
                    MessageBox.Show("You MUST fill-in CALL and Band or Frequency", ProgramName + " Input Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    ///<FMEA>The user can set the TimeOff to be prior than the Start Time. 
                    ///Action check and offer to stop the save or continue with OffTime = StartTime.</FMEA>
                    int cont = 1;
                    if (qso.StartDate >= qso.EndDate)
                    {
                        DialogResult dr = MessageBox.Show("The 'Off' time is less than the start time.\r\n" +
                                                            "Record without the 'Off' time?",
                                            ProgramName + " Input Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.No) { cont = 0; }
                        else { qso.EndDate = qso.StartDate; }
                    }

                    if (cont == 1)
                    {
                        //Now insert the QSO - Not all fields are written in the insert

                        if (qso.LogID == 0)
                        {
                            qso.LogID = mDac.AddQSO(qso.StartDate, qso.Call, qso.Frequency, qso.Mode, qso.LogName);
                        }
                        else
                        {
                            mDac.UpdateQSOTextField(qso.LogID, "Call", qso.Call);
                            mDac.UpdateQSOTextField(qso.LogID, "Frequency", qso.Frequency);
                            mDac.UpdateQSOTextField(qso.LogID, "Mode", qso.Mode);
                            mDac.UpdateQSOTextField(qso.LogID, "LogName", qso.LogName);
                        }

                        //Now update the QSO record and add the fields that are not inserted.
                        if (qso.BandID != -1) { mDac.UpdateQSOIntField(qso.LogID, "BandID", qso.BandID); }
                        if (qso.ModeID != -1) { mDac.UpdateQSOIntField(qso.LogID, "ModeID", qso.ModeID); }
                        if (qso.Mode != null) { mDac.UpdateQSOTextField(qso.LogID, "Mode", qso.Mode); }
                        if (qso.Power != null) { mDac.UpdateQSOTextField(qso.LogID, "Power", qso.Power); }
                        if (qso.TxRST != null) { mDac.UpdateQSOTextField(qso.LogID, "TXrst", qso.TxRST); }
                        if (qso.RxRST != null) { mDac.UpdateQSOTextField(qso.LogID, "RXrst", qso.RxRST); }
                        if (qso.CountryCode != -1) { mDac.UpdateQSOIntField(qso.LogID, "CountryCode", qso.CountryCode); }   //Might not work
                        if (qso.StateCode != -1) { mDac.UpdateQSOIntField(qso.LogID, "StateCode", qso.StateCode); }         //Might not work
                        if (qso.CountyCode != -1) { mDac.UpdateQSOIntField(qso.LogID, "CountyCode", qso.CountyCode); }      //Might not work
                        if (qso.Name != null) { mDac.UpdateQSOTextField(qso.LogID, "Name", qso.Name); }
                        if (qso.EndDate != DateTime.MinValue) { mDac.UpdateQSODateField(qso.LogID, "EndDate", qso.EndDate); }
                        mDac.UpdateQSOBoolField(qso.LogID, "QSLout", qso.QSLout);
                        mDac.UpdateQSOBoolField(qso.LogID, "QSLin", qso.QSLin);
                        if (qso.Remarks != null) { mDac.UpdateQSOTextField(qso.LogID, "Remarks", qso.Remarks); }
                        if (qso.Unknown1 != null) { mDac.UpdateQSOTextField(qso.LogID, "UNKNOWN1", qso.Unknown1); }
                        if (qso.Unknown2 != null) { mDac.UpdateQSOTextField(qso.LogID, "UNKNOWN1", qso.Unknown2); }
                    }
                }
            }
        }

        /// <summary>
        /// Fill-in the State ComboBox 
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="countryCode"></param>
        internal void FillStateComboBox(ComboBox cb, int countryCode)
        {
            try
            {
                zSetStatusMessage("Loading States");
                DataTable tbl = mDac.LoadStates(countryCode);
                cb.DataSource = tbl;
                cb.DisplayMember = "State";
                cb.ValueMember = "ID";
                cb.SelectedIndex = 0;
                cb.Enabled = ((tbl != null) && (tbl.Rows.Count > 0));
                zSetStatusMessage("Ready");
            }
            catch (Exception ex)
            {
                mDac.WriteStack(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        internal void FillModeComboBox(ComboBox cb)
        {
            zSetStatusMessage("Loading Modes");
            cb.DataSource = mDac.LoadModes();
            cb.DisplayMember = "Mode";
            cb.ValueMember = "ID";
            cb.SelectedIndex = 0;
            zSetStatusMessage("Ready");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        internal void FillBandComboBox(ComboBox cb)
        {
            zSetStatusMessage("Loading Bands");
            cb.DataSource = mDac.LoadBands();
            cb.DisplayMember = "Band";
            cb.ValueMember = "ID";
            cb.SelectedIndex = 0;
            zSetStatusMessage("Ready");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        internal void FillCountriesComboBox(ComboBox cb)
        {
            zSetStatusMessage("Loading Countries");
            cb.DataSource = mDac.LoadCountries();
            cb.DisplayMember = "Country";
            cb.ValueMember = "EntityCode";
            cb.SelectedIndex = 0;
            zSetStatusMessage("Ready");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="Statecode"></param>
        internal void FillCountyComboBox(ComboBox cb, int Statecode)
        {
            try
            {
                DataTable tbl = mDac.LoadCounties(Statecode);
                cb.DataSource = tbl;
                cb.DisplayMember = "CountyName";
                cb.ValueMember = "ID";
                cb.SelectedIndex = 0;
                cb.Enabled = ((tbl != null) && (tbl.Rows.Count > 0));
                zSetStatusMessage("Ready");
            }
            catch (Exception ex)
            {
                mDac.WriteStack(ex);
            }
        }

        internal CallLookup QRZLookup(string call)
        {
            XmlDocument res = null;
            CallLookup rtn = null;

            //If we have a callsign to lookup...
            if (call.Length > 0)
            {
                res = zQRZlookup(call);
                if (res != null)
                {
                    frmLookupResults frm = new XLog2.frmLookupResults();
                    frm.lblCallSign.Text = zGetNodeText(res, "call") + " - " +
                                  zGetNodeText(res, "class");
                    string addr = zGetNodeText(res, "fname") + " " +
                                    zGetNodeText(res, "name") + "\r\n" +
                                    zGetNodeText(res, "addr1") + "\r\n" +
                                    zGetNodeText(res, "addr2") + ", " +
                                    zGetNodeText(res, "state") + " " +
                                    zGetNodeText(res, "zip") + "\r\n" +
                                    zGetNodeText(res, "country");
                    frm.tbNameAddress.Text = addr;
                    frm.tbLat.Text = zGetNodeText(res, "lat");
                    frm.tbLon.Text = zGetNodeText(res, "lon");
                    frm.tbDXCC.Text = zGetNodeText(res, "dxcc");
                    frm.tbCounty.Text = zGetNodeText(res, "county");
                    frm.tbGrid.Text = zGetNodeText(res, "grid");
                    frm.tbCQZone.Text = zGetNodeText(res, "cqzone");
                    frm.tbITUZone.Text = zGetNodeText(res, "ituzone");

                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        rtn = new CallLookup(zGetNodeText(res, "fname"), zGetNodeText(res, "name"),
                                            zGetNodeText(res, "addr1"), zGetNodeText(res, "addr2"),
                                            zGetNodeText(res, "state"), zGetNodeText(res, "zip"),
                                            zGetNodeText(res, "country"), zGetNodeText(res, "lat"),
                                            zGetNodeText(res, "lon"), zGetNodeText(res, "grid"),
                                            zGetNodeText(res, "dxcc"), zGetNodeText(res, "cqzone"),
                                            zGetNodeText(res, "ituzone"));
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        internal QSO EditQSO(DataGridView dgv, int rowIndex)
        {
            QSO qso = new QSO();
            DataGridViewRow row = null;
            DataTable qsoTable = null;
            DataRow qsoRow = null;
            int intval = 0;

            toolStripStatusLabel1.Text = "Reading log file";
            Application.DoEvents();

            row = dgv.Rows[rowIndex];
            int idx = (int)row.Cells["ID"].Value;
            qsoTable = mDac.GetQSOs(idx);

            if (qsoTable.Rows.Count > 0)
            {
                qsoRow = qsoTable.Rows[0];

                qso.Awards = qsoRow["Awards"].ToString();
                if (!int.TryParse(qsoRow["BandID"].ToString(), out intval)) { intval = -1; }
                qso.BandID = intval;
                qso.Call = qsoRow["Call"].ToString();
                if (!int.TryParse(qsoRow["CountryCode"].ToString(), out intval)) { intval = -1; }
                qso.CountryCode = intval;
                if (!int.TryParse(qsoRow["CountyCode"].ToString(), out intval)) { intval = -1; }
                qso.CountyCode = intval;
                qso.EndDate = (DateTime)qsoRow["EndDate"];
                qso.Frequency = qsoRow["Frequency"].ToString();
                qso.Locator = qsoRow["Locator"].ToString();
                qso.LogName = qsoRow["LogName"].ToString();
                qso.Mode = qsoRow["Mode"].ToString();
                if (!int.TryParse(qsoRow["ModeID"].ToString(), out intval)) { intval = -1; }
                qso.ModeID = intval;
                qso.Name = qsoRow["Name"].ToString();
                qso.Power = qsoRow["Power"].ToString();
                qso.QSLin = (bool)qsoRow["QSLin"];
                qso.QSLout = (bool)qsoRow["QSLout"];
                qso.QTH = qsoRow["QTH"].ToString();
                qso.Remarks = qsoRow["Remarks"].ToString();
                qso.RxRST = qsoRow["RXrst"].ToString();
                qso.StartDate = (DateTime)qsoRow["StartDate"];
                if (!int.TryParse(qsoRow["StateCode"].ToString(), out intval)) { intval = -1; }
                qso.StateCode = intval;
                qso.TxRST = qsoRow["TXrst"].ToString();
                qso.Unknown1 = qsoRow["Unknown1"].ToString();
                qso.Unknown2 = qsoRow["Unknown2"].ToString();
                qso.Unknown1Label = qsoRow["UNKNOWN1Label"].ToString();
                qso.Unknown2Label = qsoRow["UNKNOWN2Label"].ToString();
                if (!int.TryParse(qsoRow["ID"].ToString(), out intval)) { intval = -1; }
                qso.LogID = intval;
            }

            toolStripStatusLabel1.Text = "Ready";
            Application.DoEvents();

            return qso;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qso"></param>
        internal void SaveQSO(QSO qso)
        {
            //TODO: Finish this.
        }

        #endregion
    }
}
