using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;
using TuristickaAgencija.ViewModel;

namespace TuristickaAgencija.Repository
{
   public interface IPutovanjaRepository
    {
        List<Putovanja> GetPutovanja();
    
    }
}
