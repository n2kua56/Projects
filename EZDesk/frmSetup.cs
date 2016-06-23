using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDesk
{
    public partial class frmSetup : Form
    {
        //TODO:  1) Implement the "NEW" address button in frmUser
        //TODO:  2) Implement Profiles
        //TODO:  3) Implement basic messaging
        //TODO:  4) Implement basic scheduling?

        private frmSetupDocuments mfrmDocuments = null;
        private frmSetupUsers mfrmUsers = null;
        private frmSetupSystem mfrmSystem = null;
        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private string mModName = "frmSetup";
        private string mOldTitle = "";

        private int mLeft;
        private int mTop;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        public frmSetup(EZDeskDataLayer.EZDeskCommon common, int left, int top, int width, int height)
        {
            InitializeComponent();
            mCommon = common;
            frmDocuments();
            mOldTitle = "Setup - Documents";
            this.Text = mOldTitle;

            mLeft = 0;
            mTop = 0;

            mCommon.SetFormLeftTop(left, top, width, height, this.Width, this.Height, out mLeft, out mTop); 
            //mLeft = left + ((width - this.Width) / 2);
            //mTop = top + ((height - this.Height) / 2);
        }

        /// <summary>
        /// Load the iDesk form into the hosting panel
        /// </summary>
        private void frmDocuments()
        {
            Trace.Enter(Trace.RtnName(mModName, "frmDocuments"));

            try
            {
                if (mfrmDocuments == null)
                {
                    mfrmDocuments = new frmSetupDocuments(mCommon);
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
                EZException eze = new EZUtils.EZException("frmDocuments Failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmDocuments"));
            }
        }

        /// <summary>
        /// Load the iDesk form into the hosting panel
        /// </summary>
        private void frmUsers()
        {
            Trace.Enter(Trace.RtnName(mModName, "frmUsers"));

            try
            {
                if (mfrmUsers == null)
                {
                    mfrmUsers = new frmSetupUsers(mCommon);
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
                EZException eze = new EZUtils.EZException("frmUsers failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmUsers"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void frmSystem()
        {
            Trace.Enter(Trace.RtnName(mModName, "frmSystem"));

            try
            {
                if (mfrmSystem == null)
                {
                    mfrmSystem = new frmSetupSystem(mCommon);
                    mfrmSystem.TopLevel = false;
                    mfrmSystem.Visible = true;
                    mfrmSystem.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                    mfrmSystem.Width = pnlHosting.Width;
                    mfrmSystem.Height = pnlHosting.Height;
                    mfrmSystem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(mfrmSystem);
                    //mfrmSystem.Init();
                }

                pnlHosting.Controls["frmSetupSystem"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZUtils.EZException("frmSystem failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmSystem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDocuments_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdDocuments_Click"));

            try
            {
                frmDocuments();
            }

            catch (Exception ex)
            {
                EZException eze = new EZUtils.EZException("cmdDocuments_Click failed", ex);
                ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Documents Error");
                Trace.WriteEventEntry("EZDesk", eze);
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdDocuments_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUsers_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdUsers_Click"));

            try
            {
                frmUsers();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdDocuments_Click failed", ex);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "User Error");
                Trace.WriteEventEntry("EZDesk", eze);
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdUsers_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSystem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdSystem_Click"));

            try
            {
                frmSystem();
            }

            catch (Exception ex)
            {
                EZException eze = new EZUtils.EZException("cmdDocuments_Click failed", ex);
                ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "User Error");
                Trace.WriteEventEntry("EZDesk", eze);
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdSystem_Click"));
            }
        }

        private void cmdMessaging_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
            this.Text = mOldTitle;
        }

        private void cmdMisc_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Ready yet");
            this.Text = mOldTitle;
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 100;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            mOldTitle = this.Text;
            this.Text = "Setup - " + e.ClickedItem.Name.Substring(3);
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetup_Shown(object sender, EventArgs e)
        {
            this.Left = mLeft;
            this.Top = mTop;
        }

    }
}
