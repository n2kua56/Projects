using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OOLib;

namespace OOTest
{
    public partial class Form1 : Form
    {
        ServiceManager serviceManager = null;
        Desktop desktop = null;
        TextDocument textDoc = null;

        public Form1()
        {
            InitializeComponent();
            serviceManager = new ServiceManager();
            desktop = new Desktop();
            textDoc = desktop.CreateTextDocument();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textDoc.Text.String = "Hello World";
            textDoc.StoreAsFileName(textBox1.Text.Trim());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Select file to store the OpenOffice document";
            saveFileDialog1.DefaultExt = "odt";
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox1.Text = saveFileDialog1.FileName;
            }
        }
    }
}
