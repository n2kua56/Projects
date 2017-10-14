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
    //TODO: 1) Add 3 new QSO fields Country, State, County
    //DONE: 1A) CREATE adif tables
    //DONE: 1B) Create the States table
    //DONE: 1C) Create the Counties table
    //DONE: 1D) Modify HamLogFields table, add 3 new fields
    //TODO: 1E) Modify LogEditor
    //TODO: 1F) Modify NewLog
    //DONE: 1G) Modify Export
    //TODO: 1H) Modiry the "QSO" form
    //TODO: 2) Problems with the QSL boolean field.
    // --------------------------------------------
    //TODO: 3) DXCC List form 

    public partial class frmOldForm1 : Form
    {
        public string ProgramName = "XLog2";
        List<Control> QSOentryControls = new List<Control>();   //List of controls in the QSO form
        int m_QSONumber = 0;                                    //QSO number being edited OR 0 for new
        public DataAccess mDac = null;                          //Datalayer routines
        private int mSplitterDistance = 255;                    //Used to keep the splitter bar from moving on form resize.
        private string mQRZKey = "";
        private frmWorkedBefore mFrmWorkedBefore = null;
        private Form1 mFrm1 = null;

        public frmOldForm1(Form1 frm1)
        {
            InitializeComponent();
            mFrm1 = frm1;
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
            toolStripStatusLabel1.Text = "Starting";
            Application.DoEvents();
            string logName = "";

            mSplitterDistance = splitContainer1.SplitterDistance;

            mDac = new DataAccess(mFrm1);
            if (1 != mDac.ConnectToHamLog())
            {
                MessageBox.Show("Could not connect to the HamLog database.");
                Application.Exit();
            }

            string currentLogName = mDac.GetProperty("XLOG2.LastLogViewed");
            if (currentLogName.Trim().Length == 0)
            {
                frmSelectLog frm = new frmSelectLog(mFrm1, false);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    logName = frm.mSelectedLog;
                    tabControl1.TabPages[0].Text = logName;
                }
            }
            Application.DoEvents();
            zBuildQSOEntryForm(logName);
            btnSave.Text = "Save";
            DataGridView dgv = ((DataGridView)tabControl1.SelectedTab.Controls[0]);
            zBuildLogGrid(dgv, logName);
            this.Text = ProgramName + " - " + logName;

            toolStripStatusLabel1.Text = "Ready";
        }

        /// <summary>
        /// Do NOT want the splitter bar to move when resizing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = true;
            mSplitterDistance = splitContainer1.SplitterDistance;
        }

        /// <summary>
        /// Do NOT want the splitter bar to move when resizing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            splitContainer1.IsSplitterFixed = false;
            splitContainer1.SplitterDistance = mSplitterDistance;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = mSplitterDistance;
        }

        /// <summary>
        /// When the tabControl changes tab pages the QSO "form" must
        /// be changed to match the log that has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string logName = "";

            logName = tabControl1.SelectedTab.Text;
            int splitDist = splitContainer1.SplitterDistance;
            zBuildQSOEntryForm(logName);
            splitContainer1.SplitterDistance = splitDist;
            this.Text = ProgramName + " - " + logName;
        }

        /// <summary>
        /// not even sure the control this event belongs to still exists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete pressed");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = ((DataGridView)tabControl1.SelectedTab.Controls[0]).Rows[e.RowIndex];
            int idx = (int)row.Cells["ID"].Value;
            zEditQSO(idx);
        }

        /// <summary>
        /// In DataGridView right mouse click, select the row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = ((DataGridView)tabControl1.SelectedTab.Controls[0]).HitTest(e.X, e.Y);
                ((DataGridView)tabControl1.SelectedTab.Controls[0]).Rows[hti.RowIndex].Selected = true;
            }
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
            frmDefaults form = new XLog2.frmDefaults(mFrm1, tabControl1.SelectedTab.Text);
            form.ShowDialog();
        }

        /// <summary>
        /// The user wants to view the "Dialogs" setup dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dialogsWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDialogs form = new XLog2.frmDialogs(mFrm1);
            form.ShowDialog();
        }

        /// <summary>
        /// The user wants to view the "Preferences" setup dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences form = new XLog2.frmPreferences(mFrm1);
            form.ShowDialog();
        }

        /// <summary>
        /// Open another log tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            string logName = "";
            toolStripStatusLabel1.Text = "Loading page";

            frmSelectLog frm = new XLog2.frmSelectLog(mFrm1, true);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                logName = frm.mSelectedLog;
                int found = 0;
                foreach (TabPage p in tabControl1.TabPages)
                {
                    if (p.Text == logName)
                    {
                        found = 1;
                        tabControl1.SelectTab(p);
                        msg = "Selected tab. ";
                    }
                }
                if (found == 0)
                {
                    TabPage page = new TabPage();
                    page.Text = logName;
                    DataGridView dataGridView99 = new DataGridView();
                    dataGridView99.Name = "dataGridView" + logName;
                    page.Controls.Add(dataGridView99);
                    tabControl1.TabPages.Add(page);
                    ((DataGridView)page.Controls[0]).Top = 1;
                    ((DataGridView)page.Controls[0]).Left = 2;
                    ((DataGridView)page.Controls[0]).Width = tabControl1.Width - 12;
                    ((DataGridView)page.Controls[0]).Height = tabControl1.Height - 27;
                    ((DataGridView)page.Controls[0]).Anchor =
                         (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom );
                    ((DataGridView)page.Controls[0]).AllowUserToAddRows = false;
                    ((DataGridView)page.Controls[0]).AllowUserToDeleteRows = false;
                    ((DataGridView)page.Controls[0]).AllowUserToResizeRows = false;
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = 
                            new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    ((DataGridView)page.Controls[0]).AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
                    ((DataGridView)page.Controls[0]).ColumnHeadersHeightSizeMode = 
                            System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    ((DataGridView)page.Controls[0]).ContextMenuStrip = this.contextMenuStrip1;
                    ((DataGridView)page.Controls[0]).MultiSelect = false;
                    ((DataGridView)page.Controls[0]).ReadOnly = true;
                    ((DataGridView)page.Controls[0]).RowHeadersVisible = false;
                    ((DataGridView)page.Controls[0]).SelectionMode = 
                            System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                    ((DataGridView)page.Controls[0]).CellDoubleClick += 
                                new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
                    ((DataGridView)page.Controls[0]).MouseDown += 
                                new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
                    ((DataGridView)page.Controls[0]).ScrollBars = ScrollBars.Both;
                    tabControl1.SelectTab(page);
                    zBuildQSOEntryForm(logName);
                    zBuildLogGrid(((DataGridView)page.Controls[0]), logName);
                    this.Text = ProgramName + " - " + logName;
                }
            }

            toolStripStatusLabel1.Text = msg + "Ready";
        }

        /// <summary>
        /// Edit the log layout for the currently selected log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = tabControl1.SelectedTab.Text;

            frmLogEditor frm = new XLog2.frmLogEditor(logName, mFrm1);

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
            frm.tbUnknown1.Text = mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown1Label");
            frm.tbUnknown2.Text = mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown2Label");

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

            frmNewLog frm = new frmNewLog(mFrm1);
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
            MessageBox.Show("Not implemented yet", "System Info");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workedBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mFrmWorkedBefore == null)
            {
                mFrmWorkedBefore = new frmWorkedBefore(mFrm1);
                mFrmWorkedBefore.Show();
            }
        }

        /// <summary>
        /// frmWorkedBefore has been closed.
        /// </summary>
        internal void WorkedBeforeClosing()
        {
            mFrmWorkedBefore = null;
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
            frmExport frm = new frmExport(mFrm1, tabControl1.SelectedTab.Text);
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
        /// Delete the currently selected contact from the current log file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            int QSOID = -1;
            string IDstring = "";
            int err = 0;

            DataGridViewRow row = ((DataGridView)tabControl1.SelectedTab.Controls[0]).SelectedRows[0];
            msg = "Are you sure you want to delete:\r" +
                    "  Num:  " + row.Cells["Number"].Value.ToString() + "\r" +
                    "  Call: " + row.Cells["Call"].Value.ToString() + "\r" +
                    "  Date: " + row.Cells["StartDate"].Value.ToString() + "\r" +
                    "  Freq: " + row.Cells["Frequency"].Value.ToString() + "\r" +
                    "  Mode: " + row.Cells["Mode"].Value.ToString() + "\r" +
                    "Deleting can not be undon.";
            if (DialogResult.OK == MessageBox.Show(msg, ProgramName + " - Delete", 
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                IDstring = row.Cells["ID"].Value.ToString();
                if (int.TryParse(IDstring, out QSOID))
                {
                   if (1 != mDac.DeleteQSO(Convert.ToInt32(row.Cells["ID"].Value.ToString())))
                    {
                        err = 1;
                    }
                }
                else
                {
                    err = 1;
                }
            }

            if (err == 0)
            {
                zDisplayLogGrid();
            }
        }

        /// <summary>
        /// The user wants to edit the currently selected contact in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = ((DataGridView)tabControl1.SelectedTab.Controls[0]).SelectedRows[0];
            int idx = (int)row.Cells["ID"].Value;
            zEditQSO(idx);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount > 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }

            else
            {
                MessageBox.Show("You can not close the only tab shown", ProgramName + " input error");
            }
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

        #region QSO Form

        //////////////////////////////////////////////////////////////////////////////////
        // QSO Form.                                                                    //
        //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        private void zBuildQSOEntryForm(string logName)
        {
            lblQSO.Text = "New QSO";

            int splitDist = splitContainer1.SplitterDistance;

            tableLayoutPanel1.SuspendLayout();

            while (tableLayoutPanel1.RowCount > 1)
            {
                int row = tableLayoutPanel1.RowCount - 1;
                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(i, row);
                    if (c != null)
                    {
                        tableLayoutPanel1.Controls.Remove(c);
                        c.Dispose();
                    }
                }

                try
                {
                    tableLayoutPanel1.RowStyles.RemoveAt(row);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    //TODO: It would be nice to log the error with what is in msg.
                }
                tableLayoutPanel1.RowCount--;
            }
            
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            QSOentryControls.Clear();
            if (mDac.GetLogFields(logName, "Date") > 0) { zAddDate(); }
            if (mDac.GetLogFields(logName, "UTC") > 0) { zAddStartTime(); }
            if (mDac.GetLogFields(logName, "DateEnd") > 0) { zAddEndTime(); }
            if (mDac.GetLogFields(logName, "Call") > 0) { zAddCall(); }
            if (mDac.GetLogFields(logName, "Frequency") > 0) { zAddFrequency(); }
            if (mDac.GetLogFields(logName, "Mode") > 0) { zAddMode(); }
            if (mDac.GetLogFields(logName, "TX") > 0) { zAddTx(); }
            if (mDac.GetLogFields(logName, "RX") > 0) { zAddRx(); }
            if (mDac.GetLogFields(logName, "Awards") > 0) { zAwards(); }
            if (mDac.GetLogFields(logName, "QSLOut") > 0) { zAddQslOut(); }    //QSLIn always displays with QSLOut
            if (mDac.GetLogFields(logName, "Power") > 0) { zAddPower(); }
            if (mDac.GetLogFields(logName, "Name") > 0) { zAddName(); }
            if (mDac.GetLogFields(logName, "QTH") > 0) { zAddQTH(); }
            if (mDac.GetLogFields(logName, "Locator") > 0) { zAddLocator(); }
            if (mDac.GetLogFields(logName, "UNKNOWN1") > 0) { zAddUnknown1(); }
            if (mDac.GetLogFields(logName, "UNKNOWN2") > 0) { zAddUnknown2(); }
            if (mDac.GetLogFields(logName, "Remarks") > 0) { zAddRemarks(); }
            if (mDac.GetLogFields(logName, "Country") > 0) { zAddCountry(); }
            if (mDac.GetLogFields(logName, "State") > 0) { zAddState(); }
            if (mDac.GetLogFields(logName, "County") > 0) { zAddCounty(); }

            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();

            splitContainer1.SplitterDistance = splitContainer1.SplitterDistance + 20;
            splitContainer1.SplitterDistance = splitDist;

            zFillDefaults();
        }

        #region Start/End Date/Time
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetDate(DateTime dte)
        {
            Control c = zGetControl("dtpQSODate");
            if (c != null)
            {
                ((DateTimePicker)c).Value = dte;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddDate()
        {
            if (tableLayoutPanel1.RowCount > 1)
            {
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            }

            Button btnDate = new Button();
            btnDate.Name = "btnDate";
            btnDate.Text = "Date";
            QSOentryControls.Add(btnDate);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnDate_Click);
            tableLayoutPanel1.Controls.Add(btnDate, 0, tableLayoutPanel1.RowCount - 1);

            DateTimePicker dtpQSODate = new DateTimePicker();
            dtpQSODate.Name = "dtpQSODate";
            dtpQSODate.Format = DateTimePickerFormat.Short;
            QSOentryControls.Add(dtpQSODate);
            tableLayoutPanel1.Controls.Add(dtpQSODate, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(dtpQSODate, 3);
            dtpQSODate.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetStartTime(DateTime dte)
        {
            Control c = zGetControl("dtpQSOTime");
            if (c != null)
            {
                ((DateTimePicker)c).Value = dte;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddStartTime()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            Button btnStart = new Button();
            btnStart.Name = "btnStart";
            btnStart.Text = "UTC";
            QSOentryControls.Add(btnStart);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnStart_Click);
            tableLayoutPanel1.Controls.Add(btnStart, 0, tableLayoutPanel1.RowCount - 1);

            DateTimePicker dtpQSOTime = new DateTimePicker();
            dtpQSOTime.Name = "dtpQSOTime";
            dtpQSOTime.Format = DateTimePickerFormat.Time;
            QSOentryControls.Add(dtpQSOTime);
            tableLayoutPanel1.Controls.Add(dtpQSOTime, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(dtpQSOTime, 3);
            dtpQSOTime.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetEndTime(DateTime dte)
        {
            Control c = zGetControl("dtpEnd");
            if (c != null)
            {
                ((DateTimePicker)c).Value = dte;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddEndTime()
        {
            Button btnEnd = new Button();
            btnEnd.Click += new EventHandler(btnEnd_Click);

            DateTimePicker dtpEnd = new DateTimePicker();
            dtpEnd.Format = DateTimePickerFormat.Time;

            zAddLabeledControl(btnEnd, "btnEnd", "End (UTC)",
                               dtpEnd, "dtpEnd", 3);
        }
        #endregion

        #region Call
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetCall(string val)
        {
            Control c = zGetControl("tbCall");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddCall()
        {
            Label lblCall = new Label();
            TextBox tbCall = new TextBox();
            tbCall.CharacterCasing = CharacterCasing.Upper;
            tbCall.MaxLength = 45;
            tbCall.TextChanged += new System.EventHandler(this.tbCall_TextChanged);
            zAddLabeledControl(lblCall, "lblCall", "Call",
                               tbCall, "tbCall", 2);
            //Add the lookup button
            Button btnLookup = new Button();
            btnLookup.Name = "btnLookup";
            btnLookup.Height = btnDate.Height;
            btnLookup.Width = 20;
            btnLookup.Text = "?";
            QSOentryControls.Add(btnLookup);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnLookup_Click);
            tableLayoutPanel1.Controls.Add(btnLookup, 3, tableLayoutPanel1.RowCount - 1);
            btnLookup.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
        }
        #endregion

        #region Frequency
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetFrequency(string val)
        {
            Control c = zGetControl("tbFrequency");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddFrequency()
        {
            Label lblFrequency = new Label();
            //TODO: Need to check the database for Frequency being TextBox or ComboBox
            TextBox tbFrequency = new TextBox();
            tbFrequency.MaxLength = 45;
            zAddLabeledControl(lblFrequency, "lblFrequency", "MHz",
                               tbFrequency, "tbFrequency", 3);
        }
        #endregion

        #region Mode
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetMode(string val)
        {
            Control c = zGetControl("tbMode");
            if (c != null)
            {
                ((TextBox)c).Text = (string)val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddMode()
        {
            Label lblMode = new Label();
            //TODO: Need to check database for TextBox or ComboBox
            TextBox tbMode = new TextBox();
            tbMode.MaxLength = 45;
            zAddLabeledControl(lblMode, "lblMode", "Mode",
                               tbMode, "tbMode", 3);
        }
        #endregion

        #region Rx/Tx
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetTx(string val)
        {
            Control c = zGetControl("tbTx");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddTx()
        {
            Label lblTx = new Label();
            TextBox tbTx = new TextBox();
            tbTx.MaxLength = 3;
            zAddLabeledControl(lblTx, "lblTx", "Tx (RST)",
                               tbTx, "tbTx", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetRx(string val)
        {
            Control c = zGetControl("tbRx");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddRx()
        {
            Label lblRx = new Label();
            TextBox tbRx = new TextBox();
            tbRx.MaxLength = 3;
            
            zAddLabeledControl(lblRx, "lblRx", "Rx (RST)",
                               tbRx, "tbRx", 3);
        }
        #endregion

        #region Awards
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetAwards(string val)
        {
            Control c = zGetControl("tbAwards");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAwards()
        {
            Label lblAwards = new Label();
            TextBox tbAwards = new TextBox();
            tbAwards.MaxLength = 45;
            zAddLabeledControl(lblAwards, "lblAwards", "Awards",
                               tbAwards, "tbAwards", 3);
        }
        #endregion

        #region QSL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetQslOut(bool val)
        {
            Control c = zGetControl("cbQslOut");
            if (c != null)
            {
                ((CheckBox)c).Checked = val;
            }

            c = zGetControl("cbQslIn");
            if (c != null)
            {
                ((CheckBox)c).Checked = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddQslOut()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            CheckBox cbQslOut = new CheckBox();
            cbQslOut.Name = "cbQslOut";
            cbQslOut.Text = "QSL Out";
            QSOentryControls.Add(cbQslOut);
            tableLayoutPanel1.Controls.Add(cbQslOut, 1, tableLayoutPanel1.RowCount - 1);

            CheckBox cbQslIn = new CheckBox();
            cbQslIn.Name = "cbQslIn";
            cbQslIn.Text = "QSL In";
            QSOentryControls.Add(cbQslIn);
            tableLayoutPanel1.Controls.Add(cbQslIn, 2, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(cbQslIn, 2);
        }
        #endregion

        #region Power
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetPower(string val)
        {
            Control c = zGetControl("tbPower");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddPower()
        {
            Label lblPower = new Label();
            TextBox tbPower = new TextBox();
            tbPower.MaxLength = 45;
            zAddLabeledControl(lblPower, "lblPower", "Power",
                               tbPower, "tbPower", 3);
        }
        #endregion

        #region Country
        /// <summary>
        /// 
        /// </summary>
        private void zAddCountry()
        {
            Label lblCountry = new Label();
            ComboBox cmbCountry = new ComboBox();
            cmbCountry.DataSource = mDac.LoadCountries();
            cmbCountry.DisplayMember = "Country";
            cmbCountry.ValueMember = "EntityCode";
            zAddLabeledControl(lblCountry, "lblCountry", "Country", cmbCountry, "cmbCountry", 3);
            ((ComboBox)QSOentryControls[QSOentryControls.Count - 1]).SelectedIndexChanged += 
                new System.EventHandler(this.Country_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void zSetCountry(string val)
        {
            int nval = -1;
            nval = Convert.ToInt32(val);
            Control c = zGetControl("cmbCountry");
            if (c != null)
            {
                ((ComboBox)c).SelectedValue = nval;
            }
            zSetStateDataSource(nval);
        }
        #endregion

        #region State
        /// <summary>
        /// 
        /// </summary>
        private void zAddState()
        {
            Label lblState = new Label();
            ComboBox cmbState = new ComboBox();
            zAddLabeledControl(lblState, "lblState", "State", cmbState, "cmbState", 3);
            ((ComboBox)QSOentryControls[QSOentryControls.Count - 1]).SelectedIndexChanged +=
                new System.EventHandler(this.State_SelectedIndexChanged);
            ComboBox cmbCt = (ComboBox)zFindControlByName("cmbCountry");
            Country_SelectedIndexChanged(cmbCt, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void zSetState(string val)
        {
            int nval = -1;
            nval = Convert.ToInt32(val);
            Control c = zGetControl("cmbState");
            if (c != null)
            {
                ((ComboBox)c).SelectedValue = nval;
            }
            zSetCountyDataSource(nval);
        }
        #endregion

        #region County
        /// <summary>
        /// 
        /// </summary>
        private void zAddCounty()
        {
            Label lblCounty = new Label();
            ComboBox cmbCounty = new ComboBox();
            zAddLabeledControl(lblCounty, "lblCounty", "County", cmbCounty, "cmbCounty", 3);
            ComboBox cmbSt = (ComboBox)zFindControlByName("cmbState");
            State_SelectedIndexChanged(cmbSt, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void zSetCounty(string val)
        {
            int nval = -1;
            nval = Convert.ToInt32(val);
            Control c = zGetControl("cmbCounty");
            if (c != null)
            {
                ((ComboBox)c).SelectedValue = nval;
            }
        }
        #endregion

        #region Name
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetName(string val)
        {
            Control c = zGetControl("tbName");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddName()
        {
            Label lblName = new Label();
            TextBox tbName = new TextBox();
            tbName.MaxLength = 60;
            zAddLabeledControl(lblName, "lblName", "Name",
                               tbName, "tbName", 3);
        }
        #endregion

        #region QTH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetQTH(string val)
        {
            Control c = zGetControl("tbQTH");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddQTH()
        {
            Label lblQTH = new Label();
            TextBox tbQTH = new TextBox();
            tbQTH.MaxLength = 128;
            zAddLabeledControl(lblQTH, "lblQTH", "QTH",
                               tbQTH, "tbQTH", 3);
        }
        #endregion

        #region Locator
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetLocator(string val)
        {
            Control c = zGetControl("tbLocator");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddLocator()
        {
            Label lblLocator = new Label();
            TextBox tbLocator = new TextBox();
            tbLocator.MaxLength = 45;
            zAddLabeledControl(lblLocator, "lblLocator", "Locator",
                               tbLocator, "tbLocator", 3);
        }
        #endregion

        #region Unknowns
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetUnknown1(string val)
        {
            Control c = zGetControl("tbUnknown1");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddUnknown1()
        {
            Label lblUnknown1 = new Label();
            string lblText = mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown1Label");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN1"; }
            TextBox tbUnknown1 = new TextBox();
            tbUnknown1.MaxLength = 128;
            zAddLabeledControl(lblUnknown1, "lblUnknown1", lblText,
                               tbUnknown1, "tbUnknown1", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetUnknown2(string val)
        {
            Control c = zGetControl("tbUnknown2");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddUnknown2()
        {
            //TODO: Need to get the Unknown2 field title
            Label lblUnknown2 = new Label();
            TextBox tbUnknown2 = new TextBox();
            string lblText = mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown2Label");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN2"; }
            tbUnknown2.MaxLength = 128;
            zAddLabeledControl(lblUnknown2, "lblUnknown2", lblText,
                               tbUnknown2, "tbUnknown2", 3);
        }
        #endregion

        #region Remarks
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetRemarks(string val)
        {
            Control c = zGetControl("tbRemarks");
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddRemarks()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            
            Label lblRemark = new Label();
            lblRemark.Name = "lblRemark";
            lblRemark.Text = "Remarks";
            Font font = new Font("Microsoft Sans Serif", 12.0f,
                        FontStyle.Bold);
            lblRemark.Font = font;

            lblRemark.AutoSize = false;
            lblRemark.TextAlign = ContentAlignment.BottomCenter;
            tableLayoutPanel1.Controls.Add(lblRemark, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(lblRemark, 2);

            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            TextBox tbRemarks = new TextBox();
            tbRemarks.MaxLength = 1024;
            tbRemarks.Name = "tbRemarks";
            tbRemarks.Multiline = true;
            tbRemarks.Height = 65;
            tbRemarks.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            QSOentryControls.Add(tbRemarks);
            tableLayoutPanel1.Controls.Add(tbRemarks, 0, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(tbRemarks, 4);
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
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click(object sender, EventArgs e)
        {
            XmlDocument res = null;

            //Get the callsign we want to lookup
            Control c = zGetControl("tbCall");
            string callSign = ((TextBox)c).Text.Trim();

            //If we have a callsign to lookup...
            if (callSign.Length > 0)
            {
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
                            MessageBox.Show("The KEY tag was not found in the QRZ response", ProgramName + " - QRZ Error");
                        }
                    }

                    //NO RESPONSE FROM QRZ
                    else
                    {
                        MessageBox.Show("There was no response from QRZ", ProgramName + " - QRZ Error");
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
                        if (err.Trim().Length == 0)
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
                                c = zGetControl("tb");
                                zSetName(zGetNodeText(res, "fname") + " " + zGetNodeText(res, "name"));
                                zSetQTH(zGetNodeText(res, "addr1") + "; " +
                                        zGetNodeText(res, "addr2") + ", " +
                                        zGetNodeText(res, "state") + "; " +
                                        zGetNodeText(res, "zip") + "; " +
                                        zGetNodeText(res, "country"));
                                zSetLocator(zGetNodeText(res, "lat") + "; " +
                                        zGetNodeText(res, "lon") + "; " +
                                        zGetNodeText(res, "grid") + "; " +
                                        zGetNodeText(res, "dxcc") + "; " +
                                        zGetNodeText(res, "cqzone") + "; " +
                                        zGetNodeText(res, "ituzone"));
                            }
                        }

                        else
                        {
                            MessageBox.Show(err, ProgramName + " Error");
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Couldn't sign onto the QRZ Server", ProgramName + " - QRZ Error");
                }
            }

            //No callsign
            else
            {
                MessageBox.Show("You must have a callsign to lookup", ProgramName + " input error");
            }
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

        #region QSO Utilities
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="lblName"></param>
        /// <param name="lblText"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <param name="dataColSpan"></param>
        private void zAddLabeledControl(Control lbl, String lblName, String lblText, 
                                        Control data, String dataName, int dataColSpan)
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            lbl.AutoSize = false;
            lbl.Name = lblName;
            lbl.Height = btnDate.Height;
            lbl.Width = btnDate.Width;
            lbl.Text = lblText;
            if (lbl is Label)
            {
                ((Label)lbl).TextAlign = ContentAlignment.MiddleCenter;
            }
            if (lbl is Button)
            {
                ((Button)lbl).TextAlign = ContentAlignment.MiddleCenter;
                QSOentryControls.Add(lbl);
            }
            tableLayoutPanel1.Controls.Add(lbl, 0, tableLayoutPanel1.RowCount - 1);

            data.Name = dataName;
            data.Height = dtpQSODate.Height;
            data.Width = dtpQSODate.Width;
            QSOentryControls.Add(data);
            tableLayoutPanel1.Controls.Add(data, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(data, dataColSpan);
            data.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpEnd");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1vv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDate_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpQSODate");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpQSOTime");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Control zFindControlByName(String name, int quiet = 0)
        {
            Control rtn = null;

            foreach(Control c in QSOentryControls)
            {
                if (c.Name.ToLower() == name.ToLower())
                {
                    rtn = c;
                }
            }

            if (rtn == null)
            {
                if (quiet != 1)
                {
                    MessageBox.Show("Couldn't find " + name, "System Error");
                }
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Control zGetControl(string name)
        {
            Control rtn = null;
            foreach (Control c in QSOentryControls)
            {
                if (c.Name == name)
                {
                    rtn = c;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zClearQSO()
        {
            foreach (Control c in QSOentryControls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = 0;
                }
                if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).Value = DateTime.Now;
                }
            }

            zFillDefaults();

            lblQSO.Text = "New QSO";
            btnSave.Text = "Save";
            m_QSONumber = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillDefaults()
        {
            string LogName = tabControl1.SelectedTab.Text;

            Control cc = zGetControl("tbFrequency");
            if (cc != null) { zSetFrequency(mDac.GetDefault(LogName, "Frequency")); }

            cc = zGetControl("tbMode");
            if (cc != null) { zSetMode(mDac.GetDefault(LogName, "Mode")); }

            cc = zGetControl("tbTx");
            if (cc != null) { zSetTx(mDac.GetDefault(LogName, "Tx")); }

            cc = zGetControl("tbRx");
            if (cc != null) { zSetRx(mDac.GetDefault(LogName, "Rx")); }

            cc = zGetControl("tbAwards");
            if (cc != null) { zSetAwards(mDac.GetDefault(LogName, "Awards")); }

            cc = zGetControl("tbPower");
            if (cc != null) { zSetPower(mDac.GetDefault(LogName, "Power")); }

            cc = zGetControl("tbUnknown1");
            if (cc != null) { zSetUnknown1(mDac.GetDefault(LogName, "Unknown1")); }

            cc = zGetControl("tbUnknown2");
            if (cc != null) { zSetUnknown2(mDac.GetDefault(LogName, "Unknown2")); }

            cc = zGetControl("tbRemarks");
            if (cc != null) { zSetRemarks(mDac.GetDefault(LogName, "Remarks")); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            zClearQSO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            int ID = 0;
            DateTime start;
            DateTime min;
            DateTime time;
            string call = "";
            string freq = "";
            string mode = "";
            string val = "";
            bool valBool = false;
            DateTime valDate = DateTime.Now;

            Control c = zGetControl("dtpQSODate");
            start = ((DateTimePicker)c).Value;
            c = zGetControl("dtpQSOTime");
            min = ((DateTimePicker)c).Value;
            time = new DateTime(start.Year, start.Month, start.Day, min.Hour, min.Minute, min.Second);
            c = zGetControl("tbCall");
            call = ((TextBox)c).Text.Trim();
            c = zGetControl("tbFrequency");
            freq = ((TextBox)c).Text.Trim();
            c = zGetControl("tbMode");
            mode = ((TextBox)c).Text.Trim();

            //The user must specify Call, Frequency and Mode... the dates can not
            // be verifyed because they always return a date.
            if ((call.Length == 0) || (freq.Length == 0) || (mode.Length == 0))
            {
                MessageBox.Show("You MUST specify Call, Frequency and Mode", ProgramName + " Input Error");
                return;
            }

            //if m_QSONumber is not 0 we are in edit mode and the value of
            // m_QSONumber is the recorfd index being updated. Otherwise
            // we do an insert and catch the index number of the item just
            // inserted.
            if (m_QSONumber != 0)
            {
                ID = m_QSONumber;
            }
            else
            {
                ID = mDac.AddQSO(time, call, freq, mode, tabControl1.SelectedTab.Text.Trim());
            }

            //Now we save the values the user has entered into the controls
            // of the QSO "form".
            foreach (Control cc in QSOentryControls)
            {
                //Buttons are never saved and labelse are not added to the array
                // of controls on the QSO "form".
                if (!(cc is Button))
                {
                    //If this was a new QSO, the dates, Call, Frequency and Mode have
                    //  already been saved.
                    if ((m_QSONumber != 0) || ((m_QSONumber == 0) && (cc.Name != "tbCall") && 
                                                (cc.Name != "dtpQSODate") && (cc.Name != "dtpQSOTime") &&
                                                (cc.Name != "tbMode") && (cc.Name != "tbFrequency")))
                    {
                        //Get the control value (what the user typed)
                        if (cc is TextBox)
                        {
                            val = ((TextBox)cc).Text.Trim();
                        }
                        if (cc is CheckBox)
                        {
                            valBool = ((CheckBox)cc).Checked;
                        }
                        if (cc is ComboBox)
                        {
                            val = ((ComboBox)cc).Text.Trim();
                        }
                        if (cc is DateTimePicker)
                        {
                            valDate = ((DateTimePicker)cc).Value;
                        }
                        
                        //And now do an SQL UPDATE to wrte the value out.
                        switch (cc.Name)
                        {
                            case "dtpQSODate":
                                mDac.UpdateQSODateField(ID, "StartDate", valDate);
                                //# mDac.UpdateQSODate(ID, time);
                                break;

                            case "dtpEnd":
                                mDac.UpdateQSODateField(ID, "EndDate", valDate);
                                //# mDac.UpdatEnd(ID, valDate);
                                break;

                            case "tbCall":
                                mDac.UpdateQSOTextField(ID, "Call", val);
                                //# mDac.UpdateCall(ID, val);
                                break;

                            case "tbFrequency":
                                mDac.UpdateQSOTextField(ID, "Frequency", val);
                                //# mDac.UpdateFrequency(ID, val);
                                break;

                            case "tbMode":
                                mDac.UpdateQSOTextField(ID, "Mode", val);
                                //# mDac.UpdateMode(ID, val);
                                break;
                            
                            case "tbTx":
                                mDac.UpdateQSOTextField(ID, "TXrst", val);
                                //# mDac.UpdateTx(ID, val);
                                break;

                            case "tbRx":
                                mDac.UpdateQSOTextField(ID, "RXrst", val);
                                //# mDac.UpdateRx(ID, val);
                                break;

                            case "tbAwards":
                                mDac.UpdateQSOTextField(ID, "Awards", val);
                                //# mDac.UpdateAwards(ID, val);
                                break;

                            case "cbQslOut":
                                mDac.UpdateQSOBoolField(ID, "QSLout", valBool);
                                //# mDac.UpdateQSLout(ID, valBool);
                                break;

                            case "cbQslIn":
                                mDac.UpdateQSOBoolField(ID, "QSLin", valBool);
                                //# mDac.UpdateQSLin(ID, valBool);
                                break;

                            case "tbPower":
                                mDac.UpdateQSOTextField(ID, "Power", val);
                                //# mDac.UpdatePower(ID, val);
                                break;

                            case "tbName":
                                mDac.UpdateQSOTextField(ID, "Name", val);
                                //# mDac.UpdateName(ID, val);
                                break;

                            case "tbQTH":
                                mDac.UpdateQSOTextField(ID, "QTH", val);
                                //# mDac.UpdateQTH(ID, val);
                                break;

                            case "tbLocator":
                                mDac.UpdateQSOTextField(ID, "Locator", val);
                                //# mDac.UpdateLocator(ID, val);
                                break;

                            case "tbUNKNOWN1":
                                mDac.UpdateQSOTextField(ID, "UNKNOWN1", val);
                                //# mDac.UpdateUnknown1(ID, val);
                                break;

                            case "tbUNKNOWN2":
                                mDac.UpdateQSOTextField(ID, "UNKNOWN2", val);
                                //# mDac.UpdateUnknown2(ID, val);
                                break;

                            case "tbRemarks":
                                mDac.UpdateQSOTextField(ID, "Remarks", val);
                                //# mDac.UpdateRemarks(ID, val);
                                break;

                            case "cmbCountry":
                                mDac.UpdateQSOIntField(ID, "CountryCode", Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString()));
                                break;

                            case "cmbState":
                                mDac.UpdateQSOIntField(ID, "StateCode", Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString()));
                                break;

                            case "cmbCounty":
                                mDac.UpdateQSOIntField(ID, "CountyCode", Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString()));
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            zDisplayLogGrid();
        }
        #endregion

        private void zDisplayLogGrid()
        {
            int count = 0;
            if (rb100.Checked) { count = 100; }
            if (rb500.Checked) { count = 500; }
            ((DataGridView)tabControl1.SelectedTab.Controls[0]).DataSource = mDac.GetQSOs(tabControl1.SelectedTab.Text.Trim(), count);
            Rectangle dispRect = dataGridView1.DisplayRectangle;
            int dispRowCount = dataGridView1.DisplayedRowCount(true);
            int rowHeight = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            //dispRect.Height / dispRowCount;

            //int gridRows = dataGridView1.Rows.Count;
            //Rectangle gridRows2 = dataGridView1.DisplayRectangle;
            //int rowHeight = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            //int firstRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            zClearQSO();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        private void zEditQSO(int Id)
        {
            toolStripStatusLabel1.Text = "Reading log file";

            DataTable qso = mDac.GetQSOs(Id);
            zClearQSO();

            if (qso.Rows.Count == 1)
            {
                lblQSO.Text = "QSO: " + qso.Rows[0]["Id"];
                zSetDate((DateTime)qso.Rows[0]["StartDate"]); 
                zSetStartTime((DateTime)qso.Rows[0]["StartDate"]); 
                zSetEndTime((DateTime)qso.Rows[0]["StartDate"]);
                zSetCall((qso.Rows[0]["Call"] == null) ? "" : (string)qso.Rows[0]["Call"]);
                zSetFrequency((qso.Rows[0]["Frequency"] == null) ? "" : (string)qso.Rows[0]["Frequency"]);
                zSetMode((qso.Rows[0]["Mode"] == null) ? "" : qso.Rows[0]["Mode"].ToString()); 
                zSetTx((qso.Rows[0]["TXrst"] == null) ? "" : qso.Rows[0]["TXrst"].ToString()); 
                zSetRx((qso.Rows[0]["RXrst"] == null) ? "" : qso.Rows[0]["RXrst"].ToString());
                zSetAwards((qso.Rows[0]["Awards"] == null) ? "" : qso.Rows[0]["Awards"].ToString()); 
                zSetQslOut((qso.Rows[0]["QSLOut"] == null) ? false : "1" == qso.Rows[0]["QSLOut"].ToString());
                zSetPower((qso.Rows[0]["Power"] == null) ? "" : qso.Rows[0]["Power"].ToString());
                zSetName((qso.Rows[0]["Name"] == null) ? "" : qso.Rows[0]["Name"].ToString()); 
                zSetQTH((qso.Rows[0]["QTH"] == null) ? "" : qso.Rows[0]["QTH"].ToString());
                zSetLocator((qso.Rows[0]["Locator"] == null) ? "" : qso.Rows[0]["Locator"].ToString()); 
                zSetUnknown1((qso.Rows[0]["Unknown1"] == null) ? "" : qso.Rows[0]["Unknown1"].ToString()); 
                zSetUnknown2((qso.Rows[0]["Unknown2"] == null) ? "" : qso.Rows[0]["Unknown2"].ToString()); 
                zSetRemarks((qso.Rows[0]["Remarks"] == null) ? "" : qso.Rows[0]["Remarks"].ToString());
                zSetCountry((qso.Rows[0]["CountryCode"] == null) ? "" : qso.Rows[0]["CountryCode"].ToString());
                zSetState((qso.Rows[0]["StateCode"] == null) ? "" : qso.Rows[0]["StateCode"].ToString());
                zSetCounty((qso.Rows[0]["CountyCode"] == null) ? "" : qso.Rows[0]["CountyCode"].ToString());
                m_QSONumber = Id;
                btnSave.Text = "Update";

            }
            
            toolStripStatusLabel1.Text = "Ready";
        }

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
        /// <param name="dgv"></param>
        private void zBuildLogGrid(DataGridView dgv, string logName)
        {
            int count = 0;
            if (rb100.Checked) { count = 100; }
            if (rb500.Checked) { count = 500; }
            dgv.DataSource = mDac.GetQSOs(logName, count);

            dgv.Columns["ID"].Width = 0;
            dgv.Columns["ID"].Visible = false;
            dgv.Columns["Number"].Width = 50;

            zAdjustLogGrid(dgv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dgv"></param>
        private void zAdjustLogGrid(DataGridView dgv)
        {
            string logName = tabControl1.SelectedTab.Text;
            zFormatColumnHeader(dgv, logName, "Date", "StartDate", 100);
            zFormatColumnHeader(dgv, logName, "DateEnd", "EndDate", 100);
            zFormatColumnHeader(dgv, logName, "Call", "Call", 80);
            zFormatColumnHeader(dgv, logName, "Frequency", "Frequency", 60);
            zFormatColumnHeader(dgv, logName, "Mode", "Mode", 40);
            zFormatColumnHeader(dgv, logName, "TX", "TXrst", 40);
            zFormatColumnHeader(dgv, logName, "RX", "RXrst", 40);
            zFormatColumnHeader(dgv, logName, "Awards", "Awards", 50);
            zFormatColumnHeader(dgv, logName, "QSLOut", "QSLOut", 50);
            zFormatColumnHeader(dgv, logName, "QSLIn", "QSLIn", 50);
            zFormatColumnHeader(dgv, logName, "Power", "Power", 40);
            zFormatColumnHeader(dgv, logName, "Name", "Name", 90);
            zFormatColumnHeader(dgv, logName, "QTH", "QTH", 100);
            zFormatColumnHeader(dgv, logName, "Locator", "Locator", 55);
            zFormatColumnHeader(dgv, logName, "UNKNOWN1", "UNKNOWN1", 120);
            zFormatColumnHeader(dgv, logName, "UNKNOWN2", "UNKNOWN2", 120);
            zFormatColumnHeader(dgv, logName, "Remarks", "Remarks", 300);

            List<String> hideCols = new List<String>{ "UNKNOWN1Label", "UNKNOWN2Label", "CountryCode", "StateCode", "CountyCode" };
            foreach (string name in hideCols)
            {
                dgv.Columns[name].Width = 0;
                dgv.Columns[name].Visible = false;
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
            width = (mDac.GetLogFields(logName, fldName) > 0) ? width : 0;
            dgv.Columns[gridColumnName].Width = width;
            if (width == 0) { dgv.Columns[gridColumnName].Visible = false; }
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
            AboutBox1 frm = new AboutBox1(mFrm1);
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCall_TextChanged(object sender, EventArgs e)
        {
            string val = "";
            if (mFrmWorkedBefore != null)
            {
                Control c = zGetControl("tbCall");
                if (c != null)
                {
                    val = ((TextBox)c).Text.Trim();
                    if (val.Length > 2)
                    {
                        mFrmWorkedBefore.CallSignChanged(val);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// Set the State Combo Box.
        /// </summary>
        private void Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int countryid = (int)((ComboBox)sender).SelectedValue;
                ComboBox cmbSt = (ComboBox)zFindControlByName("cmbState", 1);
                cmbSt.DataSource = mDac.LoadStates(countryid);
                cmbSt.ValueMember = "ID";
                cmbSt.DisplayMember = "State";
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void State_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string test = ((ComboBox)sender).Name;
                string test2 = ((ComboBox)sender).SelectedValue.ToString();
                DataTable dt = (DataTable)((ComboBox)sender).DataSource;
                int stateid = Convert.ToInt32(((ComboBox)sender).SelectedValue.ToString());
                ComboBox cmbCt = (ComboBox)zFindControlByName("cmbCounty", 1);
                cmbCt.DataSource = mDac.LoadCounties(stateid);
                cmbCt.DisplayMember = "CountyName";
                cmbCt.ValueMember = "ID";
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryID"></param>
        private void zSetStateDataSource(int CountryID)
        {
            try
            {
                ComboBox cmbS = (ComboBox)zFindControlByName("cmbState");
                cmbS.DataSource = mDac.LoadStates(CountryID);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StateID"></param>
        private void zSetCountyDataSource(int StateID)
        {
            try
            {
                ComboBox cmbC = (ComboBox)zFindControlByName("cmbCounty");
                cmbC.DataSource = mDac.LoadCounties(StateID);
            }
            catch (Exception ex) { }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////frmLog frm = new frmLog(this);
            ////frm.ShowDialog();
        }
    }
}
