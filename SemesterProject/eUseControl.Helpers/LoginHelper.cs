using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.Helpers
{
     public class LoginHelper
     {
          public static string HashGen(string password)
          {
               MD5 md5 = new MD5CryptoServiceProvider();
               var originalBytes = Encoding.Default.GetBytes(password + "twutm2018");
               var encodedBytes = md5.ComputeHash(originalBytes);

               return BitConverter.ToString(encodedBytes).Replace("-", "").ToLower();
          }
     }
}
