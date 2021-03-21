using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class PutovanjaPrikaziVM
    {
        public int KorisnikId { get; set; }
        public int PutovanjaId { get; set; }
        public string NazivPutovanja { get; set; }
        public string OpisPutovanja { get; set; }
        public float CijenaPutovanja { get; set; }
        public string DatumPolaska { get; set; }
        public string DatumDolaska { get; set; }
        public int BrojPutnika { get; set; }
        public string PopisPutnika { get; set; }
        //public string Klijent { get; set; }
        public string Prevoz { get; set; }
        public string TipPrevoza { get; set; }
        public int BrojMjesta { get; set; }
        public float CijenaPoMjestu { get; set; }

        public string Smjestaj { get; set; }
        public string OpisSmjestaja { get; set; }

        public string Grad { get; set; }
        public string Drzava { get; set; }
        public float CijenaNoćenja { get; set; }

        public string TipSobe { get; set; }

        public byte[] Slika { get; set; }
       
    }
}
