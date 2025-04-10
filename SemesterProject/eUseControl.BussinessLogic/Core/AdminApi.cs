﻿using eUseControl.BussinessLogic.AppBL;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using eUseControl.Domain.Entities.User;
using eUseControl.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eUseControl.BussinessLogic.Core
{
     public class AdminApi
     {
          internal BoolResp AddUserAction(AddUserData data)
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
                              FirstName = data.FirstName,
                              LastName = data.LastName,
                              Username = data.Username,
                              Password = LoginHelper.HashGen(data.Password),
                              Email = data.Email,
                              Address = data.Address,
                              Phone = data.Phone,
                              BirthDate = data.BirthDate.Date,
                              Level = data.Level,
                              Image = "default.png",
                              LastLogin = DateTime.Now,
                              LastIp = "None",
                         };
                         db.Users.Add(newUser);
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp AddCompanyAction(AddCompanyData data)
          {
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         CompanyTable existingCompany = db.Company.FirstOrDefault(u => u.Email == data.Email);
                         if (existingCompany != null)
                         {
                              return new BoolResp { Status = false, StatusMsg = "Email already registered." };
                         }

                         var newCompany = new CompanyTable
                         {
                              Name = data.Name,
                              Description = data.Description,
                              Email = data.Email,
                              Image = data.Name + ".png",
                              Phone = data.Phone,
                         };
                         db.Company.Add(newCompany);
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp AddFlightAction(AddFlightData data)
          {
               using (var db = new TableContext())
               {
                    FlightTable existingFlight = db.Flight.FirstOrDefault(u => u.StartAdress == data.StartAdress && u.EndAdress == data.EndAdress && u.CompanyName == data.CompanyName);
                    if (existingFlight != null)
                    {
                         return new BoolResp { Status = false, StatusMsg = "Flight already registered." };
                    }

                    var newFlight = new FlightTable
                    {
                         StartAdress = data.StartAdress,
                         EndAdress = data.EndAdress,
                         StartDate = data.StartDate,
                         StartHour = data.StartHour,
                         StartAirport = data.StartAirport,
                         EndAirport = data.EndAirport,
                         EndDate = data.EndDate,
                         EndHour = data.EndHour,
                         CompanyName = data.CompanyName,
                         Type = data.Type,
                         Price = data.Price,
                    };
                    db.Flight.Add(newFlight);
                    db.SaveChanges();
               }
               return new BoolResp { Status = true };


          }
          internal void DeleteUserAction(int id)
          {
               using (var db = new TableContext())
               {
                    UserTable user = db.Users.FirstOrDefault(u => u.Id == id);
                    db.Users.Remove(user);
                    db.SaveChanges();
               }
          }

          internal void DeleteCompanyAction(int id)
          {
               using (var db = new TableContext())
               {
                    CompanyTable company = db.Company.FirstOrDefault(u => u.CompanyId == id);
                    db.Company.Remove(company);
                    db.SaveChanges();
               }
          }
          internal BoolResp EditFlightAction(EditFlightData data)
          {
               using (var db = new TableContext())
               {
                    FlightTable existingFlight = db.Flight.FirstOrDefault(u => u.StartAdress == data.StartAdress && u.EndAdress == data.EndAdress && u.CompanyName == data.CompanyName);

                    existingFlight.StartAdress = data.StartAdress;
                    existingFlight.EndAdress = data.EndAdress;
                    existingFlight.StartDate = data.StartDate;
                    existingFlight.EndDate = data.EndDate;
                    existingFlight.StartHour = data.StartHour;
                    existingFlight.EndHour = data.EndHour;
                    existingFlight.CompanyName = data.CompanyName;
                    existingFlight.Type = data.Type;
                    existingFlight.Price = data.Price;
                    db.SaveChanges();
               }
               return new BoolResp { Status = true };
          }
          internal void DeleteFlightAction(int id)
          {
               using (var db = new TableContext())
               {
                    FlightTable flight = db.Flight.FirstOrDefault(u => u.Id == id);
                    db.Flight.Remove(flight);
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
                         existingUser.Username = data.Username;
                         existingUser.Email = data.Email;
                         existingUser.Address = data.Address;
                         existingUser.Phone = data.Phone;
                         existingUser.BirthDate = data.BirthDate.Date;
                         existingUser.Level = data.Level;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }

          internal BoolResp EditCompanyAction(EditCompanyData data)
          {
               CompanyTable existingCompany;
               var validate = new EmailAddressAttribute();
               if (validate.IsValid(data.Email))
               {
                    using (var db = new TableContext())
                    {
                         existingCompany = db.Company.FirstOrDefault(u => u.Email == data.Email);
                         existingCompany.Name = data.Name;
                         existingCompany.Description = data.Description;
                         existingCompany.Email = data.Email;
                         existingCompany.Phone = data.Phone;
                         db.SaveChanges();
                    }
                    return new BoolResp { Status = true };
               }
               else
                    return new BoolResp { Status = false };
          }
     }
}
