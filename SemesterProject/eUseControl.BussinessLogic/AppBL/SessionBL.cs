using eUseControl.BussinessLogic.Core;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.Entities.Admin;
using eUseControl.Domain.Entities.Airline;
using eUseControl.Domain.Entities.User;
using System.Collections.Generic;
using System.Web;

namespace eUseControl.BussinessLogic
{
    public class SessionBL : UserApi, ISession
    {
          public BoolResp UserLogin(ULoginData data)
          {
               return UserLoginAction(data);
          }

          public BoolResp UserRegister(URegisterData data)
          {
               return UserRegisterAction(data);
          }

          public BoolResp CheckUser(UCheckData data)
          {
               return CheckUserAction(data);
          }

          public List<FlightTable> GetFlightList()
          {
               return GetFlightListAction();
          }

          public BoolResp EditUser(EditUserData data)
          {
               return EditUserAction(data);
          }

          public bool RecoverPassword(string email, string password)
          {
               return RecoverPasswordAction(email, password);
          }

          public void RemoveUnusedSessions(UserMinimal user)
          {
               RemoveUnusedSessionsAction(user);
          }

          public CompanyTable GetCompanyById(string name)
          {
               return GetCompanyByIdAction(name);
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
