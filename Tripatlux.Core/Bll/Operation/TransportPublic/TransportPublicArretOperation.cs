using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class TransportPublicArretOperation : BaseOperationTransportPublic<ARRET>
    {
        public TransportPublicArretOperation(TRANSPORT_PUBLICContext context) : base(context) { }

        public override ARRET GetByName(string name)
        {
            return GetAll().SingleOrDefault(a => a.NOM == name);
        }

        public ARRET GetByReference(string reference)
        {
            return GetAll().Single(a => a.REFERENCE == reference);
        }

        public void AddRange(IList<ARRET> list)
        {
            _context.ARRETs.AddRange(list);
        }

        public List<ARRET> GetArretPlusProche(double coordX, double coordY)
        {
            List<ARRET> returnedList = new List<ARRET>();
            SqlParameter pCoordX = new SqlParameter("@coord_x", coordX);
            SqlParameter pCoordY = new SqlParameter("@coord_y", coordY);
            var test = _context.Database.SqlQuery<Sp_GET_ARRET_PLUS_PROCHEResult>("transport_public.Sp_GET_ARRET_PLUS_PROCHE @coord_x, @coord_y", pCoordX, pCoordY);
            if (test != null)
                test.ToList().ForEach(arret => returnedList.Add(GetById(arret.ID)));
            //return GetById(test.ID);

            return returnedList;
        }
    }
}
