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
        private MySqlConnection mConn;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private int mTabId = -1;

        public frmSetupTab(MySqlConnection conn, int tabId)
        {
            InitializeComponent();
            
            mConn = conn;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mConn);
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
                Datalayer.ehr.Models.tabItem tab = eCtrl.GetTab(mTabId);
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
            Datalayer.ehr.Models.tabItem tab = new Datalayer.ehr.Models.tabItem();
            tab.TabName = tbName.Text.Trim();
            tab.TabDesc = tbDesc.Text.Trim();
            tab.IsActive = cbActive.Checked;
            tab.DisplaySeq = Convert.ToInt32(tbSeq.Text.Trim());
            if (mTabId == -1)
            {
                eCtrl.WriteTab(tab);
            }
            else
            {
                tab.TabId = mTabId;
                eCtrl.WriteTab(tab);
            }
            this.Close();
        }

    }
}
