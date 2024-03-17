using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.AppBL
{
     public class SessionContext : DbContext
     {
          public SessionContext() : base("name=CCToolShop")
          {
          }
          public virtual DbSet<Session> Sessions { get; set; }
     }
}
