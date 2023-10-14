using System.Web;
using System.Web.Optimization;

namespace FIT5032_MyProject
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js")); //datetime

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                       "~/Scripts/jquery.dataTables.min.js")); //datatable


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            // I added the location.js to the bundle called mapbox.
            bundles.Add(new ScriptBundle("~/bundles/mapbox").Include(
            "~/Scripts/location.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.dataTables.min.css")); //datatable

            bundles.Add(new StyleBundle("~/Content/jqueryuicss").Include(
                      "~/Content/themes/base/jquery-ui.min.css"));//datetime

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include( //calender js 
                        "~/Scripts/lib/jquery.min.js",
                        "~/Scripts/lib/moment.min.js",
                        "~/Scripts/fullcalendar.js",
                        "~/Scripts/calendar.js" //customized
                        ));

            bundles.Add(new StyleBundle("~/Content/fullcalendar").Include( //calender css 
                  "~/Content/fullcalendar.css"));


        }
    }
}
