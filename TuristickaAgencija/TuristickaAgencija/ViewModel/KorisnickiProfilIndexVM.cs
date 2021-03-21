using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KorisnickiProfilIndexVM
    {
        public int ProfilId { get; set; }
        public string KorisnickoIme { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Kontakt { get; set; }

        public string JMBG { get; set; }

    

        
    }
}
