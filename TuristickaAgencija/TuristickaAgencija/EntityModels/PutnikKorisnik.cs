using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class PutnikKorisnik
    {
        [Key]
        public int PutnikKorisnikId { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
      

        [ForeignKey("KorisnickiNalog")]
        public int? KorisnickiNalogId { get; set; }
        public KorisnickiNalozi KorisnickiNalog { get; set; }
    }
}
