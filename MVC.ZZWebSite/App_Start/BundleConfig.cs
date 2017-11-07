using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace MVC.ZZWebSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/script/admin").Include("~/Scripts/jquery-1.7.1.min.js"));
            bundles.Add(new ScriptBundle("~/script/ajaxupload").Include("~/Scripts/ajaxupload.3.6.js"));
            bundles.Add(new StyleBundle("~/themes/admin/css").Include("~/themes/admin/site.css"));

            //前台展示
            bundles.Add(new StyleBundle("~/themes/default/css").Include("~/themes/default/site.css"));
            bundles.Add(new ScriptBundle("~/themes/default/script")
                .Include("~/Scripts/jquery-1.7.1.min.js")
                .Include("~/themes/default/script.js"));
            bundles.Add(new ScriptBundle("~/themes/default/ie6")
                .Include("~/themes/default/DD_belatedPNG_0.0.8a.js")
                .Include("~/themes/default/script_ie6.js"));
        }
    }
}