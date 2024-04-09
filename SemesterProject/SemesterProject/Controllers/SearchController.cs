using eUseControl.BussinessLogic.AppBL;
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
               ViewBag.tickets = 8;
               return View();
        }
     }
}