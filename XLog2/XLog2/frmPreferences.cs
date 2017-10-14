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
    public partial class frmPreferences : Form
    {
        private Form1 mForm1 = null;

        public frmPreferences(Form1 form1)
        {
            InitializeComponent();
            mForm1 = form1;
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {
            string val = "";
            mForm1.zCenterDialog(this);
            val = mForm1.mDac.GetProperty("XLOG.EnableClock");
            cbClock.Checked = "true" == val.ToLower();
            cbTypeAndFind.Checked = "true" == mForm1.mDac.GetProperty("XLOG.EnableType").ToLower();
            cbConfirmExit.Checked = "true" == mForm1.mDac.GetProperty("XLOG.ConfirmExit").ToLower();
            if (mForm1.mDac.GetProperty("XLOG.Mode") == "Edit")
            {
                rbModeEditbox.Checked = true;
            }
            else
            {
                rbModeMenu.Checked = true;
            }
            if (mForm1.mDac.GetProperty("XLOG.Bands") == "Edit")
            {
                rbBandEditbox.Checked = true;
            }
            else
            {
                rbBandMenu.Checked = true;
            }

            tbYourCallsign.Text = mForm1.mDac.GetProperty("XLOG.Callsign");
            tbNS.Text = mForm1.mDac.GetProperty("XLOG.Lat");
            tbEW.Text = mForm1.mDac.GetProperty("XLOG.Lon");
            cmbDistance.Text = mForm1.mDac.GetProperty("XLOG.Dist");
            tbGrid.Text = mForm1.mDac.GetProperty("XLOG.Grid");
            tbITUZone.Text = mForm1.mDac.GetProperty("XLOG.ITUZone");
            tbCQZone.Text = mForm1.mDac.GetProperty("XLOG.CQZone");
            tbQRZPassword.Text = mForm1.mDac.GetProperty("XLOG.QRZPassword");
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            mForm1.mDac.SaveProperty("XLOG.EnableClock", cbClock.Checked.ToString());
            mForm1.mDac.SaveProperty("XLOG.EnableType", cbTypeAndFind.Checked.ToString());
            mForm1.mDac.SaveProperty("XLOG.ConfirmExit", cbConfirmExit.Checked.ToString());
            mForm1.mDac.SaveProperty("XLOG.Mode", rbModeEditbox.Checked ? "Edit" : "Combo");
            mForm1.mDac.SaveProperty("XLOG.ModesList", tbModeMenuItems.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.Bands", rbBandEditbox.Checked ? "Edit" : "Combo");
            mForm1.mDac.SaveProperty("XLOG.BandsList", tbBandsList.Text.Trim());

            mForm1.mDac.SaveProperty("XLOG.Callsign", tbYourCallsign.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.Lat", tbNS.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.Lon", tbEW.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.Dist", cmbDistance.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.Grid", tbGrid.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.ITUZone", tbITUZone.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.CQZone", tbCQZone.Text.Trim());
            mForm1.mDac.SaveProperty("XLOG.QRZPassword", tbQRZPassword.Text.Trim());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
