using eUseControl.Domain.Entities.Responses;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.Interfaces
{
     public interface ISession
     {
          GeneralRes UserPassCheckAction(ULoginData data);
     }
}
