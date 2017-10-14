using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class PrintAddressLabels : Form
    {
        public PrintAddressLabels()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbCall.Text = "";
            tbName.Text = "";
            tbAddress.Text = "";
            tbCity.Text = "";
            tbState.Text = "";
            tbZip.Text = "";
            tbCountry.Text = "";
        }
    }
}
