using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class RezervacijaController : Controller
    {


        private MojContext _db;

        public RezervacijaController(MojContext db)
        {
            _db = db;
        }

        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Index()
        {
            

            List<KlijentRezervacijaPrikaziVM> model = _db.Rezervacija.Select(x => new KlijentRezervacijaPrikaziVM
            {
                RezervacijaId = x.Id,
                PutovanjeId= (int)x.PutovanjeID,
                KlijentId = (int)x.KlijentID,
                Ime = x.Ime,
                Klijent = x.Klijent.Ime + " " + x.Klijent.Prezime,
                Putovanje = x.Putovanje.NazivPutovanja,
                DatumRezervacije = x.DatumRezervacije,
                BrojOsoba = x.BrojOsoba,
                Status = x.Status,
                Napomena = x.Napomena
            }).ToList();

            return View(model);
        }

        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Potvrdjene()
        {


            List<KlijentRezervacijaPrikaziVM> model = _db.Rezervacija
                .Where(r=>r.Status=="Potvrđeno" || r.Status == "potvrđeno" || r.Status == "POTVRĐENO")
                .Select(x => new KlijentRezervacijaPrikaziVM
            {
                RezervacijaId = x.Id,
                PutovanjeId = (int)x.PutovanjeID,
                KlijentId = (int)x.KlijentID,
                Ime = x.Ime,
                Klijent = x.Klijent.Ime + " " + x.Klijent.Prezime,
                Putovanje = x.Putovanje.NazivPutovanja,
                DatumRezervacije = x.DatumRezervacije,
                BrojOsoba = x.BrojOsoba,
                Status = x.Status,
                Napomena = x.Napomena
            }).ToList();

            return View(model);
        }

        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Odbijene()
        {


            List<KlijentRezervacijaPrikaziVM> model = _db.Rezervacija
                .Where(r => r.Status == "Odbijeno" || r.Status == "odbijeno" || r.Status == "ODBIJENO")
                .Select(x => new KlijentRezervacijaPrikaziVM
                {
                    RezervacijaId = x.Id,
                    PutovanjeId = (int)x.PutovanjeID,
                    KlijentId = (int)x.KlijentID,
                    Ime = x.Ime,
                    Klijent = x.Klijent.Ime + " " + x.Klijent.Prezime,
                    Putovanje = x.Putovanje.NazivPutovanja,
                    DatumRezervacije = x.DatumRezervacije,
                    BrojOsoba = x.BrojOsoba,
                    Status = x.Status,
                    Napomena = x.Napomena
                }).ToList();

            return View(model);
        }

        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Pregled(int rezervacijaid,int putovanjeid,int klijentid,string ime,DateTime datum, int brojosoba)
        {

            RezervacijaDodajVM model = new RezervacijaDodajVM();

                model.RezervacijaId =rezervacijaid;
                model.PutovanjeId = putovanjeid;
                model.KlijentId = klijentid;
                model.Ime =ime;
                model.Datum = datum;
                model.BrojOsoba = brojosoba;
              
            return View("Pregled", model);
        }

        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Potvrdi(RezervacijaDodajVM input)
        {
            Rezervacija rezervacija = _db.Rezervacija.Find(input.RezervacijaId);

            if (!ModelState.IsValid)
            {
                return View("Pregled", input);
            }

            rezervacija.Ime = input.Ime;
            rezervacija.KlijentID = input.KlijentId;
            rezervacija.PutovanjeID = input.PutovanjeId;
            rezervacija.DatumRezervacije = input.Datum;
            rezervacija.BrojOsoba = input.BrojOsoba;
            rezervacija.Status = input.Status;
            rezervacija.Napomena = input.Napomena;
            _db.SaveChanges();

            Obavijesti obavijest = new Obavijesti();
            if (rezervacija.Status=="Potvrđeno" || rezervacija.Status == "potvrđeno" || rezervacija.Status == "POTVRĐENO")
            {
              
                obavijest.Naziv = "Odgovor na zahtjev za rezervaciju";
                obavijest.Sadrzaj = "Vaša rezervacija je potvrđena";
                obavijest.KlijentId = rezervacija.KlijentID;     
                obavijest.Datum = DateTime.Now;
                obavijest.Slika = null;
                _db.Obavijesti.Add(obavijest);
            }
            else
            {
                obavijest.Naziv = "Odgovor na zahtjev za rezervaciju";
                obavijest.Sadrzaj = "Vaša rezervacija je odbijena";
                obavijest.KlijentId = rezervacija.KlijentID;
                obavijest.Datum = DateTime.Now;
                obavijest.Slika = null;
                _db.Obavijesti.Add(obavijest);
            }

            _db.SaveChanges();
          

            return Redirect("Index");
        }
    
    }

}
