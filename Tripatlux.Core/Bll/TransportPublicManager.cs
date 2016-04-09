using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Bll.Operation;

namespace Tripatlux.Core.Bll
{
    public class TransportPublicManager : IDisposable
    {
        private TRANSPORT_PUBLICContext _context = new TRANSPORT_PUBLICContext();

        private TransportPublicParcoursOperation _transportPublicParcoursOperation;
        private TransportPublicArretOperation _transportPublicArretOperation;
        private TransportPublicLigneOperation _transportPublicLigneOperation;
        private TransportPublicParcoursEtapeOperation _transportPublicParcoursEtapeOperation;
        private TransportPublicTourneeOperation _transportPublicTourneeOperation;
        private TransportPublicTourneeEtapeOperation _transportPublicTourneeEtapeOperation;

        public TransportPublicParcoursOperation TransportPublicParcoursOperation
        {
            get
            {
                if (_transportPublicParcoursOperation == null)
                    _transportPublicParcoursOperation = new TransportPublicParcoursOperation(_context);
                return _transportPublicParcoursOperation;
            }
        }
        public TransportPublicArretOperation TransportPublicArretOperation
        {
            get
            {
                if (_transportPublicArretOperation == null)
                    _transportPublicArretOperation = new TransportPublicArretOperation(_context);
                return _transportPublicArretOperation;
            }
        }
        public TransportPublicLigneOperation TransportPublicLigneOperation
        {
            get
            {
                if (_transportPublicLigneOperation == null)
                    _transportPublicLigneOperation = new TransportPublicLigneOperation(_context);
                return _transportPublicLigneOperation;
            }
        }
        public TransportPublicParcoursEtapeOperation TransportPublicParcoursEtapeOperation
        {
            get
            {
                if (_transportPublicParcoursEtapeOperation == null)
                    _transportPublicParcoursEtapeOperation = new TransportPublicParcoursEtapeOperation(_context);
                return _transportPublicParcoursEtapeOperation;
            }
        }
        public TransportPublicTourneeOperation TransportPublicTourneeOperation
        {
            get
            {
                if (_transportPublicTourneeOperation == null)
                    _transportPublicTourneeOperation = new TransportPublicTourneeOperation(_context);
                return _transportPublicTourneeOperation;
            }
        }
        public TransportPublicTourneeEtapeOperation TransportPublicTourneeEtapeOperation
        {
            get
            {
                if (_transportPublicTourneeEtapeOperation == null)
                    _transportPublicTourneeEtapeOperation = new TransportPublicTourneeEtapeOperation(_context);
                return _transportPublicTourneeEtapeOperation;
            }
        }

        public void Save()
        {
            using (var dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        public void SaveAsync()
        {
            using (var dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChangesAsync();
                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    throw;
                }
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
                    _context.Dispose();
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
