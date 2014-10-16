using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPItem
    {
        public XmlNode node = null;

        public int ID
        {
            get { return int.Parse(node.SelectSingleNode("Field[@Name='ID']").InnerText); }
        }

        public object this[string fieldName]
        {
            get
            {
                XmlNode field = node.SelectSingleNode("Field[@Name='" + fieldName + "']");
                if (field == null) return null;
                XmlNode type = node.SelectSingleNode("../../../MetaData/Fields/Field[@Name='" + fieldName + "']/@Type");
                return FieldToObject(field.InnerText, type);
            }
            set { }
        }

        public object this[int index]
        {
            get { return node.SelectSingleNode("Field[index='" + index + "']").InnerText; }
            set { }
        }

        public object this[Guid fieldId]
        {
            get { return node.SelectSingleNode("Field[@Guid='" + fieldId + "']").InnerText; }
            set { }
        }

        public SPFieldCollection Fields
        {
            get { return new SPFieldCollection(node.SelectSingleNode("../../../MetaData/Fields")); }
        }

        public SPItem(XmlNode node)
        {
            this.node = node;
        }

        public void Update()
        {
        }

        public void Delete()
        {
        }

        private object FieldToObject(string value, XmlNode type)
        {
            if (type != null)
                switch (type.Value)
                {
                    case "Attachments": return bool.Parse(value);
                    case "Boolean": return bool.Parse(value);
                    case "Counter": return int.Parse(value);
                    case "CrossProjectLink": return bool.Parse(value);
                    case "Currency": return double.Parse(value);
                    case "DateTime": return DateTime.Parse(value);
                    case "Integer": return int.Parse(value);
                    case "MaxItems": return int.Parse(value);
                    case "ModStat": return int.Parse(value);
                    case "Number": return double.Parse(value);
                    case "Recurrence": return bool.Parse(value);
                    default: break;
                }
            return value;
        }
    }
}
