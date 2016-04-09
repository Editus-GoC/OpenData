using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class VehiculeOperation : BaseOperation<VEHICULE>
    {
        public VehiculeOperation(TRIP_AT_LUXContext context) : base(context) { }

        public IEnumerable<VEHICULE> GetByUser(Guid idUser)
        {
            return GetAll().Where(i => i.ID_UTILISATEUR == idUser);
        }
    }
}
