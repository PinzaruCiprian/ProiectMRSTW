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
     public class LoginController : BaseController
     {
          private readonly ISession _session;
          public LoginController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }




          public ActionResult Login()
          {
               var apiCookie = Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    if (profile != null)
                    {
                         _session.RemoveUnusedSessions(profile);
                    }
               }
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




          public ActionResult Register()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Register(UserRegister register)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<URegisterData>(register);

                    data.LoginIp = Request.UserHostAddress;
                    data.LoginDateTime = DateTime.Now;

                    var userRegister = _session.UserRegister(data);
                    if (userRegister.Status)
                    {
                         HttpCookie cookie = _session.GenCookie(register.Email);
                         ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", userRegister.StatusMsg);
                         return View();
                    }
               }
               return View();
          }




          public ActionResult RecoverData()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult RecoverData(RecoverPasswordData data)
          {
               if (ModelState.IsValid)
               {
                    var user = Mapper.Map<UCheckData>(data);
                    var checkUser = _session.CheckUser(user);
                    if (checkUser.Status)
                    {
                         TempData["email"] = user.Email;
                         return RedirectToAction("Recover", "Login");
                    }
                    else
                    {
                         ModelState.AddModelError("", checkUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }




          public ActionResult Recover()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Recover(RecoverPasswordData data)
          {
               string email = (string)TempData["email"];
               if (ModelState.IsValid)
               {
                    var user = Mapper.Map<UCheckData>(data);
                    var changePassword = _session.RecoverPassword(email, user.Password);
                    if (changePassword)
                    {
                         return RedirectToAction("Login", "Login");
                    }
               }
               return View();
          }
     }
}