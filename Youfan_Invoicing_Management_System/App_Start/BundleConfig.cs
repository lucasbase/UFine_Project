using System.Web;
using System.Web.Optimization;

namespace Youfan_Invoicing_Management_System
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/step/layui/layui.js",
                        "~/step/step-lay/step.js",
                        "~/Content/assets/dtree/dtree.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/layui").Include(
                        "~/Content/src2.5.6/layui.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/src2.5.6/css/layui.css", 
                      "~/Content/assets/css/modules/layui-icon-extend/layui-icon-extend/iconfont.css",
                      "~/step/layui/css/layui.css",
                      "~/step/step-lay/step.css,",
                      "~/Content/assets/dtree/font/dtreefont.css",
                      "~/Content/assets/dtree/dtree.css",
                      "~/Content/assets/font/iconfont.css"
                      ));

            //// 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            //// 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/assets/css/layui.css"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
