using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.ViewModel
{
    [Table("Obavijesti")]
    public class ObavijestKlijentVM
    {
        [Key]
        public int ObavijestiId { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }

        [DisplayName("imekorisnika")]
        public string Klijent { get; set; }
        public int KlijentId { get; set; }

        public DateTime Datum { get; set; }
        public byte[] Slika { get; set; }
    }
}
