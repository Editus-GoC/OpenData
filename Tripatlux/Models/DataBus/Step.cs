using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataBus
{
    public class Step
    {
        public Guid Id { get; set; }
        public BusStop BusStop { get; set; }
        public int Order { get; set; }
        public Parcours Parcours { get; set; }

        public Step() { }

        public Step Load(Guid id)
        {
            return (Load(Container.TPManager.TransportPublicParcoursEtapeOperation.GetById(id)));
        }

        public Step Load(PARCOURS_ETAPE data)
        {
            Id = data.ID;
            BusStop = new BusStop().Load(data.ID_ARRET);
            Parcours = new Parcours().Load(data.ID_PARCOURS);
            Order = data.ORDRE;
            return this;
        }
    }
}