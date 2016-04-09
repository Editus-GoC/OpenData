using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Core.Models.TransportPublic.Mapping;

namespace Tripatlux.Core
{
    public class TRANSPORT_PUBLICContext : DbContext
    {
        public TRANSPORT_PUBLICContext()
            : base("Name=TRIP_AT_LUXContext")
        {
        }

        public virtual DbSet<ARRET> ARRETs { get; set; }
        public virtual DbSet<LIGNE> LIGNEs { get; set; }
        public virtual DbSet<PARCOURS> PARCOURS { get; set; }
        public virtual DbSet<PARCOURS_ETAPE> PARCOURS_ETAPEs { get; set; }
        public virtual DbSet<TOURNEE> TOURNEEs { get; set; }
        public virtual DbSet<TOURNEE_ETAPE> TOURNEE_ETAPEs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ARRETMap());
            modelBuilder.Configurations.Add(new LIGNEMap());
            modelBuilder.Configurations.Add(new PARCOURSMap());
            modelBuilder.Configurations.Add(new PARCOURS_ETAPEMap());
            modelBuilder.Configurations.Add(new TOURNEEMap());
            modelBuilder.Configurations.Add(new TOURNEE_ETAPEMap());
        }
    }
}
