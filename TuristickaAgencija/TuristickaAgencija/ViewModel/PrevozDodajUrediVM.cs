using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class PrevozDodajUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Odaberite firmu koja ce biti zaduzena za prevoz")]
        public int? FirmaID { get; set; }

        public List<SelectListItem> Firma { get; set; }
        [Required(ErrorMessage = "Unesite tip prevoza")]
        public string TipPrevoza { get; set; }

        [Required(ErrorMessage = "Unesite broj mjesta")]
        public int BrojMjesta { get; set; }

        [Required(ErrorMessage = "Unesite cijenu po mjestu")]
        public float CijenaPoMjestu { get; set; }

 
    }
}
