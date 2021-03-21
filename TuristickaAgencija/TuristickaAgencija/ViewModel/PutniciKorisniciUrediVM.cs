using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class PutniciKorisniciUrediVM
    {
        public int PutnikKorisnikID { get; set; }
        public int KorisnikID { get; set; }
        public List<SelectListItem> korisnici { get; set; }
    }
}
