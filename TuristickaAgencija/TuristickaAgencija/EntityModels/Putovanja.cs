using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Putovanja
    {
        [Key]
        public int PutovanjaId { get; set; }
        public string NazivPutovanja { get; set; }
        public string OpisPutovanja { get; set; }
        public float CijenaPutovanja { get; set; }
        public DateTime DatumPolaska { get; set; }
        public DateTime DatumDolaska { get; set; }
        public int BrojPutnika { get; set; }
        public string PopisPutnika { get; set; }

        //[ForeignKey("KlijentId")]
        //public Korisnik Klijent { get; set; }
        //public int KlijentId { get; set; }
        ////greska??

        [ForeignKey("PrevozId")]
        public Prevoz Prevoz { get; set; }
        public int? PrevozId { get; set; }
        [ForeignKey("SmjestajId")]
        public Smjestaj Smjestaj { get; set; }
        public int? SmjestajId { get; set; }

    }
}
