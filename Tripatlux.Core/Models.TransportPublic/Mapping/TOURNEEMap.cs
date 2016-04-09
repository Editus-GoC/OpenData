using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic.Mapping
{
    public class TOURNEEMap : EntityTypeConfiguration<TOURNEE>
    {
        public TOURNEEMap()
        {
            HasKey(p => p.ID);

            

            ToTable("TOURNEE", "transport_public");
        }
    }
}
