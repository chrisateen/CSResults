using System.Web;
using System.Web.Optimization;

namespace CSResults
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/argon").Include(
                       "~/Scripts/argon-dashboard.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
           "~/Scripts/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.min.css",
                      "~/Content/argon-dashboard.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/Site.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                     "~/Scripts/chart.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapTable").Include(
               "~/Content/bootstrap-table.min.css",
               "~/Content/bootstrap-table-fixed-columns.css"));

        }
    }
}
