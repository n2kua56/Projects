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
    public partial class frmNewLog : Form
    {
        private Form1 mFrm1 = null;
        private int mHeight = 0;
        private int mWidth = 0;

        internal bool logName = false;
        internal bool QSONumber = false;
        internal bool Date = false;
        internal bool UTC = false;
        internal bool UTCend = false;
        internal bool Call = false;
        internal bool Frequency = false;
        internal bool Mode = false;
        internal bool TX = false;
        internal bool RX = false;
        internal bool Awards = false;
        internal bool QslOut = false;
        internal bool QslIn = false;
        internal bool Power = false;
        internal bool contactName = false;
        internal bool QTH = false;
        internal bool Locator = false;
        internal bool Unknown1 = false;
        internal string Unknown1Name = "";
        internal bool Unknown2 = false;
        internal string Unknown2Name = "";
        internal bool Remarks = false;
        internal bool Country = false;
        internal bool State = false;
        internal bool County = false;

        public frmNewLog(Form1 frm1)
        {
            InitializeComponent();
            mFrm1 = frm1;
        }

        private void frmNewLog_Load(object sender, EventArgs e)
        {
            mHeight = this.Height;
            mWidth = this.Width;

            mFrm1.zCenterDialog(this);
        }

        /// <summary>
        /// The user has decided to contine... we have to go to the
        /// Log Editor to build the log.  The user can still cancel 
        /// from there.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (tbLogName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must first enter a new log name", mFrm1.ProgramName + " - Error");
            }
            else
            {
                frmLogEditor frm = new XLog2.frmLogEditor(tbLogName.Text.Trim(), mFrm1);

                frm.lblLogName.Text = "Log: " + tbLogName.Text;
                frm.tbUnknown1.Enabled = false;
                frm.tbUnknown2.Enabled = false;
                frm.lblUnknown1.Enabled = false;
                frm.lblUnknown2.Enabled = false;

                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    logName = frm.cbLogName.Checked;
                    QSONumber = frm.cbQSONumber.Checked;
                    Date = frm.cbDate.Checked;
                    UTC = frm.cbUTC.Checked;
                    UTCend = frm.cbUTCend.Checked;
                    Call = frm.cbCall.Checked;
                    Frequency = frm.cbFrequency.Checked;
                    Mode = frm.cbMode.Checked;
                    TX = frm.cbTX.Checked;
                    RX = frm.cbRX.Checked;
                    Awards = frm.cbAwards.Checked;
                    QslOut = frm.cbQslOut.Checked;
                    QslIn = frm.cbQslIn.Checked;
                    Power = frm.cbPower.Checked;
                    contactName = frm.cbName.Checked;
                    QTH = frm.cbQTH.Checked;
                    Locator = frm.cbLocator.Checked;

                    Unknown1 = frm.cbUnknown1.Checked;
                    Unknown1Name = "UNKNOWN1";
                    if (Unknown1)
                    {
                        if (frm.tbUnknown1.Text.Trim().Length > 0)
                        {
                            Unknown1Name = frm.tbUnknown1.Text.Trim();
                        }
                    }

                    Unknown2 = frm.cbUnknown2.Checked;
                    string Unknown2Name = "UNKNOWN2";
                    if (Unknown2)
                    {
                        if (frm.tbUnknown2.Text.Trim().Length > 0)
                        {
                            Unknown2Name = frm.tbUnknown2.Text.Trim();
                        }
                    }

                    Remarks = frm.cbRemarks.Checked;

                    Country = frm.cbCountry.Checked;
                    State = frm.cbState.Checked;
                    County = frm.cbCounty.Checked;

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
                frm.Close();
            }
        }

        /// <summary>
        /// Don't allow resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNewLog_Resize(object sender, EventArgs e)
        {
            this.Width = mWidth;
            this.Height = mHeight;
        }
    }
}
