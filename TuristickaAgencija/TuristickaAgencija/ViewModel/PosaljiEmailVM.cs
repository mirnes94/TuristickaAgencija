using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.ViewModel
{
    public class PosaljiEmailVM
    {
        public string To { get; set; }
        [Required(ErrorMessage = "Unesite posiljaoca emaila.")]
        public string From { get; set; }
        [Required(ErrorMessage = "Unesite predmet emaila.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Unesite sadržaj emaila.")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Unesite lozinku Vašeg emaila.")]
        public string Password { get; set; }

    }
}
