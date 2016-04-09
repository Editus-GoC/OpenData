using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataBus
{
    public class Parcours 
    {
        public Guid Id { get; set; }
        public LineOfBus Line { get; set; }
        public string Reference { get; set; }
        public BusStop BusStopStart { get; set; }
        public BusStop BusStopEnd { get; set; }

        public Parcours() { }

        public Parcours Load(Guid id)
        {
            return Load(Container.TPManager.TransportPublicParcoursOperation.GetById(id));
        }

        public Parcours Load(PARCOURS data)
        {
            Id = data.ID;
            Line = new LineOfBus().Load(data.ID_LIGNE);
            BusStopStart = new BusStop().Load(data.ID_ARRET_DEPART);
            BusStopEnd = new BusStop().Load(data.ID_ARRET_TERMINUS);
            return this;
        }
    }
}