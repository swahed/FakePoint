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
    public class SPFieldCollection : IEnumerable<SPField>
    {
        XmlNodeList fields = null;
        XmlNode node = null;
        public int Count { get { return fields.Count; } }

        public SPField this[Guid id]
        {
            get { return new SPField(node.SelectSingleNode("Field[@ID='" + id + "']")); }
        }

        public SPField this[int index]
        {
            get { return new SPField(fields[index]); }
        }

        public SPField this[string name]
        {
            get { return new SPField(node.SelectSingleNode("Field[@Name='" + name + "']")); }
        }

        public SPFieldCollection(XmlNode node)
        {
            this.node = node;
            this.fields = node.SelectNodes("Field");
        }

        public bool ContainsField(string fieldName)
        {
            foreach (XmlNode field in fields)
                if (((XmlElement)field).GetAttribute("Name") == fieldName) return true;
            return false;
        }

        public string Add(string strDisplayName, SPFieldType type, bool bRequired)
        {
            return strDisplayName;
        }

        public SPField GetField(string strName)
        {
            return this[strName];
        }

        IEnumerator<SPField> IEnumerable<SPField>.GetEnumerator()
        {
            foreach (XmlNode field in fields)
                yield return new SPField(field);
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
