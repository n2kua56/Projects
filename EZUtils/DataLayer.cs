using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EZUtils
{
    public class DataLayer
    {
        private string mConnectionString = "";
        private MySqlConnection mConn = null;
        private MySqlCommand mCmd = null;
        private long mLastID = -1;
        public delegate void ProgressHandler(int m, int v);
        public event ProgressHandler showProgress;
        private string mModName = "DataLayer";

        public long LastID
        {
            get { return mLastID; }
        }

        public string ConnectionString
        {
            get { return mConnectionString; }
            set { mConnectionString = value; }
        }

        public MySqlConnection Conn
        {
            get { return mConn; }
            set { mConn = value; }
        }

        public DataLayer()
        {
        }

        public DataLayer(MySqlConnection conn)
        {
            if (mConn == null)
            {
                mConn = conn;
            }
        }

        /// <summary>
        /// Connect to the MySql database server
        /// </summary>
        /// <param name="connStr"></param>
        public DataLayer(string connStr)
        {
            Trace.Enter(Trace.RtnName(mModName, "DataLayer"));

            try
            {
                mConnectionString = connStr;
                Connect();
            }

            catch (Exception ex)
            {
                EZException EZe = new EZException("Failed", ex);
                throw EZe;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DataLayer"));
            }
        }

        public void Connect()
        {
            string msg = "";

            Trace.Enter(Trace.RtnName(mModName, "Connect"), "Connection string: " + mConnectionString);
            try
            {
                mConn = new MySqlConnection(mConnectionString);
                mConn.Open();
            }

            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                if (msg.StartsWith("Access denied for user"))
                {
                    string user = "";
                    string database = "";
                    string[] parts = mConnectionString.Split('=');

                    try
                    {
                        if (parts.Length > 2)
                        {
                            user = parts[2].Split(';')[0];
                        }
                    }
                    catch { }

                    try
                    {
                        if (parts.Length > 4)
                        {
                            database = parts[3].Split(';')[0];
                        }
                    }
                    catch { }

                    msg = msg + "\n" +
                        "User id '" + user + "' may be invalid or have the wrong password " +
                        "OR database '" + database + "' is unknown.";

                }

                EZException EZex = new EZException("Failed", ex);
                EZex.Add("connStr", mConnectionString);
                EZex.Add("msg", msg);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Connect"));
            }
        }

        public static string zStringToMoneyFormat(string vals)
        {
            string rtn = "";
            double vald = 0.00;
            Trace.Enter("vals: " + vals);

            try
            {
                if (vals == "") { vals = "0.00"; }
                vald = Convert.ToDouble(vals);
                rtn = vald.ToString("###,##0.00");
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("vals", vals);
                EZex.Add("rtn", rtn);
                throw EZex;
            }

            finally
            {
                Trace.Exit("rtn: " + rtn);
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        protected DataTable zGetDataTable(string sql, Dictionary<string, Object> parms)
        {
            DataTable dt = null;
            DataSet ds = null;

            Trace.Enter(Trace.RtnName(mModName, "zGetDataTable"));

            try
            {
                ds = zGetDataSet(sql, parms);
                dt = ds.Tables[0];
                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("parms", parms);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGetDataTable"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        protected DataSet zGetDataSet(string sql, Dictionary<string, Object> parms)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet ds = new DataSet();

            Trace.Enter(Trace.RtnName(mModName, "zGetDataSet"));

            try
            {
                conn = new MySqlConnection(mConnectionString);
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                if (parms != null)
                {
                    foreach (KeyValuePair<string, Object> parm in parms)
                    {
                        cmd.Parameters.AddWithValue(parm.Key, parm.Value);
                    }
                }
                
                da = new MySqlDataAdapter(cmd);
                da.Fill(ds);

                return ds;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("parms", parms);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGetDataSet"));
            }
        }

        public int ExecuteNonQuery(string sql, Dictionary<string, Object> parms)
        {
            return zExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        protected int zExecuteNonQuery(string sql, Dictionary<string, Object> parms)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            int rtn = -1;

            Trace.Enter(Trace.RtnName(mModName, "zExecuteNonQuery"));

            try
            {
                conn = new MySqlConnection(mConnectionString);
                conn.Open();
                cmd = new MySqlCommand(sql, conn);
                if (parms != null)
                {
                    foreach (KeyValuePair<string, Object> parm in parms)
                    {
                        cmd.Parameters.AddWithValue(parm.Key, parm.Value);
                    }
                }
                rtn = cmd.ExecuteNonQuery();

                try
                {
                    if (sql.Trim().ToLower().StartsWith("insert "))
                    {
                        mLastID = cmd.LastInsertedId;
                    }
                }
                catch 
                {
                    mLastID = -1;
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("parms", parms);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zExecuteNonQuery"));
            }
        }

    }
}
