﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPFieldCollection_Test
    {
        int testListId = 11;
        SPFieldCollection fields;

        [TestInitialize]
        public void Init()
        {
            fields = SPContext.Current.Web.Lists[testListId].Fields;
        }

        [TestMethod]
        public void FieldsCountCorrect()
        {
            Assert.AreEqual(71, fields.Count);
        }

        // TODO: [] int, Guid, string
    }
}
