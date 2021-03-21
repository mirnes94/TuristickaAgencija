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
    public class PutovanjaController : Controller
    {
        private MojContext _db;
        public PutovanjaController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: true)]

        public IActionResult Index()
        {
            List<PutovanjaPrikaziVM> model = _db.Putovanja.Select(x => new PutovanjaPrikaziVM
            {
                PutovanjaId = x.PutovanjaId,
                NazivPutovanja=x.NazivPutovanja,
                OpisPutovanja=x.OpisPutovanja,
                CijenaPutovanja=x.CijenaPutovanja,
                DatumPolaska=x.DatumPolaska.ToString("dd.MM.yyyy"),
                DatumDolaska =x.DatumDolaska.ToString("dd.MM.yyyy"),
                BrojPutnika=x.BrojPutnika,
                PopisPutnika=x.PopisPutnika,

                //Klijent = x.Klijent.Ime+"/ "+x.Klijent.Prezime+"/ "+x.Klijent.JMBG,
                Prevoz=x.Prevoz.Firma.Naziv,
                Smjestaj=x.Smjestaj.NazivSmjestaja
            }).ToList();
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            PutovanjaDodajUrediVM model = new PutovanjaDodajUrediVM
            {
                //Klijent = _db.Klijent.Select(x => new SelectListItem
                //{
                //    Value = x.Id.ToString(),
                //    Text = x.Ime + "/ " + x.Prezime + "/ " + x.JMBG
                //}).ToList(),
                Prevoz = _db.Prevoz.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Firma.Naziv
                }).ToList(),
                Smjestaj = _db.Smjestaj.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NazivSmjestaja
                }).ToList()
            };

            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(PutovanjaDodajUrediVM input)
        {
            
                if (!ModelState.IsValid)
                {
                    input.Prevoz = _db.Firma.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Naziv
                    }).ToList();
                    input.Smjestaj = _db.Smjestaj.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.NazivSmjestaja
                    }).ToList();

                    
                return View("Dodaj", input);
                }
             

            
            Putovanja putovanja;
            if (input.PutovanjaId != 0)
            {
                putovanja = _db.Putovanja.Find(input.PutovanjaId);

            }
            else
            {
                putovanja = new Putovanja();
                _db.Putovanja.Add(putovanja);
            }



            putovanja.NazivPutovanja = input.NazivPutovanja;
            putovanja.OpisPutovanja = input.OpisPutovanja;
            putovanja.CijenaPutovanja = input.CijenaPutovanja;
            putovanja.DatumPolaska = input.DatumPolaska;
            putovanja.DatumDolaska = input.DatumDolaska;
            putovanja.BrojPutnika = input.BrojPutnika;
            putovanja.PopisPutnika = input.PopisPutnika;

            //putovanja.KlijentId = input.KlijentId;
            putovanja.PrevozId = input.PrevozId;
            putovanja.SmjestajId = input.SmjestajId;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int id)
        {
            var putovanja = _db.Putovanja.Find(id);
            _db.Putovanja.Remove(putovanja);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int id)
        {
            var putovanja = _db.Putovanja.Find(id);

            if (putovanja == null)
            {

                return RedirectToAction(nameof(Index));
            }
            PutovanjaDodajUrediVM model = new PutovanjaDodajUrediVM
            {
                PutovanjaId = id,
                NazivPutovanja = putovanja.NazivPutovanja,
                OpisPutovanja = putovanja.NazivPutovanja,
                CijenaPutovanja = putovanja.CijenaPutovanja,
                DatumPolaska = putovanja.DatumPolaska,
                DatumDolaska = putovanja.DatumDolaska,
                BrojPutnika = putovanja.BrojPutnika,
                PopisPutnika = putovanja.PopisPutnika,

                //KlijentId = putovanja.KlijentId,
                //Klijent = _db.Klijent.Select(x => new SelectListItem
                //{
                //    Value = x.Id.ToString(),
                //    Text = x.Ime + "/ " + x.Prezime + "/ " + x.JMBG
                //}).ToList(),
                PrevozId = putovanja.PrevozId,
                Prevoz = _db.Prevoz.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Firma.Naziv
                }).ToList(),
                SmjestajId = putovanja.SmjestajId,
                Smjestaj = _db.Smjestaj.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NazivSmjestaja
                }).ToList()

            };


            return View("Dodaj", model);
        }
    }
}