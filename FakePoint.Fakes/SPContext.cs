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
        public static SPContext Current = new SPContext();
        public SPWeb Web = null;
        public SPSite Site = null;
        public static XmlDocument content = new XmlDocument(); // TODO: Why is this public?  should this be tested?

        public static void Initialize(string name)
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase.Replace("file:///", "");
            if (path.Contains("TestResults")) path = path.Substring(0, path.IndexOf("TestResults"));
            path = Path.GetDirectoryName(path).Replace("\\bin\\Debug", "");
            var method = new StackTrace().GetFrame(1).GetMethod();
            string fileName = path + "\\" + name;
            var fs = new FileStream(fileName + ".manifest.xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(fs);
            if (sr.BaseStream.Length > 0)
            {
                content.Load(sr);
                Current.Web = new SPWeb(content.SelectSingleNode("//Web"));
                Current.Site = new SPSite(content.SelectSingleNode("//Site"));
            }
            sr.Close();
            fs.Close();
        }
    }
}
