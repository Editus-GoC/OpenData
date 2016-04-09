using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class TOURNEE : BaseEntity
    {
        public bool LUNDI { get; set; }
        public bool MARDI { get; set; }
        public bool MERCREDI { get; set; }
        public bool JEUDI { get; set; }
        public bool VENDREDI { get; set; }
        public bool SAMEDI { get; set; }
        public bool DIMANCHE { get; set; }
        public bool FERIE { get; set; }
        public string REFERENCE { get; set; }

        public ICollection<TOURNEE_ETAPE> TOURNEE_ETAPE { get; set; }
    }
}
