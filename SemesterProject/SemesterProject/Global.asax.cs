using AutoMapper;
using eUseControl.Domain.Entities.User;
using SemesterProject.App_Start;
using SemesterProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SemesterProject
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);
           BundleConfig.RegisterBundles(BundleTable.Bundles);
           InitializeAutoMapper();
        }

          protected static void InitializeAutoMapper()
          {
               Mapper.Initialize(cfg =>
               {
                    cfg.CreateMap<UserLogin, ULoginData>();
                    cfg.CreateMap<UserRegister, URegisterData>();
                    cfg.CreateMap<UserTable, UserMinimal>();
                    cfg.CreateMap<UserResetPassword, ULoginData>();
               });
          }
     }
}