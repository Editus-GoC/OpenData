using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class UTILISATEUR_FAVORIMap : EntityTypeConfiguration<UTILISATEUR_FAVORI>
    {
        public UTILISATEUR_FAVORIMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.NOM)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PAYS)
                .HasMaxLength(50);

            this.Property(t => t.CODE_POSTAL)
                .HasMaxLength(50);

            this.Property(t => t.VILLE)
                .HasMaxLength(50);

            this.Property(t => t.RUE)
                .HasMaxLength(50);

            this.Property(t => t.NUMERO_RUE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UTILISATEUR_FAVORI", "trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.NOM).HasColumnName("NOM");
            this.Property(t => t.ID_UTILISATEUR).HasColumnName("ID_UTILISATEUR");
            this.Property(t => t.ID_TYPE_ETAPE).HasColumnName("ID_TYPE_ETAPE");
            this.Property(t => t.PAYS).HasColumnName("PAYS");
            this.Property(t => t.CODE_POSTAL).HasColumnName("CODE_POSTAL");
            this.Property(t => t.VILLE).HasColumnName("VILLE");
            this.Property(t => t.RUE).HasColumnName("RUE");
            this.Property(t => t.NUMERO_RUE).HasColumnName("NUMERO_RUE");

            // Relationships
            this.HasRequired(t => t.UTILISATEUR)
                .WithMany(t => t.UTILISATEUR_FAVORI)
                .HasForeignKey(d => d.ID_UTILISATEUR);
            this.HasOptional(t => t.TYPE_ETAPE)
                .WithMany(t => t.UTILISATEUR_FAVORI)
                .HasForeignKey(d => d.ID_TYPE_ETAPE);

        }
    }
}
