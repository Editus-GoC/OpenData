using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicTourneeOperation : BaseOperationTransportPublic<TOURNEE>
    {
        public TransportPublicTourneeOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public TOURNEE GetByReference(string reference)
        {
            return GetAll().SingleOrDefault(t => t.REFERENCE == reference);
        }
    }
}
