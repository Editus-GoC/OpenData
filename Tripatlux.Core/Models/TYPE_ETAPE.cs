using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class TYPE_ETAPE : BaseEntity
    {
        public TYPE_ETAPE()
        {
            this.UTILISATEUR_FAVORI = new List<UTILISATEUR_FAVORI>();
            this.VOYAGE_ETAPE = new List<VOYAGE_ETAPE>();
        }

        public string DESCRIPTION { get; set; }
        public bool EST_OBLIGATOIRE { get; set; }
        public int ORDRE_DEFAUT { get; set; }
        public virtual ICollection<UTILISATEUR_FAVORI> UTILISATEUR_FAVORI { get; set; }
        public virtual ICollection<VOYAGE_ETAPE> VOYAGE_ETAPE { get; set; }
    }
}
