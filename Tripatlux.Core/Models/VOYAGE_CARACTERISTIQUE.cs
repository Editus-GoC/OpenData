using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class VOYAGE_CARACTERISTIQUE
    {
        public System.Guid ID_VOYAGE { get; set; }
        public System.Guid ID_CARACTERISTIQUE { get; set; }
        public string COMMENTAIRE { get; set; }
        public Nullable<System.Guid> ID_UTILISATEUR { get; set; }
        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual VOYAGE VOYAGE { get; set; }
        public virtual CARACTERISTIQUE_VOYAGE CARACTERISTIQUE_VOYAGE { get; set; }
    }
}
