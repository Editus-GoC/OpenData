using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class ARRETMap : EntityTypeConfiguration<ARRET>
    {
        public ARRETMap()
        {
            HasKey(p => p.ID);

            ToTable("ARRET", "transport_public");
        }
    }
}
