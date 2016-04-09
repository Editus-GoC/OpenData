using Editus.Provider.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Bll;

namespace Tripatlux.Helpers
{
    public static class Container
    {
        private static TripAtLuxManager _manager;
        public static TripAtLuxManager Manager
        {
            get
            {
                if (_manager == null)
                    _manager = new TripAtLuxManager();
                return _manager;
            }
        }

        private static TransportPublicManager _TPManager;
        public static TransportPublicManager TPManager
        {
            get
            {
                if (_TPManager == null)
                    _TPManager = new TransportPublicManager();
                return _TPManager;
            }
        }

        private static SmsManager _smsManager;
        public static SmsManager SmsManager
        {
            get
            {
                if (_smsManager == null)
                    _smsManager = new SmsManager();
                return _smsManager;
            }
        }

        private static RechercheManager _rechercheManager;
        public static RechercheManager RechercheManager
        {
            get
            {
                if (_rechercheManager == null)
                    _rechercheManager = new RechercheManager();
                return _rechercheManager;
            }
        }
    }
}