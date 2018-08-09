using System.Web;
using System.Web.Optimization;

namespace GigHub
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                            "~/Scripts/App/app.js",
                            "~/Scripts/App/Services/artistService.js",
                            "~/Scripts/App/Services/attendanceService.js",
                            "~/Scripts/App/Controllers/artistController.js",
                            "~/Scripts/App/Controllers/gigsController.js"
                ));
            
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/underscore-min.js",
                        "~/Scripts/moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/Site.css"));

            bundles.Add(new LessBundle("~/Content/less").Include("~/Content/*.less"));
        }
    }
}
