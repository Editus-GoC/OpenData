using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class UtilisateurOperation : BaseOperation<UTILISATEUR>
    {
        public UtilisateurOperation(TRIP_AT_LUXContext context) : base(context) { }

        public int GetNbTripFinished(Guid idUser)
        {
            var request = (from v in _context.VOYAGEs
                           where v.EST_TERMINE && v.ID_UTILISATEUR_CONDUCTEUR == idUser
                           select v.ID).Distinct().Count();
            return request;
        }

        public double GetNote(Guid idUser)
        {
            var noteDriver = (from v in _context.VOYAGEs
                              join p in _context.VOYAGE_PASSAGER on v.ID equals p.ID_VOYAGE
                              where v.ID_UTILISATEUR_CONDUCTEUR == idUser && p.NOTE_CONDUCTEUR != null
                              select p.NOTE_CONDUCTEUR);

            var notePassager = (from p in _context.VOYAGE_PASSAGER
                                where p.ID_UTILISATEUR == idUser && p.NOTE_PASSAGER_PAR_CONDUCTEUR != null
                                select p.NOTE_PASSAGER_PAR_CONDUCTEUR);

            var res = notePassager.Union(noteDriver).Average(i => i);
            return res ?? 0;
        }

        public IEnumerable<Tuple<Guid, string>> GetAvis(Guid idUser)
        {
            var dico = new List<Tuple<Guid, string>>();

            var dataDriver = (from v in _context.VOYAGEs
                              join p in _context.VOYAGE_PASSAGER on v.ID equals p.ID_VOYAGE
                              where v.ID_UTILISATEUR_CONDUCTEUR == idUser
                              select new { idUti = p.ID_UTILISATEUR, p.COMMENTAIRE_CONDUCTEUR });

            var dataPassager = (from v in _context.VOYAGEs
                                join p in _context.VOYAGE_PASSAGER on v.ID equals p.ID_VOYAGE
                                where p.ID_UTILISATEUR == idUser
                                select new { idUti = v.ID_UTILISATEUR_CONDUCTEUR, p.COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR });

            foreach (var item in dataDriver)
                dico.Add(new Tuple<Guid, string>(item.idUti, item.COMMENTAIRE_CONDUCTEUR));

            foreach (var item in dataDriver)
                dico.Add(new Tuple<Guid, string>(item.idUti, item.COMMENTAIRE_CONDUCTEUR));
            return dico;
        }
    }
}
