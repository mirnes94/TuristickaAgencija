using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class FirmaUrediDodajVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unesite naziv firme.")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Odaberite grad u kome se nalazi firma")]
        public int? GradID { get; set; }
        public List<SelectListItem> listaGradova { get; set;}

        [Required(ErrorMessage = "Odaberite adresu firme")]
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Odaberite broj žiroračuna firma")]
        public string BrojZiroracuna { get; set; }
    }
}
