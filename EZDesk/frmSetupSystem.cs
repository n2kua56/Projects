using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using MySql.Data.MySqlClient;

namespace EZDesk
{
    public partial class frmSetupSystem : Form
    {
        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private string mMod = "frmSetupSystem";

        public frmSetupSystem(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            zSetupdgvProperties();
        }

        private void frmSetupSystem_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetupdgvProperties()
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zSetupDataGridView"));

            try
            {
                dgvProperties.DataSource = eCtrl.GetAllProperties();

                dgvProperties.AllowUserToAddRows = false;
                dgvProperties.AllowUserToDeleteRows = false;
                dgvProperties.AllowUserToOrderColumns = false;
                dgvProperties.AllowUserToResizeColumns = true;
                dgvProperties.AllowUserToResizeRows = false;
                dgvProperties.ColumnHeadersVisible = true;
                dgvProperties.MultiSelect = false;
                dgvProperties.ReadOnly = true;
                dgvProperties.RowHeadersVisible = false;

                dgvProperties.Columns["PropID"].Visible = false;
                dgvProperties.Columns["PropID"].Width = 0;

                dgvProperties.Columns["Created"].Visible = false;
                dgvProperties.Columns["Created"].Width = 0;

                dgvProperties.Columns["IsActive"].Visible = false;
                dgvProperties.Columns["IsActive"].Width = 0;

                dgvProperties.Columns["Modified"].Visible = false;
                dgvProperties.Columns["Modified"].Width = 0;

                dgvProperties.Columns["PROPERTYNAME"].HeaderText = "Property Name";
                dgvProperties.Columns["PROPERTYNAME"].Width = 90;

                dgvProperties.Columns["DESCRIPTION"].HeaderText = "Description";
                dgvProperties.Columns["DESCRIPTION"].Width = 90;

                dgvProperties.Columns["PROPERTYVALUE"].HeaderText = "Value";
                dgvProperties.Columns["PROPERTYVALUE"].Width = 90;

                dgvProperties.Columns["VISIBILITY"].Visible = false;
                dgvProperties.Columns["VISIBILITY"].Width = 0;

                zResizeProperties();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zSetupDataGridView failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zSetupDataGridView"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeProperties()
        {
            int dgWidth = -1;
            int p10 = -1;
            int scrollBar = -1;

            EZUtils.Trace.Enter(Trace.RtnName(mMod, "zResizeProperties"));
            
            try
            {
                dgWidth = dgvProperties.Width;
                scrollBar = 23;
                p10 = (dgWidth - scrollBar) / 100;

                dgvProperties.Columns["PROPERTYNAME"].Width = 25 * p10;
                dgvProperties.Columns["DESCRIPTION"].Width = 35 * p10;
                dgvProperties.Columns["PROPERTYVALUE"].Width = 40 * p10;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zResizeProperties failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mMod, "zResizeProperties"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProperties_Resize(object sender, EventArgs e)
        {
            zResizeProperties();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProperties_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewSelectedRowCollection selRows = null;
            int idx = -1;

            EZDeskDataLayer.ehr.Models.AvailablePropertyItem item = 
                new EZDeskDataLayer.ehr.Models.AvailablePropertyItem();
            selRows = dgvProperties.SelectedRows;
            idx = selRows[0].Index;
            item.PropID = Convert.ToInt32(dgvProperties.Rows[idx].Cells["PropId"].Value.ToString());
            item.Created = Convert.ToDateTime(dgvProperties.Rows[idx].Cells["Created"].Value.ToString());
            item.IsActive = Convert.ToBoolean(dgvProperties.Rows[idx].Cells["IsActive"].Value.ToString());
            item.Modified = Convert.ToDateTime(dgvProperties.Rows[idx].Cells["Modified"].Value.ToString());
            item.PropertyName = dgvProperties.Rows[idx].Cells["PROPERTYNAME"].Value.ToString();
            item.Description = dgvProperties.Rows[idx].Cells["DESCRIPTION"].Value.ToString();
            item.PropertyValue = dgvProperties.Rows[idx].Cells["PROPERTYVALUE"].Value.ToString();

            frmAvailableProperties frm = new frmAvailableProperties(mCommon, item);
            DialogResult dr = frm.ShowDialog();
            zSetupdgvProperties();
        }

    }
}
