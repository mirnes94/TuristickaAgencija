using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class RezervacijaDodajVM
    {
        public int RezervacijaId { get; set; }

        public int KorisnickiNalogId { get; set; }

        public string Ime { get; set; }
        public int KlijentId { get; set; }
        public int PutovanjeId { get; set; }

        public DateTime Datum { get; set; }

        public int BrojOsoba { get; set; }
        [Required(ErrorMessage = "Unesite status.")]
        [RegularExpression("^(Potvrđeno|Odbijeno|potvrđeno|odbijeno|POTVRĐENO|ODBIJENO)$",ErrorMessage = "Status rezervacije može biti potvrđeno ili odbijeno.")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Unesite napomenu.")]
        public string Napomena { get; set; }
    }
}
