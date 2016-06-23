using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.Message.Models;
using EZUtils;

namespace EZDeskDataLayer.Message
{
    public class msgCtrl : Controller
    {
        private string mModName = "msgCtrl";
        private EZDeskCommon mCommon;

        public msgCtrl(EZDeskCommon common)
        {
            mCommon = common;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void SaveMessage(msgItem item)
        {
            string sql = "";
            string step = "";
            MySqlCommand cmd = null;

            Trace.Enter(Trace.RtnName(mModName, "SaveMessage"));
            try
            {
                step = "Build querry";
                sql = "INSERT INTO msg_messges (`msgDateTime`, `msgDirection`, `msgSentBy`, " +
                                    "`msgReceivedBy`, `msgPersonID`, `msgTabID`, `msgBody` " +
                            "Values(@dte, @dir, @sent, @rcvd, @perid, @tabid, @body) ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                item.msgDateTime = DateTime.Now;
                cmd.Parameters.Add(new MySqlParameter("@dte", item.msgDateTime));
                cmd.Parameters.Add(new MySqlParameter("@dir", item.msgDirection));
                cmd.Parameters.Add(new MySqlParameter("@sent", item.msgSentBy));
                cmd.Parameters.Add(new MySqlParameter("@rcvd", item.msgReceivedBy));
                cmd.Parameters.Add(new MySqlParameter("@perid", item.msgPersonID));
                cmd.Parameters.Add(new MySqlParameter("@tabid", item.msgTabID));
                cmd.Parameters.Add(new MySqlParameter("@body", item.msgBody));

                step = "Get data";
                ExecuteNonQueryCmd(cmd);
                item.ID = Convert.ToInt32(cmd.LastInsertedId);
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("SaveMessage failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Enter(Trace.RtnName(mModName, "SaveMessage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personid"></param>
        /// <returns></returns>
        public List<msgItem> GetMessageListForPerson(int personid)
        {
            List<msgItem> rtn = new List<msgItem>();
            string sql = "";
            string step = "";
            DataTable tbl = null;
            MySqlCommand cmd = null;
            int idx = -1;
            msgItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetMessageListForPerson"));
            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `msgDateTime`, `msgDirection`, `msgSentBy`, " +
                             "`msgReceivedBy`, `msgPersonID`, `msgTabID` " +
                        "FROM msg_messges " +
                        "WHERE `msgPersonID`=@personid ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Parse Data";
                for (idx = 0; idx < tbl.Rows.Count; idx++)
                {
                    DataRow row = tbl.Rows[idx];
                    item = zMapMsgItem(row);
                    rtn.Add(item);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMessageListForPerson failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("personid", personid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetMessageListForPerson"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<msgItem> GetMessageListForSent(int ID)
        {
            List<msgItem> rtn = new List<msgItem>();
            string sql = "";
            string step = "";
            DataTable tbl = null;
            MySqlCommand cmd = null;
            int idx = -1;
            msgItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetMessageListForSent"));
            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `msgDateTime`, `msgDirection`, `msgSentBy`, " +
                             "`msgReceivedBy`, `msgPersonID`, `msgTabID` " +
                        "FROM msg_messges " +
                        "WHERE `msgSentBy`=@id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", ID));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Parse Data";
                for (idx = 0; idx < tbl.Rows.Count; idx++)
                {
                    DataRow row = tbl.Rows[idx];
                    item = zMapMsgItem(row);
                    rtn.Add(item);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMessageListForSent failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("ID", ID);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetMessageListForSent"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<msgItem> GetMessageListForRcvd(int ID)
        {
            List<msgItem> rtn = new List<msgItem>();
            string sql = "";
            string step = "";
            DataTable tbl = null;
            MySqlCommand cmd = null;
            int idx = -1;
            msgItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetMessageListForRcvd"));
            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `msgDateTime`, `msgDirection`, `msgSentBy`, " +
                             "`msgReceivedBy`, `msgPersonID`, `msgTabID` " +
                        "FROM msg_messges " +
                        "WHERE `msgReceivedBy`=@id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", ID));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Parse Data";
                for (idx = 0; idx < tbl.Rows.Count; idx++)
                {
                    DataRow row = tbl.Rows[idx];
                    item = zMapMsgItem(row);
                    rtn.Add(item);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMessageListForRcvd failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("ID", ID);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetMessageListForRcvd"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personid"></param>
        /// <param name="tabid"></param>
        /// <returns></returns>
        public List<msgItem> GetMessageListForPersonTab(int personid, int tabid)
        {
            List<msgItem> rtn = new List<msgItem>();
            string sql = "";
            string step = "";
            DataTable tbl = null;
            MySqlCommand cmd = null;
            int idx = -1;
            msgItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetMessageListForPersonTab"));
            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `msgDateTime`, `msgDirection`, `msgSentBy`, " +
                             "`msgReceivedBy`, `msgPersonID`, `msgTabID` " +
                        "FROM msg_messges " +
                        "WHERE `msgPersonID`=@personid " +
                            "AND `msgTabID`=@tabid ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));
                cmd.Parameters.Add(new MySqlParameter("@tabid", tabid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Parse Data";
                for (idx = 0; idx < tbl.Rows.Count; idx++)
                {
                    DataRow row = tbl.Rows[idx];
                    item = zMapMsgItem(row);
                    rtn.Add(item);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMessageListForRcvd failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("personid", personid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetMessageListForRcvd"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public msgItem GetFullMsgItem(int ID)
        {
            msgItem rtn = null;
            string sql = "";
            string step = "";
            DataTable tbl = null;
            MySqlCommand cmd = null;
            int idx = -1;
            msgItem item = null;

            Trace.Enter(Trace.RtnName(mModName, "GetFullMsgItem"));
            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `msgDateTime`, `msgDirection`, `msgSentBy`, " +
                             "`msgReceivedBy`, `msgPersonID`, `msgTabID`, `msgBody` " +
                        "FROM msg_messges " +
                        "WHERE `ID`=@id ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@id", ID));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Parse Data";
                if (tbl.Rows.Count > 0)
                {
                    rtn = zMapMsgItem(tbl.Rows[0]);
                    rtn.msgBody = tbl.Rows[0]["msgBody"].ToString();
                }
                
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetFullMsgItem failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("ID", ID);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetFullMsgItem"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private msgItem zMapMsgItem(DataRow row)
        {
            msgItem rtn = new msgItem();
            
            rtn.ID = Convert.ToInt32(row["ID"].ToString());
            rtn.msgDateTime = (DateTime)row["msgDateTime"];
            rtn.msgDirection = (char)row["msgDirection"];
            rtn.msgSentBy = (row["msgSentBy"] == null) ? -1 : (int)row["msgSentBy"];
            rtn.msgReceivedBy = (row["msgReceivedBy"] == null) ? -1 : (int)row["msgReceivedBy"];
            rtn.msgPersonID = (row["msgPersonID"] == null) ? -1 : (int)row["msgPersonID"];
            rtn.msgTabID = (row["msgTabID"] == null) ? -1 : (int)row["msgTabID"];
            rtn.msgBody = "";

            return rtn;
        }

    }
}
