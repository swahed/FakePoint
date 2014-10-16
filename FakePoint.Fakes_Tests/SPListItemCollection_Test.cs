using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPListItemCollection_Test
    {
        string testFileName = "TestSiteCaml";

        int testListId = 11;

        SPListItemCollection items;

        [TestInitialize]
        public void Init()
        {
            SPContext.Initialize(testFileName);
            items = SPContext.Current.Web.Lists[testListId].Items;
        }

        [TestMethod]
        public void ItemsCountCorrect()
        {
            Assert.AreEqual(items.Count, 1);
        }
    }
}
