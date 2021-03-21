using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.ViewModel
{
    public class GradoviVM
    {
        public int Id { get; set; }

        public string Naziv { get; set; }




        [Required(ErrorMessage = "Država je obavezna")]
        public int? DrzavaId { get; set; }

        public GradoviVM() { }

        public GradoviVM(Gradovi model)
        {
            Id = model.Id;
            Naziv = model.NazivGrada;

            DrzavaId = model.DrzavaId;
        }

    }
}
