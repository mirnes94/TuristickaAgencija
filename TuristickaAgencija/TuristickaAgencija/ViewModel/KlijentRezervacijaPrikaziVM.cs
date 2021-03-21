using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class KlijentRezervacijaPrikaziVM
    {
        public int RezervacijaId { get; set; }
        public int KlijentId { get; set; }
        public string Ime { get; set; }
        public string Klijent { get; set; }
        public string Putovanje { get; set; }

        public int PutovanjeId { get; set; }

        public DateTime DatumRezervacije { get; set; }

        public int BrojOsoba { get; set;}

        public string Status { get; set; }

        public string Napomena { get; set; }


    }
}
