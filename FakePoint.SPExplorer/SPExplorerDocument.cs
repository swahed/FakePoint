using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;

namespace FakePoint.Explorer
{
    public class SPExplorerDocument : XmlDocument
    {
        public bool IncludeContent { get; set; }

        public static XmlWriterSettings WriteSettings()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;
            settings.NewLineOnAttributes = false;
            settings.OmitXmlDeclaration = false;
            return settings;
        }

        public void ReadFromLocalFarm()
        {
            if (SPFarm.Local == null) throw new ApplicationException("No local farm exists");
            RemoveAll();
            XmlElement element = Serialize_SPFarm(SPFarm.Local);
            AppendChild(element);
        }

        public void ReadFromFile(string filepath)
        {
            RemoveAll();
            Load(filepath);
        }

        public void ReadFromString(string content)
        {
            RemoveAll();
            LoadXml(content);
        }

        public void SaveToFile(string filepath)
        {
            XmlWriter xw = XmlWriter.Create(filepath, WriteSettings());
            WriteTo(xw);
            xw.Close();
        }

        public string SaveToString()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb, WriteSettings());
            WriteTo(xw);
            xw.Close();
            return sb.ToString();
        }

        public XmlElement Serialize_SPFarm(SPFarm farm)
        {
            XmlElement element = CreateElement("Farm");
            element.SetAttribute("ID", "{" + SPFarm.Local.Id.ToString().ToUpper() + "}");
            element.SetAttribute("DisplayName", SPFarm.Local.DisplayName);
            element.SetAttribute("Name", SPFarm.Local.Name);
            element.AppendChild(Serialize_SPWebServices(new SPWebServiceCollection(SPFarm.Local)));
            return element;
        }

        public XmlElement Serialize_SPWebServices(SPWebServiceCollection services)
        {
            XmlElement element = CreateElement("WebServices");
            foreach (SPWebService service in services)
                element.AppendChild(Serialize_SPWebService(service));
            return element;
        }

        public XmlElement Serialize_SPWebService(SPWebService service)
        {
            XmlElement element = CreateElement("WebService");
            element.SetAttribute("ID", "{" + service.Id.ToString().ToUpper() + "}");
            element.SetAttribute("DisplayName", service.DisplayName);
            element.SetAttribute("Name", service.Name);
            element.AppendChild(Serialize_SPWebApplications(service.WebApplications));
            return element;
        }

        public XmlElement Serialize_SPWebApplications(SPWebApplicationCollection applications)
        {
            XmlElement element = CreateElement("WebApplications");
            foreach (SPWebApplication application in applications)
                if (!application.IsAdministrationWebApplication)
                    element.AppendChild(Serialize_SPWebApplication(application));
            return element;
        }

        public XmlElement Serialize_SPWebApplication(SPWebApplication application)
        {
            XmlElement element = CreateElement("WebApplication");
            element.SetAttribute("ID", "{" + application.Id.ToString().ToUpper() + "}");
            element.SetAttribute("DisplayName", application.DisplayName);
            element.SetAttribute("Name", application.Name);
            element.AppendChild(Serialize_SPSites(application.Sites));
            return element;
        }

        public XmlElement Serialize_SPSites(SPSiteCollection sitecollections)
        {
            XmlElement element = CreateElement("Sites");
            if (sitecollections != null)
                foreach (SPSite sitecollection in sitecollections)
                    element.AppendChild(Serialize_SPSite(sitecollection));
            return element;
        }

        public XmlElement Serialize_SPSite(SPSite sitecollection)
        {
            XmlElement element = CreateElement("Site");
            element.SetAttribute("ID", "{" + sitecollection.ID.ToString().ToUpper() + "}");
            element.SetAttribute("DisplayName", sitecollection.ToString());
            element.SetAttribute("Name", sitecollection.ToString());
            element.SetAttribute("Url", sitecollection.Url);

            try
            {
                element.AppendChild(Serialize_SPWeb(sitecollection.RootWeb));

            }
            catch (Exception ex)
            {
                throw new Exception("Error in site " + sitecollection.Url, ex);
            }
            return element;
        }

        public XmlElement Serialize_SPWebs(SPWebCollection sites)
        {
            XmlElement element = CreateElement("Webs");
            if (sites != null)
                foreach (SPWeb web in sites)
                    element.AppendChild(Serialize_SPWeb(web));

            return element;
        }

        public XmlElement Serialize_SPWeb(SPWeb web)
        {
            XmlElement element = CreateElement("Web");
            element.SetAttribute("ID", "{" + web.ID.ToString().ToUpper() + "}");
            element.SetAttribute("Description", web.Description);
            element.SetAttribute("Name", web.Name);
            element.SetAttribute("Url", web.Url);
            element.AppendChild(Serialize_SPWebs(web.Webs));
            if (web.RootFolder != null)
            {
                XmlElement filesElement = CreateElement("Files");
                filesElement.AppendChild(Serialize_SPFolder(web.RootFolder));
                element.AppendChild(filesElement);
            }
            XmlElement listsElement = CreateElement("UserLists");
            foreach (SPList list in web.Lists)
                listsElement.AppendChild(Serialize_SPList(list));
            element.AppendChild(listsElement);
            return element;
        }

        public XmlElement Serialize_SPFolder(SPFolder folder)
        {
            XmlElement element = CreateElement("Folder");
            element.SetAttribute("Name", folder.Name);
            if (folder.SubFolders != null)
                foreach (SPFolder subfolder in folder.SubFolders)
                    element.AppendChild(Serialize_SPFolder(subfolder));
            if (folder.Files != null)
                foreach (SPFile file in folder.Files)
                    element.AppendChild(Serialize_SPFile(file));
            return element;
        }

        public XmlElement Serialize_SPFile(SPFile file)
        {
            XmlElement element = CreateElement("File");
            element.SetAttribute("Name", file.Name);
            element.SetAttribute("Title", file.Title);
            return element;
        }

        public XmlElement Serialize_SPList(SPList list)
        {
            XmlDocumentFragment fragment = CreateDocumentFragment();
            fragment.InnerXml = list.SchemaXml;
            XmlElement listElement = fragment.FirstChild as XmlElement;
            if (listElement == null) return null;
            // add forms elements
            XmlDocumentFragment formsFragment = CreateDocumentFragment();
            if (list.Forms != null)
            {
                formsFragment.InnerXml = list.Forms.SchemaXml;
                XmlElement formsElement = formsFragment.FirstChild as XmlElement;
                if (formsElement != null) listElement.AppendChild(formsElement);
            }
            // add data elements
            if (IncludeContent)
            {
                XmlElement dataElement = CreateElement("Data");
                XmlElement rowsElement = CreateElement("Rows");
                dataElement.AppendChild(rowsElement);
                listElement.AppendChild(dataElement);
                SPListItemCollection items = list.Items;
                foreach (SPListItem item in items)
                {
                    XmlElement rowElement = CreateElement("Row");
                    rowsElement.AppendChild(rowElement);
                    foreach (SPField field in item.Fields)
                    {
                        object value = item[field.InternalName];
                        if (value == null) continue;
                        XmlElement fieldElement = CreateElement("Field");
                        rowElement.AppendChild(fieldElement);
                        try
                        {
                            fieldElement.SetAttribute("Name", field.InternalName);
                            fieldElement.InnerText = value.ToString();
                        }
                        catch
                        {
                            Debugger.Break();
                            throw;
                        }
                    }
                }
            }
            return listElement;
        }

    }
}
