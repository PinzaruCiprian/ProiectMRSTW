using SemesterProject.Extension;
using System.Collections.Generic;
using System.Web.Mvc;
using SemesterProject.Model;

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
                return RedirectToAction("Login", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();
            UserData u = new UserData
            {
                Username = user.Username,
                Products = new List<string> { "Product #1", "Product #2", "Product #3", "Product #4" }
            };
            return View(u);
        }
    }
}