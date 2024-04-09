using SemesterProject.Extension;
using System.Collections.Generic;
using System.Web.Mvc;
using SemesterProject.Model;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.User;
using System.Linq;

namespace SemesterProject.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            SessionStatus();
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               string email = (string)System.Web.HttpContext.Current.Session["Email"];
               UserTable user;
               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Email == email);
                    ViewBag.userStatus = userStatus;
                    ViewBag.user = user;
               }


               return View();
        }
    }
}