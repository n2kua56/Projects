using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EZDeskDataLayer.Address.Models;
using MySql.Data.MySqlClient;
using EZUtils;

namespace EZDeskDataLayer.Address
{
    public class AddressCtrl : Controller
    {
        private string mModName = "AddressCtrl";

        public AddressCtrl(MySqlConnection conn)
        {
            Trace.Enter(Trace.RtnName(mModName, "AddressCtrl-Constructor"));
            Init(conn);
            Trace.Exit(Trace.RtnName(mModName, "AddressCtrl-Constructor"));
        }

        /// <summary>
        /// Get the Address Type for the specified description
        /// </summary>
        /// <param name="key">Description: Undefined/Home/Work/Business</param>
        /// <returns>The AddressTypeID</returns>
        /// <remarks>
        /// The per_AddressType is considered to be one per_Address related
        /// tables. It matches up to the enum in Models.AddressType.cs.
        /// </remarks>
        public int GetAddressTypeByName(string key)
        {
            int rtn = -1;
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAddressTypeByName"));

            try
            {
                step = "Build querry";
                sql = "SELECT at.`AddressTypeID` " +
                        "FROM `per_AddressType` AS at " +
                        "WHERE at.`Description`=@key ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@key", key));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    rtn = GetInt(tbl.Rows[0], "AddressTypeID");
                }

                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetAddressTypeByName"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("key", key);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAddressTypeByName"));
            }
        }

        /// <summary>
        /// Get a List of addresses by PersonID
        /// </summary>
        /// <param name="personid">PersonID of the person to return the Address List</param>
        /// <returns>List of addresses</returns>
        public List<Models.Address> GetAddressListByPersonID(int personid)
        {
            List<Models.Address> rtn = null;
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAddressListByPersonID"));

            try
            {
                step = "Build querry";
                sql = "SELECT a.`AddressID`, a.`PersonID`, a.`AddressTypeID`, a.`Created`, " +
                            "a.`Modified`, a.`IsActive`, a.`Address1`, a.`Address2`, a.`City`, " +
                            "a.`State`, a.`Zip` " +
                        "FROM `per_Address` AS a " +
                        "WHERE a.`PersonID`=@personid ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));

                step = "Get data";
                tbl = GetDataTable(cmd);

                step = "Pull data";
                rtn = new List<Models.Address>();
                foreach (DataRow dr in tbl.Rows)
                {
                    Models.Address temp = new Models.Address();
                    temp.Address1 = dr["Address1"].ToString();
                    temp.Address2 = dr["Address2"].ToString();
                    temp.AddressID = (int)dr["AddressID"];
                    temp.AddressType = (Address.Models.Address.AddressTypeEnum)Enum.Parse(typeof(Address.Models.Address.AddressTypeEnum), dr["AddressTypeID"].ToString());
                    temp.City = dr["City"].ToString();

                    try { temp.Created = Convert.ToDateTime(dr["Created"].ToString()); }
                    catch { temp.Created = DateTime.MinValue; }

                    try { temp.Modified = Convert.ToDateTime(dr["Modified"].ToString()); }
                    catch { temp.Modified = DateTime.MinValue; }

                    string act = dr["IsActive"].ToString();
                    temp.IsActive = ("true" == act.ToLower());
                    temp.PersonID = personid;
                    temp.State = dr["State"].ToString();
                    temp.Zip = dr["Zip"].ToString();

                    rtn.Add(temp);
                }

                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetAddressListByPersonID"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("PersonID", personid);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAddressListByPersonID"));
            }
        }

        /// <summary>
        /// Insert/Update all address in the List<> of Address.
        /// </summary>
        /// <param name="addr">List of addresses to Update/Insert</param>
        /// <param name="transaction"></param>
        public void UpdateAddressList(List<Models.Address> addrs, MySqlTransaction transaction)
        {
            string sql = "";
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "UpdateAddressList"));

            try
            {
                foreach (Models.Address addr in addrs)
                {
                    //Check for the item already existing
                    //Update the ones that previously existed
                    step = "Build querry";
                    if (addr.AddressID > 0)
                    {
                        sql = "UPDATE `per_Address` SET " +
                                    "`Address1`=@address1, " +
                                    "`Address2`=@address2, " +
                                    "`City`=@city, " +
                                    "`State`=@state, " +
                                    "`Zip`=@zip, " +
                                    "`AddressTypeID`=@addresstypeid " +
                                "WHERE `AddressID`=@addressid ";
                    }

                    else
                    {
                        sql = "INSERT INTO `per_Address` (`Address1`, `Address2`, " +
                                    "`City`, `State`, `Zip`, `AddressTypeID`, " +
                                    "`PersonID`) " +
                                "VALUES(@address1, @address2, @city, @state, @zip, " +
                                    "@addresstypeid, @personid) ";
                    }

                    MySqlCommand cmd = new MySqlCommand(sql, mConn);
                    if (transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }
                    cmd.Parameters.Add(new MySqlParameter("@address1", addr.Address1));
                    cmd.Parameters.Add(new MySqlParameter("@address2", addr.Address2));
                    cmd.Parameters.Add(new MySqlParameter("@city", addr.City));
                    cmd.Parameters.Add(new MySqlParameter("@state", addr.State));
                    cmd.Parameters.Add(new MySqlParameter("@zip", addr.Zip));
                    cmd.Parameters.Add(new MySqlParameter("@addresstypeid", (int)addr.AddressType));
                    if (addr.AddressID > 0)
                    {
                        cmd.Parameters.Add(new MySqlParameter("@addressid", addr.AddressID));
                    }
                    else
                    {
                        cmd.Parameters.Add(new MySqlParameter("@personid", addr.PersonID));
                    }

                    step = "Write data";
                    cmd.ExecuteNonQuery();
                    if (addr.AddressID < 1)
                    {
                        step = "Get LastInsertedID";
                        addr.AddressID = (int)cmd.LastInsertedId;
                    }
                }
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "UpdateAddressList"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("addrs", addrs);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "UpdateAddressList"));
            }

        }

        /// <summary>
        /// Get an address by personID and type.
        /// </summary>
        /// <param name="personid">PersonID to return the address for</param>
        /// <param name="typ">AddressType (name) of the address type to return</param>
        /// <returns></returns>
        public Models.Address GetAddressByIDandType(int personid, string typ)
        {
            Models.Address rtn = null;
            string sql = "";
            DataTable tbl = null;
            string step = "";

            Trace.Enter(Trace.RtnName(mModName, "GetAddressByIDandType"));

            try
            {
                step = "Build querry";
                sql = "SELECT a.`AddressID`, a.`PersonID`, a.`AddressTypeID`, t.`Description`, " +
                            "a.`Created`, a.`Modified`, a.`IsActive`, a.`Address1`, `a.Address2`, " +
                            "a.`City`, a.`State`, a.`Zip` " +
                        "FROM `per_Address` AS a " +
                            "JOIN `per_AddressType` AS t ON (t.`AddressTypeID`=a.`AddressTypeID`) " +
                        "WHERE a.`PersonID`=@personid " +
                            "t.`Description`=@typ ";
                MySqlCommand cmd = new MySqlCommand(sql, mConn);
                cmd.Parameters.Add(new MySqlParameter("@personid", personid));
                cmd.Parameters.Add(new MySqlParameter("@typ", typ));

                step = "Get data";
                tbl = GetDataTable(cmd);
                if ((tbl != null) && (tbl.Rows.Count == 1))
                {
                    step = "Pull data";
                    rtn = new Models.Address();
                    DataRow dr = tbl.Rows[0];
                    rtn.Address1 = dr["Address1"].ToString();
                    rtn.Address2 = dr["Address2"].ToString();
                    rtn.AddressID = (int)dr["AddressID"];
                    rtn.AddressType = (Address.Models.Address.AddressTypeEnum)Enum.Parse(typeof(Address.Models.Address.AddressTypeEnum), dr["AddressType"].ToString());
                    rtn.City = dr["City"].ToString();

                    try { rtn.Created = (DateTime)dr["Created"]; }
                    catch { rtn.Created = DateTime.MinValue; }

                    try { rtn.Modified = (DateTime)dr["Modified"]; }
                    catch { rtn.Modified = DateTime.MinValue; }

                    rtn.IsActive = 1 == (int)dr["IsActive"];
                    rtn.PersonID = personid;
                    rtn.State = dr["State"].ToString();
                    rtn.Zip = dr["Zip"].ToString();
                }

                return rtn;
            }

            catch (Exception ex)
            {
                ex.Data.Add("Routine", Trace.RtnName(mModName, "GetAddressByIDandType"));
                ex.Data.Add("step", step);
                ex.Data.Add("sql", sql);
                ex.Data.Add("personid", personid);
                ex.Data.Add("typ", typ);
                throw ex;
            }

            finally
            {
                Trace.Exit(Trace.RtnName(mModName, "GetAddressByIDandType"));
            }
        }

    }
}
