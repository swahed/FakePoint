using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPList_Test
    {
        string testFileName = "TestSiteCaml";
        int testListId = 11;

        SPList list;

        [TestInitialize]
        public void Init()
        {
            SPContext.Initialize(testFileName);
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

        // GetItems returns not null

        // GetItems returns correct number of items -> SPListItemsCollection

        // GetItemById returns correct item

        // GetItemByUniqueId returns correct item

        // Update() does nothing?

        // Deletes() deletes // TODO: Doesn't thsi need allowunsafeupdates ans web.Update() ?
    }
}
