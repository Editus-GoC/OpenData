using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class VoyageOperation : BaseOperation<VOYAGE>
    {
        private VoyageCaracteristiqueOperation _voyageCaracteristiqueOperation;
        
        public VoyageOperation(TRIP_AT_LUXContext context) : base(context) { }

        public VoyageCaracteristiqueOperation VoyageCaracteristiqueOperation
        {
            get
            {
                if (_voyageCaracteristiqueOperation == null)
                    _voyageCaracteristiqueOperation = new VoyageCaracteristiqueOperation(_context);
                return _voyageCaracteristiqueOperation;
            }
        }

        public List<VOYAGE> GetVoyagePlusProche(double coordXDe, double coordYDe, double coordXA, double coordYA, int rayonM)
        {
            List<VOYAGE> returnedList = new List<VOYAGE>();
            SqlParameter pCoordXDe = new SqlParameter("@coord_x_de", coordXDe);
            SqlParameter pCoordYDe = new SqlParameter("@coord_y_de", coordYDe);
            SqlParameter pCoordXA = new SqlParameter("@coord_x_a", coordXA);
            SqlParameter pCoordYA = new SqlParameter("@coord_y_a", coordYA);
            SqlParameter pRayon = new SqlParameter("@rayon", rayonM);
            var test = _context.Database.SqlQuery<Sp_GET_VOYAGE_PLUS_PROCHEResult>("trip.Sp_GET_VOYAGE_PLUS_PROCHE @coord_x_de, @coord_y_de, @coord_x_a, @coord_y_a, @rayon", pCoordXDe, pCoordYDe, pCoordXA, pCoordYA, pRayon);
            if (test != null)
                test.ToList().ForEach(l => returnedList.Add(GetById(l.ID_VOYAGE)));

            return returnedList;
        }
    }
}
