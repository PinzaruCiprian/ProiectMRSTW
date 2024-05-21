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
               bundles.Add(new StyleBundle("~/assets/css").Include(
                       "~/assets/css/bootstrap.css",
                       "~/assets/css/icons.css",
                       "~/assets/css/main.css",
                       "~/assets/css/style.css",
                       "~/assets/css/app.css"));

               bundles.Add(new ScriptBundle("~/assets/js").Include(
                      "~/assets/js/vendor.js",
                      "~/assets/js/app.js",
                      "~/assets/js/main.js"));
          }
     }
}