using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDealerAdmin
{
    public partial class frmProperty : Form
    {
        private string mAction = "";
        
        public string PropName { get; set; }
        public string PropValue { get; set; }
        public string PropDesc { get; set; }

        public frmProperty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        /// <param name="rec"></param>
        public frmProperty(string act, int rec)
        {
            mAction = act.ToLower();
            InitializeComponent();
            tbPropId.Text = rec.ToString();
            tbPropName.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProperty_Load(object sender, EventArgs e)
        {
            switch (mAction)
            {
                case "add":
                    this.Text = "Add Property";
                    btnOK.Text = "Add";
                    break;

                case "edit":
                    this.Text = "Edit Property";
                    btnOK.Text = "Update";
                    tbPropName.ReadOnly = true;
                    zFillInForm();
                    break;

                case "delete":
                    this.Text = "Delete Property";
                    btnOK.Text = "Delete";
                    tbPropName.ReadOnly = true;
                    tbPropValue.ReadOnly = true;
                    tbPropDesc.ReadOnly = true;
                    zFillInForm();
                    break;

                default:
                    this.Text = "Unknown Property Operation";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInForm()
        {
            tbPropName.Text = PropName;
            tbPropValue.Text = PropValue;
            tbPropDesc.Text = PropDesc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int ready = 1;

            if (tbPropName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify a name");
                tbPropName.Focus();
                ready = 0;
            }

            if (tbPropValue.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify a value");
                tbPropValue.Focus();
                ready = 0;
            }

            if (ready == 1)
            {
                PropName = tbPropName.Text.Trim();
                PropValue = tbPropValue.Text.Trim();
                PropDesc = tbPropDesc.Text.Trim();

                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmProperty_Shown(object sender, EventArgs e)
        {
            ////tbPropName.Text = PropName;
            ////tbPropValue.Text = PropValue;
            ////tbPropDesc.Text = PropDesc;
        }
    }
}
