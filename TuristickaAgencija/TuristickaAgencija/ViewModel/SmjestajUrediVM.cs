using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class SmjestajUrediVM
    {
        public int SmjestajID { get; set; }
        [Required(ErrorMessage = "Unesite naziv smještaja.")]

        public string NazivSmjestaja { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti opis smještaja.")]
        public string OpisSmjestaja { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti grad.")]
        public int? GradID { get; set; }
        public List<SelectListItem> Grad { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti cijenu nočenja.")]
        public float CijenaNocenja { get; set; }
        [Required(ErrorMessage = "Potrebno je unijeti tip soba.")]
        public string Tip_sobe { get; set; }
        public IFormFile Slika { get; set; }

    }
}
