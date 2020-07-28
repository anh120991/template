using System.Web;
using System.Web.Optimization;

namespace HDBH.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/lib/bootstrap/css/bootstrap.css",
                      "~/Content/css/Site.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                     "~/Content/plugins/datatables/js/jquery.dataTables.min.js",
                     "~/Content/plugins/datatables/js/dataTables.bootstrap.min.js",
                     "~/Content/plugins/datatables/js/dataTables.responsive.min.js",
                     "~/Content/plugins/datatables/js/dataTables.fixedHeader.min.js",
                     "~/Content/plugins/datatables/js/dataTables.fixedColumns.min.js",
                     "~/Content/plugins/datatables/js/dataTables.reorderColumn.js",
                     "~/Content/plugins/datatables/js/dataTables.resizeColumn.js"
                     ));
            bundles.Add(new StyleBundle("~/style/datatable").Include(
                    "~/Content/datatables/plugins/css/jquery.dataTables.min.css"
                    ));
        }
    }
}
