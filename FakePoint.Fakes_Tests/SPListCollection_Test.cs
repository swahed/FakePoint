using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPListCollection_Test
    {
        SPListCollection collection;

        [TestInitialize]
        public void Init()
        {
            collection = SPContext.Current.Web.Lists;
        }

        [TestMethod]
        public void ListCollectionCountIsCorrect()
        {
            Assert.AreEqual(19, collection.Count);
        }

        [TestMethod]
        public void GetListByIdReturnsCorrectList()
        {
            SPList list = collection[11];
            Assert.AreEqual("Site Pages", list.Title);
        }

        [TestMethod]
        public void GetListByNameReturnsCorrectList()
        {
            SPList list = collection["Site Pages"];
            Assert.AreEqual(new Guid("{0A780E47-DD15-4C9C-A91A-3FDD1C815BB8}"), list.ID);
        }

        [TestMethod]
        public void TryGetListReturnsCorrectList()
        {
            SPList list = collection.TryGetList("Site Pages");
            Assert.AreEqual(new Guid("{0A780E47-DD15-4C9C-A91A-3FDD1C815BB8}"), list.ID);
        }

        [TestMethod]
        public void TryGetListReturnsNullIfListDoesNotExist()
        {
            SPList list = collection.TryGetList("Nonexistent list");
            Assert.IsNull(list);
        }

        [TestMethod]
        public void IteratorWorks()
        {
            foreach (SPList list in collection)
            {
                string s = list.Title;
            }
        }             
    }
}
