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
    public partial class frmSignOn : Form
    {
        public string UserId
        {
            get { return tbUser.Text; }
            set { tbUser.Text = value; }
        }

        public string Password
        {
            get { return tbPassword.Text; }
            set { tbPassword.Text = value; }
        }

        public string typ { get; set; }

        public string server
        {
            get { return tbServer.Text; }
            set { tbServer.Text = value; }
        }

        int mLeft = 0;
        int mTop = 0;

        public frmSignOn(int t, int h, int l, int w)
        {
            InitializeComponent();
            mTop = t + ((h - this.Height) / 2);
            mLeft = l + ((w - this.Width) / 2);
        }

        private void tbServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmSignOn_Load(object sender, EventArgs e)
        {
            this.Top = mTop;
            this.Left = mLeft;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbUser.Text.Trim().Length == 0)
        }
    }
}
