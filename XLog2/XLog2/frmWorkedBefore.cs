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
    public partial class frmWorkedBefore : Form
    {
        private Form1 mFrm = null;
        private bool mTableFormatted = false;

        private bool mLogName = false;
        private bool mQSONumber = false;
        private bool mDateTimeStart = false;
        private bool mDateTimeEnd = false;
        private bool mCall = false;
        private bool mFrequency = false;
        private bool mMode = false;
        private bool mTXrst = false;
        private bool mRXrst = false;
        private bool mAwards = false;
        private bool mQSLout = false;
        private bool mQSLin = false;
        private bool mPower = false;
        private bool mName = false;
        private bool mQTH = false;
        private bool mLocator = false;
        private bool mUnknown1 = false;
        private bool mUnknown2 = false;
        private bool mRemarks = false;
        private bool mCountry = false;
        private bool mState = false;
        private bool mCounty = false;
        private bool mBand = false;

        public frmWorkedBefore(Form1 frm)
        {
            InitializeComponent();
            mFrm = frm;
        }

        /// <summary>
        /// The form is closing, need to tell Form1 that it's gone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWorkedBefore_FormClosed(object sender, FormClosedEventArgs e)
        {
            mFrm.WorkedBeforeClosing();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="show"></param>
        /// <param name="width"></param>
        private void zFormatGridColumn(string columnName, bool show, int width)
        {
            try
            {
                if (show)
                {
                    dataGridView1.Columns[columnName].Width = width;
                    dataGridView1.Columns[columnName].Visible = true;
                }
                else
                {
                    dataGridView1.Columns[columnName].Width = 0;
                    dataGridView1.Columns[columnName].Visible = false;
                }
            }
            catch (Exception ex)
            {
                int i = 1;
            }
        }

        private void zFormatGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.Columns["ID"].Width = 0;
            dataGridView1.Columns["ID"].Visible = false;
            zFormatGridColumn("ID", false, 0);
            zFormatGridColumn("QSO Number", mQSONumber, 50);
            zFormatGridColumn("Start Date", mDateTimeStart, 115);
            zFormatGridColumn("End Date", mDateTimeEnd, 115);
            zFormatGridColumn("Call", mCall, 80);
            zFormatGridColumn("Frequency", mFrequency, 67);
            zFormatGridColumn("Mode", mMode, 47);
            zFormatGridColumn("RST Sent", mTXrst, 38);
            zFormatGridColumn("RST Rcvd", mRXrst, 38);
            zFormatGridColumn("Awards", mAwards, 100);
            zFormatGridColumn("QSL Sent", mQSLout, 38);
            zFormatGridColumn("QSL Rcvd", mQSLin, 38);
            zFormatGridColumn("Power", mPower, 50);
            zFormatGridColumn("Name", mName, 90);
            zFormatGridColumn("QTH", mQTH, 150);
            zFormatGridColumn("Locator", mLocator, 300);
            zFormatGridColumn("UNKNOWN1", mUnknown1, 300);
            zFormatGridColumn("UNKNOWN2", mUnknown2, 300);
            zFormatGridColumn("Remarks", mRemarks, 300);
            zFormatGridColumn("LogName", mLogName, 90);
            zFormatGridColumn("Country", mCountry, 200);
            zFormatGridColumn("State", mState, 150);
            zFormatGridColumn("County Name", mCounty, 150);
            zFormatGridColumn("Band", mBand, 80);
            zFormatGridColumn("Unknown1Label", false, 0);
            zFormatGridColumn("Unknown2Label", false, 0);
            //"`qsos`.`UNKNOWN1Label`, `qsos`.`UNKNOWN2Label`
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        internal void CallSignChanged(string call)
        {
            DataTable tbl = mFrm.mDac.GetWorkedBefore(call);
            dataGridView1.DataSource = tbl;
            if ((tbl != null) && (!mTableFormatted))
            {
                zFormatGrid();
                mTableFormatted = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWorkedBefore_Load(object sender, EventArgs e)
        {
            mLogName = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Logname");
            mQSONumber = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.QSONum");
            mDateTimeStart = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Date");
            //propValue = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.UTC");
            mDateTimeEnd = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.UTCend");
            mCall = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Call");
            mFrequency = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Frequency");
            mMode = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Mode");
            mTXrst = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Tx");
            mRXrst = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Rx");
            mAwards = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Awards");
            mQSLout = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.QslOut");
            mQSLin = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.QslIn");
            mPower = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Power");
            mName = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Name");
            mQTH = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.QTH");
            mLocator = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Locator");
            mUnknown1 = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.UNKNOWN1");
            mUnknown2 = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.UNKNOWN2");
            mRemarks = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Remarks");
            mCountry = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.Country");
            mState = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.State");
            mCounty = "1" == mFrm.mDac.GetProperty("XLOG2.Worked.County");
        }
    }
}
