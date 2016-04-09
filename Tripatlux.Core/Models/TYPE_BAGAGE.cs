using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class TYPE_BAGAGE : BaseEntity
    {
        public TYPE_BAGAGE()
        {
            this.VOYAGEs = new List<VOYAGE>();
        }

        public string TITRE { get; set; }
        public string DESCRIPTION { get; set; }
        public virtual ICollection<VOYAGE> VOYAGEs { get; set; }
    }
}
