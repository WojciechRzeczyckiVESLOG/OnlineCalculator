using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ServerFunctionality;
using DatabaseConnection;
using MySql.Data.MySqlClient;
using System.Collections.Generic;



namespace Tests
{
    [TestClass]
    public class DatabaseTest
    {
        private static MySqlConnection connection;
        private static DBConnect database = new DBConnect();

        [ClassInitialize]
        public static void initialize(TestContext context)
        {
            DBConnect db = new DBConnect();
            string connectionString = "SERVER=localhost;DATABASE=users;UID=test;PASSWORD=test;";
            connection = new MySqlConnection(connectionString);
        }

        [TestMethod]
        public void CheckTest()
        {
            database.SetUser("test", "test");
            int answer = database.Check();
            Assert.AreEqual(1, answer);
        }

        [TestMethod]
        public void SelectTest()
        {     
            List<string>[] list = new List<string>[5];
            list = database.Select();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void CountTest()
        {
            database.SetUser("test", "test");
            int count = 0;
            count = database.Count();
            Assert.IsNotNull(count);
        }

    }
}
