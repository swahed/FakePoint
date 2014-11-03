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

        internal XmlNode node = null;
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
            _requestUrl = requestUrl.TrimEnd('/');

            if (string.IsNullOrEmpty(_requestUrl))
                throw new ArgumentException("Request Url must not be empty");

            // TODO: This will not work if there is a rootweb in the site collection (Possibly make it a hard coded exception)
            node = SPContext.Current.Content.SelectSingleNode("//Site[starts-with('" + _requestUrl + "', @Url)]");

            if (node == null)
                throw new ArgumentException("No Website found for request Url " + requestUrl);
        }

        public SPSite(Guid guid)
        {
            // TODO: Filter on Id not working correctly
            node = SPContext.Current.Content.SelectSingleNode("//Site[@ID='" + guid.ToString("B").ToUpper() + "']");
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
