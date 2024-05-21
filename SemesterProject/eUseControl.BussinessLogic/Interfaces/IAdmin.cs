using eUseControl.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.Interfaces
{
     public interface IAdmin
     {
          BoolResp AddUser(AddUserData data);
          BoolResp AddCompany(AddCompanyData data);
          BoolResp AddFlight(AddFlightData data);
          BoolResp EditUser(EditUserData data);
          BoolResp EditCompany(EditCompanyData data);
          BoolResp EditFlight(EditFlightData data);
          void DeleteUser(int id);
          void DeleteCompany(int id);
          void DeleteFlight(int id);
     }
}
