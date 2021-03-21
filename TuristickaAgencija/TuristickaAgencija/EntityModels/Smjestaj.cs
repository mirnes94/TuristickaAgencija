using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Smjestaj
    {
        [Key]
        public int Id { get; set; }


        public string NazivSmjestaja { get; set; }
        public string OpisSmjestaja { get; set; }
        [ForeignKey("GradID")]
        public Gradovi Grad { get; set; }
        public int? GradID { get; set; }
        public float CijenaNocenja { get; set; }

        public string Tip_sobe { get; set; }

        public byte[] Slika { get; set; }
    }
}
