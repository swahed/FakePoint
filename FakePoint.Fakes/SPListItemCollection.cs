using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPListItemCollection : IEnumerable<SPListItem>
    {
        public XmlNodeList items = null; // more efficient to hang on to this node list?
        public XmlNode node = null;
        public int Count { get { return items.Count; } }
        public SPListItem this[int i]
        {
            get 
            { 
                return new SPListItem(items[i]); 
            }
        }

        public SPListItemCollection(XmlNode node)
        {
            this.node = node;
            items = node == null ? // TODO: Workaround while xml does not contain items. To be removed
                null : node.SelectNodes("Row");
        }

        public SPListItem Add()
        {
            XmlNode item = SPContext.content.CreateElement("Row");
            node.AppendChild(item);
            items = node.SelectNodes("List");
            return new SPListItem(item);
        }

        public SPListItem GetItemById(int id)
        {
            XmlNode item = node.SelectSingleNode("Row[Field[@Name='ID' and text()='" + id + "']]");
            return new SPListItem(item);
        }

        public SPListItem GetItemByUniqueId(Guid uniqueId)
        {
            foreach (XmlNode item in node.SelectNodes("List"))
                if (uniqueId.ToString() == ((XmlElement)item).GetAttribute("ID")) return new SPListItem(item);
            return null;
        }

        IEnumerator<SPListItem> IEnumerable<SPListItem>.GetEnumerator()
        {
            foreach (XmlNode item in items)
                yield return new SPListItem(item);
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
