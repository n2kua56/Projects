using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EZDeskDataLayer;
using EZDeskDataLayer.Communications.Models;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer.Communications
{
    public class CommunicationCtrl : Controller
    {
        private string mModName = "CommunicationCtrl";
        private EZDeskCommon mCommon = null;

        public CommunicationCtrl(EZDeskCommon common)
        {
            Trace.Enter(Trace.RtnName(mModName, "CommunicationCtrl-Constructor"));
            mCommon = common;
            Trace.Exit(Trace.RtnName(mModName, "CommunicationCtrl-Constructor"));
        }
        /// <summary>
        /// Get the CommunicationTypeID for the type passed in.
        /// </summary>
        /// <param name="key">Communication Type Name</param>
        /// <returns>CommunicationTypeID found or -1 not found</returns>
        public int GetCommunicationTypeByName(string key)
        {
            string sql = "";
            MySqlCommand cmd = null;
            DataTable tbl = null;
            int id = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetCommunicationTypeByName"));

            try
            {
                step = "Buld querry";
                sql = "SELECT `CommunicationTypeID` " +
                        "FROM `per_CommunicationType` " +
                        "WHERE `IsActive`=1 AND " +
                            "`Description` = @description;";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@description", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| description='" + key + "'");
                mCommon.eCtrl.WriteAuditRecord(aItem);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    id = GetInt(tbl.Rows[0], "CommunicationTypeID");
                }
                return id;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetCommunicationTypeByName failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetCommunicationTypeByName"));
            }
        }

        /// <summary>
        /// Get a List of Communication for the specified personid
        /// </summary>
        /// <param name="personid">personid to return the communication List for</param>
        /// <returns>List of Communication objects</returns>
        public List<Communication> GetCommunicationsListByPersonID(int personid)
        {
            List<Communication> rtn = null;
            string sql = "";
            MySqlCommand cmd = null;
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetCommunicationsListByPersonID"));

            try
            {
                step = "Build querry";
                sql = "SELECT `CommunicationID`, `PersonID`, `CommunicationTypeID`, `Created`, " +
                            "`Modified`, `IsActive`, `CommunicationCode` " +
                        "FROM `per_Communication` " +
                        "WHERE `PersonID`=@personid ";
                                //"AND `IsActive`=1 ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@PersonID", personid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                    new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, personid,
                        EZDeskDataLayer.ehr.Models.AuditAreas.Person,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| PersonID=" + personid.ToString());
                mCommon.eCtrl.WriteAuditRecord(aItem);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count > 0))
                {
                    rtn = new List<Communication>();
                    foreach (DataRow dr in tbl.Rows)
                    {
                        Communication temp = new Communication();
                        temp.CommunicationCode = dr["CommunicationCode"].ToString();
                        temp.CommunicationID = GetInt(dr, "CommunicationID");
                        temp.CommunicationType = (Communication.CommunicationsTypeEnum)Enum.Parse(typeof(Communication.CommunicationsTypeEnum), dr["CommunicationTypeID"].ToString());
                        temp.Created = GetDateTime(dr, "Created");
                        temp.IsActive = GetBool(dr, "IsActive");
                        temp.Modified = GetDateTime(dr, "Modified");
                        temp.PersonID = GetInt(dr, "PersonID");
                        rtn.Add(temp);
                    }
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetCommunicationsListByPersonID failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("PersonID", personid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetCommunicationsListByPersonID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comms"></param>
        public void UpdateCommunicationsList(List<Communication> comms, MySqlTransaction transaction)
        {
            string sql = "";
            string step = "";
            string otherData = "'";

            Trace.Enter(Trace.RtnName(mModName, "UpdateCommunicationsList"));

            try
            {
                foreach (Communication com in comms)
                {
                    step = "Build querry";
                    //Check for the item already existing
                    //Update the ones that previously existed
                    if (com.CommunicationID > 0)
                    {
                        sql = "UPDATE `per_Communication` SET " +
                                    "`CommunicationTypeID`=@commtype, " +
                                    "`IsActive`=@isactive, " +
                                    "`CommunicationCode`=@commcode " +
                                "WHERE `CommunicationID`=@commid ";
                    }

                    else
                    {
                        sql = "INSERT INTO `per_Communication` (`CommunicationTypeID`, `PersonID`, " +
                                    "`IsActive`, `CommunicationCode`) " +
                                "VALUES(@commtype, @personid, @isactive, @commcode) ";
                    }

                    MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                    cmd.Parameters.Add(new MySqlParameter("@commtype", (int)com.CommunicationType));
                    cmd.Parameters.Add(new MySqlParameter("@isactive", com.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@commcode", com.CommunicationCode));
                    if (com.CommunicationID > 0)
                    {
                        cmd.Parameters.Add(new MySqlParameter("@commid", com.CommunicationID));
                        otherData = " commid=" + com.CommunicationID.ToString();
                    }
                    else
                    {
                        cmd.Parameters.Add(new MySqlParameter("@personid", com.PersonID));
                        otherData = "personid=" + com.PersonID.ToString();
                    }

                    step = "Write data";
                    ExecuteNonQueryCmd(cmd);

                    // ----- Audit SQL call -----
                    EZDeskDataLayer.ehr.Models.AuditItem aItem =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, com.PersonID,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| commtype=" + ((int)com.CommunicationType).ToString() +
                                        " isactive=" + com.IsActive.ToString() + 
                                        " commcode=" + com.CommunicationCode +
                                        otherData);
                    mCommon.eCtrl.WriteAuditRecord(aItem);

                    if (com.CommunicationID < 1)
                    {
                        step = "Get LastInsertedID";
                        com.CommunicationID = (int)cmd.LastInsertedId;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("UpdateCommunicationsList failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("comms", comms);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateCommunicationsList"));
            }
        }

    }
}
