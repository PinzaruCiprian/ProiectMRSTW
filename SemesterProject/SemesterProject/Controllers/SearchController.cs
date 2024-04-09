using eUseControl.BussinessLogic.AppBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult SearchPage()
        {
               using (var db = new UserContext())
               {
                    ViewBag.tickets = 8;
               }
                    return View();
        }
    }
}