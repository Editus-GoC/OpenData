using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class TYPE_BAGAGEMap : EntityTypeConfiguration<TYPE_BAGAGE>
    {
        public TYPE_BAGAGEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TITRE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("TYPE_BAGAGE", "nomenclature");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TITRE).HasColumnName("TITRE");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
        }
    }
}
