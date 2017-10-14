using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class HLBDateTimePicker : UserControl
    {
        private int mHeight = 45;

        public Label Label1
        {
            get { return label1; }
            set { label1 = value; }
        }

        public DateTimePicker DateTimePicker1
        {
            get { return dateTimePicker1; }
            set { dateTimePicker1 = value; }
        }

        public HLBDateTimePicker()
        {
            InitializeComponent();
        }

        private void HLBDateTimePicker_Resize(object sender, EventArgs e)
        {
            this.Height = mHeight;
        }
    }
}
