using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class VoyageCaracteristiqueOperation : BaseOperation<VOYAGE_CARACTERISTIQUE>
    {
        public VoyageCaracteristiqueOperation(TRIP_AT_LUXContext context) : base(context) { }

        public IEnumerable<VOYAGE_CARACTERISTIQUE> GetByTrip(Guid idTrip)
        {
            return GetAll().Where(i => i.ID_VOYAGE == idTrip);
        }
    }
}
