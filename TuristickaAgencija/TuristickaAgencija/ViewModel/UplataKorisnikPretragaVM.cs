using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class UplataKorisnikPretragaVM
    {
        public int KlijentID { get; set; }
        public List<SelectListItem> Klijenti { get; set; }
        public int PutovanjeID { get; set; }

    }
}
