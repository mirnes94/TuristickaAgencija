using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class ZaposlenikUrediVM
    {
        public int ZaposlenikId { get; set; }
        public string StrucnaSprema { get; set; }
        public int KorisnikId { get; set; }
        public List<SelectListItem> korisnici { get; set; }
    }
}
