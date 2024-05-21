using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;
using System;
using System.Linq;
using System.Web.Mvc;
using SemesterProject.Extension;
using eUseControl.BussinessLogic.AppBL;

namespace SemesterProject.Controllers
{
     public class BaseController : Controller
     {
          private readonly ISession _session;

          public BaseController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }


          public void SessionStatus()
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    if (profile != null)
                    {
                         System.Web.HttpContext.Current.SetMySessionObject(profile);
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                    }
                    else
                    {
                         System.Web.HttpContext.Current.Session.Clear();
                         System.Web.HttpContext.Current.Session["LoginStatus"] = "guest";
                    }
               }
               else
               {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "guest";
               }
          }
     }
}