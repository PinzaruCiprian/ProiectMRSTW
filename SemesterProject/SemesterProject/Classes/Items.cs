using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SemesterProject.Classes
{
     public class Items
     {
          public string Title { get; set; }  
          public string Country { get; set; }  
          public string Description { get; set; }  
          public string Path_Wide { get; set; }  
          public string Path_Small { get; set; }

          public static List<Items> items = new List<Items>();

          public void addItems()
          {
               items.Add(new Items { Title = "Paris", Country = "France", Description = "Known for the Eiffel Tower, the Louvre, and Montmartre, Paris is often called the 'city of love' and is a top destination for those interested in art, history, and cuisine.", Path_Wide = "img/Locations/Paris_Wide.jpg", Path_Small = "img/Locations/Paris_Small.jpg" });
               items.Add(new Items { Title = "Bali", Country = "Indonesia", Description = "A popular destination for those seeking picturesque beaches, vibrant culture, and wellness and yoga retreats.", Path_Wide = "img/Locations/Bali_Wide.jpg", Path_Small = "img/Locations/Bali_Small.jpg" });
               items.Add(new Items { Title = "Barcelona", Country = "Spain", Description = "Famous for Gaudí's architecture, urban beaches, and lively nightlife.", Path_Wide = "img/Locations/Barcelona_Wide.jpg", Path_Small = "img/Locations/Barcelona_Small.jpg" });
               items.Add(new Items { Title = "Iceland", Country = "Iceland", Description = "Known for its dramatic natural landscapes, including volcanoes, geysers, thermal baths, and the northern lights.", Path_Wide = "img/Locations/Iceland_Wide.jpg", Path_Small = "img/Locations/Iceland_Small.jpg" });
               items.Add(new Items { Title = "Kyoto", Country = "Japan", Description = "Famous for its temples, zen gardens, and cherry blossom festival, Kyoto offers a window to traditional Japan.", Path_Wide = "img/Locations/Kyoto_Wide.jpg", Path_Small = "img/Locations/Kyoto_Small.jpg" });
               items.Add(new Items { Title = "Machu Picchu", Country = "Peru", Description = "The ancient Incan ruins located on the Andes mountain ridges are a marvel for history and nature explorers.", Path_Wide = "img/Locations/MachuPicchu_Wide.jpg", Path_Small = "img/Locations/MachuPicchu_Small.jpg" });
               items.Add(new Items { Title = "New York", Country = "USA", Description = "The \"city that never sleeps\" is famous for Broadway, Times Square, and a multitude of world-class museums.", Path_Wide = "img/Locations/NewYork_Wide.jpg", Path_Small = "img/Locations/NewYork_Small.jpg" });
               items.Add(new Items { Title = "Rome", Country = "Italy", Description = "With its ancient ruins, the Vatican, and an unmatched culinary scene, Rome is a must-see for history enthusiasts and foodies.", Path_Wide = "img/Locations/Rome_Wide.jpg", Path_Small = "img/Locations/Rome_Small.jpg" });
               items.Add(new Items { Title = "Sydney", Country = "Australia", Description = "Known for the Opera House and Harbour Bridge, Sydney offers beautiful beaches and a vibrant city life.", Path_Wide = "img/Locations/Sydney_Wide.jpg", Path_Small = "img/Locations/Sydney_Small.jpg" });
          }
          public void clearList() => items.Clear();
     }
}