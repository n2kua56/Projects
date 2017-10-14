using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmLogEditor : Form
    {
        private int mWidth = 0;
        private int mHeight = 0;
        private string mLogName = "";
        private Form1 mForm1 = null;

        public frmLogEditor(string logName, Form1 form1)
        {
            InitializeComponent();
            mLogName = logName;
            mForm1 = form1;
        }

        private void frmLogEditor_Load(object sender, EventArgs e)
        {
            mWidth = this.Width;
            mHeight = this.Height;
            lblLogName.Text = "LOG: " + mLogName;
            mForm1.zCenterDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUnknown1_CheckedChanged(object sender, EventArgs e)
        {
            lblUnknown1.Enabled = cbUnknown1.Checked;
            tbUnknown1.Enabled = cbUnknown1.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbUnknown2_CheckedChanged(object sender, EventArgs e)
        {
            lblUnknown2.Enabled = cbUnknown2.Checked;
            tbUnknown2.Enabled = cbUnknown2.Checked;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Don't let the user resize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogEditor_Resize(object sender, EventArgs e)
        {
            this.Height = mHeight;
            this.Width = mWidth;
        }
    }
}
