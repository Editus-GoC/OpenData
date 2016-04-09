using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicTourneeEtapeOperation : BaseOperationTransportPublic<TOURNEE_ETAPE>
    {
        public TransportPublicTourneeEtapeOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public IEnumerable<TOURNEE_ETAPE> GetByWay(Guid idWay)
        {
            return GetAll().Where(i => i.ID_TOURNEE == idWay);
        }

        public List<List<TOURNEE_ETAPE>> GetEtapes(LIGNE ligne, ARRET arretDepart, ARRET arretArrivee)
        {
            using (TRANSPORT_PUBLICContext context = new TRANSPORT_PUBLICContext())
            {
                List<List<TOURNEE_ETAPE>> returnedValue = new List<List<TOURNEE_ETAPE>>();
                var tourneeEtapes = context.TOURNEE_ETAPEs.Where(te => te.PARCOURS_ETAPE.PARCOURS.LIGNE.ID == ligne.ID &&
                                                                       te.PARCOURS_ETAPE.ARRET.ID == arretDepart.ID);

                foreach (var tourneeEtape in tourneeEtapes.OrderBy(p => p.HEURE_REEL))
                {
                    List<TOURNEE_ETAPE> listTourneeEtapes = new List<TOURNEE_ETAPE>();
                    var tourneeEtapeFin = context.TOURNEE_ETAPEs.SingleOrDefault(te => te.ID_TOURNEE == tourneeEtape.ID_TOURNEE && te.PARCOURS_ETAPE.ARRET.ID == arretArrivee.ID);

                    if (tourneeEtapeFin != null)
                    {
                        listTourneeEtapes.Add(tourneeEtape);
                        listTourneeEtapes.AddRange(context.TOURNEE_ETAPEs.Where(te => te.ID_TOURNEE == tourneeEtape.ID_TOURNEE &&
                                                                                      te.PARCOURS_ETAPE.ORDRE > tourneeEtape.PARCOURS_ETAPE.ORDRE &&
                                                                                      te.PARCOURS_ETAPE.ORDRE < tourneeEtapeFin.PARCOURS_ETAPE.ORDRE));
                        listTourneeEtapes.Add(tourneeEtapeFin);

                        returnedValue.Add(listTourneeEtapes);
                    }

                }
                return returnedValue; 
            }
        }
    }
}
