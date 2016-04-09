using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Core.Tool;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicLigneOperation : BaseOperationTransportPublic<LIGNE>
    {
        public TransportPublicLigneOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public LIGNE GetByNumber(int number)
        {
            return GetAll().SingleOrDefault(l => l.NUMERO == number);
        }

        public List<LIGNE> Search(ARRET depart, ARRET arrivee, DateTime horaire)
        {
            TransportPublicParcoursEtapeOperation parcoursEtape = new TransportPublicParcoursEtapeOperation(_context);
            IQueryable<PARCOURS_ETAPE> testDepart = null;

            var dateMax = horaire.AddDays(1);
            while (!testDepart.SafeAny() && horaire < dateMax)
            {
                var heure = $"{horaire.Hour.ToString("00")}{horaire.Minute.ToString("00")}";
                testDepart = parcoursEtape.GetAll().Where(etp => etp.ID_ARRET == depart.ID &&
                                                                    etp.TOURNEE_ETAPE.Any(te => te.HEURE == heure));

                horaire = horaire.AddMinutes(1);
            }
            if (testDepart != null)
            {
                var testArrivee = parcoursEtape.GetAll().Where(etp => etp.ID_ARRET == arrivee.ID);

                return testDepart.Where(etp => testArrivee.Any(etp2 => etp2.ID_PARCOURS == etp.ID_PARCOURS
                                                                    && etp2.ORDRE > etp.ORDRE))
                                 .Select(etp => etp.PARCOURS.LIGNE)
                                 .Distinct()
                                 .ToList();
            }
            return null;
        }

        public List<LIGNE> Search(ARRET depart, ARRET arrivee)
        {
            TransportPublicParcoursEtapeOperation parcoursEtape = new TransportPublicParcoursEtapeOperation(_context);
            IQueryable<PARCOURS_ETAPE> testDepart = null;


            testDepart = parcoursEtape.GetAll().Where(etp => etp.ID_ARRET == depart.ID);

            if (testDepart != null)
            {
                var testArrivee = parcoursEtape.GetAll().Where(etp => etp.ID_ARRET == arrivee.ID);

                return testDepart.Where(etp => testArrivee.Any(etp2 => etp2.ID_PARCOURS == etp.ID_PARCOURS
                                                                    && etp2.ORDRE > etp.ORDRE))
                                 .Select(etp => etp.PARCOURS.LIGNE)
                                 .Distinct()
                                 .ToList();
            }
            return null;
        }
    }
}
