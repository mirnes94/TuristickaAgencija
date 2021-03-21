using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class TrenutnaSesijaIndexVM
    {
        public List<Row> Rows { get; set; }
        public string TrenutniToken { get; set; }

        public class Row
        {
            public DateTime VrijemeLogiranja { get; set; }
            public string token { get; set; }
            public string IpAdresa { get; set; }
        }
    }
}
