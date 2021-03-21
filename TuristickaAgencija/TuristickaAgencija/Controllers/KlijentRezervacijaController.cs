using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;
using static TuristickaAgencija.ViewModel.PagingRezervacijePrikaziVM;

namespace TuristickaAgencija.Controllers
{
    public class KlijentRezervacijaController : Controller
    {
        private MojContext _db;

        public KlijentRezervacijaController(MojContext db)
        {
            _db = db;
        }

        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Index(int id)
        {
            int maxRows = 6;
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
            double pageCount = (double)((decimal)_db.Rezervacija.Where(rez => rez.KlijentID == pk.KorisnikId).Count() / Convert.ToDecimal(maxRows));
            return View(this.GetRezervacije(id, (int)Math.Ceiling(pageCount)));
        }
        [HttpPost]
        public IActionResult Index(int id, int currentPageIndex)
        {
            return View(this.GetRezervacije(id, currentPageIndex));
        }

        private PagingRezervacijePrikaziVM GetRezervacije(int id,int currentPage)
        {
            int maxRows = 6;
            PagingRezervacijePrikaziVM model = new PagingRezervacijePrikaziVM();
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
            model.KlijentId = id;
            model.listaRezervacija= _db.Rezervacija.Where(a => a.KlijentID == pk.KorisnikId).Select(x => new rows
            {
                RezervacijaId = x.Id,
                KlijentId = (int)x.KlijentID,
                Ime = x.Ime,
                Klijent = x.Klijent.Ime + " " + x.Klijent.Prezime,
                Putovanje = x.Putovanje.NazivPutovanja,
                DatumRezervacije = x.DatumRezervacije.ToString("dd.MM.yyyy/HH:mm"),
                BrojOsoba = x.BrojOsoba,
                Status = x.Status,
                Napomena = x.Napomena
            }).OrderBy(rez => rez.RezervacijaId)
              .Skip((currentPage - 1) * maxRows)
              .Take(maxRows)
              .ToList();

          
            double pageCount = (double)((decimal)_db.Rezervacija.Where(rez=>rez.KlijentID==pk.KorisnikId).Count() / Convert.ToDecimal(maxRows));
            model.PageCount = (int)Math.Ceiling(pageCount);
            
         
            model.CurrentPageIndex = currentPage;

            return model;
        }
        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Dodaj(int putovanjeid, int klijentid)
        {


            KlijentRezervacijaDodajUrediVM model = new KlijentRezervacijaDodajUrediVM();

            model.KlijentId = klijentid;
            model.PutovanjeId = putovanjeid;
            model.ImePrezimeKlijenta= _db.PutnikKorisnik.Where(s => s.KorisnikId == model.KlijentId).Include(k=>k.Korisnik).FirstOrDefault().Korisnik.Ime+" "+_db.PutnikKorisnik.Where(s => s.KorisnikId == model.KlijentId).FirstOrDefault().Korisnik.Prezime;
            model.NazivPutovanja = _db.Putovanja.Where(s => s.PutovanjaId == model.PutovanjeId).FirstOrDefault().NazivPutovanja;
            model.Datum = DateTime.Now ;

            return View("Dodaj", model);
        }

        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Snimi(KlijentRezervacijaDodajUrediVM input)
        {
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnikId == input.KlijentId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return View("Dodaj", input);
            }
            Rezervacija rezervacija;
            Obavijesti obavijest;
            if (input.RezervacijaId != 0)
            {
                rezervacija = _db.Rezervacija.Find(input.RezervacijaId);
              
            }
            else
            {
                rezervacija = new Rezervacija();
                _db.Rezervacija.Add(rezervacija);
                _db.SaveChanges();

            }
            rezervacija.Ime = input.Ime;
            rezervacija.KlijentID = input.KlijentId;
            rezervacija.PutovanjeID = input.PutovanjeId;
            rezervacija.DatumRezervacije = DateTime.Now;
            rezervacija.BrojOsoba = input.BrojOsoba;
            rezervacija.Status = "";
            rezervacija.Napomena = "";
            _db.SaveChanges();

            if (input.RezervacijaId != 0)
            {
                obavijest = new Obavijesti();
                obavijest.Naziv = "Zahtjev za rezervaciju";
                obavijest.Sadrzaj = "Izmijenjen je zahtjev za rezervaciju.";
                obavijest.KlijentId = rezervacija.KlijentID;
                obavijest.Datum = DateTime.Now;
                obavijest.Slika = null;
                _db.Obavijesti.Add(obavijest);
                _db.SaveChanges();
            }
            else
            {
                obavijest = new Obavijesti();
                obavijest.Naziv = "Zahtjev za rezervaciju";
                obavijest.Sadrzaj = "Dobili ste novi zahtjev za rezervaciju.";
                obavijest.KlijentId = rezervacija.KlijentID;
                obavijest.Datum = DateTime.Now;
                obavijest.Slika = null;
                _db.Obavijesti.Add(obavijest);
                _db.SaveChanges();
            }

            return Redirect("/KlijentRezervacija/Index?id=" + pk.KorisnickiNalogId);
        }

        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Obrisi(int id)
        {

            Rezervacija rezervacija = _db.Rezervacija.Find(id);

            var klijentid = rezervacija.KlijentID;
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(p => p.KorisnikId == klijentid).FirstOrDefault();

            Obavijesti obavijest = new Obavijesti();
            obavijest.Naziv = "Zahtjev za rezervaciju";
            obavijest.Sadrzaj = "Rezervacija je otkazana.";
            obavijest.KlijentId = rezervacija.KlijentID;
            obavijest.Datum = DateTime.Now;
            obavijest.Slika = null;
            _db.Obavijesti.Add(obavijest);
            _db.SaveChanges();

            _db.Rezervacija.Remove(rezervacija);
            _db.SaveChanges();

            return Redirect("/KlijentRezervacija/Index?id=" + pk.KorisnickiNalogId);
        }

        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Uredi (int id)
        {
            var rezervacija = _db.Rezervacija.Find(id);
            
                KlijentRezervacijaDodajUrediVM model = new KlijentRezervacijaDodajUrediVM
                {
                    RezervacijaId = rezervacija.Id,
                    Ime = rezervacija.Ime,
                    KlijentId = (int)rezervacija.KlijentID,
                    ImePrezimeKlijenta = _db.PutnikKorisnik.Where(s => s.KorisnikId == rezervacija.KlijentID).Include(k => k.Korisnik).FirstOrDefault().Korisnik.Ime + " " + _db.PutnikKorisnik.Where(s => s.KorisnikId == rezervacija.KlijentID).FirstOrDefault().Korisnik.Prezime,
                    PutovanjeId = (int)rezervacija.PutovanjeID,
                    NazivPutovanja = _db.Putovanja.Where(s => s.PutovanjaId == rezervacija.PutovanjeID).FirstOrDefault().NazivPutovanja,
                    Datum = DateTime.Now,
                    BrojOsoba = rezervacija.BrojOsoba,
                    Status = rezervacija.Status,
                    Napomena = rezervacija.Napomena
                };
                return View("Dodaj", model);
            }
    }


}


