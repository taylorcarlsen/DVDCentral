using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic.Tests
{
    [TestClass]
    public class FormatManagerTests
    {
        private FormatManager manager;

        [TestInitialize()]
        public void InitializeBeforeEachTest()
        {
            manager = new FormatManager();
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
            Format f = new Format();
            f.Description = "TestReturn";
            int id = manager.Create(f);

            var actual = manager.GetById(id);
            Assert.IsNotNull(actual);
            Assert.AreEqual("TestReturn", actual.Description);
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
            Format f = new Format();
            f.Description = "Test";
            int id = manager.Create(f);

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Update_Returns_Updated_Object()
        {
            Format f = new Format();
            f.Description = "Update";
            f.Id = manager.Create(f);

            f.Description = "UPDATED";
            Format updated = manager.Update(f);

            Assert.AreEqual("UPDATED", f.Description);
        }

        [TestMethod]
        public void Update_Returns_Null_If_Not_Exists()
        {
            Format f = new Format();
            f.Id = -2;
            Format actual = manager.Update(f);

            Assert.IsTrue(actual == null);
        }

        [TestMethod]
        public void Delete_Returns_True_If_Deleted_Existing()
        {
            Format f = new Format();
            f.Description = "Test Delete";
            int id = manager.Create(f);

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
