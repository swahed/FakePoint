using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPListCollection : IEnumerable<SPList>
    {
        XmlNodeList lists = null;
        XmlNode node = null;
        public int Count { get { return lists.Count; } }

        public SPList this[int i]
        {
            get { return new SPList(lists[i]); }
        }

        public SPList this[string name]
        {
            get
            {
                XmlNode list = node.SelectSingleNode("List[@Title='" + name + "']");
                if (list == null) throw new IndexOutOfRangeException();
                return new SPList(list);
            }
        }

        internal SPListCollection(XmlNode node)
        {
            this.node = node;
            this.lists = node.SelectNodes("List");
        }

        public SPList TryGetList(string listTitle)
        {
            XmlNode list = node.SelectSingleNode("List[@Title='" + listTitle + "']");
            if (list == null) return null;
            return new SPList(list);
        }

        IEnumerator<SPList> IEnumerable<SPList>.GetEnumerator()
        {
            foreach (XmlNode list in lists)
                yield return new SPList(list);
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
