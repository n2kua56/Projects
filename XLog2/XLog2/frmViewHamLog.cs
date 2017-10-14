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
    public partial class frmViewHamLog : Form
    {
        private Form1 mFrm1 = null;

        public frmViewHamLog(Form1 frm)
        {
            InitializeComponent();
            mFrm1 = frm;
        }

        private void frmViewHamLog_Load(object sender, EventArgs e)
        {
            //TODO: Load the Band, Mode and Country comboboxes.
            mFrm1.FillModeComboBox(cmbHRMode);
            mFrm1.FillBandComboBox(cmbHRBand);
            mFrm1.FillCountriesComboBox(cmbHRCountry);
            mFrm1.FillLogGrid(dataGridView2, rbHR100.Checked, rbHR500.Checked, rbHRAll.Checked,
                                cbHRDateRange.Checked, dtpHRStartDate.Value, dtpHRStopDate.Value, "HamLog", mFrm1.mHamLogLogName);
            lblHRTitle.Text = "Recent Contacts - " + mFrm1.mHamLogLogName;
            lblHRCount.Text = dataGridView2.RowCount.ToString() + " Listed";
        }

        #region Form events handled by this form

        /// <summary>
        /// The user has clicked the Radio Button to limit the number of
        /// entries shown in the log to 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbHR100_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user has clicked the Radio Button to limit the number of
        /// entries shown in the log to 500
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbHR500_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user has pressed the ALL Radio Button to indicate they
        /// don't want to limit the number of entries displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbHRAll_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user has clicked the Check Box to enable/disable
        /// the date range selection to display in the Log.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbHRDateRange_Click(object sender, EventArgs e)
        {
            dtpHRStartDate.Enabled = cbHRDateRange.Checked;
            dtpHRStopDate.Enabled = cbHRDateRange.Checked;

            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user has pressed the Save/Update QSO button, Save or
        /// Update the QSO 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHRAddUpdate_Click(object sender, EventArgs e)
        {
            QSO qso = null;
            DateTime start = DateTime.MinValue;
            DateTime tempDate = DateTime.MinValue;
            DateTime tempTime = DateTime.MinValue;
            int LogID = 0;

            tempDate = dtpHRDate.Value;
            tempTime = dtpHRTimeOn.Value;
            start = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 
                                    tempTime.Hour, tempTime.Minute, tempTime.Second);

            //TODO: Fill-in the last field, LogName
            qso = new QSO(LogID, tbHRCall.Text.Trim(), start, (int)cmbHRBand.SelectedValue, 
                            tbHRFrequency.Text.Trim(), (int)cmbHRMode.SelectedValue,
                            tbHRPower.Text.Trim(), (int)cmbHRCountry.SelectedValue,
                            mtbHRSent.Text.Trim(), mtbHRRec.Text.Trim(), tbHRName.Text.Trim(),
                            dtpHRTimeOff.Value, (int)cmbHRState.SelectedValue,
                            (int)cmbHRCounty.SelectedValue, tbHROther.Text.Trim(), cbHRQSLrcvd.Checked, 
                            cbHRQSLsent.Checked, tbHRRemarks.Text.Trim(), "");
            //TODO: Call the Form1 SaveUpdateQSO routine
            mFrm1.SaveQSO(qso);
        }

        /// <summary>
        /// The user has pressed the QSO Clear Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHRClear_Click(object sender, EventArgs e)
        {
            ClearQSO();
        }

        internal void ClearQSO(bool all=false)
        {
            tbHRCall.Text = "";
            dtpHRDate.Value = DateTime.Now;
            dtpHRTimeOn.Value = dtpHRDate.Value;

            if (all)
            {
                cmbHRBand.SelectedValue = 0;
                tbHRFrequency.Text = "";
                cmbHRMode.SelectedValue = 0;
                tbHRPower.Text = "";
                cmbHRCountry.SelectedValue = 0;
            }

            mtbHRSent.Text = "";
            mtbHRRec.Text = "";
            tbHRName.Text = "";
            dtpHRTimeOff.Value = DateTime.Now;
            
            cmbHRState.SelectedIndex = 0;
            if ((cmbHRCountry.SelectedValue == null) || (0 == (int)cmbHRCountry.SelectedValue))
            {
                cmbHRState.Enabled = false;
            }
            cmbHRCounty.SelectedValue = 0;
            cmbHRCounty.Enabled = false;

            tbHROther.Text = "";
            cbHRQSLsent.Checked = false;
            cbHRQSLrcvd.Checked = false;
            label6.Text = "Entry Mode";
            tbHRRemarks.Text = "";
            dataGridView2.Enabled = false;
            btnHRAddUpdate.Text = "Add";
            //TODO: Fill-in defaults
        }

        /// <summary>
        /// The user doubled clicked in the Log. This indicates that the
        /// user wants to edit the row that was clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = -1;
            DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
            if(! int.TryParse(row.Cells["ID"].ToString(), out idx)) { idx = -1; }
            zEditQSO(idx);
        }

        private void zEditQSO(int ID)
        {
            QSO qso = null;

            qso = mFrm1.EditQSO(dataGridView2, ID);
            if (qso == null)
            {
                MessageBox.Show("No QSO returned!", mFrm1.ProgramName + " System Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                label6.Text = "Edit Mode";
                tbHRCall.Text = qso.Call;
                dtpHRDate.Value = qso.StartDate;
                dtpHRTimeOn.Value = qso.StartDate;
                cmbHRBand.SelectedValue = qso.BandID;
                cmbHRMode.SelectedValue = qso.ModeID;
                tbHRPower.Text = qso.Power;
                mtbHRSent.Text = qso.TxRST;
                mtbHRRec.Text = qso.RxRST;
                cmbHRCountry.SelectedValue = qso.CountryCode;
                cmbHRState.SelectedValue = qso.StateCode;
                cmbHRCounty.SelectedValue = qso.CountyCode;
                tbHRName.Text = qso.Name;
                tbHRFrequency.Text = qso.Frequency;
                dtpHRTimeOff.Value = qso.EndDate;
                tbHROther.Text = qso.Unknown1;
                cbHRQSLsent.Checked = qso.QSLout;
                cbHRQSLrcvd.Checked = qso.QSLin;
                tbHRRemarks.Text = qso.Remarks;
                dataGridView2.Enabled = false;
                btnHRAddUpdate.Text = "Update";
            }
        }

        /// <summary>
        /// The user has pressed the mouse button while over the Log. May
        /// need to bring up the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var htj = dataGridView2.HitTest(e.X, e.Y);
                dataGridView2.Rows[htj.RowIndex].Selected = true;
            }
        }

        /// <summary>
        /// The user has pressed the Edit Context menu option.  The user
        /// wants to edit the selecte row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idx = -1;
            DataGridViewRow row = null;

            row = dataGridView2.SelectedRows[0];

            idx = (int)row.Cells["ID"].Value;
            zEditQSO(idx);
        }

        /// <summary>
        /// The user has pressed the Delete Context menu option.  The user
        /// wants to delete the selected row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            int QSOID = -1;
            string IDstring = "";
            int err = 0;
            DataGridViewRow row = null;
            DataGridView dgv = null;

            DataGridViewSelectedRowCollection rows = dataGridView2.SelectedRows;
            row = rows[0];

            msg = "Are you sure you want to delete:\r" +
                            "  Num:  " + row.Cells["Number"].Value.ToString() + "\r" +
                            "  Call: " + row.Cells["Call"].Value.ToString() + "\r" +
                            "  Date: " + row.Cells["StartDate"].Value.ToString() + "\r" +
                            "  Freq: " + row.Cells["Frequency"].Value.ToString() + "\r" +
                            "  Mode: " + row.Cells["Mode"].Value.ToString() + "\r" +
                            "Deleting can not be undon.";

            if (DialogResult.OK == MessageBox.Show(msg, mFrm1.ProgramName + " - Delete",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                IDstring = row.Cells["ID"].Value.ToString();
                if (int.TryParse(IDstring, out QSOID))
                {
                    if (1 != mFrm1.mDac.DeleteQSO(Convert.ToInt32(row.Cells["ID"].Value.ToString())))
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
                mFrm1.FillLogGrid(dataGridView2, rbHR100.Checked, rbHR500.Checked, rbHRAll.Checked,
                                cbHRDateRange.Checked, dtpHRStartDate.Value, dtpHRStopDate.Value,
                                mFrm1.mHamLogLogName);
            }
        }

        /// <summary>
        /// The user pressed the panel above the QSO Date field.  This will
        /// put the current date into the QSO Date field. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHRDate_Click(object sender, EventArgs e)
        {
            dtpHRDate.Value = DateTime.Now.Date;
            dtpHRTimeOn.Value = dtpHRDate.Value;
        }

        /// <summary>
        /// The user has pressed the panel above the QSO Time Off field.
        /// Put the current Date/Time into the Time Off field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHRTimeOff_Click(object sender, EventArgs e)
        {
            dtpHRTimeOff.Value = DateTime.Now;
        }

        /// <summary>
        /// The QSO Call "?" pressed, lookup the call to get the
        /// name, address and the like.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHReqrz_Click(object sender, EventArgs e)
        {
            string Call = "";

            Call = tbHRCall.Text.Trim();
            if (Call.Length > 0)
            {
                CallLookup qrz = mFrm1.QRZLookup(tbHRCall.Text.Trim());
                if (qrz != null)
                {
                    tbHRName.Text = qrz.FirstName + " " + qrz.LastName;
                    tbHRRemarks.Text = qrz.Addr1 + "\r\n" +
                                       qrz.Addr2 + ", " + qrz.State + " " + qrz.Zip + "\r\n" +
                                       qrz.Country + "\r\n" +
                                       qrz.Latitude + ":" +
                                       qrz.Longitude + "\r\n" +
                                       "Grid: " + qrz.Grid + "\r\n" +
                                       "DXCC: " + qrz.DXCC + "\r\n" +
                                       "CQ Zone: " + qrz.CQZone + "\r\n" +
                                       "ITU Zone: " + qrz.ITUZone;
                }
            }

            else
            {
                MessageBox.Show("You must have a callsign to lookup", mFrm1.ProgramName + " input error");
            }
        }

        #endregion

        #region Form Events Handled by Form1 routines.

        /// <summary>
        /// The call sign text has change. If we are showing the Worked Before
        /// we need to send the updated call to that window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHRCall_TabIndexChanged(object sender, EventArgs e)
        {
            mFrm1.CallTextChanged(tbHRCall.Text.Trim());
        }

        /// <summary>
        /// The user has selected a country, Build the list of States (if any).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbHRCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            int code = 0;
            string countrycode = cmbHRCountry.SelectedValue.ToString();
            if (!int.TryParse(countrycode, out code))
            {
                code = 0;
            }

            if (code > 0)
            {
                mFrm1.FillStateComboBox(cmbHRState, code);
            }
            else
            {
                cmbHRState.DataSource = null;
                cmbHRState.Enabled = false;
            }
        }

        /// <summary>
        /// The user has selected a State, Build the list of Counties (if any).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbHRState_SelectedValueChanged(object sender, EventArgs e)
        {
            int code = 0;
            if (cmbHRState.SelectedValue != null)
            {
                string statecode = cmbHRState.SelectedValue.ToString();
                if (!int.TryParse(statecode, out code))
                {
                    code = 0;
                }
            }

            if (code > 0)
            {
                mFrm1.FillCountyComboBox(cmbHRCounty, code);
            }
            else
            {
                if ((cmbHRCountry.SelectedValue == null) || (0 == (int)cmbHRCountry.SelectedValue))
                {
                    cmbHRCounty.DataSource = null;
                    cmbHRCounty.Enabled = false;
                }
            }
        }

        #endregion

        private void frmViewHamLog_ResizeEnd(object sender, EventArgs e)
        {
            mFrm1.HamLogViewSize.Width = this.Width;
            mFrm1.HamLogViewSize.Height = this.Height;
        }

        /// <summary>
        /// If we are using the Worked before form, and if we have at 
        /// lease 3 characters, search the log for possible worked before
        /// situations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHRCall_TextChanged(object sender, EventArgs e)
        {
            string val = "";

            val = ((TextBox)sender).Text.Trim();
            if (val.Length > 2)
            {
                if (mFrm1.WorkedBefore != null)
                {
                    mFrm1.WorkedBefore.CallSignChanged(val);
                }
            }
        }
    }
}
