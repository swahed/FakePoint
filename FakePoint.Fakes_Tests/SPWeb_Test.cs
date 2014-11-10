﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPWeb_Test
    {
        Guid testRootWebId = new Guid("{23A258FF-CEB6-4ABD-9069-0EDD1991D5FD}");
        Guid testSubWebId = new Guid("{D5A29DC2-2C8B-4FF6-AC32-5E891D373B1C}");

        string testSubWebUrl = "http://localhost/sites/teamsite/subsite";

        string testRootWebTitle = "Teamsite Rootweb";
        string testSubWebTitle = "Teamsite Subweb";

        SPSite Site;

        [TestInitialize]
        public void Init()
        {
            Site = SPContext.Current.Site;
        }

        [TestMethod]
        public void WebHasCorrectId()
        {
            SPWeb web = Site.OpenWeb(testSubWebUrl);
            Assert.AreEqual(testSubWebId, web.ID);
        }

        [TestMethod]
        public void WebHasCorrectUrl()
        {
            SPWeb web = Site.OpenWeb(testSubWebId);
            Assert.AreEqual(testSubWebUrl, web.Url);
        }

        [TestMethod]
        public void WebHasCorrectTitle()
        {
            SPWeb web = Site.RootWeb;
            Assert.AreEqual(testRootWebTitle, web.Title);
            web = Site.OpenWeb(testSubWebId);
            Assert.AreEqual(testSubWebTitle, web.Title);
        }

        [TestMethod]
        public void AllowUnsafeUpdatesIsTrueAsDefault()
        {
            SPWeb web = Site.RootWeb;
            Assert.IsTrue(web.AllowUnsafeUpdates);
        }

        [TestMethod]
        public void ListCollectionNotNull()
        {
            SPWeb web = Site.RootWeb;
            Assert.IsNotNull(web.Lists);
        }

        // Files not null
        // Files Count is correct

        // Folders not null
        // Folders Count is correct
        
        // RootFolder
        
        // GetFile Url returns correct file
        // GetFile ID returns correct file

        [TestMethod]
        public void DisposeInUsingBlock()
        {
            string ID = null;
            using (var web = Site.OpenWeb(testSubWebUrl))
            {
                ID = web.ID.ToString("B").ToUpper();
            }
            Assert.AreEqual(testSubWebId.ToString("B").ToUpper(), ID);
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
