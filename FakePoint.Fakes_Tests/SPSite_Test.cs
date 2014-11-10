using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;
using System.Threading.Tasks;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPSite_Test
    {
        Guid testSiteId = new Guid("{BC0D7FEA-75BA-4015-8B88-A7331AF06418}");
        Guid testRootWebId = new Guid("{23A258FF-CEB6-4ABD-9069-0EDD1991D5FD}");
        Guid testSubWebId = new Guid("{D5A29DC2-2C8B-4FF6-AC32-5E891D373B1C}");
        Guid testSiteId2 = new Guid("{819135FE-402E-44BB-A4BA-34E9C8495A53}");

        string testSiteUrl = "http://localhost/sites/teamsite";
        string testSubWebUrl = "http://localhost/sites/teamsite/subsite";
        string testSiteUrl2 = "http://localhost/sites/anotherteamsite";

        string testSiteTitle = "SPSite Url=http://localhost/sites/teamsite";
        string testSiteTitle2 = "SPSite Url=http://localhost/sites/anotherteamsite";

        string testNonexistentWebUrl = "http://localhost/sites/nonexistent";

        [TestInitialize]
        public void Init()
        {
        }

        // TODO:
        //[TestMethod]
        //public void GetSiteAsync()
        //{ 
        //}

        [TestMethod]
        public void SiteHasCorrectId()
        {
            SPSite site = new SPSite(testSiteUrl);
            Assert.AreEqual(testSiteId, site.ID);
            site = new SPSite(testSiteId);
            Assert.AreEqual(testSiteId, site.ID);
            site = new SPSite(testSiteUrl2);
            Assert.AreEqual(testSiteId2, site.ID);
            site = new SPSite(testSiteId2);
            Assert.AreEqual(testSiteId2, site.ID);
        }

        // TODO: Test with relative urls (if supported by SharePoint)

        [TestMethod]
        public void SiteHasCorrectUrl()
        {
            SPSite site = new SPSite(testSiteId);
            Assert.AreEqual(testSiteUrl, site.Url);
            site = new SPSite(testSiteUrl);
            Assert.AreEqual(testSiteUrl, site.Url);
            site = new SPSite(testSubWebUrl);
            Assert.AreEqual(testSiteUrl, site.Url);
            site = new SPSite(testSiteUrl2);
            Assert.AreEqual(testSiteUrl2, site.Url);
            site = new SPSite(testSiteId2);
            Assert.AreEqual(testSiteUrl2, site.Url);
        }

        [TestMethod]
        public void SiteHasCorrectTitle()
        {
            SPSite site = new SPSite(testSiteId);
            Assert.AreEqual(testSiteTitle, site.Title);
            site = new SPSite(testSiteUrl);
            Assert.AreEqual(testSiteTitle, site.Title);
            site = new SPSite(testSubWebUrl);
            Assert.AreEqual(testSiteTitle, site.Title);
            site = new SPSite(testSiteUrl2);
            Assert.AreEqual(testSiteTitle2, site.Title);
            site = new SPSite(testSiteId2);
            Assert.AreEqual(testSiteTitle2, site.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))] // TODO: Type?
        public void NonExistentSiteThrows()
        {
            SPSite site = new SPSite(testNonexistentWebUrl);
        }

        // TODO: Correct site needs to be opened if the url of a subweb was entered

        [TestMethod]
        public void OpenWebOpenRootWebWhenSiteWasCreatedWithRootWebUrl()
        {
            SPSite site = new SPSite(testSiteUrl);
            SPWeb web = site.OpenWeb();
            Assert.AreEqual(testRootWebId, web.ID);
        }

        [TestMethod]
        public void OpenWebOpensubWebWhenSiteWasCreatedWithsubWebUrl()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb();
            Assert.AreEqual(testSubWebId, web.ID);
        }

        // TODO: Correct web needs to be opened if the url of an element within the web (i.e. list) has been entereds

        [TestMethod]
        public void OpenWebOpenRootWebByUrl()
        {
            SPSite site = new SPSite(testSiteUrl);
            SPWeb web = site.OpenWeb(testSiteUrl);
            Assert.AreEqual(testRootWebId, web.ID);

            SPSite site1 = new SPSite(testSubWebUrl);
            SPWeb web1 = site1.OpenWeb(testSiteUrl);
            Assert.AreEqual(testRootWebId, web1.ID);
        }

        [TestMethod]
        public void OpenWebOpensubWebByUrl()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb(testSubWebUrl);
            Assert.AreEqual(testSubWebId, web.ID);

            SPSite site1 = new SPSite(testSiteUrl);
            SPWeb web1 = site1.OpenWeb(testSubWebUrl);
            Assert.AreEqual(web1.ID, testSubWebId);
        }

        [TestMethod]
        public void OpenWebOpensubWebById()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb web = site.OpenWeb(testSubWebId);
            Assert.AreEqual(testSubWebId, web.ID);
        }

        [TestMethod]
        public void RootWebIsCorrect()
        {
            SPSite site = new SPSite(testSubWebUrl);
            SPWeb rootWeb = site.RootWeb;
            Assert.AreEqual(testRootWebId, rootWeb.ID);
            Assert.AreEqual(testSiteUrl, rootWeb.Url);
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
