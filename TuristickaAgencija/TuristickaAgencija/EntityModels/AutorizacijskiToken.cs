using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.EntityModels
{
    public class AutorizacijskiToken
    {
        public int Id { get; set; }
        public string Vrijednost { get; set; }
        [ForeignKey(nameof(KorisnickiNalozi))]
        public int KorisnickiNaloziId { get; set; }
        public KorisnickiNalozi KorisnickiNalozi { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
        public string IpAdresa { get; set; }
    }
}
