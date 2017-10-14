using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer.TODO
{
    public class ToDoController : Controller
    {
        private string mModName = "ToDoController.";

        ////public ToDoController(MySqlConnection conn)
        ////{
        ////    mConn = conn;
        ////}

        public ToDoController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "ToDoController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "ToDoController-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllLists()
        {
            DataTable rtn = null;
            string sql = "SELECT `ID`, `ListName`, `IsDeleted`, `UserID` " +
                            "FROM `todo_lists` " +
                            "WHERE `IsDeleted`=0 " +
                            "ORDER BY `ListName` ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "GetAllLists"));
            
            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Clear();

                rtn = GetDataTable(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetAllLists"));
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllLists"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void AddListItem(EZDeskDataLayer.TODO.Models.ToDoList item)
        {
            string sql = "INSERT INTO `todo_lists`(`ListName`, `UserID`) " +
                            "VALUES(@listName, @userID)";
                           
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "AddListItem"));

            try
            {
                item.ID = -1;   //Not inserted yet.
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@listName", item.ListName);
                cmd.Parameters.AddWithValue("@userID", item.UserID);

                cmd.ExecuteNonQuery();
                item.IsDeleted = false;
                item.ID = (int)cmd.LastInsertedId;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "AddListItem"));
                ex.Data.Add("sql", sql);
                ex.Data.Add("ListName", item.ListName);
                ex.Data.Add("UserID", item.UserID);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AddListItem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateListItem(EZDeskDataLayer.TODO.Models.ToDoList item)
        {
            string sql = "UPDATE `todo_lists` " +            
                            "SET `ListName`=@listName, "+
                                "`UserID`=@userID, " +
                                "`IsDeleted`=@isDeleted " +
                            "WHERE `ID`=@id";

            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "UpdateListItem"));

            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@listName", item.ListName);
                cmd.Parameters.AddWithValue("@userID", item.UserID);
                cmd.Parameters.AddWithValue("@isDeleted", item.IsDeleted);
                cmd.Parameters.AddWithValue("@id", item.ID);

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "UpdateListItem"));
                ex.Data.Add("sql", sql);
                ex.Data.Add("ListName", item.ListName);
                ex.Data.Add("UserID", item.UserID);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateListItem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public DataTable GetTasksByListID(int listID)
        {
            DataTable rtn = null;
            string sql = "SELECT `ID`, `TaskName`, `Completed`, `TargetDate`, " +
                                "`IsDeleted`, `ListID` " +
                            "FROM `todo_tasks` " +
                            "WHERE `IsDeleted`=0 " +
                                "AND `ListID`=@ListID " +
                            "ORDER BY `TaskName` ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "GetTasksByListID"));

            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@ListID", listID));

                rtn = GetDataTable(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetTasksByListID"));
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetTasksByListID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(EZDeskDataLayer.TODO.Models.ToDoTasks task)
        {
            string sql = "INSERT INTO `todo_tasks`(`TaskName`, `Completed`, " +
                                    "`TargetDate`, `ListID`) " +
                            "VALUES(@taskName, @completed, @targetDate, @listID) ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "AddTask"));

            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@taskName", task.TaskName);
                cmd.Parameters.AddWithValue("@completed", task.Completed);
                cmd.Parameters.AddWithValue("@targetDate", task.TargetDate);
                cmd.Parameters.AddWithValue("@listID", task.ListID);
                cmd.ExecuteNonQuery();
                task.ID = (int)cmd.LastInsertedId;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "AddTask"));
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AddTask"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public void UpdateTask(EZDeskDataLayer.TODO.Models.ToDoTasks task)
        {
            string sql = "UPDATE `todo_tasks` SET " +
                                "`TaskName`=@taskName, " +
                                "`Completed`=@completed, " +
                                "`TargetDate`=@targetDate, " +
                                "`IsDeleted`=@isDeleted, " +
                                "`ListID`=@listID " +
                            "WHERE `ID`=@id ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "UpdateTask"));

            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.AddWithValue("@taskName", task.TaskName);
                cmd.Parameters.AddWithValue("@completed", task.Completed);
                cmd.Parameters.AddWithValue("@targetDate", task.TargetDate);
                cmd.Parameters.AddWithValue("@isDeleted", task.IsDeleted);
                cmd.Parameters.AddWithValue("@listID", task.ListID);
                cmd.Parameters.AddWithValue("@id", task.ID);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "UpdateTask"));
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateTask"));
            }
        }

    }
}
