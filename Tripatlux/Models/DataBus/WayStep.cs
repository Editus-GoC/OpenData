using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Models.DataBus
{
    public class WayStep
    {
        public TimeSpan Hours { get; set; }
        public Step Step { get; set; }

        public string GetTimeStr()
        {
            return $"{(Hours.Hours < 10 ? "0" : "")}{Hours.Hours}h{(Hours.Minutes < 10 ? "0" : "")}{Hours.Minutes}";
        }

        public WayStep() { }

        public WayStep Load(TOURNEE_ETAPE step)
        {
            Hours = step.HEURE_REEL;
            Step = new Step().Load(step.ID_PARCOURS_ETAPE);
            return this;
        }
    }
}