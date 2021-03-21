using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KlijentUrediDodajVM
    {
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }
        public string Kontakt { get; set; }

        public string JMBG { get; set; }

        public int? GradId { get; set; }

        public List<SelectListItem> Grad { get; set; }
    }
}
