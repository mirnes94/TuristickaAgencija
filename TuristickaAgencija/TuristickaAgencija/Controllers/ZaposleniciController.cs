using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class ZaposleniciController : Controller
    {
        private MojContext db;
        public ZaposleniciController(MojContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Prikazi()
        {
            List<ZaposlenikPrikaziVM> zaposlenici = db.Zaposlenici.Select(
                k => new ZaposlenikPrikaziVM
                {
                    ZaposlenikId = k.ZaposlenikId,
                    StrucnaSprema = k.StrucnaSprema,
                    ImeIPrezime = k.Korisnik.Ime + " " + k.Korisnik.Prezime
                }
                )
                .ToList();
            ViewData["zaposlenik-kljuc"] = zaposlenici;
            return View();
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            ZaposlenikUrediVM model = new ZaposlenikUrediVM();
            model.korisnici = db.Klijent.Select(o => new SelectListItem(o.Ime + " " + o.Prezime, o.Id.ToString())).ToList();

            return View("Uredi", model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(ZaposlenikUrediVM input)
        {
            Zaposlenici k;
            if (input.ZaposlenikId == 0)
            {
                k = new Zaposlenici();
                db.Add(k);

            }
            else
            {
                k = db.Zaposlenici.Find(input.ZaposlenikId);

            }


            k.ZaposlenikId = input.ZaposlenikId;
            k.StrucnaSprema = input.StrucnaSprema;
            k.KorisnikID = input.KorisnikId;
            db.SaveChanges();
            if (input.ZaposlenikId == 0)
                TempData["poruka-success"] = "Uspjesno ste dodali smještaj";
            else
                TempData["poruka-success"] = "Uspjesno ste izmijenili podatke smještaja";

            db.Dispose();

            return RedirectToAction(nameof(Prikazi));
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int ZaposlenikID)
        {

            Zaposlenici k = db.Zaposlenici.Find(ZaposlenikID);
            if (k == null)
            {

                return RedirectToAction(nameof(Prikazi));
            }

            ZaposlenikUrediVM model = new ZaposlenikUrediVM();
            model.korisnici = db.Klijent.Select(o => new SelectListItem(o.Ime + " " + o.Prezime, o.Id.ToString())).ToList();
            model.KorisnikId = k.KorisnikID;
            model.StrucnaSprema = k.StrucnaSprema;


            return View("Uredi", model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int ZaposlenikID)
        {
            Zaposlenici k = db.Zaposlenici.Find(ZaposlenikID);
            if (k == null)
            {


            }
            else
            {
                db.Remove(k);

                db.SaveChanges();


            }
            return RedirectToAction(nameof(Prikazi));
        }

    }
}