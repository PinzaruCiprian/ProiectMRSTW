﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterProject.Model
{
     public class AddFlight
     {
          public string StartAdress { get; set; }
          public string EndAdress { get; set; }
          public string StartAirport { get; set; }
          public string EndAirport { get; set; }
          public DateTime StartDate { get; set; }
          public DateTime EndDate { get; set; }
          public DateTime StartHour { get; set; }
          public DateTime EndHour { get; set; }
          public string CompanyName { get; set; }
          public string Type { get; set; }
          public decimal Price { get; set; }
     }
}