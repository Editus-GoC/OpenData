using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class CARACTERISTIQUE_VOYAGEMap : EntityTypeConfiguration<CARACTERISTIQUE_VOYAGE>
    {
        public CARACTERISTIQUE_VOYAGEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TITRE)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CARACTERISTIQUE_VOYAGE", "nomenclature");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TITRE).HasColumnName("TITRE");
            this.Property(t => t.SELECTION_UTILISATEUR).HasColumnName("SELECTION_UTILISATEUR");
        }
    }
}
