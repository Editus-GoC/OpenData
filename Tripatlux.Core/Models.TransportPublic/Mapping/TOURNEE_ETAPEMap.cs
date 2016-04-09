using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class TOURNEE_ETAPEMap : EntityTypeConfiguration<TOURNEE_ETAPE>
    {
        public TOURNEE_ETAPEMap()
        {
            HasKey(p => new { p.ID_TOURNEE, p.ID_PARCOURS_ETAPE });

            HasRequired(p => p.PARCOURS_ETAPE).WithMany(p => p.TOURNEE_ETAPE).HasForeignKey(p => p.ID_PARCOURS_ETAPE);
            HasRequired(p => p.TOURNEE).WithMany(p => p.TOURNEE_ETAPE).HasForeignKey(p => p.ID_TOURNEE);

            ToTable("TOURNEE_ETAPE", "transport_public");
        }
    }
}
