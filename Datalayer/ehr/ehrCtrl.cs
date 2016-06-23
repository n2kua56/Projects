using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EZDeskDataLayer.Address.Models;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZUtils;

namespace EZDeskDataLayer.ehr
{
    public class ehrCtrl : Controller
    {
        private string mModName = "ehrCtrl";
        //private EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskCommon mCommon;

        public ehrCtrl(EZDeskCommon common)
        {
            Trace.Enter(Trace.RtnName(mModName, "ehrCtrl-Constructor"));
            mCommon = common;
            mConn = mCommon.Connection; // conn;
            Init(mCommon.Connection);

            if (mCommon.Connection.State != ConnectionState.Open)
            {
                mCommon.Connection.Open();
            }

            Trace.Exit(Trace.RtnName(mModName, "ehrCtrl-Constructor"));
        }

        #region Auditing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void WriteAuditRecord(EZDeskDataLayer.ehr.Models.AuditItem item)
        {
            string sql = "";
            string step = "";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "WriteAuditRecord"));

            try
            {
                step = "Build Insert";
                sql = "INSERT INTO `ehr_auditlog` (`AuditDateTime`, `UserId`, `PersonId`, " +
                                "`AuditAreaId`, `AuditActivityId`, `Description`) " +
                        "VALUES(@auditTime, @user, @person, @area, @activity, @desc) ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@auditTime", item.AuditDateTime));
                cmd.Parameters.Add(new MySqlParameter("@user", item.UserId));
                cmd.Parameters.Add(new MySqlParameter("@person", item.PersonId));
                cmd.Parameters.Add(new MySqlParameter("@area", item.AuditAreaId));
                cmd.Parameters.Add(new MySqlParameter("@activity", item.AuditActivityId));
                cmd.Parameters.Add(new MySqlParameter("@desc", item.Description));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                if (item.Id < 0)
                {
                    item.Id = (int)cmd.LastInsertedId;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteAuditRecord failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteAuditRecord"));
            }
        }

        /// <summary>
        /// Get a table of Audit records that are between the min and max datetime specified
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        /// <returns></returns>
        public DataTable GetAuditLog(DateTime minDate, DateTime maxDate)
        {
            string sql = "";
            string step = "";
            DataTable rtn = null;
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "GetAuditLog"));

            try
            {
                sql = "SELECT log.`Id`, log.`AuditDateTime`, log.`UserId`, user.`UserName`, " +
		                    "user.`PersonId`, per.`FirstName`, per.`MiddleName`, per.`LastName`, " +
                            "per.`PersonTypeID`, log.`AuditAreaId`, area.`Description` AS AuditArea, " + 
		                    "log.`auditActivityId`, activity.`Description` AS AuditActivity, " +
                            "log.`Description` " +
	                    "FROM `ehr_auditlog` AS log " +
		                    "LEFT JOIN `ehr_auditarea` AS area ON (log.`AuditAreaId` = area.`AreaId`) " +
		                    "LEFT JOIN `ehr_auditActivity` AS activity ON (log.`auditActivityId`=activity.`ActivityId`) " +
		                    "LEFT JOIN `per_usersecurity` AS user ON (log.`UserId`=user.`USERSECURITYID`) " + 
		                    "LEFT JOIN `per_person` AS per ON (user.`PersonId` = per.`PersonID`) " +
	                    "WHERE log.`AuditDateTime` >= @min " +
		                    "AND log.`AuditDateTime` <= @max " +
	                    "ORDER BY `AuditDateTime` ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@min", minDate));
                cmd.Parameters.Add(new MySqlParameter("@max", maxDate));

                step = "Put data";
                rtn = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem item =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| min=" + minDate.ToString("yyyy-MM-dd hh:mm:ss") +
                                        " max=" + maxDate.ToString("yyyy-MM-dd hh:mm:ss"));
                WriteAuditRecord(item);


                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAuditLog failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAuditLog"));
            }
        }

        #endregion

        #region Available Properties
        // ========================================================================================
        //

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllProperties()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAllProperties"));

            try
            {
                step = "Build query";
                sql = "SELECT `PropID`, `Created`, `IsActive`, `Modified`, `PROPERTYNAME`, " +
                             "`DESCRIPTION`, `PROPERTYVALUE`, `VISIBILITY` " +
                        "FROM `ehr_availableproperties` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Read the data";
                tbl = GetDataTable(cmd);

                EZDeskDataLayer.ehr.Models.AuditItem item =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(item);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProperty failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllProperties"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        public void WriteAvailablePropertyItem(Models.AvailablePropertyItem prop)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteAvailablePropertyItem"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `PropID` " +
                        "FROM `ehr_availableproperties` " +
                        "WHERE `PropertyName`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", prop.PropertyName));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["PropID"].ToString()) : -2;
                
                if (id < 0)
                {
                    step = "Build Insert";
                    sql = "INSERT INTO `ehr_availableproperties` (`IsActive`, `PROPERTYNAME`, " +
                                    "`DESCRIPTION`, `PROPERTYVALUE`) " +
                            "VALUES(@active, @name, @desc, @propval) ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                }

                else
                {
                    step = "Build Update";
                    sql = "UPDATE `ehr_availableproperties` SET " +
                                "`IsActive`=@active, " + 
                                "`PROPERTYNAME`=@name, " +
                                "`DESCRIPTION`=@desc, " + 
                                "`PROPERTYVALUE`=@propval " +
                            "WHERE `PropID`=@propid ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                    cmd.Parameters.Add(new MySqlParameter("@propid", id));
                    moreInfo = " propid=" + id.ToString();
                }

                cmd.Parameters.Add(new MySqlParameter("@active", prop.IsActive));
                cmd.Parameters.Add(new MySqlParameter("@name", prop.PropertyName));
                cmd.Parameters.Add(new MySqlParameter("@desc", prop.Description));
                cmd.Parameters.Add(new MySqlParameter("@propval", prop.PropertyValue));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                if (prop.PropID < 0)
                {
                    prop.PropID = (int)cmd.LastInsertedId;
                }

                EZDeskDataLayer.ehr.Models.AuditItem item =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| active=" + prop.IsActive +
                                        " name=" + prop.PropertyName +
                                        " desc=" + prop.Description +
                                        " propval=" + prop.PropertyValue +
                                        moreInfo);
                WriteAuditRecord(item);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteAvailablePropertyItem failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("prop", prop);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteAvailablePropertyItem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Models.AvailablePropertyItem GetPropertyItem(string key)
        {
            Models.AvailablePropertyItem rtn = null;
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProperty"));

            try
            {
                step = "Build querry";
                sql = "SELECT `PropID`, `Created`, `IsActive`, `Modified`, `PROPERTYNAME`, " +
                             "`DESCRIPTION`, `PROPERTYVALUE`, `VISIBILITY`) " +
                        "FROM `ehr_availableproperties` " +
                        "WHERE `PropertyName`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    rtn = new Models.AvailablePropertyItem();
                    rtn.PropID = Convert.ToInt32(tbl.Rows[0]["PropId"].ToString());
                    rtn.Created = Convert.ToDateTime(tbl.Rows[0]["Created"].ToString());
                    rtn.IsActive = Convert.ToBoolean(tbl.Rows[0]["IsActive"].ToString());
                    rtn.Modified = Convert.ToDateTime(tbl.Rows[0]["Modified"].ToString());
                    rtn.PropertyName = tbl.Rows[0]["PropertyName"].ToString();
                    rtn.Description = tbl.Rows[0]["Description"].ToString();
                    rtn.PropertyValue = tbl.Rows[0]["PropertyValue"].ToString();
                }

                EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| key=" + key);
                WriteAuditRecord(item);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProperty failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProperty"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetProperty(string key)
        {
            string rtn = "";
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProperty"));

            try
            {
                step = "Build querry";
                sql = "SELECT `PropertyValue` " +
                        "FROM `ehr_AvailableProperties` " +
                        "WHERE `PropertyName`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    rtn = tbl.Rows[0]["PropertyValue"].ToString();
                }

                EZDeskDataLayer.ehr.Models.AuditItem item =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| key=" + key);
                WriteAuditRecord(item);
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProperty failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProperty"));
            }
        }

        #endregion

        #region Profile
        // ===============================================================
        //

        #region profUsers
        // ===============================================================
        // 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void WriteProfileUsers(Models.ProfileUsers item)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteProfileUsers"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `ProfUserID` " +
                        "FROM `prof_profusers` " +
                        "WHERE `CategoryID`=@catid " +
                          "AND `ProfID`=@profid " +
                          "AND `UserId`=@userid ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", item.CategoryID));
                cmd.Parameters.Add(new MySqlParameter("@profid", item.ProfID));
                cmd.Parameters.Add(new MySqlParameter("@userid", item.UserId));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + item.CategoryID.ToString() +
                                        " profid=" + item.ProfID.ToString() + 
                                        " userid=" + item.UserId.ToString());
                WriteAuditRecord(aItem);

                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["ProfUserID"].ToString()) : -2;

                if (tbl.Rows.Count == 0)
                {
                    step = "Build Insert";
                    sql = "INSERT INTO `prof_profusers`(`CategoryID`, `ProfID`, " +
                                "`IsActive`, `Val`, `UserId`) " +
                            "VALUES(@catid, @profid, @active, @val, @userid) ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                }

                else
                {
                    step = "Build Update";
                    sql = "UPDATE `prof_profusers` SET " +
                                "`CategoryID`=@catid, " +
                                "`ProfID`=@profid, " +
                                "`IsActive`=@active, " +
                                "`Val`=@desc, " +
                                "`UserId=@userid " +
                            "WHERE `ProfUserID`=@id ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                    cmd.Parameters.Add(new MySqlParameter("@id", item.ProfUserID));
                    moreInfo = " id=" + item.ProfUserID.ToString();
                }

                cmd.Parameters.Add(new MySqlParameter("@catid", item.CategoryID));
                cmd.Parameters.Add(new MySqlParameter("@profid", item.ProfID));
                cmd.Parameters.Add(new MySqlParameter("@active", item.IsActive));
                cmd.Parameters.Add(new MySqlParameter("@val", item.Val));
                cmd.Parameters.Add(new MySqlParameter("@userid", item.UserId));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                if (item.ProfUserID < 0)
                {
                    item.ProfUserID = (int)cmd.LastInsertedId;
                }

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + item.CategoryID.ToString() +
                                        " profid=" + item.ProfID.ToString() +
                                        " userid=" + item.UserId.ToString() +
                                        " active=" + item.IsActive.ToString() +
                                        " val=" + item.Val +  
                                        moreInfo);
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteProfileUsers failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteProfileUsers"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="catid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        //public Models.ProfileUsers GetProfileUsers(int catid, int userid)
        //{
        //    Models.ProfileUsers item = null;
        //    DataTable tbl = null;
        //    string sql = "";
        //    string step = "";
        //    int profid = -1;

        //    Trace.Enter(Trace.RtnName(mModName, "GetProfUser"));

        //    try
        //    {
        //        step = "Get the prof_profusers record";
        //        sql = "SELECT ProfUserID, CategoryID, ProfID, UserId, " +
        //                        "Created, IsActive, Modified, Val " +
        //                "FROM prof_profusers " +

        //                "WHERE CategoryID=#catid " +
        //                    "AND ProfID=@profid " +
        //                    "AND UserID=@userid ";
        //        MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
        //        cmd.Parameters.Add(new MySqlParameter("@catid", catid));
        //        cmd.Parameters.Add(new MySqlParameter("@profid", profid));
        //        cmd.Parameters.Add(new MySqlParameter("@userid", userid));
        //        tbl = GetDataTable(cmd);

        //        if ((tbl != null) && (tbl.Rows.Count == 1))
        //        {
        //            DataRow dr = tbl.Rows[0];
        //            item = new Models.ProfileUsers();
        //            item.CategoryID = catid;
        //            item.Created = Convert.ToDateTime(dr["Created"].ToString());
        //            item.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
        //            item.Modified = Convert.ToDateTime(dr["Modified"].ToString());
        //            item.ProfID = profid;
        //            item.ProfUserID = Convert.ToInt32(dr["ProfUserId"].ToString());
        //            item.UserId = userid;
        //            item.Val = dr["Val"].ToString();
        //        }

        //        // ----- Audit SQL call -----
        //        EZDeskDataLayer.ehr.Models.AuditItem aItem =
        //            new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
        //                EZDeskDataLayer.ehr.Models.AuditAreas.System,
        //                EZDeskDataLayer.ehr.Models.AuditActivities.View,
        //                "SQL: " + sql + "| catid=" + catid.ToString() +
        //                                " profid=" + profid.ToString() +
        //                                " userid=" + userid.ToString());
        //        WriteAuditRecord(aItem);

        //        return item;
        //    }

        //    catch (Exception ex)
        //    {
        //        EZException eze = new EZException("GetProfUser failed", ex);
        //        eze.Add("step", step);
        //        eze.Add("sql", sql);
        //        eze.Add("catid", catid);
        //        eze.Add("profid", profid);
        //        eze.Add("userid", userid);
        //        throw eze;
        //    }

        //    finally
        //    {
        //        Trace.Exit(Trace.RtnName(mModName, "GetProfUser"));
        //    }
        //}
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Models.ProfileUsers GetProfUser(string category, string profkey, int userid)
        {
            Models.ProfileUsers item = null;
            DataTable tbl = null;
            string sql = "";
            object obj = null;
            string step = "";
            int catid = -1;
            int profid = -1;

            Trace.Enter(Trace.RtnName(mModName, "GetProfUser"));

            try
            {
                step = "Get CatID";
                sql = "SELECT `CategoryID` " +
                        "FROM `prof_profcategories` " +
                        "WHERE `Category`=@key";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", category));
                obj = ExecuteScalar(cmd);
                catid = (obj != null) ? Convert.ToInt32(obj.ToString()) : -1;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + category);
                WriteAuditRecord(aItem);

                step = "Get ProfileID";
                sql = "SELECT `ProfID` " +
                        "FROM `prof_profdefault` " +
                        "WHERE `ProfKey`=@key";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", category));
                obj = ExecuteScalar(cmd);
                profid = (obj != null) ? Convert.ToInt32(obj.ToString()) : -1;

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + category);
                WriteAuditRecord(aItem);

                step = "Get the prof_profusers record";
                sql = "SELECT ProfUserID, CategoryID, ProfID, UserId, " +
                                "Created, IsActive, Modified, Val " +
                        "FROM prof_profusers " +
                        "WHERE CategoryID=#catid " +
                            "AND ProfID=@profid " +
                            "AND UserID=@userid ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", catid));
                cmd.Parameters.Add(new MySqlParameter("@profid", profid));
                cmd.Parameters.Add(new MySqlParameter("@userid", userid));
                tbl = GetDataTable(cmd);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    DataRow dr = tbl.Rows[0];
                    item = new Models.ProfileUsers();
                    item.CategoryID = catid;
                    item.Created = Convert.ToDateTime(dr["Created"].ToString());
                    item.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    item.Modified = Convert.ToDateTime(dr["Modified"].ToString());
                    item.ProfID = profid;
                    item.ProfUserID = Convert.ToInt32(dr["ProfUserId"].ToString());
                    item.UserId = userid;
                    item.Val = dr["Val"].ToString();
                }

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + catid.ToString() +
                                        " profid=" + profid.ToString() +
                                        " userid=" + userid.ToString());
                WriteAuditRecord(aItem);

                return item;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfUser failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("category", category);
                eze.Add("catid", catid);
                eze.Add("profkey", profkey);
                eze.Add("profid", profid);
                eze.Add("userid", userid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfUser"));
            }
        }

        /// <summary>
        /// Delete the ProfUsers entry for the userid with the specified 
        /// category and profkey.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="profkey"></param>
        /// <param name="userid"></param>
        public void DeleteProfUsers(string category, string profkey, int userid)
        {
            string sql = "";
            object obj = null;
            string step = "";
            int catid = -1;
            int profid = -1;

            Trace.Enter(Trace.RtnName(mModName, "DeleteProfUsers"));

            try
            {
                step = "Get CatID";
                sql = "SELECT `CategoryID` " +
                        "FROM `prof_profcategories` " +
                        "WHERE `Category`=@key";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", category));
                obj = ExecuteScalar(cmd);
                catid = (obj != null) ? Convert.ToInt32(obj.ToString()) : -1;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + category);
                WriteAuditRecord(aItem);

                step = "Get ProfileID";
                sql = "SELECT `ProfID` " +
                        "FROM `prof_profdefault` " +
                        "WHERE `ProfKey`=@key";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", category));
                obj = ExecuteScalar(cmd);
                profid = (obj != null) ? Convert.ToInt32(obj.ToString()) : -1;

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + category);
                WriteAuditRecord(aItem);

                step = "Deleting the prof_profusers record";
                sql = "DELETE FROM `prof_profusers` " +
                        "WHERE `CategoryID`=@catid " +
                            "AND `ProfID`=@profid " +
                            "AND `UserId`=@userid";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", catid));
                cmd.Parameters.Add(new MySqlParameter("@profid", profid));
                cmd.Parameters.Add(new MySqlParameter("@userid", userid));
                ExecuteNonQueryCmd(cmd);

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + catid.ToString() +
                                        " profid=" + profid.ToString() +
                                        " userid=" + userid.ToString());
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteProfileUsers failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("category", category);
                eze.Add("catid", catid);
                eze.Add("profkey", profkey);
                eze.Add("profid", profid);
                eze.Add("userid", userid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteProfUsers"));
            }
        }

        #endregion
        
        #region UserGroups
        // ===============================================================
        //

        /// <summary>
        /// Insert or Update the data in the ProfileUserGroups specified
        /// to add the Profile User Group or update it.
        /// </summary>
        /// <param name="item"></param>
        public void WriteProfileUserGroups(EZDeskDataLayer.ehr.Models.ProfileUserGroups item)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteProfileUserGroups"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `ProfID` " +
                        "FROM `prof_profusergroups` " +
                        "WHERE `GroupId`=@id ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", item.GroupID));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["GroupID"].ToString()) : -2;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| id=" + item.GroupID.ToString());
                WriteAuditRecord(aItem);

                if ((tbl.Rows.Count == 0) || (item.GroupID == id))
                {
                    if (item.GroupID == -1)
                    {
                        step = "Build Insert";
                        sql = "INSERT INTO `prof_profusergroups`(`IsActive`, " +
                                    "`Description`) " +
                                "VALUES(@active, @desc) ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                    }

                    else
                    {
                        step = "Build Update";
                        sql = "UPDATE `prof_profusergroups` SET " +
                                    "`IsActive`=@active, " +
                                    "`Description`=@desc " +
                                "WHERE `GroupID`=@id ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                        cmd.Parameters.Add(new MySqlParameter("@id", item.GroupID));
                        moreInfo = " id=" + item.GroupID.ToString();
                    }

                    cmd.Parameters.Add(new MySqlParameter("@active", item.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@desc", item.Description));

                    step = "Put data";
                    ExecuteNonQueryCmd(cmd);

                    if (item.GroupID < 0)
                    {
                        item.GroupID = (int)cmd.LastInsertedId;
                    }

                    // ----- Audit SQL call -----
                    aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| active=" + item.IsActive.ToString() +
                                            " desc=" + item.Description.ToString() +
                                            moreInfo);
                    WriteAuditRecord(aItem);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteProfileUserGroups failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteProfileUserGroups"));
            }
        }

        /// <summary>
        /// Get the list of all Profile User Groups
        /// </summary>
        /// <returns></returns>
        public DataTable GetProfileUserGroups()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProfileUserGroups"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `GroupID`, `IsActive`, `Description` " +
                        "FROM `prof_profusergroups` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileUserGroups failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfileUserGroups"));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Models.ProfileUserGroups GetUserGroup()
        {
            Models.ProfileUserGroups item = null;
            return item;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void DeleteUserGroup()
        {
        }

        #endregion
        
        #region Profile Default
        // ===============================================================
        //

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDefaultProfiles()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter("GetAllDefaultProfiles");

            try
            {
                step = "Build Querry";
                sql = "SELECT c.`Category`, p.`ProfKey`, p.`Description`, p.`Val` " +
                        "FROM prof_profdefault p " +
                            "LEFT JOIN prof_profcategories c ON (p.`CategoryID`=c.`ProfCatID`) " +
                        "ORDER BY c.`Category`, p.`ProfKey`;";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllDefaultProfiles failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllDefaultProfiles"));
            }
        }

        /// <summary>
        /// Insert or Update the ProfileDefault that is specified in the
        /// ProfileDefault item. This creates or updates a profile in
        /// the system and provides the default value if no group has an
        /// override and no user override is found.
        /// </summary>
        /// <param name="item"></param>
        public void WriteProfileDefault(Models.ProfileDefault item)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteProfileDefault"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `ProfID` " +
                        "FROM `prof_profdefault` " +
                        "WHERE `ProfKey`=@key " +
                          "AND `CategoryID`=@catid ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", item.ProfKey));
                cmd.Parameters.Add(new MySqlParameter("@catid", item.CategoryID));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["ProfID"].ToString()) : -2;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + item.ProfKey +
                                        " catid=" + item.CategoryID.ToString());
                WriteAuditRecord(aItem);

                if (id < 0)
                {
                    step = "Build Insert";
                    sql = "INSERT INTO `prof_profdefault`(`ProfKey`, " +
                                "`CategoryID`, `IsActive`, `Val`, " + 
                                "`Description`, `UDFID`, `Security`) " +
                            "VALUES(@key, @catid, @active, @val, " + 
                                    "@desc, @udfid, @security) ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                }

                else
                {
                    step = "Build Update";
                    sql = "UPDATE `prof_profdefault` SET " +
                                "`ProfKey`=@key, " +
                                "`CategoryID`=@catid, " + 
                                "`IsActive`=@active, " + 
                                "`Val`=@val, " + 
                                "`Description`=@desc, " + 
                                "`Security`=@security " +
                            "WHERE `ProfID`=@id ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                    cmd.Parameters.Add(new MySqlParameter("@id", id));
                    moreInfo = " id=" + item.ProfID.ToString();
                }

                cmd.Parameters.Add(new MySqlParameter("@key", item.ProfKey));
                cmd.Parameters.Add(new MySqlParameter("@catid", item.CategoryID));
                cmd.Parameters.Add(new MySqlParameter("@active", item.IsActive));
                cmd.Parameters.Add(new MySqlParameter("@val", item.Val));
                cmd.Parameters.Add(new MySqlParameter("@desc", item.Description));
                cmd.Parameters.Add(new MySqlParameter("@security", item.Security));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                if (item.ProfID < 0)
                {
                    item.ProfID = (int)cmd.LastInsertedId;
                }

                // ----- Audit SQL call -----
                aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + item.ProfKey +
                                        " catid=" + item.CategoryID.ToString() +
                                        " active=" + item.IsActive.ToString() + 
                                        " val=" + item.Val +
                                        " desc=" + item.Description + 
                                        " security=" + item.Security.ToString() +
                                        moreInfo);
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteProfileDefault failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteProfileDefault"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetProfileUsers(int CategoryID, int userId)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            try
            {
                step = "Build Querry";
                sql = "SELECT * FROM ( " +
                            "SELECT `ProfID`, `ProfKey`, `CategoryID`, `Description`, " +
                                    "`Val`, `Security`, 0 AS Override " +
                                "FROM prof_profdefault " +
                                "WHERE `CategoryID`=@catid " +
                                    "AND `ProfID` NOT IN (SELECT `ProfID` FROM prof_profusers WHERE `CategoryID`=@catid AND `UserID`=@userid) " +
                        "UNION " +
                            "SELECT d.`ProfID`, d.`ProfKey`, d.`CategoryID`, d.`Description`, " +
                                    "u.`Val`, d.`Security`, 1 AS Override " +
                                "FROM prof_profdefault AS d " +
                                    "JOIN prof_profusers AS u ON (d.`ProfID` = u.`ProfID`) " +
                                "WHERE d.`CategoryID`=@catid " +
                                    "AND u.`UserID`=@userid) AS temp " +
                        "ORDER BY `ProfKey`; ";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", CategoryID));
                cmd.Parameters.Add(new MySqlParameter("@userid", userId));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + CategoryID.ToString() +
                                        " userid=" + userId.ToString());
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileDefaults failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfileDefaults"));
            }
        }

        /// <summary>
        /// Return all of the Profile entries for a given category.
        /// </summary>
        /// <param name="categoryid"></param>
        /// <returns></returns>
        public DataTable GetProfileDefaults(int categoryid)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProfileDefaults"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `ProfID`, `ProfKey`, `CategoryID`, `Description`, " +
                            "`Val`, `Security`, 0 AS Override " + 
                        "FROM prof_profdefault " + 
                        "WHERE `CategoryID`=@catid " +
                        "ORDER BY `ProfKey` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", categoryid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + categoryid.ToString());
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileDefaults failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfileDefaults"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Models.ProfileDefault GetDefault()
        {
            Models.ProfileDefault item = null;

            return item;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void DeleteDefault()
        {
        }

        #endregion

        #region Profile Category
        // ===============================================================
        //

        /// <summary>
        /// Insert or Update the Profile Category specified by the
        /// ProfileCategory item.
        /// </summary>
        /// <param name="item"></param>
        public void WriteProfileCategory(Models.ProfileCategory item)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteProfileCategory"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `ProfCatID` " +
                        "FROM `prof_profcategories` " +
                        "WHERE `Category`=@cat ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@cat", item.Category));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["ProfCatID"].ToString()) : -2;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| cat=" + item.Category.ToString());
                WriteAuditRecord(aItem);

                if ((tbl.Rows.Count == 0) || (item.ID == id))
                {
                    if (item.ID == -1)
                    {
                        step = "Build Insert";
                        sql = "INSERT INTO `prof_profcategories`(`IsActive`, " +
                                    "`Category`, `Description`) " +
                                "VALUES(@active, @cat, @desc) ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                    }

                    else
                    {
                        step = "Build Update";
                        sql = "UPDATE `prof_profcategories` SET " +
                                    "`IsActive`=@active, " +
                                    "`Category`=@cat, " +
                                    "`Description`=@desc " +
                                "WHERE `ProfCatID`=@id ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                        cmd.Parameters.Add(new MySqlParameter("@id", item.ID));
                        moreInfo = " id=" + item.ID.ToString();
                    }

                    cmd.Parameters.Add(new MySqlParameter("@active", item.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@cat", item.Category));
                    cmd.Parameters.Add(new MySqlParameter("@desc", item.Description));

                    step = "Put data";
                    ExecuteNonQueryCmd(cmd);

                    if (item.ID < 0)
                    {
                        item.ID = (int)cmd.LastInsertedId;
                    }

                    // ----- Audit SQL call -----
                    aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| active=" + item.IsActive.ToString() +
                                            " cat=" + item.Category +
                                            " desc=" + item.Description.ToString() +
                            moreInfo);
                    WriteAuditRecord(aItem);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteProfileCategory failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteProfileCategory"));
            }
        }

        /// <summary>
        /// Get a list of all Profile Categories
        /// </summary>
        /// <returns></returns>
        public DataTable GetProfileCategories()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProfileCategories"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `ProfCatID`, `IsActive`, `Category`, `Description` " +
                        "FROM `prof_profcategories` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileCategories failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfilesCategories"));
            }
        }

        /// <summary>
        /// Get the Category with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Models.ProfileCategory GetCategory(string name)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            Models.ProfileCategory item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetCategory"));

            try
            {
                step = "Build query";
                sql = "SELECT `ProfCatId`, `Created`, `IsActive`, `Modified`, " +
                            "`Category`, `Description` " +
                        "FROM `prof_profcategories` " +
                        "WHERE `Category`=@cat ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@cat", name));

                step = "Get Data";
                tbl = GetDataTable(cmd);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    step = "Map data";
                    DataRow dr = tbl.Rows[0];
                    item = new Models.ProfileCategory();
                    item.ID = Convert.ToInt32(dr["ProfCatId"].ToString());
                    item.Created = Convert.ToDateTime(dr["Created"].ToString());
                    item.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                    item.Modified = Convert.ToDateTime(dr["Modified"].ToString());
                    item.Category = dr["Category"].ToString();
                    item.Description = dr["Description"].ToString();
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| cat=" + name);
                WriteAuditRecord(aItem);

                return item;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetCategory failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetCategory"));
            }
        }

        /// <summary>
        /// Delete the record with the specified name.
        /// NOTE: ONLY used by the unit test routines
        /// </summary>
        /// <param name="name"></param>
        public void DeleteCategory(string name)
        {
            string sql = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "DeleteCategory"));

            try
            {
                step = "Build query";
                sql = "DELETE FROM `prof_profcategories` " +
                        "WHERE `Category`=@cat ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@cat", name));

                step = "Issue command";
                ExecuteNonQueryCmd(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| cat=" + name);
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("DeleteCategory failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteCategory"));
            }
        }

        #endregion

        public int GetProfileId(int CatID, string key)
        {
            string step = "";
            string sql = "";
            DataTable tbl = null;
            int rtn = -1;

            Trace.Enter(Trace.RtnName(mModName, "GetProfileId"));

            try
            {
                step = "Build Query";
                sql = "SELECT `ProfID` " +
                        "FROM `prof_profdefault` " +
                        "WHERE `ProfKey`=@key " +
                            "AND `CategoryID`=@catid; ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@catid", CatID));
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count > 0))
                {
                    rtn = Convert.ToInt32(tbl.Rows[0]["ProfID"].ToString());
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| catid=" + CatID.ToString() +
                                        " key=" + key);
                WriteAuditRecord(aItem);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileValue failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                eze.Add("catid", CatID);
                eze.Add("tbl", tbl);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfileValue"));
            }
        }

        /// <summary>
        /// Get the Profile value for the specified category, profile key 
        /// and user id.
        /// </summary>
        /// <param name="cat">Name of the Category</param>
        /// <param name="key">The Profile key</param>
        /// <param name="userid">The User ID</param>
        /// <returns></returns>
        public string GetProfileValue(string cat, string key, int userid)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProfileValue"));
            
            try
            {
                step = "Build querry";
                sql = "SELECT d.`Val` AS `Val`, 3 AS `priority` " +
                        "FROM `prof_profdefault` AS d " +
                        "JOIN `prof_profcategories` AS c ON c.`ProfCatID`=d.`CategoryID` " +
                        "WHERE c.`Category`=@cat " +
                            "AND d.`ProfKey`=@key " +
                      "UNION " +
                      "SELECT u.Val AS `Val`, 1 AS `priority` " +
                        "FROM `prof_profusers` AS u " +
                        "JOIN `prof_profcategories` AS c ON c.`ProfCatID`=u.`CategoryID` " +
                        "JOIN `prof_profdefault` AS d ON u.`ProfID`=d.`ProfID` " +
                        "WHERE c.`Category`=@cat " +
                            "AND d.`ProfKey`=@key " +
                            "AND u.`UserId`=@userid " +
                      "UNION " +
                      "SELECT g.`Val` AS `Val`, 2 " +
                        "FROM `prof_profgroups` AS g " +
                        "JOIN `prof_profdefault` AS d ON g.`ProfDefID`=d.`ProfID` " +
                        "JOIN `prof_profgroupmembership` AS m ON m.`GroupID`=g.`GroupID` " +
                        "JOIN `prof_profcategories` AS c ON c.`ProfCatID`=d.`CategoryID` " +
                        "WHERE m.`UserID`=@userid " +
                            "AND c.`Category`=@cat " +
                            "AND d.`ProfKey`=@key " +
                      "ORDER BY `priority`; ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@cat", cat));
                cmd.Parameters.Add(new MySqlParameter("@key", key));
                cmd.Parameters.Add(new MySqlParameter("@userid", userid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count > 0))
                {
                    rtn = tbl.Rows[0]["Val"].ToString();
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| cat=" + cat +
                                        " key=" + key +
                                        " userid=" + userid.ToString());
                WriteAuditRecord(aItem);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProfileValue failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                eze.Add("cat", cat);
                eze.Add("userid", userid);
                eze.Add("tbl", tbl);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProfileValue"));
            }
        }

        #endregion

        #region tabs
        // ========================================================
        //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public Models.tabItem GetTab(int tabId)
        {
            Models.tabItem rtn =
                new Models.tabItem();
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetTab"));

            try
            {
                step = "Build querry";
                sql = "SELECT `tabId`, `tabName`, `tabDesc`, `IsActive`, `DisplaySeq` " +
                        "FROM `ehr_tabs` " +
                        "WHERE `tabId`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", tabId));

                step = "Get data";
                tbl = GetDataTable(cmd);
                if (tbl.Rows.Count > 0)
                {
                    rtn.TabId = Convert.ToInt32(tbl.Rows[0]["tabId"].ToString());
                    rtn.TabName = tbl.Rows[0]["tabName"].ToString();
                    rtn.TabDesc = tbl.Rows[0]["tabDesc"].ToString();
                    rtn.IsActive = Convert.ToBoolean(tbl.Rows[0]["IsActive"].ToString());
                    rtn.DisplaySeq = Convert.ToInt32(tbl.Rows[0]["DisplaySeq"].ToString());
                }


                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + tabId.ToString());
                WriteAuditRecord(aItem);

                return rtn;
            }
            catch (Exception ex)
            {
                EZException eze = new EZException("GetTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public void WriteTab(Models.tabItem tab)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteTab"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `tabId` " +
                        "FROM `ehr_tabs` " +
                        "WHERE `tabName`=@name ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@name", tab.TabName));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ? 
                    Convert.ToInt32(tbl.Rows[0]["tabId"].ToString()) : -2;


                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| name=" + tab.TabName);
                WriteAuditRecord(aItem);

                if ((tbl.Rows.Count == 0) || (tab.TabId == id))
                {
                    if (tab.TabId < 0)
                    {
                        step = "Insert tab - Build querry";
                        sql = "INSERT INTO `ehr_tabs`(`tabName`, `tabDesc`, `IsActive`, `DisplaySeq`) " +
                                "VALUES(@name, @desc, @active, @seq) ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                    }

                    else
                    {
                        step = "Update Tab - Build Querry";
                        sql = "UPDATE `ehr_tabs` SET " +
                                    "`tabName`=@name, " +
                                    "`tabDesc`=@desc, " +
                                    "`IsActive`=@active, " +
                                    "`DisplaySeq`=@seq " +
                                "WHERE `tabId`=@tabid ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                        cmd.Parameters.Add(new MySqlParameter("@tabid", tab.TabId));
                        moreInfo = " tabid=" + tab.TabId.ToString();
                    }
                    
                    cmd.Parameters.Add(new MySqlParameter("@name", tab.TabName));
                    cmd.Parameters.Add(new MySqlParameter("@desc", tab.TabDesc));
                    cmd.Parameters.Add(new MySqlParameter("@active", tab.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@seq", tab.DisplaySeq));

                    step = "Put data";
                    ExecuteNonQueryCmd(cmd);

                    if (tab.TabId < 0)
                    {
                        tab.TabId = (int)cmd.LastInsertedId;
                    }


                    // ----- Audit SQL call -----
                    aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| name=" + tab.TabName + 
                                            " desc=" + tab.TabDesc +
                                            " active=" + tab.IsActive.ToString() +
                                            " seq=" + tab.DisplaySeq.ToString() +
                                            moreInfo);
                    WriteAuditRecord(aItem);
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tab", tab);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTabs()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAllTabs"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `tabId`, `tabName`, `tabDesc`, " +
                            "`IsActive`, `DisplaySeq` " +
                        "FROM `ehr_tabs` " +
                        "ORDER BY `DisplaySeq`, `tabName` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);


                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllTabs failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllTabs"));
            }
        }

        #endregion

        #region Drawers
        // ======================================================
        //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public Models.DrawerItem GetDrawer(int drawerId)
        {
            Models.DrawerItem rtn = new Models.DrawerItem();
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string temp = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDrawer"));

            try
            {
                step = "Build querry";
                sql = "SELECT `Id`, `drawerName`, `drawerDesc`, " +
                        "`Seq`, `IsActive`, `Created` " +
                        "FROM `ehr_drawers` " +
                        "WHERE `Id`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", drawerId));

                step = "Get data";
                tbl = GetDataTable(cmd);
                if (tbl.Rows.Count > 0)
                {
                    DataRow dr = tbl.Rows[0];
                    rtn.DrawerId = Convert.ToInt32(tbl.Rows[0]["Id"].ToString());
                    rtn.DrawerName = tbl.Rows[0]["drawerName"].ToString();
                    rtn.DrawerDesc = tbl.Rows[0]["drawerDesc"].ToString();
                    temp = tbl.Rows[0]["IsActive"].ToString();
                    rtn.IsActive = Convert.ToBoolean(temp);
                    temp = tbl.Rows[0]["Seq"].ToString();
                    rtn.Seq = Convert.ToInt32(temp);
                    temp = tbl.Rows[0]["Created"].ToString();
                    rtn.Created = Convert.ToDateTime(temp);
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| key=" + drawerId.ToString());
                WriteAuditRecord(aItem);

                return rtn;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "GetDrawer"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("drawerId", drawerId);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDrawer"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public void WriteDrawer(Models.DrawerItem drawer)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteDrawer"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `Id` " +
                        "FROM `ehr_drawers` " +
                        "WHERE `drawerName`=@name ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@name", drawer.DrawerName));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["Id"].ToString()) : -2;

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| name=" + drawer.DrawerName);
                WriteAuditRecord(aItem);

                if ((tbl.Rows.Count == 0) || (drawer.DrawerId == id))
                {
                    if (drawer.DrawerId == -1)
                    {
                        step = "Build Insert";
                        sql = "INSERT INTO `ehr_drawers`(`drawerName`, `drawerDesc`, " +
                                    "`IsActive`, `Seq`) " +
                                "VALUES(@name, @desc, @active, @seq) ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                    }

                    else
                    {
                        step = "Build Update";
                        sql = "UPDATE `ehr_drawers` SET " +
                                    "`DrawerName`=@name, " +
                                    "`DrawerDesc`=@desc, " +
                                    "`IsActive`=@active, " +
                                    "`Seq`=@seq " +
                                "WHERE `Id`=@drawerid ";
                        cmd = new MySqlCommand(sql, mCommon.Connection);
                        cmd.Parameters.Add(new MySqlParameter("@drawerid", drawer.DrawerId));
                        moreInfo = " drawerid=" + drawer.DrawerId.ToString();
                    }

                    cmd.Parameters.Add(new MySqlParameter("@name", drawer.DrawerName));
                    cmd.Parameters.Add(new MySqlParameter("@desc", drawer.DrawerDesc));
                    cmd.Parameters.Add(new MySqlParameter("@active", drawer.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@seq", drawer.Seq));

                    step = "Put data";
                    ExecuteNonQueryCmd(cmd);

                    if (drawer.DrawerId < 0)
                    {
                        drawer.DrawerId = (int)cmd.LastInsertedId;
                    }

                    // ----- Audit SQL call -----
                    aItem = new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                            EZDeskDataLayer.ehr.Models.AuditAreas.System,
                            EZDeskDataLayer.ehr.Models.AuditActivities.View,
                            "SQL: " + sql + "| name=" + drawer.DrawerName +
                                            " desc=" + drawer.DrawerDesc +
                                            " active=" + drawer.IsActive.ToString() +
                                            " seq=" + drawer.Seq.ToString() +
                                            moreInfo);
                    WriteAuditRecord(aItem);
                }            
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteDrawer failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tab", drawer);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteDrawer"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDrawers()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAllDrawers"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `Id`, `drawerName`, `drawerDesc`, " +
                            "`IsActive`, `Seq` " +
                        "FROM `ehr_drawers` " +
                        "ORDER BY `Seq`, `drawerName` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);


                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql);
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllDrawers failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllDrawers"));
            }
        }

        #endregion

        #region DrawerTabs
        // ============================================================
        //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.drawertabsItem GetDrawerTab(int id)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            MySqlCommand cmd = null;
            Models.drawertabsItem rtn = null;

            Trace.Enter(Trace.RtnName(mModName, "GetDrawerTab"));

            try
            {
                step = "Build Select";
                sql = "SELECT `Id`, `DrawerId`, `TabId`, `Created` " +
                            "FROM `ehr_drawertabs` " +
                            "WHERE `Id` = @id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                step = "Get data";
                tbl = GetDataTable(cmd);

                if (tbl.Rows.Count > 0)
                {
                    step = "Extract data";
                    rtn = new Models.drawertabsItem();
                    DataRow dr = tbl.Rows[0];
                    rtn.Id = Convert.ToInt32(dr["Id"].ToString());
                    rtn.DrawerId = Convert.ToInt32(dr["DrawerId"].ToString());
                    rtn.TabId = Convert.ToInt32(dr["TabId"].ToString());
                    rtn.Created = Convert.ToDateTime(dr["Created"].ToString());
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| id=" + id.ToString());
                WriteAuditRecord(aItem);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDrawerTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDrawerTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAllTabsForDrawer(int id)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";
            MySqlCommand cmd = null;
            Models.drawertabsItem rtn = null;

            Trace.Enter(Trace.RtnName(mModName, "GetDrawerTab"));

            try
            {
                step = "Build Select";
                sql = "SELECT dt.`Id`, dt.`DrawerId`, dt.`TabId`, dt.`Created`, t.`tabName`, t.`tabDesc` " +
                            "FROM `ehr_drawertabs` AS dt " +
                                "JOIN `ehr_tabs` AS t ON dt.`TabId`=t.`tabId` " +
                            "WHERE `DrawerId` = @id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| id=" + id.ToString());
                WriteAuditRecord(aItem);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllTabsForDrawer failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("id", id);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllTabsForDrawer"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteDrawerTabs(int id)
        {
            string sql = "";
            string step = "";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "DeleteDrawerTabs"));

            try
            {
                step = "Build Insert";
                sql = "DELETE FROM `ehr_drawertabs` " +
                        "WHERE `Id`=@id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", id));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| id=" + id.ToString());
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("DeleteDrawerTabs failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("drawertab", id);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteDrawerTabs"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawertab"></param>
        public void AddEditDrawerTabs(Models.drawertabsItem drawertab)
        {
            string sql = "";
            string step = "";
            MySqlCommand cmd = null;
            string moreInfo = "";

            Trace.Enter(Trace.RtnName(mModName, "AddEditDrawerTabs"));

            try
            {
                if (drawertab.Id == -1)
                {
                    step = "Build Insert";
                    sql = "INSERT INTO `ehr_drawertabs` " +
                                "(`DrawerId`, `TabId`) " +
                            "VALUES(@drawerid, @tabid) ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                }

                else
                {
                    step = "Build Update";
                    sql = "UPDATE `ehr_drawertabs` " +
                            "SET " +
                                "`DrawerId` = @drawerid, " +
                                "`TabId` = @tabid " +
                            "WHERE `Id` = @id ";
                    cmd = new MySqlCommand(sql, mCommon.Connection);
                    cmd.Parameters.Add(new MySqlParameter("@id", drawertab.Id));
                    moreInfo = " id=" + drawertab.Id.ToString();
                }

                cmd.Parameters.Add(new MySqlParameter("@drawerid", drawertab.DrawerId));
                cmd.Parameters.Add(new MySqlParameter("@tabid", drawertab.TabId));

                step = "Put data";
                ExecuteNonQueryCmd(cmd);

                if (drawertab.Id < 0)
                {
                    drawertab.Id = (int)cmd.LastInsertedId;
                }

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| drawerid=" + drawertab.DrawerId.ToString() +
                                        " tabid=" + drawertab.TabId.ToString() +
                                        moreInfo);
                WriteAuditRecord(aItem);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("AddEditDrawerTabs failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("drawertab", drawertab);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AddEditDrawerTabs"));
            }
        }

        #endregion

    }
}