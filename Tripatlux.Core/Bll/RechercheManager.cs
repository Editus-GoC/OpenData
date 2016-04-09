using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Bll.Operation;

namespace Tripatlux.Core.Bll
{
    public class RechercheManager : IDisposable
    {
        private TRIP_AT_LUXContext _contextTrip = new TRIP_AT_LUXContext();
        private TRANSPORT_PUBLICContext _contextTransport = new TRANSPORT_PUBLICContext();

        private RechercheOperation _rechercheOperation;

        public RechercheOperation RechercheOperation
        {
            get
            {
                if (_rechercheOperation == null)
                    _rechercheOperation = new RechercheOperation(_contextTrip, _contextTransport);
                return _rechercheOperation;
            }
        }

        #region Diposable
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _contextTrip.Dispose();
                    _contextTransport.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
