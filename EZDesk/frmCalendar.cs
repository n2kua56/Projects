using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZDeskDataLayer;

namespace EZDesk
{
    public partial class frmCalendar : Form
    {
        private EZDeskDataLayer.EZDeskCommon mCommon = null;

        public frmCalendar(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                //calendar1.S
            }
        }
    }
}
