using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Gradovi
    {
        [Key]
        public int Id { get; set; }
        public string NazivGrada { get; set; }

        [Required]
        [ForeignKey(nameof(Drzava))]
        public int? DrzavaId { get; set; }

        public Drzava Drzava { get; set; }
    }
}
