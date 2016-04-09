using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class CARACTERISTIQUE_VOYAGE : BaseEntity
    {
        public CARACTERISTIQUE_VOYAGE()
        {
            this.VOYAGE_CARACTERISTIQUE = new List<VOYAGE_CARACTERISTIQUE>();
        }

        public string TITRE { get; set; }
        public bool SELECTION_UTILISATEUR { get; set; }
        public virtual ICollection<VOYAGE_CARACTERISTIQUE> VOYAGE_CARACTERISTIQUE { get; set; }
    }
}
