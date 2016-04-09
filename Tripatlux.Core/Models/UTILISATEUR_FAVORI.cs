using System;
using System.Collections.Generic;

namespace Tripatlux.Core.Models
{
    public partial class UTILISATEUR_FAVORI : BaseEntity
    {
        public string NOM { get; set; }
        public System.Guid ID_UTILISATEUR { get; set; }
        public Nullable<System.Guid> ID_TYPE_ETAPE { get; set; }
        public string PAYS { get; set; }
        public string CODE_POSTAL { get; set; }
        public string VILLE { get; set; }
        public string RUE { get; set; }
        public string NUMERO_RUE { get; set; }
        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual TYPE_ETAPE TYPE_ETAPE { get; set; }
    }
}
