using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Vodic
    {
        [Key]
        public int VodicId { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string JMBG { get; set; }
       
        
        [ForeignKey("PutovanjaID")]
        public Putovanja Putovanja { get; set; }
        public int? PutovanjaID { get; set; }
     
        public byte[] Slika { get; set; }
    }
}
