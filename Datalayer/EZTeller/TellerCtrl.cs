using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.Person.Models;
using EZDeskDataLayer.User.Models;
using EZDeskDataLayer.Communications;
using EZDeskDataLayer.Address;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;
using pModels = EZDeskDataLayer.Person.Models;
using EZUtils;

namespace EZDeskDataLayer.EZTeller
{
    public class TellerCtrl : Controller
    {
        private string mModName = "TellerCtrl";

        private EZDeskCommon mCommon = null;
        public TellerCtrl(EZDeskCommon common)
        {
            Trace.Enter(Trace.RtnName(mModName, "TellerCtrl-Constructor"));

            mCommon = common;

            Trace.Exit(Trace.RtnName(mModName, "TellerCtrl-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetSummaryData(DateTime runDte, string batch)
        {
            DataTable tbl = null;
            string sql = "";
            string dte = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetSummaryData"));

            try
            {
                dte = runDte.ToString("yyyy-MM-dd");

                sql = "SELECT cntrbType, SUM(cntrbGeneral) AS General, " +
                             "SUM(cntrbBuilding) AS Building, " +
                             "SUM(cntrbMissions) AS Missions, " +
                             "SUM(cntrbDesignated) AS Designated, " +
                             "(SUM(cntrbGeneral)+SUM(cntrbBuilding)+" +
                                "SUM(cntrbMissions)+SUM(cntrbDesignated)) AS Total, " +
                             "COUNT(*) As itemCount " +
                        "FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " + 
                             "AND cntrbBatch=@batch " + 
                             "AND cntrbActive='Y' " +
                        "GROUP BY cntrbType " +

                      "UNION " +
                      "SELECT 11 AS cntrbType, SUM(cntrbGeneral) AS General, " +
                             "SUM(cntrbBuilding) AS Building, " +
                             "SUM(cntrbMissions) AS Missions, " +
                             "SUM(cntrbDesignated) AS Designated, " +
                             "(SUM(cntrbGeneral)+SUM(cntrbBuilding)+" +
                                "SUM(cntrbMissions)+SUM(cntrbDesignated)) AS Total, " +
                             "0 AS itemCount " +
                        "FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " + 
                             "AND cntrbBatch=@batch " +
                             "AND cntrbActive='Y' " +
                             "AND cntrbType IN (0,1,2) " +

                      "UNION " +
                      "SELECT 12 AS cntrbType, SUM(cntrbGeneral) AS General, " +
                             "SUM(cntrbBuilding) AS Building, " +
                             "SUM(cntrbMissions) AS Missions, " +
                             "SUM(cntrbDesignated) AS Designated, " +
                             "(SUM(cntrbGeneral)+SUM(cntrbBuilding)+" +
                                "SUM(cntrbMissions)+SUM(cntrbDesignated)) AS Total, " +
                             "0 AS itemCount " +
                        "FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " + 
                             "AND cntrbBatch=@batch " + 
                             "AND cntrbActive='Y' " +
                             "AND cntrbType IN (3,4) " +

                      "UNION " +
                      "SELECT 13 AS cntrbType, SUM(cntrbGeneral) AS General, " +
                             "SUM(cntrbBuilding) AS Building, " +
                             "SUM(cntrbMissions) AS Missions, " +
                             "SUM(cntrbDesignated) AS Designated, " +
                             "(SUM(cntrbGeneral)+SUM(cntrbBuilding)+" +
                                "SUM(cntrbMissions)+SUM(cntrbDesignated)) AS Total, " +
                             "0 AS itemCount " +
                        "FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " + 
                             "AND cntrbBatch=@batch " +
                             "AND cntrbActive='Y'; ";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMatchingByName failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("dte", dte);
                eze.Add("batch", batch);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetSummaryData"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runDte"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public DataTable GetPeopleData(DateTime runDte, string sort)
        {
            DataTable dt = new DataTable();
            string sql = "";
            string step = "";
            string val = "";
            int max = -1;
            int idx = -1;

            Trace.Enter(Trace.RtnName(mModName, "GetPeopleData"));

            try
            {
                step = "Build sql";
                string sDate = runDte.ToString("yyyy");
                string eDate = sDate + "-12-31 23:59:59";
                sDate += "-01-01 00:00:00";

                // Need the maximum number of matching rows that will be returned
                // so that the progress bar will work.
                sql = "SELECT COUNT(*) " +
                        "FROM per_person AS p " +
                        "WHERE p.PersonTypeID = 1 " +
                            "AND IsActive=1 ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                max = Convert.ToInt32(ExecuteScalar(cmd).ToString());

                // Build the SELECT that will return the list of people matching
                // the "sort" parameters.  At least on a virtual machine it was
                // necessary to set the timeout to 180 secods.
                sql = "SELECT p.PersonID AS pKey, e.EnvelopeNum AS ENV, " +
                            "CONCAT(p.LastName, ', ', p.FirstName) AS NAME, " +
                            "CONCAT(a.Address1, '; ', a.Address2) AS ADDRESS, " +
                            "a.City AS CITY, a.State AS STATE, a.Zip AS ZIP, c.TOTAL " +
                        "FROM per_person AS p " +
                            "LEFT JOIN per_address AS a ON (p.PersonID=a.PersonID) " +
                            "LEFT JOIN ezteller_envelopes AS e ON (p.PersonID=e.PersonID) " +
                            "LEFT JOIN ( " +
                                "SELECT cntrbPplKey AS PersonID, SUM(cntrbGeneral + cntrbBuilding + cntrbMissions + cntrbDesignated) AS TOTAL " +
                                    "FROM ezteller_contribution " +
                                    "WHERE cntrbDate > @sDate " +
                                        "AND cntrbDate < @eDate " +
                                    "GROUP BY cntrbPplKey) AS c ON (p.PersonID=c.PersonID) " +
                        "WHERE p.PersonTypeID=1 " +
                            "AND p.IsActive=1 " +
                        "ORDER BY p.LastName, p.FirstName ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@sDate", sDate));
                cmd.Parameters.Add(new MySqlParameter("@eDate", eDate));

                step = "Get data";
                dt = GetDataTable(cmd);

                step = "Format TOTAL";
                ////idx = 0;
                ////for (idx=0; idx < dt.Rows.Count; idx++)
                ////{
                ////    val = dt.Rows[idx]["TOTAL"].ToString();
                ////    if ((val == null) || (val.Length == 0))
                ////    {
                ////        val = "0.00";
                ////    }
                ////    dt.Rows[idx]["TOTAL"] = zStringToMoneyFormat(val);
                ////}

                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                EZex.Add("runDte", runDte);
                EZex.Add("sort", sort);
                EZex.Add("val", val);
                EZex.Add("idx", idx);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetPeopleData"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vals"></param>
        /// <returns></returns>
        private static string zStringToMoneyFormat(string vals)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        public DataTable GetReconcileData(DateTime runDte, int batch)
        {
            DataTable tbl = null;
            string sql = "";
            string dte = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetReconcileData"));

            try
            {
                step = "Build SQL";
                dte = runDte.ToString("yyyy-MM-dd");

                sql = "SELECT c.cntrbSeq AS Seq, " +
                             "CONCAT(p.pplLastName, ', ', p.pplFirstName) AS Name, " +
                             "c.cntrbCleared AS Clr, " +
                             "(c.cntrbGeneral+c.cntrbBuilding+c.cntrbMissions+" +
                                "c.cntrbDesignated) AS Total, " +
                             "c.cntrbComments AS Comments " +
                        "FROM ezteller_contribution AS c " +
                            "JOIN per_person AS p ON (c.cntrbPplKey=p.pplKey) " +
                        "WHERE cntrbDate=@dte " + 
                              "AND cntrbBatch=@batch " + 
                              "AND cntrbActive='Y' " +
                              "AND cntrbType IN(3, 4) ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMatchingByName failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("dte", dte);
                eze.Add("batch", batch);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetReconcileData"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runDte"></param>
        /// <param name="runBatch"></param>
        /// <returns></returns>
        public int ResetBatch(DateTime runDte, int runBatch)
        {
            string sql = "";
            MySqlCommand cmd = null;
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "ResetBatch"));

            try
            {
                step = "Build SQL";
                sql = "UPDATE ezteller_contribution " +
                        "SET cntrbCleared=0 " +
                            "WHERE cntrbDate=@dte " +
                                "AND cntrbBatch=@batch ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", runDte.ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new MySqlParameter("@batch", runBatch));

                step = "Update data";
                ExecuteNonQueryCmd(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("runDte", runDte);
                EZex.Add("runBatch", runBatch);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ResetBatch"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="countDate"></param>
        /// <param name="countBatchNo"></param>
        /// <param name="Clr"></param>
        public int UpdateReconcileCleared(int seq, DateTime countDate, int countBatchNo, string Clr)
        {
            string sql = "";
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "UpdateReconcileCleared"));

            try
            {
                step = "Build SQL";
                sql = "UPDATE ezteller_contribution " +
                        "SET cntrbCleared=@clr " +
                        "WHERE cntrbDate=@dte " +
                            "AND cntrbBatch=@batch" +
                            "AND cntrbSeq=@seq " + seq.ToString() + " ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", countDate.ToString("yyyy-MM-dd")));
                cmd.Parameters.Add(new MySqlParameter("@clr", Clr));
                cmd.Parameters.Add(new MySqlParameter("@batch", countBatchNo));
                cmd.Parameters.Add(new MySqlParameter("@seq", seq));

                step = "Update data";
                rtn = ExecuteNonQueryCmd(cmd);
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("step", step);
                EZex.Add("seq", seq);
                EZex.Add("countDate", countDate);
                EZex.Add("countBatchNo", countBatchNo);
                EZex.Add("Clr", Clr);
                EZex.Add("sql", sql);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateReconcileCleared"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        public DataTable GetDetailData(DateTime runDte, string batch)
        {
            DataTable rtn = null;
            string sql = "";
            string dte = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetDetailData"));

            try
            {
                step = "Build SQL";
                sql = "SELECT c.cntrbSeq AS Seq, e.EnvelopeNum AS Env, CONCAT(p.LastName, ', ', p.FirstName) AS Name, " +
                            "IF(c.cntrbType='3'||c.cntrbType='4', 'Y', 'N') AS Ck, c.cntrbGeneral AS General, " +
                            "c.cntrbBuilding AS Building, c.cntrbMissions AS Missions, c.cntrbDesignated AS Designated, " +
                            "(c.cntrbGeneral+c.cntrbBuilding+c.cntrbMissions+c.cntrbDesignated) AS Total, " +
                            "c.cntrbComments AS Comments " +
                        "FROM ezteller_contribution AS c " +
                            "JOIN per_person AS p ON (c.cntrbPplKey=p.PersonID) " +
                            "LEFT JOIN ezteller_envelopes AS e ON (c.cntrbPplKey=e.PersonID) " +
                        "WHERE DATE(cntrbDate)=@dte " +
                            "AND cntrbBatch=@batch " +
                            "AND cntrbActive=1 " +
                      "UNION " +
                      "SELECT 9999 AS Seq, 0 AS Env, ' TOTAL' AS Name, 'N' AS Ck, " +
                            "SUM(c.cntrbGeneral) AS General, SUM(c.cntrbBuilding) AS Building, " +
                            "SUM(c.cntrbMissions) AS Missions, SUM(c.cntrbDesignated) AS Designated, " +
                            "SUM(c.cntrbGeneral+c.cntrbBuilding+c.cntrbMissions+c.cntrbDesignated) AS Total, " +
                            "'' AS Comments " +
                        "FROM ezteller_contribution AS c " +
                        "WHERE DATE(cntrbDate)=@dte " +
                            "AND cntrbBatch=@batch " +
                            "AND cntrbActive=1 " +
                        "ORDER BY Seq; ";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", runDte.Date));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));
                
                step = "Get Data";
                rtn = GetDataTable(cmd);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("rtn", rtn);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetDetailData"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="batch"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        public int DeleteContribution(string dte, string batch, string seq)
        {
            string sql = "";
            int rtn = -1;
            MySqlCommand cmd = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "DeleteContribution"));

            try
            {
                step = "Build SQL";
                sql = "DELETE FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " +       //dte + "' " +
                            "AND cntrbBatch=@batch " +  //batch + "' " +
                            "AND cntrbSeq=@seq ";       //+ seq + "' ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));
                cmd.Parameters.Add(new MySqlParameter("@seq", seq));

                step = "Delete row";
                rtn = ExecuteNonQueryCmd(cmd);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("dte", dte);
                EZex.Add("batch", batch);
                EZex.Add("seq", seq);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteContribution"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> getListOfYears()
        {
            string sql =  "";
            string step = "";
            List<String> rtn = new List<string>();
            DataTable dt;

            Trace.Enter(Trace.RtnName(mModName, "getListOfYears"));

            try
            {
                step = "Build SQL";
                sql = "SELECT Year(cntrbDate) AS cYear " +
                        "FROM ezteller_contribution " +
                        "GROUP BY YEAR(cntrbDate) " +
                        "ORDER BY cYear ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Update data";
                dt = GetDataTable(cmd);

                step = "Build List<String>";
                for (int idx=0; idx < dt.Rows.Count; idx++)
                {
                    rtn.Add(dt.Rows[idx]["cYear"].ToString());
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "getListOfYears"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<String> getListOfMonths(string year)
        {
            string sql = "";
            List<String> rtn = new List<string>();
            string step = "";
            DataTable dt = null;
            int idx = -1;

            Trace.Enter(Trace.RtnName(mModName, "getListOfYears"));

            try
            {
                sql = "SELECT MONTH(cntrbDate) AS cMonth " +
                        "FROM ezteller_contribution " +
                        "WHERE YEAR(cntrbDate)=@year " +
                        "GROUP BY MONTH(cntrbDate) " +
                        "ORDER BY cMonth ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@year", year));

                step = "Update data";
                dt = GetDataTable(cmd);

                step = "Build List<String>";
                for (idx = 0; idx < dt.Rows.Count; idx++)
                {
                    rtn.Add(dt.Rows[idx]["cMonth"].ToString());
                }
                
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                EZex.Add("idx", idx);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "getListOfYears"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataTable GetCountData(int year, int month)
        {
            string sql = "";
            string step = "";
            DataTable rtn = null;

            Trace.Enter(Trace.RtnName(mModName, "GetCountData"));

            try
            {
                step = "Build SQL";
                sql = "SELECT Date(cntrbDate) AS Date, cntrbBatch AS Batch, Count(*) AS Entries, " +
                            "Sum(cntrbGeneral)+Sum(cntrbBuilding)+Sum(cntrbMissions) + Sum(cntrbDesignated) AS Total " +
                        "FROM ezteller_contribution " +
                        "WHERE YEAR(cntrbDate) = @year " +
                            "AND MONTH(cntrbDate) = @month " +
                        "GROUP BY Date(cntrbDate), cntrbBatch ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@year", year));
                cmd.Parameters.Add(new MySqlParameter("@month", month));

                step = "Get data";
                rtn = GetDataTable(cmd);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException(Trace.RtnName(mModName, "GetCountData") + " Failed", ex);
                EZex.Add("step", step);
                EZex.Add("sql", sql);
                EZex.Add("year", year);
                EZex.Add("month", month);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetCountData"));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Year1"></param>
        /// <param name="Year2"></param>
        /// <returns></returns>
        public DataTable GetQuarterlyGiving(int Year1, int Year2)
        {
            DataTable dt = null;
            DataTable raw = null;
            string sql = "";
            string title1 = "";
            string title2 = "";
            int quarters = -1;
            int qtr = -1;
            double amt = 0.00;
            int drYear = 0;
            string step = "";
            int idx = -1;
            DataRow row = null;

            Trace.Enter(Trace.RtnName(mModName, "GetQuarterlyGiving"));

            try
            {
                title1 = Year1.ToString();
                title2 = Year2.ToString();

                if (Year1 != Year2) { quarters = 2; }
                else { quarters = 1; }

                step = "Build the return datatable";
                dt = new DataTable();
                dt.Columns.Add("Qtr", System.Type.GetType("System.String"));
                dt.Columns.Add(title1, System.Type.GetType("System.String"));
                if (quarters == 2)
                {
                    dt.Columns.Add(title2, System.Type.GetType("System.String"));
                }
                dt.Rows.Add(zGenerateQuarter(dt.NewRow(), "1", quarters, title1, title2));
                dt.Rows.Add(zGenerateQuarter(dt.NewRow(), "2", quarters, title1, title2));
                dt.Rows.Add(zGenerateQuarter(dt.NewRow(), "3", quarters, title1, title2));
                dt.Rows.Add(zGenerateQuarter(dt.NewRow(), "4", quarters, title1, title2));
                dt.Rows.Add(zGenerateQuarter(dt.NewRow(), "Total", quarters, title1, title2));

                step = "Build sql";
                sql = "SELECT Year(cntrbDate) AS `Year`, Quarter(cntrbDate) AS `Quarter`, " +
                            "SUM(cntrbGeneral+cntrbBuilding+cntrbMissions+cntrbDesignated) AS `Total` " +
                        "FROM ezteller_contribution " +
                        "WHERE Year(cntrbDate) IN (@y1, @y2) " +
                            "AND cntrbActive='Y' " +
                        "GROUP BY Year(cntrbDate), Quarter(cntrbDate) " +
                        "ORDER BY Year(cntrbDate), Quarter(cntrbDate) ";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@y1", Year1));
                cmd.Parameters.Add(new MySqlParameter("@y1", Year2));

                step = "Get data";
                raw = GetDataTable(cmd);

                for (idx = 0; idx < raw.Rows.Count; idx++)
                {
                    row = raw.Rows[idx];
                    qtr = Convert.ToInt32(row["Quarter"].ToString().Trim());
                    drYear = Convert.ToInt32(row["Year"].ToString().Trim());
                    amt = Convert.ToDouble(row["Total"].ToString().Trim());

                    if (drYear == Year1)
                    {
                        dt.Rows[qtr - 1][title1] = amt.ToString("##,##0.00");
                        dt.Rows[qtr - 1].AcceptChanges();
                    }

                    else
                    {
                        if (quarters == 2)
                        {
                            dt.Rows[qtr - 1][title2] = amt.ToString("##,##0.00");
                            dt.Rows[qtr - 1].AcceptChanges();
                        }
                    }
                }

                dt = zGivingSummaryTotal(dt, title1, title2, quarters);

                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("Year1", Year1);
                EZex.Add("Year2", Year2);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetQuarterlyGiving"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="qtr"></param>
        /// <param name="quarters"></param>
        /// <param name="title1"></param>
        /// <param name="title2"></param>
        /// <returns></returns>
        private DataRow zGenerateQuarter(DataRow dr, string qtr, int quarters, string title1, string title2)
        {
            Trace.Enter(Trace.RtnName(mModName, "zGenerateQuarter"));

            try
            {
                try { dr["Qtr"] = qtr; }
                catch { dr["Month"] = qtr; }
                dr[title1] = "0.00";
                if (quarters == 2) { dr[title2] = "0.00"; }
                return dr;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("dr", dr);
                EZex.Add("qtr", qtr);
                EZex.Add("quarters", quarters);
                EZex.Add("title1", title1);
                EZex.Add("title2", title2);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGenerateQuarter"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="title1"></param>
        /// <param name="title2"></param>
        /// <param name="quarters"></param>
        /// <returns></returns>
        private DataTable zGivingSummaryTotal(DataTable dt, string title1,
                        string title2, int quarters)
        {
            int idx = -1;
            Double amt = 0.00;
            Double amt2 = 0.00;

            Trace.Enter(Trace.RtnName(mModName, "zGivingSummaryTotal"));

            try
            {
                for (idx = 0; idx < dt.Rows.Count - 1; idx++)
                {
                    amt += Convert.ToDouble(dt.Rows[idx][1].ToString());
                    if (quarters == 2)
                    {
                        amt2 += Convert.ToDouble(dt.Rows[idx][2].ToString());
                    }
                }
                idx = dt.Rows.Count - 1;
                dt.Rows[idx][title1] = amt.ToString("##,##0.00");
                dt.Rows[idx].AcceptChanges();
                if (quarters == 2)
                {
                    dt.Rows[idx][title2] = amt2.ToString("##,##0.00");
                    dt.Rows[idx].AcceptChanges();
                }
                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zGivingSummaryTotal"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DeleteCount(string dte, string batch)
        {
            string sql = "";
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "DeleteCount"));

            try
            {
                step = "Build SQL";
                sql = "DELETE FROM ezteller_contribution " +
                        "WHERE cntrbDate=@dte " +
                            "AND cntrbBatch=@batch ";

                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));

                step = "Delete Data";
                rtn = ExecuteNonQueryCmd(cmd);

                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("dte", dte);
                EZex.Add("batch", batch);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteCount"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="batch"></param>
        /// <param name="PplKey"></param>
        /// <param name="General"></param>
        /// <param name="Building"></param>
        /// <param name="Missions"></param>
        /// <param name="Designated"></param>
        /// <param name="typ"></param>
        /// <param name="Comments"></param>
        /// <param name="envNo"></param>
        /// <returns></returns>
        public int InsertContribution(string dte, string batch, string PplKey,
            double General, double Building, double Missions, double Designated,
            string typ, string Comments, string envNo)
        {
            string sql = "";
            string seq = "";
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "InsertContribution"));

            try
            {
                step = "Build sql for seq";
                sql = "SELECT MAX(cntrbSeq) " +
                          "FROM ezteller_contribution " +
                          "WHERE cntrbDate=@dte1 " +
                            "AND cntrbBatch=@batch " +
                          "GROUP BY cntrbDate, cntrbBatch ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte1", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));

                step = "Get seq Data";
                object res = cmd.ExecuteScalar();
                if (res == null)
                {
                    seq = "1";
                }
                else
                {
                    string temp = res.ToString();
                    seq = (1 + Convert.ToInt32(temp)).ToString();
                }

                step = "Build INSERT sql";
                sql = "INSERT INTO ezteller_contribution (cntrbDate, cntrbBatch, cntrbSeq, cntrbPplKey, " +
                                        "cntrbGeneral, cntrbBuilding, cntrbMissions, cntrbDesignated, " +
                                        "cntrbType, cntrbComments, cntrbCleared, cntrbActive, " +
                                        "cntrbUpdated, cntrbAdded) " +
                        "VALUES(@dte1, @batch, @seq, @personid, @general, @building, @missions, @designated, " +
                                "@typ, @comments, 0, 1, @dte2, @dte3) ";
                cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte1", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));
                cmd.Parameters.Add(new MySqlParameter("@seq", seq));
                cmd.Parameters.Add(new MySqlParameter("@personid", PplKey));
                cmd.Parameters.Add(new MySqlParameter("@general", General));
                cmd.Parameters.Add(new MySqlParameter("@building", Building));
                cmd.Parameters.Add(new MySqlParameter("@missions", Missions));
                cmd.Parameters.Add(new MySqlParameter("@designated", Designated));
                cmd.Parameters.Add(new MySqlParameter("@typ", typ));
                cmd.Parameters.Add(new MySqlParameter("@comments", Comments));
                cmd.Parameters.Add(new MySqlParameter("@dte2", DateTime.Now));
                cmd.Parameters.Add(new MySqlParameter("@dte3", DateTime.Now));

                step = "Insert the data";
                rtn = cmd.ExecuteNonQuery();
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("InsertContribution Failed", ex);
                EZex.Add("step", step);
                EZex.Add("sql", sql);
                EZex.Add("seq", seq);
                EZex.Add("dte", dte);
                EZex.Add("batch", batch);
                EZex.Add("PplKey", PplKey);
                EZex.Add("General", General);
                EZex.Add("Building", Building);
                EZex.Add("Missions", Missions);
                EZex.Add("Designated", Designated);
                EZex.Add("typ", typ);
                EZex.Add("Comments", Comments);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "InsertContribution"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="EnvNo"></param>
        /// <returns></returns>
        public DataTable GetContributorWithEnvNo(string EnvNo)
        {
            DataTable dt = null;
            string step = "";
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "GetContributorWithEnvNo"), "EnvNo: " + EnvNo);

            try
            {
                dt = new DataTable();
                step = "Build sql";
                sql = "SELECT p.LastName AS LastName, p.FirstName AS FirstName, " +
                               "a.Address1 AS Addr, p.PersonID AS pplKey, c.CommunicationCode AS pplPhone1 " +
                          "FROM per_person AS p " +
                            "JOIN ezteller_envelopes as e ON (e.PersonId=p.PersonID) " + 
                            "LEFT JOIN per_address AS a ON (a.PersonID=p.PersonID) " +
                            "LEFT JOIN per_communication AS c ON (c.PersonID=p.PersonID) " +
                          "WHERE e.EnvelopeNum=@EnvNo " +
                            "AND p.IsActive = 1 ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@EnvNo", EnvNo));

                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("GetContributorWithEnvNo Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetContributorWithEnvNo"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="General"></param>
        /// <param name="Building"></param>
        /// <param name="Missions"></param>
        /// <param name="Designated"></param>
        /// <param name="Comments"></param>
        /// <returns></returns>
        public int UpdateContribution(int seq, double General, double Building,
                        double Missions, double Designated, string Comments,
                        DateTime dte, int batch)
        {
            string sql = "";
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "UpdateContribution"));

            try
            {
                step = "Build the UPDATE sql";
                sql = "UPDATE ezteller_contribution " +
                        "SET cntrbGeneral=@general, " +
                             "cntrbBuilding=@building, " +
                             "cntrbMissions=@missions, " +
                             "cntrbDesignated=@designated, " +
                             "cntrbComments=@comments, " +
                             "cntrbUpdated=@dte2 " + // + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "WHERE cntrbSeq=@seq " +
                            "AND cntrbDate=@dte1 " +
                            "AND cntrbBatch=@batch ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@dte1", dte));
                cmd.Parameters.Add(new MySqlParameter("@batch", batch));
                cmd.Parameters.Add(new MySqlParameter("@seq", seq));
                cmd.Parameters.Add(new MySqlParameter("@general", General));
                cmd.Parameters.Add(new MySqlParameter("@building", Building));
                cmd.Parameters.Add(new MySqlParameter("@missions", Missions));
                cmd.Parameters.Add(new MySqlParameter("@designated", Designated));
                cmd.Parameters.Add(new MySqlParameter("@comments", Comments));
                cmd.Parameters.Add(new MySqlParameter("@seq", seq));
                cmd.Parameters.Add(new MySqlParameter("@dte2", DateTime.Now));

                step = "Update record";
                rtn = cmd.ExecuteNonQuery();
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("UpdateContribution Failed", ex);
                EZex.Add("step", step);
                EZex.Add("sql", sql);
                EZex.Add("seq", seq);
                EZex.Add("General", General);
                EZex.Add("Building", Building);
                EZex.Add("Missions", Missions);
                EZex.Add("Designated", Designated);
                EZex.Add("Comments", Comments);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateContribution"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetPeopleSelection()
        {
            DataTable dt = null;
            string sql = "";
            string temp = "";
            string fld = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetPeopleSelection"));

            try
            {
                dt = new DataTable();
                sql = "SELECT Concat(p.LastName, ', ', p.FirstName, ' - ', a.Address1, '; ',  a.City, ', ', a.State, ':', p.PersonID) AS 'Names', p.PersonID AS 'pKey' " +
                          "FROM per_person AS p " +
                            "LEFT JOIN per_address AS a ON (p.PersonID=a.PersonID) " +
                          "WHERE p.IsActive=1 " +
                            "AND p.LastName IS NOT null " +
                            "AND p.PersonTypeID=1 " +
                          "ORDER BY p.LastName, p.FirstName; ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                step = "Get data";
                dt = GetDataTable(cmd);
   
                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("GetPeopleSelection Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("temp", temp);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetPeopleSelection"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetEnvelopeFromKey(string key)
        {
            string sql = "";
            string rtn = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetEnvelopeFromKey"));

            try
            {
                step = "Build sql";
                sql = "SELECT EnvelopeNum " +
                          "FROM ezteller_envelopes " +
                          "WHERE PersonId=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "get envelope num";
                rtn = cmd.ExecuteScalar().ToString();
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("GetEnvelopeFromKey Failed", ex);
                EZex.Add("step", step);
                EZex.Add("sql", sql);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetEnvelopeFromKey"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable NoEnvelopeQuery(string year, int minNum)
        {
            string step = "";
            DataTable dt = null;
            string sql = "";

            Trace.Enter(Trace.RtnName(mModName, "NoEnvelopeQuery"));

            try
            {
                step = "Build SQL";
                sql = "SELECT * " +
                          "FROM ( " +
                            "SELECT CONCAT(p.LastName, ', ', p.FirstName) AS Name, " +
                                   "COUNT(*) AS NoContributions AS NoContributions, " +
                                   "SUM(c.cntrbGeneral + c.cntrbMissions + c.cntrbBuilding + c.cntrbDesignated) AS Total, " +
                                    "MAX(c.cntrbDate) AS LastGiving " +
                              "FROM per_person AS p " +
                                "LEFT JOIN ezteller_envelopes AS e ON (e.PersonID=p.PersonID) " +
                                "JOIN ezteller_contribution AS c ON (p.PersonID=c.cntrbPplKey) " +
                              "WHERE YEAR(c.cntrbDate) = @year " +
                                "AND e.EnvelopeNum IS NULL " +
                              "GROUP BY p.PersonID " +
                              "ORDER BY p.LastName, p.FirstName) AS report " +
                          "WHERE report.NoContributions > @min ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@year", year));
                cmd.Parameters.Add(new MySqlParameter("@min", minNum));

                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("NoEnvelopeQuery Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "NoEnvelopeQuery"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable EnvelopeQuery()
        {
            DataTable dt = null;
            string sql = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "NoEnvelopeQuery"));

            try
            {
                dt = new DataTable();
                step = "Build SQL";
                sql = "SELECT e.EnvelopeNum AS EnvNo, CONCAT(p.Prefix, ' ', p.LastName, ', ', p.FirstName) AS Name " +
                          "FROM per_person AS p " +
                            "LEFT JOIN ezteller_envelopes AS e ON (p.PersonID=e.PersonID) " +
                          "WHERE e.EnvelopeNum IS NOT NULL " +
                            "AND e.EnvelopeNum <> 998 " +
                            "AND e.EnvelopeNum <> 999 " +
                          "ORDER BY e.EnvelopeNum";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                dt = GetDataTable(cmd);

                return dt;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("NoEnvelopeQuery Failed", ex);
                EZex.Add("sql", sql);
                EZex.Add("step", step);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "NoEnvelopeQuery"));
            }
        }

    }
}
