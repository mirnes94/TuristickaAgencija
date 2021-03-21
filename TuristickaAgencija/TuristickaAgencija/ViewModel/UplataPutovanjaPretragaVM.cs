using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.ViewModel
{
    public class UplataPutovanjaPretragaVM
    {
        public int PutovanjeID { get; set; }
        public List<SelectListItem> Putovanja { get; set; }
    }
}
