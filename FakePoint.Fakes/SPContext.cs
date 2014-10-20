using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPContext
    {
        private const string DefaultFileName = "FakePoint.Fakes";

        public SPWeb Web = null;
        public SPSite Site = null;

        internal XmlDocument Content = new XmlDocument();

        private static SPContext _current;
        public static SPContext Current
        {
            get 
            {
                if (_current == null)
                    SPContext.Initialize(DefaultFileName);

                return _current;
            }
            private set
            {
                _current = value;
            }
        }

        public static void Initialize(string name)
        {
            _current = new SPContext();
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase.Replace("file:///", "");
            if (path.Contains("TestResults")) path = path.Substring(0, path.IndexOf("TestResults"));
            path = Path.GetDirectoryName(path).Replace("\\bin\\Debug", "");
            var method = new StackTrace().GetFrame(1).GetMethod();
            string fileName = path + "\\" + name;
            var fs = new FileStream(fileName + ".manifest.xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(fs);
            if (sr.BaseStream.Length > 0)
            {
                _current.Content.Load(sr);
                Current.Web = new SPWeb(_current.Content.SelectSingleNode("//Web"));
                Current.Site = new SPSite(_current.Content.SelectSingleNode("//Site"));
            }
            sr.Close();
            fs.Close();
        }
    }
}
