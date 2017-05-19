﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace KellermanSoftware.CompareNetObjectsTests
{
    [TestFixture]
    public class CompareDataRowTests
    {

        #region Class Variables
        private CompareLogic _compare;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [OneTimeSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            _compare = new CompareLogic();
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            _compare = null;
        }
        #endregion

        #region Tests
        [Test]
        public void DataRowNegativeDataTest()
        {
            DataSet ds1 = CreateMockDataset();
            DataSet ds2 = Common.CloneWithSerialization(ds1);
            ds2.Tables[0].Rows[2][0] = "Chunky Chocolate Heaven";
            Assert.IsFalse(_compare.Compare(ds1.Tables[0].Rows[2], ds2.Tables[0].Rows[2]).AreEqual);
        }

        [Test]
        public void DataRowDeletedTest()
        {
            DataSet ds1 = CreateMockDataset();
            DataSet ds2 = Common.CloneWithSerialization(ds1);
            ds2.Tables[0].Rows[2].Delete();
            Assert.IsFalse(_compare.Compare(ds1.Tables[0], ds2.Tables[0]).AreEqual);
        }
        #endregion

        #region Supporting Methods
        private DataSet CreateMockDataset()
        {
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable("IceCream");
            ds1.Tables.Add(dt);
            dt.Columns.Add("Flavor", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            DataRow dr = dt.NewRow();
            dr["Flavor"] = "Chocolate";
            dr["Price"] = 1.99M;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Flavor"] = "Vanilla";
            dr["Price"] = 1.98M;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Flavor"] = "Banana Prune Delight";
            dr["Price"] = 2.99M;
            dt.Rows.Add(dr);
            return ds1;
        }
        #endregion
    }
}
