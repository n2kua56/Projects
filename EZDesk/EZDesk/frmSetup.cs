using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EZDesk
{
    public partial class frmSetup : Form
    {
        //TODO:  1) Double-Click on user grid goes to edit
        //TODO:  1) A: On grid change fill-in lower right groupbox
        //TODO:  1) B: Make the frmUser form take in the personid for edits
        //TODO:  2) Implement multiple addresses in frmUser
        //TODO:  3) Implement the "NEW" address button in frmUser
        //TODO:  4) Implement AvailableProperties
        //TODO:  5) Implement Profiles
        //TODO:  6) Implement Setup/Document tabs and drawers
        //TODO:  7) Implement Adding/Searching Patients
        //TODO:  8) Implement "documents" (from files for now - FilePlace)
        //TODO:  9) Implement basic messaging
        //TODO: 10) Implement basic scheduling

        private frmSetupDocuments mfrmDocuments = null;
        private frmSetupUsers mfrmUsers = null;
        private MySqlConnection mConn = null;
        private string mModName = "frmSetup";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        public frmSetup(MySqlConnection Conn)
        {
            InitializeComponent();
            mConn = Conn;
            frmDocuments(mConn);
            
        }

        /// <summary>
        /// Load the iDash form into the hosting panel
        /// </summary>
        private void frmDocuments(MySqlConnection conn)
        {
            try
            {
                mConn = conn;
                if (mfrmDocuments == null)
                {
                    mfrmDocuments = new frmSetupDocuments(mConn);
                    mfrmDocuments.TopLevel = false;
                    mfrmDocuments.Visible = true;
                    mfrmDocuments.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    mfrmDocuments.Width = pnlHosting.Width;
                    mfrmDocuments.Height = pnlHosting.Height;
                    mfrmDocuments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(mfrmDocuments);
                }

                pnlHosting.Controls["frmSetupDocuments"].BringToFront();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", mModName + ".frmDocuments");
                throw ex;
            }
        }

        /// <summary>
        /// Load the iDash form into the hosting panel
        /// </summary>
        private void frmUsers()
        {
            try
            {
                if (mfrmUsers == null)
                {
                    mfrmUsers = new frmSetupUsers(mConn);
                    mfrmUsers.TopLevel = false;
                    mfrmUsers.Visible = true;
                    mfrmUsers.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    mfrmUsers.Width = pnlHosting.Width;
                    mfrmUsers.Height = pnlHosting.Height;
                    mfrmUsers.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(mfrmUsers);
                    mfrmUsers.Init();
                }

                pnlHosting.Controls["frmSetupUsers"].BringToFront();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", mModName + ".frmUsers");
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDocuments_Click(object sender, EventArgs e)
        {
            frmDocuments(mConn);
            //QQQ: catch this and display a message that it couldn't be loaded?
        }

        private void cmdUsers_Click(object sender, EventArgs e)
        {
            frmUsers();
            //QQQ: catch this and display a message that it couldn't be loaded?
        }

        private void cmdSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void cmdIntegration_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void cmdMessaging_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void cmdRx_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void cmdFlowSheet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void cmdMisc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 100;
        }

    }
}
