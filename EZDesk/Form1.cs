using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;
using EZUtils;
using EZDeskDataLayer;
using MessageCenter;

namespace EZDesk
{
    // This will show that I can Update from outside the house //
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // FORM1:
    //
    // Form1 provides the structure that the rest of the program runs from.  The form is broken 
    // into 3 main sections:
    //  1) Bottom, the "Tabs" or "Folders". This sections shows the folders or tabs that the user
    //     has access to. Documents are dragged to these "Tabs" and then when the "Tab" is clicked
    //     the contents of that folder are displayed in the frmDocuments form in the "Display" 
    //     area.
    //  2) The "Display" area. This is in the top half of the form to the right.  The largest of
    //     the sections of Form1. Various forms, such as frmDocuments are anchored in this section
    //     as the user calls them up.
    //  3) The "Person" area. The top half on the left side is reserved for selecting people. Once
    //     a person is selected the tabs hold documents for that person only! Bringing up other
    //     features/forms will also be limited to that Person. (See the EZDeskCommon class).
    public partial class Form1 : Form
    {
        //This is a very special class. It contains the information about the currently signed
        //on user, the currently selected Person as well as the MySqlConnection that is shared
        //by all.  Eventually this will be the item that is shared with all of the various forms.
        private EZDeskCommon mCommon = null;

        //Forms that are brought up.
        private iDesk frmIDesk = null;
        private int frmIDeskIdx = -1;
        private string frmIDeskCtrlName = "";

        //private frmCalendar fCalendar = null;

        private Calendar2 fCalendar2 = null;
        private int frmCalendar2Idx = -1;
        private string frmCalendarCtrlName = "";
        
        private frmMessageCenter fMsgCenter = null;
        private int frmMsgCenterIdx = -1;
        private string frmMsgCenterCtrlName = "";

        private frmForms fForms = null;
        private int frmFormsIdx = -1;
        private string frmFormsCtrlName = "";

        private EZTeller.Form1 frmEZTeller = null;
        private int frmEZTellerIdx = -1;
        private string frmEZTellerCtrlName = "";

        private frmPerson frmPer = null;
        private frmExternalProgram pfrm = null;
        private frmDocuments frmDocs = null;
        private frmAuditLog frmAuditReport = null;
        private frmFilePlace fFilePlace = null;

        private int mSplitterDistance = 0;
        
        private DragEventArgs mDragArgs = null;
        private int mDragDropTagId = -1;
        private object mDragSender = null;
        private Button mDragTab = null;
        private string mDragName = "";

        private string mModName = "Form1";

        public event EventHandler OnPersonSelected;
        public delegate void EventHandler(EZDeskDataLayer.Person.Models.PersonSelectedArguments e);

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "Form1_Load"));
            int open = 1;
            try
            {
                mCommon = new EZDeskCommon();
                mCommon.ConnStr = zSetConnectionString();
                mCommon.Connection = new MySqlConnection(mCommon.ConnStr);
                try
                {
                    mCommon.Connection.Open();
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    MessageBox.Show("Failed to open database.\nMsg: " + msg + "\nConnStr: " + mCommon.ConnStr,
                                        "Init Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    open = 0;
                }

                if (open == 1)
                {
                    mCommon.SetControllers();

                    iDeskLoad();
                    mSplitterDistance = splitContainer2.SplitterDistance;

                    zPersonPanelLoad();

                    lblPerson.Text = "";
                    zSetTabs(0);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Form1_Load failed", ex);
                Trace.WriteEventEntry(Trace.RtnName(mModName, "Form1_Load"), ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Load"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetMenuSystem()
        {
            auditLogToolStripMenuItem.Visible = 1 == Convert.ToInt32(mCommon.eCtrl.GetProfileValue("Reports", "Audit Log", mCommon.User.UserSecurityID));
            settingsToolStripMenuItem.Visible = 1 == Convert.ToInt32(mCommon.eCtrl.GetProfileValue("Security", "Menu Settings", mCommon.User.UserSecurityID));
        }

        /// <summary>
        /// After the main form has been shown, require the user
        /// to login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            zFormShown();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFormShown()
        {
            Trace.Enter(Trace.RtnName(mModName, "Form1_Shown"));
            try
            {
                frmPassword frm = new frmPassword(mCommon, this.Left, this.Top, this.Width, this.Height);
                //frm.Left = this.Left + ((this.Width - frm.Width) / 2);
                //frm.Top = this.Top + ((this.Height - frm.Height) / 2);
                DialogResult dr = frm.ShowDialog();
                switch (dr)
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        Application.Exit();
                        this.Close();
                        break;

                    case System.Windows.Forms.DialogResult.OK:
                        mCommon.Staff = mCommon.pCtrl.GetPersonByID(mCommon.User.PersonID);
                        this.Text = "EZDesk - " + mCommon.User.UserName;
                        break;

                    default:
                        Application.Exit();
                        this.Close();
                        break;
                }

                if (mCommon.User != null)
                {
                    zSetTabs(mCommon.User.UserSecurityID);
                    zEnableDisableUserItems(true);
                    zSetMenuSystem();
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Form1_Shown failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Form1_Shown"));
            }
        }

        /// <summary>
        /// Setup the connection string
        /// </summary>
        private string zSetConnectionString()
        {
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "zSetConnectionString"));

            try
            {
                rtn = Properties.Settings.Default.connStr;
                rtn = rtn.Replace("{server}", Properties.Settings.Default.server.Trim());
                rtn = rtn.Replace("{db}", Properties.Settings.Default.db.Trim());
                rtn = rtn.Replace("{uid}", Properties.Settings.Default.uid.Trim());
                rtn = rtn.Replace("{password}", Properties.Settings.Default.password.Trim());

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSetConnectionString failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetConnectionString"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mCommon.User != null)
            {
                EZDeskDataLayer.ehr.Models.AuditItem item =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                                EZDeskDataLayer.ehr.Models.AuditAreas.User,
                                EZDeskDataLayer.ehr.Models.AuditActivities.Logout,
                                "");
                mCommon.eCtrl.WriteAuditRecord(item);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // This section deals with the buttons that represent folders! When a user has signed in the
        // buttons (tabs) are set by the drawers that the user has been assigned. Displays in the Form1 
        // context.
        #region Tab Buttons

        /// <summary>
        /// At startup and on returning from Setup... rebuild the
        /// list of "Tabs" as buttons in the flowLayoutPanel.
        /// </summary>
        private void zSetTabs(int userId)
        {
            Trace.Enter(Trace.RtnName(mModName, "zSetTabs"));

            try
            {
                flowLayoutPanel1.Controls.Clear();
                DataTable tabs = mCommon.uCtrl.GetAllTabsForUser(userId);
                foreach (DataRow dr in tabs.Rows)
                {
                    Button btn = new Button();
                    btn.Text = dr["tabName"].ToString();
                    btn.Tag = dr["tabId"].ToString();
                    btn.BackColor = ((bool)dr["IsActive"]) ?
                        SystemColors.Control : Color.Red;
                    btn.AllowDrop = (bool)dr["IsActive"];
                    btn.Click += new System.EventHandler(this.tabButton_Click);
                    btn.DragDrop += new System.Windows.Forms.DragEventHandler(btnDropTarget_DragDrop);
                    btn.DragOver += new System.Windows.Forms.DragEventHandler(btnDropTarget_DragOver);
                    flowLayoutPanel1.Controls.Add(btn);
                    btn.Enabled = false;
                    
                    if ((mCommon != null) && (mCommon.Person != null))
                    {
                        btn.Enabled = true;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zSetTabs failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zSetTabs"));
            }
        }

        /// <summary>
        /// All "Tab Buttons" in the flowLayoutPanel will get here when
        /// the user clicks on a button indicating they want to open
        /// the "Tab Folder".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabButton_Click(object sender, EventArgs e)
        {
            int tabId = -1;

            Trace.Enter(Trace.RtnName(mModName, "tabButton_Click"));

            try
            {
                tabId = Convert.ToInt32(((Button)sender).Tag.ToString());
                zFrmDocumentsLoad(tabId);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("tabButton_Click failed", ex);
                eze.Add("tabId", tabId);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "tabButton_Click failed");
                frm.Show();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tabButton_Click"));
            }
        }

        /// <summary>
        /// The user is dragging-Dropping a package of data and have just
        /// entered our button target.  The type of data in the package 
        /// must be either a List of strings (fullpath file names -- from the
        /// ListView in this application) OR a FileDrop from a System Directory
        /// drag-and-drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDropTarget_DragOver(object sender, DragEventArgs e)
        {
            // Value        Key
            // 1 (bit 0)    The left mouse button.
            // 2 (bit 1)    The right mouse button.
            // 4 (bit 2)    The SHIFT key.
            // 8 (bit 3)    The CTRL key.
            // 16 (bit 4)   The middle mouse button.
            // 32 (bit 5)   The ALT key.
            const int SHIFT = 4;
            const int CTRL = 8;
            const int LEFT = 1;
            const int RIGHT = 2;
            const int ALT = 32;

            Trace.Enter(Trace.RtnName(mModName, "btnDropTarget_DragOver"));

            try
            {
                // x Right mouse Menu Copy/Move/Cancel
                // x Shift-Right mouse Menu Copy/Move/Cancel
                // x Shift-Left mouse MOVE
                // x Left mouse COPY
                // x Ctrl-Left mouse Copy

                //Must be of type List<string> (from our ListView) or
                // FileDrop (from a System Directory).
                if ((e.Data.GetDataPresent(typeof(List<string>))) ||
                    (e.Data.GetDataPresent(DataFormats.FileDrop)))
                {
                    // Shift-Left mouse button is a MOVE
                    if (((e.KeyState & SHIFT) == SHIFT) &&
                        ((e.KeyState & LEFT) == LEFT) &&
                        ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move))
                    {
                        e.Effect = DragDropEffects.Move;
                    }

                    else
                        // No Shift Left mouse button is a COPY
                        if (((e.KeyState & LEFT) == LEFT) &&
                            ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy))
                        {
                            e.Effect = DragDropEffects.Copy;
                        }

                        else
                            // ANY Shift/Control/No Shift/No Control Right mouse is a menu
                            // if ALL, otherwise whatever it was set to when the Drag 
                            // started.
                            if ((e.KeyState & RIGHT) == RIGHT)
                            {
                                e.Effect = e.AllowedEffect;     //Menu to follow
                            }
                            else
                                //Everything else is NOT ALLOWED.
                                e.Effect = DragDropEffects.None;
                }

                //The data type was wrong so NOT ALLOWED
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("btnDropTarget_DragOver failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "btnDropTarget_DragOver"));
            }
        }

        /// <summary>
        /// The user has dropped a drag-and-drop package on the button. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDropTarget_DragDrop(object sender, DragEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "btnDropTarget_DragDrop"));

            try
            {
                mDragDropTagId = Convert.ToInt32(((Button)sender).Tag.ToString());
                mDragTab = (Button)sender;

                //Get the option, if coming from FilePlace. We are going to determine that
                // we are coming from FilePlace if the drag datatype is a List<string> AND
                // if the FilePlace form has been initialized. (Can we also determine if 
                // the FilePlace is also "active"/"Visible"/"Top").
                mDragName = "PCFileName";
                if (e.Data.GetDataPresent(typeof(List<string>)))
                {
                    if ((fFilePlace != null) && (pnlHosting.Controls.GetChildIndex(fFilePlace) == 0))
                    {
                        mDragName = fFilePlace.NameOption;
                    }
                }

                switch (e.Effect)
                {
                    case DragDropEffects.Move:
                        zMoveCopyFiles(e, "move");
                        break;
                    case DragDropEffects.Copy:
                        zMoveCopyFiles(e, "copy");
                        break;
                    default:
                        //case DragDropEffects.Link:
                        //case DragDropEffects.Scroll:
                        //case DragDropEffects.None;
                        if (((e.Effect & DragDropEffects.Copy) == DragDropEffects.Copy) &&
                            ((e.Effect & DragDropEffects.Move) == DragDropEffects.Move))
                        {
                            mDragArgs = e;
                            contextMenuTabImport.Show();
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("btnDropTarget_DragDrop failed", ex);
                eze.Add("mDragDropTagId", mDragDropTagId);
                eze.Add("mDragTab", mDragTab);
                eze.Add("mDragName", mDragName);

                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Drag-Drop failed");
                frm.Show();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "btnDropTarget_dragDrop"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dragType"></param>
        private void zMoveCopyFiles(DragEventArgs e, string dragType)
        {
            List<string> selections = new List<string>();
            string[] files = null;
            bool cont = true;
            string name = "";
            FileInfo fi = null;
            EZDeskDataLayer.Documents.DocumentsController dCtrl =
                    new EZDeskDataLayer.Documents.DocumentsController(mCommon);

            Trace.Enter(Trace.RtnName(mModName, "zMoveCopyFiles"));

            try
            {
                if (dragType.Length > 0)
                {
                    //If the user was dragging strings... they need to be full path file names.
                    if (e.Data.GetDataPresent(typeof(List<string>)))
                    {
                        selections = e.Data.GetData(typeof(List<string>)) as List<string>;
                        files = selections.ToArray();
                    }

                    //We are also going to handle dragging and dropping files from a 
                    // System directory listing.
                    else
                    {
                        if (e.Data.GetDataPresent(DataFormats.FileDrop))
                        {
                            files = (string[])e.Data.GetData(DataFormats.FileDrop);
                        }

                        else
                        {
                            //This should never happen, the drag-n-drop was verified in the
                            // DragOver method and would have been set to NONE if it wasn't
                            // a List of string or a FileDrop.  But just in case, issue a
                            // message box here.
                            MessageBox.Show("Incorrect data drag-and-drop data format", "Drag-Drop Error");
                            cont = false;
                        }
                    }

                    if (cont)
                    {
                        //Check each file that is being dropped.
                        foreach (string file in files)
                        {
                            if (mDragName.ToLower() == "pcfilename")
                            {
                                fi = new FileInfo(file);
                                name = fi.Name;
                                name = name.Substring(0, name.Length - fi.Extension.Length);
                            }
                            else
                            {
                                name = mDragTab.Text;
                            }

                            EZDeskDataLayer.Documents.Models.documentDetail doc =
                                    dCtrl.AddFile(file, name, dragType, mDragDropTagId, mCommon);
                            if (doc.DocumentError.Length == 0)
                            {
                                EZDeskDataLayer.ehr.Models.AuditItem item =
                                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, mCommon.Person.PersonID,
                                        EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                                        EZDeskDataLayer.ehr.Models.AuditActivities.Add,
                                        "Add document: " + file);
                                mCommon.eCtrl.WriteAuditRecord(item);
                            }

                            else
                            {
                                MessageBox.Show("Failed to copy/move the file.\n" + doc.DocumentError, "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zMoveCopyFiles failed", ex);
                eze.Add("files", files);
                eze.Add("dragType", dragType);
                eze.Add("name", name);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zMoveCopyFiles"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "copyToolStripMenuItem_Click"));

            try
            {
                zMoveCopyFiles(mDragArgs, "copy");
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("copyToolStripMenuItem_Click failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "copyToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "moveToolStripMenuItem_Click"));

            try
            {
                zMoveCopyFiles(mDragArgs, "move");
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("moveToolStripMenuItem_Click failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Enter(Trace.RtnName(mModName, "moveToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cancelToolStripMenuItem_Click"));

            //TODO: Cancel -- maybe nothing?

            Trace.Exit(Trace.RtnName(mModName, "cancelToolStripMenuItem_Click"));
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // This section deals with the PersonPanel. Displays in the Form1 context.
        #region PersonPanel

        /// <summary>
        /// Load the Person Panel
        /// </summary>
        private void zPersonPanelLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "zPersonPanelLoad"));

            try
            {
                if (frmPer == null)
                {
                    frmPer = new frmPerson(mCommon);
                    frmPer.TopLevel = false;
                    frmPer.Visible = true;
                    frmPer.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    frmPer.ControlBox = false;
                    frmPer.Text = String.Empty;

                    frmPer.Width = pnlPeople.Width;
                    frmPer.Height = pnlPeople.Height;
                    frmPer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    frmPer.PersonSelected += new frmPerson.EventHandler(PersonSelected); 
                    pnlPeople.Controls.Add(frmPer);
                }

                //pnlPeople.Controls["frmPer"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zPersonPanelLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zPersonPanelLoad"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonSelected(object sender, EZDeskDataLayer.Person.Models.PersonSelectedArguments e)
        {
            int pId = -1;
            string temp = "";

            Trace.Enter(Trace.RtnName(mModName, "PersonSelected"));

            try
            {
                pId = e.PersonId;
                mCommon.Person = mCommon.pCtrl.GetPersonByID(pId);
                temp = (mCommon.Person.LastName != null) ? mCommon.Person.LastName : "";
                if (mCommon.Person.FirstName.Length > 0)
                {
                    temp += (mCommon.Person.FirstName != null) ? ", " + mCommon.Person.FirstName.Trim() : "";
                }
                temp += (mCommon.Person.MiddleName != null) ? " " + mCommon.Person.MiddleName : "";
                lblPerson.Text = temp;
                if (frmIDesk != null)
                {
                    frmIDesk.PersonId = mCommon.Person.PersonID;
                }

                // Audit the access - Done here instead of iDesk as the
                // userid is available here.
                EZDeskDataLayer.ehr.Models.AuditItem audit =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID,
                            mCommon.Person.PersonID,
                            EZDeskDataLayer.ehr.Models.AuditAreas.Person, 
                            EZDeskDataLayer.ehr.Models.AuditActivities.View, 
                            "Person selected on Form1");
                mCommon.eCtrl.WriteAuditRecord(audit);

                //TODO: How indicate tabs that have data.

                zEnableDisablePersonItems(true);
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    ((Button)c).Enabled = true;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("PersonSelected failed", ex);
                eze.Add("pId", pId);
                eze.Add("temp", temp);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "PersonSelected"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblPerson_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AddEditPerson(mCommon.Person.PersonID, (int)mCommon.Person.PersonType);
            //frmIDesk.PersonId = mCommon.Person.PersonID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonID"></param>
        /// <param name="PersonType"></param>
        public void AddEditPerson(int PersonID, int PersonType)
        {
            zAddEditPerson(PersonID, PersonID);
            frmIDesk.PersonId = PersonID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void personToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zAddEditPerson(-1, 1);
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Routines dealing with the Documents form.  This form comes up with the user click on one of
        // the Tab buttons. Displays in the Form1 context.
        #region frmDocuments 

        /// <summary>
        /// Load the iDesk form into the hosting panel
        /// </summary>
        private void zFrmDocumentsLoad(int tabId)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFrmDocumentsLoad"));

            try
            {
                if (frmDocs == null)
                {
                    frmDocs = new frmDocuments(mCommon);
                    frmDocs.TopLevel = false;
                    frmDocs.Visible = true;
                    frmDocs.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    frmDocs.ControlBox = false;

                    frmDocs.Width = pnlHosting.Width;
                    frmDocs.Height = pnlHosting.Height;
                    frmDocs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmDocs);
                }

                splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls["frmDocuments"].BringToFront();
                frmDocs.TabId = tabId;
                frmDocs.PersonId = mCommon.Person.PersonID;

                EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, mCommon.Person.PersonID,
                            EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "Open Tab: " + tabId.ToString());
                mCommon.eCtrl.WriteAuditRecord(item);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zFrmDocumentsLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFrmDocumentsLoad"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // This section deals with the iDesk or Main Desktop form. Displays in the Form1 context.
        #region iDesk

        /// <summary>
        /// Load the iDesk form into the hosting panel
        /// </summary>
        private void iDeskLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "iDeskLoad"));

            try
            {
                if (frmIDesk == null)
                {
                    frmIDesk = new iDesk(mCommon);
                    frmIDesk.TopLevel = false;
                    frmIDesk.Visible = true;
                    frmIDesk.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    frmIDesk.ControlBox = false;

                    frmIDesk.Width = pnlHosting.Width;
                    frmIDesk.Height = pnlHosting.Height;
                    frmIDesk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmIDesk);
                    frmIDeskIdx = pnlHosting.Controls.Count + -1;
                    frmIDeskCtrlName = pnlHosting.Controls[frmIDeskIdx].Name;
                }

                splitContainer2.Panel1Collapsed = false;
                string name = pnlHosting.Controls[frmIDeskIdx].Name;
                pnlHosting.Controls[frmIDeskCtrlName].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("iDeskLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "iDeskLoad"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDesktop_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "cmdDesktop_Click"));

            try
            {
                iDeskLoad();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("cmdDesktop_Click failed", ex);
                MessageBox.Show(ex.ToString(), ex.Message);
                Trace.WriteEventEntry("EZDesk", ex);
                throw eze;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            iDeskLoad();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Section deals with the settings form that displays modal
        #region Settings

        /// <summary>
        /// The user has clicked on the main menu --> Settings option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = -1;
            int PersonId = -1;
            EZDeskDataLayer.User.Models.UserDetails user = null;
            EZDeskDataLayer.Person.Models.PersonFormGetDemographics person = null;

            Trace.Enter(Trace.RtnName(mModName, "settingsToolStripMenuItem_Click"));

            try
            {
                if (1 == Convert.ToInt32(mCommon.eCtrl.GetProfileValue("Security", "Menu Settings", mCommon.User.UserSecurityID)))
                {
                    frmSetup frm = new frmSetup(mCommon, this.Left, this.Top, this.Width, this.Height);
                    DialogResult dr = frm.ShowDialog();

                    //Refresh the User/Person data for the person signed in, in case
                    //it was changed while in the settings routine.
                    UserId = mCommon.User.UserSecurityID;
                    PersonId = mCommon.User.PersonID;
                    user = mCommon.uCtrl.GetUserDetails(PersonId);
                    person = mCommon.pCtrl.GetPersonByID(PersonId);

                    mCommon.User = user;
                    mCommon.Staff = person;
                    this.Text = "EZDesk - " + mCommon.User.UserName;

                    //Reset the tabs in case the drawers or tabs were changed for
                    //the signed on user.
                    zSetTabs(mCommon.User.UserSecurityID);
                    zSetMenuSystem();
                }
                else
                {
                    MessageBox.Show("You don't have access to Settings", "Access", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("settingsToolStripMenuItem_Click failed", ex);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "settingsToolStripMenuItem_Click failed");
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "settingsToolStripMenuItem_Click"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Brings up the frmForms dialog. Displays in the Form1 context.
        #region Forms

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdForms_Click(object sender, EventArgs e)
        {
            zForms();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zForms()
        {
            string formFileName = "";
            string calcFileName = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "cmdForms_Click"));

            try
            {
                if (fForms == null)
                {
                    fForms = new frmForms(mCommon); //Calendar2(mCommon);
                    fForms.TopLevel = false;
                    fForms.Text = "";
                    fForms.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    fForms.WindowState = FormWindowState.Normal;
                    fForms.Visible = true;
                    fForms.MinimizeBox = false;
                    fForms.MaximizeBox = false;
                    fForms.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    fForms.ControlBox = false;
                    fForms.ShowIcon = false;
                    fForms.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
                    fForms.Dock = DockStyle.Fill;
                    fForms.Width = pnlHosting.Width;
                    fForms.Height = pnlHosting.Height;
                    fForms.ShowInTaskbar = false;
                    fForms.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(fForms);
                    frmFormsIdx = pnlHosting.Controls.Count + -1;
                    frmFormsCtrlName = pnlHosting.Controls[frmFormsIdx].Name;
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls[frmFormsCtrlName].BringToFront();

                //step = "Build and call Forms dialog";
                //frmForms frm = new frmForms(mCommon);
                //DialogResult dr = frm.ShowDialog();
                //if (dr == DialogResult.OK)
                //{
                //    step = "Get the FormFileName from the Forms dialog";
                //    formFileName = frm.FormFileName;
                //}
                //frm.Dispose();

                //if (formFileName.Length > 0)
                //{
                //    step = "Get the OpenOffice calc program file name";
                //    calcFileName = mCommon.eCtrl.GetProperty("FormsProgram");
                //    if (calcFileName.Length > 0)
                //    {
                //        if (pfrm == null)
                //        {
                //            step = "Bring up the form panel";
                //            pfrm = new frmExternalProgram();

                //            pfrm.TopLevel = false;
                //            pfrm.Visible = true;
                //            pfrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                //            pfrm.ControlBox = false;
                //            pfrm.Width = pnlHosting.Width;
                //            pfrm.Height = pnlHosting.Height;
                //            pfrm.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                //            pnlHosting.Controls.Add(pfrm);
                //        }
                //        pnlHosting.Controls["frmExternalProgram"].BringToFront();

                //        step = "Tell frmExternalProgram to run the calc program for the selected form.";
                //        pfrm.StartExternal(calcFileName, formFileName);
                //    }
                //    else
                //    {
                //        MessageBox.Show("You must initialize the 'FormsProgram' property", "Configuration Error");
                //    }
                //}
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("cmdForms_Click failed", ex);
                eze.Add("step", step);
                eze.Add("formFileName", formFileName);
                eze.Add("calcFileName", calcFileName);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Forms failed");
                DialogResult = frm.ShowDialog();
                Trace.WriteEventEntry("EZDesk", ex);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "cmdForms_Click"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Brings up the ToDo modal form.
        #region ToDo

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbToDo_Click(object sender, EventArgs e)
        {
            zDisplayToDo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zDisplayToDo()
        {
            Trace.Enter(Trace.RtnName(mModName, "tsbToDo_Click"));

            try
            {
                Rectangle r = splitContainer2.Panel2.ClientRectangle;
                ToDo.frmToDo frm = new ToDo.frmToDo(mCommon, mCommon.User.UserSecurityID,
                                            splitContainer2.Left + splitContainer2.SplitterDistance + this.Left,
                                            splitContainer2.Top + this.Top + 50,
                                            r.Width,
                                            r.Height);
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("To Do List failed to open\n" +
                                ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "tsbToDo_Click"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // Brings up the frmFilePlace form. Displays in the Form1 context. This list files in the 
        // import directory that are waiting to be imported to person folders (tabs).
        #region FilePlace

        /// <summary>
        /// 
        /// </summary>
        private void FilePlaceLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "FilePlaceLoad"));

            try
            {
                if (fFilePlace == null)
                {
                    fFilePlace = new frmFilePlace(mCommon);
                    fFilePlace.TopLevel = false;
                    fFilePlace.Visible = true;
                    fFilePlace.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    fFilePlace.ControlBox = false;
                    fFilePlace.ShowIcon = false;

                    fFilePlace.Width = pnlHosting.Width;
                    fFilePlace.Height = pnlHosting.Height;
                    fFilePlace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(fFilePlace);
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls["frmFilePlace"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("FilePlaceLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "FilePlaceLoad"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdScanPlace_Click(object sender, EventArgs e)
        {
            zFilePlace();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFilePlace()
        {
            Trace.Enter(Trace.RtnName(mModName, "zFilePlace"));

            try
            {
                FilePlaceLoad();
                fFilePlace.Init();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("File Place failed", ex);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "File Place failed");
                DialogResult dr = frm.ShowDialog();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFilePlace"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // This section deals with auditing and the frmAuditLog form. Displays in the Form1 context.
        #region Audit

        /// <summary>
        /// 
        /// </summary>
        private void AuditReportLoad()
        {
            Trace.Enter(Trace.RtnName(mModName, "AuditReportLoad"));

            try
            {
                if (frmAuditReport == null)
                {
                    frmAuditReport = new frmAuditLog(mCommon);
                    frmAuditReport.TopLevel = false;
                    frmAuditReport.Visible = true;
                    frmAuditReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    frmAuditReport.ControlBox = false;
                    frmAuditReport.ShowIcon = false;

                    frmAuditReport.Width = pnlHosting.Width;
                    frmAuditReport.Height = pnlHosting.Height;
                    frmAuditReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmAuditReport);
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls["frmAuditLog"].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("AuditReportLoad failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AuditReportLoad"));
            }
        }

        /// <summary>
        /// Time to show an audit log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void auditLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "auditLogToolStripMenuItem"));
            
            try
            {
                if (1 == Convert.ToInt32(mCommon.eCtrl.GetProfileValue("Reports", "Audit Log", mCommon.User.UserSecurityID)))
                {
                    AuditReportLoad();
                }
                else
                {
                    MessageBox.Show("You need access rights to run the Audit Log", "Access Rights", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Audit log failed", ex);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Audit Log Report Failed");
                DialogResult dr = frm.ShowDialog();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "auditLogToolStripMenuItem"));
            }
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // This deals with the Calendar system. the frmCalendar form is displays in the Form1 context.
        #region Calendar

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalTest_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModName, "btnCalTest_Click"));

            try
            {
                zLoadCalendar2();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("btnCalTest_Click failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "btnCalTest_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zLoadCalendar2()
        {
            Trace.Enter(Trace.RtnName(mModName, "zLoadCalendar2"));

            try
            {
                if (fCalendar2 == null)
                {
                    fCalendar2 = new Calendar2(mCommon);
                    fCalendar2.TopLevel = false;
                    fCalendar2.Text = "";
                    fCalendar2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    fCalendar2.WindowState = FormWindowState.Normal;
                    fCalendar2.Visible = true;
                    fCalendar2.MinimizeBox = false;
                    fCalendar2.MaximizeBox = false;
                    fCalendar2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    fCalendar2.ControlBox = false;
                    fCalendar2.ShowIcon = false;
                    fCalendar2.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
                    fCalendar2.Dock = DockStyle.Fill;
                    fCalendar2.Width = pnlHosting.Width;
                    fCalendar2.Height = pnlHosting.Height;
                    fCalendar2.ShowInTaskbar = false;
                    fCalendar2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(fCalendar2);
                    frmCalendar2Idx = pnlHosting.Controls.Count + -1;
                    frmCalendarCtrlName = pnlHosting.Controls[frmCalendar2Idx].Name;
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls[frmCalendarCtrlName].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zLoadCalendar2 failed", ex);
                EZUtils.EventLog.WriteErrorEntry(eze);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Failure in the calendar system.");
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zLoadCalendar2"));
            }
        }

        /// <summary>
        /// The main menu View->Calendar has been pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zLoadCalendar2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            zLoadCalendar2();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestNewCalendar frm = new frmTestNewCalendar();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCalendar_Click(object sender, EventArgs e)
        {
            zLoadCalendar2();
        }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////
        // From the menu on Form1... adding People/Users/Companies
        #region AddingPeopleCompanies

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void staffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zAddEditPerson(-1, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zAddEditPerson(-1, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID of the Person/Staff/Company being edited or -1</param>
        /// <param name="persontype">PersonTypeID</param>
        private void zAddEditPerson(int id, int persontype)
        {
            EZDeskDataLayer.User.UserTypes ptype = (EZDeskDataLayer.User.UserTypes)persontype;
            if (ptype == EZDeskDataLayer.User.UserTypes.Undefined)
            {
                ptype = EZDeskDataLayer.User.UserTypes.Patient;
            }
            frmUser frm = new frmUser(mCommon, ptype, id, this.Left, this.Top, this.Width, this.Height);
            frm.ShowDialog();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMessages_Click(object sender, EventArgs e)
        {
            zDisplayMessages();
        }

        private static void zDisplayMessages()
        {
            MessageBox.Show("Not ready at this time.\n\n" +
                    "Messaging may not be implemented.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not ready at this time.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdMsgCtr_Click(object sender, EventArgs e)
        {
            zMsgCenter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanPlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zFilePlace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zForms();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zMsgCenter()
        {
            Trace.Enter(Trace.RtnName(mModName, "zMsgCenter"));

            try
            {
                if (fMsgCenter == null)
                {
                    fMsgCenter = new frmMessageCenter(mCommon); //Calendar2(mCommon);
                    fMsgCenter.TopLevel = false;
                    fMsgCenter.Text = "";
                    fMsgCenter.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    fMsgCenter.WindowState = FormWindowState.Normal;
                    fMsgCenter.Visible = true;
                    fMsgCenter.MinimizeBox = false;
                    fMsgCenter.MaximizeBox = false;
                    fMsgCenter.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    fMsgCenter.ControlBox = false;
                    fMsgCenter.ShowIcon = false;
                    fMsgCenter.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
                    fMsgCenter.Dock = DockStyle.Fill;
                    fMsgCenter.Width = pnlHosting.Width;
                    fMsgCenter.Height = pnlHosting.Height;
                    fMsgCenter.ShowInTaskbar = false;
                    fMsgCenter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(fMsgCenter);
                    frmMsgCenterIdx = pnlHosting.Controls.Count + -1;
                    frmMsgCenterCtrlName = pnlHosting.Controls[frmMsgCenterIdx].Name;
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls[frmMsgCenterCtrlName].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zMsgCenter failed", ex);
                EZUtils.EventLog.WriteErrorEntry(eze);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Failure in the Messaging system.");
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zMsgCenter"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lockSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "lockSessionToolStripMenuItem"));

            try
            {
                step = "Audit user logout";
                if (mCommon.User != null)
                {
                    EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                                    EZDeskDataLayer.ehr.Models.AuditAreas.User,
                                    EZDeskDataLayer.ehr.Models.AuditActivities.Logout,
                                    "");
                    mCommon.eCtrl.WriteAuditRecord(item);
                }
                step = "Clear person, log out user";
                mCommon.User = null;
                mCommon.Person = null;

                step = "Return to iDesk";
                iDeskLoad();

                step = "Clear demographics";
                frmIDesk.PersonId = -1;

                step = "Clear tabs, disable user and person items";
                flowLayoutPanel1.Controls.Clear();
                zEnableDisablePersonItems(false);
                zEnableDisableUserItems(false);

                step = "Display login dialog";
                zFormShown();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("lockSessionToolStripMenuItem failed", ex);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Lock Session Failed. Application ending.");
                frm.ShowDialog();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "lockSessionToolStripMenuItem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void zEnableDisableUserItems(bool state)
        {
            cmdMessages.Enabled = state;
            cmdCalendar.Enabled = state;
            cmdDesktop.Enabled = state;
            cmdMsgCtr.Enabled = state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private void zEnableDisablePersonItems(bool state)
        {
            cmdAll.Enabled = state;
            cmdScanPlace.Enabled = state;
            cmdForms.Enabled = state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 frm = new AboutBox1();
            frm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            zMsgCenter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void messageCenterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            zMsgCenter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dashBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iDeskLoad();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toDoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zDisplayToDo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void formsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            zForms();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filePlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zFilePlace();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zDisplayMessages();
        }

        private void btnToDo_Click(object sender, EventArgs e)
        {
            zDisplayToDo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            zDisplayTeller();
        }

        /// <summary>
        /// 
        /// </summary>
        private void zDisplayTeller()
        {
            Trace.Enter(Trace.RtnName(mModName, "zDisplayTeller"));

            try
            {
                if (frmEZTeller == null)
                {
                    frmEZTeller = new EZTeller.Form1(mCommon, this); //new Calendar2(mCommon);
                    frmEZTeller.TopLevel = false;
                    frmEZTeller.Text = "";
                    frmEZTeller.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    frmEZTeller.WindowState = FormWindowState.Normal;
                    frmEZTeller.Visible = true;
                    frmEZTeller.MinimizeBox = false;
                    frmEZTeller.MaximizeBox = false;
                    frmEZTeller.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    frmEZTeller.ControlBox = false;
                    frmEZTeller.ShowIcon = false;
                    frmEZTeller.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
                    frmEZTeller.Dock = DockStyle.Fill;
                    frmEZTeller.Width = pnlHosting.Width;
                    frmEZTeller.Height = pnlHosting.Height;
                    frmEZTeller.ShowInTaskbar = false;
                    frmEZTeller.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

                    pnlHosting.Controls.Add(frmEZTeller);
                    frmEZTellerIdx = pnlHosting.Controls.Count + -1;
                    frmEZTellerCtrlName = pnlHosting.Controls[frmEZTellerIdx].Name;
                }

                //splitContainer2.Panel1Collapsed = false;
                pnlHosting.Controls[frmEZTellerCtrlName].BringToFront();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zDisplayTeller failed", ex);
                EZUtils.EventLog.WriteErrorEntry(eze);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Failure in the calendar system.");
                frm.ShowDialog();
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zDisplayTeller"));
            }
        }

    }
}
