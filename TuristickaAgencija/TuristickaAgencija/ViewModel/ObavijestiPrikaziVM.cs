using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class ObavijestiPrikaziVM
    {
        public int ObavijestID { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }

        public string Klijent { get; set; }
        public DateTime Datum { get; set; }
        
        public string SlikaString { get; set; }
        public byte[] Slika { get; set; }
    }
}
