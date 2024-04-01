using AutoMapper;
using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eUseControl.BussinessLogic.Core
{
     public class UserApi
     {
          internal ULoginResp UserLoginAction(ULoginData data)
          {
               UserTable result;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    var pass = LoginHelper.HashGen(data.Password);
                    using (var db = new UserContext())
                    {
                         result = db.Users.FirstOrDefault(u => u.Email == data.Email && u.Password == pass);
                    }

                    if (result == null)
                    {
                         return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                    }

                    using (var todo = new UserContext())
                    {
                         result.LastIp = data.LoginIp;
                         result.LastLogin = data.LoginDateTime;
                         todo.SaveChanges();
                    }
                    return new ULoginResp { Status = true };
               }
               else
                    return new ULoginResp { Status = false };
          }

          internal URegisterResp UserRegisterAction(URegisterData data)
          {
               UserTable existingUser;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new UserContext())
                    {
                         existingUser = db.Users.FirstOrDefault(u => u.Email == data.Email);
                    }
                   
                    if (existingUser != null)
                    {
                         return new URegisterResp { Status = false, StatusMsg = "User With Email Already Exists" };
                    }

                    var pass = LoginHelper.HashGen(data.Password);
                    var newUser = new UserTable
                    {
                         Email = data.Email,
                         Username = data.Username,
                         Password = pass,
                         LastIp = data.LoginIp,
                         LastLogin = data.LoginDateTime,
                         Level = (URole)0,
                    };

                    using (var todo = new UserContext())
                    {
                         todo.Users.Add(newUser);
                         todo.SaveChanges();
                    }
                    return new URegisterResp { Status = true };
               }
               else
                    return new URegisterResp { Status = false };
          }

          internal HttpCookie Cookie(string loginCredential)
          {
               var apiCookie = new HttpCookie("X-KEY")
               {
                    Value = CookieGenerator.Create(loginCredential)
               };

               using (var db = new SessionContext())
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
                         curent.ExpireTime = DateTime.Now.AddMinutes(60);
                         using (var todo = new SessionContext())
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
                              ExpireTime = DateTime.Now.AddMinutes(60)
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

               using (var db = new SessionContext())
               {
                    session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
               }

               if (session == null) return null;
               using (var db = new UserContext())
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
               Mapper.Initialize(cfg => cfg.CreateMap<UserTable, UserMinimal>());
               var userminimal = Mapper.Map<UserMinimal>(curentUser);

               return userminimal;
          }
     }
}
