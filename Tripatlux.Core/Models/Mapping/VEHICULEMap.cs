using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class VEHICULEMap : EntityTypeConfiguration<VEHICULE>
    {
        public VEHICULEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.COULEUR)
                .HasMaxLength(50);

            this.Property(t => t.PLAQUE_IMMATRICULATION)
                .HasMaxLength(50);

            this.Property(t => t.MARQUE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.COMMENTAIRE)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("VEHICULE", "trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ID_UTILISATEUR).HasColumnName("ID_UTILISATEUR");
            this.Property(t => t.ID_TYPE_VEHICULE).HasColumnName("ID_TYPE_VEHICULE");
            this.Property(t => t.COULEUR).HasColumnName("COULEUR");
            this.Property(t => t.PLAQUE_IMMATRICULATION).HasColumnName("PLAQUE_IMMATRICULATION");
            this.Property(t => t.MARQUE).HasColumnName("MARQUE");
            this.Property(t => t.COMMENTAIRE).HasColumnName("COMMENTAIRE");
            this.Property(t => t.NBRE_MAX_PASSAGER).HasColumnName("NBRE_MAX_PASSAGER");

            // Relationships
            this.HasRequired(t => t.UTILISATEUR)
                .WithMany(t => t.VEHICULEs)
                .HasForeignKey(d => d.ID_UTILISATEUR);
            this.HasRequired(t => t.TYPE_VEHICULE)
                .WithMany(t => t.VEHICULEs)
                .HasForeignKey(d => d.ID_TYPE_VEHICULE);

        }
    }
}
