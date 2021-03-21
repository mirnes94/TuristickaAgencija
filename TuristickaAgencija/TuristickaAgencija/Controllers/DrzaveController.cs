using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;


namespace TuristickaAgencija.Controllers
{
    [Autorizacija(zaposlenik: true, putnikkorisnik: false)]
    public class DrzaveController : Controller
    {
        private readonly MojContext _db;
        public DrzaveController(MojContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var svePrikazi = _db.Drzava.ToList();
            return View(svePrikazi);
        }
        public IActionResult Dodaj()
        {
            return View("Forma", new Drzava());
        }
        [HttpGet]
        public IActionResult Izmjena(int id)
        {
            var drzava = _db.Drzava.Find(id);


            return View("Forma", drzava);
        }
        public IActionResult Snimi(Drzava model)
        {
            Drzava drzava;
            if (model.DrzavaId != 0)
            {
                drzava = _db.Drzava.Find(model.DrzavaId);
            }
            else
            {
                drzava = new Drzava();

                _db.Drzava.Add(drzava);
            }
            drzava.Naziv = model.Naziv;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        //public IActionResult Izbrisi(int id)
        //{
        //    var drzava = _db.Drzava.Find(id);
        //    _db.Drzava.Remove(drzava);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public IActionResult Izbrisi(int id)
        {
            Drzava
                k = _db.Drzava.Find(id);
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
    }
}