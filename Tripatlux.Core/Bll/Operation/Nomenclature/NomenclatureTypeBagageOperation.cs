using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class NomenclatureTypeBagageOperation : BaseOperation<TYPE_BAGAGE>
    {
        public NomenclatureTypeBagageOperation(TRIP_AT_LUXContext context) : base(context) { }
    }
}
