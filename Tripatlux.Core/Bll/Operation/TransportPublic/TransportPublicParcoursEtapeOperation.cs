using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicParcoursEtapeOperation : BaseOperationTransportPublic<PARCOURS_ETAPE>
    {
        public TransportPublicParcoursEtapeOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public IEnumerable<PARCOURS_ETAPE> GetByParcours(PARCOURS parcours)
        {
            return GetAll().Where(pe => pe.ID_PARCOURS == parcours.ID);
        }

        public PARCOURS_ETAPE GetByParcoursArret(PARCOURS parcours, ARRET arret)
        {
            return GetAll().SingleOrDefault(pe => pe.ID_PARCOURS == parcours.ID && pe.ID_ARRET == arret.ID);
        }
    }
}
