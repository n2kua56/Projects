using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDesk
{
    /// <summary>
    /// This form displays the documents for the selected person and in
    /// the selected tab. By selecting from the list of documents the
    /// user can display the document. The documents may not be modified.
    /// Viewing documents and editing document properties is audited.
    /// 
    /// Currently *.jpg, *.txt, *.pdf are supported for displaying.
    /// 
    /// Future types are *.odt (OpenOffice Write documents), *.ods 
    /// (OpenOffice Spread Sheets) and possibly *.doc (Microsoft word
    /// documents.
    /// </summary>
    public partial class frmDocuments : Form
    {
        //TODO: Initial document view on entering the form is not audited.
        //TODO: Read the file with a method that will read \\path files
        //TODO: Fill the entire Form1 except the bottom?
        //TODO: Context Menu (Edit - Changes document record; Delete - marks the record as deleted)
        //TODO: Drag-Drop out of the ListBox1 to a tab.
        //TODO: Display OpenOffice Writer documents
        //TODO: Display OpenOffice Spread Sheet documents

        //DONE: 20141028 Documents that are in a tab for a user are listed
        //DONE: 20141028 *.jpg, *.txt and *.pdf files are displayed
        //DONE: 20141028 View of documents are audited
        //DONE: 20141028 Edit of document properties are audited

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private EZDeskDataLayer.EZDeskCommon mCommon = null;
        private EZDeskDataLayer.Documents.DocumentsController dCtrl = null;
        private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private int mTabId = -1;
        private int mPersonId = -1;
        private string mModName = "frmDocuments";

        /// <summary>
        /// Gets/Sets the tab id selected to retrieve documents for.
        /// </summary>
        public int TabId 
        {
            get { return mTabId; }
            set
            {
                mTabId = value;
                zLoadDocuments();
            }
        }

        /// <summary>
        /// Gets/Sets the PersonId that is loaded to retrieve documents for.
        /// </summary>
        public int PersonId
        {
            get { return mPersonId; }
            set
            {
                mPersonId = value;
                zLoadDocuments();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="common"></param>
        public frmDocuments(EZDeskDataLayer.EZDeskCommon common)
        {
            InitializeComponent();
            mCommon = common;
            dCtrl = new EZDeskDataLayer.Documents.DocumentsController(mCommon);
            eCtrl = new EZDeskDataLayer.ehr.ehrCtrl(mCommon);

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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDocuments_Load(object sender, EventArgs e)
        {
            webBrowser1.Top = 0;
            webBrowser1.Left = 0;
            webBrowser1.Height = splitContainer1.Panel1.Height - panel1.Height;
            webBrowser1.Width = splitContainer1.Panel1.Width;
            webBrowser1.Visible = false;

            richTextBox1.Top = 0;
            richTextBox1.Left = 0;
            richTextBox1.Width = webBrowser1.Width;
            richTextBox1.Height = webBrowser1.Height;
            richTextBox1.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zLoadDocuments()
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "zLoadDocuments"));

            try
            {
                if ((mTabId > -1) && (mPersonId > -1))
                {
                    DataTable tbl = dCtrl.GetDocumentsForTab(mPersonId, mTabId);
                    listBox1.DataSource = tbl;
                    listBox1.DisplayMember = "docName";
                    listBox1.ValueMember = "docId";
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("zLoadDocuments failed", ex);
                EZUtils.ExceptionDialog frm = 
                    new ExceptionDialog(eze, "zLoadDocuments failed");
                frm.Show();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "zLoadDocuments"));
            }
        }
        
        /// <summary>
        /// May remove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void splitContainer1_Panel1_DragDrop(object sender, DragEventArgs e)
        {

        }

        /// <summary>
        /// The user wants to edit the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "editToolStripMenuItem_Click"));

            try
            {
                frmDocumentEdit frm = new frmDocumentEdit(mCommon);
                frm.docId = Convert.ToInt32(listBox1.SelectedValue.ToString());
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.Documents,
                            EZDeskDataLayer.ehr.Models.AuditActivities.Edit,
                            "DocId: " + frm.docId.ToString());
                    eCtrl.WriteAuditRecord(item);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("editToolStripMenuItem_Click failed", ex);
                EZUtils.ExceptionDialog efrm = new ExceptionDialog(eze, "Edit document failed");
                EZUtils.EventLog.WriteErrorEntry(eze);
            }

            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "editToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// The user wants to toggle the IsActive flag for the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 2;
            MessageBox.Show("Delete not ready", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The user wants to copy this file to another tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 3;
            MessageBox.Show("Copy not ready", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The user wants to move this file to another tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 4;
            MessageBox.Show("Move not ready", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The user wants to view the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemView_Click(object sender, EventArgs e)
        {
            int i = 0;
            MessageBox.Show("View menu option not ready", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The user has clicked an item, they want to view it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo fi = null;
            string path = "";
            DataRow dr = null;
            bool viewed = false;
            int docId = -1;

            EZUtils.Trace.Enter(EZUtils.Trace.RtnName(mModName, "listBox1_SelectedIndexChanged"));

            try
            {
                dr = ((DataTable)listBox1.DataSource).Rows[listBox1.SelectedIndex];
                path = dr[6].ToString();
                lblFileName.Text = path;
                docId = Convert.ToInt32(dr["docId"].ToString());
                fi = new FileInfo(path);
                
                switch (fi.Extension.ToLower())
                {
                    case ".jpg":
                    case ".pdf":
                        webBrowser1.Url = new Uri(path);
                        webBrowser1.Visible = true;
                        richTextBox1.Visible = false;
                        viewed = true;
                        break;

                    case ".docx":
                        webBrowser1.Navigate(path);
                        webBrowser1.Visible = true;
                        richTextBox1.Visible = false;
                        viewed = true;
                        break;

                    case ".txt":
                        string temp = File.ReadAllText(path);
                        richTextBox1.Text = temp;
                        richTextBox1.Visible = true;
                        webBrowser1.Visible = false;
                        viewed = true;
                        break;

                    case ".odt":
                        richTextBox1.Visible = false;
                        webBrowser1.Visible = false;
                        viewed = true;
                        zDisplayOpenOfficeWriter(path);
                        viewed = true;
                        break;

                    case ".ods":
                        richTextBox1.Visible = false;
                        webBrowser1.Visible = false;
                        viewed = true;
                        zDisplayOpenOfficeWriter(path);
                        viewed = true;
                        break;

                    default:
                        break;
                }

                if (viewed)
                {
                    EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, 
                            mCommon.Person.PersonID,
                            EZDeskDataLayer.ehr.Models.AuditAreas.Documents,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "DocId: " + docId.ToString());
                    eCtrl.WriteAuditRecord(item);
                }
            }

            catch (Exception ex) 
            {
                EZException eze = new EZException("View of document failed", ex);
                eze.Add("path", path);
                EZUtils.ExceptionDialog frm = new ExceptionDialog(eze, "Document View failed");
                DialogResult fdr = frm.ShowDialog();
                EZUtils.EventLog.WriteErrorEntry(eze);
            }
            
            finally
            {
                EZUtils.Trace.Exit(EZUtils.Trace.RtnName(mModName, "listBox1_SelectedIndexChanged"));
            }
        }

        private void zDisplayOpenOfficeWriter(string path)
        {
            //Process p = Process.Start(@"C:\Program Files (x86)\OpenOffice 4\program\swriter.exe ");
            Process p = Process.Start(path);
            Thread.Sleep(2000);
            SetParent(p.MainWindowHandle, splitContainer1.Panel1.Handle);
            p.WaitForExit();
        }

    }
}
