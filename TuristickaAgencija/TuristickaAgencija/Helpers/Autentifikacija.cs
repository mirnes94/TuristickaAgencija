using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EF;
using TuristickaAgencija.Models;
using Microsoft.Extensions.DependencyInjection;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.Helper
{
    public static class Autentifikacija
    {
        public static readonly string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalozi korisnik, bool snimiUCookie = false)
        {

            MojContext db = context.RequestServices.GetService<MojContext>();

            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken != null)
            {
                AutorizacijskiToken obrisati = db.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == stariToken);
                if (obrisati != null)
                {
                    db.AutorizacijskiToken.Remove(obrisati);
                    db.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                db.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnickiNaloziId = korisnik.KorisnickiNalogId,
                    VrijemeEvidentiranja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(LogiraniKorisnik);
        }

        public static KorisnickiNalozi GetLogiraniKorisnik(this HttpContext context)
        {
            MojContext db = context.RequestServices.GetService<MojContext>();

            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (token == null)
                return null;

            return db.AutorizacijskiToken
                .Where(x => x.Vrijednost == token)
                .Select(s => s.KorisnickiNalozi)
                .SingleOrDefault();

        }

    }
}
