using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPField_Test
    {
        int testListId = 11;
        Guid testFieldId = new Guid("{fa564e0f-0c70-4ab9-b863-0177e6ddd247}");
        string testFieldName = "Title";

        SPList list;
        SPField field;

        [TestInitialize]
        public void Init()
        {
            list = SPContext.Current.Web.Lists[testListId];
            field = list.Fields[testFieldName];
        }

        //[TestMethod]
        //public void FieldExistsIsTrue()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void FieldExistsIsFalse()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void TitleIsCorrect()
        {
            Assert.AreEqual(testFieldName, field.Title);
        }

        //timeLastModified
        [TestMethod]
        public void TypeIsCorrect()
        {
            Assert.AreEqual(SPFieldType.Text, field.Type);
        }
        //InternalName
    }
}
