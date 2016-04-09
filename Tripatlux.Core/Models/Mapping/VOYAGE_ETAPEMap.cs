using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class VOYAGE_ETAPEMap : EntityTypeConfiguration<VOYAGE_ETAPE>
    {
        public VOYAGE_ETAPEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ADRESSE)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("VOYAGE_ETAPE", "trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ID_VOYAGE).HasColumnName("ID_VOYAGE");
            this.Property(t => t.ID_TYPE_ETAPE).HasColumnName("ID_TYPE_ETAPE");
            this.Property(t => t.ADRESSE).HasColumnName("ADRESSE");
            this.Property(t => t.ORDRE).HasColumnName("ORDRE");
            this.Property(t => t.COORD_X).HasColumnName("COORD_X");
            this.Property(t => t.COORD_Y).HasColumnName("COORD_Y");

            // Relationships
            this.HasRequired(t => t.VOYAGE)
                .WithMany(t => t.VOYAGE_ETAPE)
                .HasForeignKey(d => d.ID_VOYAGE);
            this.HasRequired(t => t.TYPE_ETAPE)
                .WithMany(t => t.VOYAGE_ETAPE)
                .HasForeignKey(d => d.ID_TYPE_ETAPE);

        }
    }
}
