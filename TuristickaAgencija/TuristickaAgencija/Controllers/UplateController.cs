using System;
using System.Collections.Generic;
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
    public class UplateController : Controller
    {
        private MojContext db;
        public UplateController(MojContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: true)]

        public IActionResult Prikazi()
        {
            UplatePrikaziVM model = new UplatePrikaziVM()
            {
                BrojUplata = db.Uplate.Count(),
                UkupnoUplaceno = db.Uplate.Sum(s => s.Iznos),
                lista = db.Uplate.Include
                (x => x.Klijent).Include(x => x.Putovanja).
                Select(
                k => new UplatePrikaziVM.Row
                {
                    UplataId = k.UplataId,
                    Iznos = k.Iznos,
                    DatumUplate = k.DatumUplate,
                    

                    Svrha = k.PutovanjaID != null ? "Putovanje: " + k.Putovanja.NazivPutovanja : " : ",
                    Klijent = k.Klijent.Ime + " " + k.Klijent.Prezime

                })
               .ToList()
            };


            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Dodaj()
        {
            UplateUrediVM model = new UplateUrediVM
            {
                klijenti = db.Klijent.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime + "/ " + x.Prezime
                }).ToList(),
                putovanja = db.Putovanja.Select(x => new SelectListItem
                {
                    Value = x.PutovanjaId.ToString(),
                    Text = x.NazivPutovanja
                }).ToList()
            };
            model.DatumUplate = DateTime.Now;

            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public bool ProvjeraIznosa(UplateUrediVM model)
        {
            if (model.PutovanjaId != null)
            {
                Putovanja p = db.Putovanja.Find(model.PutovanjaId);
                if (db.Uplate.Where(x => x.KlijentId == model.KlijentId && x.PutovanjaID == model.PutovanjaId).
                    Sum(x => x.Iznos) >= p.CijenaPutovanja)
                {
                    return false;
                }

            }

            return true;
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Snimi(UplateUrediVM input)
        {

            if (ModelState.IsValid)
            {
                if (ProvjeraIznosa(input) == false)
                {

                    input.klijenti = db.Klijent.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Ime + "/ " + x.Prezime
                    }).ToList();
                    input.putovanja = db.Putovanja.Select(x => new SelectListItem
                    {
                        Value = x.PutovanjaId.ToString(),
                        Text = x.NazivPutovanja
                    }).ToList();
                   

                    input.Placeno = true;
                    return View("Dodaj", input);
                }
            }

            if (!ModelState.IsValid)
            {

                UplateUrediVM model = new UplateUrediVM
                {
                    klijenti = db.Klijent.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Ime + "/ " + x.Prezime
                    }).ToList(),
                    putovanja = db.Putovanja.Select(x => new SelectListItem
                    {
                        Value = x.PutovanjaId.ToString(),
                        Text = x.NazivPutovanja
                    }).ToList()
                };



                return View("Dodaj", input);

            }


            Uplate k;
            if (input.UplataId == 0)
            {
                k = new Uplate();
                db.Add(k);

            }
            else
            {
                k = db.Uplate.Find(input.UplataId);

            }


            k.Iznos = input.Iznos;
            k.DatumUplate = input.DatumUplate;
            k.KlijentId = input.KlijentId;
            k.PutovanjaID = input.PutovanjaId;

            db.SaveChanges();
            //if (input.UplataId == 0)
            //    ViewData["poruka-success"] = "Uspjesno ste dodali uplatu";
            //else
            //    ViewData["poruka-success"] = "Uspjesno ste izmijenili podatke uplate";



            return RedirectToAction(nameof(Prikazi));
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Uredi(int UplataID)
        {

            Uplate k = db.Uplate.Find(UplataID);
            if (k == null)
            {

                return RedirectToAction(nameof(Prikazi));
            }

            UplateUrediVM model = new UplateUrediVM
            {
                klijenti = db.Klijent.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime + "/ " + x.Prezime
                }).ToList(),
                putovanja = db.Putovanja.Select(x => new SelectListItem
                {
                    Value = x.PutovanjaId.ToString(),
                    Text = x.NazivPutovanja
                }).ToList()
            };






            model.KlijentId = k.KlijentId;
            model.PutovanjaId = k.PutovanjaID;

            model.Iznos = k.Iznos;
            model.DatumUplate = k.DatumUplate;



            return View("Uredi", model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult Obrisi(int UplataID)
        {
            Uplate
                k = db.Uplate.Find(UplataID);
            if (k == null)
            {


            }
            else
            {
                db.Remove(k);

                db.SaveChanges();


            }
            return RedirectToAction(nameof(Prikazi));
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult PretragaPutovanje()
        {
            UplataPutovanjaPretragaVM model = new UplataPutovanjaPretragaVM()
            {
                Putovanja = db.Putovanja.Select(a => new SelectListItem
                {
                    Value = a.PutovanjaId.ToString(),
                    Text = a.NazivPutovanja
                }).ToList()
            };
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult PrikazUplataPoPutovanju(UplataPutovanjaPretragaVM input)
        {
            UplatePrikaziVM model = new UplatePrikaziVM()
            {
                BrojUplata = db.Uplate.Where(x => x.PutovanjaID == input.PutovanjeID).Count(),
                UkupnoUplaceno = db.Uplate.Where(x => x.PutovanjaID == input.PutovanjeID).Sum(s => s.Iznos),
                SvrhaUplate = db.Putovanja.Where(x => x.PutovanjaId == input.PutovanjeID).FirstOrDefault().NazivPutovanja,
                lista = db.Uplate.Where(x => x.PutovanjaID == input.PutovanjeID).
                Select(a => new UplatePrikaziVM.Row
                {
                    UplataId = a.UplataId,
                    DatumUplate = a.DatumUplate,
                    Iznos = a.Iznos,
                    Klijent = a.Klijent.Ime + " " + a.Klijent.Prezime

                }).ToList()
            };

            return PartialView(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult PretragaKlijent()
        {
            UplataKorisnikPretragaVM model = new UplataKorisnikPretragaVM()
            {
                Klijenti = db.Klijent.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Ime + " " + a.Prezime+" "+a.JMBG
                }).ToList()
            };
            return View(model);
        }
        [Autorizacija(zaposlenik: true, putnikkorisnik: false)]

        public IActionResult PrikazUplataPoKlijentu(UplataKorisnikPretragaVM input)
        {
            UplatePrikaziVM model = new UplatePrikaziVM()
            {
                BrojUplata = db.Uplate.Where(x => x.KlijentId == input.KlijentID).Count(),
                UkupnoUplaceno = db.Uplate.Where(x => x.KlijentId == input.KlijentID).Sum(x => x.Iznos),
                lista = db.Uplate.Where(x => x.KlijentId == input.KlijentID).
                Select(a => new UplatePrikaziVM.Row
                {
                    UplataId = a.UplataId,

                    DatumUplate = a.DatumUplate,
                    Iznos = a.Iznos,
                    Klijent = a.Klijent.Ime + " " + a.Klijent.Prezime,
                    Putovanja=a.Putovanja.NazivPutovanja
                }).ToList()
            };
            return PartialView(model);
        }
        

    }
}