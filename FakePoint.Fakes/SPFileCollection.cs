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
    public class SPFileCollection : IEnumerable<SPFile>
    {
        XmlNodeList files = null;
        XmlNode node = null;
        public int Count { get { return files.Count; } }

        public SPFile this[int i]
        {
            get { return new SPFile(files[i]); }
        }

        public SPFile this[string name]
        {
            get { return new SPFile(node.SelectSingleNode("File[@Title='" + name + "']")); }
        }

        public SPFileCollection(XmlNode node)
        {
            this.node = node;
            this.files = node.SelectNodes("File");
        }

        public SPFile Add(string urlOfFile, Stream file)
        {
            return new SPFile(null); // just enough here to compile :(
        }

        public SPFile Add(string urlOfFile, Stream file, bool overwrite)
        {
            return new SPFile(null); // just enough here to compile :(
        }

        public SPFile Add(string urlOfFile, byte[] file)
        {
            return new SPFile(null); // just enough here to compile :(
        }

        public SPFile Add(string urlOfFile, byte[] file, bool overwrite)
        {
            return new SPFile(null); // just enough here to compile :(
        }

        IEnumerator<SPFile> IEnumerable<SPFile>.GetEnumerator()
        {
            foreach (XmlNode list in files)
                yield return new SPFile(list);
        }

        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
