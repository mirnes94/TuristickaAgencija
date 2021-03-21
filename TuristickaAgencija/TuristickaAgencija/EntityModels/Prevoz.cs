using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class Prevoz
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("FirmaID")]
        public Firma Firma { get; set; }
        public int? FirmaID { get; set; }

        public string TipPrevoza { get; set; }

        public int BrojMjesta { get; set; }

        public float CijenaPoMjestu { get; set; }
    }
}
