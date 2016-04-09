using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class LIGNEMap : EntityTypeConfiguration<LIGNE>
    {
        public LIGNEMap()
        {
            HasKey(p => p.ID);

            HasMany(p => p.PARCOURS).WithRequired(p => p.LIGNE).HasForeignKey(p => p.ID_LIGNE);

            ToTable("LIGNE", "transport_public");
        }
    }
}
