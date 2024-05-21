using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.User
{
     public class PurchaseTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int PurchaseId { get; set; }

          [Display(Name = "Flight Id")]
          public string FlightId { get; set; }
          [Display(Name = "User Id")]
          public string UserId { get; set; }
          [Display(Name = "Quantity")]
          public string Quantity { get; set; }
     }
}
