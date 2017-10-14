using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratePassword
{
    public partial class GeneratePassword : Form
    {
        public GeneratePassword()
        {
            InitializeComponent();
        }

        private void tbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            tbEncryptedPassword.Text = "";
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            bool cont = true;
            string toEncrypt = "";
            string Encrypted = "";
            
            if ((tbUserName.Text.Trim().Length == 0) || 
                (tbUserName.Text.Split(' ').Length > 1))
            {
                cont = false;
                MessageBox.Show("You MUST specify a valid User Name");
            }

            if ((cont && tbPassword.Text.Trim().Length == 0) ||
                (tbPassword.Text.Split(' ').Length > 1))
            {
                cont = false;
                MessageBox.Show("You MUST specify a valid Clear Text Password");
            }

            if (cont)
            {
                tbUserName.Text = tbUserName.Text.Trim();
                tbPassword.Text = tbPassword.Text.Trim();
                toEncrypt = tbUserName.Text + "+" + tbPassword.Text;
                //replace following line with a call to the encryption routine
                Encrypted = toEncrypt;
                tbEncryptedPassword.Text = Encrypted;
            }
            
        }

        private void cmdSQL_Click(object sender, EventArgs e)
        {
            string sql = "";

            if (tbEncryptedPassword.Text.Length == 0)
            {
                MessageBox.Show("You MUST first generate an Encrypted password");
            }
            else
            {
                sql = "USE eRxAdmin\n" +
                        ""
            }
        }
    }
}
