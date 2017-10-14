using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmLogMaintenance : Form
    {
        private Form1 mFrm1 = null;
        private string mLogName = "";
        private int mLogID = -1;

        public frmLogMaintenance(Form1 frm1)
        {
            mFrm1 = frm1;
            InitializeComponent();
        }

        private void frmLogMaintenance_Load(object sender, EventArgs e)
        {
            zLoadListBox(listBox1, "ALL");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="exclude"></param>
        private void zLoadListBox(ListBox listBox, string exclude)
        {
            String[] excludeLogs = exclude.Split('|');
            DataTable tbl = mFrm1.mDac.GetLogNames();
            
            for (int i = 0; i < excludeLogs.Length; i++)
            {
                for (int j = 0; j < tbl.Rows.Count; j++)
                {
                    if (tbl.Rows[j].RowState != DataRowState.Deleted)
                    {
                        string s = tbl.Rows[j]["LogName"].ToString();
                        if (excludeLogs[i] == s)
                        {
                            tbl.Rows[j].Delete();
                        }
                    }
                }
            }

            listBox.DataSource = tbl;
            listBox.DisplayMember = "LogName";
            listBox.ValueMember = "ID";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mergeLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = "";
            int logID = -1;
            DataRowView row = (DataRowView)listBox1.SelectedItem;
            logName = row["LogName"].ToString();
            logID = (int)row["ID"];

            mLogName = logName;
            mLogID = logID;
            zLoadListBox(listBox2, "ALL|" + logName);
            zSetListBox2(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = "";
            int logID = -1;
            DataRowView row = (DataRowView)listBox1.SelectedItem;
            logName = row["LogName"].ToString();
            logID = (int)row["ID"];

            DialogResult dr = MessageBox.Show("Deleting will remove the log " + logName + " from the " +
                "selection list but will not delete the QSO entries.  You will no longer " +
                "be able to edit these QSOs.  The QSOs will only be visible in the 'ALL' " +
                "Log.  Are you sure you want to delete the log " + logName + "?",
                "Delete Log " + logName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                mFrm1.mDac.RemoveLog(logName, true);
                zLoadListBox(listBox1, "ALL");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renumberLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = "";
            int logID = -1;
            DataRowView row = (DataRowView)listBox1.SelectedItem;
            DataTable tbl = null;
            string sortExp = "StartDate";
            DataRow[] drarray;
            int num = 1;
            int id = -1;

            logName = row["LogName"].ToString();
            logID = (int)row["ID"];

            tbl = mFrm1.mDac.GetQSOs(logName, 0);
            drarray = tbl.Select("1=1", sortExp, DataViewRowState.CurrentRows);
            for (int i = 0; i < drarray.Length; i++)
            {
                id = (int)drarray[i]["ID"];
                mFrm1.mDac.UpdateQSOIntField(id, "Number", num);
                num++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string logName = "";
            int logID = -1;
            DataRowView row = (DataRowView)listBox1.SelectedItem;
            logName = row["LogName"].ToString();
            logID = (int)row["ID"];

            zSetRename();
            tbRename.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            zSetListBox2(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMerge_Click(object sender, EventArgs e)
        {
            string msg = "";
            string title = "";
            string sourceLogName = "";
            int sourceLogID = -1;
            string destLogName = "";
            int destLogID = -1;
            DataRowView row = (DataRowView)listBox1.SelectedItem;
            sourceLogName = row["LogName"].ToString();
            sourceLogID = (int)row["ID"];

            if (btnMerge.Text.ToUpper() == "MERGE")
            {
                if (listBox2.SelectedItems.Count == 1)
                {
                    row = (DataRowView)listBox2.SelectedItem;
                    destLogName = row["LogName"].ToString();
                    destLogID = (int)row["ID"];
                    msg = "Do you want to merge the " + sourceLogName + " into the " +
                            destLogName + " log?\r\nThis will eliminate the " + sourceLogName + " log.";
                    title = mFrm1.ProgramName + " Merging " + sourceLogName + " into " + destLogName;
                    DialogResult dr = MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        mFrm1.mDac.MergeLogs(sourceLogName, destLogName);
                        if (0 != mFrm1.mDac.RemoveLog(sourceLogName))
                        {
                            MessageBox.Show("There are still QSOs in the log " + sourceLogName + ". The log was not removed.",
                                            mFrm1.ProgramName + " Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            else
            {
                if (tbRename.Text.Trim().Length > 0)
                {
                    if (mFrm1.mDac.GetLogId(tbRename.Text.Trim()) != -1)
                    {
                        MessageBox.Show("The log name " + tbRename.Text.Trim() + " already exists. " +
                                            "You need to specify a new log name that is not already in use",
                                        mFrm1.ProductName + " Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        mFrm1.mDac.RenameLogEntry(sourceLogName, tbRename.Text.Trim());
                        mFrm1.mDac.RenameLogEntry(sourceLogName, tbRename.Text.Trim());
                    }
                }
            }

            zLoadListBox(listBox1, "ALL");
            zSetListBox2(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        private void zSetListBox2(bool set)
        {
            btnMerge.Top = 274;
            btnMerge.Text = "Merge";

            btnCancel.Top = 274;

            label2.Text = "Merge With:";
            label2.Enabled = set;
            label2.Visible = set;

            tbRename.Text = "";
            tbRename.Enabled = false;
            tbRename.Visible = false;

            listBox1.Enabled = !set;
            listBox2.Enabled = set;
            listBox2.Visible = set;
            btnCancel.Enabled = set;
            btnCancel.Visible = set;
            btnMerge.Enabled = set;
            btnMerge.Visible = set;
        }

        private void zSetRename()
        {
            btnMerge.Top = 82;
            btnMerge.Text = "Rename";

            btnCancel.Top = 82;

            label2.Text = "New Name:";
            label2.Enabled = true;
            label2.Visible = true;

            tbRename.Enabled = true;
            tbRename.Visible = true;
            btnMerge.Enabled = true;
            btnMerge.Visible = true;
            btnCancel.Enabled = true;
            btnCancel.Visible = true;
            listBox1.Enabled = false;
        }

    }
}
