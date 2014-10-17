using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.SharePoint;

namespace FakePoint.Fakes_Tests
{
    [TestClass]
    public class SPField_Test
    {
        string testFileName = "TestSiteCaml";
        int testListId = 11;
        string testListName = "Testfield";

        int testfieldIndex = 10;
        Guid testFieldId = new Guid("{fa564e0f-0c70-4ab9-b863-0177e6ddd247}");
        string testFieldName = "Title";

        SPList list;
        SPField field;

        [TestInitialize]
        public void Init()
        {
            SPContext.Initialize(testFileName);
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
            Assert.AreEqual(field.Title, testFieldName);
        }

        //timeLastModified
        [TestMethod]
        public void TypeIsCorrect()
        {
            Assert.AreEqual(field.Type, SPFieldType.Text);
        }
        //InternalName
    }
}
