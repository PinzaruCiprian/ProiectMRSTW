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
          private readonly ISession _auth;

          public LoginController()
          {
               var bl = new BussinessLogic();
               _auth = bl.GetSessionBL();
          }

          public ActionResult Index()
          {
               return View();
          }

          public void UserLoginSimulation()
          {
               var username = "User";
               var password = "Password";
               var uLoginData = new UserLogin
               {
                    Credential = username,
                    Password = password
               };

               Login(uLoginData);
          }


          public void Login(UserLogin uLogin)
          {
               var loginData = new ULoginData
               {
                    Credential = uLogin.Credential,
                    Password = uLogin.Password,
                    LoginDate = DateTime.Now,
                    UserIp = "0.0.0.0"
               };

               GeneralRes res = _auth.UserPassCheckAction(loginData);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Index(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    ULoginData data = new ULoginData
                    {

                         Credential = login.Credential,
                         Password = login.Password,
                         UserIp = Request.UserHostAddress,
                         LoginDate = DateTime.Now
                    };
                    var userLogin = _auth.UserLogin(data);
                    if (userLogin.Status)
                    {
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", userLogin.StatusMsg);
                         return View();
                    }
               }
               return View();
          }
     }
}