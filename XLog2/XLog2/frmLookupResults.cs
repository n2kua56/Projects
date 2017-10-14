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
    public partial class frmLookupResults : Form
    {
        int mMinWidth = 0;
        int mMinHeight = 0;

        public frmLookupResults() 
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLookupResults_Load(object sender, EventArgs e)
        {
            mMinWidth = this.Width;
            mMinHeight = this.Height;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLookupResults_Resize(object sender, EventArgs e)
        {
            if (this.Width < mMinWidth) { this.Width = mMinWidth; }
            if (this.Height < mMinHeight) { this.Height = mMinHeight; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close(); 
        }
    }
}
