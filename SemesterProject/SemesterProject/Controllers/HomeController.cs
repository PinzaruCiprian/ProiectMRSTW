using SemesterProject.Extension;
using SemesterProject.Model;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SemesterProject.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            //SessionStatus();
            return View();
        }
     }
}