using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nexmo.Api;
using TuristickaAgencija.ViewModel;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Helper;
using TuristickaAgencija.EF;
using Microsoft.EntityFrameworkCore;

namespace TuristickaAgencija.Controllers
{
    public class SmsGatewayController : Controller
    {
        private MojContext _db;
        public SmsGatewayController(MojContext db)
        {
            _db = db;
        }

        public IActionResult PosaljiPoruku()
        {
            PosaljiPorukuVM model = new PosaljiPorukuVM();
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var korisnik = _db.PutnikKorisnik.Where(pk => pk.KorisnickiNalogId == logiranikorisnik.KorisnickiNalogId).Include(p => p.Korisnik).FirstOrDefault().Korisnik.Ime + " " + _db.PutnikKorisnik.Where(pk => pk.KorisnickiNalogId == logiranikorisnik.KorisnickiNalogId).Include(p => p.Korisnik).FirstOrDefault().Korisnik.Prezime;
            model.PrimalacPoruke = Configuration.Instance.Settings["appsettings:NEXMO_TO"];
            model.PosiljalacPoruke = korisnik;
            return View(model);
        }
  
        [HttpPost]
        public IActionResult Potvrdi(PosaljiPorukuVM poruka)
        {
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = Configuration.Instance.Settings["appsettings:Nexmo.api_key"],
                ApiSecret = Configuration.Instance.Settings["appsettings:Nexmo.api_secret"]
            });
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var korisnik = _db.PutnikKorisnik.Where(pk => pk.KorisnickiNalogId == logiranikorisnik.KorisnickiNalogId).Include(p => p.Korisnik).FirstOrDefault().Korisnik.Ime + " " + _db.PutnikKorisnik.Where(pk => pk.KorisnickiNalogId == logiranikorisnik.KorisnickiNalogId).Include(p => p.Korisnik).FirstOrDefault().Korisnik.Prezime;
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from =korisnik,
                to = poruka.PrimalacPoruke,
                text = poruka.SadrzajPoruke
            });
            return Redirect("/KlijentHome/Index");
        }


    }
}
