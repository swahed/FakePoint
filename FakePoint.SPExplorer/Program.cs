using System.Linq;
using FakePoint.Explorer;
using System;
using Microsoft.SharePoint.WebControls;


namespace FakePoint.SPExplorer
{
    class Program
    {
        private const string DefaultFileName = "spexplorer_export";

        static void Main(string[] args)
        {
            string filepath = args.Length > 0 &&  !string.IsNullOrEmpty(args[0]) ?
                args[0] : DefaultFileName;

            string siteId = args.Length > 1 && !string.IsNullOrEmpty(args[1]) ?
                args[0] : Guid.Empty.ToString();

            siteId = "bc0d7fea-75ba-4015-8b88-a7331af06418";

            try
            {
                var document = new SPExplorerDocument();;
                //document.FilterSiteId = new Guid(siteId) // TODO: Implement
                //document.IncludeContent = true; // TODO: Test
                document.ReadFromLocalFarm();
                document.SaveToFile(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}
