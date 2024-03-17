using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.AppBL
{
     public class UserContext : DbContext
     {
          public UserContext() :
            base("name=SemesterProject")
          {
          }

          public virtual DbSet<UDbTable> Users { get; set; }
     }
}
