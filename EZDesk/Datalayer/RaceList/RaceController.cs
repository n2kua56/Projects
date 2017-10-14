using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.RaceList.Models;
using EZUtils;

namespace EZDeskDataLayer.RaceList
{
    public class RaceController : Controller
    {
        private string mModName = "RaceController";

        ////public RaceController(MySqlConnection conn)
        ////{
        ////    mConn = conn;
        ////}

        public RaceController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "RaceController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "RaceController-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetRaceListID(string key)
        {
            string sql = "";
            DataTable tbl = null;
            int raceID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetRaceListID"));

            try
            {
                step = "Build querry";
                sql = "SELECT `ID` " +
                        "FROM `per_RaceList` " +
                        "WHERE `Race`=@Race ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@Race", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    raceID = GetInt(tbl.Rows[0], "ID");
                }

                return raceID;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetRaceListID"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("key", key);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetRaceListID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetRaceList()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetRaceList"));

            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `Race`, `HL7_RaceCode`, `DisplayOrder` " +
                        "FROM `per_RaceList` " +
                        "ORDER BY `DisplayOrder` ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetRaceList"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetRaceList"));
            }
        }

    }
}
