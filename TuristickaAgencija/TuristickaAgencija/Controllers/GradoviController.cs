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

namespace TuristickaAgencija.Controllers
{
    [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

    public class GradoviController : Controller
    {
        private readonly MojContext _db;

        public GradoviController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Gradovi> sviGradovi = _db.Gradovi.Include(x => x.Drzava).ToList();

            return View(sviGradovi);
            
        }
        [HttpGet]
        public IActionResult Dodavanje()
        {
            DrzavaListVM viewModel = new DrzavaListVM
            {
                Grad = new GradoviVM(),
                Drzave = _db.Drzava.ToList()
            };

            return View("Forma", viewModel);
        }


        [HttpPost]
        public IActionResult Snimi(DrzavaListVM model)
        {
            if (!ModelState.IsValid)
                return View("Forma", model);

            Gradovi grad;

            if (model.Grad.Id != 0)
            {
                grad = _db.Gradovi.Find(model.Grad.Id);
            }
            else
            {
                grad = new Gradovi();

                _db.Gradovi.Add(grad);
            }

            grad.NazivGrada = model.Grad.Naziv;

            grad.DrzavaId = model.Grad.DrzavaId;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        //public IActionResult Izbrisi(int id)
        //{
        //    var grad = _db.Gradovi.Find(id);
        //    if (grad == null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    _db.Gradovi.Remove(grad);
        //    _db.SaveChanges();
            

        //    return RedirectToAction("Index");
        //}
        public IActionResult Izbrisi(int id)
        {
            Gradovi
                k = _db.Gradovi.Find(id);


           
            if (k == null)
            {
                /*tu brisem?*/
                Console.WriteLine("Nije moguce brisanje");

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