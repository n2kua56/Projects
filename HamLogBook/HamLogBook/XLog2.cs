using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;

namespace HamLogBook
{
    //TODO: Settings->Preferences
    //TODO: Settings->Dialogs and Windows
    //TODO: Settings->Defaults
    //TODO: Save New Log Names (new table LogNames)
    //TODO: Save Edit Log Data (new table XLogFields)
    //TODO: Add fields to the left side
    //TODO: Change the Log Entries table to include a LogNameId column)
    //TODO: Load/Build the log in the tab control
    //TODO: Implement the Add button
    //TODO: Implement the Clear button
    //TODO: Implement a popup menu for the datagrid... Edit/Delete
    //TODO: Implement double click to be Edit
    public partial class XLog2 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private static XmlDocument session = null;

        public XLog2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XLogLogEditor frm = new XLogLogEditor("something");
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.None;

            XLogNewLog frm = new XLogNewLog();
            dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                XLogLogEditor frm2 = new XLogLogEditor("new");
                frm2.ShowDialog();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Open not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Save may not be implemented.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Save As may not be implemented");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Export not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Import not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Merge not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Page Setup not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Print not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Close not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Write not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Update not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Clear All not implemented yet. Not sure what it should do.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clickAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Click All not implemented yet. Not sure what it should do.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void writeAndClickAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Write and Click All not implemented yet. Not sure what it should do.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Find not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countryMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Country Map not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Keyer not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scoringWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Scoring Window not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workedBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Worked Before not implemented yet. Not sure it will be.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showToolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Show Toolbar not implemented yet. Not sure it will be.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dupeCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Dupe Check not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findUnknownCountriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Find Unknown Countries not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void traceHamlibToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Trace Ham Lib not implemented yet. Not sure it will be.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sortByDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Sort by Date not implemented yet. Not sure it will be, should use clicking on field name.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void awardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Awards not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Defaults not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dialogsAndWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Dialogs and Windows not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Preferences not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Documentation not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dXCCListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("DXCC not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Keys not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:
            MessageBox.Show("Manual not implemented yet.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XLog2AboutBox frm = new XLog2AboutBox();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable zGetLogEntryFormat()
        {
            //TODO: Get this data from the SQL table.
            //TODO: Set event handlers
            DataTable rtn = new DataTable();

            //ControlName, Control, ControlText, Row, Col, Span, MultiRow, CenterText
            rtn.Columns.Add("ControlName", typeof(string));
            rtn.Columns.Add("Control", typeof(int));                 //0=Label, 1=Button,  2=TextBox, 3=ComboBox, 4=Checkbox
            rtn.Columns.Add("ControlText", typeof(string));
            rtn.Columns.Add("Row", typeof(int));
            rtn.Columns.Add("Col", typeof(int));
            rtn.Columns.Add("Span", typeof(int));
            rtn.Columns.Add("MultiRow", typeof(int));
            rtn.Columns.Add("CenterText", typeof(bool));

            rtn.Rows.Add("btnDate", 1, "Date", 0, 0, 1, 1, false);
            rtn.Rows.Add("tbDate", 2, "", 0, 1, 1, 1, false);
            rtn.Rows.Add("lblDate", 0, "", 0, 2, 1, 1, false);

            rtn.Rows.Add("btnUTC", 1, "UTC", 1, 0, 1, 1, false);
            rtn.Rows.Add("tbUTC", 2, "", 1, 1, 1, 1, false);
            rtn.Rows.Add("lblUTC", 0, "", 1, 2, 1, 1, false);

            rtn.Rows.Add("lblCall", 0, "Call", 2, 0, 1, 1, false);
            rtn.Rows.Add("tbCall", 2, "", 2, 1, 1, 1, false);
            rtn.Rows.Add("btnLookup", 1, "?", 2, 2, 1, 1, false);

            rtn.Rows.Add("lblMHz", 0, "MHz", 3, 0, 1, 1, false);
            rtn.Rows.Add("tbMHz", 2, "", 3, 1, 1, 1, false);
            rtn.Rows.Add("spcMHz", 0, "", 3, 2, 1, 1, false);

            rtn.Rows.Add("lblMode", 0, "Mode", 4, 0, 1, 1, false);
            rtn.Rows.Add("cbMode", 3, "SSB|CW|FM|AM|PSK31|RTTY|HELL|AMTORFEC|ASCI|ATV|CHIP64|CHIP128|" +
                                         "CLO|CONTESTI|DSTAR|DOMINO|DOMINOF|FAX|FMHELL|FSK31|FSK44|GTOR|" +
                                         "HELL80|HFSK|JT44|JT4A|JT4B|JT4C|JT4D|JT4E|JT4F|JT4G|JT9|JT9-1|" +
                                         "JT9-2|JT9-5|JT9-10|JT9-30|JT65|JT65A|JT65B|JT65C|JT6M|MFSK8|" +
                                         "MFSK16|MT63|OLIVIA|PAC|PAC2|PAC3,|PAX|PAX2|PCW|PKT|PSK10|PSK63|" +
                                         "PSK63F|PSK125|PSKAM10|PSKAM31|PSKAM50|PSKFEC31|PSKHELL|Q15|QPSK31|" +
                                         "QPSK63|QPSK125|ROS|RTTYM|SSTV|THRB|THOR|THRBX|TOR|VOI|WINMOR|WSPR", 4, 1, 1, 1, false);
            rtn.Rows.Add("spcMode", 0, "", 4, 2, 1, 1, false);

            rtn.Rows.Add("lblTX", 0, "TX(RST)", 5, 0, 1, 1, false);
            rtn.Rows.Add("tbTX", 2, "", 5, 1, 1, 1, false);
            rtn.Rows.Add("spcTX", 0, "", 5, 2, 1, 1, false);

            rtn.Rows.Add("lblRX", 0, "RX(RST)", 6, 0, 1, 1, false);
            rtn.Rows.Add("tbRX", 2, "", 6, 1, 1, 1, false);
            rtn.Rows.Add("spcRX", 0, "", 6, 2, 1, 1, false);

            rtn.Rows.Add("cbQSLSent", 4, "QSL out", 7, 0, 1, 1, false);
            rtn.Rows.Add("cbQSLin", 4, "QSL in", 7, 1, 1, 1, false);
            rtn.Rows.Add("spcQSL", 0, "", 7, 2, 1, 1, false);

            rtn.Rows.Add("lblAwards", 0, "Awards", 8, 0, 1, 1, false);
            rtn.Rows.Add("tbAwards", 2, "", 8, 1, 1, 1, false);
            rtn.Rows.Add("spcAwards", 0, "", 8, 2, 1, 1, false);

            //ControlName, Control, ControlText, Row, Col, Span, MultiRow, CenterText
            rtn.Rows.Add("lblRemarks", 0, "Remarks", 9, 0, 2, 1, true);
            rtn.Rows.Add("spcRemarks", 0, "", 9, 2, 1, 1, false);

            //ControlName, Control, ControlText, Row, Col, Span, MultiRow, CenterText
            rtn.Rows.Add("tbRemarks", 2, "", 10, 0, 2, 3, false);
            rtn.Rows.Add("spcRemarks2", 0, "", 10, 2, 1, 1, false);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zBuildQSO()
        {
            Control thisControl = null;
            int thisWidth = -1;
            AnchorStyles thisAnchorStyle;
            string[] thisItems = null;
            int thisRow = -1;
            int thisCol = -1;
            int thisMultiRow = 1;
            string thisControlName = "";
            string thisControlText = "";
            int thisSpan = 0;
            DataTable fields = zGetLogEntryFormat();

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 1;

            int curLayoutRow = -1;
            for (int i = 0; i < fields.Rows.Count; i++)
            {
                //Have we changed tableLayoutPanel rows?
                thisRow = (int)fields.Rows[i]["Row"];
                if (curLayoutRow != thisRow)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    curLayoutRow = (int)fields.Rows[i]["Row"];
                }

                //Build the requested control
                thisControlName = (string)fields.Rows[i]["ControlName"];
                thisControlText = (string)fields.Rows[i]["ControlText"];
                switch ((int)fields.Rows[i]["Control"])
                {
                    case 0:     //Label
                        ContentAlignment thisAlignment = ContentAlignment.MiddleLeft;
                        if ((bool)fields.Rows[i]["CenterText"])
                        {
                            thisAlignment = ContentAlignment.BottomCenter;
                        }
                        thisControl = new Label() { Text = thisControlText,
                                                    Name = thisControlName,
                                                    TextAlign = thisAlignment };
                        break;

                    case 1:     //Button
                        thisWidth = (int)tableLayoutPanel1.ColumnStyles[0].Width;
                        thisAnchorStyle = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right));
                        thisControl = new Button() { Text = thisControlText,
                                                    Name = thisControlName,
                                                    Width = thisWidth, Anchor = thisAnchorStyle };
                        switch (thisControlName)
                        {
                            case "btnDate":
                                thisControl.Click += new System.EventHandler(this.btnDate_Click);
                                break;
                            case "btnLookup":
                                thisControl.Click += new System.EventHandler(this.btnLookup_Click);
                                break;
                            default:
                                break;
                        }
                        break;

                    case 2:     //TextBox
                        thisWidth = (int)tableLayoutPanel1.ColumnStyles[0].Width;
                        thisMultiRow = (int)fields.Rows[i]["MultiRow"];
                        bool multiLine = thisMultiRow > 1;
                        thisMultiRow = thisMultiRow * 20;
                        thisAnchorStyle = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right));
                        thisControl = new TextBox() { Text = thisControlText,
                                                      Name = thisControlName,
                                                      Width = thisWidth, Anchor = thisAnchorStyle,
                                                      Multiline = multiLine, Height = thisMultiRow };
                        break;

                    case 3:     //ComboBox
                        thisWidth = (int)tableLayoutPanel1.ColumnStyles[0].Width;
                        thisItems = thisControlText.Split('|');
                        thisAnchorStyle = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right));
                        thisControl = new ComboBox() { Name = thisControlName,
                                                       Width = thisWidth, Anchor = thisAnchorStyle };
                        for (int j = 0; j < thisItems.Length; j++)
                        {
                            ((ComboBox)thisControl).Items.Add(thisItems[j]);
                        }
                        break;

                    case 4:     //CheckBox
                        thisControl = new CheckBox() { Text = thisControlText,
                                                       Name = thisControlName };
                        break;

                    default:
                        break;
                }

                //And now add the control to the tableLayoutPanel
                thisRow = (int)fields.Rows[i]["Row"];
                thisCol = ((int)fields.Rows[i]["Col"]);
                tableLayoutPanel1.Controls.Add(thisControl, thisCol, thisRow);
                thisSpan = (int)fields.Rows[i]["Span"];
                if (thisSpan != 1)
                {
                    tableLayoutPanel1.SetRowSpan(thisControl, thisSpan);
                }
                tableLayoutPanel1.Show();
                tableLayoutPanel1.Refresh();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testSomethingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zBuildQSO();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDate_Click(object sender, EventArgs e)
        {
            string showDate = DateTime.Now.ToString("yyyy-MM-dd");
            tbDate.Text = showDate;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zStartSession()
        {
            string xmlText = "";
            WebRequest request = WebRequest.Create("http://xmldata.qrz.com/xml/current/?username=n2kua;password=CatDeva2000;agent=q5.0");
            WebResponse response = request.GetResponse();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Read bytes from stream and interpret them as ints
                byte[] buffer = new byte[2048];
                int count;
                // Read from the IO stream fewer times.
                while ((count = dataStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    xmlText = System.Text.Encoding.UTF8.GetString(buffer);
                }
            }
            response.Close();
            session = new XmlDocument();
            xmlText = xmlText.Replace((char)0x00, ' ');
            session.LoadXml(xmlText);
        }

        private XmlDocument zGetWebDate(string url)
        {
            string xmlText = "";
            XmlDocument ret = null;

            WebRequest request1 = WebRequest.Create(url);
            WebResponse response1 = request1.GetResponse();
            
            using (Stream dataStream = response1.GetResponseStream())
            {
                // Read bytes from stream and interpret them as ints
                byte[] buffer = new byte[2048];
                int count;
                // Read from the IO stream fewer times.
                while ((count = dataStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    xmlText += System.Text.Encoding.UTF8.GetString(buffer);
                }
            }
            response1.Close();

            xmlText = xmlText.Replace((char)0x00, ' ');
            ret = new XmlDocument();
            ret.LoadXml(xmlText);

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click(object sender, EventArgs e)
        {
            string sessionKey = "";
            string url = "";
            XmlDocument callSignData = null;

            if (session == null)
            {
                zStartSession();
            }

            XmlNodeList key = session.GetElementsByTagName ("Key");
            if (key != null)
            {
                sessionKey = key[0].InnerText;
            }

            tbMHZ.Text.Trim();
            tbUTC.Text.Trim();
            tbCall.Text.Trim();
            url = "http://xmldata.qrz.com/xml/current/?s=" + sessionKey + ";callsign=" + tbCall.Text.Trim();
            callSignData = zGetWebDate(url);

            string message = "";
            if (callSignData.GetElementsByTagName("Error").Count == 1)
            {
                message = callSignData.GetElementsByTagName("Error")[0].InnerText;
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                message = zBuildCallSignMessage(callSignData);
                message += zBuildNameMessage(callSignData);
                message += zBuildAddressMessage(callSignData);
                message += zBuildCountyMessage(callSignData);
                message += zBuildCountryMessage(callSignData);
                message += zBuildPreviousCallMessage(callSignData);
                message += zBuildLocationMessage(callSignData);
                message += zBuildQRZExpiration(callSignData);

                message += "\nDO YOU WANT TO IMPORT THIS DATA?";

                DialogResult dr = MessageBox.Show(message, "Search Results", MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes) { MessageBox.Show("would import now"); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildCallSignMessage(XmlDocument doc)
        {
            string message = "Call Sign: ";
            string data = "";

            data = zPullDocumentData(doc, "call");
            if (data == null) { message += "null"; }
            else { message += data; }
            message += "\n";
            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildNameMessage(XmlDocument doc)
        {
            string data = "";
            string message = "Name:     ";

            data = zPullDocumentData(doc, "fname");
            if (data == null) { message += "null"; }
            else { message += data; }
            data = zPullDocumentData(doc, "name");
            if (data == null) { message += "null"; }
            else { message += data; }
            message += "\n";
            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildAddressMessage(XmlDocument doc)
        {
            string data = "";
            string message = "Address: ";

            data = zPullDocumentData(doc, "addr1");
            if (data == null) { message += "null"; }
            else
            {
                message += data + "\n                ";
                data = zPullDocumentData(doc, "addr2");
                if (data == null) { message += "null"; }
                else { message += data; }

                data = zPullDocumentData(doc, "state");
                if (data == null) { message += " null"; }
                else { message += ", " + data; };

                data = zPullDocumentData(doc, "zip");
                if (data == null) { message += " null"; }
                else { message += " " + data; }
            }
            message += "\n";
            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildCountyMessage(XmlDocument doc)
        {
            string message = "";
            string data = "";

            data = zPullDocumentData(doc, "county");
            if ((data != null) && (data.Length > 0))
            { message += "County:   " + data + "\n"; }

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildCountryMessage(XmlDocument doc)
        {
            string data = "";
            string message = "Country: ";

            data = zPullDocumentData(doc, "country");
            if (data == null) { message += "null"; }
            else
            {
                message += data + " (";
                data = zPullDocumentData(doc, "ccode");
                if (data == null) { message += "null"; }
                else { message += data; }
                message += ")";
            }
            message += "\n";

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildPreviousCallMessage(XmlDocument doc)
        {
            string message = "";
            string data = "";

            data = zPullDocumentData(doc, "p_call");
            if ((data != null) && (data.Length > 0))
            { message += "Prev Call: " + data + "\n"; }

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildLocationMessage(XmlDocument doc)
        {
            string data = "";
            string message = "";

            data = zPullDocumentData(doc, "lat");
            if ((data != null) && (data.Length > 0))
            {
                message += "Latitude: " + data + "  ";
                data = zPullDocumentData(doc, "lon");
                if (data == null) { message += "null"; }
                else { message += "Longitude: " + data; }
                message += "\n";
            }

            data = zPullDocumentData(doc, "grid");
            if ((data != null) && (data.Length > 0))
            { message += "Grid:        " + data + "\n"; }

            data = zPullDocumentData(doc, "cqzone");
            if ((data != null) && (data.Length > 0))
            { message += "CQ Zone:  " + data + "\n"; }

            data = zPullDocumentData(doc, "ituzone");
            if ((data != null) && (data.Length > 0))
            { message += "ITU Zone: " + data + "\n"; }

            return message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string zBuildQRZExpiration(XmlDocument doc)
        {
            string message = "";
            string data = "";
            
            data = zPullDocumentData(doc, "SubExp");
            if ((data != null) && (data.Length > 0))
            { message += "\nQRZ exp:  " + data + "\n"; }

            return message;
        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="doc"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    private string zPullDocumentData(XmlDocument doc, string key)
        {
            string rtn = "";
            XmlNodeList nodes = doc.GetElementsByTagName(key);
            if (nodes.Count == 0) { rtn = null; }
            else
            {
                rtn = nodes[0].InnerText;
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCall_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if ((c >= 'a') || (c <= 'z'))
            {
                e.KeyChar = Convert.ToChar(Convert.ToString(c).ToUpper().Substring(0, 1));
            }
        }

    }
}
