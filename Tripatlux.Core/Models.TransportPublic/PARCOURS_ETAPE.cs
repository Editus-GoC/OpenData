using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class PARCOURS_ETAPE : BaseEntity
    {
        public Guid ID_PARCOURS { get; set; }
        public Guid ID_ARRET { get; set; }
        public short ORDRE { get; set; }

        public virtual PARCOURS PARCOURS { get; set; }
        public virtual ARRET ARRET { get; set; }

        public ICollection<TOURNEE_ETAPE> TOURNEE_ETAPE { get; set; }
    }
}
