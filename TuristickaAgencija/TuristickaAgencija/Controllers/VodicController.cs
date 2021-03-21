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
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class VodicController : Controller
    {
        private readonly MojContext _db;
        public VodicController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult SearchVodic(string searchString)
        {
            if (searchString == null)
            {
                List<VodicPrikaziVM> prikazi = _db.Vodic.Include(x => x.Putovanja)

                                            .Select(a => new VodicPrikaziVM()
                                            {
                                                Ime = a.Ime,
                                                Prezime = a.Prezime,
                                                Kontakt = a.Kontakt,
                                                JMBG = a.JMBG,
                                                Putovanje = a.Putovanja.NazivPutovanja,
                                                Slika=a.Slika
                                               

                                            }).ToList();
                return View("Index", prikazi);
            }
            List<VodicPrikaziVM> model = _db.Vodic.Include(x => x.Putovanja)
                                         .Where(x => x.Ime.ToLower().Contains(searchString.ToLower())
                                         || x.Prezime.ToLower().Contains(searchString.ToLower())
                                         || searchString == null)
                                             .Select(a => new VodicPrikaziVM()
                                             {
                                                 Ime = a.Ime,
                                                 Prezime = a.Prezime,
                                                 Kontakt = a.Kontakt,
                                                 JMBG = a.JMBG,
                                                 Putovanje = a.Putovanja.NazivPutovanja,
                                                 Slika = a.Slika


                                             }).ToList();

            return View("Index", model);
        }
        public IActionResult Index()
        {
            List<VodicPrikaziVM> model = _db.Vodic.Include(x => x.Putovanja).Select(
                k => new VodicPrikaziVM
                {
                    VodicId = k.VodicId,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    JMBG = k.JMBG,
                    Kontakt = k.Kontakt,
                    Putovanje = k.Putovanja.NazivPutovanja,
                    Slika = k.Slika
                }).ToList();
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            VodicUrediVM model = new VodicUrediVM();
            model.Putovanje = _db.Putovanja.Select(a => new SelectListItem
            {
                Value = a.PutovanjaId.ToString(),
                Text = a.NazivPutovanja
            }).ToList();
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(VodicUrediVM input)
        {
            if (!ModelState.IsValid)
            {
                return View("Dodaj", input);

            }
            Vodic k;
            if (input.VodicID == 0)
            {
                k = new Vodic();
                _db.Add(k);

            }
            else
            {
                k = _db.Vodic.Find(input.VodicID);
            }

            if (input.Slika != null)
            {
                var memoryStream = new MemoryStream();


                input.Slika.CopyTo(memoryStream);
                var j = memoryStream.ToArray();
                k.Slika = j;

            }
            k.Ime = input.Ime;
            k.Prezime = input.Prezime;
            k.Kontakt = input.Kontakt;
            k.JMBG = input.JMBG;

            k.PutovanjaID = input.PutovanjeID;


            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int VodicId)
        {

            Vodic v = _db.Vodic.Find(VodicId);
            if (v == null)
            {

                return RedirectToAction(nameof(Index));
            }

            VodicUrediVM model = new VodicUrediVM();
            model.Putovanje = _db.Putovanja.Select(o => new SelectListItem(o.NazivPutovanja, o.PutovanjaId.ToString())).ToList();
            model.PutovanjeID = v.PutovanjaID;
            model.Ime = v.Ime;
            model.Prezime = v.Prezime;
            model.Kontakt = v.Kontakt;
            model.JMBG = v.JMBG;





            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int VodicId)
        {
            Vodic v = _db.Vodic.Find(VodicId);
            if (v == null)
            {


            }
            else
            {
                _db.Remove(v);

                _db.SaveChanges();


            }
            return RedirectToAction(nameof(Index));
        }
    }
}