using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class TYPE_ETAPEMap : EntityTypeConfiguration<TYPE_ETAPE>
    {
        public TYPE_ETAPEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DESCRIPTION)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TYPE_ETAPE", "nomenclature");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.EST_OBLIGATOIRE).HasColumnName("EST_OBLIGATOIRE");
            this.Property(t => t.ORDRE_DEFAUT).HasColumnName("ORDRE_DEFAUT");
        }
    }
}
