using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class EtapeOperation : BaseOperation<VOYAGE_ETAPE>
    {
        public EtapeOperation(TRIP_AT_LUXContext context) : base(context) { }

        public IEnumerable<VOYAGE_ETAPE> GetByTrip(Guid idTrip)
        {
            return GetAll().Where(i => i.ID_VOYAGE == idTrip);
        }

        public VOYAGE_ETAPE GetByCoordinates(Guid idVoyage, double coordX, double coordY, bool depart)
        {
            SqlParameter pIdVoyage = new SqlParameter("@id_voyage", idVoyage);
            SqlParameter pCoordX = new SqlParameter("@coord_x", coordX);
            SqlParameter pCoordY = new SqlParameter("@coord_y", coordY);
            SqlParameter pDepart = new SqlParameter("@depart", depart);
            var test = _context.Database.SqlQuery<Sp_GET_VOYAGE_ETAPE_PLUS_PROCHEResult>("[trip].[Sp_GET_VOYAGE_ETAPE_PLUS_PROCHE] @id_voyage, @coord_x, @coord_y, @depart", pIdVoyage, pCoordX, pCoordY, pDepart).First();
            if (test != null)
                return GetById(test.ID);

            return null;
        }
    }
}
