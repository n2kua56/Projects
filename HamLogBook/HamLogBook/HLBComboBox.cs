using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class HLBComboBox : UserControl
    {
        private int mHeight = 45;

        public Label Label1
        {
            get { return label1; }
            set { label1 = value; }
        }

        public ComboBox ComboBox1
        {
            get { return comboBox1; }
            set { comboBox1 = value; }
        }

        public HLBComboBox()
        {
            InitializeComponent();
        }

        private void HLBComboBox_Resize(object sender, EventArgs e)
        {
            this.Height = mHeight;
        }
    }
}
