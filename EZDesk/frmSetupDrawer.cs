using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EZDesk
{
    public partial class frmSetupDrawer : Form
    {
        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        EZDeskDataLayer.ehr.Models.DrawerItem mDrawer = null;
        private int mDrawerId = -1;

        public frmSetupDrawer(EZDeskDataLayer.EZDeskCommon common, int drawerId)
        {
            InitializeComponent();

            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            mDrawerId = drawerId;

            if (mDrawerId < 0)
            {
                this.Text = "New Drawer";

                tbName.Text = "";
                tbDesc.Text = "";
                tbSeq.Text = "";
                cbActive.Checked = true;

                label4.Text = "";
            }
            else
            {
                mDrawer = eCtrl.GetDrawer(mDrawerId);
                this.Text = "Edit Drawer " + mDrawer.DrawerName;

                tbName.Text = mDrawer.DrawerName;
                tbDesc.Text = mDrawer.DrawerDesc;
                tbSeq.Text = mDrawer.Seq.ToString();
                cbActive.Checked = mDrawer.IsActive;

                label4.Text = mDrawer.DrawerId.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetupTab_Load(object sender, EventArgs e)
        {
            //Nothing here
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            int n;
            bool msg = false;
            Control ctrl = null;

            try
            {
                //Verify the form data. All fields must be filled in, the
                //  sequence field must be numeric (int).
                if (tbName.Text.Trim().Length == 0)
                {
                    msg = true;
                    ctrl = tbName;
                }
                if ((tbDesc.Text.Trim().Length == 0) && (msg == false))
                {
                    msg = true;
                    ctrl = tbDesc;
                }
                if ((tbSeq.Text.Trim().Length == 0) || (!int.TryParse(tbSeq.Text, out n)))
                {
                    msg = true;
                    ctrl = tbSeq;
                }

                //If an error was found we will display an error message and 
                //let the user select to retry or cancel.
                if (msg)
                {
                    DialogResult dr = MessageBox.Show("You must fill-in ALL fields with the correct type", "Input Error",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Retry)
                    {
                        //The user picked Retry, focus on the field that was bad.
                        ctrl.Focus();
                    }
                    else
                    {
                        //The user picked Cancel
                        this.Close();
                    }
                }

                else
                {
                    if (mDrawer == null)
                    {
                        mDrawer = new EZDeskDataLayer.ehr.Models.DrawerItem();
                    }

                    mDrawer.DrawerName = tbName.Text.Trim();
                    mDrawer.DrawerDesc = tbDesc.Text.Trim();
                    mDrawer.IsActive = cbActive.Checked;
                    mDrawer.Seq = Convert.ToInt32(tbSeq.Text.Trim());
                    mDrawer.DrawerId = mDrawerId;
                    eCtrl.WriteDrawer(mDrawer);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdOK_Click failed", ex);
                eze.Add("drawer", mDrawer);
                throw eze;
            }
        }

    }
}
