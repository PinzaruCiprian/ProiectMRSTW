using AutoMapper;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using SemesterProject.Model;
using System.Web;
using System;
using System.Web.Mvc;
using eUseControl.BussinessLogic;

namespace SemesterProject.Controllers
{
    public class LoginController : Controller
    {
          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          public ActionResult Login()
          {
               return View();
          }

          public ActionResult ResetPassword()
          {
               return View();
          }


          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult LogIn(UserLogin login)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<ULoginData>(login);

                    data.LoginIp = Request.UserHostAddress;
                    data.LoginDateTime = DateTime.Now;

                    var userLogin = _session.UserLogin(data);
                    if (userLogin.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(login.Email);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

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

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult ResetPassword(UserResetPassword reset)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<ULoginData>(reset);

                    data.Email = "user@gmail.com"; //emailul din fereastr care introduce datele pentru reset
                    data.LoginIp = Request.UserHostAddress;
                    data.LoginDateTime = DateTime.Now;

                    var userLogin = _session.UserLogin(data);
                    if (userLogin.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(data.Email);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

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