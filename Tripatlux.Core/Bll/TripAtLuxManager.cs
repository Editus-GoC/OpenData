using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Bll.Operation;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll
{
    public class TripAtLuxManager : IDisposable
    {
        private TRIP_AT_LUXContext _context = new TRIP_AT_LUXContext();

        #region Private Operations
        private UtilisateurOperation _utilisateurOperation;
        private UtilisateurFavoriOperation _utilisateurFavoriOperation;
        private VehiculeOperation _vehiculeOperation;
        private VoyageOperation _voyageOperation;
        private EtapeOperation _voyageEtape;
        private VoyagePassagerOperation _voyagePassager;
        private NomenclatureCaracteristiqueVoyageOperation _nomenclatureCaracteristiqueVoyageOperation;
        private NomenclatureTypeBagageOperation _nomenclatureTypeBagageOperation;
        private NomenclatureTypeEtapeOperation _nomenclatureTypeEtapeOperation;
        private NomenclatureTypeVehiculeOperation _nomenclatureTypeVehiculeOperation;
        private NomenclatureTypeBadge _nomenclatureTypeBadge;
        private VoyageCaracteristiqueOperation _voyageCaracteristiqueOperation;
        private VoyageGuidageOperation _voyageGuidageOperation;
        private UtilisateurBadgeOperation _utilisateurBadgeOperation;
        #endregion

        #region Accesseur Operations
        public UtilisateurOperation UtilisateurOperation
        {
            get
            {
                if (_utilisateurOperation == null)
                    _utilisateurOperation = new UtilisateurOperation(_context);
                return _utilisateurOperation;
            }
        }
        public UtilisateurFavoriOperation UtilisateurFavoriOperation
        {
            get
            {
                if (_utilisateurFavoriOperation == null)
                    _utilisateurFavoriOperation = new UtilisateurFavoriOperation(_context);
                return _utilisateurFavoriOperation;
            }
        }
        public VehiculeOperation VehiculeOperation
        {
            get
            {
                if (_vehiculeOperation == null)
                    _vehiculeOperation = new VehiculeOperation(_context);
                return _vehiculeOperation;
            }
        }
        public VoyageOperation VoyageOperation
        {
            get
            {
                if (_voyageOperation == null)
                    _voyageOperation = new VoyageOperation(_context);
                return _voyageOperation;
            }
        }
        public EtapeOperation EtapeOperation
        {
            get
            {
                if (_voyageEtape == null)
                    _voyageEtape = new EtapeOperation(_context);
                return _voyageEtape;
            }
        }
        public VoyagePassagerOperation VoyagePassagerOperation
        {
            get
            {
                if (_voyagePassager == null)
                    _voyagePassager = new VoyagePassagerOperation(_context);
                return _voyagePassager;
            }
        }
        public NomenclatureCaracteristiqueVoyageOperation NomenclatureCaracteristiqueVoyageOperation
        {
            get
            {
                if (_nomenclatureCaracteristiqueVoyageOperation == null)
                    _nomenclatureCaracteristiqueVoyageOperation = new NomenclatureCaracteristiqueVoyageOperation(_context);
                return _nomenclatureCaracteristiqueVoyageOperation;
            }
        }
        public NomenclatureTypeBagageOperation NomenclatureTypeBagageOperation
        {
            get
            {
                if (_nomenclatureTypeBagageOperation == null)
                    _nomenclatureTypeBagageOperation = new NomenclatureTypeBagageOperation(_context);
                return _nomenclatureTypeBagageOperation;
            }
        }
        public NomenclatureTypeEtapeOperation NomenclatureTypeEtapeOperation
        {
            get
            {
                if (_nomenclatureTypeEtapeOperation == null)
                    _nomenclatureTypeEtapeOperation = new NomenclatureTypeEtapeOperation(_context);
                return _nomenclatureTypeEtapeOperation;
            }
        }
        public NomenclatureTypeVehiculeOperation NomenclatureTypeVehiculeOperation
        {
            get
            {
                if (_nomenclatureTypeVehiculeOperation == null)
                    _nomenclatureTypeVehiculeOperation = new NomenclatureTypeVehiculeOperation(_context);
                return _nomenclatureTypeVehiculeOperation;
            }
        }
        public VoyageCaracteristiqueOperation VoyageCaracteristiqueOperation
        {
            get
            {
                if (_voyageCaracteristiqueOperation == null)
                    _voyageCaracteristiqueOperation = new VoyageCaracteristiqueOperation(_context);
                return _voyageCaracteristiqueOperation;
            }
        }
        public VoyageGuidageOperation VoyageGuidageOperation
        {
            get
            {
                if (_voyageGuidageOperation == null)
                    _voyageGuidageOperation = new VoyageGuidageOperation(_context);
                return _voyageGuidageOperation;
            }
        }
        public NomenclatureTypeBadge NomenclatureTypeBadge
        {
            get
            {
                if (_nomenclatureTypeBadge == null)
                    _nomenclatureTypeBadge = new NomenclatureTypeBadge(_context);
                return _nomenclatureTypeBadge;
            }
        }
        public UtilisateurBadgeOperation UtilisateurBadgeOperation
        {
            get
            {
                if (_utilisateurBadgeOperation == null)
                    _utilisateurBadgeOperation = new UtilisateurBadgeOperation(_context);
                return _utilisateurBadgeOperation;
            }
        }
        #endregion

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
