using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models.TransportPublic
{
    public class LIGNE : BaseEntity
    {
        public string NOM { get; set; }
        public short NUMERO { get; set; }

        public virtual ICollection<PARCOURS> PARCOURS { get; set; }
    }
}
