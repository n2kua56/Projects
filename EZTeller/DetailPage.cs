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
    public class DetailPage
    {
        private string mModName = "Teller_DetailPage";
        private Form1 mFrm;

        private EZDeskCommon mCommon;
        public DetailPage(EZDeskCommon Common, Form1 frm)
        {
            mCommon = Common;
            mFrm = frm;
        }

        /// <summary>
        /// 
        /// </summary>
        public void FillDetailPage(DateTime dte, int batch)
        {
            DataTable dt = null;

            Trace.Enter(Trace.RtnName(mModName, "FillDetailPage"));

            try
            {
                DataGridViewCellStyle cs = new DataGridViewCellStyle();
                cs.Alignment = DataGridViewContentAlignment.MiddleRight;
                ClearDetailPage();
                dt = mFrm.tCtrl.GetDetailData(dte, batch.ToString());
                if (dt != null)
                {
                    mFrm.lblDetailTitle.Text = dte.ToString("yyyy-MM-dd") +
                        " BATCH: " + batch.ToString();
                    mFrm.dgvDetail.DataSource = dt;
                    mFrm.dgvDetail.Columns["Seq"].Width = 50;
                    mFrm.dgvDetail.Columns["Env"].Width = 50;
                    mFrm.dgvDetail.Columns["Ck"].Width = 25;
                    mFrm.dgvDetail.Columns["Name"].Width = 133;
                    mFrm.dgvDetail.Columns["General"].Width = 65;
                    mFrm.dgvDetail.Columns["General"].DefaultCellStyle = cs;
                    mFrm.dgvDetail.Columns["Building"].Width = 65;
                    mFrm.dgvDetail.Columns["Building"].DefaultCellStyle = cs;
                    mFrm.dgvDetail.Columns["Missions"].Width = 65;
                    mFrm.dgvDetail.Columns["Missions"].DefaultCellStyle = cs;
                    mFrm.dgvDetail.Columns["Designated"].Width = 65;
                    mFrm.dgvDetail.Columns["Designated"].DefaultCellStyle = cs;
                    mFrm.dgvDetail.Columns["Total"].Width = 65;
                    mFrm.dgvDetail.Columns["Total"].DefaultCellStyle = cs;
                }
            }

            catch (Exception ex)
            {
                Trace.WriteLine(Trace.TraceLevels.Debug, "Exception");
                throw new Exception("Failed: " + ex.Message.ToString());
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "FillDetailPage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        private void ClearDetailPage()
        {
            Trace.Enter(Trace.RtnName(mModName, "ClearDetailPage"));

            try
            {
                mFrm.dgvDetail.DataSource = null;
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "ClearDetailPage"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public void DetailCellMouseDown(int idx)
        {
            Trace.Enter(Trace.RtnName(mModName, "DetailCellMouseDown"));

            try
            {
                if (idx > -1)
                {
                    mFrm.dgvDetail.Rows[idx].Selected = true;
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                EZex.Add("idx", idx);
                EZex.Add("rows", mFrm.dgPeople.Rows.Count);
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
        public void DeleteRow(string dte, string batch)
        {
            string msg = "";
            string Seq = "";
            DataGridViewRow row = null;
            DialogResult dr = DialogResult.None;

            Trace.Enter(Trace.RtnName(mModName, "DeleteRow"));

            try
            {
                row = mFrm.dgvDetail.SelectedRows[0];
                Seq = row.Cells["Seq"].Value.ToString().Trim();
                msg = "Are you sure you want to delete:\n" +
                        "  Envelope: \t" + row.Cells["Env"].Value.ToString() + "\n" +
                        "  Name: \t\t" + row.Cells["Name"].Value.ToString() + "\n" +
                        "  General: \t" + row.Cells["General"].Value.ToString() + "\n" +
                        "  Building: \t" + row.Cells["Building"].Value.ToString() + "\n" +
                        "  Missions: \t" + row.Cells["Missions"].Value.ToString() + "\n" +
                        "  Designated: \t" + row.Cells["Designated"].Value.ToString() + "\n" +
                        "  Total: \t\t" + row.Cells["Total"].Value.ToString() + "\n" +
                        "  Comments: \t" + row.Cells["Comments"].Value.ToString();
                dr = MessageBox.Show(msg, "ARE YOU SURE",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    mFrm.tCtrl.DeleteContribution(dte, batch, Seq);
                    FillDetailPage(mFrm.dteRunDate.Value, Convert.ToInt32(mFrm.BatchNo.Value));
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                EZex.Add("row", row);
                EZex.Add("Seq", Seq);
                EZex.Add("dr", dr);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DeleteRow"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        public void EditDetailRow()
        {
            string Seq = "";
            string Env = "";
            string Name = "";
            string Ck = "";
            string General = "";
            string Building = "";
            string Missions = "";
            string Designated = "";
            string Total = "";
            string Comments = "";

            Trace.Enter(Trace.RtnName(mModName, "EditDetailRow"));

            try
            {
                DataGridViewRow row = mFrm.dgvDetail.SelectedRows[0];
                Seq = row.Cells["Seq"].Value.ToString().Trim();
                if (Seq != "9999")
                {
                    Env = row.Cells["Env"].Value.ToString().Trim();
                    Name = row.Cells["Name"].Value.ToString().Trim();
                    Ck = row.Cells["Ck"].Value.ToString().Trim();
                    General = row.Cells["General"].Value.ToString().Trim();
                    Building = row.Cells["Building"].Value.ToString().Trim();
                    Missions = row.Cells["Missions"].Value.ToString().Trim();
                    Designated = row.Cells["Designated"].Value.ToString().Trim();
                    Total = row.Cells["Total"].Value.ToString().Trim();
                    Comments = row.Cells["Comments"].Value.ToString().Trim();

                    mFrm.dteRunDate.Enabled = false;
                    mFrm.BatchNo.Enabled = false;
                    if (Ck == "Y")
                    {
                        mFrm.cbType.SelectedIndex = mFrm.cbType.Items.IndexOf("CHECK");
                    }
                    mFrm.cbType.Enabled = false;
                    mFrm.tbEnvNo.Text = Env;
                    mFrm.tbEnvNo.ReadOnly = true;
                    mFrm.tbName.Visible = true;
                    mFrm.tbName.Text = Name;
                    mFrm.tbName.ReadOnly = true;
                    mFrm.cbNames.Visible = false;
                    mFrm.cbNames.Enabled = false;
                    mFrm.tbGeneral.Text = General;
                    mFrm.tbMissions.Text = Missions;
                    mFrm.tbBuilding.Text = Building;
                    mFrm.tbDesignated.Text = Designated;
                    mFrm.lblTotal.Text = Total;
                    mFrm.tbComment.Text = Comments;
                    mFrm.groupBox1.BackColor = Color.PaleGreen;
                    mFrm.SeqNo = Convert.ToInt32(Seq);
                    mFrm.tbGeneral.Focus();
                }
                else
                {
                    MessageBox.Show("You can NOT edit the total line", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "EditDetailRow"));
            }
        }

    }
}
