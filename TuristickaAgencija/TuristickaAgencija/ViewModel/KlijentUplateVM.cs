using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KlijentUplateVM
    {

        public List<rows> listaUplata { get; set; }
        public double Ukupno { get; set; }
        public class rows
        {
            public int KlijentId { get; set; }

            public float IznosUplate { get; set; }
            public string DatumUplate { get; set; }

            public string Putovanje { get; set; }
        }
       
    }
}
