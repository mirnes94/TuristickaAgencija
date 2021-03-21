using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class ObavijestiDodajVM
    {
        public int ObavijestID { get; set; }
        [Required(ErrorMessage = "Unesite naziv obavijesti!")]

        public string Naziv { get; set; }
        [Required(ErrorMessage = "Unesite sadrzaj obavijesti!")]
        public string Sadrzaj { get; set; }

        [Required(ErrorMessage = "Odaberite korisnika kome šaljete obavijest!")]
        public int? KlijentId { get; set; }

        public List<SelectListItem> klijenti { get; set; }

        [Required(ErrorMessage = "Unesite datum!")]
        public DateTime Datum { get; set; }
        public IFormFile Slika { get; set; }

    }
}
