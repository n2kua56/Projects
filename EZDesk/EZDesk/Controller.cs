using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.User.Models;
using System.Diagnostics;

namespace EZDesk
{
    public class Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal DataTable zGetDataTable(MySqlCommand cmd)
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal bool zGetBool(DataRow dr, string colName)
        {
            return ("true" == dr[colName].ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal DateTime zGetDateTime(DataRow dr, string colName)
        {
            DateTime rtn;
            string temp = dr[colName].ToString();
            if (temp.Trim().Length == 0) { temp = DateTime.MinValue.ToString(); }
            try { rtn = Convert.ToDateTime(temp); }
            catch { rtn = DateTime.MinValue; }
            return rtn;
        }

        internal DateTime zGetDateTime(DataRow dr, string colName, string defDate)
        {
            DateTime rtn;
            string temp = dr[colName].ToString();
            if (temp.Trim().Length == 0) { temp = defDate; }
            try { rtn = Convert.ToDateTime(temp); }
            catch { rtn = DateTime.MinValue; }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal int zGetInt(DataRow dr, string colName)
        {
            return Convert.ToInt32(dr[colName].ToString());
        }

    }
}
