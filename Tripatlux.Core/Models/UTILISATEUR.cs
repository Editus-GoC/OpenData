using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class UTILISATEUR : BaseEntity
    {
        public UTILISATEUR()
        {
            this.UTILISATEUR_FAVORI = new List<UTILISATEUR_FAVORI>();
            this.VEHICULEs = new List<VEHICULE>();
            this.VOYAGE_CARACTERISTIQUE = new List<VOYAGE_CARACTERISTIQUE>();
            this.VOYAGE_PASSAGER = new List<VOYAGE_PASSAGER>();
            this.VOYAGEs = new List<VOYAGE>();
        }

        public string EMAIL_LOGIN { get; set; }
        public string MOT_DE_PASSE { get; set; }
        public string EMAIL_NOTIFICATION { get; set; }
        public string SMS_NOTIFICATION { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public System.DateTime CREATION { get; set; }
        public Nullable<System.DateTime> DERNIERE_CONNEXION { get; set; }
        public bool EST_VALIDE { get; set; }
        public Nullable<System.DateTime> VALIDATION_LE { get; set; }
        public byte[] PHOTO { get; set; }
        public DateTime? DATE_DE_NAISSANCE { get; set; }
        public string ID_ASP_NET_USER { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<UTILISATEUR_FAVORI> UTILISATEUR_FAVORI { get; set; }
        public virtual ICollection<VEHICULE> VEHICULEs { get; set; }
        public virtual ICollection<VOYAGE_CARACTERISTIQUE> VOYAGE_CARACTERISTIQUE { get; set; }
        public virtual ICollection<VOYAGE_PASSAGER> VOYAGE_PASSAGER { get; set; }
        public virtual ICollection<VOYAGE> VOYAGEs { get; set; }
        public virtual ICollection<UTILISATEUR_BADGE> UTILISATEUR_BADGEs { get; set; }
    }
}
