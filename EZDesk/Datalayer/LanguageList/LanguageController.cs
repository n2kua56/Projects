using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.LanguageList.Models;
using EZUtils;

namespace EZDeskDataLayer.LanguageList
{
    public class LanguageController : Controller
    {
        private string mModName = "LanguageControllers";

        ////public LanguageController(MySqlConnection conn)
        ////{
        ////    Conn = conn;
        ////}

        public LanguageController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "LanguageController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "LanguageController-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetLanguageID(string key)
        {
            string sql = "";
            DataTable tbl = null;
            int langaugeID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetLanguageID"));

            try
            {
                step = "Build querry";
                sql = "SELECT `ID` " +
                        "FROM `per_LanguageList` " +
                        "WHERE `IsActive`=1 AND " +
                            "Language=@language ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@language", key));

                step = "Get Data";
                tbl = GetDataTable(cmd);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    langaugeID = GetInt(tbl.Rows[0], "ID");
                }

                return langaugeID;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetLanguageID"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("key", key);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetLanguageID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLanguageList()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetLanguageList"));

            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `IsActive`, `Language`, `DisplayOrder`, `DefaultDisplay`, " +
                            "`ISO639-1`, `ISO639-2` " +
                        "FROM `per_LanguageList` " +
                        "WHERE `IsActive`=1 " +
                        "ORDER BY `DisplayOrder` ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetLanguageList"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetLanguageList"));
            }
        }

    }
}
