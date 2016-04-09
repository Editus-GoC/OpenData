using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Tripatlux.Core.Models;
using Tripatlux.Core.Models.Mapping;

namespace Tripatlux.Core
{
    public partial class TRIP_AT_LUXContext : DbContext
    {
        static TRIP_AT_LUXContext()
        {
            Database.SetInitializer<TRIP_AT_LUXContext>(null);
        }

        public TRIP_AT_LUXContext()
            : base("Name=TRIP_AT_LUXContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<UTILISATEUR> UTILISATEURs { get; set; }
        public DbSet<UTILISATEUR_FAVORI> UTILISATEUR_FAVORI { get; set; }
        public DbSet<VEHICULE> VEHICULEs { get; set; }
        public DbSet<VOYAGE> VOYAGEs { get; set; }
        public DbSet<VOYAGE_CARACTERISTIQUE> VOYAGE_CARACTERISTIQUE { get; set; }
        public DbSet<VOYAGE_ETAPE> VOYAGE_ETAPE { get; set; }
        public DbSet<VOYAGE_PASSAGER> VOYAGE_PASSAGER { get; set; }
        public DbSet<CARACTERISTIQUE_VOYAGE> CARACTERISTIQUE_VOYAGE { get; set; }
        public DbSet<TYPE_BAGAGE> TYPE_BAGAGE { get; set; }
        public DbSet<TYPE_ETAPE> TYPE_ETAPE { get; set; }
        public DbSet<TYPE_VEHICULE> TYPE_VEHICULE { get; set; }
        public DbSet<VOYAGE_GUIDAGE> VOYAGE_GUIDAGE { get; set; }
        public DbSet<TYPE_BADGE> TYPE_BADGEs { get; set; }
        public DbSet<UTILISATEUR_BADGE> UTILISATEUR_BADGEs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UTILISATEURMap());
            modelBuilder.Configurations.Add(new UTILISATEUR_FAVORIMap());
            modelBuilder.Configurations.Add(new VEHICULEMap());
            modelBuilder.Configurations.Add(new VOYAGEMap());
            modelBuilder.Configurations.Add(new VOYAGE_CARACTERISTIQUEMap());
            modelBuilder.Configurations.Add(new VOYAGE_ETAPEMap());
            modelBuilder.Configurations.Add(new VOYAGE_PASSAGERMap());
            modelBuilder.Configurations.Add(new CARACTERISTIQUE_VOYAGEMap());
            modelBuilder.Configurations.Add(new TYPE_BAGAGEMap());
            modelBuilder.Configurations.Add(new TYPE_ETAPEMap());
            modelBuilder.Configurations.Add(new TYPE_VEHICULEMap());
            modelBuilder.Configurations.Add(new VOYAGE_GUIDAGEMap());
            modelBuilder.Configurations.Add(new UTILISATEUR_BADGEMap());
            modelBuilder.Configurations.Add(new TYPE_BADGEMap());
        }
    }
}
