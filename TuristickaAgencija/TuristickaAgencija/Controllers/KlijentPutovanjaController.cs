using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Helper;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class KlijentPutovanjaController : Controller
    {
        private MojContext _db;

        public KlijentPutovanjaController(MojContext db)
        {
            _db = db;
        }
        [Autorizacija(zaposlenik: false, putnikkorisnik: true)]
        public IActionResult Index(int id)
        {
            PutnikKorisnik pk = _db.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
            List<PutovanjaPrikaziVM> putovanja = _db.Putovanja.Select(x => new PutovanjaPrikaziVM
            {
               KorisnikId=pk.KorisnikId,
               PutovanjaId=x.PutovanjaId,
               NazivPutovanja=x.NazivPutovanja,
               OpisPutovanja=x.OpisPutovanja,
               CijenaPutovanja=x.CijenaPutovanja,
               DatumPolaska=x.DatumPolaska.ToString("dd.MM.yyyy/HH:mm"),
               DatumDolaska=x.DatumDolaska.ToString("dd.MM.yyyy/HH:mm"),
               BrojPutnika=x.BrojPutnika,
               Prevoz=x.Prevoz.Firma.Naziv,
               TipPrevoza=x.Prevoz.TipPrevoza,
               BrojMjesta=x.Prevoz.BrojMjesta,
               CijenaPoMjestu=x.Prevoz.CijenaPoMjestu,
               Smjestaj=x.Smjestaj.NazivSmjestaja,
               OpisSmjestaja=x.Smjestaj.OpisSmjestaja,
               Grad=x.Smjestaj.Grad.NazivGrada,
               Drzava=x.Smjestaj.Grad.Drzava.Naziv,
               CijenaNoćenja=x.Smjestaj.CijenaNocenja,
               TipSobe=x.Smjestaj.Tip_sobe,
               Slika=x.Smjestaj.Slika
            }).ToList();
            return View(putovanja);
        }
    }
}
