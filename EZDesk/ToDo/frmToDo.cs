using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using EZDeskDataLayer.TODO;
using System.Windows.Forms;
using EZUtils;
using MySql.Data.MySqlClient;

namespace ToDo
{
    //TODO: Cursor not being placed in the tbTitle text box.

    public partial class frmToDo : Form
    {
        private enum modes { listMode, taskMode }
        private modes mMode = modes.listMode;
        private DataTable mListTable = null;
        private DataTable mTaskTable = null;
        private int mCurrentListID = -1;
        private string mTitle = "";
        private string mListName = "";
        private string mModule = "ToDo";
        private MySqlConnection mConn = null;

        ///////////////////////////////////////////////////////////////////////////////
        // Form Stuff //
        ////////////////

        private ToDoController tCtrl = null;
        public frmToDo(MySqlConnection Conn)
        {
            InitializeComponent();
            mConn = Conn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmToDo_Load(object sender, EventArgs e)
        {
            string step = "";

            Trace.Enter(Trace.RtnName(mModule, "frmToDo_Load"));

            try
            {
                mTitle = this.Text;

                step = "Create connection string - start ToDoController";
                //Create the connection string, then start the ToDoController.
                //string connStr = "Server=Windows7VM-C2Q;" +
                //                    "Database=ehr;" +
                //                    "Uid=ehruser;" +
                //                    "Pwd=password";
                tCtrl = new ToDoController(mConn);

                step = "setup dataGridView1";
                dataGridView1.MultiSelect = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeColumns = true;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                step = "zShowList";
                zShowList(); //Display the todo lists;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "frmToDo_Load failed.");
                ex.Data.Add("step", step);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "frmToDo_Load"));
            }
        }

        /// <summary>
        /// The form is resizing, the column(s) in the dataGridView
        /// have to be resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "dataGridView1_Resize"));

            try
            {
                switch (mMode)
                {
                    case modes.listMode:
                        dataGridView1.Columns["ListName"].Width =
                            dataGridView1.Width - 23;
                        break;

                    case modes.taskMode:
                        dataGridView1.Columns["TaskName"].Width =
                            dataGridView1.Width - (23 + dataGridView1.Columns["TargetDate"].Width + dataGridView1.Columns["Complete"].Width);
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "dataGridView1_Resize failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "dataGridView1_Resize"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "dataGridView1_CellMouseClick"));

            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if ((e.RowIndex < 0) || (e.RowIndex >= dataGridView1.Rows.Count))
                    {
                        dataGridView1.ClearSelection();
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                        Application.DoEvents();
                        switch (mMode)
                        {
                            case modes.listMode:
                                cmsListMode.Show(MousePosition);
                                break;

                            case modes.taskMode:
                                cmsTaskMode.Show(MousePosition);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "dataGridView1_CellMouseClick failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "dataGridView1_CellMouseClick"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDone_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "cmdDone_Click"));

            try
            {
                this.Close();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "cmdDone_Click failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "cmdDone_Click"));
            }
        }

        /// <summary>
        /// The Add/Save button has been pressed, either add
        /// a new item or save the one being edited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "cmdAdd_Click"));

            try
            {
                if (tbTitle.Text.Trim().Length > 0)
                {
                    switch (mMode)
                    {
                        case modes.listMode:
                            if (cmdAdd.Text.ToLower() == "add")
                            {
                                zAddListItem();
                            }
                            else
                            {
                                zUpdateListItem();
                            }
                            break;

                        case modes.taskMode:
                            if (cmdAdd.Text.ToLower() == "add")
                            {
                                zAddTask();
                            }
                            else
                            {
                                zUpdateTask();
                            }
                            break;

                        default:
                            break;
                    }
                }
                cmdAdd.Text = "Add";
                cmdAdd.Tag = -1;
                tbTitle.Text = "";
                cmdDone.Enabled = true;
                dataGridView1.Enabled = true;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "cmdAdd_Click failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "cmdAdd_Click"));
            }
        }

        /// <summary>
        /// The user is selecting a row in the dataGridView. If the
        /// dataGridView is showing ToDoLists then bring up the tasks 
        /// for the selected list. If the dataGridView is showing a
        /// task list ?????
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "dataGridView1_CellClick"));

            try
            {
                switch (mMode)
                {
                    case modes.listMode:
                        mCurrentListID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                        mListName = dataGridView1.SelectedRows[0].Cells["ListName"].Value.ToString();
                        zShowTask(mCurrentListID);
                        mMode = modes.taskMode;
                        break;

                    case modes.taskMode:
                        if (dataGridView1.Columns[e.ColumnIndex].Name.ToLower() == "completechk")
                        {
                            int tblRow = zFindTaskTableRow(e.RowIndex);
                            mTaskTable.Rows[tblRow]["Completed"] = !(bool)mTaskTable.Rows[tblRow]["Completed"];
                            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                                zMapTaskTableToTaskItem(tblRow);
                            tCtrl.UpdateTask(taskItem);
                        }
                        break;

                    default:
                        break;
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "dataGridView1_CellClick failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "dataGridView1_CellClick"));
            }
        }

        /// <summary>
        /// The user wants to go back to the List view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBack_Click(object sender, EventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "cmdBack_Click"));

            try
            {
                zShowList();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "cmdBack_Click failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "cmdBack_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "dataGridView1_CellFormatting"));

            try
            {
                if (mMode == modes.taskMode)
                {
                    //Need to have the null dates NOT display
                    if (dataGridView1.Columns[e.ColumnIndex].Name.ToLower() == "targetdate")
                    {
                        DateTime? dt = dataGridView1.Rows[e.RowIndex].Cells["TargetDate"].Value.ToString() == "" ? null :
                            (DateTime?)Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells["TargetDate"].Value.ToString());
                        if (dt != null)
                        {
                            e.CellStyle.Format = "yyyy-MM-dd HH:mm";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "dataGridView1_CellFormatting failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "dataGridView1_CellFormatting"));
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        // LISTS //
        ///////////

        #region ToDoList
        
        /// <summary>
        /// Buld the List table (will eventually be an SQL call).
        /// </summary>
        private void zBuildListTable()
        {
            Trace.Enter(Trace.RtnName(mModule, "zBuildListTable"));

            try
            {
                mListTable = tCtrl.GetAllLists();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zBuildListTable failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zBuildListTable"));
            }
        }

        /// <summary>
        /// We need to 
        /// </summary>
        private void zShowList()
        {
            Trace.Enter(Trace.RtnName(mModule, "zShowList"));

            try
            {
                cmdBack.Visible = false;
                cmdBack.Enabled = false;
                tbTitle.Width = (cmdBack.Left + cmdBack.Width) - tbTitle.Left;

                zBuildListTable();
                dataGridView1.DataSource = mListTable;

                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["ID"].Width = 0;

                dataGridView1.Columns["ListName"].Visible = true;
                dataGridView1.Columns["ListName"].Width =
                    dataGridView1.Width - 23;

                dataGridView1.Columns["IsDeleted"].Visible = false;
                dataGridView1.Columns["IsDeleted"].Width = 0;

                dataGridView1.Columns["UserID"].Visible = false;
                dataGridView1.Columns["UserID"].Width = 0;

                mMode = modes.listMode;
                cmdAdd.Text = "Add";
                cmdAdd.Tag = -1;
                this.Text = mTitle;

                tbTitle.Focus();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zShowList failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zShowList"));
            }
        }

        /// <summary>
        /// This should not be possible as the context menu is
        /// programatically shown only when verified it should
        /// be shown. This is here just in case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmsListMode_Opening(object sender, CancelEventArgs e)
        {
            Trace.Enter(Trace.RtnName(mModule, "cmsListMode_Opening"));

            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    e.Cancel = true;
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "cmsListMode_Opening failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "cmsListMode_Opening"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddListItem()
        {
            EZDeskDataLayer.TODO.Models.ToDoList listItem =
                new EZDeskDataLayer.TODO.Models.ToDoList();
            DataRow dr = mListTable.NewRow();

            Trace.Enter(Trace.RtnName(mModule, "zAddListItem"));

            try
            {
                listItem.ListName = tbTitle.Text.Trim();
                listItem.UserID = 1;
                tCtrl.AddListItem(listItem);

                if (listItem.ID != -1)
                {
                    dr["ListName"] = listItem.ListName;
                    dr["ID"] = listItem.ID;
                    dr["IsDeleted"] = false;
                    dr["UserID"] = listItem.UserID;
                    mListTable.Rows.Add(dr);
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zAddListItem failed.");
                ex.Data.Add("listItem", listItem);
                ex.Data.Add("dr", dr);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zAddListItem"));
            }
        }

        /// <summary>
        /// The user wants to add a new ToDo List.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;

            Trace.Enter(Trace.RtnName(mModule, "renameListToolStripMenuItem_Click"));

            try
            {
                tbTitle.Text = dataGridView1.Rows[row].Cells["ListName"].Value.ToString();
                tbTitle.Focus();
                cmdAdd.Tag = row;
                cmdAdd.Text = "Save";
                cmdDone.Enabled = false;
                dataGridView1.Enabled = false;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "renameListToolStripMenuItem_Click failed.");
                ex.Data.Add("row", row);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "renameListToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zUpdateListItem()
        {
            int tblRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "zUpdateListItem"));

            try
            {
                tblRow = zFindListTableRow(-1);

                if (tblRow > -1)
                {
                    EZDeskDataLayer.TODO.Models.ToDoList listItem = zMapListTableToListItem(tblRow);
                    listItem.ListName = tbTitle.Text.Trim();

                    tCtrl.UpdateListItem(listItem);

                    mListTable.Rows[tblRow]["ListName"] = listItem.ListName;
                    //TODO: Sort the list table.
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zUpdateListItem failed.");
                ex.Data.Add("tblRow", tblRow);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zUpdateListItem"));
            }
        }

        /// <summary>
        /// Delete the selected list item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EZDeskDataLayer.TODO.Models.ToDoList listItem =
                new EZDeskDataLayer.TODO.Models.ToDoList();
            int tblRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "deleteListToolStripMenuItem_Click"));

            try
            {
                tblRow = zFindListTableRow(dataGridView1.SelectedRows[0].Index);
                if (tblRow > -1)
                {
                    listItem = zMapListTableToListItem(tblRow);
                    listItem.IsDeleted = true;

                    tCtrl.UpdateListItem(listItem);

                    mListTable.Rows.RemoveAt(tblRow);
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "deleteListToolStripMenuItem_Click failed.");
                ex.Data.Add("listItem", listItem);
                ex.Data.Add("tblRow", tblRow);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "deleteListToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listItem"></param>
        /// <param name="tblRow"></param>
        private int zFindListTableRow(int gridRow)
        {
            int dgvRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "zFindListTableRow"));

            try
            {
                dgvRow = gridRow;
                if (dgvRow < 0)
                {
                    dgvRow = (int)cmdAdd.Tag; ;
                }

                string temp = dataGridView1.Rows[dgvRow].Cells["ID"].Value.ToString();
                int id = Convert.ToInt32(temp);
                int tblRow = -1;
                for (int idx = 0; ((idx < mListTable.Rows.Count) && (tblRow == -1)); idx++)
                {
                    temp = mListTable.Rows[idx]["ID"].ToString();
                    if (Convert.ToInt32(temp) == id)
                    {
                        tblRow = idx;
                    }
                }

                return tblRow;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zFindListTableRow failed.");
                ex.Data.Add("dgvRow", dgvRow);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zFindListTableRow"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tblRow"></param>
        /// <returns></returns>
        private EZDeskDataLayer.TODO.Models.ToDoList zMapListTableToListItem(int tblRow)
        {
            EZDeskDataLayer.TODO.Models.ToDoList listItem = new EZDeskDataLayer.TODO.Models.ToDoList();

            Trace.Enter(Trace.RtnName(mModule, "zMapListTableToListItem"));

            try
            {
                listItem.ID = Convert.ToInt32(mListTable.Rows[tblRow]["ID"].ToString());
                listItem.ListName = mListTable.Rows[tblRow]["ListName"].ToString();
                listItem.UserID = Convert.ToInt32(mListTable.Rows[tblRow]["UserID"].ToString());
                listItem.IsDeleted = Convert.ToBoolean(mListTable.Rows[tblRow]["IsDeleted"].ToString());

                return listItem;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zMapListTableToListItem failed.");
                ex.Data.Add("listItem", listItem);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zMapListTableToListItem"));
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        // TASKS //
        ///////////
        // Routines for the To Do Tasks

        #region ToDoTasks

        /// <summary>
        /// Build the Task Table.
        /// </summary>
        /// <param name="listID"></param>
        private void zBuildTaskTable(int listID)
        {
            Trace.Enter(Trace.RtnName(mModule, "zBuildTaskTable"));

            try
            {
                mTaskTable = tCtrl.GetTasksByListID(listID);
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zBuildTaskTable failed");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zBuildTaskTable"));
            }
        }

        /// <summary>
        /// The user has clicked on a list, we are going to switch to the
        /// task view, show the tasks for the selected list.
        /// </summary>
        private void zShowTask(int id)
        {
            Trace.Enter(Trace.RtnName(mModule, "zShowTask"));

            try
            {
                cmdBack.Visible = true;
                cmdBack.Enabled = true;
                tbTitle.Width = (cmdBack.Left - 6) - tbTitle.Left;

                zBuildTaskTable(id);
                dataGridView1.DataSource = mTaskTable;

                dataGridView1.Columns["ID"].Visible = false;
                dataGridView1.Columns["ID"].Width = 0;

                //Some kind of problem.
                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
                column.Name = "CompleteChk";
                column.FlatStyle = FlatStyle.Standard;
                column.ThreeState = true;
                column.CellTemplate = new DataGridViewCheckBoxCell();
                column.DataPropertyName = "Completed";
                dataGridView1.Columns.Insert(1, column);
                dataGridView1.Columns["CompleteChk"].Visible = true;
                dataGridView1.Columns["CompleteChk"].Width = 25;
                dataGridView1.Columns["Completed"].Visible = false;
                dataGridView1.Columns["Completed"].Width = 0;

                dataGridView1.Columns["TaskName"].Visible = true;

                dataGridView1.Columns["TargetDate"].ValueType = typeof(System.DateTime?);
                dataGridView1.Columns["TargetDate"].Visible = true;
                dataGridView1.Columns["TargetDate"].Width = 95;
                dataGridView1.Columns["TargetDate"].DefaultCellStyle.Format = "yyyy-MM-dd";

                dataGridView1.Columns["IsDeleted"].Visible = false;
                dataGridView1.Columns["IsDeleted"].Width = 0;

                dataGridView1.Columns["ListID"].Visible = false;
                dataGridView1.Columns["ListID"].Width = 0;

                int tdWidth = dataGridView1.Columns["TargetDate"].Width;
                int compWidth = dataGridView1.Columns["CompleteChk"].Width;
                dataGridView1.Columns["TaskName"].Width =
                    dataGridView1.Width - (23 + tdWidth + compWidth);

                mMode = modes.taskMode;
                cmdAdd.Text = "Add";
                cmdAdd.Tag = -1;
                this.Text = mTitle + " - " + mListName;

                tbTitle.Focus();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zShowTask failed.");
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zShowTask"));
            }
        }

        // Where is cmsTaskMode_Opening()?

        /// <summary>
        /// 
        /// </summary>
        private void zAddTask()
        {
            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                new EZDeskDataLayer.TODO.Models.ToDoTasks();
            DataRow dr = mTaskTable.NewRow();

            Trace.Enter(Trace.RtnName(mModule, "zAddTask"));

            try
            {
                taskItem.IsDeleted = false;
                taskItem.ListID = mCurrentListID;
                taskItem.TargetDate = DateTime.MinValue;
                taskItem.TaskName = tbTitle.Text.Trim();
                taskItem.Completed = false;

                tCtrl.AddTask(taskItem);

                if (taskItem.ID != -1)
                {
                    dr["TaskName"] = taskItem.TaskName;
                    dr["ID"] = taskItem.ID;
                    dr["IsDeleted"] = false;
                    dr["TargetDate"] = taskItem.TargetDate;
                    dr["ListID"] = taskItem.ListID;
                    dr["Completed"] = taskItem.Completed;
                    mTaskTable.Rows.Add(dr);
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zAddTask failed.");
                ex.Data.Add("taskItem", taskItem);
                ex.Data.Add("dr", dr);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zAddTask"));
            }
        }

        /// <summary>
        /// The user wants to rename a task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.SelectedRows[0].Index;

            Trace.Enter(Trace.RtnName(mModule, "renameTaskToolStripMenuItem1_Click"));

            try
            {
                tbTitle.Text = dataGridView1.Rows[row].Cells["TaskName"].Value.ToString();
                cmdAdd.Tag = row;
                cmdAdd.Text = "Save";
                cmdDone.Enabled = false;
                dataGridView1.Enabled = false;

                tbTitle.Focus();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "renameTaskToolStripMenuItem1_Click failed.");
                ex.Data.Add("row", row);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "renameTaskToolStripMenuItem1_Click"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zUpdateTask()
        {
            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                new EZDeskDataLayer.TODO.Models.ToDoTasks();
            int tblRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "zUpdateTask"));

            try
            {
                tblRow = zFindTaskTableRow(-1);
                if (tblRow > -1)
                {
                    taskItem = zMapTaskTableToTaskItem(tblRow);
                    taskItem.TaskName = tbTitle.Text.Trim();

                    tCtrl.UpdateTask(taskItem);

                    mTaskTable.Rows[tblRow]["TaskName"] = taskItem.TaskName;
                    //TODO: Sort the Task List.
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zUpdateTask failed.");
                ex.Data.Add("tblRow", tblRow);
                ex.Data.Add("taskItem", taskItem);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zUpdateTask"));
            }
        }

        /// <summary>
        /// The user wants to set the target date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setTaskTargetDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                new EZDeskDataLayer.TODO.Models.ToDoTasks();
            int tblRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "setTaskTargetDateToolStripMenuItem_Click"));

            try
            {
                tblRow = zFindTaskTableRow(dataGridView1.SelectedRows[0].Index);
                if (tblRow > -1)
                {
                    taskItem = zMapTaskTableToTaskItem(tblRow);

                    frmToDoSetDate frm = new frmToDoSetDate(taskItem.TargetDate);
                    DialogResult dr = frm.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        taskItem.TargetDate = frm.SelectedDateTime;
                        tCtrl.UpdateTask(taskItem);
                        if (taskItem.TargetDate == null)
                        {
                            mTaskTable.Rows[tblRow]["TargetDate"] = DBNull.Value;
                        }
                        else
                        {
                            mTaskTable.Rows[tblRow]["TargetDate"] = taskItem.TargetDate;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "setTaskTargetDateToolStripMenuItem_Click failed.");
                ex.Data.Add("tblRow", tblRow);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "setTaskTargetDateToolStripMenuItem_Click"));
            }
        }

        /// <summary>
        /// The user wants to delete the selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteTaskToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                new EZDeskDataLayer.TODO.Models.ToDoTasks();
            int tblRow = -1;

            Trace.Enter(Trace.RtnName(mModule, "deleteTaskToolStripMenuItem1_Click"));

            try
            {
                tblRow = zFindTaskTableRow(dataGridView1.SelectedRows[0].Index);
                if (tblRow > -1)
                {
                    taskItem = zMapTaskTableToTaskItem(tblRow);
                    taskItem.IsDeleted = true;

                    tCtrl.UpdateTask(taskItem);

                    mTaskTable.Rows.RemoveAt(tblRow);
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "deleteTaskToolStripMenuItem1_Click failed.");
                ex.Data.Add("tblRow", tblRow);
                ex.Data.Add("taskItem", taskItem);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "deleteTaskToolStripMenuItem1_Click"));
            }
        }

        /// <summary>
        /// Find the DataTable row with ID from the ID in the dataGridView
        /// at the saved dataGridView row (Saved in the tag of the "Add"
        /// button).
        /// </summary>
        /// <returns></returns>
        private int zFindTaskTableRow(int gridRow)
        {
            int dgvRow = gridRow;

            Trace.Enter(Trace.RtnName(mModule, "zFindTaskTableRow"));

            try
            {
                if (dgvRow < 0)
                {
                    dgvRow = Convert.ToInt32(cmdAdd.Tag.ToString());
                }
                string temp = dataGridView1.Rows[dgvRow].Cells["ID"].Value.ToString();
                int id = Convert.ToInt32(temp);
                int tblRow = -1;
                for (int idx = 0; ((idx < mTaskTable.Rows.Count) && (tblRow == -1)); idx++)
                {
                    temp = mTaskTable.Rows[idx]["ID"].ToString();
                    if (Convert.ToInt32(temp) == id)
                    {
                        tblRow = idx;
                    }
                }
                return tblRow;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zFindTaskTableRow failed.");
                ex.Data.Add("dgvRow", dgvRow);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zFindTaskTableRow"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tblRow"></param>
        /// <returns></returns>
        private EZDeskDataLayer.TODO.Models.ToDoTasks zMapTaskTableToTaskItem(int tblRow)
        {
            EZDeskDataLayer.TODO.Models.ToDoTasks taskItem =
                new EZDeskDataLayer.TODO.Models.ToDoTasks();

            Trace.Enter(Trace.RtnName(mModule, "zMapTaskTableToTaskItem"));

            try
            {
                taskItem.ID = Convert.ToInt32(mTaskTable.Rows[tblRow]["ID"].ToString());
                taskItem.TaskName = mTaskTable.Rows[tblRow]["TaskName"].ToString();
                taskItem.ListID = Convert.ToInt32(mTaskTable.Rows[tblRow]["ListID"].ToString());
                taskItem.IsDeleted = Convert.ToBoolean(mTaskTable.Rows[tblRow]["IsDeleted"].ToString());

                taskItem.TargetDate = ((mTaskTable.Rows[tblRow]["TargetDate"] == null) || (mTaskTable.Rows[tblRow]["TargetDate"].ToString() == "")) ? null :
                    (DateTime?)Convert.ToDateTime(mTaskTable.Rows[tblRow]["TargetDate"].ToString());

                taskItem.Completed = Convert.ToBoolean(mTaskTable.Rows[tblRow]["Completed"].ToString());

                return taskItem;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Msg", "zMapTaskTableToTaskItem failed.");
                ex.Data.Add("taskItem", taskItem);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModule, "zMapTaskTableToTaskItem"));
            }
        }

        #endregion

    }
}
