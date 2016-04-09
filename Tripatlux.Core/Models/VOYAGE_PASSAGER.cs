using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class VOYAGE_PASSAGER
    {
        public System.Guid ID_VOYAGE { get; set; }
        public System.Guid ID_UTILISATEUR { get; set; }
        public Nullable<short> NOTE_CONDUCTEUR { get; set; }
        public Nullable<short> NOTE_PASSAGER_PAR_CONDUCTEUR { get; set; }
        public string COMMENTAIRE_CONDUCTEUR { get; set; }
        public string COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR { get; set; }
        public string TOKEN_PAIEMENT { get; set; }
        public bool PAIEMENT_EFFECTUE { get; set; }
        public float COUT_A_PAYER { get; set; }
        public bool? VALIDATION { get; set; }
        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual VOYAGE VOYAGE { get; set; }
    }
}
