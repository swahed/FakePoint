using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPWeb : IDisposable
    {
        internal XmlNode node = null;
        public SPListCollection Lists = null;
        public SPFileCollection Files = null;
        public Guid ID { get { return Guid.Parse(((XmlElement)node).GetAttribute("ID")); } }
        public string Url { get { return ((XmlElement)node).GetAttribute("Url"); } }
        public bool AllowUnsafeUpdates { get; set; }
        public SPFolderCollection Folders
        {
            get
            {
                XmlNode xFolders = node.SelectSingleNode("Files/Folder");
                return new SPFolderCollection(xFolders);
            }
        }

        public SPWeb(XmlNode node)
        {
            this.node = node;
            if (node != null)
            {
                XmlNode xUserLists = node.SelectSingleNode("UserLists");
                if (xUserLists != null) 
                    Lists = new SPListCollection(xUserLists);
                XmlNode xFiles = node.SelectSingleNode("Files");
                if (xFiles != null) 
                    Files = new SPFileCollection(xFiles);
            }
            AllowUnsafeUpdates = true;
        }

        public SPFile GetFile(string strUrl)
        {
            return new SPFile(node.SelectSingleNode("/File[@Url=" + strUrl + "]"));
        }

        public SPFile GetFile(Guid uniqueId)
        {
            return new SPFile(node.SelectSingleNode("/File[@ID=" + uniqueId + "]"));
        }

        public void Dispose()
        {
            if (this == SPContext.Current.Web)
                throw new Exception("Shouldn't dispose this object - it is managed by the SharePoint framework");
        }
    }
}
