using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EZDesk
{
    public partial class iDash : Form
    {
        public iDash()
        {
            InitializeComponent();

            //#MyBase.New()
            //#Me.MdiParent = pParent
            //#fParent = pParent
            this.Text = "";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Dock = DockStyle.Fill;
        }
    }
}
