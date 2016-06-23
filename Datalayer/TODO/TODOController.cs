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
        public DataTable GetAllLists(int id)
        {
            DataTable rtn = null;
            //string sql = "SELECT `ID`, `ListName`, `IsDeleted`, `UserID` " +
            //                "FROM `todo_lists` " +
            //                "WHERE `IsDeleted`=0 " +
            //                    "AND `UserId`=@id " +
            //                "ORDER BY `ListName` ";
            string sql = "SELECT l.`ID`, l.`ListName`, l.`IsDeleted`, l.`UserID`, " +
                                "CASE " +
                                    "WHEN tot.Total > 0 THEN " +
                                        "CONCAT('(', ifnull(tasks.TasksLeft, 0), ' of ', ifnull(tot.Total, 0), ')') " +
                                    "ELSE " +
                                        "'' " +
                                "END AS 'Tasks' " +
                            "FROM `todo_lists` AS l " +
                                "LEFT JOIN ( " +
                                            "SELECT `ListID`, IF(ISNULL(COUNT(*)),0, COUNT(*)) AS Total " +
                                                "FROM `todo_tasks` " +
                                                "WHERE `IsDeleted`=0 " +
                                                "GROUP BY `ListID` " +
                                            ") AS tot ON (tot.`ListID`=l.`ID`) " +
                                "LEFT JOIN ( " +
                                            "SELECT `ListID`, IF(ISNULL(COUNT(*)),0, COUNT(*)) AS TasksLeft " +
                                                "FROM `todo_tasks` " +
                                                "WHERE `IsDeleted`=0 " +
                                                    "AND `Completed`=0 " +
                                                "GROUP BY `ListID` " +
                                            ") AS tasks ON (tasks.`ListID`=l.`ID`) " +
                            "WHERE l.`IsDeleted`=0 " +
                                "AND l.`UserId`=@id " +
                            "GROUP BY l.`ID` " +
                            "ORDER BY l.`ListName`; ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "GetAllLists"));
            
            try
            {
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                rtn = GetDataTable(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllLists failed", ex);
                eze.Add("sql", sql);
                throw eze;
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

                ExecuteNonQueryCmd(cmd);
                item.IsDeleted = false;
                item.ID = (int)cmd.LastInsertedId;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("AddListItem failed", ex);
                eze.Add("sql", sql);
                eze.Add("ListName", item.ListName);
                eze.Add("UserID", item.UserID);
                throw eze;
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

                ExecuteNonQueryCmd(cmd);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("UpdateListItem failed", ex);
                eze.Add("sql", sql);
                eze.Add("ListName", item.ListName);
                eze.Add("UserID", item.UserID);
                throw eze;
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
        public DataTable GetTasksByListID(int listID, bool showDeleted, bool showCompleted)
        {
            DataTable rtn = null;
            string sql = "SELECT `ID`, `TaskName`, `Completed`, `TargetDate`, " +
                                "`IsDeleted`, `ListID` " +
                            "FROM `todo_tasks` " +
                            "WHERE `ListID`=@ListID ";
                            //    "AND `Completed`=@comp " +
                            //    "AND `ListID`=@ListID " +
                            //"ORDER BY `TaskName` ";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "GetTasksByListID"));

            try
            {
                if (!showDeleted)
                {
                    sql += "AND `IsDeleted`=0 ";
                }
                if (!showCompleted)
                {
                    sql += "AND `Completed`=0 ";
                }
                sql += "ORDER BY `Completed`, `TargetDate`, `TaskName` ";

                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@ListID", listID));

                rtn = GetDataTable(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetTaskByListID failed", ex);
                eze.Add("sql", sql);
                throw eze;
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
                ExecuteNonQueryCmd(cmd);
                task.ID = (int)cmd.LastInsertedId;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("AddTask failed", ex);
                eze.Add("sql", sql);
                throw eze;
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
                ExecuteNonQueryCmd(cmd);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("UpdateTask filed", ex);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateTask"));
            }
        }

    }
}
