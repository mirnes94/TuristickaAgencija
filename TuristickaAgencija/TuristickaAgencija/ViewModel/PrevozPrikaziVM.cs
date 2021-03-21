using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class PrevozPrikaziVM
    {
        public int Id { get; set; }

        public string Firma { get; set; }

        public string TipPrevoza { get; set; }

        public int BrojMjesta { get; set; }

        public float CijenaPoMjestu { get; set; }

    
    }
}
