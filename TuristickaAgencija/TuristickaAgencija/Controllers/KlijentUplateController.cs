using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;
using static TuristickaAgencija.ViewModel.KlijentUplateVM;

namespace TuristickaAgencija.Controllers
{
    public class KlijentUplateController : Controller
    {
        private MojContext _db;

        public KlijentUplateController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Index(int id)
        {
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
            KlijentUplateVM model = new KlijentUplateVM();
            model.Ukupno = _db.Uplate.Where(a => a.KlijentId == pk.KorisnikId).Sum(u=>u.Iznos);
            model.listaUplata = _db.Uplate.Where(a => a.KlijentId == pk.KorisnikId).Select(x => new rows
            {
                KlijentId= (int)x.KlijentId,
                Putovanje = x.Putovanja.NazivPutovanja,
                DatumUplate = x.DatumUplate.ToString("dd.MM.yyyy"),
                IznosUplate = x.Iznos
            }
            ).ToList(); 
                return View(model);
             
        }
      

    }
}
