using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZUtils;

namespace EZDesk
{
    public partial class frmAuditLog : Form
    {
        //TODO: Improve the filters

        //DONE: 20141028 Display the audit log for a specified period
        //DONE: 20141028 Audit the fact that the audit log was displayed

        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskCommon mCommon;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conn"></param>
        /// <param name="common"></param>
        public frmAuditLog(EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            label3.Text = "";

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

            ////Setup the grid
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.AllowUserToDeleteRows = false;
            //dataGridView1.AllowUserToResizeColumns = true;
            //dataGridView1.AllowUserToResizeRows = false;
            //dataGridView1.ColumnHeadersVisible = true;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAuditLog_Load(object sender, EventArgs e)
        {
            DateTime temp = DateTime.Now;
            dateTimePicker1.Value = new DateTime(temp.Year, temp.Month, temp.Day, 0, 0, 0);
            dateTimePicker2.Value = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59, 999);
            zFillinGrid();
            zSetupGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillinGrid()
        {
            EZDeskDataLayer.ehr.Models.AuditItem item =
                new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "Audit Report: " +
                        dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00" /*hh:mm:ss"*/) + " to " +
                            dateTimePicker2.Value.ToString("yyyy-MM-dd 24:00:00" /*hh:mm:ss"*/));
            eCtrl.WriteAuditRecord(item);

            dataGridView1.DataSource = eCtrl.GetAuditLog(dateTimePicker1.Value, dateTimePicker2.Value);
            label3.Text = dataGridView1.Rows.Count.ToString();

            zSetColumnStyles();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetupGrid()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void zSetColumnStyles()
        {
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Id"].Width = 0;

            dataGridView1.Columns["AuditDateTime"].HeaderText = "Date Time";
            dataGridView1.Columns["AuditDateTime"].Width = 120;

            dataGridView1.Columns["UserId"].Visible = false;
            dataGridView1.Columns["UserId"].Width = 0;

            dataGridView1.Columns["UserName"].HeaderText = "User";
            dataGridView1.Columns["UserName"].Width = 55;

            dataGridView1.Columns["PersonId"].Visible = false;
            dataGridView1.Columns["PersonId"].Width = 0;

            dataGridView1.Columns["FirstName"].HeaderText = "First Name";
            dataGridView1.Columns["FirstName"].Width = 60;

            dataGridView1.Columns["MiddleName"].HeaderText = "Middle";
            dataGridView1.Columns["MiddleName"].Width = 00;
            dataGridView1.Columns["MiddleName"].Visible = false;

            dataGridView1.Columns["LastName"].HeaderText = "Last";
            dataGridView1.Columns["LastName"].Width = 60;

            dataGridView1.Columns["PersonTypeID"].Visible = false;
            dataGridView1.Columns["PersonTypeID"].Width = 0;

            dataGridView1.Columns["AuditAreaId"].Visible = false;
            dataGridView1.Columns["AuditAreaId"].Width = 0;

            dataGridView1.Columns["AuditArea"].HeaderText = "Area";
            dataGridView1.Columns["AuditArea"].Width = 55;

            dataGridView1.Columns["auditActivityId"].Visible = false;
            dataGridView1.Columns["auditActivityId"].Width = 0;

            dataGridView1.Columns["AuditActivity"].HeaderText = "Activity";
            dataGridView1.Columns["AuditActivity"].Width = 55;

            dataGridView1.Columns["Description"].HeaderText = "Notes";
            dataGridView1.Columns["Description"].Width = 512;

            dataGridView1.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            zFillinGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
