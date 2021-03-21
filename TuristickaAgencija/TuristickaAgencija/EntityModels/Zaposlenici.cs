using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Zaposlenici
    {
        [Key]
        public int ZaposlenikId { get; set; }

        public string StrucnaSprema { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }


        [ForeignKey("KorisnickiNalog")]
        public int? KorisnickiNalogId { get; set; }
        public KorisnickiNalozi KorisnickiNalog { get; set; }
    }
}
