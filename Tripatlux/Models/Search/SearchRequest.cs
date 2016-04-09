using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Business;
using Tripatlux.Core.Bll.Operation;
using Tripatlux.Helpers;

namespace Tripatlux.Models.Search
{
    public class SearchRequest
    {
        public string StartAddress { get; set; }

        public string EndAddress { get; set; }

        public DateTime Date_Submit { get; set; }

        private Location _locationStartAddress;
        public Location LocationStartAddress
        {
            get
            {
                if (_locationStartAddress == null)
                {
                    var res = Container.RechercheManager.RechercheOperation.GetCoordinates(StartAddress);
                    _locationStartAddress = new Location() { CoordY = (decimal)res.Item2, CoordX = (decimal)res.Item1 };
                }
                return _locationStartAddress;
            }
        }

        private Location _locationEndAddress;
        public Location LocationEndAddress
        {
            get
            {
                if (_locationEndAddress == null)
                {
                    var res = Container.RechercheManager.RechercheOperation.GetCoordinates(EndAddress);
                    _locationEndAddress = new Location() { CoordY = (decimal)res.Item2, CoordX = (decimal)res.Item1 };
                }
                return _locationEndAddress;
            }
        }

        private string _cityStart;
        public string CityStart
        {
            get
            {
                if (_cityStart == null)
                    _cityStart = GoogleApi.GetCity((double)LocationStartAddress.CoordY, (double)LocationStartAddress.CoordX);
                return _cityStart;
            }
        }

        private string _cityEnd;
        public string CityEnd
        {
            get
            {
                if (_cityEnd == null)
                    _cityEnd = GoogleApi.GetCity((double)LocationEndAddress.CoordY, (double)LocationEndAddress.CoordX);
                return _cityEnd;
            }
        }


    }
}