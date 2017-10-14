using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class HLBInputBox : UserControl
    {
        private int mHeight = 45;

        public TextBox textBox1
        {
            get { return tb1; }
            set { tb1 = value; }
        }

        public Label label1
        {
            get { return lbl; }
            set { lbl = value; }
        }

        public HLBInputBox()
        {
            InitializeComponent();
        }

        private void HLBInputBox_Resize(object sender, EventArgs e)
        {
            this.Height = mHeight;
        }

        private void HLBInputBox_Load(object sender, EventArgs e)
        {
        }

        private void lbl_TextChanged(object sender, EventArgs e)
        {
            int i = 5;
        }
    }
}
