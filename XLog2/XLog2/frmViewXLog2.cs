using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XLog2
{
    public partial class frmViewXLog2 : Form
    {
        private int mTabIndex = 1;

        internal List<Control> QSOentryControls = new List<Control>();   //List of controls in the QSO for
        private Form1 mFrm1 = null;

        public frmViewXLog2(Form1 frm)
        {
            InitializeComponent();
            mFrm1 = frm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmViewXLog2_Load(object sender, EventArgs e)
        {
            mFrm1.FillLogGrid(dataGridView1, rb100.Checked, rb500.Checked, rbAll.Checked,
                               cbDateRange.Checked, dateTimePicker1.Value, dateTimePicker2.Value, "XLog2");
            tabControl1.SelectedTab.Text = mFrm1.mHamLogLogName;
            BuildQSOEntryForm(mFrm1.mHamLogLogName);
            btnDate.Focus();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                                //
        //                                            QSO FORM                                            //
        //                                                                                                //
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #region QSO

        /// <summary>
        /// 
        /// </summary>
        internal void BuildQSOEntryForm(string logName)
        {
            lblQSO.Text = "New QSO";

            int splitDist = splitContainer1.SplitterDistance;

            tableLayoutPanel1.SuspendLayout();

            while (tableLayoutPanel1.RowCount > 1)
            {
                int row = tableLayoutPanel1.RowCount - 1;
                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                {
                    Control c = tableLayoutPanel1.GetControlFromPosition(i, row);
                    if (c != null)
                    {
                        tableLayoutPanel1.Controls.Remove(c);
                        c.Dispose();
                    }
                }

                try
                {
                    tableLayoutPanel1.RowStyles.RemoveAt(row);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    //TODO: It would be nice to log the error with what is in msg.
                }
                tableLayoutPanel1.RowCount--;
            }

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            mTabIndex = 2;
            btnClear.TabIndex = 100;
            btnSave.TabIndex = 99;
            tableLayoutPanel1.TabIndex = 1;
            QSOentryControls.Clear();
            if (mFrm1.mDac.GetLogFields(logName, "Date") > 0) { zAddDate(); }
            if (mFrm1.mDac.GetLogFields(logName, "UTC") > 0) { zAddStartTime(); }
            if (mFrm1.mDac.GetLogFields(logName, "DateEnd") > 0) { zAddEndTime(); }
            if (mFrm1.mDac.GetLogFields(logName, "Call") > 0) { zAddCall(); }
            if (mFrm1.mDac.GetLogFields(logName, "Frequency") > 0) { zAddFrequency(); }
            if (mFrm1.mDac.GetLogFields(logName, "Mode") > 0) { zAddMode(); }
            if (mFrm1.mDac.GetLogFields(logName, "TX") > 0) { zAddTx(); }
            if (mFrm1.mDac.GetLogFields(logName, "RX") > 0) { zAddRx(); }
            if (mFrm1.mDac.GetLogFields(logName, "Awards") > 0) { zAddAwards(); }
            if (mFrm1.mDac.GetLogFields(logName, "QSLOut") > 0) { zAddQslOut(); }    //QSLIn always displays with QSLOut
            if (mFrm1.mDac.GetLogFields(logName, "Power") > 0) { zAddPower(); }
            if (mFrm1.mDac.GetLogFields(logName, "Name") > 0) { zAddName(); }
            if (mFrm1.mDac.GetLogFields(logName, "QTH") > 0) { zAddQTH(); }
            if (mFrm1.mDac.GetLogFields(logName, "Locator") > 0) { zAddLocator(); }
            if (mFrm1.mDac.GetLogFields(logName, "UNKNOWN1") > 0) { zAddUnknown1(); }
            if (mFrm1.mDac.GetLogFields(logName, "UNKNOWN2") > 0) { zAddUnknown2(); }
            if (mFrm1.mDac.GetLogFields(logName, "Remarks") > 0) { zAddRemarks(); }
            if (mFrm1.mDac.GetLogFields(logName, "Country") > 0) { zAddCountry(); }
            if (mFrm1.mDac.GetLogFields(logName, "State") > 0) { zAddState(); }
            if (mFrm1.mDac.GetLogFields(logName, "County") > 0) { zAddCounty(); }

            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();

            splitContainer1.SplitterDistance = splitContainer1.SplitterDistance + 20;
            splitContainer1.SplitterDistance = splitDist;

            //TODO: get the defaults to work
            zFillDefaults();
        }


        #region Call

        /// <summary>
        /// 
        /// </summary>
        private void zAddCall()
        {
            Label lblCall = new Label();
            TextBox tbCall = new TextBox();
            tbCall.CharacterCasing = CharacterCasing.Upper;
            tbCall.MaxLength = 45;
            tbCall.TextChanged += new System.EventHandler(this.tbCall_TextChanged);
            zAddLabeledControl(lblCall, "lblCall", "Call",
                               tbCall, "tbCall", 2);
            //Add the lookup button
            Button btnLookup = new Button();
            btnLookup.Name = "btnLookup";
            btnLookup.Height = btnDate.Height;
            btnLookup.Width = 20;
            btnLookup.Text = "?";
            QSOentryControls.Add(btnLookup);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnLookup_Click);
            tableLayoutPanel1.Controls.Add(btnLookup, 3, tableLayoutPanel1.RowCount - 1);
            btnLookup.Anchor = (AnchorStyles.Left | AnchorStyles.Top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetCall(string val)
        {
            zSetControlStringValue("tbCall", val);
        }

        /// <summary>
        /// Get the Call value
        /// </summary>
        /// <returns></returns>
        private string zGetCall()
        {
            return zGetControlStringValue("tbCall");
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click(object sender, EventArgs e)
        {
            string call = zGetCall();
            if (call.Trim().Length > 0)
            {
                CallLookup qrz = mFrm1.QRZLookup(zGetCall());
                if (qrz != null)
                {
                    zSetName(qrz.FirstName + " " + qrz.LastName);
                    zSetRemarks(qrz.Addr1 + "\r\n" +
                                qrz.Addr2 + ", " + qrz.State + " " + qrz.Zip + "\r\n" +
                                qrz.Country + "\r\n" +
                                qrz.Latitude + ":" +
                                qrz.Longitude + "\r\n" +
                                "Grid: " + qrz.Grid + "\r\n" +
                                "DXCC: " + qrz.DXCC + "\r\n" +
                                "CQ Zone: " + qrz.CQZone + "\r\n" +
                                "ITU Zone: " + qrz.ITUZone);
                }
            }
            else
            {
                MessageBox.Show("You must have a callsign to lookup", mFrm1.ProgramName + " input error");
            }
        }

        /// <summary>
        /// If we are using the Worked before form, and if we have at 
        /// lease 3 characters, search the log for possible worked before
        /// situations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCall_TextChanged(object sender, EventArgs e)
        {
            string val = "";

            val = ((TextBox)sender).Text.Trim();
            if (val.Length > 2)
            {
                if (mFrm1.WorkedBefore != null)
                {
                    //TODO: If we are NOT using worked before then could this fail?
                    mFrm1.WorkedBefore.CallSignChanged(val);
                }
            }
        }

        #endregion

        #region Frequency
        /// <summary>
        /// 
        /// </summary>
        private void zAddFrequency()
        {
            Label lblFrequency = new Label();
            //TODO: Need to check the database for Frequency being TextBox or ComboBox
            TextBox tbFrequency = new TextBox();
            tbFrequency.MaxLength = 45;
            zAddLabeledControl(lblFrequency, "lblFrequency", "MHz",
                               tbFrequency, "tbFrequency", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetFrequency(string val)
        {
            zSetControlStringValue("tbFrequency", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetFrequency()
        {
            return zGetControlStringValue("tbFrequency");
        }
        #endregion

        #region Mode

        /// <summary>
        /// 
        /// </summary>
        private void zAddMode()
        {
            Label lblMode = new Label();
            //TODO: Need to check database for TextBox or ComboBox
            TextBox tbMode = new TextBox();
            tbMode.MaxLength = 45;
            zAddLabeledControl(lblMode, "lblMode", "Mode",
                               tbMode, "tbMode", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetMode(string val)
        {
            zSetControlStringValue("tbMode", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetMode()
        {
            return zGetControlStringValue("tbMode");
        }

        #endregion

        #region Rx/Tx

        /// <summary>
        /// 
        /// </summary>
        private void zAddTx()
        {
            Label lblTx = new Label();
            TextBox tbTx = new TextBox();
            tbTx.MaxLength = 3;
            zAddLabeledControl(lblTx, "lblTx", "Tx (RST)",
                               tbTx, "tbTx", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetTx(string val)
        {
            zSetControlStringValue("tbTx", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetTx()
        {
            return zGetControlStringValue("tbTx");
        }

        /// <summary>
        /// 
        /// </summary>
        private void zAddRx()
        {
            Label lblRx = new Label();
            TextBox tbRx = new TextBox();
            tbRx.MaxLength = 3;

            zAddLabeledControl(lblRx, "lblRx", "Rx (RST)",
                               tbRx, "tbRx", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetRx(string val)
        {
            zSetControlStringValue("tbRx", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetRx()
        {
            return zGetControlStringValue("tbRx");
        }

        #endregion

        #region Awards

        /// <summary>
        /// 
        /// </summary>
        private void zAddAwards()
        {
            Label lblAwards = new Label();
            TextBox tbAwards = new TextBox();
            tbAwards.MaxLength = 45;
            zAddLabeledControl(lblAwards, "lblAwards", "Awards",
                               tbAwards, "tbAwards", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetAwards(string val)
        {
            zSetControlStringValue("tbAwards", val);
        }

        /// <summary>
        /// Get the Awards value
        /// </summary>
        /// <returns>Awards (or null)</returns>
        private string zGetAwards()
        {
            return zGetControlStringValue("tbAwards");
        }

        #endregion

        #region QSL

        /// <summary>
        /// 
        /// </summary>
        private void zAddQslOut()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            CheckBox cbQslOut = new CheckBox();
            cbQslOut.Name = "cbQslOut";
            cbQslOut.Text = "QSL Out";
            QSOentryControls.Add(cbQslOut);
            tableLayoutPanel1.Controls.Add(cbQslOut, 1, tableLayoutPanel1.RowCount - 1);

            CheckBox cbQslIn = new CheckBox();
            cbQslIn.Name = "cbQslIn";
            cbQslIn.Text = "QSL In";
            QSOentryControls.Add(cbQslIn);
            tableLayoutPanel1.Controls.Add(cbQslIn, 2, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(cbQslIn, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetQslOut(bool val)
        {
            zSetControlBoolValue("cbQslOut", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetQslIn(bool val)
        {
            zSetControlBoolValue("cbQslIn", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool zGetQslOut()
        {
            return zGetControlBoolValue("cbQslOut");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool zGetQslIn()
        {
            return zGetControlBoolValue("cbQslIn");
        }

        #endregion

        #region Power

        /// <summary>
        /// 
        /// </summary>
        private void zAddPower()
        {
            Label lblPower = new Label();
            TextBox tbPower = new TextBox();
            tbPower.MaxLength = 45;
            zAddLabeledControl(lblPower, "lblPower", "Power",
                               tbPower, "tbPower", 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetPower(string val)
        {
            zSetControlStringValue("tbPower", val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string zGetPower()
        {
            return zGetControlStringValue("tbPower");
        }

        #endregion

        #region Country

        /// <summary>
        /// Add the Country ComboBox
        /// </summary>
        private void zAddCountry()
        {
            Label lblCountry = new Label();
            ComboBox cmbCountry = new ComboBox();
            cmbCountry.DataSource = mFrm1.mDac.LoadCountries();

            try
            {
                if (cmbCountry.DataSource != null)
                {
                    cmbCountry.SelectedIndex = 1;
                }
            }
            catch (Exception ex) { }

            cmbCountry.DisplayMember = "Country";
            cmbCountry.ValueMember = "EntityCode";
            zAddLabeledControl(lblCountry, "lblCountry", "Country", cmbCountry, "cmbCountry", 3);
        }

        /// <summary>
        /// Set the CountryCode
        /// </summary>
        /// <param name="val">ContryCode as a string(?)</param>
        private void zSetCountry(string val)
        {
            zSetControlStringValue("cmbCountry", val);
        }

        /// <summary>
        /// Return the Selected CountryCode
        /// </summary>
        /// <returns>CountryCode</returns>
        private int zGetCountry()
        {
            int rtn = -1;

            Control c = zFindControlByName("cmbCountry");
            if (c != null)
            {
                rtn = (int)((ComboBox)c).SelectedValue;
            }

            return rtn;
        }

        /// <summary>
        /// This returns the Country combobox to "Select".
        /// </summary>
        private void zClearCountry()
        {
            Control c = zFindControlByName("cmbCountry");
            if (c != null)
            {
                ((ComboBox)c).SelectedIndex = 0;
            }
        }

        /// <summary>
        /// The user has selected a Country. Get the ID of the
        /// selected country and call the zSetStateDataSource
        /// routine to fill-in the states for the selected 
        /// country.
        /// </summary>
        /// <remarks>
        /// Not much can go wrong, just getting the selected
        /// value from the Country combobox in the current 
        /// view (XLog2 or HamLog).  Just in case, there is a
        /// try/catch with a messagebox to display the system
        /// error.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            int countryid = -1;

            try
            {
                countryid = (int)((ComboBox)sender).SelectedValue;
                mFrm1.FillStateComboBox((ComboBox)zFindControlByName("cmbState"), countryid);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error Getting Selected Country ID\r\n" +
                                "Msg: " + ex.Message, mFrm1.ProgramName + " System Error");
            }
        }

        #endregion

        #region State

        /// <summary>
        /// Add the State ComboBox to the QSO. Set the Country SelectedIndexChanged event.
        /// </summary>
        private void zAddState()
        {
            Label lblState = new Label();
            ComboBox cmbState = new ComboBox();
            zAddLabeledControl(lblState, "lblState", "State", cmbState, "cmbState", 3);

            ComboBox cmbCt = (ComboBox)zFindControlByName("cmbCountry");
            if (cmbCt != null)
            {
                cmbCt.SelectedIndexChanged +=
                        new System.EventHandler(this.Country_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// Set the state, if necessary will load the DataSource
        /// </summary>
        /// <param name="val"></param>
        private void zSetState(string val)
        {
            int nval = -1;
            nval = Convert.ToInt32(val);

            Control c = zFindControlByName("cmbState");
            if (c != null)
            {
                if (((ComboBox)c).DataSource == null)
                {
                    mFrm1.FillStateComboBox(((ComboBox)c), nval);
                }
                else
                {
                    ((ComboBox)c).SelectedValue = nval;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int zGetState()
        {
            int rtn = -1;

            Control c = zFindControlByName("cmbState");
            if (c != null)
            {
                rtn = (int)((ComboBox)c).SelectedValue;
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zClearState()
        {
            Control c = zFindControlByName("cmbState");
            if (c != null)
            {
                ((ComboBox)c).DataSource = null;
                ((ComboBox)c).Enabled = false;
            }
        }

        /// <summary>
        /// The user has made a selection in the state combobox in
        /// the current view (XLog2 or HamLog). Get the id of the
        /// state and then call the zSetCountyDataSource routine
        /// to fill-in the counties.
        /// </summary>
        /// <remarks>
        /// There is not much that can go wrong in THIS routine.
        /// Just getting the ID from the datagrid, converting it
        /// to an int then making the call.  Just in case though
        /// there is a try catch to issue a messagebox error if
        /// anything did go wrong.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void State_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stateid = -1;
            DataTable dt = null;

            try
            {
                dt = (DataTable)((ComboBox)sender).DataSource;
                stateid = Convert.ToInt32(((ComboBox)sender).SelectedValue.ToString());
                mFrm1.FillCountyComboBox((ComboBox)zFindControlByName("cmbCounty"), stateid);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in getting the StateID.\r\n" +
                                "Msg: " + ex.Message, mFrm1.ProgramName + " System Error");
            }
        }

        #endregion

        #region County

        /// <summary>
        /// 
        /// </summary>
        private void zAddCounty()
        {
            Label lblCounty = new Label();
            ComboBox cmbCounty = new ComboBox();
            zAddLabeledControl(lblCounty, "lblCounty", "County", cmbCounty, "cmbCounty", 3);

            //Fill-in the county data.
            ComboBox cmbSt = (ComboBox)zFindControlByName("cmbState");
            cmbSt.SelectedIndexChanged +=
                new System.EventHandler(this.State_SelectedIndexChanged);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void zSetCounty(string val)
        {
            int nval = -1;
            nval = Convert.ToInt32(val);
            Control c = zFindControlByName("cmbCounty");
            if (c != null)
            {
                ((ComboBox)c).SelectedValue = nval;
            }
        }

        /// <summary>
        /// Get the selected County code
        /// </summary>
        /// <returns>CountyCode</returns>
        private int zGetCounty()
        {
            int rtn = -1;

            Control c = zFindControlByName("cmbCounty");
            if (c != null)
            {
                rtn = (int)((ComboBox)c).SelectedValue;
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zClearCounty()
        {
            Control c = zFindControlByName("cmbCounty");
            if (c != null)
            {
                ((ComboBox)c).DataSource = null;
                ((ComboBox)c).Enabled = false;
            }
        }

        #endregion

        #region Name

        /// <summary>
        /// Add the Name control to the QSO
        /// </summary>
        private void zAddName()
        {
            Label lblName = new Label();
            TextBox tbName = new TextBox();
            tbName.MaxLength = 60;
            zAddLabeledControl(lblName, "lblName", "Name",
                               tbName, "tbName", 3);
        }

        /// <summary>
        /// Set the value of the Name field.
        /// </summary>
        /// <param name="val"></param>
        private void zSetName(string val)
        {
            zSetControlStringValue("tbName", val);
        }

        /// <summary>
        /// Get the Name
        /// </summary>
        /// <returns>Name (or null)</returns>
        private string zGetName()
        {
            return zGetControlStringValue("tbName");
        }

        #endregion

        #region QTH

        /// <summary>
        /// Add the QTH control to the QSO
        /// </summary>
        private void zAddQTH()
        {
            Label lblQTH = new Label();
            TextBox tbQTH = new TextBox();
            tbQTH.MaxLength = 128;
            zAddLabeledControl(lblQTH, "lblQTH", "QTH",
                               tbQTH, "tbQTH", 3);
        }

        /// <summary>
        /// Set the value of the QTH Control
        /// </summary>
        /// <param name="dte"></param>
        private void zSetQTH(string val)
        {
            zSetControlStringValue("tbQTH", val);
        }

        /// <summary>
        /// Get the QTH
        /// </summary>
        /// <returns>QTH (or null)</returns>
        private string zGetQTH()
        {
            return zGetControlStringValue("tbQTH");
        }

        #endregion

        #region Locator

        /// <summary>
        /// Add the Locator Control to the QSO
        /// </summary>
        private void zAddLocator()
        {
            Label lblLocator = new Label();
            TextBox tbLocator = new TextBox();
            tbLocator.MaxLength = 45;
            zAddLabeledControl(lblLocator, "lblLocator", "Locator",
                               tbLocator, "tbLocator", 3);
        }

        /// <summary>
        /// Set the Locator
        /// </summary>
        /// <param name="dte"></param>
        private void zSetLocator(string val)
        {
            zSetControlStringValue("tbLocator", val);
        }

        /// <summary>
        /// Get the Locator
        /// </summary>
        /// <returns>Locator (or null)</returns>
        private string zGetLocator()
        {
            return zGetControlStringValue("tbLocator");
        }

        #endregion

        #region Unknowns

        /// <summary>
        /// Add the Unknown1 Control to the QSO
        /// </summary>
        private void zAddUnknown1()
        {
            Label lblUnknown1 = new Label();
            string lblText = mFrm1.mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown1Label");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN1"; }
            TextBox tbUnknown1 = new TextBox();
            tbUnknown1.MaxLength = 128;
            zAddLabeledControl(lblUnknown1, "lblUnknown1", lblText,
                               tbUnknown1, "tbUnknown1", 3);
        }

        /// <summary>
        /// Add the Unknown2 to the QSL
        /// </summary>
        private void zAddUnknown2()
        {
            //TODO: Need to get the Unknown2 field title
            Label lblUnknown2 = new Label();
            TextBox tbUnknown2 = new TextBox();
            string lblText = mFrm1.mDac.GetDefault(tabControl1.SelectedTab.Text, "Unknown2Label");
            if ((lblText == null) || (lblText.Trim().Length == 0)) { lblText = "UNKNOWN2"; }
            tbUnknown2.MaxLength = 128;
            zAddLabeledControl(lblUnknown2, "lblUnknown2", lblText,
                               tbUnknown2, "tbUnknown2", 3);
        }

        /// <summary>
        /// Set the Unknown1 Control value
        /// </summary>
        /// <param name="val">New Value</param>
        private void zSetUnknown1(string val)
        {
            zSetControlStringValue("tbUnknown1", val);
        }

        /// <summary>
        /// Set the Unknown2 value
        /// </summary>
        /// <param name="val">New Value</param>
        private void zSetUnknown2(string val)
        {
            zSetControlStringValue("tbUnknown2", val);
        }

        /// <summary>
        /// Get the Unknown1 value
        /// </summary>
        /// <returns>Unknown1 (or null)</returns>
        private string zGetUnknown1()
        {
            return zGetControlStringValue("tbUnknown1");
        }

        /// <summary>
        /// Get the Unknown1 value
        /// </summary>
        /// <returns>Unknown1 (or null)</returns>
        private string zGetUnknown2()
        {
            return zGetControlStringValue("tbUnknown2");
        }

        #endregion

        #region Remarks

        /// <summary>
        /// Add the Remarks Control to the QSO
        /// </summary>
        private void zAddRemarks()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            Label lblRemark = new Label();
            lblRemark.Name = "lblRemark";
            lblRemark.Text = "Remarks";
            Font font = new Font("Microsoft Sans Serif", 12.0f,
                        FontStyle.Bold);
            lblRemark.Font = font;

            lblRemark.AutoSize = false;
            lblRemark.TextAlign = ContentAlignment.BottomCenter;
            tableLayoutPanel1.Controls.Add(lblRemark, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(lblRemark, 2);

            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            TextBox tbRemarks = new TextBox();
            tbRemarks.MaxLength = 1024;
            tbRemarks.Name = "tbRemarks";
            tbRemarks.Multiline = true;
            tbRemarks.Height = 65;
            tbRemarks.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            QSOentryControls.Add(tbRemarks);
            tableLayoutPanel1.Controls.Add(tbRemarks, 0, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(tbRemarks, 4);
        }

        /// <summary>
        /// Set the Remarks
        /// </summary>
        /// <param name="val">Remarks</param>
        private void zSetRemarks(string val)
        {
            zSetControlStringValue("tbRemarks", val);
        }

        /// <summary>
        /// Get the Remarks
        /// </summary>
        /// <returns>Remarks (or null)</returns>
        private string zGetRemarks()
        {
            return zGetControlStringValue("tbRemarks");
        }

        #endregion

        #region Start/End Date/Time

        /// <summary>
        /// Add Start DateControl to the QSO 
        /// </summary>
        private void zAddDate()
        {
            if (tableLayoutPanel1.RowCount > 1)
            {
                tableLayoutPanel1.RowCount++;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            }

            Button btnDate = new Button();
            btnDate.Name = "btnDate";
            btnDate.Text = "Date";
            QSOentryControls.Add(btnDate);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnDate_Click);
            tableLayoutPanel1.Controls.Add(btnDate, 0, tableLayoutPanel1.RowCount - 1);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).TabIndex = mTabIndex;
            mTabIndex++;

            DateTimePicker dtpQSODate = new DateTimePicker();
            dtpQSODate.Name = "dtpQSODate";
            dtpQSODate.Format = DateTimePickerFormat.Short;
            dtpQSODate.TabIndex = mTabIndex;
            mTabIndex++;
            QSOentryControls.Add(dtpQSODate);
            tableLayoutPanel1.Controls.Add(dtpQSODate, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(dtpQSODate, 3);
            dtpQSODate.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// Add the start time Control to the QSO 
        /// </summary>
        private void zAddStartTime()
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            Button btnStart = new Button();
            btnStart.Name = "btnStart";
            btnStart.Text = "UTC";
            QSOentryControls.Add(btnStart);
            ((Button)QSOentryControls[QSOentryControls.Count - 1]).Click += new EventHandler(btnStart_Click);
            tableLayoutPanel1.Controls.Add(btnStart, 0, tableLayoutPanel1.RowCount - 1);

            DateTimePicker dtpQSOTime = new DateTimePicker();
            dtpQSOTime.Name = "dtpQSOTime";
            dtpQSOTime.Format = DateTimePickerFormat.Time;
            QSOentryControls.Add(dtpQSOTime);
            tableLayoutPanel1.Controls.Add(dtpQSOTime, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(dtpQSOTime, 3);
            dtpQSOTime.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// Add the end time Control to the QSO 
        /// </summary>
        private void zAddEndTime()
        {
            Button btnEnd = new Button();
            btnEnd.Click += new EventHandler(btnEnd_Click);

            DateTimePicker dtpEnd = new DateTimePicker();
            dtpEnd.Format = DateTimePickerFormat.Time;

            zAddLabeledControl(btnEnd, "btnEnd", "End (UTC)",
                               dtpEnd, "dtpEnd", 3);
        }

        /// <summary>
        /// Set the Start Date
        /// </summary>
        /// <param name="dte">Date to set the QSO Start</param>
        private void zSetDate(DateTime dte)
        {
            zSetControlDateTimeValue("dtpQSODate", dte);
        }

        /// <summary>
        /// Set the Start Time
        /// </summary>
        /// <param name="dte"></param>
        private void zSetStartTime(DateTime dte)
        {
            zSetControlDateTimeValue("dtpQSOTime", dte);
        }

        /// <summary>
        /// Set the End Time 
        /// </summary>
        /// <param name="dte"></param>
        private void zSetEndTime(DateTime dte)
        {
            zSetControlDateTimeValue("dtpEnd", dte);
        }

        /// <summary>
        /// Get the QSO Date
        /// </summary>
        /// <returns>Start Date of the QSO</returns>
        private DateTime zGetDate()
        {
            return zGetControlDateTimeValue("dtpQSODate");
        }

        /// <summary>
        /// Get the QSO Start time
        /// </summary>
        /// <returns></returns>
        private DateTime zGetStartTime()
        {
            return zGetControlDateTimeValue("dtpQSOTime");
        }

        private DateTime zGetEndTime()
        {
            return zGetControlDateTimeValue("dtpEnd");
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpQSOTime");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1vv
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDate_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpQSODate");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        /// <summary>
        /// These form level events are here because they are part 
        /// of the QSO "form" in splitter panel 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Control c = zFindControlByName("dtpEnd");
            if (c != null)
            {
                ((DateTimePicker)c).Value = DateTime.Now;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="lblName"></param>
        /// <param name="lblText"></param>
        /// <param name="data"></param>
        /// <param name="dataName"></param>
        /// <param name="dataColSpan"></param>
        private void zAddLabeledControl(Control lbl, String lblName, String lblText,
                                        Control data, String dataName, int dataColSpan)
        {
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            lbl.AutoSize = false;
            lbl.Name = lblName;
            lbl.Height = btnDate.Height;
            lbl.Width = btnDate.Width;
            lbl.Text = lblText;
            if (lbl is Label)
            {
                ((Label)lbl).TextAlign = ContentAlignment.MiddleCenter;
            }
            if (lbl is Button)
            {
                ((Button)lbl).TextAlign = ContentAlignment.MiddleCenter;
                QSOentryControls.Add(lbl);
            }
            tableLayoutPanel1.Controls.Add(lbl, 0, tableLayoutPanel1.RowCount - 1);

            data.Name = dataName;
            data.Height = dtpQSODate.Height;
            data.Width = dtpQSODate.Width;
            QSOentryControls.Add(data);
            tableLayoutPanel1.Controls.Add(data, 1, tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.SetColumnSpan(data, dataColSpan);
            data.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Control zFindControlByName(String name, int quiet = 1)
        {
            Control rtn = null;

            foreach (Control c in QSOentryControls)
            {
                if (c.Name.ToLower() == name.ToLower())
                {
                    rtn = c;
                }
            }

            if (rtn == null)
            {
                if (quiet != 1)
                {
                    MessageBox.Show("Couldn't find " + name, "System Error");
                }
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string zGetControlStringValue(string field)
        {
            string rtn = null;

            Control c = zFindControlByName("field");
            if (c != null)
            {
                rtn = ((TextBox)c).Text;
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private void zSetControlStringValue(string field, string val)
        {
            Control c = zFindControlByName(field);
            if (c != null)
            {
                ((TextBox)c).Text = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private DateTime zGetControlDateTimeValue(string field)
        {
            DateTime rtn = DateTime.MinValue;

            Control c = zFindControlByName("field");
            if (c != null)
            {
                rtn = ((DateTimePicker)c).Value;
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="val"></param>
        private void zSetControlDateTimeValue(string field, DateTime val)
        {
            Control c = zFindControlByName("field");
            if (c != null)
            {
                ((DateTimePicker)c).Value = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private bool zGetControlBoolValue(string field)
        {
            bool rtn = false;

            Control c = zFindControlByName("field");
            if (c != null)
            {
                rtn = ((CheckBox)c).Checked;
            }

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="val"></param>
        private void zSetControlBoolValue(string field, bool val)
        {
            Control c = zFindControlByName("field");
            if (c != null)
            {
                ((CheckBox)c).Checked = val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void zClearQSO()
        {
            foreach (Control c in QSOentryControls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = 0;
                }
                if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).Value = DateTime.Now;
                }
            }

            zFillDefaults();

            lblQSO.Text = "New QSO";
            btnSave.Text = "Save";
            mFrm1.m_QSONumber = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void zFillDefaults()
        {
            string LogName = tabControl1.SelectedTab.Text;

            zSetFrequency(mFrm1.mDac.GetDefault(LogName, "Frequency"));
            zSetMode(mFrm1.mDac.GetDefault(LogName, "Mode"));
            zSetTx(mFrm1.mDac.GetDefault(LogName, "Tx")); 
            zSetRx(mFrm1.mDac.GetDefault(LogName, "Rx"));
            zSetAwards(mFrm1.mDac.GetDefault(LogName, "Awards")); 
            zSetPower(mFrm1.mDac.GetDefault(LogName, "Power")); 
            zSetUnknown1(mFrm1.mDac.GetDefault(LogName, "Unknown1")); 
            zSetUnknown2(mFrm1.mDac.GetDefault(LogName, "Unknown2")); 
            zSetRemarks(mFrm1.mDac.GetDefault(LogName, "Remarks")); 
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion

        /// <summary>
        /// The user has pressed the QSO Clear Button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            zClearQSO();
        }

        /// <summary>
        /// The user has pressed the Save/Update QSO button, Save or
        /// Update the QSO 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            QSO qso = new QSO();

            int ID = 0;
            DateTime start;
            DateTime min;
            DateTime time;
            string call = "";
            string freq = "";
            string mode = "";
            string val = "";
            bool valBool = false;
            DateTime valDate = DateTime.Now;
 
            start = zGetDate();
            min = zGetStartTime();
            time = new DateTime(start.Year, start.Month, start.Day, min.Hour, min.Minute, min.Second);
            qso.StartDate = time;

            qso.Call = zGetCall();
            qso.Frequency = zGetFrequency();
            qso.Mode = zGetMode();
            qso.LogID = (btnSave.Text == "Save") ? 0 : 1;  //TODO: FIX THIS
            qso.LogName = tabControl1.SelectedTab.Text.Trim();

            //Now we save the values the user has entered into the controls
            // of the QSO "form".
            foreach (Control cc in QSOentryControls)
            {
                //Buttons are never saved and labelse are not added to the array
                // of controls on the QSO "form".
                if (!(cc is Button))
                {
                    //Get the control value (what the user typed)
                    if (cc is TextBox) { val = ((TextBox)cc).Text.Trim(); }
                    if (cc is CheckBox) { valBool = ((CheckBox)cc).Checked; }
                    if (cc is ComboBox) { val = ((ComboBox)cc).Text.Trim(); }
                    if (cc is DateTimePicker) { valDate = ((DateTimePicker)cc).Value; }

                    //And now do an SQL UPDATE to wrte the value out.
                    switch (cc.Name)
                    {
                        case "dtpEnd":
                            qso.EndDate = valDate;
                                break;

                        case "tbCall":
                            qso.Call = val;
                            break;

                        case "tbFrequency":
                            qso.Frequency = val;
                            break;

                        case "tbMode":
                            qso.Mode = val;
                            break;

                        case "tbTx":
                            qso.TxRST = val;
                            break;

                        case "tbRx":
                            qso.RxRST = val;
                            break;

                        case "tbAwards":
                            qso.Awards = val;
                            break;

                        case "cbQslOut":
                            qso.QSLout = valBool;
                            break;

                        case "cbQslIn":
                            qso.QSLin = valBool;
                            break;

                        case "tbPower":
                            qso.Power = val;
                            break;

                        case "tbName":
                            qso.Name = val;
                            break;

                        case "tbQTH":
                            qso.QTH = val;
                            break;

                        case "tbLocator":
                            qso.Locator = val;
                            break;

                        case "tbUNKNOWN1":
                            qso.Unknown1 = val;
                            break;

                        case "tbUNKNOWN2":
                            qso.Unknown1 = val;
                            break;

                        case "tbRemarks":
                            qso.Remarks = val;
                            break;

                        case "cmbCountry":
                            qso.CountryCode = Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString());
                            break;

                        case "cmbState":
                            qso.StateCode = Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString());
                            break;

                        case "cmbCounty":
                            qso.CountyCode = Convert.ToInt32(((ComboBox)cc).SelectedValue.ToString());
                            break;

                        default:
                            break;
                    }
                }
            }
            mFrm1.AddUpdate(qso);
            //TODO: mForm1.FillQSOLog();
            zClearQSO();

        }

        /// <summary>
        /// The Radio Button to limite the display to 100 entries
        /// has been pressed, re-fill the Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb100_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The Radio Button to NOT limit the display to 100/500
        /// entries (ALL entries) has been pressed, re-fill the Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAll_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The Radio Button to limit the display to 500 entries 
        /// has been pressed, re-fill the Log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rb500_Click(object sender, EventArgs e)
        {
            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user has changed the checkbox that determines if the user
        /// wants to limit the entries to a specific date range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDateRange_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = cbDateRange.Checked;
            dateTimePicker2.Enabled = cbDateRange.Checked;

            //TODO: Call the Form1 FillLog routine
        }

        /// <summary>
        /// The user doubled clicked in the Log. This indicates that the
        /// user wants to edit the row that was clicked on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = -1;
            DataGridViewRow row = null;

            row = ((DataGridView)tabControl1.SelectedTab.Controls[0]).SelectedRows[0];
            idx = (int)row.Cells["ID"].Value;

            zEditQSO(idx);
        }

        private void zEditQSO(int ID)
        {
            //TODO: Get the ID of the record to edit.
            DataTable qso = mFrm1.mDac.GetQSOs(ID);
            lblQSO.Text = "QSO: " + qso.Rows[0]["Id"];
            zSetDate((DateTime)qso.Rows[0]["StartDate"]);
            zSetStartTime((DateTime)qso.Rows[0]["StartDate"]);
            zSetEndTime((DateTime)qso.Rows[0]["EndDate"]);
            zSetCall((qso.Rows[0]["Call"] == null) ? "" : (string)qso.Rows[0]["Call"]);
            zSetFrequency((qso.Rows[0]["Frequency"] == null) ? "" : (string)qso.Rows[0]["Frequency"]);
            zSetMode((qso.Rows[0]["Mode"] == null) ? "" : qso.Rows[0]["Mode"].ToString());
            zSetTx((qso.Rows[0]["TXrst"] == null) ? "" : qso.Rows[0]["TXrst"].ToString());
            zSetRx((qso.Rows[0]["RXrst"] == null) ? "" : qso.Rows[0]["RXrst"].ToString());
            zSetAwards((qso.Rows[0]["Awards"] == null) ? "" : qso.Rows[0]["Awards"].ToString());
            zSetQslOut((qso.Rows[0]["QSLOut"] == null) ? false : "1" == qso.Rows[0]["QSLOut"].ToString());
            zSetPower((qso.Rows[0]["Power"] == null) ? "" : qso.Rows[0]["Power"].ToString());
            zSetName((qso.Rows[0]["Name"] == null) ? "" : qso.Rows[0]["Name"].ToString());
            zSetQTH((qso.Rows[0]["QTH"] == null) ? "" : qso.Rows[0]["QTH"].ToString());
            zSetLocator((qso.Rows[0]["Locator"] == null) ? "" : qso.Rows[0]["Locator"].ToString());
            zSetUnknown1((qso.Rows[0]["Unknown1"] == null) ? "" : qso.Rows[0]["Unknown1"].ToString());
            zSetUnknown2((qso.Rows[0]["Unknown2"] == null) ? "" : qso.Rows[0]["Unknown2"].ToString());
            zSetRemarks((qso.Rows[0]["Remarks"] == null) ? "" : qso.Rows[0]["Remarks"].ToString());
            zSetCountry((qso.Rows[0]["CountryCode"] == null) ? "" : qso.Rows[0]["CountryCode"].ToString());
            zSetState((qso.Rows[0]["StateCode"] == null) ? "" : qso.Rows[0]["StateCode"].ToString());
            zSetCounty((qso.Rows[0]["CountyCode"] == null) ? "" : qso.Rows[0]["CountyCode"].ToString());
            btnSave.Text = "Update";
        }
   
        /// <summary>
        /// 
        /// </summary>
        internal void RemoveCurrentTab()
        {
            if (tabControl1.TabCount > 1)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }

            else
            {
                MessageBox.Show("You can not close the only tab shown",mFrm1. ProgramName + " input error");
            }
        }

        /// <summary>
        /// The user has pressed the mouse button while over the Log. May
        /// need to bring up the context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = ((DataGridView)tabControl1.SelectedTab.Controls[0]).HitTest(e.X, e.Y);
                ((DataGridView)tabControl1.SelectedTab.Controls[0]).Rows[hti.RowIndex].Selected = true;
            }
        }

        /// <summary>
        /// The user has pressed the Edit Context menu option.  The user
        /// wants to edit the selecte row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idx = -1;
            DataGridViewRow row = null;
            
            row = ((DataGridView)tabControl1.SelectedTab.Controls[0]).SelectedRows[0];
            idx = (int)row.Cells["ID"].Value;

            zEditQSO(idx);
        }

        /// <summary>
        /// The user has pressed the Delete Context menu option.  The user
        /// wants to delete the selected row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            int QSOID = -1;
            string IDstring = "";
            int err = 0;
            DataGridViewRow row = null;
            DataGridView dgv = null;

            dgv = (DataGridView)tabControl1.SelectedTab.Controls[0];
            row = dgv.SelectedRows[0];

            msg = "Are you sure you want to delete:\r" +
                            "  Num:  " + row.Cells["Number"].Value.ToString() + "\r" +
                            "  Call: " + row.Cells["Call"].Value.ToString() + "\r" +
                            "  Date: " + row.Cells["StartDate"].Value.ToString() + "\r" +
                            "  Freq: " + row.Cells["Frequency"].Value.ToString() + "\r" +
                            "  Mode: " + row.Cells["Mode"].Value.ToString() + "\r" +
                            "Deleting can not be undon.";

            if (DialogResult.OK == MessageBox.Show(msg, mFrm1.ProgramName + " - Delete",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                IDstring = row.Cells["ID"].Value.ToString();
                if (int.TryParse(IDstring, out QSOID))
                {
                    if (1 != mFrm1.mDac.DeleteQSO(Convert.ToInt32(row.Cells["ID"].Value.ToString())))
                    {
                        err = 1;
                    }
                }
                else
                {
                    err = 1;
                }
            }

            if (err == 0)
            {
                mFrm1.FillLogGrid(dgv, rb100.Checked, rb500.Checked, rbAll.Checked,
                                cbDateRange.Checked, dateTimePicker1.Value, dateTimePicker2.Value,
                                tabControl1.SelectedTab.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mFrm1.mHamLogLogName = tabControl1.SelectedTab.Text;
            int splitDist = splitContainer1.SplitterDistance;
            DataGridView dgv = (DataGridView)tabControl1.SelectedTab.Controls[0];
            mFrm1.FillLogGrid(dgv, rb100.Checked, rb500.Checked, rbAll.Checked,
                                cbDateRange.Checked, dateTimePicker1.Value, dateTimePicker2.Value,
                                "XLog2", mFrm1.mHamLogLogName);
            BuildQSOEntryForm(mFrm1.mHamLogLogName);
            splitContainer1.SplitterDistance = splitDist;
            this.Text = mFrm1.ProgramName + " - " + mFrm1.mHamLogLogName;
            mFrm1.mDac.SaveProperty("XLOG2.LastLogViewed", mFrm1.mHamLogLogName);
        }
    }
}
