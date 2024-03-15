using eUseControl.BussinessLogic.Core;
using eUseControl.BussinessLogic.Interfaces;
using eUseControl.Domain.Entities.Responses;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic
{
    public class SessionBL : UserApi, ISession
    {
          public GeneralRes UserPassCheckAction(ULoginData data)
          {
               return UserAuthLogic(data);
          }
     }
}
