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
    public partial class frmSetupTab : Form
    {
        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private int mTabId = -1;
        private string mMod = "frmSetupTab";

        public frmSetupTab(EZDeskDataLayer.EZDeskCommon common, int tabId)
        {
            InitializeComponent();

            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            mTabId = tabId;

            if (mTabId < 0)
            {
                this.Text = "New Tab";

                tbName.Text = "";
                tbDesc.Text = "";
                tbSeq.Text = "";
                cbActive.Checked = true;

                label4.Text = "";
            }
            else
            {
                EZDeskDataLayer.ehr.Models.tabItem tab = eCtrl.GetTab(mTabId);
                this.Text = "Edit tab " + tab.TabName;
                
                tbName.Text = tab.TabName;
                tbDesc.Text = tab.TabDesc;
                tbSeq.Text = tab.DisplaySeq.ToString();
                cbActive.Checked = tab.IsActive;

                label4.Text = tab.TabId.ToString();
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

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "cmdOK_Click"));
            
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
                    //No errors were found, add/update the item.
                    EZDeskDataLayer.ehr.Models.tabItem tab = new EZDeskDataLayer.ehr.Models.tabItem();
                    tab.TabName = tbName.Text.Trim();
                    tab.TabDesc = tbDesc.Text.Trim();
                    tab.IsActive = cbActive.Checked;
                    tab.DisplaySeq = Convert.ToInt32(tbSeq.Text.Trim());

                    tab.TabId = mTabId;
                    eCtrl.WriteTab(tab);

                    this.Close();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdOK_Click failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "cmdOK_Click"));
            }
        }

    }
}
