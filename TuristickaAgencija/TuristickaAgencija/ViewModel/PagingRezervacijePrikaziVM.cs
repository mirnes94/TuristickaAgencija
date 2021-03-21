using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.ViewModel
{
    public class PagingRezervacijePrikaziVM
    {

        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int KlijentId { get; set; }
        public List<rows> listaRezervacija { get; set; }

        public class rows
        {
            public int RezervacijaId { get; set; }
            public int KlijentId { get; set; }
            public string Ime { get; set; }
            public string Klijent { get; set; }
            public string Putovanje { get; set; }

            public int PutovanjeId { get; set; }

            public string DatumRezervacije { get; set; }

            public int BrojOsoba { get; set; }

            public string Status { get; set; }

            public string Napomena { get; set; }
        }
    }
}
