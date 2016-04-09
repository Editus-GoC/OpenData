using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class PARCOURS : BaseEntity
    {
        public Guid ID_LIGNE { get; set; }
       
        public bool LUNDI { get; set; }
        public bool MARDI { get; set; }
        public bool MERCREDI { get; set; }
        public bool JEUDI { get; set; }
        public bool VENDREDI { get; set; }
        public bool SAMEDI { get; set; }
        public bool DIMANCHE { get; set; }
        public bool FERIE { get; set; }
        public string REFERENCE { get; set; }
        public Guid ID_ARRET_DEPART { get; set; }
        public Guid ID_ARRET_TERMINUS { get; set; }


        public virtual LIGNE LIGNE { get; set; }
       
        public virtual ICollection<PARCOURS_ETAPE> PARCOURS_ETAPE { get; set; }

        public virtual ARRET ARRET_DEPART { get; set; }
        public virtual ARRET ARRET_TERMINUS { get; set; }
    }
}
