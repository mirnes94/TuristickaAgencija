using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class VodicPrikaziVM
    {
        public int VodicId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string JMBG { get; set; }
        public string Putovanje { get; set; }
        public byte[] Slika { get; set; }
    }
}
