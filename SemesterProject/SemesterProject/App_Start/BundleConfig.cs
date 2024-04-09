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
                       "~/assets/css/bootstrap.min.css",
                       "~/assets/css/icons.min.css",
                       "~/assets/css/main.css",
                       "~/assets/css/app.min.css"));

               bundles.Add(new ScriptBundle("~/assets/js").Include(
                      "~/assets/js/vendor.min.js",
                      "~/assets/js/app.min.js",
                      "~/assets/js/main.js"));
          }
     }
}