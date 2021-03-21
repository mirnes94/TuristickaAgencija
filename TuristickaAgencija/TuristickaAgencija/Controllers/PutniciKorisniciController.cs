using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class PutniciKorisniciController : Controller
    {
        private MojContext db;
        public PutniciKorisniciController(MojContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Prikazi()
        {
            List<PutnikKorisnikPrikaziVM> putnici = db.PutnikKorisnik.Select(
                k => new PutnikKorisnikPrikaziVM
                {
                    PutnikKorisnikId = k.PutnikKorisnikId,
                    ImeIPrezime = k.Korisnik.Ime + " " + k.Korisnik.Prezime
                }
                )
                .ToList();
            ViewData["putnicikorisnici-kljuc"] = putnici;
            return View();
        }
        public IActionResult Dodaj()
        {
            PutniciKorisniciUrediVM model = new PutniciKorisniciUrediVM();
            model.korisnici = db.Klijent.Select(o => new SelectListItem(o.Ime + " " + o.Prezime, o.Id.ToString())).ToList();

            return View("Uredi", model);
        }

        public IActionResult Snimi(PutniciKorisniciUrediVM input)
        {
            PutnikKorisnik k;
            if (input.PutnikKorisnikID == 0)
            {
                k = new PutnikKorisnik();
                db.Add(k);

            }
            else
            {
                k = db.PutnikKorisnik.Find(input.PutnikKorisnikID);

            }


            k.PutnikKorisnikId = input.PutnikKorisnikID;
            k.KorisnikId = input.KorisnikID;
            db.SaveChanges();
            if (input.PutnikKorisnikID == 0)
                TempData["poruka-success"] = "Uspjesno ste dodali putnika";
            else
                TempData["poruka-success"] = "Uspjesno ste izmijenili podatke putnika";

            db.Dispose();

            return RedirectToAction(nameof(Prikazi));
        }
        public IActionResult Uredi(int PutnikKorisnikID)
        {

            PutnikKorisnik k = db.PutnikKorisnik.Find(PutnikKorisnikID);
            if (k == null)
            {

                return RedirectToAction(nameof(Prikazi));
            }

            PutniciKorisniciUrediVM model = new PutniciKorisniciUrediVM();
            model.korisnici = db.Klijent.Select(o => new SelectListItem(o.Ime + " " + o.Prezime, o.Id.ToString())).ToList();
            model.KorisnikID = k.KorisnikId;


            return View("Uredi", model);
        }

        public IActionResult Obrisi(int PutnikKorisnikID)
        {
            PutnikKorisnik k = db.PutnikKorisnik.Find(PutnikKorisnikID);
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