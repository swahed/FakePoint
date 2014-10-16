using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
  public class SPList
  {
    public XmlNode node = null;
    public SPListItemCollection Items = null;
    public DateTime LastItemModifiedDate { get { return DateTime.Parse(((XmlElement)node).GetAttribute("LastItemModifiedDate")); } }
    public SPFieldCollection Fields = null;

    public SPList(XmlNode node)
    {
      this.node = node;
      Items = new SPListItemCollection(node.SelectSingleNode("*/Rows"));
      Fields = new SPFieldCollection(node.SelectSingleNode("*/Fields"));
    }

    public int ItemCount
    {
      get { return Items.Count; }
    }

    public SPListItemCollection GetItems(SPQuery query)
    {
      return Items;
    }

    public SPListItem GetItemById(int id)
    {
      foreach (XmlNode item in node.SelectNodes("Data/Rows/Row"))
        if (id == int.Parse(item.SelectSingleNode("Field[@Name='ID']").InnerText)) return new SPListItem(item);
      return null;
    }

    public SPListItem GetItemByUniqueId(Guid uniqueId)
    {
      foreach (XmlNode item in node.SelectNodes("Data/Rows/Row"))
        if (uniqueId.ToString() == ((XmlElement)item).GetAttribute("ID")) return new SPListItem(item);
      return null;
    }

    public void Update()
    {
    }

    public void Delete()
    {
    }

  }
}
