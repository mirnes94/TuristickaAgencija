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
    public class AutentifikacijaController : Controller
    {
        private MojContext db;
        public AutentifikacijaController(MojContext _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiLozinku = true,
            });
        }

        public IActionResult Login(LoginVM input)
        {

            KorisnickiNalozi korisnik = db.KorisnickiNalozi
                .SingleOrDefault(x => x.KorisnickoIme == input.KorisnickoIme && x.Lozinka == input.Lozinka);

            if (korisnik == null)
            {
                TempData["error_poruka"] = "pogrešan username ili password";
                return View("Index", input);
            }

            HttpContext.SetLogiraniKorisnik(korisnik);
            PutnikKorisnik pk = db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == korisnik.KorisnickiNalogId).FirstOrDefault();
            Zaposlenici z = db.Zaposlenici.Where(s => s.KorisnickiNalogId == korisnik.KorisnickiNalogId).FirstOrDefault();
            
            if (z != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "KlijentHome");
            }
           
        }
        public IActionResult Registracija()
        {
            RegistracijaVM model = new RegistracijaVM();
            model.korisnicko = "";
            model.korisnikovalozinka = "";
            model.gradovi = db.Gradovi.Select(z => new SelectListItem
            {
                Value = z.Id.ToString(),
                Text = z.NazivGrada
            }).ToList();

            return View("Registracija", model);
        }



        [ValidateAntiForgeryToken]
        public IActionResult SnimiRegistraciju(RegistracijaVM input)
        {
            if (!ModelState.IsValid)
            {
                return View("Registracija");
            }
            if (input != null)
            {
                var n = db.KorisnickiNalozi
                    .Where((x) => x.KorisnickoIme == input.korisnicko)
                    .FirstOrDefault();

                //if(n != null)
                //{
                //    throw new Exception("Korisnicki nalog postoji");
                //}

                if (n != null)
                {
                    TempData["error_poruka"] = "Vec postoji korisnik sa tim korisničkim imenom";
                    return View("Index"
                        );
                }

                Korisnik k = new Korisnik
                {
                    Ime = input.Ime,
                    Prezime = input.Prezime,
                    DatumRodjenja = input.DatumRodjenja,
                    Kontakt = input.Kontakt,
                    JMBG = input.JMBG,
                    GradID = input.GradID
                };
                db.Klijent.Add(k);
                db.SaveChanges();

                KorisnickiNalozi kn = new KorisnickiNalozi
                {
                    KorisnickoIme = input.korisnicko,
                    Lozinka = input.korisnikovalozinka
                };
                db.KorisnickiNalozi.Add(kn);
                db.SaveChanges();

                PutnikKorisnik pk = new PutnikKorisnik
                {
                    KorisnikId = k.Id,
                    KorisnickiNalogId = kn.KorisnickiNalogId
                };
                db.PutnikKorisnik.Add(pk);
                db.SaveChanges();
            }

           
            return RedirectToAction("Index");
        }


       
    }
}