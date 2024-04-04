using AutoMapper;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.BussinessLogic;
using eUseControl.Domain.Enums;
using SemesterProject.Extension;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SemesterProject.Atributes
{
     public class AdminModeAttribute : ActionFilterAttribute
     {
          private readonly ISession _sessionBusinessLogic;
          public AdminModeAttribute()
          {
               var businessLogic = new BussinessLogic();
               _sessionBusinessLogic = businessLogic.GetSessionBL();
          }

          public override void OnActionExecuting(ActionExecutingContext filterContext)
          {
               var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
               if (apiCookie != null)
               {
                    var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);
                    if (profile != null && profile.Level == URole.Admin)
                    {
                         HttpContext.Current.SetMySessionObject(profile);
                    }
                    else
                    {
                         filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                    }
               }
          }
     }
}