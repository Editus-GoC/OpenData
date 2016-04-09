using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicParcoursOperation : BaseOperationTransportPublic<PARCOURS>
    {
        public TransportPublicParcoursOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public PARCOURS GetByReference(string reference)
        {
            return GetAll().SingleOrDefault(p => p.REFERENCE == reference);
        }

        public PARCOURS GetByLigneEtArrets(LIGNE ligne, ARRET arretDepart, ARRET arretTerminus)
        {
            return GetAll().SingleOrDefault(p => p.ID_LIGNE == ligne.ID && p.ID_ARRET_DEPART == arretDepart.ID && p.ID_ARRET_TERMINUS == arretTerminus.ID);
        }
    }
}
