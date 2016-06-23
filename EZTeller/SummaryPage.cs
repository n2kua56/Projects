using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using EZDeskDataLayer;

namespace EZTeller
{
    public class SummaryPage
    {
        private string mModName = "Teller_SummaryPage";
        private Form1 mFrm;
        private EZDeskDataLayer.EZTeller.TellerCtrl tCtrl = null;

        private EZDeskCommon mCommon;
        public SummaryPage(EZDeskCommon Common, Form1 frm)
        {
            mCommon = Common;
            mFrm = frm;
            tCtrl = new EZDeskDataLayer.EZTeller.TellerCtrl(mCommon);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearSummaryPage()
        {
            DataTable dt = null;
            DataRow dr = null;

            Trace.Enter(Trace.RtnName(mModName, "ClearSummaryPage"));

            try
            {
                dt = new DataTable();
                dt.Columns.Add("cntrbType", System.Type.GetType("System.String"));
                dt.Columns.Add("General", System.Type.GetType("System.String"));
                dt.Columns.Add("Building", System.Type.GetType("System.String"));
                dt.Columns.Add("Missions", System.Type.GetType("System.String"));
                dt.Columns.Add("Designated", System.Type.GetType("System.String"));
                dt.Columns.Add("Total", System.Type.GetType("System.String"));
                dt.Columns.Add("itemCount", System.Type.GetType("System.String"));

                dr = dt.NewRow();
                dr["cntrbType"] = "0";
                dr["General"] = "0.00";
                dr["Building"] = "0.00";
                dr["Missions"] = "0.00";
                dr["Designated"] = "0.00";
                dr["Total"] = "0.00";
                dr["itemCount"] = "";

                zFillSummaryCashCredit(dr);
                zFillSummaryCashLoose(dr);
                zFillSummaryCashNonCredit(dr);
                zFillSummaryCashTotal(dr);
                zFillSummaryCheckCredit(dr);
                zFillSummaryCheckNonCredit(dr);
                zFillSummaryCheckTotal(dr);
                zFillSummaryTotal(dr);

                mFrm.lblSummaryTitle.Text = "YYYY-MM-DD BATCH: n";
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("ClearSummaryPage Failed ", ex);
                EZex.Add("dt", dt);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ClearSummaryPage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateSummaryPage(DateTime dte, int batch)
        {
            string typ = "";
            DataTable dt = null;

            Trace.Enter(Trace.RtnName(mModName, "UpdateSummaryPage"));

            try
            {
                ClearSummaryPage();
                dt = tCtrl.GetSummaryData(dte, batch.ToString());
                mFrm.lblSummaryTitle.Text = dte.ToString("yyyy-MM-dd") +
                    " BATCH: " + batch.ToString();

                foreach (DataRow dr in dt.Rows)
                {
                    typ = dr["cntrbType"].ToString();
                    switch (typ)
                    {
                        case "0":   // Cash|Credit
                            zFillSummaryCashCredit(dr);
                            break;

                        case "1":   // Cash|Loose
                            zFillSummaryCashLoose(dr);
                            break;

                        case "2":   // Cash|NonCredit
                            zFillSummaryCashNonCredit(dr);
                            break;

                        case "11":  // Casg|Total
                            zFillSummaryCashTotal(dr);
                            break;

                        case "3":   // Check|Credit
                            zFillSummaryCheckCredit(dr);
                            break;

                        case "4":
                            zFillSummaryCheckNonCredit(dr);
                            break;

                        case "12":
                            zFillSummaryCheckTotal(dr);
                            break;

                        case "13":
                            zFillSummaryTotal(dr);
                            break;

                        default:
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("UpdateSummaryPage Failed ", ex);
                EZex.Add("dte", dte);
                EZex.Add("batch", batch);
                EZex.Add("dt", dt);
                EZex.Add("typ", typ);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateSummaryPage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryTotal(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryTotal"));

            try
            {
                mFrm.lblGeneralTotal.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblBuildingTotal.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblMissionsTotal.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblDesignatedTotal.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblTotalTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryTotal Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryTotal"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string zFmtMoney(string p)
        {
            double mny = 0;
            string rtn = "";

            Trace.Enter(Trace.RtnName(mModName, "zFmtMoney"), p);

            try
            {
                if (p.Trim().Length == 0)
                {
                    p = "0.00";
                }
                mny = Convert.ToDouble(p);
                rtn = mny.ToString("##,##0.00");
                return rtn;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFmtMoney Failed ", ex);
                EZex.Add("p", p);
                EZex.Add("mny", mny);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFmtMoney"), rtn);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCheckTotal(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCheckTotal"));

            try
            {
                mFrm.lblCheckGeneralTotal.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCheckBuildingTotal.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCheckMissionsTotal.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCheckDesignatedTotal.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCheckTotalTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCheckTotal Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCheckTotal"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCheckNonCredit(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCheckNonCredit"));

            try
            {
                mFrm.label59.Text = "Non-Credit (" + dr["itemCount"].ToString() + ")";
                mFrm.lblCheckNonCreditGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCheckNonCreditBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCheckNonCreditMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCheckNonCreditDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCheckNonCreditTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCheckNonCredit Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCheckNonCredit"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCheckCredit(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCheckCredit"));

            try
            {
                mFrm.label60.Text = "Credit (" + dr["itemCount"].ToString() + ")";
                mFrm.lblCheckCreditGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCheckCreditBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCheckCreditMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCheckCreditDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCheckCreditTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCheckCredit Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCheckCredit"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCashTotal(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCashTotal"));

            try
            {
                mFrm.lblCashTotalGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCashTotalBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCashTotalMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCashTotalDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCashTotalTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCashTotal Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCashTotal"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCashNonCredit(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCashNonCredit"));
            try
            {
                mFrm.lblCashNonCreditGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCashNonCreditBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCashNonCreditMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCashNonCreditDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCashNonCreditTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCashNonCredit Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCashNonCredit"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCashLoose(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCashLoose"));

            try
            {
                mFrm.lblCashLooseGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCashLooseBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCashLooseMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCashLooseDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCashLooseTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCashLoose Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCashLoose"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dr"></param>
        private void zFillSummaryCashCredit(DataRow dr)
        {
            Trace.Enter(Trace.RtnName(mModName, "zFillSummaryCashCredit"));

            try
            {
                mFrm.lblCashCreditGeneral.Text = zFmtMoney(dr["General"].ToString());
                mFrm.lblCashCreditBuilding.Text = zFmtMoney(dr["Building"].ToString());
                mFrm.lblCashCreditMissions.Text = zFmtMoney(dr["Missions"].ToString());
                mFrm.lblCashCreditDesignated.Text = zFmtMoney(dr["Designated"].ToString());
                mFrm.lblCashCreditTotal.Text = zFmtMoney(dr["Total"].ToString());
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("zFillSummaryCashCredit Failed ", ex);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillSummaryCashCredit"));
            }
        }
        
    }
}
