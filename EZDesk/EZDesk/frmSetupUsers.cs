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
using EZDeskDataLayer.User;
using EZDeskDataLayer.User.Models;
using EZDeskDataLayer.Documents;
using EZUtils;
using pModels = EZDeskDataLayer.Person.Models;

namespace EZDesk
{
    public partial class frmSetupUsers : Form
    {
        private MySqlConnection mConn = null;
        private UserController uCtrl = null;
        private EZDeskDataLayer.Person.PersonCtrl pCtrl = null;
        private DocumentsController docCtrl = null;
        const int idealFormMinWidth = 741;
        const int idealNameColWidth = 90;
        private bool inSetup = false;
        private string mModName = "frmSetupUsers";
 
        /// <summary>
        /// 
        /// </summary>
        public frmSetupUsers(MySqlConnection Conn)
        {
            InitializeComponent();
            inSetup = true;
            zResizeGroupBox2();
            zResizeGroupBox3();
            mConn = Conn;
            uCtrl = new UserController(mConn);
            docCtrl = new DocumentsController(mConn);
            pCtrl = new EZDeskDataLayer.Person.PersonCtrl(mConn);
            ddControl1.Text = "New";
            List<string> lstSting = new List<string>();
            foreach (ToolStripItem item in contextMenuStrip1.Items)
            {
                lstSting.Add(item.Text);
            }
            ddControl1.FillControlList(lstSting);
            inSetup = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSetupUsers_Load(object sender, EventArgs e)
        {
            int i = 1;
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
                inSetup = true;
                zFillinUserNamesGrid();
                zFillinDrawersForAdmin();
                inSetup = false;
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
        private void zFillinUserNamesGrid()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinUserNamesGrid"));

            try
            {
                DataTable usersTable = uCtrl.GetSetupUserList();
                dgvUserNames.DataSource = usersTable;
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
        private void zFillinDrawersForAdmin()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillinDrawersForAdmin"));

            try
            {
                DataTable drawerTable = docCtrl.GetAvailableDrawers(rbAlphabetically.Checked);
                dgvAvailableDrawers.DataSource = drawerTable;

                dgvAvailableDrawers.Columns[0].Visible = false;
                dgvAvailableDrawers.Columns[0].HeaderText = "ID";
                dgvAvailableDrawers.Columns[0].Width = 0;
                dgvAvailableDrawers.Columns[0].Name = "ID";

                dgvAvailableDrawers.Columns[1].HeaderText = "Available Drawers";
                dgvAvailableDrawers.Columns[1].Width = 179;
                dgvAvailableDrawers.Columns[1].Name = "Name";
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "zFillinDrawersForAdmin"));
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillinDrawersForAdmin"));
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
                    case "doctor":
                        frm = new frmUser(mConn, UserTypes.Doctor, -1);
                        break;

                    case "clinical staff":
                        frm = new frmUser(mConn, UserTypes.ClinicalStaff, -1);
                        break;

                    case "staff":
                        frm = new frmUser(mConn, UserTypes.Staff, -1);
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
                typ = pCtrl.GetPersonTypeByID(pid);
                switch (typ.ToString().ToLower())
                {
                    case "doctor":
                        frm = new frmUser(mConn, UserTypes.Doctor, pid);
                        break;

                    case "clinical staff":
                        frm = new frmUser(mConn, UserTypes.ClinicalStaff, pid);
                        break;

                    case "staff":
                        frm = new frmUser(mConn, UserTypes.Staff, pid);
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
                throw ex;
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
            pModels.PersonFormGetDemographics p = null;
            string temp = "";
            int pID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "dgvUserNames_SelectionChanged"));

            try
            {
                if (!inSetup)
                {
                    step = "Get personID";
                    temp = dgvUserNames.SelectedRows[0].Cells["PersonID"].Value.ToString();
                    pID = Convert.ToInt32(temp);

                    step = "Get Person Data";
                    p = pCtrl.GetPersonByID(pID);

                    step = "Fill-in Name boxes";
                    tbFirstName.Text = p.FirstName;
                    tbMI.Text = p.MiddleName;
                    tbLastName.Text = p.LastName;
                    tbSuffix.Text = p.Suffix;
                }
            }

            catch (Exception ex)
            {
                //#Trace.ExceptionDataKeyAdd(ex.Data, "Rtn", Trace.RtnName(mModName, "dgvUserNames_SelectionChanged"));
                //#Trace.ExceptionDataKeyAdd(ex.Data, "personid", pID.ToString());
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "dgvUserNames_SelectionChanged"));
            }
        }

    }
}
