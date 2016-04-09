using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class VEHICULE : BaseEntity
    {
        public VEHICULE()
        {
            this.VOYAGEs = new List<VOYAGE>();
        }

        public Guid ID_UTILISATEUR { get; set; }
        public Guid ID_TYPE_VEHICULE { get; set; }
        public string COULEUR { get; set; }
        public string PLAQUE_IMMATRICULATION { get; set; }
        public string MARQUE { get; set; }
        public string COMMENTAIRE { get; set; }
        public Nullable<short> NBRE_MAX_PASSAGER { get; set; }
        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual TYPE_VEHICULE TYPE_VEHICULE { get; set; }
        public virtual ICollection<VOYAGE> VOYAGEs { get; set; }
    }
}
