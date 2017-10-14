using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmDefaults : Form
    {
        private Form1 mFrm1 = null;
        string mLogName = "";

        public frmDefaults(Form1 frm1, string LogName)
        {
            InitializeComponent();
            mFrm1 = frm1;
            mLogName = LogName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            mFrm1.mDac.AddUpdateDefault(mLogName, "Frequency", tbFrequency.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Mode", tbMode.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Tx", tbTx.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Rx", tbRx.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Awards", tbAwards.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Power", tbPower.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Unknown1", tbUnknown1.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Unknown2", tbUnknown2.Text.Trim());
            mFrm1.mDac.AddUpdateDefault(mLogName, "Remarks", tbRemarks.Text.Trim());
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDefaults_Load(object sender, EventArgs e)
        {
            tbFrequency.Text = mFrm1.mDac.GetDefault(mLogName, "Frequency");
            tbMode.Text = mFrm1.mDac.GetDefault(mLogName, "Mode");
            tbTx.Text = mFrm1.mDac.GetDefault(mLogName, "Tx");
            tbRx.Text = mFrm1.mDac.GetDefault(mLogName, "Rx");
            tbAwards.Text = mFrm1.mDac.GetDefault(mLogName, "Awards");
            tbPower.Text = mFrm1.mDac.GetDefault(mLogName, "Power");
            tbUnknown1.Text = mFrm1.mDac.GetDefault(mLogName, "Unknown1");
            tbUnknown2.Text = mFrm1.mDac.GetDefault(mLogName, "Unknown2");
            tbRemarks.Text = mFrm1.mDac.GetDefault(mLogName, "Remarks");

            string lblText = mFrm1.mDac.GetDefault(mLogName, "Unknown1Name");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN1"; }
            lblUnknown1.Text = lblText;

            lblText = mFrm1.mDac.GetDefault(mLogName, "Unknown2Name");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN2"; }
            lblUnknown2.Text = lblText;

            mFrm1.zCenterDialog(this);
        }
    }
}
