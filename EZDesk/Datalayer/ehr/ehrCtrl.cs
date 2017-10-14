using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EZDeskDataLayer.Address.Models;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer.ehr
{
    public class ehrCtrl : Controller
    {
        private string mModName = "ehrCtrl";

        ////public ehrCtrl(MySqlConnection conn)
        ////{
        ////    mConn = conn;
        ////}

        public ehrCtrl(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "ehrCtrl-Constructor"));
            
            Init(conn);
            
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            Trace.Exit(Trace.RtnName(mModName, "ehrCtrl-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetProperty(string key)
        {
            string rtn = "";
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetProperty"));

            try
            {
                step = "Build querry";
                sql = "SELECT `PropertyValue` " +
                        "FROM `ehr_AvailableProperties` " +
                        "WHERE `PropertyName`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    rtn = tbl.Rows[0]["PropertyValue"].ToString();
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetProperty failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetProperty"));
            }
        }
        
        #region tabs

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public Datalayer.ehr.Models.tabItem GetTab(int tabId)
        {
            Datalayer.ehr.Models.tabItem rtn =
                new Datalayer.ehr.Models.tabItem();
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetTab"));

            try
            {
                step = "Build querry";
                sql = "SELECT `tabId`, `tabName`, `tabDesc` " +
                        "FROM `ehr_tabs` " +
                        "WHERE `tabId`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@key", tabId));

                step = "Get data";
                tbl = GetDataTable(cmd);
                if (tbl.Rows.Count > 0)
                {
                    rtn.TabId = Convert.ToInt32(tbl.Rows[0]["tabId"].ToString());
                    rtn.TabName = tbl.Rows[0]["tabName"].ToString();
                    rtn.TabDesc = tbl.Rows[0]["tabDesc"].ToString();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                EZException eze = new EZException("GetTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tabId", tabId);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public void WriteTab(Datalayer.ehr.Models.tabItem tab)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteTab"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `tabId` " +
                        "FROM `ehr_tabs` " +
                        "WHERE `tabName`=@name ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@name", tab.TabName));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ? 
                    Convert.ToInt32(tbl.Rows[0]["tabId"].ToString()) : -2;

                if ((tbl.Rows.Count == 0) || (tab.TabId == id))
                {
                    if (tab.TabId < 0)
                    {
                        step = "Insert tab - Build querry";
                        sql = "INSERT INTO `ehr_tabs`(`tabName`, `tabDesc`, `IsActive`, `DisplaySeq`) " +
                                "VALUES(@name, @desc, @active, @seq) ";
                        cmd = new MySqlCommand(sql, mConn);
                    }

                    else
                    {
                        step = "Update Tab - Build Querry";
                        sql = "UPDATE `ehr_tabs` SET " +
                                    "`tabName`=@name, " +
                                    "`tabDesc`=@desc, " +
                                    "`IsActive`=@active, " +
                                    "`DisplaySeq`=@seq, " +
                                "WHERE `tabId`=@tabid ";
                        cmd = new MySqlCommand(sql, mConn);
                        cmd.Parameters.Add(new MySqlParameter("@tabid", tab.TabId));
                    }
                    
                    cmd.Parameters.Add(new MySqlParameter("@name", tab.TabName));
                    cmd.Parameters.Add(new MySqlParameter("@desc", tab.TabDesc));
                    cmd.Parameters.Add(new MySqlParameter("@active", tab.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@seq", tab.DisplaySeq));

                    step = "Put data";
                    cmd.ExecuteNonQuery();

                    if (tab.TabId < 0)
                    {
                        tab.TabId = (int)cmd.LastInsertedId;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteTab failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tab", tab);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteTab"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTabs()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAllTabs"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `tabId`, `tabName`, `tabDesc`, " +
                            "`IsActive`, `DisplaySeq` " +
                        "FROM `ehr_tabs` " +
                        "ORDER BY `DisplaySeq`, `tabName` ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllTabs failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllTabs"));
            }
        }

        #endregion

        #region Drawers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public Datalayer.ehr.Models.DrawerItem GetDrawer(int drawerId)
        {
            Datalayer.ehr.Models.DrawerItem rtn =
                new Datalayer.ehr.Models.DrawerItem();
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDrawer"));

            try
            {
                step = "Build querry";
                sql = "SELECT `Id`, `drawerName`, `drawerDesc` " +
                        "`Seq`, `IsActive`, `Created` " +
                        "FROM `ehr_drawers` " +
                        "WHERE `Id`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@key", drawerId));

                step = "Get data";
                tbl = GetDataTable(cmd);
                if (tbl.Rows.Count > 0)
                {
                    rtn.DrawerId = Convert.ToInt32(tbl.Rows[0]["Id"].ToString());
                    rtn.DrawerName = tbl.Rows[0]["drawerName"].ToString();
                    rtn.DrawerDesc = tbl.Rows[0]["drawerDesc"].ToString();
                }
                return rtn;
            }
            catch (Exception ex)
            {
                ex.Data.Add("Rtn", Trace.RtnName(mModName, "GetDrawer"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("drawerId", drawerId);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDrawer"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public void WriteDrawer(Datalayer.ehr.Models.DrawerItem drawer)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "WriteDrawer"));

            try
            {
                step = "Find Dups - Build Querry";
                sql = "SELECT `Id` " +
                        "FROM `ehr_drawers` " +
                        "WHERE `drawerName`=@name ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@name", drawer.DrawerName));

                step = "Get data";
                tbl = GetDataTable(cmd);
                int id = (tbl.Rows.Count == 1) ?
                    Convert.ToInt32(tbl.Rows[0]["Id"].ToString()) : -2;

                if ((tbl.Rows.Count == 0) || (drawer.DrawerId == id))
                {
                    if (drawer.DrawerId == -1)
                    {
                        step = "Build Insert";
                        sql = "INSERT INTO `ehr_drawers`(`drawerName`, `drawerDesc`, " +
                                    "`IsActive`, `Seq`) " +
                                "VALUES(@name, @desc, @active, @seq) ";
                        cmd = new MySqlCommand(sql, mConn);
                    }

                    else
                    {
                        step = "Build Update";
                        sql = "UPDATE `ehr_drawers` SET " +
                                    "`DrawerName`=@name, " +
                                    "`DrawerDesc`=@desc, " +
                                    "`IsActive`=@active, " +
                                    "`Seq`=@seq, " +
                                "WHERE `Id`=@drawerid ";
                        cmd = new MySqlCommand(sql, mConn);
                        cmd.Parameters.Add(new MySqlParameter("@drawerid", drawer.DrawerId));
                    }

                    cmd.Parameters.Add(new MySqlParameter("@name", drawer.DrawerName));
                    cmd.Parameters.Add(new MySqlParameter("@desc", drawer.DrawerDesc));
                    cmd.Parameters.Add(new MySqlParameter("@active", drawer.IsActive));
                    cmd.Parameters.Add(new MySqlParameter("@seq", drawer.Seq));

                    step = "Put data";
                    cmd.ExecuteNonQuery();

                    if (drawer.DrawerId < 0)
                    {
                        drawer.DrawerId = (int)cmd.LastInsertedId;
                    }
                }            
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("WriteDrawer failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("tab", drawer);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "WriteDrawer"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDrawers()
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAllDrawers"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `Id`, `drawerName`, `drawerDesc`, " +
                            "`IsActive`, `Seq` " +
                        "FROM `ehr_drawers` " +
                        "ORDER BY `Seq`, `drawerName` ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetAllDrawers failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAllDrawers"));
            }
        }

        #endregion

    }
}