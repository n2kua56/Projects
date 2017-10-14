using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoDealerAdmin
{
    public partial class frmVehicle : Form
    {
        private string mAction = "";
        private DataTable mDt = null;

        public string VehName { get; set; }
        public int VehCatId { get; set; }
        public string VehVIN { get; set; }
        public double VehPrice { get; set; }
        public DateTime VehStart { get; set; }
        public DateTime VehEnd { get; set; }
        public int VehFeatured { get; set; }
        public string VehSmallPic { get; set; }
        public int VehSmallPicUpload { get; set; }
        public string VehLargePic { get; set; }
        public int VehLargePicUpload { get; set; }
        public string VehDesc { get; set; }
        public string VehPageText { get; set; }

        public string StagingPath { get; set; }
        public string SmallPicPath { get; set; }
        public string LargePicPath { get; set; }
        public string GallaryPath { get; set; }
        public DataTable GallaryPicTable { get; set; }

        public frmVehicle()
        {
            InitializeComponent();
        }

        //TODO: Gallery - How do I assign "Large" picture file name?

        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        /// <param name="rec"></param>
        public frmVehicle(string act, int rec, DataTable dt)
        {
            mAction = act.ToLower();
            InitializeComponent();
            tbVehRecNo.Text = rec.ToString();
            mDt = dt;
            cbVehCatId.DataSource = mDt;
            cbVehCatId.DisplayMember = "Name";
            cbVehCatId.ValueMember = "Id";
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVehicle_Load(object sender, EventArgs e)
        {
            cbVehCatId.SelectedValue = VehCatId;
            switch (mAction)
            {
                case "add":
                    this.Text = "Add Vehicle";
                    btnOK.Text = "Add";
                    zFillInForm();
                    break;

                case "edit":
                    this.Text = "Edit Vehicle - " + VehName;
                    btnOK.Text = "Update";
                    zFillInForm();
                    zFillInPics();
                    break;

                case "delete":
                    this.Text = "Delete Vehicle - " + VehName;
                    btnOK.Text = "Delete";
                    tbVehName.ReadOnly = true;
                    cbVehCatId.Enabled = false;
                    tbVehVIN.ReadOnly = true;
                    tbVehPrice.ReadOnly = true;
                    dtpVehStart.Enabled = false;
                    dtpVehEnd.Enabled = false;
                    zFillInForm();
                    zFillInPics();
                    break;

                default:
                    this.Text = "Unknown Vehicle Operation";
                    break;
            }
            fsLargePic.fsRootPath = Path.Combine(StagingPath, LargePicPath);
            fsLargePic.fsFileName = VehLargePic;
            fsLargePic.fsExtentions = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            fsLargePic.fsTitle = "Select the Large Picture file";
            fsLargePic.fsInitialPath = fsLargePic.fsRootPath;

            fsSmallPic.fsRootPath = Path.Combine(StagingPath, SmallPicPath);
            fsSmallPic.fsFileName = VehSmallPic;
            fsSmallPic.fsExtentions = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            fsSmallPic.fsTitle = "Select the Small Picture file";
            fsSmallPic.fsInitialPath = fsSmallPic.fsRootPath;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInForm()
        {
            tbVehName.Text = VehName;
            cbVehCatId.SelectedValue = VehCatId;
            tbVehVIN.Text = VehVIN;
            tbVehPrice.Text = VehPrice.ToString("##,###.#0");
            dtpVehStart.Value = VehStart;

            if (VehEnd != DateTime.MaxValue)
            {
                dtpVehEnd.Value = VehEnd;
            }
            
            tbVehDesc.Text = VehDesc;
            tbVehPageText.Text = VehPageText;
            cbVehFeatured.Checked = (VehFeatured == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInPics()
        {
            string filePathName = "";

            pictureBox1.Visible = false;
            if ((SmallPicPath.Trim().Length > 0) &&
                (VehSmallPic.Trim().Length > 0))
            {
                filePathName = Path.Combine(StagingPath, SmallPicPath.Trim(), VehSmallPic.Trim());
                if (File.Exists(filePathName))
                {
                    pictureBox1.Visible = true;
                    pictureBox1.ImageLocation = filePathName;
                }
            }
            cbSmallPicUpload.Checked = VehSmallPicUpload == 1;

            pictureBox2.Visible = false;
            if ((LargePicPath.Trim().Length > 0) &&
                (VehLargePic.Trim().Length > 0))
            {
                filePathName = Path.Combine(StagingPath, LargePicPath.Trim(), VehLargePic.Trim());
                if (File.Exists(filePathName))
                {
                    pictureBox2.Visible = true;
                    pictureBox2.ImageLocation = filePathName;
                }
            }
            cbLargePicUpload.Checked = VehLargePicUpload == 1;

            listBox1.Enabled = false;
            listBox1.DataSource = GallaryPicTable;
            listBox1.DisplayMember = "GallaryPic";
            listBox1.ValueMember = "Id";
            listBox1.Enabled = true;
            if (listBox1.Items.Count > 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmVehicle_Shown(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            int OkToReturn = 1;

            if (tbVehName.Text.Trim().Length == 0)
            {
                MessageBox.Show("You MUST specify a name for the Vehicle.",
                            "Input Error");
                OkToReturn = 0;
                tbVehName.Focus();
                Application.DoEvents();
            }

            //if ((OkToReturn == 1) && ())

            if (OkToReturn == 1)
            {
                VehName = tbVehName.Text.Trim();
                VehCatId = (int)cbVehCatId.SelectedValue;
                VehVIN = tbVehVIN.Text.Trim();

                if (tbVehPrice.Text.Trim().Length == 0)
                {
                    tbVehPrice.Text = "0.00";
                }
                VehPrice = Convert.ToDouble(tbVehPrice.Text.Trim());

                VehSmallPic = fsSmallPic.fsFileName;
                VehLargePic = fsLargePic.fsFileName;
                VehDesc = tbVehDesc.Text.Trim();
                VehPageText = tbVehPageText.Text.Trim();
                VehStart = dtpVehStart.Value;
                VehEnd = dtpVehEnd.Value;

                if (cbVehFeatured.Checked)
                {
                    VehFeatured = 1;
                } 
                else
                {
                    VehFeatured = 0;
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbVehPrice_Leave(object sender, EventArgs e)
        {
            string val = "";
            double dVal = -1;

            val = tbVehPrice.Text.Trim();
            if (!double.TryParse(val, out dVal))
            {
                MessageBox.Show("You MUST specify a valid dollar amount.", "Input Error");
                tbVehPrice.Focus();
                Application.DoEvents();
            }
            else
            {
                tbVehPrice.Text = dVal.ToString("##,###.##");
            }
        }

        private void fsSmallPic_fsValueChanged(object sender, EventArgs e)
        {
            VehSmallPic = fsSmallPic.fsFileName;
            zFillInPics();
        }

        private void fsLargePic_fsValueChanged(object sender, EventArgs e)
        {
            VehLargePic = fsLargePic.fsFileName;
            zFillInPics();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fileName = ((DataRowView)listBox1.SelectedItem)["GallaryPic"].ToString();
            fileName = Path.Combine(GallaryPath, fileName);
            fileName = Path.Combine(StagingPath, fileName);
            fileName = fileName.Replace("\\", "/");
            pictureBox3.ImageLocation = fileName;
        }
    }
}
