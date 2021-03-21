using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Uplate
    {
        [Key]
        public int UplataId { get; set; }
        public float Iznos { get; set; }
        public DateTime DatumUplate { get; set; }
        [ForeignKey("KlijentId")]
        public Korisnik Klijent { get; set; }
        public int? KlijentId { get; set; }

        [ForeignKey("PutovanjaID")]
        public Putovanja Putovanja { get; set; }
        public int? PutovanjaID { get; set; }
        
    }
}
