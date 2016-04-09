using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class PARCOURS_ETAPEMap : EntityTypeConfiguration<PARCOURS_ETAPE>
    {
        public PARCOURS_ETAPEMap()
        {
            HasKey(p => p.ID);

            HasRequired(p => p.ARRET).WithMany(p => p.PARCOURS_ETAPE).HasForeignKey(p => p.ID_ARRET);
            HasRequired(p => p.PARCOURS).WithMany(p => p.PARCOURS_ETAPE).HasForeignKey(p => p.ID_PARCOURS);

            ToTable("PARCOURS_ETAPE", "transport_public");
        }
    }
}
