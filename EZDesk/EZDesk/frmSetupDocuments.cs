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
    public partial class frmSetupDocuments : Form
    {
        private MySqlConnection mConn = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;

        public frmSetupDocuments(MySqlConnection Conn)
        {
            InitializeComponent();
            mConn = Conn;
        }

        private void frmSetupDocuments_Load(object sender, EventArgs e)
        {
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mConn);
            dataGridView1.DataSource = eCtrl.GetAllTabs();
            zSetupDataGridView();
            zSetupDrawersGrid();
            zSetupAvailableTabs();
            zSetupAssignedTabs();
        }

        private void zSetupAssignedTabs()
        {
            //throw new NotImplementedException();
            //dgvAssignedTabs
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetupAvailableTabs()
        {
            DataTable tabs = null;
            tabs = eCtrl.GetAllTabs();
            dgvAvailableTabs.DataSource = tabs;
            
            dgvAvailableTabs.RowHeadersVisible = false;
            dgvAvailableTabs.ReadOnly = true;
            dgvAvailableTabs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAvailableTabs.MultiSelect = true;
            dgvAvailableTabs.AllowUserToAddRows = false;
            dgvAvailableTabs.AllowUserToDeleteRows = false;
            dgvAvailableTabs.AllowUserToResizeRows = false;
            
            dgvAvailableTabs.Columns["tabId"].Visible = false;
            dgvAvailableTabs.Columns["tabId"].Width = 0;

            dgvAvailableTabs.Columns["DisplaySeq"].Visible = false;
            dgvAvailableTabs.Columns["DisplaySeq"].Width = 0;

            dgvAvailableTabs.Columns["IsActive"].Visible = false;
            dgvAvailableTabs.Columns["IsActive"].Width = 0;
        }

        private void zSetupDrawersGrid()
        {
            DataTable drawers = null;

            drawers = eCtrl.GetAllDrawers();
            dgvDrawers.DataSource = drawers;

            dgvDrawers.RowHeadersVisible = false;
            dgvDrawers.ReadOnly = true;
            dgvDrawers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDrawers.MultiSelect = false;
            dgvDrawers.AllowUserToAddRows = false;
            dgvDrawers.AllowUserToDeleteRows = false;
            dgvDrawers.AllowUserToResizeRows = false;

            dgvDrawers.Columns["Id"].Visible = false;
            dgvDrawers.Columns["Id"].Width = 0;
            dgvDrawers.Columns["Seq"].Width = 40;
            dgvDrawers.Columns["Seq"].HeaderText = "Disp. Seq";
            dgvDrawers.Columns["IsActive"].Width = 40;
            dgvDrawers.Columns["IsActive"].HeaderText = "Active";
        }

        /// <summary>
        /// The dataGridView is being resized. Need to adjust the
        /// columns inside the dataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            zResizeGrid();
        }

        /// <summary>
        /// The user has pressed the "New Tab" button at the bottom
        /// of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            zAddTab();
        }

        /// <summary>
        /// The user has selected to add a tab from the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewTab_Click(object sender, EventArgs e)
        {
            zAddTab();
        }

        /// <summary>
        /// The user has selected to Edit a tab from the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditTab_Click(object sender, EventArgs e)
        {
            int tabId = zGetSelectedTabId();

            if (tabId > -1)
            {
                zEditTab(tabId);
            }
        }

        /// <summary>
        /// The user has selected to toggle the IsActive flag on a tab
        /// from the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDeleteTab_Click(object sender, EventArgs e)
        {
            zDeleteTab();
        }

        /// <summary>
        /// The usesr has double clicked on a row indicating they want
        /// to edit that tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int tabId = zGetSelectedTabId();

            if (tabId > -1)
            {
                zEditTab(tabId);
            }
        }

        /// <summary>
        /// Bring up the frmSetupTab form to add a tab.
        /// </summary>
        private void zAddTab()
        {
            frmSetupTab frm = new frmSetupTab(mConn, -1);
            frm.ShowDialog();
            dataGridView1.DataSource = eCtrl.GetAllTabs();
        }

        /// <summary>
        /// Bring up the frmSetupTab to Edit the selected tabId
        /// </summary>
        /// <param name="tabId"></param>
        private void zEditTab(int tabId)
        {
            frmSetupTab frm = new frmSetupTab(mConn, tabId);
            frm.ShowDialog();
            dataGridView1.DataSource = eCtrl.GetAllTabs();
        }

        /// <summary>
        /// Toggle the rows IsActive flag
        /// </summary>
        private void zDeleteTab()
        {
            int tabId = zGetSelectedTabId();
            if (tabId > -1)
            {
                Datalayer.ehr.Models.tabItem tab = new Datalayer.ehr.Models.tabItem();
                tab = eCtrl.GetTab(tabId);
                tab.IsActive = !tab.IsActive;
                eCtrl.WriteTab(tab);
                dataGridView1.DataSource = eCtrl.GetAllTabs();
            }
        }

        /// <summary>
        /// Return the tabId of the currently selected Row in the dataGridView.
        /// </summary>
        /// <returns></returns>
        private int zGetSelectedTabId()
        {
            int tabId = -1;
         
            DataGridViewSelectedRowCollection rows =
                dataGridView1.SelectedRows;
            if (rows.Count > 0)
            {
                tabId = Convert.ToInt32(rows[0].Cells["tabId"].Value);
            }

            return tabId;
        }

        /// <summary>
        /// The dataGridView control is being initialized. Need
        /// to set the initial column widths, set the column
        /// headers and set the dataGridView data member.
        /// </summary>
        private void zSetupDataGridView()
        {
            //dataGridView1.DataMember = "tabId";

            dataGridView1.Columns["tabId"].Visible = false;
            dataGridView1.Columns["tabId"].Width = 0;

            dataGridView1.Columns["tabName"].HeaderText = "Tab Name";
            dataGridView1.Columns["tabName"].Width = 90;

            dataGridView1.Columns["tabDesc"].HeaderText = "Description";

            dataGridView1.Columns["IsActive"].HeaderText = "Active";
            dataGridView1.Columns["IsActive"].Width = 50;

            dataGridView1.Columns["DisplaySeq"].HeaderText = "Display";
            dataGridView1.Columns["DisplaySeq"].Width = 50;

            zResizeGrid();
        }

        /// <summary>
        /// The dataGridView columns need to be resized.
        /// </summary>
        private void zResizeGrid()
        {
            int w = dataGridView1.Width;
            int fixW = dataGridView1.Columns["tabName"].Width +
                        dataGridView1.Columns["IsActive"].Width +
                        dataGridView1.Columns["DisplaySeq"].Width;
            int scrollBar = 23;
            w = w - (fixW + scrollBar);
            dataGridView1.Columns["tabDesc"].Width = w;
        }

    }
}
