using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.ViewModel
{
    public class PosaljiPorukuVM
    {
        [Required(ErrorMessage = "Unesite primaoca poruke.")]
        public string PrimalacPoruke { get; set; }
        [Required(ErrorMessage = "Unesite sadrzaj poruke.")]
        public string SadrzajPoruke { get; set; }
        [Required(ErrorMessage = "Unesite posiljaoca poruke.")]
        public string PosiljalacPoruke { get; set; }
    }
}
