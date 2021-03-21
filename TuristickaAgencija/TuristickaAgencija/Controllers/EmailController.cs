using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.ViewModel;
using System.Net;
using System.Net.Mail;
using TuristickaAgencija.Helper;
using Microsoft.EntityFrameworkCore;
namespace TuristickaAgencija.Controllers
{
    public class EmailController : Controller
    {
        private readonly MojContext _db;

        public EmailController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            PosaljiEmailVM model = new PosaljiEmailVM();
            var logiranikorisnik = HttpContext.GetLogiraniKorisnik();
            var korisnik = _db.PutnikKorisnik.Where(pk => pk.KorisnickiNalogId == logiranikorisnik.KorisnickiNalogId).Include(p => p.Korisnik).FirstOrDefault().Korisnik.Kontakt;
            model.To = "mirnes.turkovic94@gmail.com";
            model.From = korisnik;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(PosaljiEmailVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                else
                {
                    MailMessage mm = new MailMessage(model.From, model.To);
                    mm.Subject = model.Subject;
                    mm.Body = model.Body;
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;


                    NetworkCredential nc = new NetworkCredential(model.From, model.Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;

                    smtp.Send(mm);
                }
                   
            }
            catch(Exception e)
            {
                ViewData["error"]=e.Message.ToString();
                model.From = "";
                model.Body = "";
                model.Subject = "";
                model.Password = "";
                return View();
            }
          
            return Redirect("/KlijentHome/Index");
        }

    }
}
