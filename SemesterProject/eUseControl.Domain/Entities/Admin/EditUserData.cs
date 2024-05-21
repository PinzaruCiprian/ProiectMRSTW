using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Admin
{
     public class EditUserData
     {
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Username { get; set; }
          public string Email { get; set; }
          public string Password { get; set; }
          public string Address { get; set; }
          public string Phone { get; set; }
          public DateTime BirthDate { get; set; }
          public URole Level { get; set; }
          
}
