using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.EthnicityList.Models;
using EZUtils;

namespace EZDeskDataLayer.EthnicityList
{
    public class EthnicityController : Controller
    {
        private string mModName = "EthnicityController";

        public EthnicityController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "EthnicityController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "EthnicityController-Constructor"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetEthnicityID(string key)
        {
            string sql = "";
            DataTable tbl = null;
            int ethnicityID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetEthnicityID"));

            try
            {
                step = "Build query";
                sql = "SELECT `ID` " +
                        "FROM `per_EthnicityList` " +
                        "WHERE `Ethnicity`=@ethnicity ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@ethnicity", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    ethnicityID = GetInt(tbl.Rows[0], "ID");
                }

                return ethnicityID;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetEthnicityID failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetEthnicityID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetEthnicityList()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetEthnicityList"));

            try
            {
                step = "Build querry";
                sql = "SELECT `ID`, `Ethnicity`, `HL7_EthnicityCode`, `DisplayOrder` " +
                        "FROM `per_EthnicityList` " +
                        "ORDER BY `DisplayOrder` ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetEthnicityList failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetEthnicityList"));
            }
        }

    }
}
