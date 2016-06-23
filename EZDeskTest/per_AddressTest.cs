using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EZDeskTest
{
    /// <summary>
    /// Summary description for per_AddressTest
    /// </summary>
    [TestClass]
    public class per_AddressTest
    {
        MySqlConnection mConn = null;
        EZDeskDataLayer.Address.AddressCtrl mCtrl = null;
        EZDeskDataLayer.Address.Models.Address mHomeAddr =
                new EZDeskDataLayer.Address.Models.Address();
        EZDeskDataLayer.Address.Models.Address mWorkAddr =
                new EZDeskDataLayer.Address.Models.Address();
        EZDeskDataLayer.Address.Models.Address mUpdatedHome =
                new EZDeskDataLayer.Address.Models.Address();

        public per_AddressTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            string conString = "Server=localhost;Database=ehr;Uid=ehruser;Pwd=password";
            mConn = new MySqlConnection(conString);
            mConn.Open();
            //#mCtrl = new EZDeskDataLayer.Address.AddressCtrl(mConn);

            mHomeAddr.AddressID = 0;
            mHomeAddr.Address1 = "Test Address Home";
            mHomeAddr.Address2 = "Test Address 2";
            mHomeAddr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home;
            mHomeAddr.City = "Test City";
            mHomeAddr.IsActive = true;
            mHomeAddr.PersonID = -100;   //Test UserID
            mHomeAddr.State = "XX";
            mHomeAddr.Zip = "11111";

            mWorkAddr.AddressID = 0;
            mWorkAddr.Address1 = "Test Address Work";
            mWorkAddr.Address2 = "Test Address 2";
            mWorkAddr.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Work;
            mWorkAddr.City = "Test City";
            mWorkAddr.IsActive = true;
            mWorkAddr.PersonID = -100;   //Test UserID
            mWorkAddr.State = "XX";
            mWorkAddr.Zip = "11111";

            mUpdatedHome.AddressID = 0;
            mUpdatedHome.Address1 = "Test Updated Address Home";
            mUpdatedHome.Address2 = "Test Address 2 Updated";
            mUpdatedHome.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home;
            mUpdatedHome.City = "Test City Updated";
            mUpdatedHome.IsActive = true;
            mUpdatedHome.PersonID = -100;   //Test UserID
            mUpdatedHome.State = "YY";
            mUpdatedHome.Zip = "22222";
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            mConn.Close();
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetAddressTypeByNameTest()
        {
            string msg = "";
            int num = -1;

            try
            {
                foreach (EZDeskDataLayer.Address.Models.Address.AddressTypeEnum e in
                        Enum.GetValues(typeof(EZDeskDataLayer.Address.Models.Address.AddressTypeEnum)).Cast<EZDeskDataLayer.Address.Models.Address.AddressTypeEnum>())
                {
                    num = mCtrl.GetAddressTypeByName(e.ToString());
                    if ((int)e != num)
                    {
                        msg += "GetAddressTypeByName for " + e.ToString() +
                                    " returned " + num.ToString() +
                                    ", expected " + ((int)e).ToString() + "\n";
                    }
                }

                Assert.AreEqual(0, msg.Length, msg);
            }

            catch (Exception ex)
            {
                Assert.AreEqual(0, msg.Length, msg);
                Assert.Fail("GetAddressTypeByNameTest failed" + ex.Message);
            }
        }

        /// <summary>
        /// Testing adding a Address List of 2 items
        /// </summary>
        [TestMethod]
        public void UpdateAddressListTest1()
        {
            List<EZDeskDataLayer.Address.Models.Address> addrs =
                new List<EZDeskDataLayer.Address.Models.Address>();
            string sql = "";
            MySqlCommand cmd = null;

            try
            {
                sql = "DELETE FROM `per_Address` WHERE `PersonID` = -100 ";
                cmd = new MySqlCommand(sql, mConn);
                cmd.ExecuteNonQuery();

                addrs.Add(mHomeAddr);
                addrs.Add(mWorkAddr);

                mCtrl.UpdateAddressList(addrs, null);
                Assert.AreNotEqual(0, addrs[0].AddressID, "Failed to update AddressID 1");
                Assert.AreNotEqual(0, addrs[1].AddressID, "Failed to update AddressID 2");
            }

            catch (Exception ex)
            {
                Assert.Fail("UpdateAddressListTest1 failed" + ex.Message);
            }
        }

        /// <summary>
        /// Testing the previously added 2 Address items are correct.
        /// </summary>
        [TestMethod]
        public void UpdateAddressListTest2()
        {
            List<EZDeskDataLayer.Address.Models.Address> addrs = null;
            string msg = "";
            int count = 0;

            try
            {
                addrs = mCtrl.GetAddressListByPersonID(-100);
                if (addrs != null)
                {
                    foreach (EZDeskDataLayer.Address.Models.Address addr in addrs)
                    {
                        switch ((int)addr.AddressType)
                        {
                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Work):
                                count++;
                                msg += zCompareAddrs(mWorkAddr, addr);
                                break;

                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home):
                                count++;
                                msg += zCompareAddrs(mHomeAddr, addr);
                                break;

                            default:
                                msg += "Unexpected address type: " + addr.AddressType.ToString() + "\n";
                                break;
                        }
                    }
                    Assert.AreEqual(0, msg.Length, msg);
                    Assert.AreEqual(2, count, "Should have been 2 address, found: " + addrs.Count.ToString());
                }

                else
                {
                    Assert.Fail("Failed to return any Addresses");
                }
            }

            catch (Exception ex)
            {
                Assert.Fail("Test failed: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void UpdateAddressListTest3()
        {
            List<EZDeskDataLayer.Address.Models.Address> addrs = null;
            string msg = "";
            int count = 0;

            try
            {
                addrs = mCtrl.GetAddressListByPersonID(-100);
                Assert.AreEqual(2, addrs.Count, "Incorrect number of addresses returned");

                if (addrs[0].AddressType == EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home)
                {
                    mUpdatedHome.AddressID = addrs[0].AddressID;
                    addrs[0] = mUpdatedHome;
                }
                else
                {
                    mUpdatedHome.AddressID = addrs[1].AddressID;
                    addrs[1] = mUpdatedHome;
                }
                mCtrl.UpdateAddressList(addrs, null);

                if (addrs != null)
                {
                    foreach (EZDeskDataLayer.Address.Models.Address addr in addrs)
                    {
                        switch ((int)addr.AddressType)
                        {
                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Work):
                                count++;
                                msg += zCompareAddrs(mWorkAddr, addr);
                                break;

                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home):
                                count++;
                                msg += zCompareAddrs(mUpdatedHome, addr);
                                break;

                            default:
                                msg += "Unexpected address type: " + addr.AddressType.ToString() + "\n";
                                break;
                        }
                    }
                    Assert.AreEqual(0, msg.Length, msg);
                    Assert.AreEqual(2, count, "Should have been 2 address, found: " + addrs.Count.ToString());
                }

                else
                {
                    Assert.Fail("Failed to return any Addresses");
                }
            }

            catch (Exception ex)
            {
                Assert.Fail("Test failed: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkAddr"></param>
        /// <param name="srcAddr"></param>
        /// <returns></returns>
        private string zCompareAddrs(EZDeskDataLayer.Address.Models.Address checkAddr,
                                EZDeskDataLayer.Address.Models.Address srcAddr)
        {
            string msg = "";

            if (checkAddr.Address1 != srcAddr.Address1) 
            { 
                msg += "Address1 doesn't match: found: " + checkAddr.Address1 + " expected: " + srcAddr.Address1 + "\n"; 
            }

            if (checkAddr.Address2 != srcAddr.Address2) 
            { 
                msg += "Address2 doesn't match: found: " + checkAddr.Address2 + " expected: " + srcAddr.Address2 + "\n"; 
            }

            if (checkAddr.AddressType != srcAddr.AddressType) 
            { 
                msg += "AddressType doesn't match: found: " + checkAddr.AddressType.ToString() + " expected: " + srcAddr.AddressType.ToString() + "\n"; 
            }

            if (checkAddr.City != srcAddr.City) 
            {
                msg += "City doesn't match: found: " + checkAddr.City + " expected: " + srcAddr.City + "\n"; 
            }

            if (checkAddr.IsActive != srcAddr.IsActive) 
            {
                msg += "IsActive doesn't match: found: " + checkAddr.IsActive.ToString() + " expected: " + srcAddr.IsActive.ToString() + "\n"; 
            }

            if (checkAddr.State != srcAddr.State) 
            {
                msg += "State doesn't match: found: " + checkAddr.State + " expected: " + srcAddr.State + "\n"; 
            }

            if (checkAddr.Zip != srcAddr.Zip) 
            {
                msg += "Zip doesn't match: found: " + checkAddr.Zip + " expected: " + srcAddr.Zip + "\n"; 
            }

            return msg;
        }

    }
}
