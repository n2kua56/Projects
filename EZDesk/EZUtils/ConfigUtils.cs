using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EZUtils
{
    public class ConfigUtils
    {
        private static System.Configuration.SettingsPropertyCollection mProps;

        public static string GetConnectionString(System.Configuration.SettingsPropertyCollection props)
        {
            Trace.Enter("ConfigUtils.GetConnectionString");
            
            mProps = props;

            string rtn = MiscUtils.Config("connect", props);
            rtn = MiscUtils.Config(rtn, props);
            rtn = rtn.Replace("{serverAddress}", MiscUtils.Config("serverAddress", props));
            rtn = rtn.Replace("{db}", MiscUtils.Config("db", props));
            rtn = rtn.Replace("{userid}", MiscUtils.Config("userid", props));
            rtn = rtn.Replace("{password}", MiscUtils.Config("password", props));

            Trace.Exit("ConfigUtils.GetConnectionString", rtn);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProperty(string key)
        {
            Trace.Enter("Key: " + key);

            SqlConnection conn = new SqlConnection(GetConnectionString(mProps));
            string SQL =
                "SELECT PropertyValue " +
                    "FROM AvailableProperties " +
                    "WHERE PropertyName = '" + key.Trim().ToUpper() + "' ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(SQL, conn);
            string rtn = null;
            try
            {
                rtn = cmd.ExecuteScalar().ToString();
            }
            catch { }

            Trace.Exit("rtn: " + rtn);
            return rtn;
        }

        public static void SetProperty(string key, string value)
        {
            Trace.Enter("Key: " + key + "=" + value);

            SqlConnection conn = new SqlConnection(GetConnectionString(mProps));

            string SQL = "UPDATE AvailableProperties " +
                    "SET PropertyValue = '" + value.Trim() + "' " +
                    "WHERE PropertyName = '" + key.Trim().ToUpper() + "' ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(SQL, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
