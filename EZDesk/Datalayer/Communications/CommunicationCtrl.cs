using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EZDeskDataLayer.Communications.Models;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer.Communications
{
    public class CommunicationCtrl : Controller
    {
        private string mModName = "CommunicationCtrl";

        ////public CommunicationCtrl(MySqlConnection conn)
        ////{
        ////    mConn = conn;
        ////}

        public CommunicationCtrl(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "CommunicationCtrl-Constructor"));
            Init(conn);
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
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@description", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    id = GetInt(tbl.Rows[0], "CommunicationTypeID");
                }
                return id;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetCommunicationTypeByName"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("key", key);
                throw ex;
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
                cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@PersonID", personid));

                step = "Get data";
                tbl = GetDataTable(cmd);

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
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetCommunicationsListByPersonID"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("PersonID", personid);
                throw ex;
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

                    MySqlCommand cmd = new MySqlCommand(sql, mConn);
                    cmd.Parameters.Add(new MySqlParameter("@commtype", (int)com.CommunicationType));
                    cmd.Parameters.Add(new MySqlParameter("@isactive", com.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@commcode", com.CommunicationCode));
                    if (com.CommunicationID > 0)
                    {
                        cmd.Parameters.Add(new MySqlParameter("@commid", com.CommunicationID));
                    }
                    else
                    {
                        cmd.Parameters.Add(new MySqlParameter("@personid", com.PersonID));
                    }

                    step = "Write data";
                    cmd.ExecuteNonQuery();
                    if (com.CommunicationID < 1)
                    {
                        step = "Get LastInsertedID";
                        com.CommunicationID = (int)cmd.LastInsertedId;
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "UpdateCommunicationsList"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("comms", comms);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateCommunicationsList"));
            }
        }

    }
}
