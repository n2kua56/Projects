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
        //TODO: File Types tab

        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private string mMod = "frmSetupDocuments";

        public frmSetupDocuments(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
        }

        private void frmSetupDocuments_Load(object sender, EventArgs e)
        {
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);
            dataGridView1.DataSource = eCtrl.GetAllTabs();
            zSetupDataGridView();

            zSetupDrawersGrid();
            zSetupAvailableTabs();
            if (dgvDrawers.Rows.Count > 0)
            {
                dgvDrawers.Rows[0].Selected = true;
            }
            //zSetupAssignedTabs();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        // TABS PAGE
        #region Tabs Page

        /// <summary>
        /// The dataGridView is being resized. Need to adjust the
        /// columns inside the dataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dataGridView1_Resize"));

            try
            {
                zResizeGrid();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dataGridView1_Resize failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dataGridView1_Resize"));
            }
        }

        /// <summary>
        /// The user has pressed the "New Tab" button at the bottom
        /// of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "cmdNew_Click"));

            zAddTab();

            EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "cmdNew_Click"));
        }

        /// <summary>
        /// The user has selected to add a tab from the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNewTab_Click(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "mnuNewTab_Click"));

            zAddTab();

            EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "mnuNewTab_Click"));
        }

        /// <summary>
        /// The user has selected to Edit a tab from the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEditTab_Click(object sender, EventArgs e)
        {
            int tabId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "mnuEditTab_Click"));

            try
            {
                tabId = zGetSelectedTabId();

                if (tabId > -1)
                {
                    zEditTab(tabId);
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("mnuEditTab_Click", ex);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "mnuEditTab_Click"));
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
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "mnuDeleteTab_Click"));

            zDeleteTab();

            EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "mnuDeleteTab_Click"));
        }

        /// <summary>
        /// The usesr has double clicked on a row indicating they want
        /// to edit that tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int tabId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dataGridView1_CellDoubleClick"));

            try
            {
                tabId = zGetSelectedTabId();

                if (tabId > -1)
                {
                    zEditTab(tabId);
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dataGridView1_CellDoubleClick failed", ex);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dataGridView1_CellDoubleClick"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idx = e.RowIndex;
            int id = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dataGridView1_CellMouseDown"));

            try
            {
                if ((dataGridView1.Columns[e.ColumnIndex].Name == "IsActive") && (e.Button == MouseButtons.Left))
                {
                    idx = e.RowIndex;
                    id = Convert.ToInt32(dataGridView1.Rows[idx].Cells["tabId"].Value.ToString());
                    zToggleTabDeleteFlag(id);
                }

                else
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        mnuNewTab.Enabled = true;
                        mnuEditTab.Enabled = true;
                        mnuDeleteTab.Enabled = true;

                        if (idx > -1)
                        {
                            dataGridView1.Rows[idx].Selected = true;
                        }

                        else
                        {
                            mnuEditTab.Enabled = false;
                            mnuDeleteTab.Enabled = false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dataGridView1_CellMouseDown failed", ex);
                eze.Add("idx", idx);
                eze.Add("MouseButton", e.Button);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Mouse event failed");
                frm.ShowDialog();
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dataGridView1_CellMouseDown"));
            }
        }

        /// <summary>
        /// Bring up the frmSetupTab form to add a tab.
        /// </summary>
        private void zAddTab()
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zAddTab"));

            try
            {
                frmSetupTab frm = new frmSetupTab(mCommon, -1);
                frm.ShowDialog();
                dataGridView1.DataSource = eCtrl.GetAllTabs();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zAddTab failed", ex);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Add/Edit Tab failed");
                DialogResult dr = frm.ShowDialog();
                EZUtils.Trace.WriteEventEntry("EZDesk", eze);
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zAddTab"));
            }
        }

        /// <summary>
        /// Bring up the frmSetupTab to Edit the selected tabId
        /// </summary>
        /// <param name="tabId"></param>
        private void zEditTab(int tabId)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zEditTab"));

            try
            {
                frmSetupTab frm = new frmSetupTab(mCommon, tabId);
                frm.ShowDialog();
                dataGridView1.DataSource = eCtrl.GetAllTabs();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zEditTab failed", ex);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Edit Tab failed");
                DialogResult dr = frm.ShowDialog();
                EZUtils.Trace.WriteEventEntry("EZDesk", eze);
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zEditTab"));
            }
        }

        /// <summary>
        /// Toggle the rows IsActive flag
        /// </summary>
        private void zDeleteTab()
        {
            int tabId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zDeleteTab"));

            try
            {
                tabId = zGetSelectedTabId();
                if (tabId > -1)
                {
                    EZDeskDataLayer.ehr.Models.tabItem tab = new EZDeskDataLayer.ehr.Models.tabItem();
                    tab = eCtrl.GetTab(tabId);
                    tab.IsActive = !tab.IsActive;
                    eCtrl.WriteTab(tab);
                    dataGridView1.DataSource = eCtrl.GetAllTabs();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zDeleteTab failed", ex);
                eze.Add("tabId", tabId);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Delete tab error");
                DialogResult dr = frm.ShowDialog();
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zDeleteTab"));
            }
        }

        /// <summary>
        /// Return the tabId of the currently selected Row in the dataGridView.
        /// </summary>
        /// <returns></returns>
        private int zGetSelectedTabId()
        {
            int tabId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zGetSelectedTabId"));

            try
            {
                DataGridViewSelectedRowCollection rows =
                    dataGridView1.SelectedRows;
                if (rows.Count > 0)
                {
                    tabId = Convert.ToInt32(rows[0].Cells["tabId"].Value);
                }

                return tabId;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zGetSelectedTabId failed", ex);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zGetSelectedTabId"));
            }
        }

        /// <summary>
        /// The dataGridView control is being initialized. Need
        /// to set the initial column widths, set the column
        /// headers and set the dataGridView data member.
        /// </summary>
        private void zSetupDataGridView()
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zSetupDataGridView"));

            try
            {
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
                dataGridView1.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
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
        /// The dataGridView columns need to be resized.
        /// </summary>
        private void zResizeGrid()
        {
            int w = -1;
            int fixW = -1;
            int scrollBar = 23;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zResizeGrid"));

            try
            {
                w = dataGridView1.Width;
                fixW = dataGridView1.Columns["tabName"].Width +
                       dataGridView1.Columns["IsActive"].Width +
                       dataGridView1.Columns["DisplaySeq"].Width;
                w = w - (fixW + scrollBar);
                dataGridView1.Columns["tabDesc"].Width = w;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zResizeGrid failed", ex);
                eze.Add("w", w);
                eze.Add("fixW", fixW);
                eze.Add("scrollBar", scrollBar);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zResizeGrid"));
            }
        }

        /// <summary>
        /// Toggle the delete flag of the specified drawerId.
        /// </summary>
        /// <param name="idx">Id of the Drawer to be deleted</param>
        private void zToggleTabDeleteFlag(int idx)
        {
            EZDeskDataLayer.ehr.Models.tabItem tab = null;
            bool act = false;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zToggleTabDeleteFlag"));

            try
            {
                if (idx > -1)
                {
                    tab = eCtrl.GetTab(idx);
                    act = tab.IsActive;
                    act = !act;
                    tab.IsActive = act;
                    eCtrl.WriteTab(tab);
                    dataGridView1.DataSource = eCtrl.GetAllTabs();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zToggleTabDeleteFlag failed", ex);
                eze.Add("idx", idx);
                eze.Add("tab", tab);
                eze.Add("act", act);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zToggleTabDeleteFlag"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        // TABS PAGE
        #region FileDrawers

        /// <summary>
        /// The available tabs grid view is being resized, need to resize the
        /// columns. The description field expands and contracts to fill the
        /// available space with a minimum width (at which point the horizontal
        /// scroll bar moves.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAvailableTabs_Resize(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvAvailableTabs_Resize"));

            try
            {
                zResizeAvailableTabsGrid();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvAvailableTabs_Resize failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvAvailableTabs_Resize"));
            }
        }

        /// <summary>
        /// The Assigned tabs grid is being resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAssignedTabs_Resize(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvAssignedTabs_Resize"));

            try
            {
                zResizeSelectedTabsGrid();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvAssignedTabs_Resize failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvAssignedTabs_Resize"));
            }
        }

        /// <summary>
        /// The drawers grid is being resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawers_Resize(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvDrawers_Resize"));

            try
            {
                zResizeDrawers();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvDrawers_Resize failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvDrawers_Resize"));
            }
        }

        /// <summary>
        /// The user has pressed the new button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDrawersNew_Click(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "cmdDrawersNew_Click"));

            try
            {
                zAddEditDrawer(-1);
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdDrawersNew_Click failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "cmdDrawersNew_Click"));
            }
        }

        /// <summary>
        /// The groupbox4 needs to be resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox4_Resize(object sender, EventArgs e)
        {
            int gutter = 6;
            int bWidth = -1;
            int gbWidth = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "groupBox4_Resize"));

            try
            {
                //Center the buttons
                bWidth = btnRight.Width;
                gbWidth = groupBox4.Width;
                btnRight.Left = (gbWidth - bWidth) / 2;
                btnLeft.Left = btnRight.Left;

                //Size the left Available tabs grid
                dgvAvailableTabs.Width = (btnRight.Left - dgvAvailableTabs.Left) - gutter;

                //Size and move the Assigned tabs grid
                dgvAssignedTabs.Width = dgvAvailableTabs.Width;
                dgvAssignedTabs.Left = btnRight.Left + btnRight.Width + gutter;
                label3.Left = dgvAssignedTabs.Left;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("groupBox4_Resize failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "groupBox4_Resize"));
            }
        }

        /// <summary>
        /// The user has pressed the Add context menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addDrawerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "addDrawerToolStripMenuItem_Click"));

            try
            {
                zAddEditDrawer(-1);
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("Add Drawer Strip Menu failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "addDrawerToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// The user has pressed the Edit context menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editDrawerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "editDrawerToolStripMenuItem_Click"));

            try
            {
                id = Convert.ToInt32(dgvDrawers.SelectedRows[0].Cells["Id"].Value.ToString());

                zAddEditDrawer(id);
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("editDrawerToolStripMenuItem_Click failed", ex);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "editDrawerToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// The user has pressed the Delete context menu option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteDrawerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "deleteDrawerToolStripMenuItem_Click"));

            try
            {
                id = Convert.ToInt32(dgvDrawers.SelectedRows[0].Cells["Id"].Value.ToString());

                zToggleDeleteFlag(id);
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("deleteDrawerToolStripMenuItem_Click failed", ex);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "deleteDrawerToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// The user has double clicked on a drawer - Edit the entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvDrawers_CellDoubleClick"));

            try
            {
                id = Convert.ToInt32(dgvDrawers.SelectedRows[0].Cells["Id"].Value.ToString());

                zAddEditDrawer(id);
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvDrawers_CellDoubleClick failed", ex);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvDrawers_CellDoubleClick"));
            }
        }

        /// <summary>
        /// The user has pressed the right arrows button, we need to copy the
        /// selected tabs into the assigned tabs grid, write the records to the
        /// drawertabs table.  Care must be taken to not add duplicate tabs.
        /// Tabs are not actually removed from the available tab list as the
        /// assumption is that there should be relativly few tabs and we are
        /// looking for duplicate tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            int drawerId = -1;
            int tabId = -1;
            int sTabId = -1;
            bool found = false;
            EZDeskDataLayer.ehr.Models.drawertabsItem drawertab = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "btnRight_Click"));

            try
            {
                drawerId = Convert.ToInt32(dgvDrawers.SelectedRows[0].Cells["Id"].Value.ToString());
                if (drawerId > 0)
                {
                    DataGridViewSelectedRowCollection rows = dgvAvailableTabs.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        //Search for duplicate tabs already in the assignedtab grid. Set
                        //found to true if we find a duplicate.
                        tabId = Convert.ToInt32(row.Cells["tabId"].Value.ToString());
                        found = false;
                        foreach (DataGridViewRow sRow in dgvAssignedTabs.Rows)
                        {
                            sTabId = Convert.ToInt32(sRow.Cells["tabId"].Value.ToString());
                            if (tabId == sTabId)
                            {
                                found = true;
                                break;
                            }
                        }

                        //If there were no duplicates found then add the tab to the drawertabs table.
                        if (!found)
                        {
                            drawertab = new EZDeskDataLayer.ehr.Models.drawertabsItem();
                            drawertab.Id = -1;
                            drawertab.DrawerId = drawerId;
                            drawertab.TabId = tabId;
                            eCtrl.AddEditDrawerTabs(drawertab);
                            zSetupAssignedTabs();
                        }

                        row.Selected = false;   //Found or not, unselect the row.
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("btnRight_Click failed", ex);
                eze.Add("drawerId", drawerId);
                eze.Add("tabId", tabId);
                eze.Add("sTabId", sTabId);
                eze.Add("found", found);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "btnRight_Click"));
            }
        }

        /// <summary>
        /// The user has pressed the left arrows button. We need to remove the
        /// selected tabs from the assigned tabs grid and delete them from the
        /// ehr_drawertabs table. On completion we call the zSetupAssignedTabs
        /// to re-fill-in the grid of assigned tabs grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            int drawerId = -1;
            int sTabId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "btnLeft_Click"));

            try
            {
                drawerId = Convert.ToInt32(dgvDrawers.SelectedRows[0].Cells["Id"].Value.ToString());
                if (drawerId > 0)
                {
                    DataGridViewSelectedRowCollection rows = dgvAssignedTabs.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        sTabId = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                        eCtrl.DeleteDrawerTabs(sTabId);
                    }
                    zSetupAssignedTabs();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("btnLeft_Click failed", ex);
                eze.Add("drawerId", drawerId);
                eze.Add("sTabId", sTabId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "btnLeft_Click"));
            }
        }

        /// <summary>
        /// The user as depressed the mouse in a cell. If it's the IsActive cell we
        /// need to toggle the IsActive flag.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idx = e.RowIndex;
            int id = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvDrawers_CellMouseDown"));

            try
            {
                if ((dgvDrawers.Columns[e.ColumnIndex].Name == "IsActive") && (e.Button == MouseButtons.Left))
                {
                    idx = e.RowIndex;
                    id = Convert.ToInt32(dgvDrawers.Rows[idx].Cells["Id"].Value.ToString());
                    zToggleDeleteFlag(id);
                }

                else
                {
                    if ((e.Button == MouseButtons.Right) && (idx > -1))
                    {
                        dgvDrawers.Rows[idx].Selected = true;
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvDrawers_CellMouseDown failed", ex);
                eze.Add("idx", idx);
                eze.Add("MouseButton", e.Button);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvDrawers_CellMouseDown"));
            }
        }

        /// <summary>
        /// The user has selected a different drawer. Need to set the Assigned tabs grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDrawers_SelectionChanged(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "dgvDrawers_SelectionChanged"));

            try
            {
                zSetupAssignedTabs();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvDrawers_SelectionChanged failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "dgvDrawers_SelectionChanged"));
            }
        }

        /// <summary>
        /// Add/Edit the specified DrawerId.
        /// </summary>
        /// <param name="id">Drawer ID to edit or -1 to add</param>
        private void zAddEditDrawer(int id)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zAddEditDrawer"));

            try
            {
                frmSetupDrawer frm = new frmSetupDrawer(mCommon, id);
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    zSetupDrawersGrid();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zAddEditDrawer failed", ex);
                eze.Add("id", id);
                EZUtils.ExceptionDialog frm = new EZUtils.ExceptionDialog(eze, "Add/Edit drawer error");
                DialogResult dr = frm.ShowDialog();
                EZUtils.Trace.WriteEventEntry("EZDesk", eze);
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zAddEditDrawer"));
            }
        }

        /// <summary>
        /// Resize the Drawers grid.
        /// </summary>
        private void zResizeDrawers()
        {
            int fixWidth = -1;
            int descWidth = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zResizeDrawers"));

            try
            {
                fixWidth = dgvDrawers.Columns["drawerName"].Width +
                            dgvDrawers.Columns["IsActive"].Width +
                            dgvDrawers.Columns["Seq"].Width + 23;
                descWidth = dgvDrawers.Width - fixWidth;
                dgvDrawers.Columns["drawerDesc"].Width = (descWidth < 80) ? 80 : descWidth;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zResizeDrawers failed", ex);
                eze.Add("fixWidth", fixWidth);
                eze.Add("descWidth", descWidth);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zResizeDrawers"));
            }
        }

        /// <summary>
        /// Resize the available Tabs Grid.
        /// </summary>
        private void zResizeAvailableTabsGrid()
        {
            int fixWidth = -1;
            int descWidth = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zResizeAvailableTabsGrid"));

            try
            {
                fixWidth = dgvAvailableTabs.Columns["tabName"].Width + 23;
                descWidth = dgvAvailableTabs.Width - fixWidth;
                dgvAvailableTabs.Columns["tabDesc"].Width = (descWidth < 80) ? 80 : descWidth;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zResizeAvailableTabsGrid failed", ex);
                eze.Add("fixWidth", fixWidth);
                eze.Add("descWidth", descWidth);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zResizeAvailableTabsGrid"));
            }
        }

        /// <summary>
        /// Resize the selected Tabs Grid.
        /// </summary>
        public void zResizeSelectedTabsGrid()
        {
            int fixWidth = -1;
            int descWidth = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zResizeSelectedTabsGrid"));

            try
            {
                if ((dgvAssignedTabs != null) && (dgvAssignedTabs.Columns != null))
                {
                    fixWidth = dgvAssignedTabs.Columns["tabName"].Width + 23;
                    descWidth = dgvAssignedTabs.Width - fixWidth;
                    dgvAssignedTabs.Columns["tabDesc"].Width = (descWidth < 80) ? 80 : descWidth;
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zResizeSelectedTabsGrid failed", ex);
                eze.Add("fixWidth", fixWidth);
                eze.Add("descWidth", descWidth);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zResizeSelectedTabsGrid"));
            }
        }

        /// <summary>
        /// Load the available tabs data grid view, set the grid view attributes,
        /// hide columns and setup the other ones.
        /// </summary>
        private void zSetupDrawersGrid()
        {
            DataTable drawers = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zSetupDrawersGrid"));

            try
            {
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

                dgvDrawers.Columns["drawerName"].HeaderText = "Name";
                dgvDrawers.Columns["drawerName"].Width = 80;

                dgvDrawers.Columns["drawerDesc"].HeaderText = "Description";
                dgvDrawers.Columns["drawerDesc"].Width = 150;

                zResizeDrawers();
                dgvDrawers.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zSetupDrawersGrid failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zSetupDrawersGrid"));
            }
        }

        /// <summary>
        /// Load the available tabs data grid view, set the grid view attributes,
        /// hide columns and setup the other ones.
        /// </summary>
        private void zSetupAvailableTabs()
        {
            DataTable tabs = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zSetupAvailableTabs"));

            try
            {
                tabs = eCtrl.GetAllTabs();
                dgvAvailableTabs.DataSource = tabs;

                //Set the gridview attributes
                dgvAvailableTabs.RowHeadersVisible = false;
                dgvAvailableTabs.ReadOnly = true;
                dgvAvailableTabs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAvailableTabs.MultiSelect = true;
                dgvAvailableTabs.AllowUserToAddRows = false;
                dgvAvailableTabs.AllowUserToDeleteRows = false;
                dgvAvailableTabs.AllowUserToResizeRows = false;

                //Hide columns we don't want
                dgvAvailableTabs.Columns["tabId"].Visible = false;
                dgvAvailableTabs.Columns["tabId"].Width = 0;

                dgvAvailableTabs.Columns["DisplaySeq"].Visible = false;
                dgvAvailableTabs.Columns["DisplaySeq"].Width = 0;

                dgvAvailableTabs.Columns["IsActive"].Visible = false;
                dgvAvailableTabs.Columns["IsActive"].Width = 0;

                //Set the width on columns we want... will be over-riden in resize
                dgvAvailableTabs.Columns["tabName"].HeaderText = "Name";
                dgvAvailableTabs.Columns["tabName"].Width = 80;

                dgvAvailableTabs.Columns["tabDesc"].HeaderText = "Description";
                dgvAvailableTabs.Columns["tabDesc"].Width = 100;

                // Make sure nothing is selected
                foreach (DataGridViewRow dr in dgvAvailableTabs.Rows)
                {
                    dr.Selected = false;
                }

                zResizeAvailableTabsGrid();
                dgvAvailableTabs.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zSetupAvailableTabs failed", ex);
                eze.Add("tabs", tabs);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zSetupAvailableTabs"));
            }
        }

        /// <summary>
        /// Setup the dgvAssignedTabs grid. Fill with data, set grid properties, set column 
        /// widths and clear all selections.
        /// </summary>
        private void zSetupAssignedTabs()
        {
            DataTable tabs = null;
            int id = -1;
            string temp = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zSetupAssignedTabs"));

            try
            {
                DataGridViewSelectedRowCollection rows = dgvDrawers.SelectedRows;
                if (rows.Count > 0)
                {
                    temp = rows[0].Cells["Id"].Value.ToString();
                    id = Convert.ToInt32(temp);
                    if (id > 0)
                    {
                        tabs = eCtrl.GetAllTabsForDrawer(id);
                        dgvAssignedTabs.DataSource = tabs;

                        //Set the gridview attributes
                        dgvAssignedTabs.RowHeadersVisible = false;
                        dgvAssignedTabs.ReadOnly = true;
                        dgvAssignedTabs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgvAssignedTabs.MultiSelect = true;
                        dgvAssignedTabs.AllowUserToAddRows = false;
                        dgvAssignedTabs.AllowUserToDeleteRows = false;
                        dgvAssignedTabs.AllowUserToResizeRows = false;

                        //Hide columns we don't want
                        dgvAssignedTabs.Columns["Id"].Visible = false;
                        dgvAssignedTabs.Columns["Id"].Width = 0;

                        dgvAssignedTabs.Columns["DrawerId"].Visible = false;
                        dgvAssignedTabs.Columns["DrawerId"].Width = 0;

                        dgvAssignedTabs.Columns["TabId"].Visible = false;
                        dgvAssignedTabs.Columns["TabId"].Width = 0;

                        dgvAssignedTabs.Columns["Created"].Visible = false;
                        dgvAssignedTabs.Columns["Created"].Width = 0;

                        dgvAssignedTabs.Columns["Created"].Visible = false;
                        dgvAssignedTabs.Columns["Created"].Width = 0;

                        dgvAssignedTabs.Columns["tabName"].Visible = true;
                        dgvAssignedTabs.Columns["tabName"].HeaderText = "Name";
                        dgvAssignedTabs.Columns["tabName"].Width = 40;

                        dgvAssignedTabs.Columns["tabDesc"].Visible = true;
                        dgvAssignedTabs.Columns["tabDesc"].HeaderText = "Description";
                        dgvAssignedTabs.Columns["tabDesc"].Width = 80;

                        // Make sure nothing is selected
                        foreach (DataGridViewRow dr in dgvAssignedTabs.Rows)
                        {
                            dr.Selected = false;
                        }

                        zResizeSelectedTabsGrid();

                        dgvAssignedTabs.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zSetupAssignedTabs failed", ex);
                eze.Add("tabs", tabs);
                eze.Add("temp", temp);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zSetupAssignedTabs"));
            }
        }

        /// <summary>
        /// Toggle the delete flag of the specified drawerId.
        /// </summary>
        /// <param name="idx">Id of the Drawer to be deleted</param>
        private void zToggleDeleteFlag(int idx)
        {
            EZDeskDataLayer.ehr.Models.DrawerItem drawer = null;
            bool act = false;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mMod, "zToggleDeleteFlag"));

            try
            {
                if (idx > -1)
                {
                    drawer = eCtrl.GetDrawer(idx);
                    act = drawer.IsActive;
                    act = !act;
                    drawer.IsActive = act;
                    eCtrl.WriteDrawer(drawer);
                    zSetupDrawersGrid();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zToggleDeleteFlag failed", ex);
                eze.Add("idx", idx);
                eze.Add("drawer", drawer);
                eze.Add("act", act);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mMod, "zToggleDeleteFlag"));
            }
        }

        #endregion

        #region FileTypes

        #endregion

    }
}
