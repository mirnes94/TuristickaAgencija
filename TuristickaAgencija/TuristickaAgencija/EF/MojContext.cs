using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.EntityModels;

namespace TuristickaAgencija.EF
{
    public class MojContext:DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
         : base(options)
        {
        }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Gradovi> Gradovi { get; set; }
        public DbSet<Smjestaj> Smjestaj { get; set; }

       
        public DbSet<Firma> Firma { get; set; }

        public DbSet<Prevoz> Prevoz { get; set; }
    
        public DbSet<Putovanja> Putovanja { get; set; }
        public DbSet<Uplate> Uplate { get; set; }
        public DbSet<Vodic> Vodic { get; set; }

        public DbSet<Obavijesti> Obavijesti { get; set; }
        public DbSet<Korisnik> Klijent { get; set; } 


        public DbSet<KorisnickiNalozi> KorisnickiNalozi { get; set; }
        public DbSet<Zaposlenici> Zaposlenici { get; set; }
        public DbSet<PutnikKorisnik> PutnikKorisnik { get; set; }
        public DbSet<AutorizacijskiToken> AutorizacijskiToken { get; set; }

        public DbSet<Rezervacija> Rezervacija { get; set; }


    }
}
