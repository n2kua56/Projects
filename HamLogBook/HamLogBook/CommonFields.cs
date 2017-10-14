using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class CommonFields : Form
    {
        //TODO: Use this form in the correct tab (MDIParent)
        int m_StandAlone = 0;
        private DataAccess mDac = null;

        public CommonFields(DataAccess dac, int StandAlone)
        {
            InitializeComponent();
            m_StandAlone = StandAlone;
            mDac = dac;
        }

        private void CommonFields_Load(object sender, EventArgs e)
        {
            string val = "";

            tbCall.Text = mDac.GetProperty("CommonCall");
            tbCountry.Text = mDac.GetProperty("CommonCounty");
            tbContinent.Text = mDac.GetProperty("CommonContinent");
            tbLatitude.Text = mDac.GetProperty("CommonLat");
            tbLongitude.Text = mDac.GetProperty("CommonLong");
            tbOperator.Text = mDac.GetProperty("CommonOperator");
            tbInitials.Text = mDac.GetProperty("CommonInitials");

            if (m_StandAlone == 1)
            {
                panel3.Visible = false;
                label13.Visible = false;
                cbAlwaysDisplay.Visible = false;
                cbAlwaysDisplay.Enabled = false;
                this.Text = "Common Fields";
                button1.Visible = true;
                button1.Enabled = true;
            }
            
            else
            {
                button1.Visible = false;
                button1.Enabled = false;
                val = mDac.GetProperty("CommonAlwaysShow");
                if (val.Trim() == "1")
                {
                    cbAlwaysDisplay.Checked = true;
                }
            }
        }

        private void tbCall_Leave(object sender, EventArgs e)
        {
            string key = "";

            if (sender == tbCall) { key = "CommonCall"; tbCall.Text = tbCall.Text.Trim().ToUpper(); }
            if (sender == tbCountry) { key = "CommonCounty";  }
            if (sender == tbContinent) { key = "CommonContinent"; }
            if (sender == tbLatitude) { key = "CommonLat"; }
            if (sender == tbLongitude) { key = "CommonLong"; }
            if (sender == tbOperator) { key = "CommonOperator"; tbOperator.Text = tbOperator.Text.Trim().ToUpper(); }
            if (sender == tbInitials) { key = "CommonInitials"; tbInitials.Text = tbInitials.Text.Trim().ToUpper(); }

            mDac.SaveProperty(key, ((TextBox)sender).Text.Trim());
        }

        private void cbAlwaysDisplay_CheckedChanged(object sender, EventArgs e)
        {
            string val = "";
            if (cbAlwaysDisplay.Checked)
            {
                val = "1";
            }
            else
            {
                val = "0";
            }
            mDac.SaveProperty("CommonAlwaysShow", val);
        }
        
    }
}
