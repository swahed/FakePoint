using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Microsoft.SharePoint
{
    public class SPSecurity
    {
        public delegate void CodeToRunElevated();

        public static void RunWithElevatedPrivileges(CodeToRunElevated secureCode)
        {
        }
    }
}
