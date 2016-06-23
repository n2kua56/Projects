using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using EZDeskDataLayer;

namespace EZTeller
{
    public partial class Form1 : Form
    {
        private string mModName = "Teller_Form1";
        private int mSetupSw = 0;
        private int mModified = 0;
        private int mPplKey = -1;
        private DataTable mSelPeople = null;
        private bool mInPeopleCBUpdate = false;
        private string mNameSoFar = "";
        private string mOrigVal = "";
        private Reports mReports = null;

        public SummaryPage sumPage;
        public CountsPage countPage;
        public DetailPage dtlPage;
        public PeoplePage pplPage;
        public ReconcilePage recPage;

        public EZDeskDataLayer.EZTeller.TellerCtrl tCtrl = null;

        private int mSeqNo = -1;
        public int SeqNo
        {
            get { return mSeqNo; }
            set { mSeqNo = value; }
        }

        private Form mEZDeskForm1 = null;

        private EZDeskCommon mCommon;
        public Form1(EZDeskCommon Common, Form ezdeskForm1)
        {
            InitializeComponent();
            mCommon = Common;

            Trace.Enter(Trace.RtnName(mModName, "Form1_Load"));

            try
            {
                sumPage = new SummaryPage(mCommon, this);
                countPage = new CountsPage(mCommon, this);
                dtlPage = new DetailPage(mCommon, this);
                pplPage = new PeoplePage(mCommon, this);
                recPage = new ReconcilePage(mCommon, this);
                tCtrl = new EZDeskDataLayer.EZTeller.TellerCtrl(mCommon);

                mEZDeskForm1 = ezdeskForm1;
                mReports = new Reports(this);
                zMergeMenus();          //Manage the menu
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("EZTeller Constructor failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Load"));
            }
        }

        /// <summary>
        /// Merge the EZTeller menus into the EZDesk menus
        /// </summary>
        private void zMergeMenus()
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zMergeMenus"));

            try
            {
                step = "File Menu";
                zMergeMenu("fileToolStripMenuItem", "fileToolStripMenuItem");

                step = "Reports Menu";
                zMergeMenu("reportsToolStripMenuItem", "reportToolStripMenuItem");

                step = "Help Menu";
                zMergeMenu("helpToolStripMenuItem", "helpToolStripMenuItem");

                step = "Hide EZTeller menu";
                menuStrip1.Visible = false;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zMergeMenus failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zMergeMenus"));
            }
        }

        /// <summary>
        /// Merge the specified menus
        /// </summary>
        /// <param name="EZDeskMenuItemName"></param>
        /// <param name="EZTellerMenuItemName"></param>
        private void zMergeMenu(string EZDeskMenuItemName, string EZTellerMenuItemName)
        {
            ToolStripItem[] mainFileSubMenu = null;
            ToolStripItem[] tellerFileSubMenu = null;
            ToolStripItem[] eztellthere = null;
            ToolStripItem[] items = null;
            ToolStripItem eztellerFile = null;
            string MenuName = "EZTeller";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "zMergMenu"));

            try
            {
                step = "Finding Main Menu Item to merge into";
                //Find the "File" main menu item in the EZDesk Form1 Main menu.
                mainFileSubMenu = mEZDeskForm1.MainMenuStrip.Items.Find(EZDeskMenuItemName, false);
                if (mainFileSubMenu.Length == 1)
                {
                    step = "Main Menu Item found, is EZTeller already there?";
                    eztellthere = menuStrip1.Items.Find(MenuName, false);
                    //Make sure there is only one EZTeller item.
                    if (eztellthere.Length == 0)
                    {
                        step = "EZTeller not already merged, find EZTeller menu to merge";
                        //Find the "File main menu item in the EZTeller Form1 Main menu.
                        tellerFileSubMenu = menuStrip1.Items.Find(EZTellerMenuItemName, false);
                        if (tellerFileSubMenu.Length == 1)
                        {
                            step = "Creatge the new EZTeller menu item";
                            //Create the new EZTeller file menu item from the File menu item 
                            //  in the EZTeller Form1 Main Menu File menu item.
                            eztellerFile = ((ToolStripMenuItem)mainFileSubMenu[0]).DropDownItems.Add("EZTeller");
                            eztellerFile.AllowDrop = true;

                            step = "Merge in the menu items from the EZTeller menu to the new Menu Item";
                            //Convert the DropDownItems collection to an array of menu items.
                            items = zCollectionToArray(((ToolStripMenuItem)tellerFileSubMenu[0]).DropDownItems);

                            //And finally add items to the new File->EZTeller item.
                            ((ToolStripMenuItem)eztellerFile).DropDownItems.AddRange(items);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zMergMenu Failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zMergMenu"));
            }
        }

        /// <summary>
        /// Convert the DropDownItems collection to an array of menu items.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private ToolStripItem[] zCollectionToArray(ToolStripItemCollection collection)
        {
            int idx = 0;
            int exitIdx = -1;
            string itemName = "";
            ToolStripItem[] items = new ToolStripItem[collection.Count];

            Trace.Enter(Trace.RtnName(mModName, "zCollectionToArray"));

            try
            {
                foreach (ToolStripItem i in collection)
                {
                    itemName = i.Text;
                    items[idx] = i;
                    idx++;
                }

                //Remove null from EXIT not being done
                items = items.Where(w => w.Text.ToLower() != "exit").ToArray();
                
                return items;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zCollectionToArray failed", ex);
                eze.Add("idx", idx);
                eze.Add("itemName", itemName);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCollectionToArray"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "Form1_Load"));

            try
            {
                step = "";
                //#mProperties = EZTeller2.Properties.Settings.Default.Properties;
                mSetupSw = 1;
                this.dteRunDate.Value = DateTime.Now;
                BatchNo.Value = 1;
                mSetupSw = 0;

                cbType.SelectedIndex = 0;

                lblTitle.Text = mCommon.eCtrl.GetProperty("Teller_ChurchName") +
                    " EZ Teller Input";

                //#step = "version";
                //#zGetVersion();
                //#step = "";

                cbNames.Top = tbName.Top;
                cbNames.Left = tbName.Left;
                cbNames.Visible = false;
                tbName.Visible = true;

                tabControl1.SelectedIndex = 0;
                sumPage.UpdateSummaryPage(dteRunDate.Value, Convert.ToInt32(BatchNo.Value));
                tabControl1.TabPages[0].Tag = "";
                for (int idx = 1; idx < tabControl1.TabCount; idx++)
                {
                    tabControl1.TabPages[idx].Tag = "U";
                }

                Trace.WriteLine(Trace.TraceLevels.Error, "Setting up the Count page");
                countPage.Init();
                lblKey.Text = "";
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed (step = " + step + ")", ex);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Load"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dteRunDate_DropDown(object sender, EventArgs e)
        {
            mSetupSw = 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dteRunDate_CloseUp(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "dteRunDate_CloseUp"));

            try
            {
                mSetupSw = 0;
                dteRunDate_ValueChanged(sender, e);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "dteRunDate_CloseUp"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dteRunDate_ValueChanged(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "dteRunDate_ValueChanged"));

            try
            {
                if (mSetupSw == 0)
                {
                    sumPage.UpdateSummaryPage(dteRunDate.Value,
                                    Convert.ToInt32(BatchNo.Value));
                    dtlPage.FillDetailPage(dteRunDate.Value, Convert.ToInt32(BatchNo.Value));

                    // Mark each page as needing updating except the current page.
                    for (int idx = 0; idx < tabControl1.TabCount; idx++)
                    {
                        if (idx != tabControl1.SelectedIndex)
                        {
                            tabControl1.TabPages[idx].Tag = "U";
                        }
                        else
                        {
                            tabControl1.TabPages[idx].Tag = "";
                        }
                    }

                    // Fix the current page.
                    switch (tabControl1.SelectedIndex)
                    {
                        case 0:
                            // Summary Page
                            System.Windows.Forms.Application.DoEvents();
                            sumPage.UpdateSummaryPage(dteRunDate.Value,
                                Convert.ToInt32(BatchNo.Value));
                            System.Windows.Forms.Application.DoEvents();
                            break;

                        case 1:
                            // Details Page
                            break;

                        case 2:
                            // People Page
                            System.Windows.Forms.Application.DoEvents();
                             pplPage.FillPeoplePage(dteRunDate.Value, "'Y'");
                            System.Windows.Forms.Application.DoEvents();
                            break;

                        case 3:
                            //Fill-in Reconcile Checks
                            break;

                        case 4:
                            // Counts Page
                            break;

                        default:
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "dteRunDate_ValueChanged"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void StatusMessage(string text)
        {
            toolStripStatusLabel1.Text = text;
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "deleteToolStripMenuItem_Click"));

            try
            {
                countPage.DeleteCount();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("deleteToolStripMenuItem_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "deleteToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "renameToolStripMenuItem_Click"));

            try
            {
                countPage.RenameCount();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("renameToolStripMenuItem_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "renameToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "addToolStripMenuItem_Click"));

            try
            {
                //? mEZDeskForm1.AddEditPerson(-1, 1);
                //Contributor frm = new Contributor(this, "0");
                //DialogResult dr = frm.ShowDialog();
                //frm.Dispose();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("addToolStripMenuItem_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "addToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int peopleID = -1;
            string val = "";
            DataGridViewRow row = null;

            Trace.Enter(Trace.RtnName(mModName, "editToolStripMenuItem_Click"));

            try
            {
                row = dgPeople.SelectedRows[0];
                val = row.Cells[0].Value.ToString();
                //? mEZDeskForm1.AddEditPerson(val, 1);

                //Contributor frm = new Contributor(this, val);
                //DialogResult dr = frm.ShowDialog();
                //if (dr == DialogResult.OK)
                //{
                //    // Need to refresh the people screen
                //}
                //frm.Dispose();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("editToolStripMenuItem_Click Failed ", ex);
                EZex.Add("peopleID", peopleID);
                EZex.Add("val", val);
                EZex.Add("row", row);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "editToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Toggle the Archvie (active) flag
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((TabControl)sender).SelectedTab == tabPagePeople) { pplPage.FillPeoplePage(DateTime.Now, "sort"); }
            //if (((TabControl)sender).SelectedTab == tabPateCounts) { countPage.FillCountPage(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl2_Resize(object sender, EventArgs e)
        {
            try
            {
                int w = this.Width;
                Size sz = tabControl2.ItemSize;
                w = (tabControl2.Width / 27);
                if (w != sz.Width)
                {
                    sz.Width = w;
                    tabControl2.ItemSize = sz;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Failed", ex);
                throw eze;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Import People not implemented yet", 
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Import Details not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Import Counts not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export People is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void givingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export Giving is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export Counts is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Backup is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Restore is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmSetup frm = new frmSetup(mCommon, this);
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commentsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comments Report is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contributorLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contributor Letters is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Labels is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void envelopeNumberReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Envelope Number Report is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noEnvelopeUseageReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No Envelope Useage Report is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void adminReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Admin Report is not implemented yet",
                                "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox frm = new AboutBox();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cbType_SelectedIndexChanged"));
            try
            {
                tbEnvNo.Focus();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("cbType_SelectedIndexChanged Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cbType_SelectedIndexChanged"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEnvNo_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dt = null;
            double num = -1;
            string val = "";
            string EnvNo = "";
            bool beep = true;

            Trace.Enter(Trace.RtnName(mModName, "tbEnvNo_KeyDown"));

            try
            {
                toolStripStatusLabel1.Text = "";

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        zEndOfEntry();
                        mModified = 0;
                        groupBox1.BackColor = SystemColors.Control;
                        e.Handled = true;
                        lblKey.Text = "";
                        break;

                    case Keys.Tab:
                        if (sender == tbEnvNo)
                        {
                            EnvNo = tbEnvNo.Text.Trim();
                            if (EnvNo == "0")
                            {
                                EnvNo = "";
                            }
                            if (EnvNo.Length == 0)
                            {
                                tbName.Visible = false;
                                cbNames.Visible = true;
                                cbNames.Focus();
                            }
                            else
                            {
                                dt = tCtrl.GetContributorWithEnvNo(EnvNo);
                                if (dt.Rows.Count == 0)
                                {
                                    toolStripStatusLabel1.Text = "Can't find the Envelope Number";
                                    Console.Beep();
                                    tbEnvNo.Focus();
                                }
                                else
                                {
                                    tbName.Text =
                                        dt.Rows[0].ItemArray[0] + ", " +
                                            dt.Rows[0].ItemArray[1] + " " +
                                            dt.Rows[0].ItemArray[2] + " | " +
                                            dt.Rows[0].ItemArray[3];
                                    tbName.Visible = true;
                                    cbNames.Visible = false;
                                    tbGeneral.Focus();
                                    mPplKey = Convert.ToInt32(dt.Rows[0].ItemArray[3]);
                                }
                            }
                        }

                        if (sender == cbNames)
                        {
                        }

                        if (sender == tbGeneral)
                        {
                            tbBuilding.Focus();
                        }

                        if (sender == tbBuilding)
                        {
                            tbMissions.Focus();
                        }

                        if (sender == tbMissions)
                        {
                            tbDesignated.Focus();
                        }

                        if (sender == tbDesignated)
                        {
                            tbComment.Focus();
                        }

                        if (sender == tbComment)
                        {
                            tbEnvNo.Focus();
                        }
                        e.SuppressKeyPress = true;
                        e.Handled = true;
                        break;

                    default:
                        // For the numeric fields don't allow non-numerics or dots.
                        if ((sender == tbGeneral) ||
                            (sender == tbBuilding) ||
                            (sender == tbMissions) ||
                            (sender == tbDesignated))
                        {
                            beep = false;
                            val = ((TextBox)sender).Text.Trim();    //This does NOT have the current character typed.
                            Keys k = e.KeyData;
                            if ((k == Keys.Back) ||
                                (k == Keys.Left) ||
                                (k == Keys.Right) ||
                                (k == Keys.Delete))
                            {
                            }
                            else
                            {
                                if (Convert.ToChar(e.KeyData) == '¾')   //KeyData returns ¾ for decimal
                                {
                                    val += ".";
                                }
                                else
                                {
                                    val = val + Convert.ToChar(e.KeyValue);
                                }
                                if (val != ".")
                                {
                                    if (!double.TryParse(val, out num))
                                    {
                                        toolStripStatusLabel1.Text = "Invalid character";
                                        beep = true;
                                    }
                                }
                            }

                            if (beep)
                            {
                                Console.Beep();
                                e.Handled = true;
                                e.SuppressKeyPress = true;
                            }
                        }

                        if (((groupBox1.BackColor == Color.LightCoral) ||
                             (groupBox1.BackColor == Color.PaleGreen)) &&
                            (toolStripStatusLabel1.Text.Length == 0))
                        {
                            toolStripStatusLabel1.Text = "Press 'Enter' to record";
                        }

                        break;
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("tbEnvNo_KeyDown Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tbEnvNo_KeyDown"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zEndOfEntry()
        {
            int err = 0;

            Trace.Enter(Trace.RtnName(mModName, "zEndOfEntry"));

            try
            {
                //Make sure we have the right data
                //If right data, write the data to the table
                //   Clear the fields
                //   put the cursor back to env number
                // Else display message
                //   put cursor in first bad field
                err = zCheckType();
                if (err == 0)
                {
                    err = zCheckContributor();
                }
                if (err == 0)
                {
                    err = zCheckAmount();
                }

                if (err == 0)
                {
                    if (mSeqNo == -1)
                    {
                        zAddContribution();
                    }
                    else
                    {
                        zUpdateContribution();
                        dteRunDate.Enabled = true;
                        BatchNo.Enabled = true;
                        cbType.Enabled = true;
                        tbEnvNo.ReadOnly = false;
                        tbName.ReadOnly = false;
                        cbNames.Enabled = true;
                        mSeqNo = -1;
                    }
                    zClearInput();
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zEndOfEntry Failed ", ex);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zEndOfEntry failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zEndOfEntry"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zClearInput()
        {
            Trace.Enter(Trace.RtnName(mModName, "zClearInput"));

            try
            {
                tbEnvNo.Text = "";
                tbName.Text = "";
                tbName.Visible = true;
                cbNames.Text = "";
                cbNames.DataSource = null;
                cbNames.Items.Clear();
                cbNames.Visible = false;
                tbGeneral.Text = "";
                tbBuilding.Text = "";
                tbMissions.Text = "";
                tbDesignated.Text = "";
                zFillInTotal();
                tbComment.Text = "";
                tbEnvNo.Focus();
                toolStripStatusLabel1.Text = "";
                groupBox1.BackColor = SystemColors.Control;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zClearInput Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zClearInput"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillInTotal()
        {
            double total = 0.00;
            double val = 0.00;

            Trace.Enter(Trace.RtnName(mModName, "zFillInTotal"));

            try
            {
                if (tbGeneral.Text.Trim().Length > 0)
                {
                    val = Convert.ToDouble(tbGeneral.Text.Trim());
                    total += val;
                    tbGeneral.Text = val.ToString("0.00");
                }
            }
            catch { }

            try
            {
                if (tbBuilding.Text.Trim().Length > 0)
                {
                    val = Convert.ToDouble(tbBuilding.Text.Trim());
                    total += val;
                    tbBuilding.Text = val.ToString("0.00");
                }
            }
            catch { }

            try
            {
                if (tbMissions.Text.Trim().Length > 0)
                {
                    val = Convert.ToDouble(tbMissions.Text.Trim());
                    total += val;
                    tbMissions.Text = val.ToString("0.00");
                }
            }
            catch { }

            try
            {
                if (tbDesignated.Text.Trim().Length > 0)
                {
                    val = Convert.ToDouble(tbDesignated.Text.Trim());
                    total += val;
                    tbDesignated.Text = val.ToString("0.00");
                }
            }
            catch { }

            lblTotal.Text = total.ToString("###,##0.00");

            Trace.Exit(Trace.RtnName(mModName, "zFillInTotal"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int zCheckType()
        {
            int rtn = 0;

            Trace.Enter(Trace.RtnName(mModName, "zCheckType"));

            try
            {
                if (cbType.Text.Trim().Length == 0)
                {
                    MessageBox.Show("You MUST specify a contribution type",
                        "Entry Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Trace.WriteLine(Trace.TraceLevels.Error,
                        "Contribution Type missing");
                    rtn = 1;
                    cbType.Focus();
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zCheckType Failed ", ex);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zCheckType failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCheckType"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int zCheckContributor()
        {
            int rtn = 0;

            Trace.Enter(Trace.RtnName(mModName, "zCheckContributor"));

            try
            {
                if ((cbNames.Text.Trim().Length == 0) &&
                    (tbName.Text.Trim().Length == 0) &&
                    (tbEnvNo.Text.Trim().Length == 0))
                {
                    MessageBox.Show("You MUST select a contributor",
                        "Entry Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Trace.WriteLine(Trace.TraceLevels.Error,
                            "Contributor missing");
                    rtn = 1;
                    tbEnvNo.Focus();
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zCheckContributor Failed ", ex);
                EZex.Add("cbNames", cbNames.Text);
                EZex.Add("tbName", tbName.Text);
                EZex.Add("tbEnvNo", tbEnvNo.Text);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zCheckContributor failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCheckContributor"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int zCheckAmount()
        {
            int rtn = 0;

            Trace.Enter(Trace.RtnName(mModName, "zCheckAmount"));

            try
            {
                if ((tbGeneral.Text.Trim().Length == 0) &&
                    (tbBuilding.Text.Trim().Length == 0) &&
                    (tbMissions.Text.Trim().Length == 0) &&
                    (tbDesignated.Text.Trim().Length == 0))
                {
                    MessageBox.Show("You MUST enter a contribution",
                              "Entry Error", MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                    Trace.WriteLine(Trace.TraceLevels.Error,
                            "Contribution missing");
                    rtn = 1;
                    tbGeneral.Focus();
                }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zCheckAmount Failed ", ex);
                EZex.Add("tbGeneral", tbGeneral.Text);
                EZex.Add("tbBuilding", tbBuilding.Text);
                EZex.Add("tbMissions", tbMissions.Text);
                EZex.Add("tbDesignated", tbDesignated.Text);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zCheckContributor failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zCheckAmount"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddContribution()
        {
            int recs = -1;
            double general = 0.00;
            double building = 0.00;
            double missions = 0.00;
            double designated = 0.00;
            int cntrbType = -1;

            Trace.Enter(Trace.RtnName(mModName, "zAddContribution"));

            try
            {
                general = tbGeneral.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbGeneral.Text.Trim());
                building = tbBuilding.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbBuilding.Text.Trim());
                missions = tbMissions.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbMissions.Text.Trim());
                designated = tbDesignated.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbDesignated.Text.Trim());

                cntrbType = zSetContributionType(tbEnvNo.Text.Trim(), cbType.Text.Trim().ToUpper());

                recs = tCtrl.InsertContribution(
                    dteRunDate.Value.ToString("yyyy-MM-dd"),
                    BatchNo.Value.ToString(),
                    mPplKey.ToString(), general, building, missions, designated,
                    cntrbType.ToString(), tbComment.Text.Trim(), tbEnvNo.Text.Trim());

                System.Windows.Forms.Application.DoEvents();
                switch (tabControl1.SelectedIndex)
                {
                    case 0:     //Summary
                        sumPage.UpdateSummaryPage(dteRunDate.Value,
                            Convert.ToInt32(BatchNo.Value));
                        break;

                    case 1:     //Detail
                        dtlPage.FillDetailPage(dteRunDate.Value,
                                Convert.ToInt32(BatchNo.Value));
                        //dgDetail.FirstDisplayedScrollingRowIndex;
                        //dgDetail.DisplayedRowCount;
                        //dgDetail.Rows.Count;
                        int topRow = dgvDetail.Rows.Count - dgvDetail.DisplayedRowCount(true);
                        if (topRow < 0)
                        {
                            topRow = 0;
                        }
                        dgvDetail.FirstDisplayedScrollingRowIndex = topRow;
                        break;

                    case 2:     //People
                        pplPage.FillPeoplePage(dteRunDate.Value, "'Y'");
                        break;

                    case 3:     //Reconcile Checks;
                        break;

                    case 4:     //Counts
                        ///countPage.CountSetYears();
                        break;

                    default:
                        break;
                }

                System.Windows.Forms.Application.DoEvents();

                // Reset the Tag property of the tab pages to show which
                // need updating.
                for (int idx = 0; idx < tabControl1.TabCount; idx++)
                {
                    if (idx != tabControl1.SelectedIndex)
                    {
                        tabControl1.TabPages[idx].Tag = "U";
                    }
                    else
                    {
                        tabControl1.TabPages[idx].Tag = "";
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zAddContribution Failed ", ex);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zAddContribution failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zAddContribution"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="paymentType"></param>
        /// <param name="cntrbType"></param>
        /// <returns></returns>
        private int zSetContributionType(string env, string paymentType)
        {
            int cntrbType = -1;

            if (env == "998")
            {
                if (paymentType == "CASH")
                {
                    cntrbType = 2;
                }
                if (paymentType == "CHECK")
                {
                    cntrbType = 4;
                }
            }

            if (env == "999")
            {
                cntrbType = 1;
            }

            if (cntrbType == -1)
            {
                if (paymentType == "CASH")
                {
                    cntrbType = 0;
                }
                if (paymentType == "CHECK")
                {
                    cntrbType = 3;
                }
            }

            return cntrbType;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zUpdateContribution()
        {
            int recs = -1;
            double general = 0.00;
            double building = 0.00;
            double missions = 0.00;
            double designated = 0.00;

            Trace.Enter(Trace.RtnName(mModName, "zUpdateContribution"));

            try
            {
                general = tbGeneral.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbGeneral.Text.Trim());
                building = tbBuilding.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbBuilding.Text.Trim());
                missions = tbMissions.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbMissions.Text.Trim());
                designated = tbDesignated.Text.Trim().Length == 0 ? 0.00 :
                    Convert.ToDouble(tbDesignated.Text.Trim());

                recs = tCtrl.UpdateContribution(mSeqNo, general,
                            building, missions, designated, tbComment.Text.Trim(),
                            dteRunDate.Value, Convert.ToInt32(BatchNo.Value));

                System.Windows.Forms.Application.DoEvents();
                switch (tabControl1.SelectedIndex)
                {
                    case 0:     //Summary
                        sumPage.UpdateSummaryPage(dteRunDate.Value,
                            Convert.ToInt32(BatchNo.Value));
                        break;

                    case 1:     //Detail
                        dtlPage.FillDetailPage(dteRunDate.Value,
                                Convert.ToInt32(BatchNo.Value));
                        break;

                    case 2:     //People
                        pplPage.FillPeoplePage(dteRunDate.Value, "'Y'");
                        break;

                    case 3:     //Reconcile Checks;
                        break;

                    case 4:     //Counts
                        ///countPage.CountSetYears();
                        break;

                    default:
                        break;
                }

                System.Windows.Forms.Application.DoEvents();

                // Reset the Tag property of the tab pages to show which
                // need updating.
                for (int idx = 0; idx < tabControl1.TabCount; idx++)
                {
                    if (idx != tabControl1.SelectedIndex)
                    {
                        tabControl1.TabPages[idx].Tag = "U";
                    }
                    else
                    {
                        tabControl1.TabPages[idx].Tag = "";
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zUpdateContribution Failed ", ex);
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "zAddContribution failed. Msg: " + ex.Message);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zUpdateContribution"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEnvNo_Leave(object sender, EventArgs e)
        {
            String env = "";
            String name = "";
            DataTable contributor = null;
            DataRow row = null;

            Trace.Enter(Trace.RtnName(mModName, "tbEnvNo_Leave"));

            try
            {
                ////if (sender == cmdExit)
                ////{
                ////    Trace.WriteLine(Trace.TraceLevels.Debug, "Leaving because of Exit button");
                ////}

                //---------------------------------------------------------------------------
                // Check the "Envelope" field, did the user specify one?
                env = tbEnvNo.Text.Trim();
                if (env == "0")
                {
                    env = "";
                }
                if (env.Length == 0)
                {
                    // NO envelope not specified, need to get the name...
                    mInPeopleCBUpdate = true;
                    mSelPeople = tCtrl.GetPeopleSelection();
                    name = mSelPeople.Rows[0][0].ToString();
                    tbName.Visible = false;
                    cbNames.Text = "";
                    cbNames.DataSource = mSelPeople;
                    cbNames.DisplayMember = "Names";
                    cbNames.ValueMember = "pKey";
                    cbNames.SelectedIndex = -1;
                    cbNames.Visible = true;
                    cbNames.Text = "";
                    mNameSoFar = "";
                    cbNames.Focus();
                    mInPeopleCBUpdate = false;
                } //if

                //---------------------------------------------------------------------------
                // YES, the user has entered an envelope number....
                else
                {
                    //-------------------------------------------------------------------------
                    // Try to find the envelope number
                    contributor = tCtrl.GetContributorWithEnvNo(env);

                    //-------------------------------------------------------------------------
                    // Did we find the envelope number?
                    if (contributor.Rows.Count == 0)
                    {                     //NOT FOUND
                        toolStripStatusLabel1.Text = "** " + env + " NOT FOUND **";
                        Console.Beep();
                        tbEnvNo.Focus();
                        lblKey.Text = "";
                    } //if

                    //-------------------------------------------------------------------------
                    // YES it was found...
                    else
                    {
                        toolStripStatusLabel1.Text = "";
                        row = contributor.Rows[0];
                        name = row["LastName"].ToString() + " " +                   // row.ItemArray[lastNameCol].ToString() + " " +
                                row["FirstName"].ToString() + " - " +               // row.ItemArray[firstNameCol].ToString() + " - " +
                                row["pplPhone1"].ToString() + " : " +               // row.ItemArray[phone1Col].ToString() + " : " +
                                row["pplKey"].ToString();                           // row.ItemArray[keyCol].ToString();
                        mPplKey = Convert.ToInt32(row["pplKey"].ToString());
                        lblKey.Text = mPplKey.ToString();
                        tbName.Visible = true;
                        cbNames.Visible = false;
                        tbName.Text = name;
                        tbGeneral.Focus();
                    } //else
                } //else
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("tbEnvNo_Leave Failed ", ex);
                EZex.Add("env", env);
                EZex.Add("name", name);
                EZex.Add("contributor", contributor);
                EZex.Add("row", row);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tbEnvNo_Leave"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_BindingContextChanged(object sender, EventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_BindingContextChanged " + cbNames.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_DataSourceChanged(object sender, EventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_DataSourceChanged " + cbNames.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_DropDown(object sender, EventArgs e)
        {
            zSetNamesText();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_Enter(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cbNames_Enter"));

            try
            {
                cbNames.DroppedDown = true;
                zSetNamesText();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("cbNames_Enter Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cbNames_Enter"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_MouseClick(object sender, MouseEventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_MouseClick " + cbNames.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string line = "";
            string key = "";
            string env = "";
            string[] flds = null;

            Trace.Enter(Trace.RtnName(mModName, "cbNames_SelectedIndexChanged"));

            try
            {
                if (!mInPeopleCBUpdate)
                {
                    Trace.WriteLine(Trace.TraceLevels.Debug, "At start contents: " + cbNames.Text);

                    line = cbNames.Text;
                    if (line.Length > 0)
                    {
                        Trace.WriteLine(Trace.TraceLevels.Debug, "Line: " + line);
                        flds = line.Split(':');
                        if (flds.Length == 2)
                        {
                            key = flds[flds.Length - 1];
                            mPplKey = Convert.ToInt32(key);
                            lblKey.Text = mPplKey.ToString();

                            env = tCtrl.GetEnvelopeFromKey(key);
                            if (env == "0")
                            {
                                env = "";
                            }
                            tbEnvNo.Text = env;

                            Trace.WriteLine(Trace.TraceLevels.Debug, "Setting mPplKey(" + mPplKey.ToString() +
                                        ") abd the envelope number(" + tbEnvNo.Text + ")");
                            tbGeneral.Focus();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("cbNames_SelectedIndexChanged Failed ", ex);
                EZex.Add("line", line);
                EZex.Add("key", key);
                EZex.Add("flds", flds);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cbNames_SelectedIndexChanged"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_SelectedValueChanged(object sender, EventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_SelectedValueChanged |" + cbNames.Text + "|");

            if (mInPeopleCBUpdate == true)
            {
                zSetNamesText();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_SelectionChangeCommitted " + cbNames.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_TextUpdate(object sender, EventArgs e)
        {
            string max = "";
            bool testing = false;

            Trace.Enter(Trace.RtnName(mModName, "cbNames_TextUpdate"));

            try
            {
                if (mInPeopleCBUpdate == false)
                {
                    mInPeopleCBUpdate = true;
                    mNameSoFar = cbNames.Text;
                    if (testing)
                    {
                        Trace.WriteLine(Trace.TraceLevels.Debug, "nameSoFar='" + mNameSoFar + "'");
                    }
                    if (mNameSoFar.Length > 0)
                    {
                        max = zBuildMaxKey();
                        string selString = "Names >='" + mNameSoFar + "' AND Names < '" + max + "'";
                        string orderString = "Names ASC";
                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "selString = |" + selString + "|");
                            Trace.WriteLine(Trace.TraceLevels.Debug, "orderString = |" + orderString + "|");
                        }
                        cbNames.Enabled = false;
                        DataRow[] rows = mSelPeople.Select(selString, orderString);
                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "After filter found: " + rows.Length.ToString() + " rows");
                        }
                        DataTable newTable = mSelPeople.Clone();
                        for (int idx = 0; idx < rows.Length; idx++)
                        {
                            DataRow dr = newTable.NewRow();
                            dr[0] = rows[idx][0];
                            dr[1] = rows[idx][1];
                            newTable.Rows.Add(dr);
                        }
                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "Assign new data source");
                        }
                        cbNames.DataSource = newTable;

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "Reset the Display member and value member");
                        }
                        cbNames.DisplayMember = "Names";
                        cbNames.ValueMember = "pKey";

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "Set the index to -1");
                        }
                        cbNames.SelectedIndex = -1;

                        zSetNamesText();

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "Finally set other properties, DroppedDown, Enabled, Focus");
                            Trace.WriteLine(Trace.TraceLevels.Debug, "1: |" + cbNames.Text +
                                "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        cbNames.DroppedDown = true;

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "2: |" + cbNames.Text +
                                "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        zSetNamesText();

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "3: |" + cbNames.Text +
                                "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        cbNames.Enabled = true;

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "4: |" + cbNames.Text +
                                "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        cbNames.Focus();

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "5: |" + cbNames.Text +
                                 "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                 "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        zSetNamesText();

                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "6: |" + cbNames.Text +
                                 "|" + mNameSoFar + "|selStart: " + cbNames.SelectionStart.ToString() +
                                 "|selLen: " + cbNames.SelectionLength.ToString());
                        }
                        mInPeopleCBUpdate = false;
                    }
                    else
                    {
                        if (testing)
                        {
                            Trace.WriteLine(Trace.TraceLevels.Debug, "Resetting list to mSelPeople");
                        }
                        cbNames.DataSource = mSelPeople;
                        mInPeopleCBUpdate = false;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("cbNames_TextUpdate Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cbNames_TextUpdate"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbNames_Validating(object sender, CancelEventArgs e)
        {
            Trace.WriteLine(Trace.TraceLevels.Debug, "cbNames_Validating " + cbNames.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetNamesText()
        {
            Trace.Enter(Trace.RtnName(mModName, "zSetNamesText"));

            try
            {
                Trace.WriteLine(Trace.TraceLevels.Debug, "Reset the text to what we have so far nothing selected");
                cbNames.Text = mNameSoFar;
                cbNames.SelectionLength = 0;
                cbNames.SelectionStart = mNameSoFar.Length;
                cbNames.Cursor = Cursors.Arrow;
                this.Cursor = Cursors.Arrow;
                System.Windows.Forms.Application.DoEvents();
                Trace.WriteLine(Trace.TraceLevels.Debug, "Set to: |" + cbNames.Text + "|");
            }
            catch (Exception ex)
            {
                EZException EZex = new EZException("zSetNamesText Failed ", ex);
                throw EZex;
            }
            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetNamesText"));
            }
        }

        private string zBuildMaxKey()
        {
            char m = ' ';
            byte b = 0;
            int i = 0;
            string max = "";

            Trace.Enter(Trace.RtnName(mModName, "zBuildMaxKey"));

            try
            {
                max = mNameSoFar.Substring(0, mNameSoFar.Length - 1);
                m = Convert.ToChar(mNameSoFar.Substring(mNameSoFar.Length - 1, 1));
                i = Convert.ToByte(m);
                i = i + 1;
                b = Convert.ToByte(i);
                m = Convert.ToChar(b);
                max = max + Convert.ToString(m);

                return max;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zBuildMaxKey Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zBuildMaxKey"));
            }
        }

        private void tbGeneral_Enter(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "tbGeneral_Enter"));

            try
            {
                mOrigVal = ((TextBox)sender).Text.Trim();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("tbGeneral_Enter Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tbGeneral_Enter"));
            }
        }

        private void tbGeneral_Leave(object sender, EventArgs e)
        {
            int err = 0;

            Trace.Enter(Trace.RtnName(mModName, "tbGeneral_Leave"));

            TextBox tb = (TextBox)sender;
            string sNum = tb.Text.Trim();
            if ((sNum != mOrigVal) && (mModified == 0))
            {
                mModified = 1;
                if (mSeqNo == -1)
                {
                    groupBox1.BackColor = Color.LightCoral;
                }
                else
                {
                    groupBox1.BackColor = Color.PaleGreen;
                }
                toolStripStatusLabel1.Text = "Press 'Enter' to record";
            }

            if (sNum.Length > 0)
            {
                try
                {
                    double num = Convert.ToDouble(sNum);
                }

                catch
                {
                    Console.Beep();
                    tb.Focus();
                    err = 1;
                }
            }

            if (err == 0)
            {
                zFillInTotal();
            }

            Trace.Exit(Trace.RtnName(mModName, "tbGeneral_Leave"));
        }

        private void cmdPrintDetail_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdPrintDetail_Click"));

            try
            {
                mReports.SummaryReport(lblSummaryTitle.Text.Trim(),
                                    lblCashCreditGeneral.Text, lblCashCreditBuilding.Text,
                                    lblCashCreditMissions.Text, lblCashCreditDesignated.Text,
                                    lblCashCreditTotal.Text,
                                    lblCashLooseGeneral.Text, lblCashLooseBuilding.Text,
                                    lblCashLooseMissions.Text, lblCashLooseDesignated.Text,
                                    lblCashLooseTotal.Text,
                                    lblCashNonCreditGeneral.Text, lblCashNonCreditBuilding.Text,
                                    lblCashNonCreditMissions.Text, lblCashNonCreditDesignated.Text,
                                    lblCashNonCreditTotal.Text,
                                    lblCashTotalGeneral.Text, lblCashTotalBuilding.Text,
                                    lblCashTotalMissions.Text, lblCashTotalDesignated.Text,
                                    lblCashTotalTotal.Text,
                                    lblCheckCreditGeneral.Text, lblCheckCreditBuilding.Text,
                                    lblCheckCreditMissions.Text, lblCheckCreditDesignated.Text,
                                    lblCheckCreditTotal.Text,
                                    lblCheckNonCreditGeneral.Text, lblCheckNonCreditBuilding.Text,
                                    lblCheckNonCreditMissions.Text, lblCheckNonCreditDesignated.Text,
                                    lblCheckNonCreditTotal.Text,
                                    lblCheckGeneralTotal.Text, lblCheckBuildingTotal.Text,
                                    lblCheckMissionsTotal.Text, lblCheckDesignatedTotal.Text,
                                    lblCheckTotalTotal.Text,
                                    lblGeneralTotal.Text, lblBuildingTotal.Text,
                                    lblMissionsTotal.Text, lblDesignatedTotal.Text,
                                    lblTotalTotal.Text);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("cmdPrintDetail_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdPrintDetail_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "editToolStripMenuItem1_Click"));

            try
            {
                dtlPage.EditDetailRow();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("editToolStripMenuItem1_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "editToolStripMenuItem1_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "deleteToolStripMenuItem1_Click"));

            try
            {
                dtlPage.DeleteRow(dteRunDate.Value.ToString("yyyy-MM-dd"),
                            BatchNo.Value.ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("deleteToolStripMenuItem1_Click Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "deleteToolStripMenuItem1_Click"));
            }
        }

        /// <summary>
        /// A new year tab has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabStripYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            countPage.YearSelected();
        }

        /// <summary>
        /// A Month tab has been selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabStripMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            countPage.MonthSelected();
        }

        private void grdCounts_Resize(object sender, EventArgs e)
        {
            //tabStripMonths.Width = grdCounts.Width;
        }

        private void grdCounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            countPage.ChangeCount();
        }

    }
}
