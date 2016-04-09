using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class VOYAGE_PASSAGERMap : EntityTypeConfiguration<VOYAGE_PASSAGER>
    {
        public VOYAGE_PASSAGERMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ID_VOYAGE, t.ID_UTILISATEUR });

            // Properties
            this.Property(t => t.TOKEN_PAIEMENT)
                .IsRequired()
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("VOYAGE_PASSAGER", "trip");
            this.Property(t => t.ID_VOYAGE).HasColumnName("ID_VOYAGE");
            this.Property(t => t.ID_UTILISATEUR).HasColumnName("ID_UTILISATEUR");
            this.Property(t => t.NOTE_CONDUCTEUR).HasColumnName("NOTE_CONDUCTEUR");
            this.Property(t => t.NOTE_PASSAGER_PAR_CONDUCTEUR).HasColumnName("NOTE_PASSAGER_PAR_CONDUCTEUR");
            this.Property(t => t.COMMENTAIRE_CONDUCTEUR).HasColumnName("COMMENTAIRE_CONDUCTEUR");
            this.Property(t => t.COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR).HasColumnName("COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR");
            this.Property(t => t.TOKEN_PAIEMENT).HasColumnName("TOKEN_PAIEMENT");
            this.Property(t => t.PAIEMENT_EFFECTUE).HasColumnName("PAIEMENT_EFFECTUE");
            this.Property(t => t.COUT_A_PAYER).HasColumnName("COUT_A_PAYER");

            // Relationships
            this.HasRequired(t => t.UTILISATEUR)
                .WithMany(t => t.VOYAGE_PASSAGER)
                .HasForeignKey(d => d.ID_UTILISATEUR);
            this.HasRequired(t => t.VOYAGE)
                .WithMany(t => t.VOYAGE_PASSAGER)
                .HasForeignKey(d => d.ID_VOYAGE);

        }
    }
}
