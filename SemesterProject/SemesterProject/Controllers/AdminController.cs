using AutoMapper;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using eUseControl.Domain.Entities.User;
using SemesterProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.InteropServices;

namespace SemesterProject.Controllers
{
    public class AdminController : Controller
    {
          // GET: Admin
          private readonly IAdmin _admin;
          private readonly ISession _session;

          public AdminController()
          {
               var bl = new BussinessLogic();
               _admin = bl.GetAdminBL();
               _session = bl.GetSessionBL();
          }

          public void GetCurrentUserAndStatus()
          {
               SessionStatus();
               var apiCookie = System.Web.HttpContext.Current.Request.Cookies["X-KEY"];
               string userStatus = (string)System.Web.HttpContext.Current.Session["LoginStatus"];
               if (userStatus != "guest")
               {
                    var profile = _session.GetUserByCookie(apiCookie.Value);
                    ViewBag.user = profile;
               }
               ViewBag.userStatus = userStatus;
          }




          public ActionResult AddUser()
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    List<UserTable> usersList = db.Users.OrderByDescending(u => u.Level).ToList();
                    ViewBag.usersList = usersList;
               }
               return View();
          }

          public ActionResult AddCompany()
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    List<CompanyTable> companiesList = db.Company.ToList();
                    ViewBag.companiesList = companiesList;
               }
               return View();
          }

          public ActionResult AddFlight()
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    List<FlightTable> FlightsList = db.Flight.ToList();
                    List<string> CompaniesList = db.Company.OrderBy(c => c.Name).Select(c => c.Name).ToList();
                    List<string> TypeList = new List<string> { "Summer", "Winter", "Cultural", "Wellness" };
                    ViewBag.FlightsList = FlightsList;
                    ViewBag.CompaniesList = CompaniesList;
                    ViewBag.TypeList = TypeList;
               }
               return View();
          }




          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddUser(AddUser user)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<AddUserData>(user);

                    var addUser = _admin.AddUser(data);
                    if (addUser.Status)
                    {
                         return RedirectToAction("AddUser", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", addUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddCompany(AddCompany company, HttpPostedFileBase imageFile)
          {
               if (ModelState.IsValid)
               {
                    if (imageFile != null && imageFile.ContentType == "image/png")
                    {
                         using (var db = new TableContext())
                         {
                              var path = Path.Combine(Server.MapPath($"~/assets/images/companies/{company.Name}.png"));
                              imageFile.SaveAs(path);
                         }
                    }

                    var data = Mapper.Map<AddCompanyData>(company);

                    var addCompany = _admin.AddCompany(data);
                    if (addCompany.Status)
                    {
                         return RedirectToAction("AddCompany", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", addCompany.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult AddFlight(AddFlight Flight)
          {
               if (ModelState.IsValid)
               {
                    using (var db = new TableContext())
                    {
                         List<FlightTable> FlightsList = db.Flight.ToList();
                         List<string> CompaniesList = db.Company.OrderBy(c => c.Name).Select(c => c.Name).ToList();
                         List<string> TypeList = new List<string> { "Summer", "Winter", "Cultural", "Wellness" };
                         ViewBag.FlightsList = FlightsList;
                         ViewBag.CompaniesList = CompaniesList;
                         ViewBag.TypeList = TypeList;
                    }
                    var data = Mapper.Map<AddFlightData>(Flight);

                    var addFlight = _admin.AddFlight(data);
                    if (addFlight.Status)
                    {
                         return RedirectToAction("AddFlight", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", addFlight.StatusMsg);
                         return View();
                    }
               }
               return View();
          }
          public ActionResult DeleteUser(int id)
          {
               using (var db = new TableContext())
               {
                    UserTable user = db.Users.FirstOrDefault(u => u.Id == id);
                    if (user.Image != "default.png")
                    {
                         var path = Path.Combine(Server.MapPath($"~/assets/images/users/{user.Image}"));
                         System.IO.File.Delete(path);
                    }
               }
               _admin.DeleteUser(id);
               return RedirectToAction("AddUser", "Admin");
          }

          public ActionResult DeleteCompany(int id)
          {
               using (var db = new TableContext())
               {
                    CompanyTable company = db.Company.FirstOrDefault(u => u.CompanyId == id);
                    var path = Path.Combine(Server.MapPath($"~/assets/images/companies/{company.Image}"));
                    System.IO.File.Delete(path);
               }
               _admin.DeleteCompany(id);
               return RedirectToAction("AddCompany", "Admin");
          }

          public ActionResult DeleteFlight(int id)
          {
               _admin.DeleteFlight(id);
               return RedirectToAction("AddFlight", "Admin");
          }



     }
}