using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataBus
{
    public class LineOfBus
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public LineOfBus Load(Guid id)
        {
            return Load(Container.TPManager.TransportPublicLigneOperation.GetById(id));
        }

        public LineOfBus Load(LIGNE data)
        {
            Id = data.ID;
            Name = data.NOM;
            Number = data.NUMERO;
            return this;
        }
    }
}