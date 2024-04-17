using SemesterProject.Extension;
using System.Collections.Generic;
using System.Web.Mvc;
using SemesterProject.Model;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.User;
using System.Linq;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;

namespace SemesterProject.Controllers
{
    public class HomeController : BaseController
    {
          private readonly ISession _session;
          public HomeController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          public void GetCurrentUserAndStatus()
          {
               SessionStatus();
               var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               if (userStatus != "guest")
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    ViewBag.user = profile;
               }
               ViewBag.userStatus = userStatus;
          }
          public ActionResult Index()
          {
               GetCurrentUserAndStatus();
               return View();
          }

          public ActionResult SearchPage()
          {
               GetCurrentUserAndStatus();
               ViewBag.tickets = 8;
               return View();
          }

          public ActionResult UserProfile()
          {
               GetCurrentUserAndStatus();
               return View();
          }
     }
}