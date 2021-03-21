using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class KlijentObavijestiController : Controller
    {
        private MojContext _db;

        public KlijentObavijestiController(MojContext db)
        {
            _db = db;
        }

        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Index(int id)
        {
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
            List<ObavijestiPrikaziVM> obavijesti = _db.Obavijesti.Where(k=>k.KlijentId==pk.KorisnikId).Select(x => new ObavijestiPrikaziVM
            {
               ObavijestID=x.ObavijestiId,
               Naziv=x.Naziv,
               Sadrzaj=x.Sadrzaj,
               Klijent=x.Klijent.Ime+" "+ x.Klijent.Prezime,
               Datum=x.Datum,
               Slika=x.Slika,
               SlikaString=x.Slika.ToString()
            }).ToList();
            return View(obavijesti);
        }
    }
}

