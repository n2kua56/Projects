using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data;
using MySql.Data.MySqlClient;
using cModels = EZDeskDataLayer.Communications.Models;
using aModels = EZDeskDataLayer.Address.Models;
using pModels = EZDeskDataLayer.Person.Models;

namespace EZDeskTest
{
    /// <summary>
    /// Summary description for per_Person
    /// </summary>
    [TestClass]
    public class per_PersonTest
    {
        MySqlConnection mConn = null;
        EZDeskDataLayer.Person.PersonCtrl mCtrl = null;
        pModels.PersonFormGetDemographics mPerson =
                new pModels.PersonFormGetDemographics();
        aModels.Address mAddress = new aModels.Address();
        cModels.Communication mCommunication =
                new cModels.Communication();
        public per_PersonTest()
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
        
        //Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            string conString = "Server=localhost;Database=ehr;Uid=ehruser;Pwd=password";
            mConn = new MySqlConnection(conString);
            mConn.Open();
            mCtrl = new EZDeskDataLayer.Person.PersonCtrl(mConn);

            mAddress.AddressID = 0;
            mAddress.Address1 = "Test Address Home";
            mAddress.Address2 = "Test Address 2";
            mAddress.AddressType = EZDeskDataLayer.Address.Models.Address.AddressTypeEnum.Home;
            mAddress.City = "Test City";
            mAddress.IsActive = true;
            mAddress.PersonID = 0;   //Unknown Test UserID
            mAddress.State = "XX";
            mAddress.Zip = "11111";

            mCommunication.CommunicationID = 0;
            mCommunication.CommunicationCode = "(845)240-1234";
            mCommunication.CommunicationType = EZDeskDataLayer.Communications.Models.Communication.CommunicationsTypeEnum.HomePhone;
            mCommunication.IsActive = true;
            mCommunication.PersonID = 0;

            mPerson.BirthDate = new DateTime(1956, 5, 4);
            mPerson.EthnicityTypeID = 1;
            mPerson.FirstName = "Test First Name";
            mPerson.LanguageTypeID = 1;
            mPerson.LastName = "Test Last Name";
            mPerson.MiddleName = "Test Middle Name";
            mPerson.Note = "Test Person";
            mPerson.PersonID = 0;   //Unknown test person id
            mPerson.PersonType = pModels.PersonFormGetDemographics.PersonTypeEnum.Doctor;
            mPerson.Prefix = "Dr";
            mPerson.RaceTypeID = 1;
            mPerson.Sex = "M";
            mPerson.SharedID = "UnitTest";
            mPerson.SSNO = "222-22-2222";
            mPerson.Suffix = "MD";
            mPerson.UDF1 = "Test UDF1";
            mPerson.UDF2 = "Test UDF2";
            mPerson.UDF3 = "Test UDF3";
            mPerson.UDF4 = "Test UDF4";
            mPerson.UDF5 = "Test UDF5";
            mPerson.UDF6 = "Test UDF6";
            mPerson.UDF7 = "Test UDF7";
            mPerson.UDF8 = "Test UDF8";
            mPerson.UDF9 = "Test UDF9";
            mPerson.UDF10 = "Test UDF10";
        }
        
        //Use TestCleanup to run code after each test has run
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
        public void GetPersonTypeByNameTest()
        {
            string msg = "";
            int num = -1;

            try
            {
                foreach (pModels.PersonFormGetDemographics.PersonTypeEnum e in
                        Enum.GetValues(typeof(pModels.PersonFormGetDemographics.PersonTypeEnum)).Cast<EZDeskDataLayer.Address.Models.Address.AddressTypeEnum>())
                {
                    num = mCtrl.GetPersonTypeByName(e.ToString());
                    if ((int)e != num)
                    {
                        msg += "GetPersonTypeByName for " + e.ToString() +
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
        public void UpdatePersonTest1()
        {
            List<EZDeskDataLayer.Address.Models.Address> addrs =
                new List<EZDeskDataLayer.Address.Models.Address>();
            string sql = "";
            MySqlCommand cmd = null;

            try
            {
                sql = "DELETE FROM `per_Person` WHERE `Note` = 'Test Person'";
                cmd = new MySqlCommand(sql, mConn);
                cmd.ExecuteNonQuery();

                mPerson.addresses = new List<aModels.Address>();
                mPerson.addresses.Add(mAddress);

                mPerson.comms = new List<cModels.Communication>();
                mPerson.comms.Add(mCommunication);

                mCtrl.UpDatePerson(mPerson, null);
                Assert.AreNotEqual(0, mPerson.PersonID, "Failed to update Person");
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
        public void UpdatePersonTest2()
        {
            pModels.PersonFormGetDemographics inPerson = null;
            string msg = "";

            try
            {
                inPerson = mCtrl.GetPersonByID(mPerson.PersonID);
                if (inPerson != null)
                {
                    msg += zComparePerson(mPerson, inPerson, "");
                    
                    Assert.AreEqual(0, msg.Length, msg);
                }

                else
                {
                    Assert.Fail("Failed to return Person");
                }
            }

            catch (Exception ex)
            {
                Assert.Fail("UpdatePersonTest2 failed: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void UpdatePersonTest3()
        {
            pModels.PersonFormGetDemographics inPerson = null;
            string msg = "";
            MySqlTransaction transaction = null;

            try
            {
                inPerson = mCtrl.GetPersonByID(mPerson.PersonID);
                inPerson.FirstName = "NEW TEST FIRST NAME";

                transaction = mConn.BeginTransaction();
                mCtrl.UpDatePerson(inPerson, transaction);
                transaction.Commit();

                inPerson = mCtrl.GetPersonByID(mPerson.PersonID);
                msg += zComparePerson(mPerson, inPerson, "NEW TEST FIRST NAME");
                Assert.AreEqual(0, msg.Length, msg);
            }

            catch (Exception ex)
            {
                Assert.Fail("UpdatePersonTest3 failed: " + ex.Message);
                transaction.Rollback();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="inPerson"></param>
        /// <returns></returns>
        private string zComparePerson(pModels.PersonFormGetDemographics orig,
                        pModels.PersonFormGetDemographics inPerson, string fname)
        {
            string msg = "";

            if (orig.BirthDate != inPerson.BirthDate)
            {
                msg += "BirthDate doesn't match: found: " + inPerson.BirthDate.ToString() +
                        " expected: " + orig.BirthDate.ToString() + "\n";
            }

            if (orig.EthnicityTypeID != inPerson.EthnicityTypeID)
            {
                msg += "EthnicityTypeID doesn't match: found: " + inPerson.EthnicityTypeID.ToString() +
                        " expected: " + orig.EthnicityTypeID.ToString() + "\n";
            }

            if (fname.Length == 0)
            {
                if (orig.FirstName != inPerson.FirstName)
                {
                    msg += "FirstName doesn't match: found: " + inPerson.FirstName +
                            " expected: " + orig.FirstName + "\n";
                }
            }
            else
            {
                if (fname != inPerson.FirstName)
                {
                    msg += "FirstName doesn't match: found: " + inPerson.FirstName +
                            " expected: " +fname + "\n";
                }
            }

            if (orig.LanguageTypeID != inPerson.LanguageTypeID)
            {
                msg += "LanguageTypeID doesn't match: found: " + inPerson.LanguageTypeID.ToString() +
                        " expected: " + orig.LanguageTypeID.ToString() + "\n";
            }

            if (orig.LastName != inPerson.LastName)
            {
                msg += "LastName doesn't match: found: " + inPerson.LastName +
                        " expected: " + orig.LastName + "\n";
            }

            if (orig.MiddleName != inPerson.MiddleName)
            {
                msg += "MiddleName doesn't match: found: " + inPerson.MiddleName +
                        " expected: " + orig.MiddleName + "\n";
            }

            if (orig.Note != inPerson.Note)
            {
                msg += "Note doesn't match: found: " + inPerson.Note +
                        " expected: " + orig.Note + "\n";
            }

            if (orig.PersonType != inPerson.PersonType)
            {
                msg += "PersonType doesn't match: found: " + inPerson.PersonType.ToString() +
                        " expected: " + orig.PersonType.ToString() + "\n";
            }

            if (orig.Prefix != inPerson.Prefix)
            {
                msg += "Prefix doesn't match: found: " + inPerson.Prefix +
                        " expected: " + orig.Prefix + "\n";
            }

            if (orig.RaceTypeID != inPerson.RaceTypeID)
            {
                msg += "RaceTypeID doesn't match: found: " + inPerson.RaceTypeID.ToString() +
                        " expected: " + orig.RaceTypeID.ToString() + "\n";
            }

            if (orig.Sex != inPerson.Sex)
            {
                msg += "Sex doesn't match: found: " + inPerson.Sex +
                        " expected: " + orig.Sex + "\n";
            }

            if (orig.SharedID != inPerson.SharedID)
            {
                msg += "SharedID doesn't match: found: " + inPerson.SharedID +
                        " expected: " + orig.SharedID + "\n";
            }

            if (orig.SSNO != inPerson.SSNO)
            {
                msg += "SSNO doesn't match: found: " + inPerson.SSNO +
                        " expected: " + orig.SSNO + "\n";
            }

            if (orig.Suffix != inPerson.Suffix)
            {
                msg += "Suffix doesn't match: found: " + inPerson.Suffix +
                        " expected: " + orig.Suffix + "\n";
            }

            if (orig.UDF1 != inPerson.UDF1)
            {
                msg += "UDF1 doesn't match: found: " + inPerson.UDF1 +
                        " expected: " + orig.UDF1 + "\n";
            }

            if (orig.UDF2 != inPerson.UDF2)
            {
                msg += "UDF2 doesn't match: found: " + inPerson.UDF2 +
                        " expected: " + orig.UDF2 + "\n";
            }

            if (orig.UDF3 != inPerson.UDF3)
            {
                msg += "UDF3 doesn't match: found: " + inPerson.UDF3 +
                        " expected: " + orig.UDF3 + "\n";
            }

            if (orig.UDF4 != inPerson.UDF4)
            {
                msg += "UDF4 doesn't match: found: " + inPerson.UDF4 +
                        " expected: " + orig.UDF4 + "\n";
            }

            if (orig.UDF5 != inPerson.UDF5)
            {
                msg += "UDF5 doesn't match: found: " + inPerson.UDF5 +
                        " expected: " + orig.UDF5 + "\n";
            }

            if (orig.UDF6 != inPerson.UDF6)
            {
                msg += "UDF6 doesn't match: found: " + inPerson.UDF6 +
                        " expected: " + orig.UDF6 + "\n";
            }

            if (orig.UDF7 != inPerson.UDF7)
            {
                msg += "UDF7 doesn't match: found: " + inPerson.UDF7 +
                        " expected: " + orig.UDF7 + "\n";
            }

            if (orig.UDF8 != inPerson.UDF8)
            {
                msg += "UDF8 doesn't match: found: " + inPerson.UDF8 +
                        " expected: " + orig.UDF8 + "\n";
            }

            if (orig.UDF9 != inPerson.UDF9)
            {
                msg += "UDF9 doesn't match: found: " + inPerson.UDF9 +
                        " expected: " + orig.UDF9 + "\n";
            }

            if (orig.UDF10 != inPerson.UDF10)
            {
                msg += "UDF10 doesn't match: found: " + inPerson.UDF10 +
                        " expected: " + orig.UDF10 + "\n";
            }

            if ((inPerson.addresses == null) ||
                (inPerson.addresses.Count != orig.addresses.Count))
            {
                msg += "Addresses copied wrong\n";
            }
            else
            {
                msg += zCompareAddrs(inPerson.addresses[0], orig.addresses[0]);
            }

            if ((inPerson.comms == null) ||
                (inPerson.comms.Count != orig.comms.Count))
            {
                msg += "Communications copied wrong\n";
            }
            else
            {
                msg += zCompareComms(inPerson.comms[0], orig.comms[0]);
            }

            return msg;
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
                msg += "CommunicationCode doesn't match: found: " + srcComm.CommunicationCode + " expected: " + checkComm.CommunicationCode + "\n";
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
