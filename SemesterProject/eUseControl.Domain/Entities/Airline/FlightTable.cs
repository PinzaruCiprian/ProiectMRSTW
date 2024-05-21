using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Airline
{
     public class FlightTable
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Display(Name = "Start Address")]
          public string StartAdress { get; set; }

          [Display(Name = "Start Airport")]
          public string StartAirport { get; set; }

          [Display(Name = "End Address")]
          public string EndAdress { get; set; }

          [Display(Name = "End Airport")]
          public string EndAirport { get; set; }

          [Display(Name = "Start Date")]
          public DateTime StartDate { get; set; }

          [Display(Name = "End Date")]
          public DateTime EndDate { get; set; }

          [Display(Name = "Start Hour")]
          public DateTime StartHour { get; set; }

          [Display(Name = "End Hour")]
          public DateTime EndHour { get; set; }

          [Display(Name = "Company Name")]
          public string CompanyName { get; set; }

          [Display(Name = "Flight Type")]
          public string Type { get; set; }

          [Display(Name = "Price")]
          public decimal Price { get; set; }
     }
}
