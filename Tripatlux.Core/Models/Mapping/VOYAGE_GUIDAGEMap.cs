using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.Mapping
{
   public class VOYAGE_GUIDAGEMap : EntityTypeConfiguration<VOYAGE_GUIDAGE>
    {
        public VOYAGE_GUIDAGEMap()
        {
            HasKey(p => new { p.ID_VOYAGE_ETAPE_DE, p.ID_VOYAGE_ETAPE_A, p.ORDRE_ETAPE });

            ToTable("VOYAGE_GUIDAGE", "trip");
        }
    }
}
