using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using EZDeskDataLayer;

namespace EZTeller
{
    public partial class frmSetup : Form
    {
        private string mModName = "Teller_Form1";
        private int inSetup = 0;

        private EZDeskCommon mCommon;
        private Form mEZDeskForm1 = null;
        public frmSetup(EZDeskCommon Common, Form ezdeskForm1)
        {
            InitializeComponent();
            mCommon = Common;
            mEZDeskForm1 = ezdeskForm1;
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready", "Info");
        }

        private void cmdChange_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready", "Info");
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "frmSetup_Load"));

            try
            {
                inSetup = 1;
                tbCash.Text = mCommon.eCtrl.GetProperty(tbCash.Tag.ToString());
                tbSpecial.Text = mCommon.eCtrl.GetProperty(tbSpecial.Tag.ToString());
                tbEnvFirst.Text = mCommon.eCtrl.GetProperty(tbEnvFirst.Tag.ToString());
                tbEnvLast.Text = mCommon.eCtrl.GetProperty(tbEnvLast.Tag.ToString());
                tbName.Text = mCommon.eCtrl.GetProperty(tbName.Tag.ToString());
                tbInitials.Text = mCommon.eCtrl.GetProperty(tbInitials.Tag.ToString());
                inSetup = 0;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("frmSetup_Load failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmSetup_Load"));
            }
        }

        private void tbCash_TextChanged(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "tbCash_TextChanged"));
            string key = "";

            try
            {
                if (inSetup == 0)
                {
                    EZDeskDataLayer.ehr.Models.AvailablePropertyItem prop =
                        new EZDeskDataLayer.ehr.Models.AvailablePropertyItem();

                    prop.PropertyValue = ((TextBox)sender).Text;
                    prop.Modified = DateTime.Now;
                    prop.IsActive = true;
                    key = ((TextBox)sender).Tag.ToString();
                    prop.PropertyName = key;

                    if (sender == tbCash) { prop.Description = "Cash 'Envelope' number"; }
                    if (sender == tbSpecial) { prop.Description = "Special 'Envelope' number"; }
                    if (sender == tbEnvFirst) { prop.Description = "First 'Envelope' number in use"; }
                    if (sender == tbEnvLast) { prop.Description = "Last 'Envelope' number in use"; }
                    if (sender == tbName) { prop.Description = "The Church name"; }

                    mCommon.eCtrl.WriteAvailablePropertyItem(prop);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("tbCash_TextChanged failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tbCash_TextChanged"));
            }
        }

        private void tbCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < '0') || (e.KeyChar > '9')) && (e.KeyChar != '\b'))
            {
                char c = e.KeyChar;
                e.Handled = true;
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
