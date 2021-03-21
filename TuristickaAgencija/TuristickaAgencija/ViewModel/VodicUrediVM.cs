using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class VodicUrediVM
    {
        public int VodicID { get; set; }

        [Required(ErrorMessage = "Unesite ime.")]
        public string Ime { get; set; }


        [Required(ErrorMessage = "Unesite prezime.")]
        public string Prezime { get; set; }


        [Required(ErrorMessage = "Unesite kontakt.")]
        public string Kontakt { get; set; }

        [Required(ErrorMessage = "Unesite JMBG.")]
        public string JMBG { get; set; }



        [Required(ErrorMessage = "Unesite putovanje za vodica")]
        public int? PutovanjeID { get; set; }
        public List<SelectListItem> Putovanje { get; set; }

        public IFormFile Slika { get; set; }
    }
}
