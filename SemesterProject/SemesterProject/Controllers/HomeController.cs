using SemesterProject.Extension;
using System.Collections.Generic;
using System.Web.Mvc;
using SemesterProject.Model;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.User;
using System.Linq;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;
using AutoMapper;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using System.IO;
using System.Web;
using System;

namespace SemesterProject.Controllers
{
    public class HomeController : BaseController
    {
          private readonly ISession _session;
          public HomeController()
          {
               var bl = new BussinessLogic();
               _session = bl.GetSessionBL();
          }

          public void GetCurrentUserAndStatus()
          {
               SessionStatus();
               ViewBag.itemsInCart = 0;
               var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               if (userStatus != "guest")
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    ViewBag.user = profile;
                    ViewBag.itemsInCart = 6;
               }
               ViewBag.userStatus = userStatus;
          }

          public ActionResult Index()
          {
               GetCurrentUserAndStatus();
               var cheapFlightList = _session.GetFlightList().OrderBy(f => f.Price).Take(3);
               var expensiveFlightList = _session.GetFlightList().OrderBy(f => f.Price).Skip(Math.Max(0, _session.GetFlightList().Count - 3)).Reverse().ToList();
               using (var db = new TableContext())
               {
                    var randomCompaniesList = db.Company.OrderBy(c => Guid.NewGuid()).Take(6).ToList();
                    ViewBag.randomCompaniesList = randomCompaniesList;
               }
               ViewBag.cheapFlightList = cheapFlightList;
               ViewBag.expensiveFlightList = expensiveFlightList;
               return View(cheapFlightList);
          }

          public ActionResult SearchPage(string type)
          {
               GetCurrentUserAndStatus();
               List<FlightTable> flightsList = _session.GetFlightList();
               if (type == "Summer")
                    flightsList = _session.GetFlightList().Where(f => f.Type == "Summer").ToList();
               else if (type == "Winter")
                    flightsList = _session.GetFlightList().Where(f => f.Type == "Winter").ToList();
               else if (type == "Cultural")
                    flightsList = _session.GetFlightList().Where(f => f.Type == "Cultural").ToList();
               else if (type == "Wellness")
                    flightsList = _session.GetFlightList().Where(f => f.Type == "Wellness").ToList();
               ViewBag.flightList = flightsList;
               return View();
          }

          public ActionResult UserProfile(int id)
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditUser>(user);
                    return View(data);
               }
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult UserProfile(EditUser user, HttpPostedFileBase imageFile)
          {
               if (ModelState.IsValid)
               {
                    if (imageFile != null && imageFile.ContentType == "image/png")
                    {
                         using (var db = new TableContext())
                         {
                              UserTable existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                              var path = Path.Combine(Server.MapPath($"~/assets/images/users/{existingUser.Username}.png"));
                              existingUser.Image = existingUser.Username + ".png";
                              db.SaveChanges();
                              System.IO.File.Delete(path);
                              imageFile.SaveAs(path);
                         }
                    }

                    var data = Mapper.Map<EditUserData>(user);

                    var editUser = _session.EditUser(data);
                    if (editUser.Status)
                    {
                         return RedirectToAction("UserProfile", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError("", editUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          public ActionResult FlightData(string name)
          {
               GetCurrentUserAndStatus();
               var company = _session.GetCompanyById(name);
               var flightsList = _session.GetFlightList().Where(f => f.CompanyName == name).ToList();

               ViewBag.company = company;
               ViewBag.FlightsList = flightsList;
               return View();
          }
     }
}