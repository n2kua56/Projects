using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoDealerAdmin
{
    public partial class PathSearch : UserControl
    {
        /// <summary>
        /// Gets/Sets the label to the left of the path text box.
        /// </summary>
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        /// <summary>
        /// Sets the left position of the Path Text Box
        /// </summary>
        public int PathLeft
        {
            get { return tbPath.Left; }
            set
            {
                tbPath.Left = value;
                zAdjustFieldSizes();
            }
        }

        /// <summary>
        /// Gets/Sets the entered Directory Path.
        /// </summary>
        public string dirPath
        {
            get { return tbPath.Text.Trim(); }
            set { tbPath.Text = value.Trim(); }
        }

        private string mRootPath = "";
        public string RootPath
        {
            get { return mRootPath; }
            set
            {
                string s = "";
                if (value == null)
                {
                    s = "";
                }
                else
                {
                    s = value;
                    s = s.Replace("\\", "/");
                    while (s.Contains("//"))
                    {
                        s = s.Replace("//", "/");
                    }
                }
                mRootPath = s;
            }
        }
        public bool Changed { get; set; }

        public PathSearch()
        {
            InitializeComponent();
        }

        private void label_Resize(object sender, EventArgs e)
        {
            zAdjustFieldSizes();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAdjustFieldSizes()
        {
            tbPath.Width = (btnSearch.Left - tbPath.Left) + 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int okToContinue = 1;   //Assume new path is ok.
            string spath = "";
            string[] pathfields = null;
            string[] rPathFields = null;

            //Try to force the folderBrowserDialog box to the right path
            //TODO: This is not working - Force the folderBrowserDialog to start in the correct directory
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.SelectedPath = RootPath;

            //Now let the user select a folder 
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //Condition selected path to make sure we have "/" delimiters.
                spath = folderBrowserDialog1.SelectedPath;
                spath = spath.Replace("\\", "/");

                if (RootPath.Length > 0)
                {
                    //Seperate the RootPath and the selected paths into
                    // individual directories.
                    pathfields = spath.Split(Path.AltDirectorySeparatorChar);
                    rPathFields = RootPath.Split('/');

                    //The pathfields MUST match the rPathFields.
                    //The RootPath MUST have fewer directories than 
                    // the selected path.
                    if (rPathFields.Length > pathfields.Length)
                    {
                        okToContinue = 0;
                    }

                    //It does... so the directories in the RootPath
                    // MUST match the corresponding directories in
                    // the selected path.
                    else
                    {
                        for (int idx = 0; (idx < rPathFields.Length) && (okToContinue == 1); idx++)
                        {
                            if (pathfields[idx] != rPathFields[idx])
                            {
                                okToContinue = 0;
                            }
                        }
                        //Remove the RootPath directories from the 
                        // selected directories.
                        if (okToContinue == 1)
                        {
                            spath = "";
                            for (int idx = rPathFields.Length; idx < pathfields.Length; idx++)
                            {
                                spath = Path.Combine(spath, pathfields[idx]);
                            }
                        }
                    }
                }

                //If we are still good to go...
                if (okToContinue == 1)
                {
                    spath = spath.Replace("\\", "/");
                    tbPath.Text = spath;
                    Changed = true;
                }

                //Otherwise display an error message and stay
                // in the path text box.
                else
                {
                    MessageBox.Show("You didn't select a path within the staging path:\n" +
                                                RootPath, "Selection Error");
                    tbPath.Focus();
                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PathSearch_Enter(object sender, EventArgs e)
        {
            Changed = false;
        }
    }
}
