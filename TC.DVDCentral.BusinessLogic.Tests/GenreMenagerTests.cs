using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic.Tests
{
    [TestClass]
    public class GenreMenagerTests
    {
        private GenreManager manager;

        [TestInitialize()]
        public void InitializeBeforeEachTest()
        {
            manager = new GenreManager();
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
            Genre g = new Genre();
            g.Description = "TestReturn";
            int id = manager.Create(g);

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
            Genre g = new Genre();
            g.Description = "Test";
            int id = manager.Create(g);

            Assert.IsTrue(id > 0);
        }

        [TestMethod]
        public void Update_Returns_Updated_Object()
        {
            Genre g = new Genre();
            g.Description = "Update";
            g.Id = manager.Create(g);

            g.Description = "UPDATED";
            Genre updated = manager.Update(g);

            Assert.AreEqual("UPDATED", g.Description);
        }

        [TestMethod]
        public void Update_Returns_Null_If_Not_Exists()
        {
            Genre g = new Genre();
            g.Id = -2;
            Genre actual = manager.Update(g);

            Assert.IsTrue(actual == null);
        }

        [TestMethod]
        public void Delete_Returns_True_If_Deleted_Existing()
        {
            Genre g = new Genre();
            g.Description = "Test Delete";
            int id = manager.Create(g);

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
