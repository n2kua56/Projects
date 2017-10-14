using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace XLog2
{
    public class DataAccess
    {
        //Not a critical application, no security concerns...
        //  the database, user and password are hard coded.
        string mConnString = "Server={myServerAddress};Database={myDataBase};Uid=xlog2;Pwd=password;";


        //The connection that all functions will use.
        MySqlConnection mConnection = null;
        Form1 mForm1 = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frm"></param>
        public DataAccess(Form1 frm)
        {
            mForm1 = frm;
            mConnString = mConnString.Replace("{myServerAddress}", Properties.Settings.Default.myServerAddress);
            mConnString = mConnString.Replace("{myDataBase}", Properties.Settings.Default.myDataBase);
        }

        /// <summary>
        /// Make the connection to the HamLog table.
        /// </summary>
        /// <returns></returns>
        public int ConnectToHamLog()
        {
            int rtn = 0;

            try
            {
                mConnection = new MySqlConnection(mConnString);
                mConnection.Open();
                rtn = 1;
                //mConnection.State;
                return rtn;
            }
            catch (Exception ex)
            {
                mForm1.ShowMessage(ex.StackTrace, "Could not connect to the HamLog database.");
                //mForm1.eventLog1.WriteEvent("ConnectToHamLog failed: " + ex.StackTrace,
                //    EventLogEntryType.Information, 101, 1);
                return 0;
            }
        }

        public int IsConnectedToHamLog()
        {
            return (mConnection.State == ConnectionState.Open) ? 1 : 0;

        }

        public int DisConnectFromHamLog()
        {
            int rtn = 1;
            try
            {
                mConnection.Close();
            }
            catch(Exception ex)
            {
                rtn = 0;
            }
            return rtn;
        }

        /////////////////////////////////////////////////////////////////////////////////
        // Table: Address - Later
        /////////////////////////////////////////////////////////////////////////////////

        #region Properties
        /////////////////////////////////////////////////////////////////////////////////
        // Table: AvailableProperties
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get the specified AvailableProperty specified
        /// by "key"
        /// </summary>
        /// <param name="key">The name of the available property to return the value of.</param>
        /// <returns></returns>
        public string GetProperty(string key)
        {
            string sql = "SELECT `PropertyValue` " +
                            "FROM `hamradio`.`availableproperties` " +
                            "WHERE `PropertyName`=@key";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@key", key));
            return zGetScalar(sql, parms);
        }

        /// <summary>
        /// Save the value back into the Available Property
        /// specified by key.
        /// </summary>
        /// <param name="key">The name of the available property</param>
        /// <param name="val">The value to be saved</param>
        public int SaveProperty(string key, string val)
        {
            string sql = "UPDATE `hamradio`.`availableproperties` " +
                            "SET `PropertyValue` = @val " +
                            "WHERE `PropertyName` = @key";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            parms.Add(new MySqlParameter("@key", key));
            parms.Add(new MySqlParameter("@val", val));
            return zExecuteNonQuery(sql, parms);
        }
        #endregion

        #region Logs
        /////////////////////////////////////////////////////////////////////////////////
        // Logs
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetLogNames()
        {
            string sql = "SELECT `ID`, `LogName`, `Description` " +
                            "FROM `logs` " +
                            "Order By `LogName`; ";
            return zGetDataTable(sql, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SaveLogName(string name)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int count = 0;
            int rtn = 0;

            sql = "SELECT COUNT(*) FROM `logs` WHERE `LogName` = @LogName";
            parms.Clear();
            parms.Add(new MySqlParameter("@LogName", name));
            if (int.TryParse(zGetScalar(sql, parms), out count))
            {
                sql = "INSERT INTO `logs` (`LogName`, `Description`) " +
                            "VALUES (@LogName, ''); ";
                parms.Clear();
                parms.Add(new MySqlParameter("@LogName", name));
                zExecuteNonQuery(sql, parms);
                rtn = 1;
            }
            
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int zGetLogFileID(string name)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;

            sql = "SELECT `ID` FROM `logs` WHERE `LogName`= @LogName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@LogName", name));
            if (!(int.TryParse(zGetScalar(sql, parms), out rtn)))
            {
                rtn = 0;
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        public int RemoveLog(string logName, bool force = false)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int count = 0;

            sql = "SELECT COUNT(*) " +
                    "FROM `hamradio`.`qsos` " +
                    "WHERE `LogName` = @logName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logName", logName));

            count = Convert.ToInt32(zGetScalar(sql, parms));
            if ((count == 0) || (force))
            {
                sql = "DELETE FROM `hamradio`.`logs` " +
                        "WHERE `LogName` = @logName; ";
                zExecuteNonQuery(sql, parms);
            }
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        public int GetLogId(string logName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = -1;

            sql = "SELECT `ID` " +
                    "FROM `hamradio`.`logs` " +
                    "WHERE `LogName`= @logName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logName", logName));
            
            if (!int.TryParse(zGetScalar(sql, parms), out rtn))
            {
                rtn = -1;
            }

            return rtn;
        }

        #endregion

        #region logFields
        /////////////////////////////////////////////////////////////////////////////////
        // LogsFields
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileID"></param>
        public int ClearLogFields(string LogName)
        {
            string sql = "";
            string logid = "";
            int logFileID = 0;
            int rtn = 0;
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "SELECT `ID` " +
                        "FROM `logs` " +
                        "WHERE `LogName` = @name; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@name", LogName));
            logid = zGetScalar(sql, parms);

            if ((logid != null) && (logid.Length > 0))
            {
                logFileID = Convert.ToInt32(logid);

                sql = "DELETE FROM `logfields` " +
                            "WHERE `LogID`=@id; ";

                parms.Clear();
                parms.Add(new MySqlParameter("@id", logFileID));
                rtn = zExecuteNonQuery(sql, parms);
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileID"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int SaveLogFields(int logFileID, string fieldName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;
            rtn = GetHamLogField(fieldName);
            if (rtn > 0)
            {
                sql = "INSERT INTO `logfields`(`LogId`, `FieldId`, `DefaultValue`, `Title`) " +
                        "VALUES(@logid, @fieldid, '', ''); ";
                parms.Clear();
                parms.Add(new MySqlParameter("@logid", logFileID));
                parms.Add(new MySqlParameter("@fieldid", rtn));
                rtn = zExecuteNonQuery(sql, parms);
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileID"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int GetLogFields(int logFileID, string fieldName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;

            rtn = GetHamLogField(fieldName);
            if (rtn > 0)
            {
                sql = "SELECT `ID` FROM `logfields` WHERE `LogID` = @logid AND `FieldID` = @fieldid; ";
                parms.Clear();
                parms.Add(new MySqlParameter("@logid", logFileID));
                parms.Add(new MySqlParameter("@fieldid", rtn));
                if (!(int.TryParse(zGetScalar(sql, parms), out rtn)))
                {
                    rtn = 0;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileID"></param>
        /// <param name="fieldID"></param>
        /// <returns></returns>
        public int GetLogFields(int logFileID, int fieldID)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;

            sql = "SELECT `ID` FROM `logfields` WHERE `LogID` = @logid AND `FieldID` = @fieldid; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logid", logFileID));
            parms.Add(new MySqlParameter("@fieldid", fieldID));
            if (!(int.TryParse(zGetScalar(sql, parms), out rtn)))
            {
                rtn = 0;
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logFileName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int GetLogFields(string logFileName, string fieldName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int logFileID = 0;
            int rtn = 0;

            logFileID = zGetLogFileID(logFileName);
            if (logFileID > 0)
            {
                rtn = GetHamLogField(fieldName);
                if (rtn > 0)
                {
                    sql = "SELECT `ID` FROM `logfields` WHERE `LogID` = @logid AND `FieldID` = @fieldid; ";
                    parms.Clear();
                    parms.Add(new MySqlParameter("@logid", logFileID));
                    parms.Add(new MySqlParameter("@fieldid", rtn));
                    if (!(int.TryParse(zGetScalar(sql, parms), out rtn)))
                    {
                        rtn = 0;
                    }
                }
            }
            return rtn;
        }
        #endregion

        #region hamLogFields
        /////////////////////////////////////////////////////////////////////////////////
        // LogsFields
        /////////////////////////////////////////////////////////////////////////////////
        public int GetHamLogField(string fieldName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;

            sql = "SELECT `ID` FROM `hamlogfields` WHERE `FieldName` = @fieldName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@fieldName", fieldName));
            if (!(int.TryParse(zGetScalar(sql, parms), out rtn)))
            {
                rtn = 0;
            }

            return rtn;
        }

        #endregion

        #region QSO

        /////////////////////////////////////////////////////////////////////////////////
        // Table: QSO                                                                  //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="call"></param>
        /// <returns></returns>
        public DataTable GetWorkedBefore(string call)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            DataTable tbl = null;

            sql = "SELECT q.`ID`, q.`Number` AS `QSO Number`, q.`StartDate` AS `Start Date`, q.`EndDate` AS `End Date`, " +
                        "q.`Call`, q.`Frequency`, b.`Band`, " +
                        "CASE WHEN q.`Mode` IS NULL OR q.`Mode` = '' THEN m.`Mode` ELSE q.`Mode` END AS `Mode`, " +
                        "q.`TXrst` AS `RST Sent`, q.`RXrst` AS `RST Rcvd`, " +
                        "q.`Awards`, q.`QSLout` AS `QSL sent`, q.`QSLin` AS `QSL Rcvd`, q.`Power`, q.`Name`, " +
                        "q.`QTH`, q.`Locator`, q.`UNKNOWN1`, q.`UNKNOWN2`, q.`Remarks`, q.`LogName`, " +
                        "q.`UNKNOWN1Label`, q.`UNKNOWN2Label`, d.`EntityName` AS `Country`, s.`State`, c.`CountyName` AS `County Name` " +
                "FROM `hamradio`.`qsos` AS q " +
		            "LEFT JOIN `hamradio`.`adif_dxcc_entity_code` AS d ON(d.`EntityCode` = q.`CountryCode`) " +
                    "LEFT JOIN `hamradio`.`states` AS s ON(s.`ID` = q.`StateCode`) " +
                    "LEFT JOIN `hamradio`.`counties` AS c ON(c.`ID` = q.`CountyCode`) " +
                    "LEFT JOIN `hamradio`.`adif_band` AS b ON(b.`ID` = q.`BandID`) " +
                    "LEFT JOIN `hamradio`.`adif_mode` AS m ON(m.`ID` = q.`ModeID`) " +
                "WHERE `Call` LIKE @filter; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@filter", call+"%"));

            tbl = zGetDataTable(sql, parms);
            return tbl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataTable GetQSOs(int Id)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "SELECT q.*, b.`Band` AS BandName, m.`Mode` AS ModeName, " +
                        "d.`EntityName` AS CountryName, s.`State` AS StateName, " +
                        "c.`CountyName` " +
	                "FROM `hamradio`.`qsos` AS q " +
                        "LEFT JOIN `hamradio`.`adif_band` AS b ON (q.`BandID` = b.`ID`) " +
                        "LEFT JOIN `hamradio`.`adif_mode` AS m ON (q.`ModeID` = m.`ID`) " +
                        "LEFT JOIN `hamradio`.`adif_dxcc_entity_code` AS d ON (q.`CountryCode` = d.`EntityCode`) " +
                        "LEFT JOIN `hamradio`.`states` AS s ON (q.`StateCode` = s.`ID`) " +
                        "LEFT JOIN `hamradio`.`counties` AS c ON (q.`CountyCode` = c.`ID`) " +
                    "WHERE q.`ID` = @id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@id", Id));

            return zGetDataTable(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        public DataTable GetQSOs(string logName, int count)
        {
            string log = logName;
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            DataTable rtn = null; 

            if (log.ToUpper() == "ALL") { log = ""; }

            sql = "SELECT q.*, d.`EntityName` AS CountryName, s.`State` AS StateName, c.`CountyName`, " +
                          "b.`Band` AS BandName, m.`Mode` AS ModeName " +
                    "FROM `hamradio`.`qsos` AS q " +
                        "LEFT JOIN `hamradio`.`adif_dxcc_entity_code` AS d ON(q.`CountryCode` = d.`EntityCode`) " +
                        "LEFT JOIN `hamradio`.`states` AS s ON(q.`StateCode` = s.`ID`) " +
                        "LEFT JOIN `hamradio`.`counties` AS c oN(q.`CountyCode` = c.`ID`) " +
                        "LEFT JOIN `hamradio`.`adif_band` AS b ON (q.`BandID` = b.`ID`) " +
                        "LEFT JOIN `hamradio`.`adif_mode` AS m ON (q.`ModeID` = m.`ID`) ";
            parms.Clear();
            if (log.Length > 0)
            {
                sql += "WHERE `LogName`=@logname ";
                parms.Add(new MySqlParameter("@logname", log));
            }
            sql += "ORDER BY `StartDate` DESC ";
            if (count > 0)
            {
                sql += "LIMIT @limt ";
                parms.Add(new MySqlParameter("@limt", count));
            }
            rtn = zGetDataTable(sql, parms);
            return rtn;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="call"></param>
        /// <param name="freq"></param>
        /// <param name="mode"></param>
        /// <param name="logName"></param>
        /// <returns></returns>
        public int AddQSO(DateTime start, string call, string freq, string mode, string logName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 0;
            int number = 0;

            sql = "SELECT MAX(`Number`) " +
                    "FROM `hamradio`.`qsos` " +
                    "WHERE `LogName` = @logName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logName", logName));
            if (!(int.TryParse(zGetScalar(sql, parms), out number)))
            {
                number = 0;
            }
            number++;

            sql = "INSERT INTO `qsos` (`Number`, `StartDate`, `Call`, `Frequency`, `Mode`, `LogName`) " +
                        "VALUES(@number, @startdate, @call, @freq, @mode, @logname); ";
            parms.Clear();
            parms.Add(new MySqlParameter("@number", number));
            parms.Add(new MySqlParameter("@startdate", start));
            parms.Add(new MySqlParameter("@call", call));
            parms.Add(new MySqlParameter("@freq", freq));
            parms.Add(new MySqlParameter("@mode", mode));
            parms.Add(new MySqlParameter("@logname", logName));
            rtn = zExecuteNonQuery(sql, parms);

            if (rtn == 1)
            {
                if (!(int.TryParse(zGetScalar("SELECT LAST_INSERT_ID(); ", null), out rtn)))
                {
                    rtn = -1;
                }
            }

            return rtn;
        }

        /// <summary>
        /// Update one of the QSO Date fields (StartDate, EndDate)
        /// </summary>
        /// <param name="ID">ID of the record to be updated</param>
        /// <param name="fieldName">Name of the field to be updated</param>
        /// <param name="time">DateTime value to be updated to</param>
        /// <returns></returns>
        public int UpdateQSODateField(int ID, string fieldName, DateTime time)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            sql = "UPDATE `qsos` SET `" + fieldName + "` = @val WHERE `ID` = @id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@id", ID));
            parms.Add(new MySqlParameter("@val", time));

            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }

        /// <summary>
        /// Update one of the QSO fields
        /// </summary>
        /// <param name="ID">ID of the record to be updated</param>
        /// <param name="fieldName">Name of the field to be updated</param>
        /// <param name="time">DateTime value to be updated to</param>
        /// <returns></returns>
        public int UpdateQSOTextField(int ID, string fieldName, string val)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            sql = "UPDATE `qsos` SET `" + fieldName + "` = @val WHERE `ID` = @id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@id", ID));
            parms.Add(new MySqlParameter("@val", val));

            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="fieldName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int UpdateQSOIntField(int ID, string fieldName, int val)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            sql = "UPDATE `qsos` SET `" + fieldName + "` = @val WHERE `ID` = @id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@id", ID));
            parms.Add(new MySqlParameter("@val", val));

            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="fieldName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int UpdateQSOBoolField(int ID, string fieldName, bool val)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            sql = "UPDATE `qsos` SET `" + fieldName + "` = @val WHERE `ID` = @id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@ID", ID));
            parms.Add(new MySqlParameter("@val", val));

            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logName"></param>
        /// <param name="fileName"></param>
        /// <param name="ExportLogname"></param>
        /// <param name="ExportQSONum"></param>
        /// <param name="ExportDate"></param>
        /// <param name="ExportUTC"></param>
        /// <param name="ExportUTCend"></param>
        /// <param name="ExportCall"></param>
        /// <param name="ExportFrequency"></param>
        /// <param name="ExportMode"></param>
        /// <param name="ExportTx"></param>
        /// <param name="ExportRx"></param>
        /// <param name="ExportAwards"></param>
        /// <param name="QslOut"></param>
        /// <param name="QslIn"></param>
        /// <param name="Power"></param>
        /// <param name="Name"></param>
        /// <param name="QTH"></param>
        /// <param name="Locator"></param>
        /// <param name="Unknown1"></param>
        /// <param name="Unknown2"></param>
        /// <param name="Remarks"></param>
        /// <param name="CalcBearing"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataTable GetExportData(string logName, string fileName, bool ExportLogname, bool ExportQSONum, 
                            bool ExportDate, bool ExportUTC, bool ExportUTCend, bool ExportCall, 
                            bool ExportFrequency, bool ExportMode, bool ExportTx, bool ExportRx, 
                            bool ExportAwards, bool QslOut, bool QslIn, bool Power, bool Name, bool QTH, 
                            bool Locator, bool Unknown1, bool Unknown2, bool Remarks, bool Country, 
                            bool State, bool County, bool CalcBearing, DateTime startDate, DateTime endDate)
        {
            DataTable tbl = null;
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            parms.Clear();
            sql = "SELECT ";
            if (ExportLogname) { sql += "q.`LogName`, "; }
            if (ExportQSONum) { sql += "q.`Number`, "; }
            if (ExportDate) { sql += "q.`StartDate`, "; }
            if (ExportUTC) { sql += "q.`StartDate`, "; }
            if (ExportUTCend) { sql += "q.`EndDate`, "; }
            if (ExportCall) { sql += "q.`Call`, "; }
            if (ExportFrequency) { sql += "q.`Frequency`, "; }
            if (ExportMode) { sql += "q.`Mode`, "; }
            if (ExportTx) { sql += "q.`TXrst`, "; }
            if (ExportRx) { sql += "q.`RXrst`, "; }
            if (ExportAwards) { sql += "q.`Awards`, "; }
            if (QslOut) { sql += "q.`QSLout`, "; }
            if (QslIn) { sql += "q.`QSLin`, "; }
            if (Power) { sql += "q.`Power`, "; }
            if (Name) { sql += "q.`Name`, "; }
            if (QTH) { sql += "q.`QTH`, "; }
            if (Locator) { sql += "q.`Locator`, "; }
            if (Unknown1) { sql += "q.`UNKNOWN1`, "; }
            if (Unknown2) { sql += "q.`UNKNOWN2`, "; }
            if (Remarks) { sql += "q.`Remarks`, "; }
            if (Country) { sql += "e.`EntityName` AS 'Country', "; }
            if (State) { sql += "s.`State`, "; }
            if (County) { sql += "c.`CountyName` AS 'County', "; }
            //if (CalcBearing) { sql += "`
            sql = sql.Remove(sql.Length - 2, 2);

            sql += " FROM `qsos` AS q " +
                        "LEFT JOIN adif_dxcc_entity_code AS e ON (e.`EntityCode` = q.`CountryCode`) " +
                        "LEFT JOIN states AS s ON (s.ID = q.`StateCode`) " +
                        "LEFT JOIN counties AS c ON (c.ID = q. `CountyCode`) ";


            sql += "WHERE q.`StartDate` >= @startDate AND q.`StartDate` <= @endDate ";
            parms.Add(new MySqlParameter("@startDate", startDate));
            parms.Add(new MySqlParameter("@endDate", endDate));
            if (logName != "ALL")
            {
                sql += "AND q.`LogName` = @logname ";
                parms.Add(new MySqlParameter("@logname", logName));
            }

            sql += "ORDER BY q.`StartDate`; ";

            tbl = zGetDataTable(sql, parms);
            return tbl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int DeleteQSO(int ID)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            sql = "DELETE FROM `qsos` WHERE ID=@id; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@id", ID));

            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcLogName"></param>
        /// <param name="destLogName"></param>
        public void MergeLogs(string srcLogName, string destLogName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "UPDATE `hamradio`.`qsos` " +
                    "SET `LogName` = @dest " +
                    "WHERE `LogName` = @src; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@src", srcLogName));
            parms.Add(new MySqlParameter("@dest", destLogName));

            zExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public void RenameLog(string oldName, string newName)
        {
            MergeLogs(oldName, newName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public void RenameLogEntry(string oldName, string newName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "UPDATE `hamradio`.`qsos` " +
                    "SET `LogName` = @newName " +
                    "WHERE `LogName` = @oldName; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@newName", newName));
            parms.Add(new MySqlParameter("@oldName", oldName));

            zExecuteNonQuery(sql, parms);
        }

        #endregion

        #region Defaults

        /////////////////////////////////////////////////////////////////////////////////
        // Table: Defaults                                                             //
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogName"></param>
        /// <param name="DefaultName"></param>
        /// <returns></returns>
        public string GetDefault(string LogName, string DefaultName)
        {
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            string rtn = "";

            sql = "SELECT `DefaultValue` " +
                        "FROM `defaults` " +
                        "WHERE `LogName`=@logname AND " +
                            "`DefaultName`=@defaultname; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logname", LogName));
            parms.Add(new MySqlParameter("@defaultname", DefaultName));

            rtn = zGetScalar(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LogName"></param>
        /// <param name="DefaultName"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int AddUpdateDefault(string LogName, string DefaultName, string val)
        {
            int ID = 0;
            string IDval = "";
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();
            int rtn = 1;

            //Try to find the record to update.
            sql = "SELECT ID " +
                        "FROM `defaults` " +
                        "WHERE `LogName`=@logname AND " +
                            "`DefaultName`=@defaultname; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@logname", LogName));
            parms.Add(new MySqlParameter("@defaultname", DefaultName));

            IDval = zGetScalar(sql, parms);
            if ((IDval != null) && (IDval.Length > 0))
            {
                ID = Convert.ToInt32(IDval);
            }

            if (ID == 0)
            {
                sql = "INSERT INTO `defaults` (`LogName`, `DefaultName`, `DefaultValue`) " +
                            "VALUES(@logname, @defaultname, @defaultvalue); ";
                parms.Clear();
                parms.Add(new MySqlParameter("@logname", LogName));
                parms.Add(new MySqlParameter("@defaultname", DefaultName));
                parms.Add(new MySqlParameter("@defaultvalue", val));
            }

            else
            {
                sql = "UPDATE `defaults` " +
                            "SET `DefaultValue`=@defaultvalue " +
                            "WHERE `ID`=@id; ";
                parms.Clear();
                parms.Add(new MySqlParameter("@defaultvalue", val));
                parms.Add(new MySqlParameter("@id", ID));
            }

            rtn = zExecuteNonQuery(sql, parms);
            return rtn;
        }

        #endregion

        #region adif

        public DataTable LoadModes()
        {
            DataTable rtn = null;
            string sql = "";

            sql = "SELECT `Mode`, `ID` " +
                    "FROM `hamradio`.`adif_mode` " +
                    "ORDER BY `SortSeq`, `Mode`; ";

            rtn = zGetDataTable(sql, null);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable LoadBands()
        {
            DataTable rtn = null;
            string sql = "";

            sql = "SELECT `Band`, `ID` " +
                    "FROM `hamradio`.`adif_band` " +
                    "ORDER BY `SortSeq`, `LowerFreqMHz`; ";

            rtn = zGetDataTable(sql, null);

            return rtn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string ConvertFreqToBand(double val)
        {
            string sql = "";
            string rtn = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "SELECT `Band` FROM `hamradio`.`adif_band` " +
                        "WHERE `LowerFreqMHz` <= @freq AND `UpperFrequencyMHz` >= @freq; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@freq", (float)val));

            rtn = zGetScalar(sql, parms);

            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable LoadCountries()
        {
            string sql = "";
            DataTable rtn = null;

            sql = "SELECT `EntityName` AS 'Country', `EntityCode` FROM `hamradio`.`adif_dxcc_entity_code` " +
                    "WHERE `IsDeleted` = 0 " +
                    "ORDER BY `SortSeq`, `EntityName`; ";
            rtn = zGetDataTable(sql, null);
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateid"></param>
        /// <returns></returns>
        public DataTable LoadStates(int countryid)
        {
            string sql = "";
            DataTable rtn = null;
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "SELECT `State`, `ID` " +
                    "FROM `hamradio`.`states` " +
                    "WHERE `CountryiD`= @countryid OR " +
                            "`SortSeq` = 0 " +
                    "ORDER BY `SortSeq`, `State`; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@countryid", countryid));
            rtn = zGetDataTable(sql, parms);

            return rtn;
        }

        public DataTable LoadCounties(int stateid)
        {
            string sql = "";
            DataTable rtn = null;
            List<MySqlParameter> parms = new List<MySqlParameter>();

            sql = "SELECT `CountyName`, `ID` " +
                    "FROM `hamradio`.`counties` " +
                    "WHERE `StateID` = @stateid OR " +
                            "`SortSeq` = 0 " +
                    "ORDER BY `SortSeq`, `CountyName`; ";
            parms.Clear();
            parms.Add(new MySqlParameter("@stateid", stateid));
            rtn = zGetDataTable(sql, parms);

            return rtn;
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        //
        // Above this line is used by XLOG2
        //
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public int WriteStack(Exception ex)
        {
            int rtn = 0;
            string sql = "";
            List<MySqlParameter> parms = new List<MySqlParameter>();

            var properties = ex.GetType()
                        .GetProperties();
            var fields = properties
                             .Select(property => new {
                                 Name = property.Name,
                                 Value = property.GetValue(ex, null)
                             })
                             .Select(x => String.Format(
                                 "{0} = {1}",
                                 x.Name,
                                 x.Value != null ? x.Value.ToString() : String.Empty
                             ));
            string StackTrace = String.Join("\n", fields);

            sql = "INSERT INTO `hamradio`.`stacktraces` (`StackTrace`) " +
                        "VALUES(@stacktrace); ";
            parms.Clear();
            parms.Add(new MySqlParameter("@stacktrace", StackTrace));
            rtn = zExecuteNonQuery(sql, parms);

            return rtn;
        }
        /////////////////////////////////////////////////////////////////////////////////
        // U T I L I T Y   F U N C T I O N S
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Get the scalar value requested by the SQL with the optional
        /// parameters specified.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms">List of SqlParameter (List<SqlParameter>)</param>
        /// <returns></returns>
        private string zGetScalar(string sql, List<MySqlParameter> parms)
        {
           MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(sql, mConnection);
                if (parms != null)
                {
                    foreach (MySqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                ex.Data.Add("SQL", sql);
                if (parms != null)
                {
                    foreach (MySqlParameter p in parms)
                    {
                        ex.Data.Add(p.ParameterName, p.Value);
                    }
                }
                
                //mForm1.eventLog1.WriteEntry("zGetScalar failed: " + ex.StackTrace,
                //        EventLogEntryType.Information, 101, 1);

                return null;
            }
        }

        /// <summary>
        /// Execute SQL that is not a query. Updates, Inserts, Deletes.
        /// </summary>
        /// <param name="sql">The SQL for the Insert, Update or Delete</param>
        /// <returns>Number of rows affected or -1 for error</returns>
        private int zExecuteNonQuery(string sql, List<MySqlParameter> parms)
        {
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(sql, mConnection);
                if (parms != null)
                {
                    foreach (MySqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                return cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
                if (parms != null)
                {
                    foreach (MySqlParameter p in parms)
                    {
                        string data = p.ParameterName + " : " + p.MySqlDbType.ToString();
                        msg += "\n" + data;
                    }
                    //TODO: It would be nice to log this error by writting out the contents of msg.
                }

                return -1;
            }
        }

        /// <summary>
        /// Return a DataTable for the given SQL SELECT.
        /// </summary>
        /// <param name="sql">The SQL SELECT that will return a Table.</param>
        /// <returns>The Table or null (no data or error)</returns>
        private DataTable zGetDataTable(string sql, List<MySqlParameter> parms)
        {
            MySqlDataAdapter adapter = null;
            DataSet rtnDS = null;
            MySqlCommand cmd = null;

            try
            {
                cmd = new MySqlCommand(sql, mConnection);

                if (parms != null)
                {
                    foreach (MySqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                adapter = new MySqlDataAdapter(cmd);

                rtnDS = new DataSet();
                adapter.Fill(rtnDS);
                DataTable tbl = rtnDS.Tables[0];
                return tbl;
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
                //mForm1.eventLog1.WriteEntry("zGetDataTable failed: " + ex.StackTrace,
                //        EventLogEntryType.Information, 101, 1);
                //TODO: It would be nice to log the error with what is in msg.
                return null;
            }
        }
    }
}
