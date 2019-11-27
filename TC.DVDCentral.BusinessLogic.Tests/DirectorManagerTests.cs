using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TC.DVDCentral.BusinessLogic;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic.Tests
{
    [TestClass]
    public class DirectorManagerTests
    {
        private DirectorManager manager;

        [TestInitialize()]
        public void InitializeBeforeEachTest()
        {
            manager = new DirectorManager();
        }

        [TestCleanup()]
        public void CleanUpAfterEachTest()
        {
            manager.Dispose();
        }

        [TestMethod]
        public void Get_All_Returns_More_Than_One()
        {
            var all = manager.GetAll();
            Assert.IsTrue(all.Count > 1);
        }

        [TestMethod]
        public void Get_By_Id_Returns_Existing()
        {
            Director d = new Director();
            d.FirstName = "TestReturn";
            d.LastName = "Test";
            int id = manager.Create(d);

            var actual = manager.GetById(id);
            Assert.IsNotNull(actual);
            Assert.AreEqual("TestReturn", actual.FirstName);
        }

        [TestMethod]
        public void Get_By_Id_Returns_Null_If_Not_Exists()
        {
            var actual = manager.GetById(-2);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Create_Returns_New_Id()
        {
            Director d = new Director();
            d.FirstName = "Test";
            d.LastName = "Last";
            int id = manager.Create(d);

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Update_Returns_Updated_Object()
        {
            Director d = new Director();
            d.FirstName = "Update";
            d.LastName = "Test";
            d.Id = manager.Create(d);

            d.FirstName = "UPDATED";
            Director updated = manager.Update(d);

            Assert.AreEqual("UPDATED", d.FirstName);
        }

        [TestMethod]
        public void Update_Returns_Null_If_Not_Exists()
        {
            Director d = new Director();
            d.Id = -2;
            Director actual = manager.Update(d);

            Assert.IsTrue(actual == null);
        }

        [TestMethod]
        public void Delete_Returns_True_If_Deleted_Existing()
        {
            Director d = new Director();
            d.FirstName = "Test Delete";
            d.LastName = "Test";
            int id = manager.Create(d);

            bool actual = manager.Delete(id);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Delete_Returns_False_If_Not_Exists()
        {
            bool actual = manager.Delete(-2);
            Assert.IsFalse(actual);
        }
    }
}
