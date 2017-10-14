using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmSelectLog : Form
    {
        public string mSelectedLog = "";
        private Form1 mFrm1 = null;
        private bool mAllowExitWithoutSelLog = true;

        public frmSelectLog(Form1 frm, bool allowExitWithoutSelLog)
        {
            InitializeComponent();
            mFrm1 = frm;
            mAllowExitWithoutSelLog = allowExitWithoutSelLog;
        }

        private void frmSelectLog_Load(object sender, EventArgs e)
        {
            DataTable tbl = mFrm1.mDac.GetLogNames();
            if ((tbl != null) && (tbl.Rows.Count > 0))
            {
                listBox1.DataSource = tbl;
                listBox1.DisplayMember = "LogName";
                listBox1.ValueMember = "LogName";
            }

            mFrm1.zCenterDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            zSelLog();
        }

        /// <summary>
        /// The user wants to create a new log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            string logFileName = "";
            int logFileID = 0;

            frmNewLog frm = new frmNewLog(mFrm1);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                logFileName = frm.tbLogName.Text.Trim();
                if (1 == mFrm1.mDac.SaveLogName(logFileName))
                {
                    logFileID = mFrm1.mDac.zGetLogFileID(logFileName);
                    if (logFileID > 0)
                    {
                        if (frm.logName) { mFrm1.mDac.SaveLogFields(logFileID, "Logname"); }
                        if (frm.QSONumber) { mFrm1.mDac.SaveLogFields(logFileID, "QSO Number"); }
                        if (frm.Date) { mFrm1.mDac.SaveLogFields(logFileID, "Date"); }
                        if (frm.UTC) { mFrm1.mDac.SaveLogFields(logFileID, "UTC"); }
                        if (frm.UTCend) { mFrm1.mDac.SaveLogFields(logFileID, "DateEnd"); }
                        if (frm.Call) { mFrm1.mDac.SaveLogFields(logFileID, "Call"); }

                        if (frm.Frequency) { mFrm1.mDac.SaveLogFields(logFileID, "Frequency"); }
                        if (frm.Mode) { mFrm1.mDac.SaveLogFields(logFileID, "Mode"); }
                        if (frm.TX) { mFrm1.mDac.SaveLogFields(logFileID, "TX"); }
                        if (frm.RX) { mFrm1.mDac.SaveLogFields(logFileID, "RX"); }
                        if (frm.Awards) { mFrm1.mDac.SaveLogFields(logFileID, "Awards"); }
                        if (frm.QslOut) { mFrm1.mDac.SaveLogFields(logFileID, "QSLOut"); }
                        if (frm.QslIn) { mFrm1.mDac.SaveLogFields(logFileID, "QSLIn"); }
                        if (frm.Power) { mFrm1.mDac.SaveLogFields(logFileID, "Power"); }
                        if (frm.contactName) { mFrm1.mDac.SaveLogFields(logFileID, "Name"); }
                        if (frm.QTH) { mFrm1.mDac.SaveLogFields(logFileID, "QTH"); }
                        if (frm.Locator) { mFrm1.mDac.SaveLogFields(logFileID, "Locator"); }
                        if (frm.Unknown1) { mFrm1.mDac.SaveLogFields(logFileID, "UNKNOWN1"); }
                        //else { (frm.tbUnknown1.Text.Trim().Length > 0) }
                        if (frm.Unknown2) { mFrm1.mDac.SaveLogFields(logFileID, "UNKNOWN2"); }
                        //else { (frm.tbUnknown2.Text.Trim().Length > 0) }
                        if (frm.Remarks) { mFrm1.mDac.SaveLogFields(logFileID, "Remarks"); }
                    }
                }

                //And now update the select table.
                DataTable tbl = mFrm1.mDac.GetLogNames();
                if ((tbl != null) && (tbl.Rows.Count > 0))
                {
                    listBox1.DataSource = tbl;
                    listBox1.DisplayMember = "LogName";
                    listBox1.ValueMember = "LogName";
                }
            }
            frm.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelect_Click(object sender, EventArgs e)
        {
            zSelLog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSelLog()
        {
            mSelectedLog = ((string)listBox1.SelectedValue).Trim();
            if (mSelectedLog.Length > 0)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nothing selected", "Input Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSelectLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!mAllowExitWithoutSelLog) && (mSelectedLog.Trim().Length == 0))
            {
                e.Cancel = true;
                MessageBox.Show("You MUST select a log", "Input Error");
            }
        }

    }
}
