using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class SmjestajPrikaziVM
    {
        public int SmjestajID { get; set; }
        public string NazivSmjestaja { get; set; }
        public string OpisSmjestaja { get; set; }
        public string Grad { get; set; }

        public float CijenaNocenja { get; set; }
        public string Tip_sobe { get; set; }
        public string SlikaString { get; set; }
        public byte[] Slika { get; set; }
    }
}
