using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Firma
    {
        [Key]
        public int Id { get; set; }
      
        public string Naziv { get; set; }

        [ForeignKey("GradID")]
        public Gradovi Grad { get; set; }
        public int? GradID { get; set; }

        public string Adresa { get; set; }

        public string BrojZiroracuna { get; set; }
    }
}
