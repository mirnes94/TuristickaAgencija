using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KlijentRezervacijaDodajUrediVM
    {
        public int RezervacijaId { get; set; }

        public int KorisnickiNalogId { get; set; }

        [Required(ErrorMessage = "Unesite ime na koje glasi rezervacija.")]
        public string Ime { get; set; }
        public int KlijentId { get; set; }
        public string ImePrezimeKlijenta { get; set; }
        public int PutovanjeId { get; set; }

        public string NazivPutovanja { get; set; }

        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Unesite broj osoba.")]
        [RegularExpression("[0-9]+", ErrorMessage = "Iznos mora biti pozitivna brojna vrijednost!")]
        public int BrojOsoba { get; set; }
      
        public string Status { get; set; }

        public string Napomena { get; set; }
        


    }
}
