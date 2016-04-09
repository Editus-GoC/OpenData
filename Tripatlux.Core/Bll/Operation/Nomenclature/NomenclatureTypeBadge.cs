using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class NomenclatureTypeBadge : BaseOperation<TYPE_BADGE>
    {
        public NomenclatureTypeBadge(TRIP_AT_LUXContext context) : base(context) { }
    }
}
