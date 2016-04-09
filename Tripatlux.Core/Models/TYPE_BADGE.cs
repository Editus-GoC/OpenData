using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models
{
    public class TYPE_BADGE : BaseEntity
    {
        public string DESCRIPTION { get; set; }

        public ICollection<UTILISATEUR_BADGE> UTILISATEUR_BADGEs { get; set; }
    }
}
