using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class KorisnickiNalozi
    {
        [Key]
        public int KorisnickiNalogId { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }
    }
}
