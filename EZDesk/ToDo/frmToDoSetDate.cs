using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToDo
{
    public partial class frmToDoSetDate : Form
    {
        private DateTime? mDte = null;
        public DateTime? SelectedDateTime 
        {
            get { return mDte; }
            set { mDte = value; }
        }

        public frmToDoSetDate(DateTime? dte)
        {
            InitializeComponent();
            mDte = dte;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmToDoSetDate_Load(object sender, EventArgs e)
        {
            DateTime dte = DateTime.Now;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            if (mDte != null)
            {
                mDte = (DateTime)mDte;
            }

            dtpDate.Value = dte;
            dtpTime.Value = dte;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdClearDate_Click(object sender, EventArgs e)
        {
            mDte = null;
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSetDate_Click(object sender, EventArgs e)
        {
            DateTime dte = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day,
                                        dtpTime.Value.Hour, dtpTime.Value.Minute, 0);
            mDte = (DateTime?)dte;
            this.DialogResult = DialogResult.OK;
        }

    }
}
