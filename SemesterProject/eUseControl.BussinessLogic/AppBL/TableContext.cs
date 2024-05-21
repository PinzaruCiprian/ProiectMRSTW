using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BussinessLogic.AppBL
{
     public class TableContext : DbContext
     {
          public TableContext() : base("name=SemesterProject")
          {
          }

          public virtual DbSet<UserTable> Users { get; set; }
          public virtual DbSet<Session> Sessions { get; set; }
          public virtual DbSet<CompanyTable> Company { get; set; }
          public virtual DbSet<FlightTable> Flight { get; set; }
          public virtual DbSet<PurchaseTable> Purchase { get; set; }
     }
}
