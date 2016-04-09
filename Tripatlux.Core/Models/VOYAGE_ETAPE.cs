using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class VOYAGE_ETAPE : BaseEntity
    {
        public System.Guid ID_VOYAGE { get; set; }
        public System.Guid ID_TYPE_ETAPE { get; set; }
        public string ADRESSE { get; set; }
        public int ORDRE { get; set; }
        public Nullable<decimal> COORD_X { get; set; }
        public Nullable<decimal> COORD_Y { get; set; }
        public virtual VOYAGE VOYAGE { get; set; }
        public virtual TYPE_ETAPE TYPE_ETAPE { get; set; }
    }
}
