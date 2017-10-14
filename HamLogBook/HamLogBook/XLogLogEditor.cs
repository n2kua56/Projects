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
    public partial class XLogLogEditor : Form
    {
        private string mOpenType = "";

        public XLogLogEditor(string openType)
        {
            InitializeComponent();
            mOpenType = openType;

            //If it's a new log, default the Show/Hide comboboxes
            if (mOpenType.ToUpper() == "NEW")
            {
                cbEndTime.SelectedItem = "Show";
                cbAwards.SelectedItem = "Hide";
                cbQSL.SelectedItem = "Hide";
                cbPower.SelectedItem = "Hide";
                cbName.SelectedItem = "Hide";
                cbQTH.SelectedItem = "Hide";
                cbQTHLocator.SelectedItem = "Hide";
                cbUnknown1.SelectedItem = "Hide";
                cbUnknown2.SelectedItem = "Hide";
                cbRemarks.SelectedItem = "Hide";
            }
            else
            {

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void XLogLogEditor_Load(object sender, EventArgs e)
        {
        }
    }
}
