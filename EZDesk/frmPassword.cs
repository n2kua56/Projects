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
using EZDeskDataLayer;
using EZDeskDataLayer.User;
using EZDeskDataLayer.User.Models;

namespace EZDesk
{
    public partial class frmPassword : Form
    {
        private EZDeskCommon mCommon;
        private int mTries = 0;
        private int mMaxTries = 3;
        private UserDetails mUser = null;

        private int mLeft;
        private int mTop;

        /// <summary>
        /// 
        /// </summary>
        public UserDetails User
        {
            get { return mUser; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        public frmPassword(EZDeskCommon common, int left, int top, int width, int height)
        {
            InitializeComponent();
            mCommon = common;
            label3.Text = "";

            mLeft = 0;
            mTop = 0;
            mCommon.SetFormLeftTop(left, top, width, height, this.Width, this.Height, out mLeft, out mTop); 
            //mLeft = left + ((width - this.Width) / 2);
            //mTop = top + ((height - this.Height) / 2);

            //Following was for debugging. 
            label4.Text = "OL:" + left.ToString() + " OT:" + top.ToString() +
                    " OW:" + width.ToString() + " OH:" + height.ToString();
            label5.Text = " L:" + this.Left.ToString() + "  T:" + this.Top.ToString() +
                    "  W:" + this.Width.ToString() + "  H:" + this.Height.ToString();

            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
        }

        /// <summary>
        /// Check the entered username/password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            string userName = tbUser.Text.Trim();
            string password = tbPassword.Text;
            
            mCommon.User = null;
            label3.Text = "";

            //Check for the back-door
            if ((userName == "super") && (password == "catdeva2000"))
            {
                mCommon.User = mCommon.uCtrl.GetSuperUser();
            }

            //If we didn't find the back-door user, try to match the
            // entere userid and password.
            if (mCommon.User == null)
            {
                mCommon.User = mCommon.uCtrl.CheckUserPassword(userName, password);
            }

            //If we still didn't find anything then the user doesn't have the
            // right userid and password combination. Issue the error message
            // and display an error message that the userid/password was wrong.
            if (mCommon.User == null)
            {
                label3.Text = "User Name not found or Password incorrect";
                if (mMaxTries <= ++mTries)
                {
                    MessageBox.Show("You've exceeded the number of tries to get the password",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }

            //Otherwise log the login, and set the dialog result to ok, the 
            // user is logged on.
            else
            {
                EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                                EZDeskDataLayer.ehr.Models.AuditAreas.User,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Login,
                                "");
                mCommon.eCtrl.WriteAuditRecord(item);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Tab) || (e.KeyData == Keys.Enter))
            {
                tbPassword.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                cmdOk_Click(sender, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPassword_Load(object sender, EventArgs e)
        {
            int rInt = -1;
            Image pic = null;

            DirectoryInfo di = new DirectoryInfo("images");
            FileInfo[] files = di.GetFiles("Logon*.jpg");
            if (files.Length > 0)
            {
                Random r = new Random();
                rInt = r.Next(0, files.Length); 
                pic = Image.FromFile(files[rInt].FullName);
                pictureBox1.Image = pic;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPassword_Move(object sender, EventArgs e)
        {
            //This was for debug.
            label6.Text = "L:" + this.Left.ToString() + " T:" + this.Top.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPassword_Shown(object sender, EventArgs e)
        {
            this.Left = mLeft;
            this.Top = mTop;
        }

    }
}
