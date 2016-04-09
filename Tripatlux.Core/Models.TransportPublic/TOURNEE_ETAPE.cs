using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class TOURNEE_ETAPE
    {
        public Guid ID_TOURNEE { get; set; }
        public Guid ID_PARCOURS_ETAPE { get; set; }
        public string HEURE { get; set; }
        public System.TimeSpan HEURE_REEL { get; set; }

        public virtual TOURNEE TOURNEE { get; set; }
        public virtual PARCOURS_ETAPE PARCOURS_ETAPE { get; set; }
    }
}
