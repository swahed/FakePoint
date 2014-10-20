using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPList_Test
    {
        int testListId = 11;

        SPList list;

        [TestInitialize]
        public void Init()
        {
            list = SPContext.Current.Web.Lists[testListId];
        }

        [TestMethod]
        public void TitleIsCorrect()
        {
            Assert.AreEqual(list.Title, "Site Pages");
        }

        [TestMethod]
        public void ItemsIsNotNull()
        {
            Assert.IsNotNull(list.Items);
        }

        [TestMethod]
        public void ItemsCountCorrect()
        {
            Assert.AreEqual(list.ItemCount, 1);
        }

        [TestMethod]
        public void FieldsIsNotNull()
        {
            Assert.IsNotNull(list.Fields);
        }

        [TestMethod]
        public void ItemsReturnsNotNull()
        {
            SPListItemCollection items = list.Items;
            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void GetItemsReturnsNotNull()
        {
            SPListItemCollection items = list.Items;
            items = list.GetItems(new SPQuery());
            Assert.IsNotNull(items);
        }

        // GetItems returns correct number of items -> SPListItemsCollection
        [TestMethod]
        public void GetItemsRetrunsCorrectNumberOfitems()
        {
            SPListItemCollection items = list.Items;
            items = list.GetItems(new SPQuery());
            Assert.AreEqual(items.Count, 1);
        }

        [TestMethod]
        public void GetItemByIdReturnsCorrectItem()
        {
            SPItem item = list.GetItemById(11); 
            Assert.AreEqual(item.ID, 11);
        }

        // Update() does nothing?
        //[TestMethod]
        //public void UpdateUpdates()
        //{
        //    list.Update();
        //    Assert.Fail();
        //}

        // Deletes() deletes // TODO: Doesn't thsi need allowunsafeupdates ans web.Update() ?
        //[TestMethod]
        //public void DeleteDeletes()
        //{
        //    list.Delete();
        //    Assert.IsNull(list);
        //}
    }
}
