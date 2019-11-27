using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic.Tests
{
    [TestClass]
    public class RatingManagerTests
    {
        private RatingManager manager;

        [TestInitialize()]
        public void InitializeBeforeEachTest()
        {
            manager = new RatingManager();
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
            Rating r = new Rating();
            r.Description = "TestReturn";
            int id = manager.Create(r);

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
            Rating r = new Rating();
            r.Description = "Test";
            int id = manager.Create(r);

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Update_Returns_Updated_Object()
        {
            Rating r = new Rating();
            r.Description = "Update";
            r.Id = manager.Create(r);

            r.Description = "UPDATED";
            Rating updated = manager.Update(r);

            Assert.AreEqual("UPDATED", r.Description);
        }

        [TestMethod]
        public void Update_Returns_Null_If_Not_Exists()
        {
            Rating r = new Rating();
            r.Id = -2;
            Rating actual = manager.Update(r);

            Assert.IsTrue(actual == null);
        }

        [TestMethod]
        public void Delete_Returns_True_If_Deleted_Existing()
        {
            Rating r = new Rating();
            r.Description = "Test Delete";
            int id = manager.Create(r);

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
