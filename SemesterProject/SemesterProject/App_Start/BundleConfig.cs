using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SemesterProject.App_Start
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               bundles.Add(new StyleBundle("~/css").Include(
                       "~/css/bootstrap.min.css",
                       "~/css/bootstrap-theme.min.css",
                       "~/css/fontAwesome.css",
                       "~/css/light-box.css",
                       "~/css/templatemo-style.css"));

               bundles.Add(new ScriptBundle("~/js").Include(
                       "~/js/plugins.js",
                       "~/js/main.js"));

               bundles.Add(new ScriptBundle("~/js/vendor").Include(
                       "~/js/vendor/bootstrap.min.js",
                       "~/js/vendor/modernizr-2.8.3-respond-1.4.2.min.js"));

               bundles.Add(new ScriptBundle("~/js/login").Include(
                       "~/js/login/Login.js"));

               bundles.Add(new StyleBundle("~/css/login").Include(
                 "~/css/login/Login.css"));
          }
     }
}