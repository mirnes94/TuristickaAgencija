using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class RegistracijaVM
    {
        [Required(ErrorMessage = "Obavezno**")]

        [StringLength(100, ErrorMessage = "Ime ne smije biti prazno.", MinimumLength = 1)]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Obavezno**")]

        [StringLength(100, ErrorMessage = "Prezime ne smije biti prazno.", MinimumLength = 1)]
        public string Prezime { get; set; }


        //[RegularExpression(@"[0-9]{2}[.]{1}[0-9]{2}[.]{1}[0-9]{4}", ErrorMessage = "Datum je u formatu: dd.mm.yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }

        //[RegularExpression(@"[0-9]{9}", ErrorMessage = "Kontakt mora sadrzavati 9 znamenki.")]
        [Required(ErrorMessage = "Obavezno**")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Neispravan format e-mail-a.")]
        public string Kontakt { get; set; }


        [RegularExpression(@"[0-9]{13}", ErrorMessage = "JMBG mora sadrzavati 13 znamenki.")]
        public string JMBG { get; set; }


        public List<SelectListItem> gradovi { get; set; }
        public int GradID { get; set; }






        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati mininalno 3 karaktera.", MinimumLength = 3)]
        public string korisnicko { get; set; }

        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati mininalno 4 karaktera.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string korisnikovalozinka { get; set; }

    }
}
