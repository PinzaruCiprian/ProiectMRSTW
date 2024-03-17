using SemesterProject.Extension;
using SemesterProject.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
               SessionStatus();
               if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
               {
                    return RedirectToAction("Index", "Login");
               }

               var user = System.Web.HttpContext.Current.GetMySessionObject();
               UserData u = new UserData
               {
                    Username = user.Username,
                    Products = new List<string> { "Product #1", "Product #2", "Product #3", "Product #4" }
               };
               return View();
        }
          public ActionResult Product()
          {
               var product = Request.QueryString["p"];

               UserData u = new UserData();
               u.Username = "Customer";
               u.SingleProduct = product;

               return View(u);
          }

          [HttpPost]
          public ActionResult Product(string btn)
          {
               return RedirectToAction("Product", "Home", new { @p = btn });
          }
     }
}