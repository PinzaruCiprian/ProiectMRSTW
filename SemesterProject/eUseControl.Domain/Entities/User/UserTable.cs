using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public class UserTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Display(Name = "FirstName")]
          public string FirstName { get; set; }

          [Display(Name = "LastName")]
          public string LastName { get; set; }

          [Display(Name = "Username")]
          public string Username { get; set; }

          [Display(Name = "Password")]
          public string Password { get; set; }

          [Display(Name = "Email Address")]
          public string Email { get; set; }

          [Display(Name = "Address")]
          public string Address { get; set; }

          [Display(Name = "Phone")]
          public string Phone { get; set; }

          [Display(Name = "Image")]
          public string Image { get; set; }

          [Display(Name = "Birth Date")]
          public DateTime BirthDate { get; set; }

          [Display(Name = "Last Login")]
          public DateTime LastLogin { get; set; }

          [Display(Name = "Last Ip")]
          public string LastIp { get; set; }

          [Display(Name = "Level")]
          public URole Level { get; set; }
     }
}
