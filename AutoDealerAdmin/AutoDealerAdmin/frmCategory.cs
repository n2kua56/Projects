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
    public partial class frmCategory : Form
    {
        private string mAction = "";

        public string CatName { get; set; }
        public string CatDesc { get; set; }
        public int CatSeq { get; set; }
        public string catPageDesc { get; set; }

        public frmCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        /// <param name="rec"></param>
        public frmCategory(string act, int rec)
        {
            mAction = act.ToLower();
            InitializeComponent();
            tbCatId.Text = rec.ToString();
            tbCatName.Focus();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            switch (mAction)
            {
                case "add":
                    this.Text = "Add Category";
                    btnOK.Text = "Add";
                    tbCatSeq.Text = "5";
                    zFillInForm();
                    break;

                case "edit":
                    this.Text = "Edit Category";
                    btnOK.Text = "Update";
                    zFillInForm();
                    break;

                case "delete":
                    this.Text = "Delete Category";
                    btnOK.Text = "Delete";
                    tbCatName.ReadOnly = true;
                    tbCatSeq.ReadOnly = true;
                    tbCatDesc.ReadOnly = true;
                    tbCatPageDesc.ReadOnly = true;
                    zFillInForm();
                    break;

                default:
                    this.Text = "Unknown Category Operation";
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInForm()
        {
            tbCatName.Text = CatName;
            tbCatDesc.Text = CatDesc;
            tbCatSeq.Text = CatSeq.ToString();
            tbCatPageDesc.Text = catPageDesc;
            tbCatName.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCategory_Shown(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int value = -1;
            int ready = 1;

            if (tbCatName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must specify a name", "Input Error");
                tbCatName.Focus();
                ready = 0;
            }

            if ((ready == 1) && (tbCatSeq.Text.Trim().Length == 0))
            {
                MessageBox.Show("You must specify a sequence number", "Input Error");
                tbCatSeq.Focus();
                ready = 0;
            }

            if (ready == 1)
            {
                if (!int.TryParse(tbCatSeq.Text.Trim(), out value))
                {
                    MessageBox.Show("The sequence number must be a number", "Input Error");
                    tbCatSeq.Focus();
                    ready = 0;
                }
            }

            if (ready == 1)
            {
                CatName = tbCatName.Text.Trim();
                CatDesc = tbCatDesc.Text.Trim();
                CatSeq = Convert.ToInt32(tbCatSeq.Text.Trim());
                catPageDesc = tbCatPageDesc.Text.Trim();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
