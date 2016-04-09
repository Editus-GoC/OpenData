using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class NomenclatureTypeEtapeOperation : BaseOperation<TYPE_ETAPE>
    {
        public NomenclatureTypeEtapeOperation(TRIP_AT_LUXContext context) : base(context) { }
    }
}
