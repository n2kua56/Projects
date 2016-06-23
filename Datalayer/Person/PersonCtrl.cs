﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.Person.Models;
using EZDeskDataLayer.User.Models;
using EZDeskDataLayer.Communications;
using EZDeskDataLayer.Address;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;
using pModels = EZDeskDataLayer.Person.Models;
using EZUtils;

namespace EZDeskDataLayer.Person
{
    public class PersonCtrl : Controller
    {
        private string mModName = "PersonCtrl";
        public MySqlCommand cmd { get; set; }
        private EZDeskCommon mCommon = null;

        public PersonCtrl(EZDeskCommon common)
        {
            Trace.Enter(Trace.RtnName(mModName, "PersonCtrl-Constructor"));

            mCommon = common;

            Trace.Exit(Trace.RtnName(mModName, "PersonCtrl-Constructor"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataTable GetMatchingByName(string key)
        {
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetMatchingByName"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `PersonID`, CONCAT(`LastName`, ', ', `FirstName`, ' ', `MiddleName`) AS PName " +
	                    "FROM `per_person` " + 
	                    "WHERE `FirstName` LIKE '" + key + "%' " + 
		                    "OR `LastName` LIKE '" + key + "%' " +
	                    "ORDER BY `LastName`, `FirstName`, `MiddleName` ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);

                step = "Get data";
                tbl = GetDataTable(cmd);

                return tbl;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetMatchingByName failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetMatchingByName"));
            }
        }

        /// <summary>
        /// Get the PersonTypeID for the key
        /// </summary>
        /// <param name="key">name of the PersonTypeID that is needed</param>
        /// <returns>the id of the requested PersonType</returns>
        public int GetPersonTypeByName(string key)
        {
            string sql = "";
            DataTable tbl = null;
            int PersonTypeID = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetPersonTypeByName"));

            try
            {
                step = "Build Querry";
                sql = "SELECT `PersonTypeID` " +
                        "FROM `per_PersonType` " +
                        "WHERE `Description`=@Description ";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@Description", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull result";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    PersonTypeID = GetInt(tbl.Rows[0], "PersonTypeID");
                }

                return PersonTypeID;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetPersonTypeByName failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("key", key);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetPersonTypeByName"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personID"></param>
        /// <returns></returns>
        public PersonFormGetDemographics.PersonTypeEnum GetPersonTypeByID(int personID)
        {
            Trace.Enter(Trace.RtnName(mModName, "GetPersonTypeByID"));
            try
            {
                PersonFormGetDemographics p = GetPersonByID(personID);
                return p.PersonType;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetPersonTypeByID failed", ex);
                eze.Add("Routine", mModName + ".GetPersonTypeByID");
                eze.Add("personID", personID);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetPersonTypeByID"));
            }
        }

        /// <summary>
        ///  Get all of the person data for the specified personid
        /// </summary>
        /// <param name="personid">personid that is to be retrieved.</param>
        /// <returns></returns>
        public PersonFormGetDemographics GetPersonByID(int personid)
        {
            string sql = "";
            DataTable tbl = null;
            PersonFormGetDemographics rtn = null;
            string msg = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetPersonByID"));

            try
            {
                step = "Build querry";
                sql = "SELECT `SharedID`, `SSNO`, `Prefix`, `FirstName`, `MiddleName`, " +
                            "`LastName`, `Suffix`, `BirthDate`, `PersonTypeID`, `Sex`, `RaceTypeID`, " +
                            "`LanguageTypeID`, `PicturePath`, `EthnicityTypeID`, `Note`, `UDF1`, `UDF2`, " +
                            "`UDF3`, `UDF4`, `UDF5`, `UDF6`, `UDF7`, `UDF8`, `UDF9`,`UDF10` " +
                        "FROM `per_Person` " +
                        "WHERE `PersonID`=@personid";
                MySqlCommand cmd = new MySqlCommand(sql, mCommon.Connection);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                // ----- Audit SQL call -----
                EZDeskDataLayer.ehr.Models.AuditItem aItem =
                        new EZDeskDataLayer.ehr.Models.AuditItem(mCommon.User.UserSecurityID, null,
                        EZDeskDataLayer.ehr.Models.AuditAreas.System,
                        EZDeskDataLayer.ehr.Models.AuditActivities.View,
                        "SQL: " + sql + "| personid=" + personid.ToString());
                mCommon.eCtrl.WriteAuditRecord(aItem);

                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    step = "Pull Data";
                    DataRow dr = tbl.Rows[0];
                    rtn = new PersonFormGetDemographics();
                    rtn.PersonID = personid;
                    rtn.SharedID = dr["SharedID"].ToString();
                    rtn.SSNO = dr["SSNO"].ToString();
                    rtn.Prefix = dr["Prefix"].ToString();
                    rtn.FirstName = dr["FirstName"].ToString();
                    rtn.MiddleName = dr["MiddleName"].ToString();
                    rtn.LastName = dr["LastName"].ToString();
                    rtn.Suffix = dr["Suffix"].ToString();
                    rtn.BirthDate = GetDateTime(dr, "BirthDate", "1/1/1753");

                    rtn.PersonType = (pModels.PersonFormGetDemographics.PersonTypeEnum)Enum.Parse(typeof(pModels.PersonFormGetDemographics.PersonTypeEnum), dr["PersonTypeID"].ToString());
                    rtn.Sex = dr["Sex"].ToString();
                    rtn.RaceTypeID = GetInt(dr, "RaceTypeID");
                    rtn.LanguageTypeID = GetInt(dr, "LanguageTypeID");
                    rtn.EthnicityTypeID = GetInt(dr, "EthnicityTypeID");
                    rtn.PicturePath = dr["PicturePath"].ToString();
                    rtn.Note = dr["Note"].ToString();
                    rtn.UDF1 = dr["UDF1"].ToString();
                    rtn.UDF2 = dr["UDF2"].ToString();
                    rtn.UDF3 = dr["UDF3"].ToString();
                    rtn.UDF4 = dr["UDF4"].ToString();
                    rtn.UDF5 = dr["UDF5"].ToString();
                    rtn.UDF6 = dr["UDF6"].ToString();
                    rtn.UDF7 = dr["UDF7"].ToString();
                    rtn.UDF8 = dr["UDF8"].ToString();
                    rtn.UDF9 = dr["UDF9"].ToString();
                    rtn.UDF10 = dr["UDF10"].ToString();

                    rtn.comms = mCommon.cCtrl.GetCommunicationsListByPersonID(personid);
                    rtn.addresses = mCommon.aCtrl.GetAddressListByPersonID(personid);
                }

                else
                {
                    msg = "GetDemographics failed: Table null or other than 1 row.";
                    Trace.WriteLine(msg, "EZDesk");
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("GetPersonByID failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("personid", personid);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetPersonByID"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        public int UpDatePerson(PersonFormGetDemographics item, MySqlTransaction transaction)
        {
            string sql = "";
            MySqlCommand cmd = null;
            int rtn = -1;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "UpDatePerson"));
            try
            {
                step = "Build querry";
                if (item.PersonID > 0)
                {
                    sql = "UPDATE `per_Person` SET " +
                              "`SharedID`=@SharedID, " +
                              "`SSNO`=@SSNO, " +  
                              "`Prefix`=@Prefix, " +
                              "`FirstName`=@FirstName, " +
                              "`MiddleName`=@MiddleName, " +
                              "`LastName`=@LastName, " +
                              "`Suffix`=@Suffix, " +
                              "`BirthDate`=@BirthDate, " +
                              "`Sex`=@Sex, " +
                              "`RaceTypeID`=@RaceTypeID, " +
                              "`LanguageTypeID`=@LanguageTypeID, " +
                              "`EthnicityTypeID`=@EthnicityTypeID, " +
                              "`Note`=@Note, " + 
                              "`UDF1`=@UDF1, " +
                              "`UDF2`=@UDF2, " + 
                              "`UDF3`=@UDF3, " +
                              "`UDF4`=@UDF4, " +
                              "`UDF5`=@UDF5, " + 
                              "`UDF6`=@UDF6, " +
                              "`UDF7`=@UDF7, " + 
                              "`UDF8`=@UDF8, " + 
                              "`UDF9`=@UDF9, " +
                              "`UDF10`=@UDF10, " + 
                              "`PersonTypeID`=@PersonTypeID " +
                            "WHERE PersonID=@personid ";
                    cmd = new MySqlCommand(sql, mCommon.Connection, transaction);
                    cmd.Parameters.Add(new MySqlParameter("@personid", item.PersonID));
                }

                else
                {
                    sql = "INSERT INTO `per_Person` (`SharedID`, `SSNO`, `Prefix`, " +
                                        "`FirstName`, `MiddleName`, `LastName`, `Suffix`, `BirthDate`, " +
                                        "`Sex`, `RaceTypeID`, `LanguageTypeID`, `EthnicityTypeID`, " +
                                        "`Note`, `UDF1`, `UDF2`, `UDF3`, `UDF4`, `UDF5`, `UDF6`, " +
                                        "`UDF7`, `UDF8`, `UDF9`, `UDF10`, `PersonTypeID`) " +
                                    "VALUES(@SharedID, @SSNO, @Prefix, @FirstName, @MiddleName, " +
                                        "@LastName, @Suffix, @BirthDate, @Sex, @RaceTypeID, " +
                                        "@LanguageTypeID, @EthnicityTypeID, @Note, " +
                                        "@UDF1, @UDF2, @UDF3, @UDF4, @UDF5, " +
                                        "@UDF6, @UDF7, @UDF8, @UDF9, @UDF10, @PersonTypeID)";
                    cmd = new MySqlCommand(sql, mCommon.Connection, transaction);
                }

                cmd.Parameters.Add(new MySqlParameter("@SharedID", item.SharedID));
                cmd.Parameters.Add(new MySqlParameter("@SSNO", item.SSNO));
                cmd.Parameters.Add(new MySqlParameter("@Prefix", item.Prefix));
                cmd.Parameters.Add(new MySqlParameter("@FirstName", item.FirstName));
                cmd.Parameters.Add(new MySqlParameter("@MiddleName", item.MiddleName));
                cmd.Parameters.Add(new MySqlParameter("@LastName", item.LastName));
                cmd.Parameters.Add(new MySqlParameter("@Suffix", item.Suffix));
                cmd.Parameters.Add(new MySqlParameter("@BirthDate", item.BirthDate));
                cmd.Parameters.Add(new MySqlParameter("@Sex", item.Sex));
                cmd.Parameters.Add(new MySqlParameter("@RaceTypeID", item.RaceTypeID));
                cmd.Parameters.Add(new MySqlParameter("@LanguageTypeID", item.LanguageTypeID));
                cmd.Parameters.Add(new MySqlParameter("@EthnicityTypeID", item.EthnicityTypeID));
                cmd.Parameters.Add(new MySqlParameter("@Note", item.Note));
                cmd.Parameters.Add(new MySqlParameter("@UDF1", item.UDF1));
                cmd.Parameters.Add(new MySqlParameter("@UDF2", item.UDF2));
                cmd.Parameters.Add(new MySqlParameter("@UDF3", item.UDF3));
                cmd.Parameters.Add(new MySqlParameter("@UDF4", item.UDF4));
                cmd.Parameters.Add(new MySqlParameter("@UDF5", item.UDF5));
                cmd.Parameters.Add(new MySqlParameter("@UDF6", item.UDF6));
                cmd.Parameters.Add(new MySqlParameter("@UDF7", item.UDF7));
                cmd.Parameters.Add(new MySqlParameter("@UDF8", item.UDF8));
                cmd.Parameters.Add(new MySqlParameter("@UDF9", item.UDF9));
                cmd.Parameters.Add(new MySqlParameter("@UDF10", item.UDF10));
                cmd.Parameters.Add(new MySqlParameter("@PersonTypeID", item.PersonType));

                step = "Write data";
                int rc = ExecuteNonQueryCmd(cmd); 
                if (item.PersonID < 1)
                {
                    step = "Get LastInsertedId";
                    item.PersonID = (int)cmd.LastInsertedId;
                    rtn = item.PersonID;
                }
                else
                {
                    rtn = 0;
                }

                ///This should not be required.
                // Save the addresses
                if (item.addresses != null)
                {
                    step = "UpdateAddressList";
                    mCommon.aCtrl.UpdateAddressList(item.addresses, transaction);
                }

                // Save the phone numbers/email
                if (item.comms != null)
                {
                    step = "UpdateCommunicationsList";
                    mCommon.cCtrl.UpdateCommunicationsList(item.comms, transaction);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                EZException eze = new EZException("UpDatePerson failed", ex);
                eze.Add("step", step);
                eze.Add("sql", sql);
                eze.Add("item", item);
                throw eze;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpDatePerson"));
            }
        }

    }
}
