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
    /// Summary description for per_CommunicationsTest
    /// </summary>
    [TestClass]
    public class per_CommunicationsTest
    {
        MySqlConnection mConn = null;
        EZDeskDataLayer.Communications.CommunicationCtrl mCtrl = null;
        EZDeskDataLayer.Communications.Models.Communication mHomePhone =
                new EZDeskDataLayer.Communications.Models.Communication();
        EZDeskDataLayer.Communications.Models.Communication mWorkPhone =
                new EZDeskDataLayer.Communications.Models.Communication();
        EZDeskDataLayer.Communications.Models.Communication mUpdatedHome =
                new EZDeskDataLayer.Communications.Models.Communication();

        public per_CommunicationsTest()
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
            mCtrl = new EZDeskDataLayer.Communications.CommunicationCtrl(mConn);

            mHomePhone.CommunicationID = 0;
            mHomePhone.CommunicationCode = "(845)240-1234";
            mHomePhone.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone;
            mHomePhone.IsActive = true;
            mHomePhone.PersonID = -100;

            mWorkPhone.CommunicationID = 0;
            mWorkPhone.CommunicationCode = "(845)380-9876";
            mWorkPhone.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone;
            mWorkPhone.IsActive = true;
            mWorkPhone.PersonID = -100;

            mUpdatedHome.CommunicationID = 0;
            mUpdatedHome.CommunicationCode = "(111)111-1111";
            mUpdatedHome.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone;
            mUpdatedHome.IsActive = true;
            mUpdatedHome.PersonID = -100;
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
        public void GetCommunicationTypeByNameTest()
        {
            string msg = "";
            int num = -1;

            try
            {
                foreach (EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum c in
                        Enum.GetValues(typeof(EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum)).Cast<EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum>())
                {
                    num = mCtrl.GetCommunicationTypeByName(c.ToString());
                    if ((int)c != num)
                    {
                        msg += "GetCommunicationTypeByNameTest for " + c.ToString() +
                                    " returned " + num.ToString() +
                                    ", expected " + ((int)c).ToString() + "\n";
                    }
                }

                Assert.AreEqual(0, msg.Length, msg);
            }

            catch (Exception ex)
            {
                Assert.AreEqual(0, msg.Length, msg);
                Assert.Fail("GetCommunicationTypeByNameTest failed" + ex.Message);
            }
        }

        /// <summary>
        /// Testing adding a Communications List of 2 items
        /// </summary>
        [TestMethod]
        public void UpdateCommunicationListTest1()
        {
            List<EZDeskDataLayer.Communications.Models.Communication> comms =
                new List<EZDeskDataLayer.Communications.Models.Communication>();
            string sql = "";
            MySqlCommand cmd = null;

            try
            {
                sql = "DELETE FROM `per_Communication` WHERE `PersonID` = -100 ";
                cmd = new MySqlCommand(sql, mConn);
                cmd.ExecuteNonQuery();

                comms.Add(mHomePhone);
                comms.Add(mWorkPhone);

                mCtrl.UpdateCommunicationsList(comms, null);
                Assert.AreNotEqual(0, comms[0].CommunicationID, "Failed to update Communications 1");
                Assert.AreNotEqual(0, comms[1].CommunicationID, "Failed to update Communications 2");
            }

            catch (Exception ex)
            {
                Assert.Fail("UpdateCommunicationListTest1 failed" + ex.Message);
            }
        }

        /// <summary>
        /// Testing the previously added 2 Communication items are correct.
        /// </summary>
        [TestMethod]
        public void UpdateCommunicationListTest2()
        {
            List<EZDeskDataLayer.Communications.Models.Communication> comms = null;
            string msg = "";
            int count = 0;

            try
            {
                comms = mCtrl.GetCommunicationsListByPersonID(-100);
                if (comms != null)
                {
                    foreach (EZDeskDataLayer.Communications.Models.Communication comm in comms)
                    {
                        switch ((int)comm.CommunicationType)
                        {
                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone):
                                count++;
                                msg += zCompareComms(mWorkPhone, comm);
                                break;

                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home):
                                count++;
                                msg += zCompareComms(mHomePhone, comm);
                                break;

                            default:
                                msg += "Unexpected address type: " + comm.CommunicationType.ToString() + "\n";
                                break;
                        }
                    }
                    Assert.AreEqual(0, msg.Length, msg);
                    Assert.AreEqual(2, count, "Should have been 2 address, found: " + comms.Count.ToString());
                }

                else
                {
                    Assert.Fail("Failed to return any Communications List");
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
        public void UpdateCommunicationListTest3()
        {
            List<EZDeskDataLayer.Communications.Models.Communication> comms = null;
            string msg = "";
            int count = 0;

            try
            {
                comms = mCtrl.GetCommunicationsListByPersonID(-100);
                Assert.AreEqual(2, comms.Count, "Incorrect number of Communications returned");

                if (comms[0].CommunicationType == EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone)
                {
                    mUpdatedHome.CommunicationID = comms[0].CommunicationID;
                    comms[0] = mUpdatedHome;
                }
                else
                {
                    mUpdatedHome.CommunicationID = comms[1].CommunicationID;
                    comms[1] = mUpdatedHome;
                }
                mCtrl.UpdateCommunicationsList(comms, null);

                if (comms != null)
                {
                    foreach (EZDeskDataLayer.Communications.Models.Communication comm in comms)
                    {
                        switch ((int)comm.CommunicationType)
                        {
                            case ((int)EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.WorkPhone):
                                count++;
                                msg += zCompareComms(mWorkPhone, comm);
                                break;

                            case ((int)EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home):
                                count++;
                                msg += zCompareComms(mUpdatedHome, comm);
                                break;

                            default:
                                msg += "Unexpected address type: " + comm.CommunicationType.ToString() + "\n";
                                break;
                        }
                    }
                    Assert.AreEqual(0, msg.Length, msg);
                    Assert.AreEqual(2, count, "Should have been 2 communications, found: " + comms.Count.ToString());
                }

                else
                {
                    Assert.Fail("Failed to return any Communications");
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
        private string zCompareComms(EZDeskDataLayer.Communications.Models.Communication checkComm,
                                EZDeskDataLayer.Communications.Models.Communication srcComm)
        {
            string msg = "";

            if (checkComm.CommunicationCode != srcComm.CommunicationCode) 
            { 
                msg += "CommunicationCode doesn't match: found: " + srcComm.CommunicationCode + " expected: " + checkComm.CommunicationCode +  "\n"; 
            }

            if (checkComm.CommunicationType != srcComm.CommunicationType) 
            { 
                msg += "CommunicationType doesn't match: found: " + srcComm.CommunicationType.ToString() + " expected: " + checkComm.CommunicationType.ToString() + "\n"; 
            }

            if (checkComm.IsActive != srcComm.IsActive) 
            { 
                msg += "IsActive doesn't match: found: " + srcComm.IsActive.ToString() + " expected: " + checkComm.IsActive.ToString() + "\n"; 
            }

            if (checkComm.PersonID != srcComm.PersonID) 
            { 
                msg += "PersonID doesn't match: found: " + srcComm.PersonID.ToString() + " expected: " + checkComm.PersonID.ToString() + "\n"; 
            }

            return msg;
        }

    }
}
