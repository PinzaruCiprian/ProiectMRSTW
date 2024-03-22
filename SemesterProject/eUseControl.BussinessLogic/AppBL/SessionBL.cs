using eUseControl.BussinessLogic.Core;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using System.Web;

namespace eUseControl.BussinessLogic
{
    public class SessionBL : UserApi, ISession
    {
          public ULoginResp UserLogin(ULoginData data)
          {
               return UserLoginAction(data);
          }

          public HttpCookie GenCookie(string loginCredential)
          {
               return Cookie(loginCredential);
          }
          public UserMinimal GetUserByCookie(string apiCookieValue)
          {
               return UserCookie(apiCookieValue);
          }
     }
}
