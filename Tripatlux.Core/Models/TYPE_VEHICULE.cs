using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class TYPE_VEHICULE : BaseEntity
    {
        public TYPE_VEHICULE()
        {
            this.VEHICULEs = new List<VEHICULE>();
        }

        public string TITRE { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual ICollection<VEHICULE> VEHICULEs { get; set; }
    }
}
