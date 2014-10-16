using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakePoint.Explorer;
using Microsoft.SharePoint;
using System.IO;

namespace FakePoint.SPExplorer_Tests
{
    [TestClass]
    public class SPExplorerDocument_Test
    {
        SPExplorerDocument document;

        public SPExplorerDocument_Test()                        // TODO: This is called on every test. Should be called as a one-time test setup method
        {
            SPContext.Initialize("..\\TestResults\\test_site");     // TODO: This should check if the test file exist and prompt the user with instructions to create ones
            document = new SPExplorerDocument();
            document.IncludeContent = true;
            document.ReadFromLocalFarm();
        }

        static string currentSourceNode = "";
        private string GetExpected(string name)
        {
            XmlNode node = SPContext.content.SelectSingleNode(currentSourceNode + "/@" + name);
            if (node == null) return null;

            return node.Value;
        }

        private bool AttributesMatch(XmlNode node, string name)
        {
            XmlAttribute attr = node.Attributes[name];
            if (attr == null) return GetExpected(name) == null;
            return attr.Value == GetExpected(name);
        }

        [TestMethod]
        public void WriteTo()
        {
            string filepath = "test";
            document.SaveToFile(filepath);
            XmlDocument savedDocument = new XmlDocument();
            savedDocument.Load(filepath);
            Assert.IsTrue(savedDocument.DocumentElement.Name == "Farm");
        }

        [TestMethod]
        public void Farm()
        {
            currentSourceNode = "//Farm";
            XmlNode node = document.SelectSingleNode(currentSourceNode);
            Assert.IsNotNull(node);
            Assert.IsTrue(AttributesMatch(node, "Name"));
            Assert.IsTrue(AttributesMatch(node, "DisplayName"));
            Assert.IsTrue(AttributesMatch(node, "ID"));
        }

        [TestMethod]
        public void WebServices()
        {
          currentSourceNode = "//Farm/WebServices";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
        }

        [TestMethod]
        public void WebService()
        {
          currentSourceNode = "//Farm/WebServices/WebService";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "DisplayName"));
          Assert.IsTrue(AttributesMatch(node, "ID"));
        }

        [TestMethod]
        public void WebApplications()
        {
          currentSourceNode = "//Farm//WebApplications";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
        }

        [TestMethod]
        public void WebApplication()
        {
          currentSourceNode = "//Farm//WebApplications/WebApplication";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "ID"));
          Assert.IsTrue(AttributesMatch(node, "DisplayName"));
        }

        [TestMethod]
        public void Sites()
        {
          currentSourceNode = "//Farm//Sites";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
        }

        [TestMethod]
        public void Site()
        {
          currentSourceNode = "//Farm//Sites/Site";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "ID"));
          Assert.IsTrue(AttributesMatch(node, "Url"));
        }

        [TestMethod]
        public void Web()
        {
          currentSourceNode = "//Farm//Sites/Site/Web";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "Description"));
          Assert.IsTrue(AttributesMatch(node, "ID"));
          Assert.IsTrue(AttributesMatch(node, "Url"));
        }

        [TestMethod]
        public void SubWebs()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/Webs/Web";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "Description"));
          Assert.IsTrue(AttributesMatch(node, "ID"));
          Assert.IsTrue(AttributesMatch(node, "Url"));
        }

        [TestMethod]
        public void RootFolder()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/Files/Folder";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
        }

        [TestMethod]
        public void Folder()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/Files/Folder/Folder";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
        }

        [TestMethod]
        public void SubFolder()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/Files/Folder/Folder/Folder";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
        }

        [TestMethod]
        public void File()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/Files/Folder/Folder/Folder/File";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Name"));
        }

        [TestMethod]
        public void Lists()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/UserLists";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
        }

        [TestMethod]
        public void List()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/UserLists/List";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "ID"));
          Assert.IsTrue(AttributesMatch(node, "Title"));
          Assert.IsTrue(AttributesMatch(node, "Description"));
          Assert.IsTrue(AttributesMatch(node, "Url"));
          Assert.IsTrue(AttributesMatch(node, "OrderedList"));
          Assert.IsTrue(AttributesMatch(node, "AllowDeletion"));
          Assert.IsTrue(AttributesMatch(node, "Direction"));
          Assert.IsTrue(AttributesMatch(node, "BaseType"));
          Assert.IsTrue(AttributesMatch(node, "ServerTemplate"));
          Assert.IsTrue(AttributesMatch(node, "DisableAttachments"));
          Assert.IsTrue(AttributesMatch(node, "FolderCreation"));
          Assert.IsTrue(AttributesMatch(node, "DisallowContentTypes"));
          Assert.IsTrue(AttributesMatch(node, "Version"));
        }

        [TestMethod]
        public void ListData()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/UserLists/List/Data/Rows/Row";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.AreEqual(node.SelectSingleNode("Field[@Name='Title']").InnerXml, "Chapter 1");
          Assert.AreEqual(node.SelectSingleNode("Field[@Name='Ordinal']").InnerXml, "1000");
          Assert.AreEqual(node.SelectSingleNode("Field[@Name='ContentType']").InnerXml, "Chapters");
          Assert.AreEqual(node.SelectSingleNode("Field[@Name='Created']").InnerXml, "23/02/2007 16:29:40");
          Assert.AreEqual(node.SelectSingleNode("Field[@Name='Author']").InnerXml, "1;#FS1001\\administrator");
        }

        [TestMethod]
        public void ListForms()
        {
          currentSourceNode = "//Farm//Sites/Site/Web/UserLists/List/Forms/Form";
          XmlNode node = document.SelectSingleNode(currentSourceNode);
          Assert.IsNotNull(node);
          Assert.IsTrue(AttributesMatch(node, "Type"));
          Assert.IsTrue(AttributesMatch(node, "Name"));
          Assert.IsTrue(AttributesMatch(node, "Url"));
          Assert.IsTrue(AttributesMatch(node, "Default"));
        }

        private void AssertDocumentIsOk(SPExplorerDocument documentToCheck)
        {
          XmlNode node;
          node = documentToCheck.SelectSingleNode("//Farm");
          Assert.IsNotNull(node);
          Assert.AreEqual(node.Attributes["ID"].Value, "{C9C1B078-5DB9-4193-B342-4BB6DCF15799}");
          Assert.AreEqual(node.Attributes["DisplayName"].Value, "Test farm");
          Assert.AreEqual(node.Attributes["Name"].Value, "Test");
          node = documentToCheck.SelectSingleNode("//Farm//WebService");
          Assert.IsNotNull(node);
          Assert.AreEqual(node.Attributes["ID"].Value, "{AAB329C5-ECFE-4883-8D7C-22FEFA8343CD}");
          Assert.AreEqual(node.Attributes["DisplayName"].Value, "Test web service");
          Assert.AreEqual(node.Attributes["Name"].Value, "Test");
          node = documentToCheck.SelectSingleNode("//Farm//WebApplication");
          Assert.IsNotNull(node);
          Assert.AreEqual(node.Attributes["ID"].Value, "{19447D7D-ED91-48F5-932C-A62A5FB36B51}");
          Assert.AreEqual(node.Attributes["DisplayName"].Value, "Test web application");
          Assert.AreEqual(node.Attributes["Name"].Value, "Test");
        }

        [TestMethod]
        public void ReadFromFile()
        {
          document.SaveToFile("test_ReadFromFile.xml");
          SPExplorerDocument documentToRead = new SPExplorerDocument();
          documentToRead.ReadFromFile("test_ReadFromFile.xml");
          AssertDocumentIsOk(documentToRead);
        }

        [TestMethod]
        public void ReadFromString()
        {
          SPExplorerDocument documentToRead = new SPExplorerDocument();
          documentToRead.ReadFromString(@"<?xml version=""1.0"" encoding=""utf-16""?>
    <Farm ID=""{C9C1B078-5DB9-4193-B342-4BB6DCF15799}"" DisplayName=""Test farm"" Name=""Test"">
      <WebServices>
        <WebService ID=""{AAB329C5-ECFE-4883-8D7C-22FEFA8343CD}"" DisplayName=""Test web service"" Name=""Test"">
          <WebApplications>
            <WebApplication ID=""{19447D7D-ED91-48F5-932C-A62A5FB36B51}"" DisplayName=""Test web application"" Name=""Test"">
            </WebApplication>
          </WebApplications>
        </WebService>
      </WebServices>
    </Farm>
    ");
          AssertDocumentIsOk(documentToRead);
        }

        private void AssertContentIsOk(string content)
        {
          Assert.IsTrue(content.Contains("<Farm ID="));
          Assert.IsTrue(content.Contains("<WebServices>"));
          Assert.IsTrue(content.Contains("</WebServices>"));
          Assert.IsTrue(content.Contains("<Web ID="));
          Assert.IsTrue(content.Contains("<Sites>"));
          Assert.IsTrue(content.Contains("<Folder Name=\"\">"));
          Assert.IsTrue(content.Contains("<Folder Name=\"Lists\">"));
          Assert.IsTrue(content.Contains("<Folder Name=\"Categories\">"));
          Assert.IsTrue(content.Contains("<File Name=\"AllCategories.aspx\" Title=\"\" />"));
          Assert.IsTrue(content.Contains("<UserLists>"));
        }

        [TestMethod]
        public void SaveToFile()
        {
          document.SaveToFile("test_SaveToFile.xml");
          var sr = new StreamReader("test_SaveToFile.xml");
          string content = sr.ReadToEnd();
          sr.Close();
          Assert.IsTrue(content.StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\"?>"));
          AssertContentIsOk(content);
        }

        [TestMethod]
        public void SaveToString()
        {
          string content = document.SaveToString();
          Assert.IsTrue(content.StartsWith("<?xml version=\"1.0\" encoding=\"utf-16\"?>"));
          AssertContentIsOk(content);
        }
    }
}
