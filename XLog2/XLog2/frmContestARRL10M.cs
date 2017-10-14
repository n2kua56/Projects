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
    public partial class frmContestARRL10M : Form
    {
        private Form1 mFrm1 = null;

        public frmContestARRL10M(Form1 frm)
        {
            InitializeComponent();
            mFrm1 = frm;
            //TODO: Need to get this value from accumulated time on in the database.
            tbTotalTimeOn.Tag = new TimeSpan(0,0,0);
            tbCall.CharacterCasing = CharacterCasing.Upper;
        }

        private void frmContestARRL10M_Load(object sender, EventArgs e)
        {
            btnSaveUpdate.Text = "Save";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmContestARRL10M_Shown(object sender, EventArgs e)
        {
            tbCall.Focus();
        }

        /// <summary>
        /// The user has pressed the QSO "?" button. Need to do a QRZ
        /// lookup to get the name, address, etc of the callsign in
        /// the QSO Call field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 QRZ lookup routine
        }

        /// <summary>
        /// The user has pressed the On/Off toggle button to indicate
        /// the start/stop of time in the contest. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnOff_Click(object sender, EventArgs e)
        {
            if (btnOnOff.Text.Trim().ToLower() == "on")
            {
                //TODO: Write a record in the time log to indicate the operator is on
                timer1.Enabled = true;
                btnOnOff.Text = "Off";
                tbSessionTimeOn.Tag = DateTime.Now;
            }
            else
            {
                //TODO: Write a record in the time log to indicate the operator is off

                timer1.Enabled = false;

                btnOnOff.Text = "On";

                double diffInSeconds = (DateTime.Now - (DateTime)tbSessionTimeOn.Tag).TotalSeconds;
                TimeSpan t = new TimeSpan(0, 0, (int)diffInSeconds);
                tbTotalTimeOn.Tag = ((TimeSpan)tbTotalTimeOn.Tag) + t;
            }
        }

        /// <summary>
        /// Timer Pop, update the screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            double diffInSeconds = (DateTime.Now - (DateTime)tbSessionTimeOn.Tag).TotalSeconds;
            TimeSpan t = new TimeSpan(0, 0, (int)diffInSeconds);
            tbSessionTimeOn.Text = new DateTime(t.Ticks).ToString("HH:mm:ss");
            TimeSpan total = (TimeSpan)tbTotalTimeOn.Tag;
            total += t;
            if (t.TotalSeconds != 0)
            {
                tbTotalTimeOn.Text = new DateTime(total.Ticks).ToString("HH:mm:ss");
            }
        }

        /// <summary>
        /// User is requesting to clear the QSO data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            zClearQSO();
        }

        /// <summary>
        /// Clear the QSO fields.
        /// </summary>
        private void zClearQSO()
        {
            tbCall.Text = "";
            tbRSTSent.Text = "";
            tbRSTRcvd.Text = "";
            if (cmbState.DataSource != null)
            {
                cmbState.SelectedIndex = 1;
            }
            tbComment.Text = "";
        }

        /// <summary>
        /// The user wants to save or update the QSO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            if ((!timer1.Enabled) && (btnSaveUpdate.Text.Trim().ToLower() == "save"))
            {
                MessageBox.Show("You must be on the clock to Save a new QSO.",
                                mFrm1.ProductName + " Save not allowed",
                                MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if ((tbCall.Text.Trim().Length == 0) ||
                    (tbRSTSent.Text.Trim().Length == 0) ||
                    (tbRSTRcvd.Text.Trim().Length == 0) ||
                    ((int)cmbState.SelectedValue == 1) ||
                    (cmbMode.Text.Trim().Length == 0))
                {
                    MessageBox.Show("Call, RS(T)Sent, RS(T)Rcvd, State and Mode are required",
                                    mFrm1.ProgramName + " Input Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    //TODO: Fix the first parameter, ID
                    //TODO: Fix the last parameter, LogName
                    QSO qso = new QSO(0, tbCall.Text.Trim(),
                                        DateTime.Now,
                                        tbRSTSent.Text.Trim(),
                                        tbRSTRcvd.Text.Trim(),
                                        (int)cmbState.SelectedValue,
                                        tbComment.Text.Trim(),
                                        cmbMode.Text.Trim(),
                                        "logname");
                    //TODO: Call the Form1 SaveUpdateQSO routine
                    zFillInScoringBox();
                    zFillInStatesBox();
                    zClearQSO();
                    btnSaveUpdate.Text = "Save";
                }
            }
        }

        /// <summary>
        /// Fill-In the Scoring box
        /// </summary>
        private void zFillInScoringBox()
        {
            //TODO: ??????
        }

        /// <summary>
        /// Fill-in the States box (hilight states worked)
        /// </summary>
        private void zFillInStatesBox()
        {
            //TODO: ??????
        }

        /// <summary>
        /// The grid needs to be formated
        /// </summary>
        public void FormatLogGrid()
        {
            //TODO: ??????
        }

        /// <summary>
        /// The user wants to Edit a Log entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //TODO: ??????
        }

        /// <summary>
        /// The user wants to Edit a QSO (from context menu)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: ??????
        }

        /// <summary>
        /// User is going to Edit a QSO
        /// </summary>
        private void zEditQSO()
        {
            btnSaveUpdate.Text = "Update";
            //TODO: Call the Form1 EditQSO routine return QSO
            QSO qso = new QSO();
            tbCall.Text = qso.Call;
            tbRSTSent.Text = qso.TxRST;
            tbRSTRcvd.Text = qso.RxRST;
            cmbState.SelectedValue = qso.StateCode;
            tbComment.Text = qso.Remarks;

            zEditModeLocks(false);

            tbCall.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void zEditModeLocks(bool state)
        {
            dataGridView1.Enabled = state;
            editToolStripMenuItem.Enabled = state;
            deleteToolStripMenuItem.Enabled = state;
        }

        /// <summary>
        /// The user wants to delete the specifed QSO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: ??????
        }

    }
}
