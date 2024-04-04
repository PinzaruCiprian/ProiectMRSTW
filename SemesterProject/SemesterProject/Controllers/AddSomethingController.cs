using SemesterProject.Atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class AddSomethingController : BaseController
    {
        [AdminMode]
        public ActionResult AddSomething()
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