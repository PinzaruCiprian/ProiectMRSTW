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
    public class AdminController : BaseController
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

          public ActionResult EditUser(int id)
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    var user = db.Users.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditUser>(user);

                    ViewBag.userToEdit = data;
                    return View(data);
               }
          }

          public ActionResult EditCompany(int id)
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    var company = db.Company.FirstOrDefault(u => u.CompanyId == id);
                    var data = Mapper.Map<EditCompany>(company);

                    ViewBag.companyToEdit = data;
                    return View(data);
               }
          }

          public ActionResult EditFlight(int id)
          {
               GetCurrentUserAndStatus();
               using (var db = new TableContext())
               {
                    var Flight = db.Flight.FirstOrDefault(u => u.Id == id);
                    var data = Mapper.Map<EditFlight>(Flight);
                    List<string> TypeList = new List<string> { "Summer", "Winter", "Cultural", "Wellness" };
                    ViewBag.TypeList = TypeList;
                    ViewBag.FlightToEdit = data;
                    return View(data);
               }
          }




          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditUser(EditUser user)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<EditUserData>(user);

                    var editUser = _admin.EditUser(data);
                    if (editUser.Status)
                    {
                         return RedirectToAction("AddUser", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editUser.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditCompany(EditCompany company, HttpPostedFileBase imageFile)
          {
               if (ModelState.IsValid)
               {
                    if (imageFile != null && imageFile.ContentType == "image/png")
                    {
                         using (var db = new TableContext())
                         {
                              CompanyTable existingCompany = db.Company.FirstOrDefault(u => u.Email == company.Email);
                              var path = Path.Combine(Server.MapPath($"~/assets/images/companies/{existingCompany.Name}.png"));
                              existingCompany.Image = existingCompany.Name + ".png";
                              db.SaveChanges();
                              System.IO.File.Delete(path);
                              imageFile.SaveAs(path);
                         }
                    }

                    var data = Mapper.Map<EditCompanyData>(company);

                    var editCompany = _admin.EditCompany(data);
                    if (editCompany.Status)
                    {
                         return RedirectToAction("AddCompany", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editCompany.StatusMsg);
                         return View();
                    }
               }
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult EditFlight(EditFlight Flight)
          {
               if (ModelState.IsValid)
               {
                    var data = Mapper.Map<EditFlightData>(Flight);

                    var editFlight = _admin.EditFlight(data);
                    if (editFlight.Status)
                    {
                         return RedirectToAction("AddFlight", "Admin");
                    }
                    else
                    {
                         ModelState.AddModelError("", editFlight.StatusMsg);
                         return View();
                    }
               }
               return View();
          }


     }
}