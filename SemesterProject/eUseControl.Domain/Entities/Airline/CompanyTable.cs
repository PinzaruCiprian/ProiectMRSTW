using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Airline
{
     public class CompanyTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int CompanyId { get; set; }

          [Required]
          [Display(Name = "Name")]
          public string Name { get; set; }

          [Display(Name = "Description")]
          public string Description { get; set; }

          [Required]
          [Display(Name = "Email")]
          public string Email { get; set; }

          [Display(Name = "Phone")]
          public string Phone { get; set; }

          [Display(Name = "Image")]
          public string Image { get; set; }
     }
}
