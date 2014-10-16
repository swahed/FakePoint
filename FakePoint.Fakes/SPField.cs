﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPField
    {
        public XmlNode node = null;
        public bool Exists { get { return node != null; } }
        public DateTime TimeLastModified { get { return DateTime.Parse(((XmlElement)node).GetAttribute("TimeLastModified")); } }
        public Type FieldValueType
        {
            get { return System.Type.GetType("System." + ((XmlElement)node).GetAttribute("Type"), false, true); }
        }
        public SPFieldType Type
        {
            get { return (SPFieldType)Enum.Parse(typeof(SPFieldType), ((XmlElement)node).GetAttribute("Type")); }
        }

        public SPField(XmlNode node)
        {
            this.node = node;
        }
    }
}
