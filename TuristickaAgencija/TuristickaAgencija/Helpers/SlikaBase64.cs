using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Helpers
{
    public static class SlikaBase64
    {
        public static string Prikaz(byte[] slika)
        {
            if (slika != null)
            {
                return $"data:image/jpg;base64,{Convert.ToBase64String(slika)}";
            }
            else
            {
                return null;
            }
        
        }
    }
}
