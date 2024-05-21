using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public class UserMinimal
     {
          public int Id { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Username { get; set; }
          public string Password { get; set; }
          public string Email { get; set; }
          public string Address { get; set; }
          public string Phone { get; set; }
          public string Image { get; set; }
          public DateTime BirthDate { get; set; }
          public DateTime LastLogin { get; set; }
          public string LasIp { get; set; }
          public URole Level { get; set; }
     }
}
