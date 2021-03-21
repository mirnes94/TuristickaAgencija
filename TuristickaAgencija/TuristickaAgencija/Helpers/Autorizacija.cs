using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using TuristickaAgencija.Helper;
using TuristickaAgencija.EF;

namespace TuristickaAgencija.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool zaposlenik, bool putnikkorisnik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { zaposlenik, putnikkorisnik };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool zaposlenik, bool putnicikorisnici)
        {
            _zaposlenik = zaposlenik;
            _putnicikorisnici = putnicikorisnici;
        }
        private readonly bool _zaposlenik;
        private readonly bool _putnicikorisnici;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalozi k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Login", "Autentifikacija", new { @area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            MojContext db = filterContext.HttpContext.RequestServices.GetService<MojContext>();

            //zaposlenici mogu pristupiti 
            if (_zaposlenik && db.Zaposlenici.Any(s => s.KorisnickiNalogId == k.KorisnickiNalogId))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //putnicikorisnici mogu pristupiti studenti

            if (_putnicikorisnici && db.PutnikKorisnik.Any(s => s.KorisnickiNalogId == k.KorisnickiNalogId))
            {
                await next();//ok - ima pravo pristupa
                return;
            }
            if (db.PutnikKorisnik.All(x => x.KorisnickiNalogId != k.KorisnickiNalogId) && db.Zaposlenici.Any(x => x.KorisnickiNalogId != k.KorisnickiNalogId))
            {
                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
                return;
            }
            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}