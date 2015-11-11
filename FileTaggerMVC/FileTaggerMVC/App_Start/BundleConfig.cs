using System.Web.Optimization;

namespace FileTaggerMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-1.11.3.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jstree.min.js",
                "~/Scripts/selectize.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/jstree/default/style.min.css",
                      "~/Content/jstree/default-dark/style.min.css",
                      "~/Content/selectize.bootstrap3.css"));
        }
    }
}
