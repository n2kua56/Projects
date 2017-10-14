using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HamLogBook
{
    public partial class Form1 : Form
    {
		//DONE:  1) Edit an Item
        //DONE:     1a) mDac update
        //TODO:     1b) Edit not getting QSL Sent/Rcvd
        //DONE:     1c) Make cell double click edit as well
        //TODO:  2) Delete an Item
        //TODO:  3) Resequence the RecNo field
        //TODO:  4) Work on the settings tabs
        //TODO:  5) Find button
        //TODO:  6) Call "Find" when exit call sign
        //TODO:  7) Add "Find" to stip menu
        //TODO:  8) Add HamLog field for just listening
        //TODO:  9) County not working
        //TODO: 10) Make the data entry a form so we can substitute other forms (i.e. 10-10, etc.)		
        //TODO:  Build Table and fill-in Band
        //TODO:  Build Table and fill-in Mode
        //TODO:  Build Table and fill-in Country
        //TODO:  Build Table and fill-in State
        //TODO:  Build Table and fill-in County
		
        private int mStarted = 0;
        DataAccess mDac = null;
        private int mRecNo = 0;
        private int mCommentId = -1;

        public Form1()
        {
            InitializeComponent();
            mDac = new DataAccess(this);
        }

        // /////////////////////////////////////////////////////////////////
        // Form events.
        // /////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.LogInformation("Starting", "HamLogBook");

            timer1.Start();
            if (1 != mDac.ConnectToHamLog())
            {
                MessageBox.Show("Could not connect to the HamLog database.");
                Application.Exit();
            }

            hlbInputBox1.label1.Text = "Call";

            //zzTest();

            zFillBand();
            zFillMode();
            zFillCountries();
            zSetContactInitialValues(0);

            zFillContacts();
            zFormatGrid();

            hlbInputBox1.textBox1.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (mStarted == 0)
            {
                CommonFields frm = new HamLogBook.CommonFields(mDac, 1);
                frm.ShowDialog();
                mStarted = 1;
                frm.Dispose();
                hlbInputBox1.textBox1.Focus();
                cbBand.SelectedIndex = 0;
                cbMode.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            //TODO:
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string ampm = "";
            string h = "";
            DateTime utc = DateTime.MinValue;
            string utctime = "";

            //Format the localtime using the Property TimeDisplay (12|24)
            string localtime = now.Minute.ToString("00") + ":" + now.Second.ToString("00");
            //IF we are using 12 hour clock then change the 24 hour clock to 12 with the AM|PM appended
            if (Properties.Settings.Default.TimeDisplay == 12)
            {
                ampm = " AM";
                if (now.Hour >= 12) { ampm = " PM"; }
                h = now.Hour.ToString("00");
                if (now.Hour == 0) { h = "12"; }
                if (now.Hour > 12) { h = (now.Hour - 12).ToString("00"); }
            }
            else
            {
                h = now.Hour.ToString("00");
            }
            localtime = h + ":" + localtime + ampm;
            TSStLLocalTime.Text = localtime;

            utc = now.ToUniversalTime();
            utctime = utc.Hour.ToString("00") + ":" + utc.Minute.ToString("00") + ":" + utc.Second.ToString("00") + " UTC";
            TSStLLocalTime.Text += "  " + utctime;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            //TODO: Finish this
            MessageBox.Show("Find not implemented yet");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogContact_Click(object sender, EventArgs e)
        {
            string sql = "";
            string call = "";
            int bandid = 0;
            int modeid = 0;
            DateTime DteTmeStart = DateTime.MinValue;
            DateTime DteTmeEnd = DateTime.MinValue;
            int power = -1;
            int freq = -1;
            int sent = -1;
            int rcvd = -1;
            int countryid = 0;
            int stateid = 0;
            int countyid = 0;
            string name = "";
            string other = "";
            string temp = "";
            bool qslsent = false;
            bool qslrcvd = false;
            int idx = -1;
            string comment = "";

            //Check for fields that are required for Listening or not.
            call = hlbInputBox1.textBox1.Text.Trim();

            try
            {
                bandid = (int)cbBand.SelectedValue;
            }
            catch
            {
                bandid = 1;
            }

            if (-1 == zCheckCommonRequiredFields(call, bandid))
            {
                return;
            }

            //Get the other values from the screen.
            try
            {
                modeid = (int)cbMode.SelectedValue;
            }
            catch
            {
                modeid = 1;
            }
            DteTmeStart = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day,
                                        dtpTimeOn.Value.Hour, dtpTimeOn.Value.Minute, 0);
            DteTmeEnd = dtpTimeOff.Value;

            temp = tbPower.Text.Trim();
            if (!int.TryParse(temp, out power))
            {
                power = -1;
            }

            temp = tbFrequency.Text.Trim();
            while ((idx = temp.IndexOf(',')) >= 0)
            {
                temp = temp.Remove(idx, 1);
            }
            if (!int.TryParse(temp, out freq))
            {
                freq = -1;
            }

            temp = tbSent.Text.Trim();
            if (!int.TryParse(temp, out sent))
            {
                sent = -1;
            }

            temp = tbRec.Text.Trim();
            if (!int.TryParse(temp, out rcvd))
            {
                rcvd = -1;
            }

            countryid = (int)cbCountry.SelectedValue;
            name = tbName.Text.Trim();
            try
            {
                stateid = (int)cbState.SelectedValue;
            }
            catch
            {
                stateid = 1;
            }
            try
            {
                countyid = (int)cbCounty.SelectedValue;
            }
            catch
            {
                countyid = 1;
            }
            other = tbOther.Text.Trim();
            qslsent = cbQSLSent.Checked;
            qslrcvd = cbQSLRcvd.Checked;

            //Now check for required fields when not listening only.
            if (!cbListen.Checked)
            {
                if (-1 == zCheckContactRequiredFields(modeid))
                {
                    return;
                }
            }

            comment = tbComments.Text.Trim();
            temp = tbSent.Text.Trim();
            if (!int.TryParse(temp, out sent))
            {
                sent = -1;
            }
            temp = tbSent.Text.Trim();
            if (!int.TryParse(temp, out rcvd))
            {
                rcvd = -1;
            }

            if (mRecNo == 0)
            {
                mDac.AddContact(DteTmeStart, DteTmeEnd, bandid, freq, power, call, countryid,
                                stateid, countyid, name, modeid, other, qslsent, qslrcvd, comment,
                                sent, rcvd);
            }
            else
            {
                mDac.UpdateContact(DteTmeStart, DteTmeEnd, bandid, freq, power, call, countryid,
                                 stateid, countyid, name, modeid, other, qslsent, qslrcvd, comment,
                                 sent, rcvd, mRecNo, 0);
            }

            lblEntryMode.Text = "Entry Mode";
            mRecNo = 0;

            zFillContacts();
            zSetContactInitialValues(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            hlbInputBox1.textBox1.Text = "";
            dtpDate.Value = DateTime.Now;
            tbPower.Text = "";
            dtpTimeOn.Value = DateTime.Now;
            tbSent.Text = "";
            tbRec.Text = "";
            tbName.Text = "";
            tbFrequency.Text = "";
            tbOther.Text = "";
            dtpTimeOff.Value = DateTime.Now;
            cbQSLSent.Checked = false;
            cbQSLRcvd.Checked = false;

            //cbBand.SelectedIndex = 0;
            //cbMode.SelectedIndex = 0;
            //cbCountry.SelectedIndex = 0;
            //cbState.SelectedIndex = 0;
            //cbCounty.SelectedIndex = 0;

            lblEntryMode.Text = "Entry Mode";
            mRecNo = 0;

            hlbInputBox1.textBox1.Focus();
            this.ActiveControl = hlbInputBox1.textBox1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            int id;

            cbState.Enabled = false;

            object r = cbCountry.SelectedValue;
            if (r != null)
            {
                string rs = r.ToString();
                if (int.TryParse(rs, out id))
                {
                    DataTable states = mDac.GetStates(id);
                    if ((states != null) && (states.Rows.Count > 0))
                    {
                        ////DataRow newRow = states.NewRow();
                        ////newRow["Id"] = -1;
                        ////newRow["StateName"] = "Select";
                        ////states.Rows.InsertAt(newRow, 0);

                        cbState.DataSource = states;
                        cbState.DisplayMember = "StateName";
                        cbState.ValueMember = "Id";
                        cbState.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id;

            cbCounty.Enabled = false;

            object r = cbState.SelectedValue;
            if (r != null)
            {
                string rs = r.ToString();
                if (int.TryParse(rs, out id))
                {
                    DataTable counties = mDac.GetCounties(id);
                    if ((counties != null) && (counties.Rows.Count > 0))
                    {
                        ////DataRow newRow = counties.NewRow();
                        ////newRow["Id"] = -1;
                        ////newRow["CountyName"] = "Select";
                        ////counties.Rows.InsertAt(newRow, 0);

                        cbCounty.DataSource = counties;
                        cbCounty.DisplayMember = "CountyName";
                        cbCounty.ValueMember = "Id";
                        cbCounty.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlbInputBox1_Leave(object sender, EventArgs e)
        {
            hlbInputBox1.textBox1.Text = hlbInputBox1.textBox1.Text.Trim().ToUpper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpDate_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //eventLog1.Close();
        }

        // /////////////////////////////////////////////////////////////////
        // TEST routines 
        // /////////////////////////////////////////////////////////////////

        /// <summary>
        /// Test Routine for mDac routines
        /// </summary>
        private void zzTest()
        {
            string testValue = "";
            DataTable testTable = null;
            int idx = -1;
            DateTime dteStart;
            DateTime dteEnd;
            TimeSpan span;

            //AvailableProperties
            testValue = mDac.GetProperty("CommonCall");
            if (testValue.Trim().Length == 0) { MessageBox.Show("CommonCall AvailableProperty not set"); }
            testValue = mDac.GetProperty("CommonInitials");
            if (testValue.Trim().Length == 0) { MessageBox.Show("CommonInititials AvailableProperty not set"); }

            //Band
            testTable = mDac.GetBands();    // SELECT* FROM Band; --30 Rows
            if (testTable.Rows.Count != 30) { MessageBox.Show("Bands is missing records"); }
            testTable = mDac.GetBandData(2);
            if (testTable.Rows.Count != 1) { MessageBox.Show("Bands didn't return a single row"); }
            if (testTable.Rows[0]["Band"].ToString() != "2190m") { MessageBox.Show("Wrong Bands datga returned"); }
            testValue = mDac.GetBandName(5);
            if (testValue != "80m") { MessageBox.Show("Band Name didn't return 80m"); }

            //Countries
            testTable = mDac.GetCountries();
            if (testTable.Rows.Count != 206) { MessageBox.Show("Countries has wrong number of rows"); }
            testTable = mDac.GetCountryData(2);
            if (testTable.Rows.Count != 1) { MessageBox.Show("Contries id=2 did not return a single row"); }
            if (testTable.Rows[0]["CountryName"].ToString() != "United States of America")
            {
                MessageBox.Show("Countries returned the wrong country");
            }
            if (testTable.Rows[0]["CountryShortName"].ToString() != "USA")
            {
                MessageBox.Show("Countries returned wrong short name");
            }
            testValue = mDac.GetCountryName(3);
            if (testValue != "Canada") { MessageBox.Show("GetCountryName returned wrong country"); }
            testValue = mDac.GetCountryShortName(2);
            if (testValue != "USA") { MessageBox.Show("GetCountryShortName failed"); }

            //Mode
            testTable = mDac.GetModes();
            if (testTable.Rows.Count != 52) { MessageBox.Show("GetModes got wrong number of rows"); }
            testTable = mDac.GetModeData(2);
            if (testTable.Rows.Count != 1) { MessageBox.Show("GetModeData didn't return 1 row"); }
            if (testTable.Rows[0]["Mode"].ToString() != "AM") { MessageBox.Show("GetModeData returned the wrong row"); }
            testValue = mDac.GetModeName(2);
            if (testValue != "AM") { MessageBox.Show("GetModeName returned the wrong value"); }

            //State
            testTable = mDac.GetStates(2);
            if (testTable.Rows.Count != 50) { MessageBox.Show("GetStates returned the wrong number of rows"); }
            testTable = mDac.GetStateData(15);
            if (testTable.Rows.Count != 1) { MessageBox.Show("GetStateData failed to return 1 row"); }
            if (testTable.Rows[0]["StateName"].ToString() != "New York") { MessageBox.Show("GetStateData failed to return correct state (1)"); }
            if (testTable.Rows[0]["StateShortName"].ToString() != "NY") { MessageBox.Show("GetStateData failed to return correct state (2)"); }
            testValue = mDac.GetStateName(15);
            if (testValue != "New York") { MessageBox.Show("GetStateName failed to return correct state"); }
            testValue = mDac.GetStateShortName(15);
            if (testValue != "NY") { MessageBox.Show("GetStateShortName failed to return correct state"); }

            //Test Log file entry
            span = new TimeSpan(0, 0, 30, 0, 0);
            dteStart = DateTime.Now.Subtract(span);
            dteEnd = DateTime.Now;
            idx = mDac.AddContact(dteStart, dteEnd, 15, 144500, 5, "TEST", 2, 15, 14, "Test", 14,
                        "", false, false, "Test Comment 1", 59, 59);
            if (idx != 1) { MessageBox.Show("AddContact failed"); }

            idx = mDac.AddContact(dteStart, dteEnd, 15, 0, 0, "TEST", 2, 1, 1, "TEST", 14,
                        "", false, false, "Test Comment 2", 59, 59);
            if (idx != 1) { MessageBox.Show("AddContact(2) failed"); }
        }

        // /////////////////////////////////////////////////////////////////
        // Menu routines
        // /////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintLog frm = null;
            frm = new PrintLog();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintAddressLabels frm = null;
            frm = new PrintAddressLabels();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open Log Database File";
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            //TODO: Add the extention
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //TODO: Open the database
            }
            else
            {
                //TODO: Do nothing
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save Log Database File As...";
            saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckFileExists = true;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                //TODO: Save the database
            }
            else
            {
                //TODO: Nothing
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: do this
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
			//TODO: Do this
            MessageBox.Show("Reset counters not ready at this time.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings frm = null;
            frm = new Settings();
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// read connectionstring from config file
            //var connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            // read backup folder from config file ("C:/temp/")
            var backupFolder = Settings1.Default.BackupFolder;
            int rtn = mDac.BackupDatabase(backupFolder);
            //var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            //// set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            //var backupFileName = String.Format("{0}{1}-{2}.bak",
            //    backupFolder, sqlConStrBuilder.InitialCatalog,
            //    DateTime.Now.ToString("yyyy-MM-dd"));

            //using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            //{
            //    var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
            //        sqlConStrBuilder.InitialCatalog, backupFileName);

            //    using (var command = new SqlCommand(query, connection))
            //    {
            //        connection.Open();
            //        command.ExecuteNonQuery();
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backupOptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Backup Options Not Ready at This time.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eQSLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eQSL Not Ready at This Time.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string temp = "";
            int i = (int)dataGridView1.SelectedRows[0].Cells["RecNo"].Value;
            DataTable dt = mDac.GetContact(i);

            if ((dt != null) && (dt.Rows.Count == 1))
            {
                hlbInputBox1.textBox1.Text = dt.Rows[0]["CallSign"].ToString();
                tbName.Text = dt.Rows[0]["Name"].ToString();
                tbOther.Text = dt.Rows[0]["Other"].ToString();
                tbComments.Text = dt.Rows[0]["Comment"].ToString();

                temp = dt.Rows[0]["CommentId"].ToString();
                if (!int.TryParse(temp, out mCommentId))
                { mCommentId = -1; }

                dtpTimeOn.Value = zGetDateTime(dt, "DatetimeStart");
                dtpTimeOff.Value = zGetDateTime(dt, "DatetimeEnd");

                cbBand.SelectedValue = zGetcbValueIndex(dt, "BandId");
                cbMode.SelectedValue = zGetcbValueIndex(dt, "ModeId");
                cbCountry.SelectedValue = zGetcbValueIndex(dt, "CountryId");
                cbState.SelectedValue = zGetcbValueIndex(dt, "StateId");

                temp = dt.Rows[0]["QSLSent"].ToString();
                cbQSLSent.Checked = (bool)dt.Rows[0]["QSLSen"];
                cbQSLRcvd.Checked = dt.Rows[0]["QSLRcvd"].ToString() == "1";

                tbFrequency.Text = zGetPossibleNegOneValue(dt, "Frequency");
                tbPower.Text = zGetPossibleNegOneValue(dt, "Power");
                tbSent.Text = zGetPossibleNegOneValue(dt, "RSTSent");
                tbRec.Text = zGetPossibleNegOneValue(dt, "RSTRcvd");

                lblEntryMode.Text = "Edit Mode";
                mRecNo = i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete Contact Not Ready at This Time.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print Label Not Ready at This Time.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qSLSentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = (int)dataGridView1.SelectedRows[0].Cells["RecNo"].Value;
            string j = (string)dataGridView1.SelectedRows[0].Cells["QSL Sent"].Value;
            mDac.SetQSLSent(i, j);
            zFillContacts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void receivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = (int)dataGridView1.SelectedRows[0].Cells["RecNo"].Value;
            string j = (string)dataGridView1.SelectedRows[0].Cells["QSL Rcvd"].Value;
            mDac.SetQSLRcvd(i, j);
            zFillContacts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Add this
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.Focus();
            }
        }

        // /////////////////////////////////////////////////////////////////
        // Internal routines
        // /////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        /// <param name="bandid"></param>
        /// <returns></returns>
        private int zCheckCommonRequiredFields(string call, int bandid)
        {
            if (call.Length == 0)
            {
                MessageBox.Show("You MUST enter a Call Sign");
                hlbInputBox1.textBox1.Focus();
                return -1;
            }

            if (bandid == 1)
            {
                MessageBox.Show("You MUST select a Band");
                cbBand.Focus();
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modeid"></param>
        /// <returns></returns>
        private int zCheckContactRequiredFields(int modeid)
        {
            if (modeid == 1)
            {
                MessageBox.Show("You MUST select a Mode");
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillBand()
        {
            DataTable band = mDac.GetBands();

            ////DataRow newRow = band.NewRow();
            ////newRow["Id"] = -1;
            ////newRow["Band"] = "Select";
            ////band.Rows.InsertAt(newRow, 0);

            if (band.Rows.Count > 0)
            {
                cbBand.DataSource = band;
                cbBand.DisplayMember = "band";
                cbBand.ValueMember = "Id";
                cbBand.SelectedIndex = 0;
                cbBand.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillMode()
        {
            DataTable modes = mDac.GetModes();

            ////DataRow newRow = modes.NewRow();
            ////newRow["Id"] = -1;
            ////newRow["Mode"] = "Select";
            ////modes.Rows.InsertAt(newRow, 0);

            cbMode.DataSource = modes;
            cbMode.DisplayMember = "mode";
            cbMode.ValueMember = "Id";
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillCountries()
        {
            DataTable countries = mDac.GetCountries();

            ////DataRow newRow = countries.NewRow();
            ////newRow["id"] = -1;
            ////newRow["CountryName"] = "Select";
            ////countries.Rows.InsertAt(newRow, 0);

            cbCountry.DataSource = countries;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "Id";
        }

        /// <summary>
        /// 
        /// </summary>
        private void zSetContactInitialValues(int typ)
        {
            if (typ == 0)
            {
                cbCountry.SelectedValue = 1;
                cbBand.SelectedValue = 1;
                cbMode.SelectedValue = 1;
                if ((cbMode.ValueMember != null) && (cbMode.ValueMember != ""))
                {
                    cbMode.SelectedIndex = 0;
                }

                if ((cbCountry.DataSource == null) || ((int)cbCountry.SelectedValue == 0))
                {
                    cbState.DataSource = null;
                    cbState.Enabled = false;
                    cbCounty.DataSource = null;
                    cbCounty.Enabled = false;
                }
            }

            hlbInputBox1.textBox1.Text = "";
            tbSent.Text = "";
            tbRec.Text = "";
            tbOther.Text = "";
            tbName.Text = "";
            tbFrequency.Text = "";
            tbComments.Text = "";

            hlbInputBox1.Focus();
            hlbInputBox1.textBox1.Focus();
            this.ActiveControl = hlbInputBox1.textBox1;
            cbQSLRcvd.Checked = false;
            cbQSLSent.Checked = false;
            dtpTimeOn.Value = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillContacts()
        {
            int numContacts = 0;
            DataTable contacts = null;

            if (rbLast50.Checked)
            {
                numContacts = 50;
            }
            contacts = mDac.GetContacts(numContacts);
            dataGridView1.DataSource = contacts;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFormatGrid()
        {
            dataGridView1.Columns["RecNo"].Width = 60;
            dataGridView1.Columns["Call"].Width = 90;
            dataGridView1.Columns["Band"].Width = 50;
            dataGridView1.Columns["Mode"].Width = 50;
            dataGridView1.Columns["Power"].Width = 50;
            dataGridView1.Columns["Sent"].Width = 45;
            dataGridView1.Columns["Rcvd"].Width = 45;
            dataGridView1.Columns["Country"].Width = 150;
            dataGridView1.Columns["ST"].Width = 30;
            dataGridView1.Columns["Sent"].Width = 45;
            dataGridView1.Columns["Rcvd"].Width = 45;
            dataGridView1.Columns["Comment"].Width = 150;
            dataGridView1.Columns["Freq"].Width = 60;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fld"></param>
        /// <returns></returns>
        private DateTime zGetDateTime(DataTable dt, string fld)
        {
            DateTime val;
            string temp = dt.Rows[0][fld].ToString();
            if (!DateTime.TryParse(temp, out val))
            {
                val = DateTime.MinValue;
                MessageBox.Show("Invalid Date/Time for " + fld);
            }
            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fld"></param>
        /// <returns></returns>
        private int zGetcbValueIndex(DataTable dt, string fld)
        {
            int idx = -1;
            string temp = dt.Rows[0][fld].ToString();
            if (!int.TryParse(temp, out idx))
            {
                idx = 1;
                MessageBox.Show("Invalid " + fld + ".");
            }
            return idx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fld"></param>
        /// <returns></returns>
        private string zGetPossibleNegOneValue(DataTable dt, string fld)
        {
            string temp = dt.Rows[0][fld].ToString();
            if (temp == "-1") { temp = ""; }
            return temp;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Selected = true;
            editToolStripMenuItem_Click(sender, e);
        }
    }
}
