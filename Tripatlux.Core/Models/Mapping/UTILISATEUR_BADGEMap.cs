using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.Mapping
{
    public class UTILISATEUR_BADGEMap : EntityTypeConfiguration<UTILISATEUR_BADGE>
    {
        public UTILISATEUR_BADGEMap()
        {
            HasKey(p => new { p.ID_UTILISATEUR, p.ID_BADGE });

            HasRequired(p => p.TYPE_BADGE).WithMany(p => p.UTILISATEUR_BADGEs).HasForeignKey(p => p.ID_BADGE);
            HasRequired(p => p.UTILISATEUR).WithMany(p => p.UTILISATEUR_BADGEs).HasForeignKey(p => p.ID_UTILISATEUR);

            ToTable("UTILISATEUR_BADGE", "trip");
        }
    }
}
