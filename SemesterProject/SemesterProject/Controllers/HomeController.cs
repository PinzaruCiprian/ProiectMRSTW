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


            return View();
        }
    }
}