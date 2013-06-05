using System.Web;
using System.Web.Optimization;

namespace TeachMeTeachYouSurvey
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/editor").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/themeEditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/account.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/html5reset-{version}.css",
                "~/Content/site.css"));
        }
    }
}