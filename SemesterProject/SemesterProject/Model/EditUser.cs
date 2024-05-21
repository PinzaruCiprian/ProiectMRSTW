using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterProject.Model
{
     public class EditUser
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
}