using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class FirmaPrikaziVM
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public string Grad { get; set; }

        public string Adresa { get; set; }

        public string BrojZiroracuna { get; set; }
    }
}
