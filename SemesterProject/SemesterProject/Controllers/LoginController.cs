using eUseControl.BussinessLogic;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.WebControls;
using SemesterProject.Model;
using eUseControl.Domain.Entities.Responses;

namespace SemesterProject.Controllers
{
    public class LoginController : Controller
    {
          public ActionResult Login()
          {
               return View();
          }
     }
}