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
    public partial class frmBrowser : Form
    {
        private string mDocumentPath = "";
        private string mTitle = "";

        public frmBrowser(string documentPath, string title)
        {
            InitializeComponent();
            mDocumentPath = documentPath;
            mTitle = title;
        }

        private void frmBrowser_Load(object sender, EventArgs e)
        {
            this.Text = mTitle;
            webBrowser1.Url = new Uri(mDocumentPath);
            webBrowser1.AllowNavigation = true;
            webBrowser1.IsWebBrowserContextMenuEnabled = true;
            webBrowser1.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
            {
                webBrowser1.GoBack();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
            {
                webBrowser1.GoForward();
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }
    }
}
