using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KlijentPrikaziVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string DatumRodjenja { get; set; }
        public string Kontakt { get; set; }

        public string JMBG { get; set; }

        public string Grad { get; set; }
    }
}
