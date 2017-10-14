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
    public partial class frmDialogs : Form
    {
        private Form1 mFrm1 = null;

        public frmDialogs(Form1 frm1)
        {
            InitializeComponent();
            mFrm1 = frm1;
        }

        private void frmDialogs_Load(object sender, EventArgs e)
        {
            cbLogName.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Logname");
            cbQSONumber.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.QSONum");
            cbDate.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Date");
            cbUTC.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.UTC");
            cbUTCend.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.UTCend");
            cbCall.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Call");
            cbFrequency.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Frequency");
            cbMode.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Mode");
            cbTX.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Tx");
            cbRX.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Rx");
            cbAwards.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Awards");
            cbQslOut.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.QslOut");
            cbQslIn.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.QslIn");
            cbPower.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Power");
            cbName.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Name");
            cbQTH.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.QTH");
            cbLocator.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Locator");
            cbUnknown1.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.UNKNOWN1");
            cbUnknown1.Text = mFrm1.mDac.GetProperty("XLOG.Unknown1Name");
            if (cbUnknown1.Text.Trim().Length == 0) { cbUnknown1.Text = "Unknown1"; }
            cbUnknown2.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.UNKNOWN2");
            cbUnknown2.Text = mFrm1.mDac.GetProperty("XLOG.Unknown2Name");
            if (cbUnknown2.Text.Trim().Length == 0) { cbUnknown2.Text = "Unknown2"; }
            cbRemarks.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Remarks");
            cbCountry.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.Country");
            cbState.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.State");
            cbCounty.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Worked.County");

            cbScoring136.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.0.136");
            cbScoring501.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.0.501");
            cbScoring18.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.1.8");
            cbScoring35.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.3.5");
            cbScoring52.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.5.2");
            cbScoring7.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.7");
            cbScoring10.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.10");
            cbScoring14.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.14");
            cbScoring18M.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.18");
            cbScoring21.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.21");
            cbScoring24.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.24");
            cbScoring28.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.28");
            cbScoring50.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.50");
            cbScoring70.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.70");
            cbScoring144.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.144");
            cbScoring222.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.222");
            cbScoring420.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.420");
            cbScoring902.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.902");
            cbScoring1240.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.1240");
            cbScoring2300.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.2300");
            cbScoring3300.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.3300");
            cbScoring5650.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.5650");
            cbScoring10000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.10000");
            cbScoring24000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.24000");
            cbScoring47000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.47000");
            cbScoring75500.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.75500");
            cbScoring120000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.120000");
            cbScoring142000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.142000");
            cbScoring300000.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.300000");
            cbScoringWAC.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.WAC");
            cbScoringWAS.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.WAS");
            cbScoringWAZ.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.WAZ");
            cbScoringIOTA.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.IOTA");
            cbScoringGridLoc.Checked = "1" == mFrm1.mDac.GetProperty("XLOG2.Scoring.GridLocator");

            mFrm1.zCenterDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Logname", cbLogName.Checked ? "1":"0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.QSONum", cbQSONumber.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Date", cbDate.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.UTC", cbUTC.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.UTCend", cbUTCend.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Call", cbCall.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Frequency", cbFrequency.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Mode", cbMode.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Tx", cbTX.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Rx", cbRX.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Awards", cbAwards.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.QslOut", cbQslOut.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.QslIn", cbQslIn.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Power", cbPower.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Name", cbName.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.QTH", cbQTH.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Locator", cbLocator.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.UNKNOWN1", cbUnknown1.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.UNKNOWN2", cbUnknown2.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Remarks", cbRemarks.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.Country", cbCountry.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.State", cbState.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Worked.County", cbCounty.Checked ? "1" : "0");

            mFrm1.mDac.SaveProperty("XLOG2.Scoring.0.136", cbScoring136.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.0.501", cbScoring501.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.1.8", cbScoring18.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.3.5", cbScoring35.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.5.2", cbScoring52.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.7", cbScoring7.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.10", cbScoring10.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.14", cbScoring14.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.18", cbScoring18M.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.21", cbScoring21.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.24", cbScoring24.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.28", cbScoring28.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.50", cbScoring50.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.70", cbScoring70.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.144", cbScoring144.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.222", cbScoring222.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.420", cbScoring420.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.902", cbScoring902.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.1240", cbScoring1240.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.2300", cbScoring2300.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.3300", cbScoring3300.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.5650", cbScoring5650.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.10000", cbScoring10000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.24000", cbScoring24000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.47000", cbScoring47000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.75500", cbScoring75500.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.120000", cbScoring120000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.142000", cbScoring142000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.300000", cbScoring300000.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.WAC", cbScoringWAC.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.WAS", cbScoringWAS.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.WAZ", cbScoringWAZ.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.IOTA", cbScoringIOTA.Checked ? "1" : "0");
            mFrm1.mDac.SaveProperty("XLOG2.Scoring.GridLocator", cbScoringGridLoc.Text.Trim());

            this.Close();
        }
    }
}
