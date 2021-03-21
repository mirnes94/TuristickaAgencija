using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KorisnickiProfilUrediVM
    {
        public int ProfilId { get; set; }
        [Required(ErrorMessage = "Unesite ime!")]
        [RegularExpression("[A-Z]{1}[a-zA-Z]+", ErrorMessage = "npr. John")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Unesite prezime!")]
        [RegularExpression("[A-Z]{1}[a-zA-Z]+", ErrorMessage = "npr. Doe")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Unesite datum rodjenja!")]
        public DateTime DatumRodjenja { get; set; }

        public string Kontakt { get; set; }

        [Required(ErrorMessage = "Unesite JMBG!")]
        [RegularExpression("[0-9]{13}", ErrorMessage = "JMBG sadrzi 13 karaktera.")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Unesite korisničko ime.")]
        public string KorisnickoIme { get; set; }
        [Required(ErrorMessage = "Unesite lozinku.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Odaberite grad.")]
        public int GradId { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
      
       
    }
}
