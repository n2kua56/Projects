using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmExport : Form
    {
        //TODO: Calculate Bearing and Distance
        //TODO: Sort by DXCC

        private Form1 mForm1 = null;
        private string mLogName = "";
        private NumericUpDown[] mSeqs;
        private CheckBox[] mCbs;
        private int[] mSeqsValues;
        private int mMinWidth = 0;
        private int mMinHeight = 0;

        public frmExport(Form1 frm, string logName)
        {
            InitializeComponent();
            mForm1 = frm;
            mLogName = logName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExport_Load(object sender, EventArgs e)
        {
            mMinWidth = this.Width;
            mMinHeight = this.Height;

            mSeqs = new NumericUpDown[] { LognameSeq, QSONumSeq, DateSeq, UTCSeq, UTCEndSeq,
                                          CallSeq, FreqSeq, ModeSeq, TxSeq, RxSeq, AwardsSeq,
                                          QSLOutSeq, QSLInSeq, PowerSeq, NameSeq, QTHSeq,
                                          LocatorSeq, Unknown1Seq, Unknown2Seq, RemarksSeq,
                                          CalcBearingSeq, CountrySeq, StateSeq, CountySeq };
            mCbs = new CheckBox[] { cbExportLogname, cbExportQSONum, cbExportDate, cbExportUTC,
                                    cbExportUTCend, cbExportCall, cbExportFrequency, cbExportMode,
                                    cbExportTx, cbExportRx, cbExportAwards, cbQslOut, cbQslIn,
                                    cbPower, cbName, cbQTH, cbLocator, cbUnknown1, cbUnknown2,
                                    cbRemarks, cbCalcBearing, cbCountry, cbState, cbCounty };
            mSeqsValues = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExport_Click(object sender, EventArgs e)
        {
            string del = "";
            string fileName = "";

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.SupportMultiDottedExtensions = true;
            saveFileDialog1.Title = "Select CSV file to Export to";
            DialogResult dr = saveFileDialog1.ShowDialog();
            Application.DoEvents();
            if (dr == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                lblFileName.Text = fileName;

                del = cmbDel.Text;
                if (del.Trim().Length == 0)
                {
                    MessageBox.Show("You must select a delimiter", mForm1.ProgramName + " - Input Error");
                }

                else
                {
                    if (dateTimePicker1.Value >= dateTimePicker2.Value)
                    {
                        MessageBox.Show("You must select valid dates.", mForm1.ProgramName + " - Input Error");
                    }
                    else
                    {
                        zExport(del, fileName, dateTimePicker1.Value, dateTimePicker2.Value, tbUse.Text.Trim());
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="del"></param>
        /// <param name="fileName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void zExport(string del, string fileName, DateTime startDate, DateTime endDate, string useLogName)
        {
            DataTable exportRows = null;
            string line = "";
            ArrayList list = new ArrayList();
            double dVal = 0.0;

            exportRows = mForm1.mDac.GetExportData(mLogName, fileName, cbExportLogname.Checked, cbExportQSONum.Checked,
                                        cbExportDate.Checked, cbExportUTC.Checked, cbExportUTCend.Checked,
                                        cbExportCall.Checked, cbExportFrequency.Checked, cbExportMode.Checked,
                                        cbExportTx.Checked, cbExportRx.Checked, cbExportAwards.Checked,
                                        cbQslOut.Checked, cbQslIn.Checked, cbPower.Checked, cbName.Checked,
                                        cbQTH.Checked, cbLocator.Checked, cbUnknown1.Checked, cbUnknown2.Checked,
                                        cbRemarks.Checked, cbCountry.Checked, cbState.Checked, cbCounty.Checked,
                                        cbCalcBearing.Checked, startDate, endDate);

            if (exportRows != null)
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                progressBar1.Maximum = exportRows.Rows.Count;

                //Build array of field names in the correct order.
                
                for (int idx = 0; idx < mSeqs.Length; idx++)
                {
                    if (mSeqs[idx].Value > 0)
                    {
                        list.Add(mSeqs[idx]);
                    }
                }
                //Sort the list
                for (int idx = 0; idx < list.Count - 1; idx++)
                {
                    for (int jdx = idx + 1; jdx < list.Count; jdx++)
                    {
                        if (((NumericUpDown)list[idx]).Value > ((NumericUpDown)list[jdx]).Value)
                        {
                            object temp = list[idx];
                            list[idx] = list[jdx];
                            list[jdx] = temp;
                        }
                    }
                }

                fileName = lblFileName.Text;
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                //And finally write the delimited file.
                using (StreamWriter fs = new StreamWriter(fileName))
                {
                    //Write the heading line
                    line = "";
                    for (int idx = 0; idx < list.Count; idx++)
                    {
                        line += ((NumericUpDown)list[idx]).Tag.ToString() + del;
                    }
                    line = line.Remove(line.Length - 1, 1);
                    fs.WriteLine(line);

                    foreach (DataRow row in exportRows.Rows)
                    {
                        line = "";
                        for (int idx = 0; idx < list.Count; idx++)
                        {
                            //Some fields need to have the values modified...
                            // such as formating date fields and logname.
                            string fld = ((NumericUpDown)list[idx]).Tag.ToString();
                            string val = "";
                            switch (fld)
                            {
                                case "StartDate":
                                    val = ((DateTime)row[fld]).Date.ToString("MM/dd/yyyy");
                                    break;
                       
                                case "UTC":
                                    val = ((DateTime)row["StartDate"]).ToString("HH:mm");
                                    break;

                                case "EndDate":
                                    val = ((DateTime)row["EndDate"]).ToString("MM/dd/yyyy HH:mm");
                                    break;

                                case "LogName":
                                    val = (useLogName.Length > 0) ? useLogName : row["LogName"].ToString();
                                    break;

                                case "Frequency":
                                    string newVal = "";
                                    val = row["Frequency"].ToString();
                                    if (double.TryParse(val, out dVal))
                                    {
                                        newVal = mForm1.mDac.ConvertFreqToBand(dVal);
                                    }
                                    if ((newVal != null) && (newVal.Trim().Length > 0)) { val = newVal; }
                                    break;

                                default:
                                    val = row[((NumericUpDown)list[idx]).Tag.ToString()].ToString();
                                    break;
                            }
                            
                            line += val + del;
                        }
                        line = line.Remove(line.Length - 1, 1);
                        fs.WriteLine(line);
                        progressBar1.Value++;
                        Application.DoEvents();
                    }
                }

                progressBar1.Visible = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nothing was returned to be exported", mForm1.ProgramName + " - Information");
            }
        }
        /// <summary>
        /// A NumericUpDown has changed value.  We need to resequene the
        /// values of the other checked NumericUpDowns.  BUT we must not
        /// let a value go below 0 or above what should be the maximum.
        /// If a value goes to zero, then we uncheck the checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LognameSeq_ValueChanged(object sender, EventArgs e)
        {
            int thisControlIndex = -1;
            int direction = 0;
            int value = -1;
            int fieldCount = 0;

            //Get the index of this item
            for (int idx = 0; idx < mSeqs.Length; idx++)
            {
                if (mSeqs[idx] == sender) { thisControlIndex = idx; }
                if (mSeqs[idx].Value > 0) { fieldCount++; }
            }

            //Set the value of the NumericUpDown that has been selected.
            value = (int)((NumericUpDown)sender).Value;
            if (value > fieldCount) { value = fieldCount; }
            direction = value - mSeqsValues[thisControlIndex];
            mSeqsValues[thisControlIndex] = value;
            mSeqs[thisControlIndex].Value = value;
            if (value == 0)
            {
                mCbs[thisControlIndex].Checked = false;
                for (int idx = 0; idx < mSeqs.Length; idx++)
                {
                    if (mSeqs[idx].Value != 0)
                    {
                        mSeqs[idx].Value--;
                    }
                }
            }

            //Run through each of the Seq controls.
            for (int idx = 0; idx < mSeqs.Length; idx++)
            {
                //Only worry about the items that are enabled.
                if (mSeqs[idx].Enabled)
                {
                    //Don't do anything to the selected NumericUpDown...
                    //   and only deal with the the items that are have the same value as the selected item.
                    if ((mSeqs[idx] != sender) && (value == (int)((NumericUpDown)mSeqs[idx]).Value))
                    {
                        //Change the value of NumericUpDown with the selected value.
                        int newValue = (int)((NumericUpDown)mSeqs[idx]).Value - direction;
                        if (newValue == 0)
                        {
                            mCbs[idx].Checked = false;
                            mSeqs[idx].Value = 0;
                            mSeqs[idx].Enabled = false;
                        }
                        else
                        {
                            ((NumericUpDown)mSeqs[idx]).Value = ((NumericUpDown)mSeqs[idx]).Value - direction;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A checkbox has changed state. Either enable or disable the seqence
        /// box. If disableing we need to set the value to 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbExportLogname_CheckedChanged(object sender, EventArgs e)
        {
            int thisControlIndex = -1;
            int maxValue = 0;
            for (int idx = 0; idx < mCbs.Length; idx++)
            {
                if (mCbs[idx] == sender)
                {
                    thisControlIndex = idx;
                }
                if (((int)mSeqs[idx].Value) > maxValue)
                {
                    maxValue = (int)mSeqs[idx].Value;
                }
            }

            mSeqs[thisControlIndex].Enabled = mCbs[thisControlIndex].Checked;
            if (!mSeqs[thisControlIndex].Enabled)
            {
                mSeqs[thisControlIndex].Value = 0;
                mSeqsValues[thisControlIndex] = 0;
            }
            else
            {
                mSeqs[thisControlIndex].Value = maxValue + 1;
            }
        }

        /// <summary>
        /// The form is resizing, don't go below the minimums.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExport_Resize(object sender, EventArgs e)
        {
            if (this.Width < mMinWidth) { this.Width = mMinWidth; }
            if (this.Height < mMinHeight) { this.Height = mMinHeight; }
        }

        //=================================================================
        // Information on ADIF and ADX can be found at:                  ==
        // http://www.adif.org/306/ADIF_306.htm#Introduction_Development ==
        //=================================================================

        //-----------------------
        // Sample ADIF exports --
        //-----------------------
        //<adif_ver:5>3.0.5
        //<programid:7>monolog
        //<USERDEF1:14:N> EPC
        //<USERDEF2:19:E>SweaterSize,{ S,M,L}
        //<USERDEF3:15:N>ShoeSize,{5:20}
        //<EOH>
        //
        //<qso_date:8>19900620
        //<time_on:4>1523
        //<call:5>VK9NS
        //<band:3>20M
        //<mode:4>RTTY
        //<sweatersize:1> M
        //<shoesize:2>11
        //<app_monolog_compression:3>off
        //<eor>
        //
        //<qso_date:8>20101022
        //<time_on:4>0111
        //<call:5>ON4UN
        //<band:3>40M
        //<mode:3>PSK
        //<submode:5> PSK63
        //<epc:5>32123
        //<app_monolog_compression:3>off
        //<eor>

        //-------------------
        // Sample ADX file --
        //-------------------
        //<?xml version = "1.0" encoding="UTF-8"?>
        //<ADX>
        //    <HEADER>
        //        <!--Generated on 2011-11-22 at 02:15:23Z for WN4AZY-->
        //        <ADIF_VER>3.0.5</ADIF_VER>
        //        <PROGRAMID>monolog</PROGRAMID>
        //        <USERDEF FIELDID = "1" TYPE="N">EPC</USERDEF>
        //        <USERDEF FIELDID = "2" TYPE="E" ENUM="{S,M,L}">SWEATERSIZE</USERDEF>
        //        <USERDEF FIELDID = "3" TYPE="N" RANGE="{5:20}">SHOESIZE</USERDEF>
        //    </HEADER>
        //    <RECORDS>
        //        <RECORD>
        //            <QSO_DATE>19900620</QSO_DATE>
        //            <TIME_ON>1523</TIME_ON>
        //            <CALL>VK9NS</CALL>
        //            <BAND>20M</BAND>
        //            <MODE>RTTY</MODE>
        //            <USERDEF FIELDNAME = "SWEATERSIZE" > M </ USERDEF >
        //            <USERDEF FIELDNAME="SHOESIZE">11</USERDEF>
        //            <APP PROGRAMID = "MONOLOG" FIELDNAME="Compression" TYPE="s">off</APP>
        //        </RECORD>
        //        <RECORD>
        //            <QSO_DATE>20101022</QSO_DATE>
        //            <TIME_ON>0111</TIME_ON>
        //            <CALL>ON4UN</CALL>
        //            <BAND>40M</BAND>
        //            <MODE>PSK</MODE>
        //            <SUBMODE>PSK63</SUBMODE>
        //            <USERDEF FIELDNAME = "EPC" > 32123 </ USERDEF >
        //            <APP PROGRAMID="MONOLOG" FIELDNAME="COMPRESSION" TYPE="s">off</APP>
        //        </RECORD>
        //    </RECORDS>
        //</ADX>
    }
}
