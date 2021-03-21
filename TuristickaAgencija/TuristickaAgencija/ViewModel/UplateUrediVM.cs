using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class UplateUrediVM
    {
        public int UplataId { get; set; }

        [Required(ErrorMessage = "Obavezno**")]
        [Range(1, double.MaxValue, ErrorMessage = "Neispravan iznos!")]
        [RegularExpression("[0-9]+", ErrorMessage = "Iznos mora biti pozitivna brojna vrijednost!")]
        public float Iznos { get; set; }
        public bool Placeno { get; set; }

        [Required]
        public DateTime DatumUplate { get; set; }
        [Required(ErrorMessage = "Obavezno**")]
        public int? KlijentId { get; set; }
        public List<SelectListItem> klijenti { get; set; }
        public int? PutovanjaId { get; set; }
        public List<SelectListItem> putovanja { get; set; }
    }
}
