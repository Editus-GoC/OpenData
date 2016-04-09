using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class VOYAGE_CARACTERISTIQUEMap : EntityTypeConfiguration<VOYAGE_CARACTERISTIQUE>
    {
        public VOYAGE_CARACTERISTIQUEMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID_VOYAGE, t.ID_CARACTERISTIQUE });

            // Properties
            this.Property(t => t.COMMENTAIRE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("VOYAGE_CARACTERISTIQUE", "trip");
            this.Property(t => t.ID_VOYAGE).HasColumnName("ID_VOYAGE");
            this.Property(t => t.ID_CARACTERISTIQUE).HasColumnName("ID_CARACTERISTIQUE");
            this.Property(t => t.COMMENTAIRE).HasColumnName("COMMENTAIRE");
            this.Property(t => t.ID_UTILISATEUR).HasColumnName("ID_UTILISATEUR");

            // Relationships
            this.HasOptional(t => t.UTILISATEUR)
                .WithMany(t => t.VOYAGE_CARACTERISTIQUE)
                .HasForeignKey(d => d.ID_UTILISATEUR);
            this.HasRequired(t => t.VOYAGE)
                .WithMany(t => t.VOYAGE_CARACTERISTIQUE)
                .HasForeignKey(d => d.ID_VOYAGE);
            this.HasRequired(t => t.CARACTERISTIQUE_VOYAGE)
                .WithMany(t => t.VOYAGE_CARACTERISTIQUE)
                .HasForeignKey(d => d.ID_CARACTERISTIQUE);

        }
    }
}
