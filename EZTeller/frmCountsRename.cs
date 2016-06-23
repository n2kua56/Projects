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
    public partial class frmCountsRename : Form
    {
        private Form1 mFrm;
        private string mModName = "Teller_frmCountsRename";
        private int mWidth;
        private int mHeight;
        private EZDeskCommon mCommon;

        /// <summary>
        /// 
        /// </summary>
        public string NewCountDate
        {
            get
            {
                return dateTimePicker1.Value.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string NewCountBatch
        {
            get
            {
                return numericUpDown1.Value.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="fromBatch"></param>
        /// <param name="frm"></param>
        public frmCountsRename(EZDeskCommon Common, string fromDate, string fromBatch, Form1 frm)
        {
            InitializeComponent();
            mFrm = frm;
            mCommon = Common;
            mWidth = this.Width;
            mHeight = this.Height;

            tbFromDate.Text = fromDate;
            tbFromBatch.Text = fromBatch.ToString();
        }

        private void frmCountsRename_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCountsRename_Resize(object sender, EventArgs e)
        {
            this.Height = mHeight;
            this.Width = mWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            string newDate = "";
            string newBatch = "";

            Trace.Enter(Trace.RtnName(mModName, "RenameCount"));

            try
            {
                newDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                newBatch = numericUpDown1.Value.ToString().Trim();

                //? if (mFrm.EZTellerDataLayer.CountExist(newDate, newBatch))
                //? {
                //?     MessageBox.Show("That Date/Batch already exists", "Rename Error",
                //?                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                //? }

                //? else
                //? {
                //?     this.DialogResult = DialogResult.OK;
                //? }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "RenameCount"));
            }
        }

    }
}
