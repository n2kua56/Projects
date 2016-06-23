using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZUtils;
using EZDeskDataLayer;

namespace EZDesk
{
    public partial class frmFilePlace : Form
    {
        //TODO: Unselect items in the FilePlace ListView as they are handled
        //TODO: Got the msg select person first, but the form came up -- bad!

        EZDeskCommon mCommon = null;
        EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;

        /// <summary>
        /// Gets the setting of the document name type.
        /// </summary>
        public string NameOption 
        {
            get
            {
                string rtn = "";
                if (rbTab.Checked) { rtn = "TabName"; }
                else { rtn = "PCFileName"; }
                return rtn;
            }
        }

        public frmFilePlace(EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);

            //Formst that will be hosted will always have the following
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

            comboBox1.SelectedIndex = 3;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFilePlace_Load(object sender, EventArgs e)
        {
            rbPC.Checked = true;
            Init();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            if (mCommon.Person != null)
            {
                string path = Properties.Settings.Default.FilePlaceDirectory;
                //string path = eCtrl.GetProperty("FilePlacePath");

                lblDirectory.Text = path;
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles();
                
                listView1.Items.Clear();
                listView1.Columns.Clear();

                foreach (FileInfo fi in files)
                {
                    List<string> fields = new List<string>();
                    fields.Add(fi.Name);
                    fields.Add(fi.FullName);
                    fields.Add(fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    fields.Add(((int)fi.Length).ToString("###,###,##0"));
                    ListViewItem i = new ListViewItem(fields.ToArray());
                    listView1.Items.Add(i);
                }
                label1.Text = files.Length.ToString();
            }

            else
            {
                MessageBox.Show("You must first select a Person", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    listView1.View = View.LargeIcon;
                    break;
                case 1:
                    listView1.View = View.Details;
                    listView1.Columns.Clear();
                    ColumnHeader h = new ColumnHeader();
                    h.Text = "File Name";
                    listView1.Columns.Add(h);
                    h = new ColumnHeader();
                    h.Text = "FullPathName";
                    h.Width = 0;
                    listView1.Columns.Add(h);
                    h = new ColumnHeader();
                    h.Text = "Date";
                    listView1.Columns.Add(h);
                    h = new ColumnHeader();
                    h.Text = "Size";
                    h.TextAlign = HorizontalAlignment.Right;
                    listView1.Columns.Add(h);
                    zResize();
                    break;
                case 2:
                    listView1.View = View.SmallIcon;
                    break;
                case 3:
                    listView1.View = View.List;
                    break;
                case 4:
                    listView1.View = View.Tile;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResize()
        {
            if (listView1.View == View.Details)
            {
                listView1.Columns[2].Width = 120;
                listView1.Columns[3].Width = 80;
                double width = (double)listView1.Width;
                width = width - 223;
                width = width * 0.8;
                listView1.Columns[0].Width = (int)width;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_Resize(object sender, EventArgs e)
        {
            zResize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            List<string> data = new List<string>();
            foreach (ListViewItem item in listView1.SelectedItems)   //ListView.SelectedListViewItemCollection)
            {
                data.Add(item.SubItems[1].Text);
            }
            //StatusLabel1.Text = "";
            //StatusLabel1.ForeColor = Color.Black;

            DoDragDrop(data, DragDropEffects.All);
        }

    }
}
