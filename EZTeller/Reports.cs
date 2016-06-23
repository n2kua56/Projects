using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZUtils;
using EZDeskDataLayer;

namespace EZTeller
{
    public class Reports
    {
        private Form1 mfrm = null;
        private object mOfficeFalseValue = false;
        private object mOfficeTrueValue = true;
        private object mOfficeMissing = Type.Missing;
        private string mModName = "EZTeller_Reports";

        ////private Microsoft.Office.Interop.Word.Application mWordApp = null;
        ////private Microsoft.Office.Interop.Word.Document mWordDoc = null;
        ////private object mOfficeReplaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
        private bool mWordAvailable = false;

        public bool WordAvailable
        {
            get { return mWordAvailable; }
        }

        public Reports(Form1 frm)
        {
            mfrm = frm;
            ////try
            ////{
            ////    mWordApp = new Microsoft.Office.Interop.Word.Application();
            ////    mWordAvailable = true;
            ////}
            ////catch
            ////{
                mWordAvailable = false;
                mfrm = frm;
            ////}
        }

        /// <summary>
        /// 
        /// </summary>
        ////public void ContributorYearlyReport(Form1 frm)
        ////{
        ////    ////Microsoft.Office.Interop.Word.Document doc = null;
        ////    Dictionary<string, string> findandreplace = new Dictionary<string, string>();
        ////    string printerName = "";
        ////    int numToPrint = -1;
        ////    string year = "";
        ////    string name = "";
        ////    int idx = -1;
        ////    DataTable yearGiving = null;

        ////    Trace.Enter(Trace.RtnName(mModName, "zMergeMenus"));

        ////    try
        ////    {
        ////        frmContributorReport rptfrm = new frmContributorReport(frm);
        ////        DialogResult dresult = rptfrm.ShowDialog();
        ////        if (dresult == DialogResult.OK)
        ////        {
        ////            year = rptfrm.SelectedYear;
        ////            numToPrint = rptfrm.NumberToPrint;
        ////            if (numToPrint == 0)
        ////            {
        ////                numToPrint = 99999;
        ////            }

        ////            PrintDialog pDialog = new PrintDialog();
        ////            pDialog.AllowSelection = true;
        ////            pDialog.AllowCurrentPage = true;
        ////            pDialog.AllowPrintToFile = true;
        ////            pDialog.AllowSomePages = true;
        ////            pDialog.ShowHelp = true;
        ////            pDialog.ShowNetwork = true;
        ////            pDialog.UseEXDialog = true;

        ////            // Display the dialog. This returns true if the user presses the Print button.
        ////            DialogResult print = pDialog.ShowDialog();
        ////            if (print == DialogResult.OK)
        ////            {
        ////                printerName = pDialog.PrinterSettings.PrinterName;

        ////                mWordApp = new Microsoft.Office.Interop.Word.Application();
        ////                mWordApp.Visible = false;

        ////                yearGiving = frm.EZTellerDataLayer.GetYearlyGiving(year);
        ////                for (idx = 0; ((idx < yearGiving.Rows.Count) && (idx < numToPrint)); idx++)
        ////                {
        ////                    name = yearGiving.Rows[idx]["Title"].ToString();
        ////                    if (name.Length > 0)
        ////                    {
        ////                        name += " ";
        ////                    }
        ////                    name += yearGiving.Rows[idx]["FirstName"].ToString();
        ////                    if (yearGiving.Rows[idx]["FirstName"].ToString().Length > 0)
        ////                    {
        ////                        name += " ";
        ////                    }
        ////                    name += yearGiving.Rows[idx]["LastName"].ToString();

        ////                    doc = zOpenWordDoc("c:\\test.doc");
        ////                    doc.Activate();

        ////                    findandreplace.Clear();
        ////                    findandreplace.Add("[FullName]", name);
        ////                    findandreplace.Add("[Title]", yearGiving.Rows[idx]["Title"].ToString());
        ////                    findandreplace.Add("[FirstName]", yearGiving.Rows[idx]["FirstName"].ToString());
        ////                    findandreplace.Add("[LastName]", yearGiving.Rows[idx]["LastName"].ToString());
        ////                    findandreplace.Add("[Address1]", yearGiving.Rows[idx]["Address1"].ToString());
        ////                    findandreplace.Add("[Address2]", yearGiving.Rows[idx]["Address2"].ToString());
        ////                    findandreplace.Add("[City]", yearGiving.Rows[idx]["City"].ToString());
        ////                    findandreplace.Add("[State]", yearGiving.Rows[idx]["State"].ToString());
        ////                    findandreplace.Add("[Zip]", yearGiving.Rows[idx]["Zip"].ToString());
        ////                    findandreplace.Add("[General]", yearGiving.Rows[idx]["General"].ToString());
        ////                    findandreplace.Add("[Building]", yearGiving.Rows[idx]["Building"].ToString());
        ////                    findandreplace.Add("[Missions]", yearGiving.Rows[idx]["Missions"].ToString());
        ////                    findandreplace.Add("[Designated]", yearGiving.Rows[idx]["Designated"].ToString());
        ////                    findandreplace.Add("[TotalGiving]", yearGiving.Rows[idx]["TotalGiving"].ToString());
        ////                    findandreplace.Add("[Today-yyyy-mm-dd]", DateTime.Now.ToString("yyyy-MM-dd"));
        ////                    findandreplace.Add("[TodayShort]", DateTime.Now.ToShortDateString());
        ////                    findandreplace.Add("[TodayLong]", DateTime.Now.ToLongDateString());
        ////                    findandreplace.Add("[Year]", DateTime.Now.ToString("yyyy"));

        ////                    zFindAndReplace(mWordApp, findandreplace);

        ////                    zPrintWordDoc(mWordApp, doc, printerName);
        ////                    doc.Close(ref mOfficeFalseValue, ref mOfficeMissing, ref mOfficeMissing);
        ////                }
        ////            }
        ////        }
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        EZException EZex = new EZException("Failed ", ex);
        ////        throw EZex;
        ////    }

        ////    finally
        ////    {
        ////        Trace.Exit();
        ////        try
        ////        {
        ////            if (doc != null)
        ////            {
        ////                doc.Close(ref mOfficeFalseValue, ref mOfficeMissing, ref mOfficeMissing);
        ////            }
        ////            if (mWordApp != null)
        ////            {
        ////                mWordApp.Quit(ref mOfficeFalseValue, ref mOfficeMissing, ref mOfficeMissing);
        ////            }
        ////            //if (doc != null)
        ////            //{
        ////            //    doc.Close(ref mOfficeMissing, ref mOfficeMissing, ref mOfficeMissing);
        ////            //    Marshal.ReleaseComObject(doc);
        ////            //}
        ////            //if (mWordApp != null)
        ////            //{
        ////            //    mWordApp.Quit(ref mOfficeMissing, ref mOfficeMissing, ref mOfficeMissing);
        ////            //    Marshal.ReleaseComObject(mWordApp);
        ////            //}
        ////        }
        ////        catch
        ////        {
        ////        }
        ////    }

        ////}

        /// <summary>
        /// Microsoft Word interface
        ///   word.Documents.Open(
        ///      ref Object fileName,          (object fileName = "c:\\test.doc";)
        ///      ref ConfirmConversions,       (object mOfficeMissing)
        ///      ref ReadOnlyAttribute,        (object mOfficeTrue)
        ///      ref AddToRecentFiles,         (object mOfficeMissing)
        ///      ref PasswordDocument,         (object mOfficeMissing)
        ///      ref PasswordTemplate,         (object mOfficeMissing)
        ///      ref Revert,                   (object mOfficeMissing)
        ///      ref WritePasswordDocument,    (object mOfficeMissing)
        ///      ref WritePasswordTemplate,    (object mOfficeMissing)
        ///      ref FormatException,          (object mOfficeMissing)
        ///      ref Encoding,                 (object mOfficeMissing)
        ///      ref Visible,                  (object mOfficeMissing)
        ///      ref OpenAndRepair,            (object mOfficeMissing)
        ///      ref DocumentDirection,        (object mOfficeMissing)
        ///      ref NoEncodingDialog,         (object mOfficeMissing)
        ///      ref XMLTransform);            (object mOfficeMissing)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        ////private Microsoft.Office.Interop.Word.Document zOpenWordDoc(string fileName)
        ////{
        ////    Microsoft.Office.Interop.Word.Document rtn = null;
        ////    object wordFileName = fileName;

        ////    Trace.Enter("FileName: " + fileName);

        ////    try
        ////    {
        ////        rtn = mWordApp.Documents.Open(ref wordFileName,     //Doc file to open 
        ////                                ref mOfficeMissing,         //ConfirmConversions 
        ////                                ref mOfficeTrueValue,       //ReadOnlyAttribute 
        ////                                ref mOfficeMissing,        //AddToRecentFiles 
        ////                                ref mOfficeMissing,         //PasswordDocument 
        ////                                ref mOfficeMissing,         //PasswordTemplate 
        ////                                ref mOfficeMissing,         //Revert 
        ////                                ref mOfficeMissing,         //WritePasswordDocument 
        ////                                ref mOfficeMissing,         //WritePasswordTemplate 
        ////                                ref mOfficeMissing,         //FormatException 
        ////                                ref mOfficeMissing,         //Encoding 
        ////                                ref mOfficeMissing,         //Visible 
        ////                                ref mOfficeMissing,         //OpenAndRepair 
        ////                                ref mOfficeMissing,         //DocumentDirection
        ////                                ref mOfficeMissing,         //NoEncodingDialog 
        ////                                ref mOfficeMissing);        //XMLTransform 
        ////        return rtn;
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        EZException EZex = new EZException("Failed ", ex);
        ////        throw EZex;
        ////    }

        ////    finally
        ////    {
        ////        Trace.Exit();
        ////    }
        ////}

        /// <summary>
        /// </summary>
        /// <remarks>
        /// word.Selection.Find.Execute(ref FindText,
        ///                             ref MatchCash,
        ///                             ref matchWholeWord,
        ///                             ref MatchWildcards,
        ///                             ref MatchSoundsLike,
        ///                             ref MatchAllWordForms,
        ///                             ref Forward,
        ///                             ref Wrap,
        ///                             ref Format,
        ///                             ref replaceWith,
        ///                             ref Replace,
        ///                             ref MatchKashida,
        ///                             ref MatchDiacritics,
        ///                             ref MatchAlertHamza,
        ///                             ref MatchControl);
        /// </remarks>
        /// <param name="word"></param>
        /// <param name="findandreplace"></param>
        ////private Microsoft.Office.Interop.Word.Application
        ////        zFindAndReplace(Microsoft.Office.Interop.Word.Application word,
        ////                        Dictionary<string, string> findandreplace)
        ////{
        ////    Trace.Enter();

        ////    try
        ////    {
        ////        word.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

        ////        foreach (KeyValuePair<string, string> pair in findandreplace)
        ////        {
        ////            object findme = pair.Key;
        ////            object replacewithme = pair.Value;

        ////            word.Selection.Find.Execute(ref findme,         //FindText
        ////                                ref mOfficeFalseValue,      //MatchCash
        ////                                ref mOfficeTrueValue,       //matchWholeWord
        ////                                ref mOfficeMissing,         //MatchWildcards
        ////                                ref mOfficeMissing,         //MatchSoundsLike
        ////                                ref mOfficeMissing,         //MatchAllWordForms
        ////                                ref mOfficeMissing,         //Forward
        ////                                ref mOfficeMissing,         //Wrap
        ////                                ref mOfficeMissing,         //Format
        ////                                ref replacewithme,          //replaceWith
        ////                                ref mOfficeReplaceAll,      //Replace
        ////                                ref mOfficeMissing,         //MatchKashida
        ////                                ref mOfficeMissing,         //MatchDiacritics
        ////                                ref mOfficeMissing,         //MatchAlertHamza
        ////                                ref mOfficeMissing);        //MatchControl
        ////        }
        ////        return word;
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        EZException EZex = new EZException("Build Connection String Failed ", ex);
        ////        throw EZex;
        ////    }

        ////    finally
        ////    {
        ////        Trace.Exit();
        ////    }
        ////}

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// doc.PrintOut(ref Background,
        ///                    ref Append,
        ///                    ref Range,
        ///                    ref OutputFileName,
        ///                    ref Form,
        ///                    ref To,
        ///                    ref Item,
        ///                    ref Copies,
        ///                    ref Pages,
        ///                    ref PageType,
        ///                    ref PrintToFile,
        ///                    ref Collate,
        ///                    ref ActivePrinterMacGX,
        ///                    ref ManualDuplexPrint,
        ///                    ref PrintZoomColumn,
        ///                    ref PrintZoomRow,
        ///                    ref PrintZoomPaperHeight);
        /// </remarks>
        /// <param name="word"></param>
        ////private void zPrintWordDoc(Microsoft.Office.Interop.Word.Application word,
        ////                            Microsoft.Office.Interop.Word.Document doc,
        ////                            string printerName)
        ////{
        ////    word.ActivePrinter = printerName;
        ////    doc.PrintOut(ref mOfficeMissing,       //Background 
        ////                 ref mOfficeMissing,       //Append
        ////                 ref mOfficeMissing,       //Range
        ////                 ref mOfficeMissing,       //OutputFileName
        ////                 ref mOfficeMissing,       //Form
        ////                 ref mOfficeMissing,       //To
        ////                 ref mOfficeMissing,       //Item
        ////                 ref mOfficeMissing,       //Copies
        ////                 ref mOfficeMissing,       //Pages
        ////                 ref mOfficeMissing,       //PageType
        ////                 ref mOfficeMissing,       //PrintToFile
        ////                 ref mOfficeMissing,       //Collate
        ////                 ref mOfficeMissing,       //ActivePrinterMacGX
        ////                 ref mOfficeMissing,       //ManualDuplexPrint
        ////                 ref mOfficeMissing,       //PrintZoomColumn
        ////                 ref mOfficeMissing,       //PrintZoomRow
        ////                 ref mOfficeMissing,        //PrintZoomPaperWidth
        ////                 ref mOfficeMissing);       //PrintZoomPaperHeight
        ////}

        /////// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="CashCreditGeneral"></param>
        /// <param name="CashCreditBuilding"></param>
        /// <param name="CashCreditMissions"></param>
        /// <param name="CashCreditDesignated"></param>
        /// <param name="CashCreditTotal"></param>
        /// <param name="CashLooseGeneral"></param>
        /// <param name="CashLooseBuilding"></param>
        /// <param name="CashLooseMissions"></param>
        /// <param name="CashLooseDesignated"></param>
        /// <param name="CashLooseTotal"></param>
        /// <param name="CashNonCreditGeneral"></param>
        /// <param name="CashNonCreditBuilding"></param>
        /// <param name="CashNonCreditMissions"></param>
        /// <param name="CashNonCreditDesignated"></param>
        /// <param name="CashNonCreditTotal"></param>
        /// <param name="CashTotalGeneral"></param>
        /// <param name="CashTotalBuilding"></param>
        /// <param name="CashTotalMissions"></param>
        /// <param name="CashTotalDesignated"></param>
        /// <param name="CashTotalTotal"></param>
        /// <param name="CheckCreditGeneral"></param>
        /// <param name="CheckCreditBuilding"></param>
        /// <param name="CheckCreditMissions"></param>
        /// <param name="CheckCreditDesignated"></param>
        /// <param name="CheckCreditTotal"></param>
        /// <param name="CheckNonCreditGeneral"></param>
        /// <param name="CheckNonCreditBuilding"></param>
        /// <param name="CheckNonCreditMissions"></param>
        /// <param name="CheckNonCreditDesignated"></param>
        /// <param name="CheckNonCreditTotal"></param>
        /// <param name="CheckGeneralTotal"></param>
        /// <param name="CheckBuildingTotal"></param>
        /// <param name="CheckMissionsTotal"></param>
        /// <param name="CheckDesignatedTotal"></param>
        /// <param name="CheckTotalTotal"></param>
        /// <param name="GeneralTotal"></param>
        /// <param name="BuildingTotal"></param>
        /// <param name="MissionsTotal"></param>
        /// <param name="DesignatedTotal"></param>
        /// <param name="TotalTotal"></param>
        public void SummaryReport(string title, string CashCreditGeneral,
                                string CashCreditBuilding, string CashCreditMissions,
                                string CashCreditDesignated, string CashCreditTotal,
                                string CashLooseGeneral, string CashLooseBuilding,
                                string CashLooseMissions, string CashLooseDesignated,
                                string CashLooseTotal,
                                string CashNonCreditGeneral, string CashNonCreditBuilding,
                                string CashNonCreditMissions, string CashNonCreditDesignated,
                                string CashNonCreditTotal,
                                string CashTotalGeneral, string CashTotalBuilding,
                                string CashTotalMissions, string CashTotalDesignated,
                                string CashTotalTotal,
                                string CheckCreditGeneral, string CheckCreditBuilding,
                                string CheckCreditMissions, string CheckCreditDesignated,
                                string CheckCreditTotal,
                                string CheckNonCreditGeneral, string CheckNonCreditBuilding,
                                string CheckNonCreditMissions, string CheckNonCreditDesignated,
                                string CheckNonCreditTotal,
                                string CheckGeneralTotal, string CheckBuildingTotal,
                                string CheckMissionsTotal, string CheckDesignatedTotal,
                                string CheckTotalTotal,
                                string GeneralTotal, string BuildingTotal,
                                string MissionsTotal, string DesignatedTotal,
                                string TotalTotal)
        {
            string line = "";
            string fileName = "";

            Trace.Enter(Trace.RtnName(mModName, "SummaryReport"));

            try
            {
                line = "<HTML>" + "\n" +
                       "  <HEAD>" + "\n" +
                       "    <TITLE>SUMMARY REPORT</TITLE>" + "\n" +
                       "  </HEAD>" + "\n" +
                       "  <BODY>" + "\n" +
                       "    <TABLE BORDER=\"1\" WIDTH=80%>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TH><center><B><SIZE=\"+2\">SUMMARY REPORT</SIZE></B></center></TH>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD> </TD>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TH><center><B><SIZE=\"+2\">" + title + "</SIZE></B></center></TH>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD>&nbsp;</TD>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD>" + "\n" +
                       "          <TABLE BORDER=\"1\">" + "\n" +
                       "            <TR>\n" +
                       "              <TH ALIGN=\"LEFT\"><B>Cash:</B></TH>\n" +
                       "              <TH WIDTH=16%><B>General</B></TH>\n" +
                       "              <TH WIDTH=16%><B>Building</B></TH>\n" +
                       "              <TH WIDTH=16%><B>Missions</B></TH>\n" +
                       "              <TH WIDTH=16%><B>Designated</B></TH>\n" +
                       "              <TH WIDTH=16%><B>Total</B></TH>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;Credit</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashCreditGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashCreditBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashCreditMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashCreditDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashCreditTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;Loose</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashLooseGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashLooseBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashLooseMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashLooseDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashLooseTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;Non-Credit</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashNonCreditGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashNonCreditBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashNonCreditMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashNonCreditDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashNonCreditTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;&nbsp;&nbsp;<B>Total</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashTotalGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashTotalBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashTotalMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashTotalDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CashTotalTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD COLSPAN=6> </TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TH ALIGN=\"LEFT\"><B>Check:</B></TH>\n" +
                       "              <TH WIDTH=16%><B>&nbsp;</B></TH>\n" +
                       "              <TH WIDTH=16%><B>&nbsp;</B></TH>\n" +
                       "              <TH WIDTH=16%><B>&nbsp;</B></TH>\n" +
                       "              <TH WIDTH=16%><B>&nbsp;</B></TH>\n" +
                       "              <TH WIDTH=16%><B>&nbsp;</B></TH>\n" +
                       "            </TR>\n" +

                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;Credit</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckCreditGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckCreditBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckCreditMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckCreditDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckCreditTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;Non-Credit</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckNonCreditGeneral + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckNonCreditBuilding + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckNonCreditMissions + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckNonCreditDesignated + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckNonCreditTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\">&nbsp;&nbsp;&nbsp;&nbsp;<B>Total</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckGeneralTotal + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckBuildingTotal + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckMissionsTotal + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckDesignatedTotal + "</TD>\n" +
                       "              <TD ALIGN=\"RIGHT\">" + CheckTotalTotal + "</TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\"> </B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"> </TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"> </TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"> </TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"> </TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"> </TD>\n" +
                       "            </TR>\n" +
                       "            <TR>\n" +
                       "              <TD ALIGN=\"LEFT\"><B>Total</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"><B>" + GeneralTotal + "</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"><B>" + BuildingTotal + "</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"><B>" + MissionsTotal + "</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"><B>" + DesignatedTotal + "</B></TD>\n" +
                       "              <TD ALIGN=\"RIGHT\"><B>" + TotalTotal + "</B></TD>\n" +
                       "            </TR>" + "\n";
                line += zEndOfStandardHTMLreport();

                zFileCleanup("Summary");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.Summary." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("SummaryReport Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "SummaryReport"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="title"></param>
        public void DetailReport(Form1 frm, string title)
        {
            int idx = -1;
            DataGridViewRow row = null;
            string line = "";
            string fileName = "";
            string Env = "";
            string Name = "";
            string Ck = "";
            string Comment = "";

            Trace.Enter(Trace.RtnName(mModName, "DetailReport"));

            try
            {
                line = "<HTML>" + "\n" +
                       "  <HEAD>" + "\n" +
                       "    <TITLE>DETAIL REPORT</TITLE>" + "\n" +
                       "  </HEAD>" + "\n" +
                       "  <BODY>" + "\n" +
                       "    <TABLE BORDER=\"1\" WIDTH=90%>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TH><center><B><SIZE=\"+2\">DETAIL REPORT</SIZE></B></center></TH>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD> </TD>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TH><center><B><SIZE=\"+2\">" + title + "</SIZE></B></center></TH>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD>&nbsp;</TD>" + "\n" +
                       "      </TR>" + "\n" +
                       "      <TR>" + "\n" +
                       "        <TD>" + "\n" +
                       "          <TABLE BORDER=\"1\">" + "\n" +
                       "            <TR>" + "\n" +
                       "              <TH WIDTH=5%><B>Seq</B></TH>" + "\n" +
                       "              <TH WIDTH=5%><B>Env</B></TH>" + "\n" +
                       "              <TH WIDTH=20%><B>Name</B></TH>" + "\n" +
                       "              <TH WIDTH=5%><B>Ck</B></TH>" + "\n" +
                       "              <TH WIDTH=10%><B>General</B></TH>" + "\n" +
                       "              <TH WIDTH=10%><B>Building</B></TH>" + "\n" +
                       "              <TH WIDTH=10%><B>Missions</B></TH>" + "\n" +
                       "              <TH WIDTH=10%><B>Designated</B></TH>" + "\n" +
                       "              <TH WIDTH=10%><B>Total</B></TH>" + "\n" +
                       "              <TH WIDTH=15%><B>Comments</B></TH>" + "\n" +
                       "            </TR>" + "\n";

                for (idx = 0; idx < frm.dgvDetail.Rows.Count; idx++)
                {
                    row = frm.dgvDetail.Rows[idx];
                    Env = row.Cells["Env"].Value.ToString();
                    if (Env.Length == 0) { Env = "&nbsp;"; }
                    Name = row.Cells["Name"].Value.ToString();
                    if (Name.Length == 0) { Name = "&nbsp;"; }
                    Ck = row.Cells["Ck"].Value.ToString();
                    if (Ck.Length == 0) { Ck = "&nbsp;"; }
                    Comment = row.Cells["Comments"].Value.ToString();
                    if (Comment.Length == 0) { Comment = "&nbsp;"; }

                    line +=
                        "            <TR> " + "\n" +
                        "              <TD ALIGN=\"LEFT\">" + row.Cells["Seq"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"LEFT\">" + Env + "</TD>" + "\n" +
                        "              <TD ALIGN=\"LEFT\">" + Name + "</TD>" + "\n" +
                        "              <TD ALIGN=\"LEFT\">" + Ck + "</TD>" + "\n" +
                        "              <TD ALIGN=\"RIGHT\">" + row.Cells["General"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"RIGHT\">" + row.Cells["Building"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"RIGHT\">" + row.Cells["Missions"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"RIGHT\">" + row.Cells["Designated"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"RIGHT\">" + row.Cells["Total"].Value.ToString() + "</TD>" + "\n" +
                        "              <TD ALIGN=\"LEFT\">" + Comment + "</TD>" + "\n" +
                        "            </TR>" + "\n";
                }

                line += zEndOfStandardHTMLreport();

                zFileCleanup("Detail");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.Detail." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("DetailReport Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "DetailReport"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rpt"></param>
        private void zFileCleanup(string rpt)
        {
            DirectoryInfo di = null;
            FileInfo[] files = null;

            di = new DirectoryInfo(Path.GetTempPath());
            files = di.GetFiles("EZTeller." + rpt + ".*.html");
            foreach (FileInfo fi in files)
            {
                try { fi.Delete(); }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void GivingReportPrint(Form1 frm)
        {
            string line = "";
            string fileName = "";
            int idx = -1;

            Trace.Enter(Trace.RtnName(mModName, "GivingReportPrint"));

            try
            {
                line = zStartOfStandardHTMLreport("GIVING SUMMARY REPORT");

                line += "            <TR>" + "\n";

                if (frm.rbQuarterly.Checked)
                {
                    line += "              <TH ALIGN=\"LEFT\" WIDTH=10%><B>Quarter</B></TH>\n";
                }
                else
                {
                    line += "              <TH ALIGN=\"LEFT\" WIDTH=10%><B>Month</B></TH>\n";
                }

                line += "              <TH WIDTH=20%><B>" + frm.dgGivingSummary.Columns[1].Name + "</B></TH>\n";

                if (frm.dgGivingSummary.Columns.Count == 3)
                {
                    line += "              <TH WIDTH=20%><B>" + frm.dgGivingSummary.Columns[2].Name + "</B></TH>\n";
                }

                line += "            </TR>" + "\n";

                for (idx = 0; idx < frm.dgGivingSummary.Rows.Count; idx++)
                {
                    line += "            <TR>\n";
                    line += "              <TD ALIGN=\"LEFT\">" + frm.dgGivingSummary.Rows[idx].Cells[0].Value.ToString() + "</TD>\n";
                    line += "              <TD ALIGN=\"RIGHT\">" + frm.dgGivingSummary.Rows[idx].Cells[1].Value.ToString() + "</TD>\n";
                    if (frm.dgGivingSummary.Columns.Count == 3)
                    {
                        line += "              <TD ALIGN=\"RIGHT\">" + frm.dgGivingSummary.Rows[idx].Cells[2].Value.ToString() + "</TD>\n";
                    }
                    line += "            </TR>\n";
                }

                line = zEndOfStandardHTMLreport();

                zFileCleanup("Giving");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.Giving." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("GivingReportPrint Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GivingReportPrint"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void PeoplePrint(Form1 frm)
        {
            string line = "";
            string fileName = "";
            string val = "";
            string align = "";
            int idx = -1;
            int cdx = -1;

            Trace.Enter(Trace.RtnName(mModName, "PeoplePrint"));

            try
            {
                line = zStartOfStandardHTMLreport("PEOPLE REPORT");
                line += "            <TR>" + "\n";

                for (idx = 0; idx < frm.dgPeople.Columns.Count; idx++)
                {
                    align = "LEFT";
                    if (idx == 7)
                    {
                        align = "RIGHT";
                    }
                    line += "              <TH ALIGN=\"" + align + "\"><B>" +
                        frm.dgPeople.Columns[idx].Name + "</B></TH>\n";
                }

                line += "            </TR>\n";

                for (idx = 0; idx < frm.dgPeople.Rows.Count; idx++)
                {
                    line += "            <TR>\n";
                    for (cdx = 0; cdx < frm.dgPeople.Columns.Count; cdx++)
                    {
                        align = "LEFT";
                        if (cdx == 7)
                        {
                            align = "RIGHT";
                        }
                        val = frm.dgPeople.Rows[idx].Cells[cdx].Value.ToString().Trim();
                        if (val.Length == 0)
                        {
                            val = "&nbsp;";
                        }
                        line += "              <TD ALIGN=\"" + align + "\">" + val + "</TD>\n";
                    }
                    line += "            </TR>\n";
                }

                line += zEndOfStandardHTMLreport();

                zFileCleanup("People");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.People." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);
            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("PeoplePrint Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "PeoplePrint"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void NoEnvelopeNumberReport(Form1 frm, int minNum)
        {
            string line = "";
            DataTable NoEnvTable = null;
            string fileName = "";
            string year = "";
            int idx = -1;

            Trace.Enter(Trace.RtnName(mModName, "NoEnvelopeNumberReport"));

            try
            {
                year = DateTime.Now.ToString("yyyy");
                NoEnvTable = mfrm.tCtrl.NoEnvelopeQuery(year, minNum);

                line = zStartOfStandardHTMLreport("NO ENVELOPE NUMBER REPORT");

                line += "            <TR>\n" +
                       "              <TH>Name</TH>\n" +
                       "              <TH>No. Contribs.</TH>\n" +
                       "              <TH>Total Giving</TH>\n" +
                       "              <TH>Last Giving</TH>\n" +
                       "            </TR>\n";
                for (idx = 0; idx < NoEnvTable.Rows.Count; idx++)
                {
                    DataRow dr = NoEnvTable.Rows[idx];
                    line += "            <TR>\n" +
                            "              <TD>" + Convert.ToString(dr["Name"]) + "</TD>\n" +
                            "              <TD>" + Convert.ToString(dr["NoContributions"]) + "</TD>\n" +
                            "              <TD>" + Convert.ToString(dr["Total"]) + "</TD>\n" +
                            "              <TD>" + Convert.ToString(dr["LastGiving"]) + "</TD>\n" +
                            "            </TR>\n";
                }
                line += zEndOfStandardHTMLreport();

                zFileCleanup("NoEnvRpt");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.NoEnvRpt." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);

            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("NoEnvelopeNumberReport Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "NoEnvelopeNumberReport"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public void EnvelopeNumberReport(Form1 frm)
        {
            string line = "";
            DataTable envTable = null;
            string fileName = "";
            int idx = -1;

            Trace.Enter(Trace.RtnName(mModName, "EnvelopeNumberReport"));

            try
            {
                envTable = mfrm.tCtrl.EnvelopeQuery();

                line = zStartOfStandardHTMLreport("ENVELOPE REPORT");

                line += "            <TR>\n" +
                       "              <TH>Envelope Number</TH>\n" +
                       "              <TH>Name</TH>\n" +
                       "            </TR>\n";
                for (idx = 0; idx < envTable.Rows.Count; idx++)
                {
                    DataRow dr = envTable.Rows[idx];
                    line += "            <TR>\n" +
                            "              <TD>" + Convert.ToString(dr["EnvNo"]) + "</TD>\n" +
                            "              <TD>" + Convert.ToString(dr["Name"]) + "</TD>\n" +
                            "            </TR>\n";
                }
                line += zEndOfStandardHTMLreport();

                zFileCleanup("EnvRpt");
                fileName = Path.Combine(Path.GetTempPath(),
                    ("EZTeller.EnvRpt." + Guid.NewGuid().ToString() + ".html"));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(line);
                }

                System.Diagnostics.Process.Start(fileName);

            }

            catch (Exception ex)
            {
                EZException EZex = new EZException("EnvelopeNumberReport Failed ", ex);
                throw EZex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "EnvelopeNumberReport"));
            }
        }


        private string zStartOfStandardHTMLreport(string rptName)
        {
            string line = "<HTML>\n" +
                       "  <HEAD>\n" +
                       "    <TITLE>" + rptName + "</TITLE>\n" +
                       "  </HEAD>\n" +
                       "  <BODY>\n" +
                       "    <TABLE BORDER=\"1\" WIDTH=100%>\n" +
                       "      <TR>\n" +
                       "        <TH><center><B><SIZE=\"+2\">" + rptName + "</SIZE></B></center></TH>\n" +
                       "      </TR>\n" +
                       "      <TR>\n" +
                       "        <TD>&nbsp;</TD>\n" +
                       "      </TR>\n" +
                       "      <TR>\n" +
                       "        <TD>\n" +
                       "          <TABLE BORDER=\"1\">\n";
            return line;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zEndOfStandardHTMLreport()
        {
            string rtn = "          </TABLE>\n" +
                       "        </TD>\n" +
                       "      </TR>\n" +
                       "      <TR>\n" +
                       "        <TD>&nbsp;</TD>\n" +
                       "      </TR>\n" +
                       "    </TABLE>\n" +
                       "  </BODY>\n" +
                       "</html>";
            return rtn;
        }
    }
}
