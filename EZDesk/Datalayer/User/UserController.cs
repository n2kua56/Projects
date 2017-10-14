using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.User.Models;
using EZUtils;

namespace EZDeskDataLayer.User
{
    public class UserController : Controller
    {
        private string mModName = "UserController";

        ////public UserController(MySqlConnection Conn)
        ////{
        ////    mConn = Conn;
        ////}

        public UserController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "UserController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "UserController-Constructor"));
        }

        /// <summary>
        /// Get the entire list of entries in the UserSecurity table.
        /// </summary>
        /// <returns>UserSecurityID, UserName, IsActive for each user</returns>
        public DataTable GetSetupUserList()
        {
            DataTable dt = null;
            string step = "";
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "GetSetupUserList"));

            try
            {
                step = "Build querry";
                sql = "SELECT `UserSecurityID` AS ID, `PersonID`, " +
                                    "`UserName` AS Name, `IsActive` AS Active, " +
                                    "`CanSendMessages` AS Sign, `" +
                                    "CanRcvdSignMessages` AS Designate " +
                                "FROM `per_UserSecurity` " +
                                "ORDER BY `UserName`;";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                //Get the requested data into a databable.
                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "GetSetupUserList"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetSetupUserList"));
            }
        }

        /// <summary>
        /// Get a single user record matching the supplied PersonID
        /// </summary>
        /// <param name="personid">PersonID from the person table</param>
        /// <returns></returns>
        public UserDetails GetUserDetails(int personid)
        {
            string sql = "";
            MySqlCommand cmd = null;
            DataTable dt = null;
            UserDetails item = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetUserDetails"));
            try
            {
                step = "Build querry";
                sql = "SELECT `UserSecurityID`, `UserName`, `PersonID`, `UserPassWord`, " +
                                "`UserPassWordTime`, `Created`, `IsActive`, `CanSendMessages`, " +
                                "`CanRcvdSignMessages`, `Modified`, `LastViewedRelNotes`, " +
                                "`LoginCount`, `LastLogin`, `UDF1`, `UDF2`, `UDF3`, `UDF4`, " +
                                "`UDF5`, `UDF6`, `UDF7`, `UDF8`, `UDF9`, `UDF10`, `DirectEMail` " +
                            "FROM `per_UserSecurity` " +
                            "WHERE `PersonID`=@personid ";
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));

                step = "Get data";
                dt = GetDataTable(cmd);

                step = "Pull data";
                if ((dt != null) && (dt.Rows.Count == 1))
                {
                    DataRow dr = dt.Rows[0];
                    item = new UserDetails();
                    item.CanRcvdSignMessages = GetBool(dr, "CanRcvdSignMessages");
                    item.CanSendMessages = GetBool(dr, "CanSendMessages");
                    item.Created = GetDateTime(dr, "Created");
                    item.IsActive = GetBool(dr, "IsActive");
                    item.LastLogin = GetDateTime(dr, "LastLogin");
                    item.LastViewedRelNotes = dr["LastViewedRelNotes"].ToString();
                    item.LoginCount = GetInt(dr, "LoginCount");
                    item.Modified = GetDateTime(dr, "Modified");
                    item.PersonID = GetInt(dr, "PersonID");
                    item.UDF1 = dr["UDF1"].ToString();
                    item.UDF2 = dr["UDF2"].ToString();
                    item.UDF3 = dr["UDF3"].ToString();
                    item.UDF4 = dr["UDF4"].ToString();
                    item.UDF5 = dr["UDF5"].ToString();
                    item.UDF6 = dr["UDF6"].ToString();
                    item.UDF7 = dr["UDF7"].ToString();
                    item.UDF8 = dr["UDF8"].ToString();
                    item.UDF9 = dr["UDF9"].ToString();
                    item.UDF10 = dr["UDF10"].ToString();
                    item.UserName = dr["UserName"].ToString();
                    item.UserPassWord = dr["UserPassWord"].ToString();
                    item.UserPassWordTime = GetDateTime(dr, "UserPassWordTime");
                    item.UserSecurityID = GetInt(dr, "UserSecurityID");
                    item.DirectEmail = dr["DirectEMail"].ToString();
                }

                return item;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "GetUserDetails"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("personid", personid);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetUserDetails"));
            }
        }

        /// <summary>
        /// Save User record (INSERT or UPDATE) 
        /// </summary>
        /// <param name="user"></param>
        public void SaveUserDetails(UserDetails user, MySqlTransaction trans)
        {
            string sql = "";
            MySqlCommand cmd = null;
            DataTable dt = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "SaveUserDetails"));
            try
            {
                step = "Build querry";
                if (user.UserSecurityID > -1)
                {
                    sql = "UPDATE `per_UserSecurity` SET " + 
                                "`UserName`=@userName, " +  
                                "`UserPassWord`=@userpassword, " +
                                "`IsActive`=@active, " + 
                                "`CanSendMessages`=@canSendMsg, " +
                                "`CanRcvdSignMessages`=@canRcvdSignMsg, " + 
                                "`UDF1`=@udf1, " + 
                                "`UDF2`=@udf2, " +
                                "`UDF3`=@udf3, " + 
                                "`UDF4`=@udf4, " +
                                "`UDF5`=@udf5, " + 
                                "`UDF6`=@udf6, " + 
                                "`UDF7`=@udf7, " + 
                                "`UDF8`=@udf8, " + 
                                "`UDF9`=@udf9, " + 
                                "`UDF10`=@udf10 " +
                            "WHERE `UserSecurityId`=@userSecurityId ";
                    cmd = new MySqlCommand(sql, mConn, trans);
                }

                else
                {
                    sql = "INSERT INTO  `per_UserSecurity`(`UserSecurityID`, " + 
                                "`UserName`, `PersonID`, `UserPassWord`, " +
                                "`IsActive`, `CanSendMessages`, " +
                                "`CanRcvdSignMessages`, `LoginCount`, " + 
                                "`UDF1`, `UDF2`, `UDF3`, `UDF4`, " +
                                "`UDF5`, `UDF6`, `UDF7`, `UDF8`, " + 
                                "`UDF9`, `UDF10` " +
                            "VALUES(@userSecurityId, " +
                                "@userName, @personId, @userpassword, " +
                                "@active, @canSendMsg, " +
                                "@canSendMsg, 0, " +
                                "@udf1, @udf2, @udf3, @udf4, " +
                                "@udf5, @udf6, @udf7, @udf8, " +
                                "@udf9, @udf10) ";
                    cmd = new MySqlCommand(sql, mConn, trans);
                    cmd.Parameters.Add(new MySqlParameter("@personId", user.PersonID));
                }

                cmd.Parameters.Add(new MySqlParameter("@userName", user.UserName));
                cmd.Parameters.Add(new MySqlParameter("@userpassword", user.UserPassWord));
                cmd.Parameters.Add(new MySqlParameter("@active", user.IsActive));
                cmd.Parameters.Add(new MySqlParameter("@canSendMsg", user.CanSendMessages));
                cmd.Parameters.Add(new MySqlParameter("@canRcvdSignMsg", user.CanRcvdSignMessages));
                cmd.Parameters.Add(new MySqlParameter("@udf1", user.UDF1));
                cmd.Parameters.Add(new MySqlParameter("@udf2", user.UDF2));
                cmd.Parameters.Add(new MySqlParameter("@udf3", user.UDF3));
                cmd.Parameters.Add(new MySqlParameter("@udf4", user.UDF4));
                cmd.Parameters.Add(new MySqlParameter("@udf5", user.UDF5));
                cmd.Parameters.Add(new MySqlParameter("@udf6", user.UDF6));
                cmd.Parameters.Add(new MySqlParameter("@udf7", user.UDF7));
                cmd.Parameters.Add(new MySqlParameter("@udf8", user.UDF8));
                cmd.Parameters.Add(new MySqlParameter("@udf9", user.UDF9));
                cmd.Parameters.Add(new MySqlParameter("@udf10", user.UDF10));
                cmd.Parameters.Add(new MySqlParameter("@userSecurityId", user.UserSecurityID));

                step = "Get data";
                cmd.ExecuteNonQuery();

                if (user.UserSecurityID < 0)
                {
                    user.UserSecurityID = (int)cmd.LastInsertedId;
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "SaveUserDetails"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "SaveUserDetails"));
            }
        }

        public UserDetails CheckUserPassword(string userName, string password)
        {
            UserDetails rtn = null;
            string sql = "";
            MySqlCommand cmd = null;
            DataTable dt = null;
            UserDetails item = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "CheckUserPassword"));
            try
            {
                step = "Build querry";
                sql = "SELECT `UserSecurityID`, `UserName`, `PersonID`, `UserPassWord`, " +
                                "`UserPassWordTime`, `Created`, `IsActive`, `CanSendMessages`, " +
                                "`CanRcvdSignMessages`, `Modified`, `LastViewedRelNotes`, " +
                                "`LoginCount`, `LastLogin`, `UDF1`, `UDF2`, `UDF3`, `UDF4`, " +
                                "`UDF5`, `UDF6`, `UDF7`, `UDF8`, `UDF9`, `UDF10`, `DirectEMail` " +
                            "FROM `per_UserSecurity` " +
                            "WHERE `UserName`=@username " +
                                "AND `UserPassWord`=@password ";
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@username", userName));
                cmd.Parameters.Add(new MySqlParameter("@password", password));

                step = "Get data";
                dt = GetDataTable(cmd);

                step = "Pull data";
                if ((dt != null) && (dt.Rows.Count == 1))
                {
                    DataRow dr = dt.Rows[0];
                    rtn = zMapUserDetails(dr);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "CheckUserPassword"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "CheckUserPassword"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private UserDetails zMapUserDetails(DataRow dr)
        {
            UserDetails item = new UserDetails();

            item.CanRcvdSignMessages = GetBool(dr, "CanRcvdSignMessages");
            item.CanSendMessages = GetBool(dr, "CanSendMessages");
            item.Created = GetDateTime(dr, "Created");
            item.IsActive = GetBool(dr, "IsActive");
            item.LastLogin = GetDateTime(dr, "LastLogin");
            item.LastViewedRelNotes = dr["LastViewedRelNotes"].ToString();
            item.LoginCount = GetInt(dr, "LoginCount");
            item.Modified = GetDateTime(dr, "Modified");
            item.PersonID = GetInt(dr, "PersonID");
            item.UDF1 = dr["UDF1"].ToString();
            item.UDF2 = dr["UDF2"].ToString();
            item.UDF3 = dr["UDF3"].ToString();
            item.UDF4 = dr["UDF4"].ToString();
            item.UDF5 = dr["UDF5"].ToString();
            item.UDF6 = dr["UDF6"].ToString();
            item.UDF7 = dr["UDF7"].ToString();
            item.UDF8 = dr["UDF8"].ToString();
            item.UDF9 = dr["UDF9"].ToString();
            item.UDF10 = dr["UDF10"].ToString();
            item.UserName = dr["UserName"].ToString();
            item.UserPassWord = dr["UserPassWord"].ToString();
            item.UserPassWordTime = GetDateTime(dr, "UserPassWordTime");
            item.UserSecurityID = GetInt(dr, "UserSecurityID");
            
            return item;
        }

    }
}
