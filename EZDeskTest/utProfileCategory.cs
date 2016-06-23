using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data;
using MySql.Data.MySqlClient;
using EZDeskDataLayer;
using EZDeskDataLayer.ehr;
using EZDeskDataLayer.ehr.Models;

namespace EZDeskTest
{
    /// <summary>
    /// Summary description for per_AddressTest
    /// </summary>
    [TestClass]
    public class utProfileCategory
    {
        MySqlConnection mConn = null;
        ProfileCategory mCat = null;
        EZDeskDataLayer.ehr.ehrCtrl eCtrl = null;
        private EZDeskCommon mCommon;

        public utProfileCategory()
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

            eCtrl = new ehrCtrl(mCommon);

            try
            {
                mCat = new ProfileCategory("EZDeskTest", "EZDesk test category");
                eCtrl.DeleteCategory(mCat.Category);
            }

            catch (Exception ex)
            {
                Assert.Fail("MyTestInitialize failed");
                throw ex;
            }
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            mConn.Close();
        }
        
        #endregion

        #region Category tests

        [TestMethod]
        public void CreateCategory()
        {
            mCat.ID = -1;
            eCtrl.WriteProfileCategory(mCat);
            Assert.AreNotEqual(-1, mCat.ID, "Profile Category ID not set");
        }

        [TestMethod]
        public void ReadCategory()
        {
            ProfileCategory readCat = eCtrl.GetCategory(mCat.Category);

            Assert.AreEqual(mCat.ID, readCat.ID, "Read ID wrong");
            Assert.AreEqual(mCat.Category, readCat.Category, "Read Category wrong");
            Assert.AreEqual(mCat.Description, readCat.Description, "Read Description wrong");
            Assert.AreEqual(mCat.IsActive, readCat.IsActive, "Read IsActive wrong");
        }

        [TestMethod]
        public void UpdateCategory()
        {
            mCat.Description = mCat.Description + " Modified";
            eCtrl.WriteProfileCategory(mCat);

            ProfileCategory readCat = eCtrl.GetCategory(mCat.Category);

            Assert.AreEqual(mCat.ID, readCat.ID, "UPDATE ID wrong");
            Assert.AreEqual(mCat.Category, readCat.Category, "Update Category wrong");
            Assert.AreEqual(mCat.Description, readCat.Description, "Update Description wrong");
            Assert.AreEqual(mCat.IsActive, readCat.IsActive, "Update IsActive wrong");
        }

        [TestMethod]
        public void ListCategory()
        {
            DataTable cats = eCtrl.GetProfileCategories();
            Boolean found = false;
            foreach (DataRow dr in cats.Rows)
            {
                if (mCat.ID == Convert.ToInt32(dr["ProfCatID"].ToString()))
                {
                    found = true;
                }
            }
            Assert.AreEqual(true, found, "Catagory not found in list");
        }

        #endregion
    }
}
