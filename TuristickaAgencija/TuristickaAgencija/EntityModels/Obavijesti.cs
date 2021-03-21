using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Obavijesti
    {
        [Key]
        public int ObavijestiId { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }

        [ForeignKey("KlijentId")]
        public Korisnik Klijent { get; set; }
        public int? KlijentId { get; set; }

        public DateTime Datum { get; set; }
        public byte[] Slika { get; set; }
    }
}
