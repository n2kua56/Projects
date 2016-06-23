using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.User;
using EZDeskDataLayer.User.Models;
using EZDeskDataLayer.Documents;
using EZUtils;
using pModels = EZDeskDataLayer.Person.Models;

namespace EZDesk
{
    public partial class frmSetupUsers : Form
    {
        //TODO: After drawer added, list on user is not refreshed

        private EZDeskCommon mCommon = null;
        private DocumentsController docCtrl = null;
        const int idealFormMinWidth = 741;
        const int idealNameColWidth = 90;
        private string mModName = "frmSetupUsers";
        private int InProfileUserSetup = 0;
 
        /// <summary>
        /// 
        /// </summary>
        public frmSetupUsers(EZDeskCommon common)
        {
            InitializeComponent();
            zResizeGroupBox2();
            zResizeGroupBox3();
            mCommon = common;
            docCtrl = new DocumentsController(mCommon);
            ddControl1.Text = "New";
            List<string> lstSting = new List<string>();
            foreach (ToolStripItem item in contextMenuStrip1.Items)
            {
                lstSting.Add(item.Text);
            }
            ddControl1.FillControlList(lstSting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetupUsers_Load(object sender, EventArgs e)
        {
            dgvProfile.AllowUserToAddRows = false;
            dgvProfile.AllowUserToDeleteRows = false;
            dgvProfile.AllowUserToOrderColumns = true;
            dgvProfile.AllowUserToResizeColumns = true;
            dgvProfile.AllowUserToResizeRows = false;
            dgvProfile.ColumnHeadersVisible = true;
            dgvProfile.MultiSelect = false;
            dgvProfile.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProfile.RowHeadersVisible = false;
            dgvProfile.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
        }

        #region Resize

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox2_Resize(object sender, EventArgs e)
        {
            zResizeGroupBox2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox3_Resize(object sender, EventArgs e)
        {
            zResizeGroupBox3();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeGroupBox2()
        {
            int mid = (groupBox2.Width - 6) / 2;
            cmdMoveRight.Left = mid - (cmdMoveRight.Width / 2);
            cmdMoveLeft.Left = cmdMoveRight.Left;

            dgvAvailableDrawers.Width = ((cmdMoveRight.Left - (dgvAvailableDrawers.Left * 2)) -
                    dgvAvailableDrawers.Left) + 6;
            dgvUserDrawers.Left = cmdMoveRight.Left + cmdMoveRight.Width + dgvAvailableDrawers.Left;
            dgvUserDrawers.Width = dgvAvailableDrawers.Width;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeGroupBox3()
        {
            int mid = (groupBox3.Width / 2) - 2;
            label6.Left = mid - 5;
            label7.Left = label6.Left;

            tbFirstName.Width = (mid - 12) - tbFirstName.Left;
            tbMI.Width = tbFirstName.Width;
            tbSuffix.Width = tbFirstName.Width;
            tbProfileGroup.Width = tbFirstName.Width;
            tbLastName.Width = tbFirstName.Width;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            Trace.Enter(Trace.RtnName(mModName, "Init"));

            try
            {
                zFillinUserNamesGrid();
                zFillinDrawersForAdmin();
                zFillinProfCategories();
                zFillinProfGroups();
                zFillProfileList();
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "Init"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Init"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillProfileList()
        {
            Trace.Enter("zFillProfileList");

            try
            {
                DataTable ProfCategoryTable = mCommon.eCtrl.GetAllDefaultProfiles();
                dgvCurrentProfile.DataSource = ProfCategoryTable;
                dgvCurrentProfile.AllowUserToOrderColumns = false;
                dgvCurrentProfile.AllowUserToResizeColumns = true;
                dgvCurrentProfile.AllowUserToResizeRows = false;
                dgvCurrentProfile.ColumnHeadersVisible = true;

                dgvCurrentProfile.Columns["Category"].HeaderText = "Category";
                dgvCurrentProfile.Columns["Category"].Width = 100;
                
                dgvCurrentProfile.Columns["ProfKey"].HeaderText = "Name";
                dgvCurrentProfile.Columns["ProfKey"].Width = 120;

                int widthLeft = dgvCurrentProfile.Width - 
                        (22 + 3 + 
                            dgvCurrentProfile.Columns["Category"].Width + 
                            dgvCurrentProfile.Columns["ProfKey"].Width);
                dgvCurrentProfile.Columns["Description"].HeaderText = "Description";
                dgvCurrentProfile.Columns["Description"].Width = widthLeft / 2;

                dgvCurrentProfile.Columns["Val"].HeaderText = "Value";
                dgvCurrentProfile.Columns["Val"].Width = widthLeft / 2;

                dgvCurrentProfile.MultiSelect = false;
                dgvCurrentProfile.ReadOnly = true;
                dgvCurrentProfile.RowHeadersVisible = false;

                dgvCurrentProfile.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
            }
            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZException("zFillProfileList failed", ex);
                throw eze;
            }
            finally
            {
                Trace.Exit("zFillProfileList");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillinProfGroups()
        {
            string step = "";

            Trace.Enter("zFillinProfGroups");

            try
            {
                step = "Read data";
                DataTable ProfCategoryTable = mCommon.eCtrl.GetProfileCategories();
                lbUserGroups.DataSource = ProfCategoryTable;
                lbUserGroups.DisplayMember = "Category";
                lbUserGroups.ValueMember = "ProfCatID";
                if (ProfCategoryTable.Rows.Count > 0)
                {
                    lbUserGroups.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZException("zFillinProfGroups failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit("zFillinProfGroups");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddControl1_ItemClickedEvent(object sender, ToolStripItemClickedEventArgs e)
        {
            frmUser frm = null;

            Trace.Enter(Trace.RtnName(mModName, "ddControl1_ItemClickedEvent"));

            try
            {
                switch (e.ClickedItem.Text.ToLower())
                {
                    //case "doctor":
                    //    frm = new frmUser(mCommon, UserTypes.Doctor, -1, this.Left, this.Top, this.Width, this.Height);
                    //    break;

                    //case "clinical staff":
                    //    frm = new frmUser(mCommon, UserTypes.ClinicalStaff, -1, this.Left, this.Top, this.Width, this.Height);
                    //    break;

                    case "staff":
                        frm = new frmUser(mCommon, UserTypes.Staff, -1, this.Left, this.Top, this.Width, this.Height);
                        break;

                    default:
                        break;
                }

                if (frm != null)
                {
                    DialogResult dr = frm.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "ddControl1_ItemClickedEvent"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ddControl1_ItemClickedEvent"));
            }
        }

        private void frmSetupUsers_Resize(object sender, EventArgs e)
        {
            label11.Text = this.Width.ToString() + "x" + this.Height.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupBox1_Resize(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "groupBox1_Resize"));

            try
            {
                if ((dgvUserNames.Columns != null) && (dgvUserNames.Columns.Count > 2))
                {
                    int diff = this.Width - idealFormMinWidth;
                    if ((idealNameColWidth + diff) > 40)
                    {
                        dgvUserNames.Columns[2].Width = idealNameColWidth + diff;
                    }
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "groupBox1_Resize"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "groupBox1_Resize"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUserNames_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            pModels.PersonFormGetDemographics.PersonTypeEnum typ;
            frmUser frm = null;
            string temp = "";

            Trace.Enter(Trace.RtnName(mModName, "dgvUserNames_CellMouseDoubleClick"));

            try
            {
                temp = dgvUserNames.SelectedRows[0].Cells[1].Value.ToString();
                int pid = Convert.ToInt32(temp);
                typ = mCommon.pCtrl.GetPersonTypeByID(pid);
                switch (typ.ToString().ToLower())
                {
                    //case "doctor":
                    //    frm = new frmUser(mCommon, UserTypes.Doctor, pid, this.Left, this.Top, this.Width, this.Height);
                    //    break;

                    //case "clinical staff":
                    //    frm = new frmUser(mCommon, UserTypes.ClinicalStaff, pid, this.Left, this.Top, this.Width, this.Height);
                    //    break;

                    case "staff":
                        frm = new frmUser(mCommon, UserTypes.Staff, pid, this.Left, this.Top, this.Width, this.Height);
                        break;

                    default:
                        break;
                }

                if (frm != null)
                {
                    DialogResult dr = frm.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "dgvUserNames_CellMouseDoubleClick"));
                EZUtils.ExceptionDialog dlg = new ExceptionDialog(ex, "Edit/Add User failed");
                dlg.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "dgvUserNames_CellMouseDoubleClick"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUserNames_SelectionChanged(object sender, EventArgs e)
        {
            zSelectedUserChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillinDrawersForAdmin()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinDrawersForAdmin"));
            string step = "";

            try
            {
                step = "Read data";
                DataTable drawerTable = mCommon.eCtrl.GetAllDrawers();
                dgvAvailableDrawers.DataSource = drawerTable;

                dgvAvailableDrawers.AllowDrop = false;
                dgvAvailableDrawers.AllowUserToAddRows = false;
                dgvAvailableDrawers.AllowUserToDeleteRows = false;
                dgvAvailableDrawers.AllowUserToOrderColumns = false;
                dgvAvailableDrawers.AllowUserToResizeColumns = true;
                dgvAvailableDrawers.AllowUserToResizeRows = false;
                dgvAvailableDrawers.ColumnHeadersVisible = true;
                dgvAvailableDrawers.MultiSelect = true;
                dgvAvailableDrawers.ReadOnly = true;
                dgvAvailableDrawers.RowHeadersVisible = false;

                step = "Id";
                dgvAvailableDrawers.Columns["Id"].Visible = false;
                dgvAvailableDrawers.Columns["Id"].HeaderText = "ID";
                dgvAvailableDrawers.Columns["Id"].Width = 0;
                dgvAvailableDrawers.Columns["Id"].Name = "ID";
                dgvAvailableDrawers.Columns["Id"].Visible = false;

                step = "drawerName";
                dgvAvailableDrawers.Columns["drawerName"].HeaderText = "Available Drawers";
                dgvAvailableDrawers.Columns["drawerName"].Width = 179;
                //dgvAvailableDrawers.Columns["drawerName"].Name = "Name";
                dgvAvailableDrawers.Columns["drawerName"].Visible = true;

                step = "drawerDesc";
                dgvAvailableDrawers.Columns["drawerDesc"].HeaderText = "Description";
                dgvAvailableDrawers.Columns["drawerDesc"].Visible = true;

                step = "Seq";
                dgvAvailableDrawers.Columns["Seq"].Visible = false;
                dgvAvailableDrawers.Columns["Seq"].Width = 0;

                foreach (DataGridViewRow row in dgvAvailableDrawers.SelectedRows) 
                {
                    row.Selected = false;
                }

                dgvAvailableDrawers.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZException("zFillinDrawersForAdmin failed", ex);
                eze.Add("step", step);
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinDrawersForAdmin"));
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinDrawersForAdmin"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillinProfCategories()
        {
            string step = "";

            try
            {
                step = "Read data";
                DataTable ProfCategoryTable = mCommon.eCtrl.GetProfileCategories();
                cmbProfCategories.DataSource = ProfCategoryTable;
                cmbProfCategories.DisplayMember = "Category";
                cmbProfCategories.ValueMember = "ProfCatID";
                if (ProfCategoryTable.Rows.Count > 0)
                {
                    cmbProfCategories.SelectedIndex = 0;
                }
                rbDefaultProfile.Checked = true;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZException("zFillinProfCategories failed", ex);
                eze.Add("step", step);
                throw eze;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillinUserNamesGrid()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinUserNamesGrid"));

            try
            {
                DataTable usersTable = mCommon.uCtrl.GetSetupUserList();
                dgvUserNames.DataSource = usersTable;

                dgvUserNames.AllowDrop = false;
                dgvUserNames.AllowUserToAddRows = false;
                dgvUserNames.AllowUserToDeleteRows = false;
                dgvUserNames.AllowUserToOrderColumns = false;
                dgvUserNames.AllowUserToResizeColumns = true;
                dgvUserNames.AllowUserToResizeRows = false;
                dgvUserNames.ColumnHeadersVisible = true;
                dgvUserNames.MultiSelect = false;
                dgvUserNames.ReadOnly = true;
                dgvUserNames.RowHeadersVisible = false;

                dgvUserNames.Columns[0].Width = 45;
                dgvUserNames.Columns[0].HeaderText = "ID";
                dgvUserNames.Columns[0].Name = "ID";

                dgvUserNames.Columns[1].Width = 0;
                dgvUserNames.Columns[1].HeaderText = "PersonID";
                dgvUserNames.Columns[1].Name = "PersonID";
                dgvUserNames.Columns[1].Visible = false;

                dgvUserNames.Columns[2].Width = idealNameColWidth;
                dgvUserNames.Columns[2].HeaderText = "Name";
                dgvUserNames.Columns[2].Name = "Name";

                dgvUserNames.Columns[3].Width = 40;
                dgvUserNames.Columns[3].HeaderText = "Active";
                dgvUserNames.Columns[3].Name = "Active";

                dgvUserNames.Columns[4].Width = 40;
                dgvUserNames.Columns[4].HeaderText = "Sign";
                dgvUserNames.Columns[4].Name = "Sign";

                dgvUserNames.Columns[5].Width = 40;
                dgvUserNames.Columns[5].HeaderText = "Designate";
                dgvUserNames.Columns[5].Name = "Designate";

                dgvUserNames.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinUserNamesGrid"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinUserNamesGrid"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSelectedUserChanged()
        {
            pModels.PersonFormGetDemographics p = null;
            string temp = "";
            int pID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zSelectedUserChanged"));

            try
            {
                step = "Get personID";
                if (dgvUserNames.SelectedRows.Count > 0)
                {
                    temp = dgvUserNames.SelectedRows[0].Cells["PersonID"].Value.ToString();
                    pID = Convert.ToInt32(temp);

                    step = "Get Person Data";
                    p = mCommon.pCtrl.GetPersonByID(pID);

                    step = "Fill-in Name boxes";
                    tbFirstName.Text = p.FirstName;
                    tbMI.Text = p.MiddleName;
                    tbLastName.Text = p.LastName;
                    tbSuffix.Text = p.Suffix;
                    tbUserId.Text = dgvUserNames.SelectedRows[0].Cells["Id"].Value.ToString();
                    tbSharedId.Text = p.SharedID;
                    tbUserType.Text = p.PersonType.ToString();

                    groupBox2.Text = "Drawers for " + p.LastName + ", " + p.FirstName;
                }

                zSetupAssignedDrawers();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSelectedUserChanged failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSelectedUserChanged"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMoveRight_Click(object sender, EventArgs e)
        {
            int userId = -1;
            int drawerId = -1;
            int sDrawerId = -1;
            bool found = false;
            EZDeskDataLayer.User.Models.userDrawerItem userdrawer = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "cmdMoveRight_Click"));

            try
            {
                userId = Convert.ToInt32(dgvUserNames.SelectedRows[0].Cells["Id"].Value.ToString());
                if (userId > 0)
                {
                    DataGridViewSelectedRowCollection rows = dgvAvailableDrawers.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        //Search for duplicate tabs already in the assignedtab grid. Set
                        //found to true if we find a duplicate.
                        drawerId = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                        found = false;
                        foreach (DataGridViewRow sRow in dgvUserDrawers.Rows)
                        {
                            sDrawerId = Convert.ToInt32(sRow.Cells["drawerId"].Value.ToString());
                            if (drawerId == sDrawerId)
                            {
                                found = true;
                                break;
                            }
                        }

                        //If there were no duplicates found then add the tab to the drawertabs table.
                        if (!found)
                        {
                            userdrawer = new EZDeskDataLayer.User.Models.userDrawerItem();
                            userdrawer.Id = -1;
                            userdrawer.DrawerId = drawerId;
                            userdrawer.UserId = userId;
                            mCommon.uCtrl.AddEditUserDrawer(userdrawer);
                            zSetupAssignedDrawers();
                        }

                        row.Selected = false;   //Found or not, unselect the row.
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdMoveRight_Click failed", ex);
                eze.Add("drawerId", drawerId);
                eze.Add("userId", userId);
                eze.Add("sDrawerId", sDrawerId);
                eze.Add("found", found);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "cmdMoveRight_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMoveLeft_Click(object sender, EventArgs e)
        {
            int userId = -1;
            int sDrawerId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "cmdMoveLeft_Click"));

            try
            {
                userId = Convert.ToInt32(dgvUserNames.SelectedRows[0].Cells["Id"].Value.ToString());
                if (userId > 0)
                {
                    DataGridViewSelectedRowCollection rows = dgvUserDrawers.SelectedRows;
                    foreach (DataGridViewRow row in rows)
                    {
                        sDrawerId = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                        mCommon.uCtrl.DeleteUserDrawer(sDrawerId);
                    }
                    zSetupAssignedDrawers();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdMoveLeft_Click failed", ex);
                eze.Add("userId", userId);
                eze.Add("sDrawerId", sDrawerId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "cmdMoveLeft_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetupAssignedDrawers()
        {
            int userId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "zSetupAssignedDrawers"));

            try
            {
                if (dgvUserNames.SelectedRows.Count > 0)
                {
                    userId = Convert.ToInt32(dgvUserNames.SelectedRows[0].Cells["Id"].Value.ToString());
                    dgvUserDrawers.DataSource = mCommon.uCtrl.GetAllDrawersForUser(userId);

                    dgvUserDrawers.AllowUserToAddRows = false;
                    dgvUserDrawers.AllowUserToDeleteRows = false;
                    dgvUserDrawers.AllowUserToResizeColumns = true;
                    dgvUserDrawers.AllowUserToResizeRows = false;
                    dgvUserDrawers.ColumnHeadersVisible = true;
                    dgvUserDrawers.MultiSelect = true;
                    dgvUserDrawers.ReadOnly = true;
                    dgvUserDrawers.RowHeadersVisible = false;
                    dgvUserDrawers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    dgvUserDrawers.Columns["Id"].Visible = false;
                    dgvUserDrawers.Columns["Id"].Width = 0;

                    dgvUserDrawers.Columns["UserId"].Visible = false;
                    dgvUserDrawers.Columns["UserId"].Width = 0;

                    dgvUserDrawers.Columns["DrawerId"].Visible = false;
                    dgvUserDrawers.Columns["DrawerId"].Width = 0;

                    dgvUserDrawers.Columns["Created"].Visible = false;
                    dgvUserDrawers.Columns["Created"].Width = 0;

                    dgvUserDrawers.Columns["DrawerName"].HeaderText = "Name";
                    dgvUserDrawers.Columns["DrawerDesc"].HeaderText = "Description";

                    dgvUserDrawers.AlternatingRowsDefaultCellStyle = mCommon.AltRowGrey;
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zSetupAssignedDrawers failed", ex);
                eze.Add("userId", userId);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "zSetupAssignedDrawers"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillProfileGrid()
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "zFillProfileGrid"));
            int CategoryID = -1;
            DataTable profiles = null;
            int w = 0;
            int userId = -1;

            try
            {
                if (InProfileUserSetup == 0)
                {
                    CategoryID = (int)cmbProfCategories.SelectedValue;
                    if (rbDefaultProfile.Checked)
                    {
                        profiles = mCommon.eCtrl.GetProfileDefaults(CategoryID);
                    }

                    if (rbGroupProfile.Checked)
                    {
                    }

                    if (rbUserProfile.Checked)
                    {
                        userId = (int)listBox1.SelectedValue;
                        profiles = mCommon.eCtrl.GetProfileUsers(CategoryID, userId);
                    }

                    dgvProfile.DataSource = profiles;

                    dgvProfile.Columns["ProfID"].Visible = false;
                    dgvProfile.Columns["ProfID"].Width = 0;
                    dgvProfile.Columns["CategoryID"].Visible = false;
                    dgvProfile.Columns["CategoryID"].Width = 0;
                    dgvProfile.Columns["Security"].Visible = false;
                    dgvProfile.Columns["Security"].Width = 0;

                    dgvProfile.Columns["ProfKey"].HeaderText = "Profile Name";
                    dgvProfile.Columns["Val"].HeaderText = "Value";

                    dgvProfile.ReadOnly = true;

                    zResizeProfileGrid();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zFillProfileGrid failed", ex);
                eze.Add("CategoryID", CategoryID);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "zFillProfileGrid"));
            }
            //dgvProfile
        }

        /// <summary>
        /// 
        /// </summary>
        private void zResizeProfileGrid()
        {
            int w = 0;
            w = dgvProfile.Width - (dgvProfile.Columns["ProfKey"].Width + 20 + 2);
            dgvProfile.Columns["Description"].Width = w / 2;
            dgvProfile.Columns["Val"].Width = w / 2;
        }

        /// <summary>
        /// The user is selecting a new "context" (default/Group/User) with the a radio button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbProfileSelection_CheckedChanged(object sender, EventArgs e)
        {
            string radioButton = "";

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "rbProfileSelection_CheckedChanged"));

            try
            {
                if (((RadioButton)sender).Checked)
                {
                    if (((RadioButton)sender) == rbDefaultProfile)
                    {
                        radioButton = "DefaultProfile";
                        label9.Visible = false;
                        listBox1.Visible = false;
                        zFillProfileGrid();
                    }

                    if (((RadioButton)sender) == rbGroupProfile)
                    {
                        radioButton = "GroupProfile";
                        label9.Text = "Profile User Group";
                        label9.Visible = true;
                        listBox1.Visible = true;
                        zFillProfileGrid();
                    }

                    if (((RadioButton)sender) == rbUserProfile)
                    {
                        radioButton = "UserProfile";
                        label9.Text = "User";
                        label9.Visible = true;
                        listBox1.Visible = true;
                        listBox1.Items.Clear();
                        zFillProfileUsers();
                        zFillProfileGrid();
                    }
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("rbProfileSelection_CheckedChanged failed", ex);
                eze.Add("radioButton", radioButton);
                EZUtils.EventLog.WriteErrorEntry(eze);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Switch profile context failed");
                frm.ShowDialog();
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "rbProfileSelection_CheckedChanged"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillProfileUsers()
        {
            DataTable tbl = null;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "zFillProfileUsers"));

            try
            {
                InProfileUserSetup = 1;
                tbl = mCommon.uCtrl.GetSetupUserList();
                listBox1.DataSource = tbl;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "ID";
                InProfileUserSetup = 0;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("zFillProfileUsers failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "zFillProfileUsers"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProfile_Resize(object sender, EventArgs e)
        {
            if (dgvProfile.DataSource != null)
            {
                zResizeProfileGrid();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProfile_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProfile.DataSource != null)
            {
                DataGridViewSelectedRowCollection selRow = dgvProfile.SelectedRows;
                if (selRow.Count > 0)
                {
                    tbProfileName.Text = (String)selRow[0].Cells["ProfKey"].Value;
                    tbProfileDescription.Text = (String)selRow[0].Cells["Description"].Value;
                    tbProfileValue.Text = (String)selRow[0].Cells["Val"].Value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            int catID = -1;
            int userID = -1;
            int groupID = -1;
            int profID = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "button1_Click"));
            try
            {
                catID = (int)cmbProfCategories.SelectedValue;
                if ((dgvProfile.DataSource != null) && (dgvProfile.Rows.Count > 0))
                {
                    profID = (int)dgvProfile.SelectedRows[0].Cells["ProfID"].Value;
                }

                //We are in Default Profile mode.
                if (rbDefaultProfile.Checked)
                {
                    EZDeskDataLayer.ehr.Models.ProfileDefault defaultItem =
                        new EZDeskDataLayer.ehr.Models.ProfileDefault(tbProfileName.Text,
                                                                    catID, tbProfileValue.Text,
                                                                    tbProfileDescription.Text);
                    defaultItem.ProfID = profID;
                    mCommon.eCtrl.WriteProfileDefault(defaultItem);
                }

                //We are in Group Profile mode.
                if (rbGroupProfile.Checked)
                {
                    //TODO: Save a GroupProfile
                }

                //We are in User Profile mode.
                if (rbUserProfile.Checked)
                {
                    userID = (int)listBox1.SelectedValue;
                    EZDeskDataLayer.ehr.Models.ProfileUsers userItem =
                        new EZDeskDataLayer.ehr.Models.ProfileUsers(catID, profID, tbProfileValue.Text, userID);
                    mCommon.eCtrl.WriteProfileUsers(userItem);
                }
                
                zFillProfileGrid();
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("button1_Click failed", ex);
                EZUtils.EventLog.WriteErrorEntry(eze);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Save profile setting failed");
                frm.ShowDialog();
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "button1_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProfCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "rbDefaultProfile_CheckedChanged"));

            try
            {
                if (rbDefaultProfile.Checked)
                {
                    zFillProfileGrid();
                }
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("rbDefaultProfile_CheckedChanged failed", ex);
                throw eze;
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "rbDefaultProfile_CheckedChanged"));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            zFillProfileGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Trace.Enter("cmdAdd_Click");

            groupBox8.Visible = true;
            groupBox8.Enabled = true;
            tbGroupName.Text = "";
            tbGroupDescription.Text = "";
            cmdGroupAdd.Text = "Add";
            groupBox6.Enabled = false;

            Trace.Exit("cmdAdd_Click");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroupCancel_Click(object sender, EventArgs e)
        {
            Trace.Enter("btnGroupCancel_Click");

            groupBox6.Enabled = true;
            groupBox8.Visible = false;
            groupBox8.Enabled = false;

            Trace.Exit("btnGroupCancel_Click");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGroupAdd_Click(object sender, EventArgs e)
        {
            Trace.Enter("cmdGroupAdd_Click");

            try
            {
                //TODO: INSERT/Update the value
                zFillinProfGroups();        //Refill the listbox to catch the new/updated item
                groupBox6.Enabled = true;
                groupBox8.Visible = false;
                groupBox8.Enabled = false;
            }

            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdGroupAdd_Click failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit("cmdGroupAdd_Click");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            Trace.Enter("cmdEdit_Click");

            try
            {
                if (lbUserGroups.SelectedIndex > -1)
                {
                    groupBox8.Visible = true;
                    groupBox8.Enabled = true;
                    tbGroupName.Text = "";
                    tbGroupDescription.Text = "";
                    cmdGroupAdd.Text = "Save";
                    groupBox6.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You must select an item to Edit", "Edit Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdEdit_Click failed", ex);
                throw eze;
            }
            finally
            {
                Trace.Exit("cmdEdit_Click");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            Trace.Enter("cmdDelete_Click");

            try
            {
                if (lbUserGroups.SelectedIndex > -1)
                {
                    //TODO: Mark the entry as deleted
                    zFillinProfGroups();        //Refill the listbox to remove deleted item.
                }
                else
                {
                    MessageBox.Show("You must select an item to Edit", "Edit Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("cmdDelete_Click failed", ex);
                throw eze;
            }
            finally
            {
                Trace.Exit("cmdDelete_Click");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCurrentProfile_Resize(object sender, EventArgs e)
        {
            Trace.Enter("dgvCurrentProfile_Resize");

            try
            {
                if (dgvCurrentProfile.Rows.Count > 0)
                {
                    int widthLeft = dgvCurrentProfile.Width -
                        (22 + 3 +
                            dgvCurrentProfile.Columns["Category"].Width +
                            dgvCurrentProfile.Columns["ProfKey"].Width);
                    dgvCurrentProfile.Columns["Description"].Width = widthLeft / 2;
                    dgvCurrentProfile.Columns["Val"].Width = widthLeft / 2;
                }
            }
            catch (Exception ex)
            {
                EZUtils.EZException eze = new EZUtils.EZException("dgvCurrentProfile_Resize failed", ex);
                throw eze;
            }
            finally
            {
                Trace.Exit("dgvCurrentProfile_Resize");
            }

        }

    }
}
