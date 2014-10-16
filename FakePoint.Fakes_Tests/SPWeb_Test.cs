using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPWeb_Test
    {
        string testFileName = "TestSiteCaml";
        Guid testSiteId = new Guid("{BC0D7FEA-75BA-4015-8B88-A7331AF06418}");
        Guid testRootWebId = new Guid("{23A258FF-CEB6-4ABD-9069-0EDD1991D5FD}");
        Guid testSubWebId = new Guid("{D5A29DC2-2C8B-4FF6-AC32-5E891D373B1C}");

        string testSiteUrl = "http://localhost/sites/teamsite";
        string testSubWebUrl = "http://localhost/sites/teamsite/subsite";

        SPWeb web;

        [TestInitialize]
        public void Init()
        {
            SPContext.Initialize(testFileName);
            SPSite site = new SPSite(testSiteId);
            web = site.OpenWeb(testSubWebUrl);
        }

        [TestMethod]
        public void WebHasCorrectId()
        {
            Assert.AreEqual(web.ID, testSubWebId);
        }

        [TestMethod]
        public void WebHasCorrectUrl()
        {
            Assert.AreEqual(web.Url, testSubWebUrl);
        }

        [TestMethod]
        public void AllowUnsafeUpdatesIsTrueAsDefault()
        { 
            Assert.IsTrue(web.AllowUnsafeUpdates);
        }

        [TestMethod]
        public void ListCollectionNotNull()
        {
            Assert.IsNotNull(web.Lists);
        }
        
        [TestMethod]
        public void ListCollectionCountIsCorrect()
        {
            Assert.AreEqual(web.Lists.Count, 19);
        }

        // Files not null
        // Files Count is correct

        // Folders not null
        // Folders Count is correct

        //GetFile Url returns correct file
        //GetFile ID returns correct file

        [TestMethod]
        public void DisposeInUsingBlock()
        {
            using (SPSite site = new SPSite(testSiteId))
            {
                using (SPWeb web = site.OpenWeb(testSubWebUrl))
                {
                    // TODO
                }
            }
        }

        // TODO:
        //[TestMethod]
        //public void DiposeDisposes()
        //{
        //    SPSite site = new SPSite(testSubWebUrl);
        //    SPWeb web = site.OpenWeb();
        //    web.Dispose();
        //    System.GC.WaitForPendingFinalizers();
        //    Assert.IsNull(web);
        //}
    }
}
