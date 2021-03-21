using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Korisnik
    {

        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Kontakt { get; set; }

        public string JMBG { get; set; }

        [ForeignKey("GradID")]
        public Gradovi Grad { get; set; }
        public int? GradID { get; set; }
   
    }
}
