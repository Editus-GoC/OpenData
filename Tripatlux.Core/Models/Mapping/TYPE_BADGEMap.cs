using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.Mapping
{
    public class TYPE_BADGEMap : EntityTypeConfiguration<TYPE_BADGE>
    {
        public TYPE_BADGEMap()
        {
            HasKey(p => p.ID);

            ToTable("TYPE_BADGE", "nomenclature");
        }
    }
}
