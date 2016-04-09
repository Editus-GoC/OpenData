using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class PARCOURSMap : EntityTypeConfiguration<PARCOURS>
    {
        public PARCOURSMap()
        {
            HasKey(p => p.ID);

            HasRequired(p => p.ARRET_DEPART).WithMany(p => p.PARCOURS_DEPART).HasForeignKey(p => p.ID_ARRET_DEPART);
            HasRequired(p => p.ARRET_TERMINUS).WithMany(p => p.PARCOURS_TERMINUS).HasForeignKey(p => p.ID_ARRET_TERMINUS);

            ToTable("PARCOURS", "transport_public");
        }
    }
}
