using AutoMapper;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace eUseControl.BussinessLogic.Core
{
     public class UserApi
     {
          internal BoolResp UserLoginAction(ULoginData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    var pass = LoginHelper.HashGen(data.Password);
                    using (var db = new TableContext())
                    {
                         UserTable result = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Password == pass);
                         if (result == null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "Incorrect username or password." };
                         }
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         db.Entry(result).State = EntityState.Modified;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp UserRegisterAction(URegisterData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         UserTable existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                         if (existingUser != null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "Email already registered." };
                         }
                         var newUser = new UserTable
                         {
                              FirstName = "None",
                              LastName = "None",
                              Username = data.Username,
                              Password = LoginHelper.HashGen(data.Password),
                              Email = data.Email,
                              Address = "None",
                              Phone = "None",
                              Image = "default.png",
                              BirthDate = DateTime.Now.Date,
                              LastLogin = data.LoginDateTime,
                              LastIp = data.LoginIp,
                              Level = (URole)0,
                         };
                         db.Users.Add(newUser);
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp CheckUserAction(UCheckData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         UserTable existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Username == data.Username);
                         if (existingUser == null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "User does not exists." };
                         }
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal List<FlightTable> GetFlightListAction()
          {
               List<FlightTable> allFlights;
               using (var db = new TableContext())
               {
                    allFlights = db.Flight.ToList();
               }
               return allFlights;
          }

          internal bool RecoverPasswordAction(string email, string password)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(email))
               {
                    using (var db = new TableContext())
                    {
                         UserTable user = db.Users.FirstOrDefault(u => u.Email == email);
                         var pass = LoginHelper.HashGen(password);
                         user.Password = pass;
                         db.SaveChanges();
                    }
                    return true;
               }
               else
                    return false;
          }

          internal CompanyTable GetCompanyByIdAction(string name)
          {
               using (var db = new TableContext())
               {
                    CompanyTable company = db.Company.FirstOrDefault(f => f.Name == name);
                    return company;
               }
          }

          internal void RemoveUnusedSessionsAction(UserMinimal user)
          {
               using (var db = new TableContext())
               {
                    var expiredSessions = db.Sessions.Where(s => s.ExpireTime < DateTime.Now).ToList();

                    var sessionToDelete = db.Sessions.FirstOrDefault(s => s.Username == user.Email);
                    db.Sessions.Remove(sessionToDelete);
                    if (expiredSessions.Any())
                    {
                         foreach (var session in expiredSessions)
                         {
                              db.Sessions.Remove(session);
                         }
                    }
                    db.SaveChanges();
               }
          }

          internal BoolResp EditUserAction(EditUserData data)
          {
               UserTable existingUser;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                         existingUser.FirstName = data.FirstName;
                         existingUser.LastName = data.LastName;
                         existingUser.Email = data.Email;
                         existingUser.Address = data.Address;
                         existingUser.Phone = data.Phone;
                         existingUser.BirthDate = data.BirthDate.Date;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal HttpCookie Cookie(string loginCredential)
          {
               int sessionTime = 60;
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(loginCredential)
               };

               using (var db = new TableContext())
               {
                    Session curent;
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(loginCredential))
                    {
                         curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                    }
                    else
                    {
                         curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                    }

                    if (curent != null)
                    {
                         curent.CookieString = apiCookie.Value;
                         curent.ExpireTime = DateTime.Now.AddMinutes(sessionTime);
                         using (var todo = new TableContext())
                         {
                              todo.Entry(curent).State = EntityState.Modified;
                              todo.SaveChanges();
                         }
                    }
                    else
                    {
                         db.Sessions.Add(new Session
                         {
                              Username = loginCredential,
                              CookieString = apiCookie.Value,
                              ExpireTime = DateTime.Now.AddMinutes(sessionTime)
                         });
                         db.SaveChanges();
                    }
               }
               return apiCookie;
          }

          internal UserMinimal UserCookie(string cookie)
          {
               Session session;
               UserTable curentUser;

               using (var db = new TableContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new TableContext())
               {
                    var validate = new EmailAddressAttribute();
                    if (validate.IsValid(session.Username))
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                    }
                    else
                    {
                         curentUser = db.Users.FirstOrDefault(u => u.Username == session.Username);
                    }
               }

               if (curentUser == null) return null;
               var userminimal = Mapper.Map<UserMinimal>(curentUser);

               return userminimal;
          }
     }
}
