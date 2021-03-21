using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class PutovanjaDodajUrediVM
    {
        public int PutovanjaId { get; set; }
        [Required(ErrorMessage = "Unesite naziv putovanja")]
        public string NazivPutovanja { get; set; }
        [Required(ErrorMessage = "Unesite opis putovanja")]
        public string OpisPutovanja { get; set; }
        [Required(ErrorMessage = "Unesite cijenu putovanja")]
        public float CijenaPutovanja { get; set; }
        [Required(ErrorMessage = "Unesite datum polaska")]
        public DateTime DatumPolaska { get; set; }
        [Required(ErrorMessage = "Unesite naziv dolaska")]
        public DateTime DatumDolaska { get; set; }
        [Required(ErrorMessage = "Unesite broj putnika")]
        public int BrojPutnika { get; set; }
        [Required(ErrorMessage = "Unesite popis putnika")]
        public string PopisPutnika { get; set; }

        //public int KlijentId { get; set; }

        //public List<SelectListItem> Klijent { get; set; }
        [Required(ErrorMessage = "Odaberite prevoz putovanja")]
        public int? PrevozId { get; set; }

        public List<SelectListItem> Prevoz { get; set; }

        [Required(ErrorMessage = "Odaberite smjestaj")]
        public int? SmjestajId { get; set; }

        public List<SelectListItem> Smjestaj { get; set; }
    }
}
