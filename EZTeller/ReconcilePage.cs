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
    public class ReconcilePage
    {
        private string mModName = "Teller_ReconcilePage";
        private Form1 mFrm;

        private static DateTime mCountDate = DateTime.MinValue;
        private static int mCountBatchNo = -1;

        private EZDeskCommon mCommon;
        public ReconcilePage(EZDeskCommon Common, Form1 frm)
        {
            mCommon = Common;
            mFrm = frm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="dte"></param>
        /// <param name="sort"></param>
        public void FillReconcilePage(DateTime dte, int BatchNo)
        {
            DataTable dt = null;

            Trace.Enter(Trace.RtnName(mModName, "FillReconcilePage"));

            try
            {
                mCountDate = dte;
                mCountBatchNo = BatchNo;

                DataGridViewCellStyle cs = new DataGridViewCellStyle();
                cs.Alignment = DataGridViewContentAlignment.MiddleRight;

                mFrm.StatusMessage("Loading Reconcile");
                mFrm.toolStripStatusLabel1.Text = "Loading Reconcile";
                mFrm.Enabled = false;
                mFrm.Cursor = Cursors.WaitCursor;

                dt = mFrm.tCtrl.GetReconcileData(dte, BatchNo);
                mFrm.dgReconcile.DataSource = dt;
                mFrm.dgReconcile.Columns["Seq"].Width = 45;
                mFrm.dgReconcile.Columns["Name"].Width = 150;
                mFrm.dgReconcile.Columns["Clr"].Width = 25;
                mFrm.dgReconcile.Columns["Total"].Width = 66;
                mFrm.dgReconcile.Columns["TOTAL"].DefaultCellStyle = cs;
                mFrm.dgReconcile.Columns["Comments"].Width = 150;

                zCalcTotals();

                mFrm.StatusMessage("");
                mFrm.toolStripStatusLabel1.Text = "";
                mFrm.Enabled = true;
                mFrm.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "FillReconcilePage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        public void Reset()
        {
            DateTime runDte = DateTime.MinValue;
            int runBatch = -1;
            int rtn = -1;

            Trace.Enter(Trace.RtnName(mModName, "Reset"));
            try
            {
                runDte = mFrm.dteRunDate.Value;
                runBatch = Convert.ToInt32(mFrm.BatchNo.Value);
                rtn = mFrm.tCtrl.ResetBatch(runDte, runBatch);
                FillReconcilePage(runDte, runBatch);
            }
            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }
            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "Reset"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void ClearReconcilePage()
        {
            Trace.Enter(Trace.RtnName(mModName, "ClearReconcilePage"));

            try
            {
                mFrm.dgReconcile.Rows.Clear();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ClearReconcilePage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="RowIndex"></param>
        public void DetailCellMouseDown(int RowIndex)
        {
            Trace.Enter(Trace.RtnName(mModName, "DetailCellMouseDown"));

            try
            {
                if (RowIndex > -1)
                {
                    mFrm.dgReconcile.Rows[RowIndex].Selected = true;
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DetailCellMouseDown"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void EditDetailRow(int row, int col)
        {
            string Clr = "";
            string newClr = "";
            string seq = "";
            int recsUpdated = -1;

            Trace.Enter(Trace.RtnName(mModName, "EditDetailRow"));

            try
            {
                Clr = mFrm.dgReconcile[col, row].Value.ToString();
                seq = mFrm.dgReconcile["Seq", row].Value.ToString();

                if (Clr == "Y")
                {
                    newClr = "N";
                }

                else
                {
                    newClr = "Y";
                }

                recsUpdated = mFrm.tCtrl.UpdateReconcileCleared(Convert.ToInt32(seq),
                                mCountDate, mCountBatchNo, newClr);
                mFrm.dgReconcile[col, row].Value = newClr;
                zCalcTotals();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("recsUpdated", recsUpdated);
                EZex.Add("row", row);
                EZex.Add("col", col);
                EZex.Add("seq", seq);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "EditDetailRow"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        private void zCalcTotals()
        {
            int idx = -1;
            double totalAmt = 0.00;
            int totalNoChecks = 0;
            double amtCleared = 0.00;
            int checksCleared = 0;
            double difference = 0.00;
            double amt = 0.00;
            string clr = "";

            totalNoChecks = mFrm.dgReconcile.Rows.Count;
            for (idx = 0; idx < mFrm.dgReconcile.Rows.Count; idx++)
            {
                clr = mFrm.dgReconcile["Clr", idx].Value.ToString();
                amt = Convert.ToDouble(mFrm.dgReconcile["Total", idx].Value.ToString());
                totalAmt += amt;
                if (clr == "Y")
                {
                    checksCleared++;
                    amtCleared += amt;
                }
            }
            difference = totalAmt - amtCleared;
            mFrm.tbTotalNoChecks.Text = totalNoChecks.ToString();
            mFrm.tbTotalAmt.Text = totalAmt.ToString("##,##0.00");
            mFrm.tbChecksCleared.Text = checksCleared.ToString();
            mFrm.tbAmtCleared.Text = amtCleared.ToString("##,##0.00");
            mFrm.tbDifference.Text = difference.ToString("##,##0.00");
        }

    }
}
