using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }

        public string Ime { get;set; }

        [ForeignKey("KlijentID")]
        public Korisnik Klijent { get; set; }
        public int? KlijentID { get; set; }

        [ForeignKey("PutovanjeID")]
        public Putovanja Putovanje { get; set; }
        public int? PutovanjeID { get; set; }

        public DateTime DatumRezervacije { get; set; }

        public int BrojOsoba { get; set; }

        public string Status { get; set; }

        public string Napomena { get; set; }

    }
}
