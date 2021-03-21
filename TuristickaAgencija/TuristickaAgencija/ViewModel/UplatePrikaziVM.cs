using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class UplatePrikaziVM
    {
        public float UkupnoUplaceno { get; set; }
        public int BrojUplata { get; set; }
        public string SvrhaUplate { get; set; }
        public List<Row> lista { get; set; }
        public class Row
        {


            public int UplataId { get; set; }
            public float Iznos { get; set; }
            public DateTime DatumUplate { get; set; }

            public string Klijent { get; set; }
            public string Putovanja { get; set; }


            public string Svrha { get; set; }
        }
    }
}
