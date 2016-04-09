using System;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataBus
{
    public class BusStop
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public BusStop() { }

        public BusStop Load(Guid id)
        {
            return Load(Container.TPManager.TransportPublicArretOperation.GetById(id));
        }

        public BusStop Load(ARRET data)
        {
            Id = data.ID;
            Name = data.NOM;
            Latitude = data.COORD_Y;
            Longitude = data.COORD_X;
            return this;
        }
    }
}