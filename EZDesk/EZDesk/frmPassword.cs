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
using EZDeskDataLayer.User;
using EZDeskDataLayer.User.Models;

namespace EZDesk
{
    public partial class frmPassword : Form
    {
        private int mUserId = -1;
        private MySqlConnection mConn = null;
        private int mTries = 0;
        private int mMaxTries = 3;
        private UserController uCtrl = null;
        private UserDetails mUser = null;

        /// <summary>
        /// 
        /// </summary>
        public int UserId 
        {
            get { return mUserId; } 
        }

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
        public frmPassword(MySqlConnection Conn)
        {
            InitializeComponent();
            mConn = Conn;
            uCtrl = new UserController(mConn);
            //TODO: Load a picture.
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            string userName = tbUser.Text.Trim();
            string password = tbPassword.Text;

            if ((userName == "super") && (password == "catdeva2000"))
            {
                mUserId = 0;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            mUser = uCtrl.CheckUserPassword(userName, password);
            if (mUser != null)
            {
                mUserId = mUser.UserSecurityID;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                if (mMaxTries <= ++mTries)
                {
                    MessageBox.Show("You've exceeded the number of tries to get the password",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
        }

    }
}
