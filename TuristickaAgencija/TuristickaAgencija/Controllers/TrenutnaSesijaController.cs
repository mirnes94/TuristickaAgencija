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
    [Autorizacija(zaposlenik: true, putnikkorisnik: true)]
    public class TrenutnaSesijaController : Controller
    {
        private readonly MojContext _db;

        public TrenutnaSesijaController(MojContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            TrenutnaSesijaIndexVM model = new TrenutnaSesijaIndexVM();
            model.Rows = _db.AutorizacijskiToken.Select(s => new TrenutnaSesijaIndexVM.Row
            {
                VrijemeLogiranja = s.VrijemeEvidentiranja,
                token = s.Vrijednost,
                IpAdresa = s.IpAdresa
            }).ToList();
            model.TrenutniToken = HttpContext.GetTrenutniToken();
            return View(model);
        }
        public IActionResult Obrisi(string token)
        {
            AutorizacijskiToken obrisati = _db.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == token);
            if (obrisati != null)
            {
                _db.AutorizacijskiToken.Remove(obrisati);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}