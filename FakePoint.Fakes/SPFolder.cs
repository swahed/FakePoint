using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPFolder
    {
        public XmlNode node = null;
        public bool Exists { get { return node != null; } }
        public DateTime TimeLastModified { get { return DateTime.Parse(((XmlElement)node).GetAttribute("TimeLastModified")); } }
        public string Name { get { return ((XmlElement)node).GetAttribute("Name"); } }
        public SPFileCollection Files { get { return new SPFileCollection(node); } }
        public SPFolderCollection SubFolders { get { return new SPFolderCollection(node); } }

        public SPFolder(XmlNode node)
        {
            this.node = node;
        }
    }
}
