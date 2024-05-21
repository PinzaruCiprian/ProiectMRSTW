using eUseControl.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.AppBL
{
     public class AdminBL
     {
          public BoolResp AddUser(AddUserData data)
          {
               return AddUserAction(data);
          }

          public BoolResp AddCompany(AddCompanyData data)
          {
               return AddCompanyAction(data);
          }

          public BoolResp AddFlight(AddFlightData data)
          {
               return AddFlightAction(data);
          }

          public BoolResp EditUser(EditUserData data)
          {
               return EditUserAction(data);
          }

          public BoolResp EditCompany(EditCompanyData data)
          {
               return EditCompanyAction(data);
          }

          public BoolResp EditFlight(EditFlightData data)
          {
               return EditFlightAction(data);
          }

          public void DeleteUser(int id)
          {
               DeleteUserAction(id);
          }

          public void DeleteCompany(int id)
          {
               DeleteCompanyAction(id);
          }

          public void DeleteFlight(int id)
          {
               DeleteFlightAction(id);
          }
     }
}
