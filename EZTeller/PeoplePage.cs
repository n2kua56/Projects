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
    public class PeoplePage
    {
        private string mModName = "Teller_PeoplePage";
        private Form1 mFrm;

        private EZDeskCommon mCommon;
        public PeoplePage(EZDeskCommon Common, Form1 frm)
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
        public void FillPeoplePage(DateTime dte, string sort)
        {
            DataTable dt = null;

            Trace.Enter(Trace.RtnName(mModName, "FillPeoplePage"));

            try
            {
                DataGridViewCellStyle cs = new DataGridViewCellStyle();
                cs.Alignment = DataGridViewContentAlignment.MiddleRight;
                cs.DataSourceNullValue = "0.00";
                cs.Format = "##,##0.00";

                mFrm.StatusMessage("Loading People");
                mFrm.toolStripStatusLabel1.Text = "Loading People";
                mFrm.Enabled = false;
                mFrm.Cursor = Cursors.WaitCursor;

                ClearPeoplePage();

                dt = mFrm.tCtrl.GetPeopleData(dte, sort);
                mFrm.dgPeople.DataSource = dt;
                mFrm.dgPeople.Columns[0].Width = 0;  //Hide the index column
                mFrm.dgPeople.Columns[0].Visible = false;
                mFrm.dgPeople.Columns["Env"].Width = 45;
                mFrm.dgPeople.Columns["NAME"].Width = 150;
                mFrm.dgPeople.Columns["ADDRESS"].Width = 185;
                mFrm.dgPeople.Columns["CITY"].Width = 120;
                mFrm.dgPeople.Columns["STATE"].Width = 45;
                mFrm.dgPeople.Columns["ZIP"].Width = 70;
                mFrm.dgPeople.Columns["TOTAL"].Width = 66;
                mFrm.dgPeople.Columns["TOTAL"].DefaultCellStyle = cs;

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
                Trace.Exit(Trace.RtnName(mModName, "FillPeoplePage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void ClearPeoplePage()
        {
            Trace.Enter(Trace.RtnName(mModName, "ClearPeoplePage"));

            try
            {
                mFrm.dgPeople.DataSource = null;
                mFrm.dgPeople.Rows.Clear();
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                //throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ClearPeoplePage"));
            }
        }

        /// <summary>
        /// Sets the FirstVisibleCardRow to match the first
        /// character in the key string (i.e. If the key is
        /// 'j' then the first row with 'J' as the first
        /// letter in the last name is now the first visible
        /// row).
        /// </summary>
        /// <param name="frm">Form1 pointer</param>
        /// <param name="key">key to move to</param>
        public void MoveToTab(string key)
        {
            int newTopIndex = -1;
            char gc = ' ';
            char kc = ' ';
            int idx = -1;
            string name = "";

            Trace.Enter(Trace.RtnName(mModName, "MoveToTab"), "key: " + key);

            try
            {
                kc = Convert.ToChar(key.Substring(0, 1));
                for (idx = 0;
                    ((idx < mFrm.dgPeople.Rows.Count) && (newTopIndex == -1));
                    idx++)
                {
                    name = mFrm.dgPeople.Rows[idx].Cells["Name"].Value.ToString();
                    if (name.Length > 0)
                    {
                        gc = Convert.ToChar(name.Substring(0, 1));
                    }
                    else
                    {
                        gc = ' ';
                    }

                    if (gc >= kc)
                    {
                        newTopIndex = idx;
                        mFrm.dgPeople.FirstDisplayedCell = mFrm.dgPeople[0, idx];
                    }
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("idx", idx);
                EZex.Add("key", key);
                EZex.Add("kc", kc);
                EZex.Add("gc", gc);
                EZex.Add("name", name);
                EZex.Add("newTopIndex", newTopIndex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "MoveToTab"));
            }
        }

        /// <summary>
        /// An Alpha tab has been pressed (OR we've noticed that the 
        /// first character of the last name has chanaged at the 
        /// FirstVisibleCardRow.
        /// </summary>
        /// <param name="frm">Pointer to Form1</param>
        /// <param name="e">The tab we've moved to</param>
        public void AlphaTabChanged(System.Windows.Forms.TabControl e)
        {
            string tab = "";

            Trace.Enter(Trace.RtnName(mModName, "AlphaTabChanged"));

            try
            {
                tab = e.TabPages[e.SelectedIndex].Text;
                MoveToTab(tab);
            }
            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed", ex);
                EZex.Add("tab", tab);
                throw EZex;
            }
            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "AlphaTabChanged"));
            }
        }

    }
}
