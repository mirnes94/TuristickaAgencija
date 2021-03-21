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
    public class FirmaController : Controller
    {
        private readonly MojContext _db;

        public FirmaController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: true)]

        public IActionResult Index()
        {
            List<FirmaPrikaziVM> firme = _db.Firma.Select(x => new FirmaPrikaziVM
            {
                Id=x.Id,
                Naziv=x.Naziv,
                Grad=x.Grad.NazivGrada,
                Adresa=x.Adresa,
                BrojZiroracuna=x.BrojZiroracuna
            }).ToList();
                
            return View(firme);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            FirmaUrediDodajVM model = new FirmaUrediDodajVM();
      
            model.listaGradova=_db.Gradovi.Select(x=> new SelectListItem { 
                Value=x.Id.ToString(),
                Text=x.NazivGrada
            }).ToList();
            return View("Dodaj", model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(FirmaUrediDodajVM model)
        {
            if (!ModelState.IsValid)
            {
                model.listaGradova = _db.Gradovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.NazivGrada
                }).ToList();
              
                return View("Dodaj", model);

            }
            Firma firma;
            if (model.Id != 0)
            {
                firma = _db.Firma.Find(model.Id);

            }
            else
            {
                firma = new Firma();
                _db.Firma.Add(firma);
            }

            firma.GradID = model.GradID;
            firma.Naziv = model.Naziv;
            firma.Adresa = model.Adresa;
            firma.BrojZiroracuna = model.BrojZiroracuna;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
        public IActionResult Obrisi(int id)
        {
            Firma
                k = _db.Firma.Find(id);
            if (k == null)
            {


            }
            else
            {
                _db.Remove(k);

                _db.SaveChanges();


            }
            return RedirectToAction(nameof(Index));
        }
        //public IActionResult Obrisi(int id)
        //{
        //    var firma = _db.Firma.Find(id);
        //    _db.Firma.Remove(firma);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int id)
        {
            var firma = _db.Firma.Find(id);

            if (firma == null)
            {

                return RedirectToAction(nameof(Index));
            }

            FirmaUrediDodajVM model= new FirmaUrediDodajVM
            {
                Id=firma.Id,
                Naziv=firma.Naziv,
                GradID=firma.GradID,
                Adresa=firma.Adresa,
                BrojZiroracuna=firma.BrojZiroracuna

            };
           model.listaGradova = _db.Gradovi.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NazivGrada
            }).ToList();

           
            return View("Dodaj", model);
        }
    }
}
