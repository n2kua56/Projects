using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;

namespace EZDesk
{
    public partial class frmPerson : Form
    {
        private EZDeskCommon mCommon = null;
        //#private EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        
        public event EventHandler PersonSelected;
        public delegate void EventHandler(frmPerson m, EZDeskDataLayer.Person.Models.PersonSelectedArguments e);

        public frmPerson(EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            //#pCtrl = new EZDeskDataLayer.Person.PersonCtrl(mCommon.Connection);
            label2.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool setGrid = false;
            int selStart = -1;
            if (textBox1.Text.Length < 4)
            {
                dataGridView1.DataSource = null;
            }

            else
            {
                setGrid = dataGridView1.DataSource == null;
                selStart = textBox1.SelectionStart;

                textBox1.Enabled = false;
                dataGridView1.DataSource = mCommon.pCtrl.GetMatchingByName(textBox1.Text);
                label2.Text = dataGridView1.Rows.Count.ToString();

                if (setGrid)
                {
                    zSetUpGrid();
                }

                textBox1.Enabled = true;
                textBox1.Focus();
                textBox1.SelectionStart = selStart;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetUpGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns["PersonID"].Visible = false;
            dataGridView1.Columns["PersonID"].Width = 0;

            dataGridView1.Columns["PName"].Visible = true;
            dataGridView1.Columns["PName"].HeaderText = "Name";
            dataGridView1.Columns["PName"].ToolTipText = "Double Click to select";

            zResizeGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeGrid()
        {
            int gutter = 23;

            if (dataGridView1.DataSource != null)
            {
                dataGridView1.Columns["PName"].Width = dataGridView1.Width - gutter;
            }
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            zResizeGrid();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string temp = "";
            int idx = -1;

            EventHandler handler = PersonSelected;
            if (handler != null)
            {
                idx = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[idx];
                temp = row.Cells["PersonID"].Value.ToString();

                EZDeskDataLayer.Person.Models.PersonSelectedArguments args = new EZDeskDataLayer.Person.Models.PersonSelectedArguments();
                args.PersonId = Convert.ToInt32(temp);
                handler(this, args);
            }
        }

    }
}
