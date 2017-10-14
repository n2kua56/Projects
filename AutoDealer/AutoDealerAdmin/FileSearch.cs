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
    // Declare a delegate
    public delegate void fsValueChangedEventHandler(object sender, EventArgs e);

    public partial class FileSearch : UserControl
    {
        // Declare an event
        [Category("Action")]
        [Description("Fires when the value is changed")]
        public event fsValueChangedEventHandler fsValueChanged;

        /// <summary>
        /// Gets/Sets the label to the left of the path text box.
        /// </summary>
        public string fsLabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        /// <summary>
        /// Sets the left position of the Path Text Box
        /// </summary>
        public int fsPathLeft
        {
            get { return tbFileName.Left; }
            set
            {
                tbFileName.Left = value;
                //zAdjustFieldSizes();
            }
        }

        string mInitialFileName = "";
        /// <summary>
        /// Gets/Sets the Selected FileName
        /// </summary>
        public string fsFileName
        {
            get { return tbFileName.Text.Trim(); }
            set { tbFileName.Text = value.Trim(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string fsExtentions
        {
            get
            {
                return openFileDialog1.Filter;
            }
            set
            {
                openFileDialog1.Filter = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string fsTitle
        {
            get { return openFileDialog1.Title; }
            set { openFileDialog1.Title = value; }
        }

        public string fsInitialPath
        {
            get { return openFileDialog1.InitialDirectory; }
            set { openFileDialog1.InitialDirectory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private string mRootPath = "";
        public string fsRootPath
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

        /// <summary>
        /// 
        /// </summary>
        public bool fsChanged { get; set; }

        public FileSearch()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAdjustFieldSizes()
        {
            tbFileName.Width = (btnSearch.Left - tbFileName.Left) + 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Resize(object sender, EventArgs e)
        {
            zAdjustFieldSizes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int okToContinue = 1;   //Assume new path is ok.
            string spath = "";
            string[] pathfields = null;
            string[] rPathFields = null;

            //Try to force the folderBrowserDialog box to the right path
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FileName = fsRootPath;

            //Now let the user select a folder 
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //Condition selected path to make sure we have "/" delimiters.
                spath = openFileDialog1.FileName;
                spath = spath.Replace("\\", "/");

                if (fsRootPath.Length > 0)
                {
                    //Seperate the RootPath and the selected paths into
                    // individual directories.
                    pathfields = spath.Split(Path.AltDirectorySeparatorChar);
                    rPathFields = fsRootPath.Split('/');

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
                    tbFileName.Text = spath;
                    fsChanged = true;
                    OnValueChanged(null);
                }

                //Otherwise display an error message and stay
                // in the path text box.
                else
                {
                    MessageBox.Show("You didn't select a path within the staging path:\n" +
                                                fsRootPath, "Selection Error");
                    tbFileName.Focus();
                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSearch_Enter(object sender, EventArgs e)
        {
            fsChanged = false;
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            // Raise the event
            if (fsValueChanged != null)
            {
                fsValueChanged(this, null);
            }
        }

    }
}
