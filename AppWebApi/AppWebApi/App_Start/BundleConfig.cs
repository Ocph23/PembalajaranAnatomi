using System.Web;
using System.Web.Optimization;

namespace AppWebApi
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
                      "~/js/bootstrap.min.js",
                      "~/Scripts/respond.js",
                     "~/Scripts/angular.js",
                     "~/Scripts/angular-route.js",
                      "~/ClientViews/app.js",
                       "~/ClientViews/controller.js",

                      "~/js/tether.min.js",
                      "~/js/jquery.cookie.js",
                      "~/js/grasp_mobile_progress_circle-1.0.0.min.js",
                      "~/js/jquery.nicescroll.min.js",
                      "~/js/jquery.validate.min.js",
                      "~/js/charts-home.js",
                       "~/js/front.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/css/bootstrap.min.css",
                       "~/css/grasp_mobile_progress_circle-1.0.0.min.css",
                        "~/css/custom.css",
                         "~/css/style.red.css",
                         "~/css/font-awesome.css"


                      ));
        }
    }
}
