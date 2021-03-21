using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class KlijentController : Controller
    {
        private readonly MojContext _db;

        public KlijentController(MojContext db)
        {
            _db = db;
        }
        public IActionResult SearchKorisnik(string searchString)
        {
            if (searchString == null)
            {
                List<KlijentPrikaziVM> prikazi = _db.Klijent.Include(x => x.Grad)

                                            .Select(a => new KlijentPrikaziVM()
                                            {
                                                Ime = a.Ime,
                                                Prezime = a.Prezime,
                                                Kontakt = a.Kontakt,
                                                JMBG = a.JMBG,
                                                Grad=a.Grad.NazivGrada
                                                



                                            }).ToList();
                return View("Index", prikazi);
            }
            List<KlijentPrikaziVM> model = _db.Klijent.Include(x => x.Grad)
                                         .Where(x => x.Ime.ToLower().Contains(searchString.ToLower())
                                         || x.Prezime.ToLower().Contains(searchString.ToLower())
                                         || searchString == null)
                                             .Select(a => new KlijentPrikaziVM()
                                             {
                                                 Ime = a.Ime,
                                                 Prezime = a.Prezime,
                                                 Kontakt = a.Kontakt,
                                                 JMBG = a.JMBG,
                                                 Grad = a.Grad.NazivGrada


                                             }).ToList();

            return View("Index", model);
        }

        public IActionResult Index()
        {
            List<KlijentPrikaziVM> klijenti = _db.Klijent.Select(x => new KlijentPrikaziVM
            {
                KlijentId=x.Id,
                Ime=x.Ime,
                Prezime=x.Prezime,
                DatumRodjenja=x.DatumRodjenja.ToString("dd.MM.yyyy"),
                Kontakt=x.Kontakt,
                JMBG=x.JMBG,
                Grad=x.Grad.NazivGrada
            }).ToList();

            return View(klijenti);
        }
        public IActionResult Dodaj()
        {
            KlijentUrediDodajVM model = new KlijentUrediDodajVM
            {
                Grad = _db.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NazivGrada
                }).ToList()
            };
            
            return View(model);
        }
        public IActionResult Snimi(KlijentUrediDodajVM input)
        {
            Korisnik klijent;
            if (input.KlijentId!= 0)
            {
                klijent = _db.Klijent.Find(input.KlijentId);

            }
            else
            {
                klijent = new Korisnik();
                _db.Klijent.Add(klijent);
            }



            klijent.Ime = input.Ime;
            klijent.Prezime = input.Prezime;
            klijent.DatumRodjenja = input.DatumRodjenja;
            klijent.Kontakt = input.Kontakt;
            klijent.JMBG = input.JMBG;
            klijent.GradID = input.GradId;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Obrisi(int id)
        {
            var klijent = _db.Klijent.Find(id);
            _db.Klijent.Remove(klijent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int id)
        {
            var klijent = _db.Klijent.Find(id);

            if (klijent == null)
            {

                return RedirectToAction(nameof(Index));
            }
            KlijentUrediDodajVM model = new KlijentUrediDodajVM
            {
                KlijentId=id,
                Ime=klijent.Ime,
                Prezime=klijent.Prezime,
                DatumRodjenja=klijent.DatumRodjenja,
                Kontakt=klijent.Kontakt,
                JMBG=klijent.JMBG,
                GradId=klijent.GradID,
                Grad = _db.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NazivGrada
                }).ToList()
            };

            return View("Dodaj", model);
        }
    }
}
