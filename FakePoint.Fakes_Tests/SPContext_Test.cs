using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;
using System.IO;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPContext_Test
    {
        // TODO: Test:             SPContext.Initialize(testFileName);

        [TestMethod]
        public void CurrentIsNotNull()
        {
            Assert.IsNotNull(SPContext.Current);
        }

        [TestMethod]
        public void CurrentSiteIsNotNull()
        {
            Assert.IsNotNull(SPContext.Current.Site);
        }

        [TestMethod]
        public void CurrentSiteHasCorrectId()
        {
            Assert.AreEqual(SPContext.Current.Site.ID, new Guid("{BC0D7FEA-75BA-4015-8B88-A7331AF06418}"));
        }

        [TestMethod]
        public void CurrentWebIsNotNull()
        {
            Assert.IsNotNull(SPContext.Current.Web);
        }

        [TestMethod]
        public void CurrentWebHasCorrectId()
        {
            Assert.AreEqual(SPContext.Current.Web.ID, new Guid("{23A258FF-CEB6-4ABD-9069-0EDD1991D5FD}"));
        }        
    }
}
