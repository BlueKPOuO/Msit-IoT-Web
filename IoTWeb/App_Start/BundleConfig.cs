using System.Web;
using System.Web.Optimization;

namespace IoTWeb
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/AdminStyle/vendor/jquery/jquery.min.js",
                        "~/AdminStyle/vendor/bootstrap/js/bootstrap.bundle.min.js",
                        "~/AdminStyle/vendor/jquery-easing/jquery.easing.min.js",
                        "~/AdminStyle/vendor/chart.js/Chart.min.js",
                        "~/AdminStyle/vendor/datatables/jquery.dataTables.js",
                        "~/AdminStyle/vendor/datatables/dataTables.bootstrap4.js",
                        "~/AdminStyle/js/sb-admin.min.js", 
                        //"~/AdminStyle/js/sb-admin-charts.min.js",
                        "~/AdminStyle/js/sb-admin-datatables.min.js"
                        ));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/admin").Include(
                    "~/AdminStyle/vendor/bootstrap/css/bootstrap.min.css",
                    "~/AdminStyle/vendor/datatables/dataTables.bootstrap4.css",
                    "~/AdminStyle/css/sb-admin.css"));
        }
    }
}
