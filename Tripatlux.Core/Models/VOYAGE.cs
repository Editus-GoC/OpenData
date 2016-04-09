using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class VOYAGE : BaseEntity
    {
        public VOYAGE()
        {
            this.VOYAGE_CARACTERISTIQUE = new List<VOYAGE_CARACTERISTIQUE>();
            this.VOYAGE_ETAPE = new List<VOYAGE_ETAPE>();
            this.VOYAGE_PASSAGER = new List<VOYAGE_PASSAGER>();
        }

        public System.Guid ID_UTILISATEUR_CONDUCTEUR { get; set; }
        public System.Guid ID_VEHICULE { get; set; }
        public System.Guid ID_TYPE_BAGAGE { get; set; }
        public short NOMBRE_DE_PASSAGER { get; set; }
        public string COMMENTAIRE { get; set; }
        public bool EST_VALIDE { get; set; }
        public bool EST_TERMINE { get; set; }
        public float COUT_AU_KM { get; set; }
        public bool RETOUR_PRIS_EN_CHARGE { get; set; }
        public bool? RECURENCE { get; set; }
        public System.TimeSpan TEMPS_PREVU { get; set; }
        public DateTime HEURE_DEPART { get; set; }
        public DateTime? HEURE_RETOUR { get; set; }
        public int? ID_TIERS { get; set; }
        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual VEHICULE VEHICULE { get; set; }
        public virtual ICollection<VOYAGE_CARACTERISTIQUE> VOYAGE_CARACTERISTIQUE { get; set; }
        public virtual ICollection<VOYAGE_ETAPE> VOYAGE_ETAPE { get; set; }
        public virtual ICollection<VOYAGE_PASSAGER> VOYAGE_PASSAGER { get; set; }
        public virtual TYPE_BAGAGE TYPE_BAGAGE { get; set; }
    }
}
