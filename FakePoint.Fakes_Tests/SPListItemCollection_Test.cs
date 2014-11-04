using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPListItemCollection_Test
    {
        int testListId = 11;

        SPListItemCollection items;

        [TestInitialize]
        public void Init()
        {
            items = SPContext.Current.Web.Lists[testListId].Items;
        }

        [TestMethod]
        public void ItemsCountCorrect()
        {
            Assert.AreEqual(1, items.Count);
        }
    }
}
