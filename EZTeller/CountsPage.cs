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
    public class CountsPage
    {
        private string mModName = "Teller_CountsPage";
        private Form1 mFrm;
        private int mInSetup = 0;

        private EZDeskCommon mCommon;
        public CountsPage(EZDeskCommon Common, Form1 frm)
        {
            mCommon = Common;
            mFrm = frm;
        }

        /// <summary>
        /// Called when the form needs to be initialized. Fill-in the years
        /// that we have counts for, then select the most recent year.
        /// </summary>
        public void Init()
        {
            zCountSetYears();
            mFrm.tabStripYears.SelectedIndex = mFrm.tabStripYears.TabCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabId"></param>
        public void YearSelected()
        {
            Trace.Enter(Trace.RtnName(mModName, "YearSelected"));

            try
            {
                zCountSetMonths();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException(Trace.RtnName(mModName, "YearSelected"), ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "YearSelected"));
            }
        }

        /// <summary>
        /// A month has been selected it's time to fill-in the count grid
        /// </summary>
        public void MonthSelected()
        {
            Trace.Enter(Trace.RtnName(mModName, "MonthSelected"));

            try
            {
                zFillCountGrid();
            }

            catch (Exception ex)
            {
                EZException eze = new EZException(Trace.RtnName(mModName, "MonthSelected"), ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "MonthSelected"));
            }
        }

        /// <summary>
        /// The user has double clicked on a count - to reopen it.
        /// </summary>
        public void ChangeCount()
        {
            DateTime? newCountDate = null;
            int newCountBatch = -1;
            object val = null;

            Trace.Enter(Trace.RtnName(mModName, "ChangeCount"));

            try
            {
                val = mFrm.grdCounts.CurrentRow.Cells["Date"].Value;
                newCountDate = ((DateTime)val).Date;
                mFrm.dteRunDate.Value = (DateTime)newCountDate;

                val = mFrm.grdCounts.CurrentRow.Cells["Batch"].Value;
                newCountBatch = Convert.ToInt32(val.ToString());
                mFrm.BatchNo.Value = newCountBatch;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException(Trace.RtnName(mModName, "ChangeCount"), ex);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ChangeCount"));
            }
        }

        /// <summary>
        /// Setting the years
        /// </summary>
        /// <param name="frm"></param>
        public void zCountSetYears()
        {
            List<string> years = new List<string>();

            Trace.Enter(Trace.RtnName(mModName, "CountSetYears"));

            try
            {
                mInSetup = 1;
                mFrm.tabStripYears.TabPages.Clear();
                years = mFrm.tCtrl.getListOfYears();
                foreach (string year in years)
                {
                    mFrm.tabStripYears.TabPages.Add(year, year);
                }
                mInSetup = 0;
                mFrm.tabStripYears.SelectedIndex = mFrm.tabStripYears.TabCount;
            }

            catch (Exception ex)
            {
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "Failed to set the Count year tabs. Msg: " + ex.Message);
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "CountSetYears"));
            }
        }

        /// <summary>
        /// A year has been selected, we now have to set the tabs for the months
        /// that have contributions for the selected year. After filling in the
        /// month tabs select the last one.
        /// </summary>
        /// <param name="frm"></param>
        public void zCountSetMonths()
        {
            string name = "";
            string year = "";
            List<string> months = new List<string>();
            string[] monthArray =
                ((string)"Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec").Split('|');

            Trace.Enter(Trace.RtnName(mModName, "CountSetMonths"));

            try
            {
                if (mInSetup == 0)
                {
                    year = mFrm.tabStripYears.SelectedTab.Text;
                    mInSetup = 1;
                    mFrm.tabStripMonths.TabPages.Clear();
                    months = mFrm.tCtrl.getListOfMonths(year);
                    foreach (string month in months)
                    {
                        name = monthArray[Convert.ToInt32(month) - 1];
                        mFrm.tabStripMonths.TabPages.Add(name, name);
                    }
                    mInSetup = 0;
                    //Select the last tab added.
                    mFrm.tabStripMonths.SelectedIndex = mFrm.tabStripMonths.TabCount - 1;
                }
            }

            catch (Exception ex)
            {
                Trace.WriteLine(Trace.TraceLevels.Error,
                    "Failed to set the Count year tabs. Msg: " + ex.Message);
                throw new Exception("Failed: " + ex.Message.ToString());
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "CountSetMonths"));
            }
        }

        /////// <summary>
        /////// 
        /////// </summary>
        /////// <param name="frm"></param>
        ////public void tabStripYears_SelectedTabChanged()
        ////{
        ////    Trace.Enter(Trace.RtnName(mModName, "tabStripYears_SelectedTabChanged"));
        ////    try
        ////    {
        ////        zCountSetMonths();
        ////        GenerateGivingReport();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        EZException EZex = new EZException("Failed ", ex);
        ////        throw EZex;
        ////    }
        ////    finally
        ////    {
        ////        Trace.Exit(Trace.RtnName(mModName, "tabStripYears_SelectedTabChanged"));
        ////    }
        ////}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        public void GenerateGivingReport()
        {
            int year1 = 0;
            int year2 = 0;
            DataTable dt = null;

            Trace.Enter(Trace.RtnName(mModName, "GenerateGivingReport"));

            try
            {
                year1 = Convert.ToInt32(mFrm.tabStripYears.SelectedTab.Text);
                if (mFrm.rbLastThisYear.Checked) { year2 = year1 - 1; }
                else { year2 = year1; }

                if (mFrm.rbQuarterly.Checked)
                {
                    dt = mFrm.tCtrl.GetQuarterlyGiving(year1, year2);
                }
                else
                {
                    //? dt = frm.EZTellerDataLayer.GetMonthlyGiving(year1, year2);
                }

                mFrm.dgGivingSummary.DataSource = dt;
                DataGridViewCellStyle cs = new DataGridViewCellStyle();
                cs.Alignment = DataGridViewContentAlignment.MiddleRight;
                mFrm.dgGivingSummary.Columns[0].Width = 40;
                mFrm.dgGivingSummary.Columns[1].Width = 72;
                mFrm.dgGivingSummary.Columns[1].DefaultCellStyle = cs;
                if (mFrm.dgGivingSummary.Columns.Count == 3)
                {
                    mFrm.dgGivingSummary.Columns[2].Width = 72;
                    mFrm.dgGivingSummary.Columns[2].DefaultCellStyle = cs;
                }

            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GenerateGivingReport"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        private void zFillCountGrid()
        {
            DataTable dt = null;
            int year = 0;
            int month = 0;
            string monthStr = "";
            string val = "";
            string[] monthArray =
                ((string)"Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec").Split('|');
            int idx = -1;
            int entries = 0;
            double total = 0.00;
            DataRow row = null;

            Trace.Enter(Trace.RtnName(mModName, "zFillCountGrid"));

            try
            {
                if (mInSetup == 0)
                {
                    year = Convert.ToInt32(mFrm.tabStripYears.SelectedTab.Text);
                    monthStr = mFrm.tabStripMonths.SelectedTab.Text;
                    switch (monthStr)
                    {
                        case "Jan":
                            month = 1;
                            break;
                        case "Feb":
                            month = 2;
                            break;
                        case "Mar":
                            month = 3;
                            break;
                        case "Apr":
                            month = 4;
                            break;
                        case "May":
                            month = 5;
                            break;
                        case "Jun":
                            month = 6;
                            break;
                        case "Jul":
                            month = 7;
                            break;
                        case "Aug":
                            month = 8;
                            break;
                        case "Sep":
                            month = 9;
                            break;
                        case "Oct":
                            month = 10;
                            break;
                        case "Nov":
                            month = 11;
                            break;
                        case "Dec":
                            month = 12;
                            break;
                    }

                    dt = mFrm.tCtrl.GetCountData(year, month);
                    for (idx = 0; idx < dt.Rows.Count; idx++)
                    {
                        row = dt.Rows[idx];
                        val = row["Entries"].ToString();
                        entries += Convert.ToInt32(val);
                        val = row["Total"].ToString();
                        total += Convert.ToDouble(val);
                    }
                    row = dt.NewRow();

                    //row["Date"] = "  MONTH TOTAL";
                    row["Batch"] = dt.Rows.Count.ToString();
                    row["Entries"] = entries.ToString();
                    row["Total"] = total.ToString("###,##0.00");
                    dt.Rows.Add(row);
                    ////mFrm.grdCounts.Rows[mFrm.grdCounts.Rows.Count - 1].Cells["Date"].ValueType = typeof(string);
                    ////mFrm.grdCounts.Rows[mFrm.grdCounts.Rows.Count - 1].Cells["Date"].Value = "  MONTH TOTAL";

                    DataSet dataSet1 = new DataSet();
                    dataSet1.Tables.Add(dt);
                    mFrm.grdCounts.DataSource = dt;
                    mFrm.grdCounts.AllowUserToAddRows = false;
                    mFrm.grdCounts.AllowUserToDeleteRows = false;
                    mFrm.grdCounts.AllowUserToOrderColumns = false;
                    mFrm.grdCounts.AllowUserToResizeRows = false;

                    mFrm.grdCounts.Columns[mFrm.grdCounts.ColumnCount - 1].DefaultCellStyle.Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                    mFrm.grdCounts.Columns[mFrm.grdCounts.ColumnCount - 2].DefaultCellStyle.Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                    mFrm.grdCounts.Columns[mFrm.grdCounts.ColumnCount - 1].HeaderCell.Style.Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                    mFrm.grdCounts.Columns[mFrm.grdCounts.ColumnCount - 2].HeaderCell.Style.Alignment =
                        System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                    mFrm.grdCounts.Columns["Total"].DefaultCellStyle.Format = "##,##0.00";
                    int x = mFrm.grdCounts.Left;
                    x = mFrm.grdCounts.Top;
                    x = mFrm.grdCounts.Height;
                    x = mFrm.grdCounts.Width;
                }
            }

            catch (Exception ex)
            {
                EZException eze = new EZException(Trace.RtnName(mModName, "zFillCountGrid") + " failed", ex);
                eze.Add("year", year);
                eze.Add("monthStr", monthStr);
                eze.Add("month", month);
                Trace.WriteLine(Trace.TraceLevels.Debug, "Exception, Msg: " + ex.Message);

                throw new Exception("Failed: " + ex.Message.ToString());
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "zFillCountGrid"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void RenameCount()
        {
            string countDate = "";
            string countBatch = "";
            int countEntries = -1;
            double countTotal = -1.00;

            Trace.Enter(Trace.RtnName(mModName, "RenameCount"));

            try
            {
                DataGridViewRow row = mFrm.grdCounts.SelectedRows[0];
                countDate = row.Cells[0].Value.ToString();
                countBatch = row.Cells[1].Value.ToString();
                countEntries = Convert.ToInt32(row.Cells[2].Value.ToString());
                countTotal = Convert.ToDouble(row.Cells[3].Value.ToString());

                //? FormCountsRename rFrm = new FormCountsRename(countDate, countBatch, frm);
                //? DialogResult dr = rFrm.ShowDialog();
                //? if (dr == DialogResult.OK)
                //? {
                //?     newCountDate = rFrm.NewCountDate;
                //?     newCountBatch = rFrm.NewCountBatch;
                //?
                //?     frm.EZTellerDataLayer.RenameCount(countDate, countBatch,
                //?                     newCountDate, newCountBatch);

                //?     zFillCountGrid(frm);
                //?}
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "RenameCount"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void DeleteCount()
        {
            DataGridViewRow row = null;
            string msg = "";
            string dte = "";
            string batch = "";
            DialogResult dr = DialogResult.None;

            Trace.Enter(Trace.RtnName(mModName, "DeleteCount"));

            try
            {
                row = mFrm.grdCounts.SelectedRows[0];
                dte = row.Cells["Date"].Value.ToString();
                if (dte.Trim() != "MONTH TOTAL")
                {
                    msg = "Are you sure you want to delete:\n" +
                            "  Date:\t" + row.Cells["Date"].Value.ToString() + "\n" +
                            "  Batch:\t" + row.Cells["Batch"].Value.ToString() + "\n" +
                            "  Entries:\t" + row.Cells["Entries"].Value.ToString() + "\n" +
                            "  Total:\t" + row.Cells["Total"].Value.ToString();
                    dr = MessageBox.Show(msg, "ARE YOU SURE",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        dte = row.Cells["Date"].Value.ToString();
                        batch = row.Cells["Batch"].Value.ToString();
                        mFrm.tCtrl.DeleteCount(dte, batch);
                        zFillCountGrid();
                        mFrm.sumPage.UpdateSummaryPage(mFrm.dteRunDate.Value,
                                Convert.ToInt32(mFrm.BatchNo.Value));
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteCount"));
            }
        }
    }
}
