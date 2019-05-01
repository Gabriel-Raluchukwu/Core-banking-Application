using System.Web;
using System.Web.Optimization;

namespace BankTwo.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/layout-theme.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryTable").Include(
                        "~/Scripts/DataTables/jquery.dataTables.js",
                         "~/Scripts/SearchTableFunction.js",
                        "~/Scripts/DataTables/dataTables.bootstrap4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/layout_theme").Include(
                      "~/Content/layout-template.css"));

            bundles.Add(new StyleBundle("~/Content/tables_theme").Include(
                   "~/Content/DataTables/css/jquery.dataTables.css",
                    "~/Content/DataTables/css/jquery.dataTables_themeroller.css"));
        }
    }
}
