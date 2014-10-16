using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPListItem : SPItem
    {
        public string Title { get { return node.SelectSingleNode("Field[@Name='Title']").InnerText; } }
        public string Name { get { return ((XmlElement)node).GetAttribute("Name"); } }

        public SPListItem(XmlNode node)
            : base(node)
        {
            this.node = node;
        }

        // TODO: Properties need to be XML unsecaped
    }
}
