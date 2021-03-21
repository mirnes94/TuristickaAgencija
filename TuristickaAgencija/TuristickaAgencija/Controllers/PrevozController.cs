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
    public class PrevozController : Controller
    {
        private readonly MojContext _db;

        public PrevozController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: true)]

        public IActionResult Index()
        {
            List<PrevozPrikaziVM> prevozPrikazi = _db.Prevoz.Select(x => new PrevozPrikaziVM
            {
                Id=x.Id,
                Firma = x.Firma.Naziv,
                BrojMjesta=x.BrojMjesta,
                CijenaPoMjestu=x.CijenaPoMjestu,
                TipPrevoza=x.TipPrevoza
            }).ToList();
            return View("Index",prevozPrikazi);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            PrevozDodajUrediVM model = new PrevozDodajUrediVM();
            model.Firma = _db.Firma.Select (x=>new SelectListItem
            {
                Value=x.Id.ToString(),
                Text=x.Naziv
            }).ToList();
            return View("Dodaj",model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(PrevozDodajUrediVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Firma = _db.Firma.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Naziv
                }).ToList();
                return View("Dodaj", model);

            }
            Prevoz prevoz;
            if (model.Id != 0)
            {
                prevoz = _db.Prevoz.Find(model.Id);

            }
            else
            {
                prevoz = new Prevoz();
                _db.Prevoz.Add(prevoz);
            }


            prevoz.FirmaID = model.FirmaID;
            prevoz.TipPrevoza = model.TipPrevoza;
            prevoz.BrojMjesta = model.BrojMjesta;
            prevoz.CijenaPoMjestu = model.CijenaPoMjestu;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int id)
        {
            var prevoz = _db.Prevoz.Find(id);
            _db.Prevoz.Remove(prevoz);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int id)
        {
            var prevoz = _db.Prevoz.Find(id);

            if (prevoz == null)
            {

                return RedirectToAction(nameof(Index));
            }
            PrevozDodajUrediVM model = new PrevozDodajUrediVM
            {
                Id=prevoz.Id,
                FirmaID=prevoz.FirmaID,
                TipPrevoza=prevoz.TipPrevoza,
                BrojMjesta=prevoz.BrojMjesta,
                CijenaPoMjestu=prevoz.CijenaPoMjestu
            };
            model.Firma = _db.Firma.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
            }).ToList();

            return View("Dodaj",model );
        }
    }
    }

