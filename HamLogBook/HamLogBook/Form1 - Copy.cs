using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class Form1 : Form
    {
        //TODO: 1) Build Table and fill-in Band
        //TODO: 2) Build Table and fill-in Mode
        //TODO: 3) Build Table and fill-in Country
        //TODO: 4) Build Table and fill-in State
        //TODO: 5) Build Table and fill-in County
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string ampm = "";
            string h = "";
            DateTime utc = DateTime.MinValue;
            string utctime = "";

            //Format the localtime using the Property TimeDisplay (12|24)
            string localtime = now.Minute.ToString("00") + ":" + now.Second.ToString("00");
            //IF we are using 12 hour clock then change the 24 hour clock to 12 with the AM|PM appended
            if (Properties.Settings.Default.TimeDisplay == 12)
            {
                ampm = " AM";
                if (now.Hour >= 12) { ampm = " PM"; }
                h = now.Hour.ToString("00");
                if (now.Hour == 0) { h = "12"; }
                if (now.Hour > 12) { h = (now.Hour - 12).ToString("00"); }
            }
            else
            {
                h = now.Hour.ToString("00");
            }
            localtime = h + ":" + localtime + ampm;
            TSStLLocalTime.Text = localtime;

            utc = now.ToUniversalTime();
            utctime = utc.Hour.ToString("00") + ":" + utc.Minute.ToString("00") + ":" + utc.Second.ToString("00") + " UTC";
            TSStLLocalTime.Text += "  " + utctime;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //TODO:
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintLog frm = null;
            frm = new PrintLog();
            frm.ShowDialog();
        }

        private void printAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintAddressLabels frm = null;
            frm = new PrintAddressLabels();
            frm.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Log Database File";
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            //TODO: Add the extention
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //TODO: Open the database
            }
            else
            {
                //TODO: Do nothing
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Log Database File As...";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckFileExists = true;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //TODO: Save the database
            }
            else
            {
                //TODO: Nothing
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: do this
        }

        private void resetCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Do this
        }

        private void showFileLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFileLocations frm = null;
            frm = new ShowFileLocations();
            frm.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings frm = null;
            frm = new Settings();
            frm.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //TODO: Finish this
            MessageBox.Show("Find not implemented yet");
        }

        private void btnLogContact_Click(object sender, EventArgs e)
        {
            //TODO: Finish this
            MessageBox.Show("Log Contact not implemented yet");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //hlbInputCall.textBox1.Text = "";
            dtpDate.Value = DateTime.Now;
            tbPower.Text = "";
            dtpTimeOn.Value = DateTime.Now;
            tbSent.Text = "";
            tbRec.Text = "";
            tbName.Text = "";
            tbFrequency.Text = "";
            tbOther.Text = "";
            dtpTimeOff.Value = DateTime.Now;
            cbQSLSent.Checked = false;
            cbQSLRcvd.Checked = false;
            //cbBand.SelectedIndex = 0;
            //cbMode.SelectedIndex = 0;
            //cbCountry.SelectedIndex = 0;
            //cbState.SelectedIndex = 0;
            //cbCounty.SelectedIndex = 0;
        }

        private void xLog2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XLog2 frm = new XLog2();
            frm.ShowDialog();
        }
    }
}
