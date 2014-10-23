using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPSite_Test
    {
        Guid testSiteId = new Guid("{BC0D7FEA-75BA-4015-8B88-A7331AF06418}");
        Guid testRootWebId = new Guid("{23A258FF-CEB6-4ABD-9069-0EDD1991D5FD}");
        Guid testSubWebId = new Guid("{D5A29DC2-2C8B-4FF6-AC32-5E891D373B1C}");

        string testSiteUrl = "http://localhost/sites/teamsite";
        string testSubWebUrl = "http://localhost/sites/teamsite/subsite";

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void SiteHasCorrectId()
        {
            SPSite site = new SPSite(testSiteUrl);
            Assert.AreEqual(site.ID, testSiteId);
            site = new SPSite(testSiteId);
            Assert.AreEqual(site.ID, testSiteId);
        }

        // TODO: Test with relative urls (if supported by SharePoint)

        [TestMethod]
        public void SiteHasCorrectUrl()
        {
            SPSite site = new SPSite(testSiteId);
            Assert.AreEqual(site.Url, testSiteUrl);
            site = new SPSite(testSiteUrl);
            Assert.AreEqual(site.Url, testSiteUrl);
            site = new SPSite(testSubWebUrl);
            Assert.AreEqual(site.Url, testSiteUrl);
        }

        // TODO: Correct site needs to be opened if the url of a subweb was entered

        [TestMethod]
        public void OpenWebOpenRootWebWhenSiteWasCreatedWithRootWebUrl()
        {
            SPSite site = new SPSite(testSiteUrl);
            SPWeb web = site.OpenWeb();
            Assert.AreEqual(web.ID, testRootWebId);
        }

        [TestMethod]
        public void OpenWebOpensubWebWhenSiteWasCreatedWithsubWebUrl()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb();
            Assert.AreEqual(web.ID, testSubWebId);
        }

        // TODO: Correct web needs to be opened if the url of an element within the web (i.e. list) has been entereds

        [TestMethod]
        public void OpenWebOpenRootWebByUrl()
        {
            SPSite site = new SPSite(testSiteUrl);
            SPWeb web = site.OpenWeb(testSiteUrl);
            Assert.AreEqual(web.ID, testRootWebId);

            SPSite site1 = new SPSite(testSubWebUrl);
            SPWeb web1 = site1.OpenWeb(testSiteUrl);
            Assert.AreEqual(web1.ID, testRootWebId);
        }

        [TestMethod]
        public void OpenWebOpensubWebByUrl()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb(testSubWebUrl);
            Assert.AreEqual(web.ID, testSubWebId);

            SPSite site1 = new SPSite(testSiteUrl);
            SPWeb web1 = site1.OpenWeb(testSubWebUrl);
            Assert.AreEqual(web1.ID, testSubWebId);
        }

        [TestMethod]
        public void OpenWebOpensubWebById()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb(testSubWebId);
            Assert.AreEqual(web.ID, testSubWebId);
        }

        [TestMethod]
        public void RootWebIsCorrect()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb rootWeb = site.RootWeb;
            Assert.AreEqual(rootWeb.ID, testRootWebId);
            Assert.AreEqual(rootWeb.Url, testSiteUrl);
        }

        [TestMethod]
        public void DisposeInUsingBlock()
        {
            using (SPSite site = new SPSite(testSiteId))
            {
            }
        }

        // TODO:
        //[TestMethod]
        //public void DiposeDisposes()
        //{
        //    SPSite site = new SPSite(testSubWebUrl);
        //    site.Dispose();
        //    System.GC.WaitForPendingFinalizers();
        //    Assert.IsNull(site);
        //}
    }
}
