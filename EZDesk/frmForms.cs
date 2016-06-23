using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDesk
{
    public partial class frmForms : Form
    {
        private EZDeskDataLayer.EZDeskCommon mCommon;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private string mModName = "frmForms.";

        public string FormFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        public frmForms(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmForms_Load(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            string dir = "";
            DirectoryInfo di = null;

            Trace.Enter(Trace.RtnName(mModName, "frmForms_Load"));

            try
            {
                tbl.Columns.Add(new DataColumn("Form"));
                tbl.Columns.Add(new DataColumn("FullFileName"));
                dgvForms.DataSource = tbl;
                dgvForms.Columns["Form"].Width = dgvForms.Width - 20;
                dgvForms.Columns["FullFileName"].Width = 0;
                dgvForms.Columns["FullFileName"].Visible = false;

                dir = eCtrl.GetProperty("FormsDir");
                di = new DirectoryInfo(dir);
                FileInfo[] files = di.GetFiles();
                foreach (FileInfo fi in files)
                {
                    DataRow row = tbl.NewRow();
                    row["Form"] = fi.Name;
                    row["FullFileName"] = fi.FullName;
                    tbl.Rows.Add(row);
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", mModName + "frmForms_Load()");
                ex.Data.Add("dir", dir);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "frmForms_Load"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvForms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "dgvForms_CellDoubleClick"));

            try
            {
                Trace.Enter(Trace.RtnName(mModName, "dgvForms_CellDoubleClick"));
                DataGridViewSelectedRowCollection rows = dgvForms.SelectedRows;
                if (rows.Count > 0)
                {
                    DataGridViewRow row = rows[0];
                    FormFileName = row.Cells["FullFileName"].Value.ToString();
                    this.DialogResult = DialogResult.OK;
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "dgvForms_CellDoubleClick"));
                ex.Data.Add("FormFileName", FormFileName);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "dgvForms_CellDoubleClick"));
            }
        }

    }
}
