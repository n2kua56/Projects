using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer.User.Models;
using EZUtils;

namespace EZDeskDataLayer.Documents
{
    public class DocumentsController : Controller
    {
        private string mModName = "DocumentsController";

        ////public DocumentsController(MySqlConnection Conn)
        ////{
        ////    mConn = Conn;
        ////}

        public DocumentsController(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "DocumentsController-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "DocumentsController-Constructor"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public DataTable GetAvailableDrawers(bool alpha)
        {
            DataTable dt = null;
            List<SetupUserList> rtn = new List<SetupUserList>();
            string step = "";
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAvailableDrawers"));

            try
            {
                step = "Build querry";
                sql = "SELECT `TabsID`, `Name` " +
                         "FROM `doc_Tabs` " +
                         "WHERE IsActive=1 ";
                if (alpha) { sql += "ORDER BY `Name`"; }
                else { sql += "ORDER BY `SortSeq` "; }
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                //Get the requested data into a databable.
                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetAvailableDrawers"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAvailableDrawers"));
            }
        }

    }
}
