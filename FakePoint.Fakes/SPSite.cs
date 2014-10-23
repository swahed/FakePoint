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
    public class SPSite : IDisposable
    {
        private string _requestUrl;

        public XmlNode node = null;
        public string Url { get { return ((XmlElement)node).GetAttribute("Url"); } }
        public Guid ID { get { return Guid.Parse(((XmlElement)node).GetAttribute("ID")); } }
        public SPWeb RootWeb { get { return new SPWeb(node.SelectSingleNode("//Web")); } }
        public bool AllowUnsafeUpdates { get; set; }

        public SPSite(XmlNode node)
        {
            this.node = node;
        }

        public SPSite(string requestUrl)
        {
            _requestUrl = requestUrl;

            // TODO: Should remove trailing slashes
            // TODO: Filter on Url not working correctly
            // node = SPContext.content.SelectSingleNode("//Site[@Url=" + requestUrl + "]");
            // TODO: Also, correct site needs to be opened if the url of a subweb was entered

            var xml = SPContext.Current.Content;
            var sitenodes = xml.SelectNodes("//Site");
            
            XmlNode result = null;
            foreach(XmlNode sitenode in sitenodes)
            {
                var siteUrl = (sitenode.Attributes["Url"]).Value;
                if (requestUrl.StartsWith(siteUrl)) // Issue: can be called with Url from content
                        result = sitenode;          // Issue: it mus be checked if this is a better match, then the previous one
            }

            // Current Workaround
            node = SPContext.Current.Content.SelectSingleNode("//Site");
        }

        public SPSite(Guid guid)
        {
            // TODO: Filter on Id not working correctly
            //node = SPContext.content.SelectSingleNode("//Site[@ID=" + guid.ToString() + "]");
            node = SPContext.Current.Content.SelectSingleNode("//Site");
        }

        public SPWeb OpenWeb()
        {
            if(string.IsNullOrEmpty(_requestUrl))
                return new SPWeb(node.SelectSingleNode("//Web"));
            else
                return new SPWeb(node.SelectSingleNode("//Web[@Url='" + _requestUrl + "']"));
        }

        public SPWeb OpenWeb(string strUrl)
        {
            return new SPWeb(node.SelectSingleNode("//Web[@Url='" + strUrl + "']"));
        }

        public SPWeb OpenWeb(Guid guid)
        {
            return new SPWeb(node.SelectSingleNode("//Web[@ID='" + guid.ToString("B").ToUpper() + "']")); // TODO: Should be case insensitive comparison
        }

        public void Dispose()
        {
            // TODO: Ensure that using this once disposed throws error
        }
    }
}
