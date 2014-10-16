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
    public class SPFolderCollection : IEnumerable<SPFolder>
    {
        XmlNodeList folders = null;
        XmlNode node = null;
        public int Count { get { return folders.Count; } }

        public SPFolder this[int i]
        {
            get { return new SPFolder(folders[i]); }
        }

        public SPFolder this[string name]
        {
            get
            {
                return new SPFolder(node.SelectSingleNode("Folder[@Name='" + name + "']"));
            }
        }

        public SPFolderCollection(XmlNode node)
        {
            this.node = node;
            this.folders = node.SelectNodes("Folder");
        }

        IEnumerator<SPFolder> IEnumerable<SPFolder>.GetEnumerator()
        {
            foreach (XmlNode folder in folders)
                yield return new SPFolder(folder);
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
