using SemesterProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
               Items items = new Items();
               items.clearList();
               items.addItems();
               return View(Items.items);
        }
    }
}