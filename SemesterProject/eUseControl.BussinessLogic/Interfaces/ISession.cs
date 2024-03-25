using eUseControl.Domain.Entities.User;
using System.Web;

namespace eUseControl.BussinessLogic.Interfaces
{
     public interface ISession
     {
          ULoginResp UserLogin(ULoginData data);
          URegisterResp UserRegister(URegisterData data);
          HttpCookie GenCookie(string loginCredential);
          UserMinimal GetUserByCookie(string apiCookieValue);
     }
}
