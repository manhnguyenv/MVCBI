using System.Web.Optimization;

namespace VASJ.BI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Backend

            bundles.Add(new ScriptBundle("~/bundles/backend-js").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/bootstrap-tokenfield.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/url.js",
                        "~/Content/shared/js/jquery.*", //This entry will catch all new jquery plugins named jquery.xxxxx.js, where xxxxx stands for the name of the new plugin. e.g. jquery.slide.js
                        "~/Content/backend/js/VASJ.BI-*", //This entry will catch all new modules named VASJ.BI-xxxxx.js, where xxxxx stands for the name of the new module. e.g. VASJ.BI-news.js
                        "~/Content/shared/js/ays-beforeunload-shim.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/backend-css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/shared/css/font-awesome.css",
                        "~/Content/bootstrap-select.css",
                        "~/Content/bootstrap-tokenfield.css",
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/shared/css/jquery.*", //This entry will catch all new jquery plugins named jquery.xxxxx.css, where xxxxx stands for the name of the new plugin. e.g. jquery.slide.css
                        "~/Content/backend/css/VASJ.BI-*" //This entry will catch all new modules named VASJ.BI-xxxxx.css, where xxxxx stands for the name of the new module. e.g. VASJ.BI-news.css
                        ));

            //Frontend

            bundles.Add(new ScriptBundle("~/bundles/frontend-js").Include(
                        "~/Scripts/jquery.unobtrusive*",
                         "~/Scripts/jquery.validate*",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/moment-with-locales.js",
                         "~/Scripts/bootstrap-select.js",
                         "~/Scripts/bootstrap-tokenfield.js",
                         "~/Scripts/bootstrap-datetimepicker.js",
                         "~/Scripts/url.js",
                         "~/Content/shared/js/jquery.*", //This entry will catch all new jquery plugins named jquery.xxxxx.js, where xxxxx stands for the name of the new plugin. e.g. jquery.slide.js
                        "~/Content/frontend/js/VASJ.BI-*", //This entry will catch all new modules named VASJ.BI-xxxxx.js, where xxxxx stands for the name of the new module. e.g. VASJ.BI-news.js
                         "~/Content/shared/js/ays-beforeunload-shim.js"
                         ));

            bundles.Add(new StyleBundle("~/bundles/frontend-css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/shared/css/font-awesome.css",
                        "~/Content/bootstrap-select.css",
                        "~/Content/bootstrap-tokenfield.css",
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/frontend/css/jquery.*", //This entry will catch all new jquery plugins named jquery.xxxxx.css, where xxxxx stands for the name of the new plugin. e.g. jquery.slide.css
                        "~/Content/frontend/css/VASJ.BI-*" //This entry will catch all new modules named VASJ.BI-xxxxx.css, where xxxxx stands for the name of the new module. e.g. VASJ.BI-news.css
                        ));

            //Enables optimization only in Release mode
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = false;
#endif
        }
    }
}