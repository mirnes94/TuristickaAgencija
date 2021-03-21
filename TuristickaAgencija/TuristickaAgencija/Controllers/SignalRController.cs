using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.EF;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.Repository;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Controllers
{
    public class SignalRController : Controller
    {
      
        private readonly IPutovanjaRepository _repository;
        private MojContext _context;
        public SignalRController(IPutovanjaRepository repository,MojContext context)
        {
          
            _repository = repository;
            _context = context;
        }
       
        public IActionResult Index(int id)
        {
            PutnikKorisnik pk = _context.PutnikKorisnik.Where(s => s.KorisnickiNalogId == id).FirstOrDefault();
           
            return View(pk);
          
        }
        [HttpGet]
        public IActionResult GetPutovanja()
        {
           
            return Ok(_repository.GetPutovanja());
        }

       

    }
}
