using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.ViewModel;
using TuristickaAgencija.Helper;


namespace TuristickaAgencija.Controllers
{
    public class SmjestajController : Controller
    {
        private readonly MojContext _db;
        public SmjestajController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: true)]
        public IActionResult Index()
        {
            List<SmjestajPrikaziVM> model = _db.Smjestaj.Include(x => x.Grad).Select(
                k => new SmjestajPrikaziVM
                {
                    SmjestajID = k.Id,
                    NazivSmjestaja = k.NazivSmjestaja,
                    OpisSmjestaja = k.OpisSmjestaja,
                    Grad = k.Grad.NazivGrada,
                    CijenaNocenja = k.CijenaNocenja,
                    Tip_sobe = k.Tip_sobe,
                    Slika = k.Slika
                }).ToList();
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            SmjestajUrediVM model = new SmjestajUrediVM();
            model.Grad = _db.Gradovi.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.NazivGrada
            }).ToList();
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(SmjestajUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                input.Grad = _db.Gradovi.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.NazivGrada
                }).ToList();
             
                return View("Dodaj", input);

            }
            Smjestaj k;
            if (input.SmjestajID == 0)
            {
                k = new Smjestaj();
                _db.Add(k);

            }
            else
            {
                k = _db.Smjestaj.Find(input.SmjestajID);
            }

            if (input.Slika != null)
            {
                var memoryStream = new MemoryStream();


                input.Slika.CopyTo(memoryStream);
                var j = memoryStream.ToArray();
                k.Slika = j;

            }
            k.Id = input.SmjestajID;
            k.NazivSmjestaja = input.NazivSmjestaja;
            k.OpisSmjestaja = input.OpisSmjestaja;
            k.GradID = input.GradID;
            k.CijenaNocenja = input.CijenaNocenja;
            k.Tip_sobe = input.Tip_sobe;

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int SmjestajID)
        {

            Smjestaj k = _db.Smjestaj.Find(SmjestajID);
            if (k == null)
            {
                
                return RedirectToAction(nameof(Index));
            }

            SmjestajUrediVM model = new SmjestajUrediVM();
            model.Grad = _db.Gradovi.Select(o => new SelectListItem(o.NazivGrada, o.Id.ToString())).ToList();
            model.GradID = k.GradID;
            model.NazivSmjestaja = k.NazivSmjestaja;
            model.OpisSmjestaja = k.OpisSmjestaja;
            model.CijenaNocenja = k.CijenaNocenja;
            model.Tip_sobe = k.Tip_sobe;



            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int SmjestajID)
        {
            Smjestaj k = _db.Smjestaj.Find(SmjestajID);
            if (k == null)
            {
                return RedirectToAction(nameof(Index));

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