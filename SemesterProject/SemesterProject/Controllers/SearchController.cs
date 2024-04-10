using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult SearchPage()
        {
               SessionStatus();
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               string email = (string)System.Web.HttpContext.Current.Session["Email"];
               UserTable user;
               using (var db = new UserContext())
               {
                    user = db.Users.FirstOrDefault(u => u.Email == email);
                    ViewBag.userStatus = userStatus;
                    ViewBag.tickets = 8;
                    ViewBag.user = user;
               }
               return View();
        }
     }
}