using eUseControl.BussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic
{
     public class BussinessLogic
     {
          public ISession GetSessionBL()
          {
               return new SessionBL();
          }
     }
}
