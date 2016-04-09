using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class ARRET : BaseEntity
    {
        public string NOM { get; set; }
        public decimal COORD_X { get; set; }
        public decimal COORD_Y { get; set; }
        public string REFERENCE { get; set; }

        public virtual ICollection<PARCOURS_ETAPE> PARCOURS_ETAPE { get; set; }
        public virtual ICollection<PARCOURS> PARCOURS_DEPART { get; set; }
        public virtual ICollection<PARCOURS> PARCOURS_TERMINUS { get; set; }
    }
}
