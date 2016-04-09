using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class VOYAGEMap : EntityTypeConfiguration<VOYAGE>
    {
        public VOYAGEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("VOYAGE", "trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ID_UTILISATEUR_CONDUCTEUR).HasColumnName("ID_UTILISATEUR_CONDUCTEUR");
            this.Property(t => t.ID_VEHICULE).HasColumnName("ID_VEHICULE");
            this.Property(t => t.ID_TYPE_BAGAGE).HasColumnName("ID_TYPE_BAGAGE");
            this.Property(t => t.NOMBRE_DE_PASSAGER).HasColumnName("NOMBRE_DE_PASSAGER");
            this.Property(t => t.COMMENTAIRE).HasColumnName("COMMENTAIRE");
            this.Property(t => t.EST_VALIDE).HasColumnName("EST_VALIDE");
            this.Property(t => t.EST_TERMINE).HasColumnName("EST_TERMINE");
            this.Property(t => t.COUT_AU_KM).HasColumnName("COUT_AU_KM");
            this.Property(t => t.RETOUR_PRIS_EN_CHARGE).HasColumnName("RETOUR_PRIS_EN_CHARGE");
            this.Property(t => t.TEMPS_PREVU).HasColumnName("TEMPS_PREVU");
            this.Property(t => t.HEURE_DEPART).HasColumnName("HEURE_DEPART");

            // Relationships
            this.HasRequired(t => t.UTILISATEUR)
                .WithMany(t => t.VOYAGEs)
                .HasForeignKey(d => d.ID_UTILISATEUR_CONDUCTEUR);
            this.HasRequired(t => t.VEHICULE)
                .WithMany(t => t.VOYAGEs)
                .HasForeignKey(d => d.ID_VEHICULE);
            this.HasRequired(t => t.TYPE_BAGAGE)
                .WithMany(t => t.VOYAGEs)
                .HasForeignKey(d => d.ID_TYPE_BAGAGE);

        }
    }
}
