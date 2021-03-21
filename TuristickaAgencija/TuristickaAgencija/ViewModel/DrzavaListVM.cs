using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.ViewModel
{
    public class DrzavaListVM
    {
        public GradoviVM Grad { get; set; }
        public List<Drzava> Drzave { get; set; }
    }
}
