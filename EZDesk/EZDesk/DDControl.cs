using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Dropdown_Button {

    public delegate void ItemClickedDelegate(object sender, ToolStripItemClickedEventArgs e);

    public partial class DDControl : UserControl {

        public event ItemClickedDelegate ItemClickedEvent;

        public new string Text 
        {
            get { return btnDropDown.Text; }
            set { btnDropDown.Text = value; }  
        }

        #region Members

        public List<string> LstOfValues = new List<string>();

        #endregion

        #region Constructors

        public DDControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lst"></param>
        public void FillControlList(List<string> lst) 
        {
            LstOfValues = lst;
            SetMyButtonProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ShowDropDown() 
        {
            try
            {
                ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
                //get the path of the image
                string imgPath = GetFilePath();
                //adding contextMenuStrip items acconrding to LstOfValues count
                for (int i = 0; i < LstOfValues.Count; i++)
                {
                    //add the item
                    contextMenuStrip.Items.Add(LstOfValues[i]);
                    //add the image
                    if (File.Exists(imgPath + @"icon" + i + ".bmp"))
                    {
                        contextMenuStrip.Items[i].Image = 
                            Image.FromFile(imgPath + @"icon" + i + ".bmp");
                    }
                }
                //adding ItemClicked event to contextMenuStrip
                contextMenuStrip.ItemClicked += contextMenuStrip_ItemClicked;
                //show menu strip control
                contextMenuStrip.Show(btnDropDown, new Point(0, btnDropDown.Height));
            }
            catch (Exception ex)
            {
                string msg = "ShowDropDown failed: " + ex.Message;
                Trace.WriteLine(msg, "DDControl");
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMyButtonProperties() 
        {
            // Assign an image to the button.
            string imgPath = GetFilePath();
            btnDropDown.Image = Image.FromFile(imgPath + @"arrow.png");
            // Align the image right of the button
            btnDropDown.ImageAlign = ContentAlignment.MiddleRight;
            //Align the text left of the button.
            btnDropDown.TextAlign = ContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetFilePath() 
        {
            string value = Application.StartupPath.Substring(Application.StartupPath.IndexOf(@"bin", System.StringComparison.Ordinal));
            return Application.StartupPath.Replace(value, string.Empty);
        }


        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDropDown_Click(object sender, EventArgs e) 
        {
            try 
            {
                ShowDropDown();
            }
            catch (Exception ex) 
            {
                string msg = "btnDropDown_Click failed: " + ex.Message;
                Trace.WriteLine(msg, "DDControl");
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e) 
        {
            try {
                ToolStripItem item = e.ClickedItem;
                //set the text of the button
                //btnDropDown.Text = item.Text;
                if (ItemClickedEvent != null) 
                {
                    ItemClickedEvent(sender, e);
                }
            }
            catch (Exception ex) 
            {
                string msg = "ItemClicked failed: " + ex.Message;
                System.Diagnostics.Trace.WriteLine(msg, "DDControls");
                MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}
