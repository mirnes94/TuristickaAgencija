using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{

    public class KlijentHomeController : Controller
    {
        private readonly MojContext _db;

        public KlijentHomeController(MojContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult UsloviPutovanja()
        {
            return View();
        }
       
    }
}
