using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eUseControl.Domain.Entities.User;
using SemesterProject.Model;

namespace SemesterProject.Controllers
{
    public class RegisterController : Controller
    {

          private readonly ISession _session;
          public RegisterController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }
          // GET: Register
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
     }
}