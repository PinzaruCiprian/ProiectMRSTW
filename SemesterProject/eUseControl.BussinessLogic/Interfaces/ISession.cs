using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using eUseControl.Domain.Entities.User;
using System.Collections.Generic;
using System.Web;

namespace eUseControl.BussinessLogic.Interfaces
{
     public interface ISession
     {
          BoolResp UserLogin(ULoginData data);
          BoolResp UserRegister(URegisterData data);
          BoolResp CheckUser(UCheckData data);
          List<FlightTable> GetFlightList();
          bool RecoverPassword(string email, string password);
          void RemoveUnusedSessions(UserMinimal user);
          CompanyTable GetCompanyById(string name);
          BoolResp EditUser(EditUserData data);
          HttpCookie GenCookie(string loginCredential);
          UserMinimal GetUserByCookie(string apiCookieValue);
     }
}
