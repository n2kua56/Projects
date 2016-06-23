using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPOELogConsole
{
    public partial class Form1 : Form
    {
        private int mSpliterBar1Offset = 0;
        private string mTitle = "CPOE Log Console";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mSpliterBar1Offset = splitContainer1.SplitterDistance;
            this.Text = mTitle;

            dtpStartDate.Value = DateTime.Now.Date;
            dtpEndDate.Value = dtpStartDate.Value;

            dtpStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpEndTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = mSpliterBar1Offset;
        }

        private void connectToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zSignOn();
        }

        private void zSignOn()
        {
            int tries = 0;
            int loggedOn = 0;
            string userId = "";
            string password = "";
            string server = "";

            frmSignOn frm = new frmSignOn(this.Top, this.Height, this.Left, this.Width);
            while (tries < 3)
            {
                DialogResult dlg = frm.ShowDialog();
                if (dlg == DialogResult.Cancel) { tries = 3; }
                else
                {
                    tries++;
                    userId = frm.UserId;
                    password = frm.Password;
                    //TOOD: Try to log in
                }
            }
            frm.Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            zSignOn();
        }
    }
}
