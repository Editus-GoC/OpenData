using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class TYPE_VEHICULEMap : EntityTypeConfiguration<TYPE_VEHICULE>
    {
        public TYPE_VEHICULEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TITRE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("TYPE_VEHICULE", "nomenclature");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TITRE).HasColumnName("TITRE");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
        }
    }
}
