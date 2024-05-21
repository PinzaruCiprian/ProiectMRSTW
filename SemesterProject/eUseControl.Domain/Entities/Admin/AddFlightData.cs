using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Domain.Entities.Admin
{
     public class AddFlightData
     {
          public string StartAdress { get; set; }

          public string EndAdress { get; set; }

          public string StartAirport { get; set; }

          public string EndAirport { get; set; }

          public string StartDate { get; set; }

          public string EndDate { get; set; }

          public string StartHour { get; set; }

          public string EndHour { get; set; }

          public string CompanyName { get; set; }

          public string Type { get; set; }

          public string Price { get; set; }
     }
}
