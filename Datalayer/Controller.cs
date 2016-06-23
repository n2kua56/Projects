using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer
{
    public class Controller
    {
        private string mModName = "Controller.";
        internal MySqlConnection mConn = null;

        public Controller()
        {
        }

        public void Init(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "Controller-Init"));

            try
            {
                if (mConn == null)
                {
                    mConn = conn;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("MySQL init failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Controller-Init"));
            }
        }

        public Controller(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "Controller-Connection"));

            try
            {
                if (mConn == null)
                {
                    mConn = conn;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("Controller failed", ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Controller-Connection"));
            }
        }

        /// <summary>
        /// Return the DataTable from MySQL for the sql SELECT
        /// and parameters in the MySqlCommand 'cmd' passed in.
        /// </summary>
        /// <param name="cmd">Initialized MySqlCommand</param>
        /// <returns>Retrieved DataTable</returns>
        internal DataTable GetDataTable(MySqlCommand cmd)
        {
            DataTable tbl = new DataTable();
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDataTable"));

            try
            {
                //Check to see if the connection needs to be remade
                if ((cmd.Connection.State == ConnectionState.Broken) || (cmd.Connection.State == ConnectionState.Closed))
                {
                    step = "Reopen";
                    cmd.Connection.Open();
                }

                step = "Fill table";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(tbl);
                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDataTable failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDataTable"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal bool GetBool(DataRow dr, string colName)
        {
            return ("true" == dr[colName].ToString().ToLower());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal DateTime GetDateTime(DataRow dr, string colName)
        {
            DateTime rtn;

            Trace.Enter(Trace.RtnName(mModName, "zGetDateTime"));

            try
            {
                string temp = dr[colName].ToString();
                if (temp.Trim().Length == 0) { temp = DateTime.MinValue.ToString(); }
                try { rtn = Convert.ToDateTime(temp); }
                catch { rtn = DateTime.MinValue; }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDateTime failed", ex);
                eze.Add("colName", colName);
                eze.Add("dr", dr);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGetDateTime"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <param name="defDate"></param>
        /// <returns></returns>
        internal DateTime GetDateTime(DataRow dr, string colName, string defDate)
        {
            DateTime rtn;

            Trace.Enter(Trace.RtnName(mModName, "zGetDateTime"));

            try
            {
                string temp = dr[colName].ToString();
                if (temp.Trim().Length == 0) { temp = defDate; }
                try { rtn = Convert.ToDateTime(temp); }
                catch { rtn = DateTime.MinValue; }
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetDateTime failed", ex);
                eze.Add("dr", dr);
                eze.Add("colName", colName);
                eze.Add("defDate", defDate);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGetDateTime"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        internal int GetInt(DataRow dr, string colName)
        {
            return Convert.ToInt32(dr[colName].ToString());
        }

        /// <summary>
        /// Return a string value from the passed DataTable.
        /// </summary>
        /// <param name="tbl">DataTable with ONE row to be extracted from</param>
        /// <param name="fldName">Field name to retrieve from specified table</param>
        /// <returns></returns>
        internal string GetStringValue(DataTable tbl, string fldName)
        {
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "GetStringValue"));

            try
            {
                if (tbl.Rows.Count == 1)
                {
                    rtn = tbl.Rows[0][fldName].ToString();
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetStringValue failed", ex);
                eze.Add("tbl.Rows.Count", tbl.Rows.Count);
                eze.Add("fldName", fldName);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetStringValue"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmc"></param>
        internal int ExecuteNonQueryCmd(MySqlCommand cmd)
        {
            string step = "";
            int rc = -1;

            Trace.Enter(Trace.RtnName(mModName, "ExecuteNonQueryCmd"));

            try
            {
                //Check to see if the connection needs to be remade
                if ((cmd.Connection.State == ConnectionState.Broken) || (cmd.Connection.State == ConnectionState.Closed))
                {
                    step = "Reopen";
                    cmd.Connection.Open();
                }

                step = "Execute the command";
                rc = cmd.ExecuteNonQuery();
                return rc;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("ExecuteNonQueryCmd failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ExecuteNonQueryCmd"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        internal object ExecuteScalar(MySqlCommand cmd)
        {
            string step = "";
            object rtn = null;

            Trace.Enter(Trace.RtnName(mModName, "ExecuteScalar"));

            try 
            {
                //Check to see if the connection needs to be remade
                if ((cmd.Connection.State == ConnectionState.Broken) || (cmd.Connection.State == ConnectionState.Closed))
                {
                    step = "Reopen";
                    cmd.Connection.Open();
                }

                step = "Execute the command";
                rtn = cmd.ExecuteScalar();
                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("ExecuteScalar failed", ex);
                eze.Add("step", step);
                throw eze;
            }

            finally 
            {
                Trace.Exit(Trace.RtnName(mModName, "ExecuteScalar"));
            }
        }

    }
}
