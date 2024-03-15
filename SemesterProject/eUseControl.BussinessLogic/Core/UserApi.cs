using eUseControl.Domain.Entities.Responses;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.Core
{
     public class UserApi
     {
          public GeneralRes UserAuthLogic(ULoginData data)
          {
               return new GeneralRes { Status = false };
          }
     }
}
