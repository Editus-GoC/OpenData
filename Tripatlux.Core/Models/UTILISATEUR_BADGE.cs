using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models
{
    public class UTILISATEUR_BADGE
    {
        public Guid ID_UTILISATEUR { get; set; }
        public Guid ID_BADGE { get; set; }

        public virtual UTILISATEUR UTILISATEUR { get; set; }
        public virtual TYPE_BADGE TYPE_BADGE { get; set; }
    }
}
