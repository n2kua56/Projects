using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using MySql;

namespace HamLogBook
{
    public class DataAccess
    {
        //Not a critical application, no security concerns...
        //  the database, user and password are hard coded.
        string mConnString = "Server=localhost\\SQLEXPRESS;Database=HamLog;" +
            "User Id=hamlog;Password=password;";

        //The connection that all functions will use.
        SqlConnection mConnection = null;
        Form1 mForm1 = null;

        public DataAccess(Form1 frm)
        {
            mForm1 = frm;
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
                mConnection = new SqlConnection(mConnString);
                mConnection.Open();
                rtn = 1;
                //mConnection.State;
                return rtn;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, "HamLogBook");

                //mForm1.eventLog1.WriteEvent("ConnectToHamLog failed: " + ex.StackTrace,
                //    EventLogEntryType.Information, 101, 1);
                return 0;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: Address - Later
        //
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
            string rtn = "";
            string sql = "SELECT [Value] " +
                            "FROM [AvailableProperties] " +
                            "WHERE [Name]=@key";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@key", key));
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
            string sql = "UPDATE [AvailableProperties] " +
                            "SET [Value] = @val " +
                            "WHERE [Name] = @key";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@key", key));
            parms.Add(new SqlParameter("@val", val));
            return zExecuteNonQuery(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: Bands
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetBands()
        {
            string sql = "SELECT [Id], [Band] " +
                            "FROM [Band] " +
                            "Order By [SortOrder]; ";
            return zGetDataTable(sql, null);
        }
        
        /// <summary>
        /// Get all data for the specified Id
        /// </summary>
        /// <param name="id">Id of the band data to return</param>
        /// <returns>Table containing requested table data</returns>
        public DataTable GetBandData(int id)
        {
            string sql = "SELECT * " +
                            "FROM [Band] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetDataTable(sql, parms);
        }

        /// <summary>
        /// Return the Band Name for the specified band id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Name of the requested band</returns>
        public string GetBandName(int id)
        {
            string sql = "SELECT [Band] " +
                            "FROM [Band] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: BandPlan - Later
        //
        // Table: Countries
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCountries()
        {
            string sql = "SELECT [Id], [CountryName] " +
                            "FROM [Countries] " +
                            "ORDER BY [SortOrder], [CountryName]; ";
            return zGetDataTable(sql, null);
        }

        /// <summary>
        /// Return the data on the specified country.
        /// </summary>
        /// <param name="id">Id of the country to return</param>
        /// <returns></returns>
        public DataTable GetCountryData(int id)
        {
            string sql = "SELECT * " +
                            "FROM [Countries] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetDataTable(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCountryName(int id)
        {
            string sql = "SELECT [CountryName] " +
                            "FROM [Countries] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCountryShortName(int id)
        {
            string sql = "SELECT [CountryShortName] " +
                            "FROM [Countries] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: County
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        public DataTable GetCounties(int stateId)
        {
            string sql = "SELECT [Id], [CountryName] " +
                            "FROM [County] " +
                            "WHERE [StateId] = 1 " +
                         "UNION " +
                         "SELECT [Id], [CountyName] " +
                            "FROM [County] " +
                            "WHERE [StateId] = @stateId " +
                            "ORDER BY [SortOrder], [CountyName]; ";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@stateId", stateId));
            return zGetDataTable(sql, parms);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetCountyName(int id)
        {
            string sql = "SELECT [CountyName] " +
                            "FROM Countries " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: Hamlog - Last
        // Table: Licenses - Later
        //
        // Table: Mode
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetModes()
        {
            string sql = "SElECT [Id], [Mode] " + 
                            "FROM [Mode] " +
                            "Order By [SortOrder], [Mode]; ";
            return zGetDataTable(sql, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetModeData(int id)
        {
            string sql = "SELECT * " +
                            "FROM [Mode] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetDataTable(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetModeName(int id)
        {
            string sql = "SELECT [Mode] " +
                            "FROM [Mode] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: OtherData - Later
        // Table: OtherName - Later
        // Table: Privileges - Later
        //
        // Table: State
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public DataTable GetStates(int countryId)
        {
            string sql = "SELECT [Id], [StateShortName] AS StateName " +
                            "FROM [States] " +
                            "WHERE [CountryId] = @countryId " +
                            "ORDER BY [SortOrder], [StateShortName]; ";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@countryId", countryId));
            return zGetDataTable(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetStateData(int id)
        {
            string sql = "SELECT * " +
                            "FROM [States] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetDataTable(sql, parms);
        }

        public string GetStateName(int id)
        {
            string sql = "SELECT [StateName] " +
                            "FROM [States] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetStateShortName(int id)
        {
            string sql = "SELECT [StateShortName] " +
                            "FROM [States] " +
                            "WHERE [Id] = @id";
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@id", id));
            return zGetScalar(sql, parms);
        }


        /////////////////////////////////////////////////////////////////////////////////
        // Table: HamLog - Make a test entry.
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DteTmeStart"></param>
        /// <param name="DteTmeEnd"></param>
        /// <param name="bandid"></param>
        /// <param name="freq"></param>
        /// <param name="power"></param>
        /// <param name="call"></param>
        /// <param name="countryid"></param>
        /// <param name="stateid"></param>
        /// <param name="name"></param>
        /// <param name="qslsent"></param>
        /// <param name="qslrcvd"></param>
        /// <returns></returns>
        public int AddContact(DateTime DteTmeStart, DateTime DteTmeEnd, int bandid, int freq, 
                            int power, string call, int country, int state, int county, 
                            string name, int mode, string other, bool qslsent, bool qslrcvd, 
                            string comment, int rstSent, int rstRcvd)
        {
            string sql = "";
            int? countryid = null;
            int? stateid = null;
            int? countyid = null;
            int? modeid = null;
            int contactNo = 0;
            int id = 0;
            int rtn = -1;
            string temp = "";
            string MyCall = "";

            List<SqlParameter> parms = new List<SqlParameter>();

            //Get the next contactNo
            sql = "SELECT MAX(ContactNo) FROM HamLog ";
            temp = zGetScalar(sql, null);
            if (temp.Trim().Length > 0)
            {
                contactNo = 1 + Convert.ToInt32(temp);
            }
            else
            {
                contactNo = 1;
            }

            MyCall = GetProperty("CommonOperator");

            //INSERT the contact
            sql = "INSERT INTO HamLog (ContactNo, DatetimeStart, DateTimeEnd, MyCall, CallSign, [Name], " +
                                       "CountryId, StateId, CountyId, RstSent, RstRcvd, BandId, " +
                                       "Frequency, ModeId, [Power], Other, QSLSent, QSLRcvd) " +
                       "VALUES(@contactNo, @DteTmeStart, @DteTmeEnd, @MyCall, @call, @name, " +
                            "@countryid, @stateid, @countyid, @Sent, @Rcvd, @bandid, " +
                            "@freq, @modeid, @power, @other, @qslsent, @qslrcvd) ";

            if (country > -1) { countryid = country; }
            if (state > -1) { stateid = state; }
            if (county > -1) { countyid = county; }
            if (mode > -1) { modeid = mode; }

            parms.Add(new SqlParameter("@contactNo", contactNo));
            parms.Add(new SqlParameter("@DteTmeStart", DteTmeStart));
            parms.Add(new SqlParameter("@DteTmeEnd", DteTmeEnd));
            parms.Add(new SqlParameter("@call", call));
            parms.Add(new SqlParameter("@name", name));
            parms.Add(new SqlParameter("@countryid", countryid));
            parms.Add(new SqlParameter("@stateid", stateid));
            parms.Add(new SqlParameter("@countyid", countyid));
            parms.Add(new SqlParameter("@bandid", bandid));
            parms.Add(new SqlParameter("@freq", freq));
            parms.Add(new SqlParameter("@modeid", modeid));
            parms.Add(new SqlParameter("@power", power));
            parms.Add(new SqlParameter("@other", other));
            parms.Add(new SqlParameter("@qslsent", qslsent));
            parms.Add(new SqlParameter("@qslrcvd", qslrcvd));
            parms.Add(new SqlParameter("@Sent", rstSent));
            parms.Add(new SqlParameter("@Rcvd", rstRcvd));
            parms.Add(new SqlParameter("@MyCall", MyCall));
            rtn = zExecuteNonQuery(sql, parms);

            //Insert the comment if there is one.
            if (comment.Trim().Length > 0)
            {
                //Get the log entry just INSERTed
                sql = "SELECT Id " +
                        "FROM HamLog " +
                        "WHERE ContactNo=@contactNo ";
                parms.Clear();
                parms.Add(new SqlParameter("@contactNo", contactNo));
                id = Convert.ToInt32(zGetScalar(sql, parms));

                //INSERT the comment
                sql = "INSERT INTO Comments(HamLogId, CommentText) " +
                            "VALUES(@HamLogId, @CommentText) ";
                parms.Clear();
                parms.Add(new SqlParameter("@HamLogId", id));
                parms.Add(new SqlParameter("@CommentText", comment));
                zExecuteNonQuery(sql, parms);
            }

            return rtn;
        }

        public int UpdateContact(DateTime DteTmeStart, DateTime DteTmeEnd, int bandid, int freq,
                            int power, string call, int country, int state, int county,
                            string name, int mode, string other, bool qslsent, bool qslrcvd,
                            string comment, int rstSent, int rstRcvd, int RecNo, int CmtId)
        {
            string sql = "";
            int? countryid = null;
            int? stateid = null;
            int? countyid = null;
            int? modeid = null;
            int contactNo = 0;
            int id = 0;
            int rtn = -1;
            string temp = "";
            string MyCall = "";

            List<SqlParameter> parms = new List<SqlParameter>();

            MyCall = GetProperty("CommonOperator");

            //INSERT the contact
            sql = "UPDATE HamLog SET " +
                        "DatetimeStart = @DteTmeStart, " +
                        "DateTimeEnd = @DteTmeEnd, " +
                        "MyCall = @MyCall, " +
                        "CallSign = @call, " +
                        "[Name] = @name, " +
                        "CountryId = @countryid, " +
                        "StateId = @stateid, " +
                        "CountyId = @countyid, " +
                        "RstSent = @Sent, " +
                        "RstRcvd = @Rcvd, " +
                        "BandId = @bandid, " +
                         "Frequency = @freq, " +
                         "ModeId = @modeid, " +
                         "[Power] = @power, " +
                         "Other = @other, " +
                         "QSLSent = @qslsent, " +
                         "QSLRcvd = @qslrcvd " +
                       "WHERE contactNo = @recno";

            if (country > -1) { countryid = country; }
            if (state > -1) { stateid = state; }
            if (county > -1) { countyid = county; }
            if (mode > -1) { modeid = mode; }

            parms.Add(new SqlParameter("@contactNo", RecNo));
            parms.Add(new SqlParameter("@DteTmeStart", DteTmeStart));
            parms.Add(new SqlParameter("@DteTmeEnd", DteTmeEnd));
            parms.Add(new SqlParameter("@call", call));
            parms.Add(new SqlParameter("@name", name));
            parms.Add(new SqlParameter("@countryid", countryid));
            parms.Add(new SqlParameter("@stateid", stateid));
            parms.Add(new SqlParameter("@countyid", countyid));
            parms.Add(new SqlParameter("@bandid", bandid));
            parms.Add(new SqlParameter("@freq", freq));
            parms.Add(new SqlParameter("@modeid", modeid));
            parms.Add(new SqlParameter("@power", power));
            parms.Add(new SqlParameter("@other", other));
            parms.Add(new SqlParameter("@qslsent", qslsent));
            parms.Add(new SqlParameter("@qslrcvd", qslrcvd));
            parms.Add(new SqlParameter("@Sent", rstSent));
            parms.Add(new SqlParameter("@Rcvd", rstRcvd));
            parms.Add(new SqlParameter("@MyCall", MyCall));
            rtn = zExecuteNonQuery(sql, parms);

            //Get the HamLog Id
            sql = "SELECT Id FROM HamLog WHERE contactNo = @contactno ";
            parms.Clear();
            parms.Add(new SqlParameter("@CommentText", comment));
            parms.Add(new SqlParameter("@contactno", RecNo));
            temp = zGetScalar(sql, parms);
            if (int.TryParse(temp, out id))
            {
                //UPDATE the comment
                if (CmtId != -1)
                {
                    sql = "UPDATE Comments SET CommentText = @CommentText " +
                                "WHERE HamLogId = @HamLogId ";
                }
                else
                {
                    sql = "INSERT INTO Comments (CommentText, HamLogId) " +
                            "VALUES(@CommentText, @HamLogId)";
                }
                parms.Clear();
                parms.Add(new SqlParameter("@HamLogId", id));
                parms.Add(new SqlParameter("@CommentText", comment));
                zExecuteNonQuery(sql, parms);
            }

            return rtn;
        }

        public DataTable GetContacts(int numContacts)
        {
            DataTable rtn = null;
            string sql = "";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (numContacts == 0)
            {
                sql = "SELECT ";
            }
            else
            {
                sql = "SELECT TOP " + numContacts.ToString() + " ";
            }
            sql += " H.ContactNo AS RecNo, H.CallSign AS Call, " +
                        "SUBSTRING(CONVERT(VARCHAR, H.DateTimeStart, 120), 1, 16) AS 'Date/Time', " +
                        "B.Band, M.Mode, " +
                        "CASE WHEN H.[Power] < 0 THEN 0 ELSE H.[Power] END AS[Power], " +
                        "CASE WHEN H.RstSent < 1 THEN '' ELSE H.RstSent END AS 'Sent', " +
                        "CASE WHEN H.RstRcvd < 1 THEN '' ELSE H.RstRcvd END AS 'Rcvd', " +
                        "SUBSTRING(CONVERT(VARCHAR, H.DateTimeEnd, 120), 1, 16) AS 'Off', " +
                        "CASE WHEN H.CountryId< 2 THEN '' ELSE C.CountryName END AS Country, " +
                        "CASE WHEN S.StateName = 'Make Selection' THEN '' ELSE S.StateShortName END AS ST, " +
                        "CASE WHEN CT.CountyName = 'Make Selection' THEN '' ELSE CT.CountyName END AS County, " +
                        "H.Name, H.Other, " +
                        "CASE WHEN H.QSLSent = 1 THEN 'Y' ELSE 'N' END AS 'QSL Sent', " +
                        "CASE WHEN H.QSLRcvd = 1 THEN 'Y' ELSE 'N' END AS 'QSL Rcvd', " +
                        "ISNULL(CM.CommentText,'') AS Comment, " +
                        "CASE WHEN H.Frequency< 1 THEN '' ELSE H.Frequency END AS Freq, " +
                        "H.MyCall " +
                    "FROM HamLog AS H " +
                        "LEFT JOIN Band AS B ON (H.BandId = B.Id) " +
                        "LEFT JOIN Mode AS M ON(H.ModeId = M.Id) " +
                        "LEFT JOIN Countries AS C ON(H.CountryId = C.Id) " +
                        "LEFT JOIN States AS S ON(H.StateId = S.Id) " +
                        "LEFT JOIN County AS CT ON(H.CountyId = CT.Id) " +
                        "LEFT JOIN Comments AS CM ON(CM.HamLogId = H.Id) " +
                    "ORDER BY H.ContactNo DESC ";
            rtn = zGetDataTable(sql, null);
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecNo"></param>
        /// <param name="curSetting"></param>
        public void SetQSLSent(int RecNo, string curSetting)
        {
            string sql = "";
            int sent = 0;
            List<SqlParameter> parms = new List<SqlParameter>();

            if (curSetting == "N") { sent = 1; }
            else { sent = 0; }

            sql = "UPDATE HamLog " +
                    "SET QslSent = @sent " +
                    "WHERE ContactNo = @recno ";
            parms.Add(new SqlParameter("@sent", sent));
            parms.Add(new SqlParameter("@recno", RecNo));
            zExecuteNonQuery(sql, parms);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecNo"></param>
        /// <param name="curSetting"></param>
        public void SetQSLRcvd(int RecNo, string curSetting)
        {
            string sql = "";
            int sent = 0;
            List<SqlParameter> parms = new List<SqlParameter>();

            if (curSetting == "N") { sent = 1; }
            else { sent = 0; }

            sql = "UPDATE HamLog " +
                    "SET QslRcvd = @sent " +
                    "WHERE ContactNo = @recno ";
            parms.Add(new SqlParameter("@sent", sent));
            parms.Add(new SqlParameter("@recno", RecNo));
            zExecuteNonQuery(sql, parms);
        }

        public DataTable GetContact(int RecNo)
        {
            DataTable rtn = null;
            string sql = "";
            List<SqlParameter> parms = new List<SqlParameter>();

            sql = "SELECT H.*, ISNULL(CM.CommentText,'') AS Comment, CM.Id AS CommentId " +
                    "FROM HamLog AS H " +
                        "LEFT JOIN Comments AS CM ON(CM.HamLogId = H.Id) " +
                    "WHERE H.ContactNo = @recno ";
            parms.Add(new SqlParameter("@recno", RecNo));

            rtn = zGetDataTable(sql, parms);

            return rtn;
        }

        public int BackupDatabase(string backupFolder)
        {
            int rtn = -1;

            var sqlConStrBuilder = new SqlConnectionStringBuilder(mConnString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                            backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, mConnection))
                {
                    rtn = command.ExecuteNonQuery();
                }
            
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
        private string zGetScalar(string sql, List<SqlParameter> parms)
        {
            SqlCommand cmd = null;
          
            try
            {
                cmd = new SqlCommand(sql, mConnection);
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                return cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {
                ex.Data.Add("SQL", sql);
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        ex.Data.Add(p.ParameterName, p.Value);
                    }
                }

                Logger.LogException(ex, "HamLogBook");
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
        private int zExecuteNonQuery(string sql, List<SqlParameter> parms)
        {
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(sql, mConnection);
                if (parms != null)
                {
                    foreach(SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }
                return cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Logger.LogException(ex, "HamLogBook");
                Logger.LogInformation("SQL: " + sql, "HamLogBook");
                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        string data = p.ParameterName + " : " + p.SqlValue.ToString();
                        Logger.LogInformation(data, "HameLogBook");
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// Return a DataTable for the given SQL SELECT.
        /// </summary>
        /// <param name="sql">The SQL SELECT that will return a Table.</param>
        /// <returns>The Table or null (no data or error)</returns>
        private DataTable zGetDataTable(string sql, List<SqlParameter> parms)
        {
            SqlDataAdapter adapter = null;
            DataSet rtnDS = null;
            SqlCommand cmd = null;

            try
            {
                cmd = new SqlCommand(sql, mConnection);

                if (parms != null)
                {
                    foreach (SqlParameter p in parms)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                adapter = new SqlDataAdapter(cmd);
                
                rtnDS = new DataSet();
                adapter.Fill(rtnDS); 
                return rtnDS.Tables[0];
            }

            catch (Exception ex)
            {
                Logger.LogException(ex, "HamLogBook");
                //mForm1.eventLog1.WriteEntry("zGetDataTable failed: " + ex.StackTrace,
                //        EventLogEntryType.Information, 101, 1);

                return null;
            }
        }

    }
}
