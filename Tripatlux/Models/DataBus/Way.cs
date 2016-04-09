using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataBus
{
    public class Way : Item
    {
        public Guid Id { get; set; }
        public override DateTime DateStart { get { return DateTime.Today.Add(StepStart.Hours); } set { } }

        public WayStep StepStart => WaySteps.First();
        public WayStep StepEnd => WaySteps.Last();

        public string Duration
        {
            get
            {
                var time = (StepEnd.Hours - StepStart.Hours);
                if (time.Hours == 0)
                    return time.Minutes + "min";
                return $"{(time.Hours < 10 ? "0" : "")}{time.Hours}h{(time.Minutes < 10 ? "0" : "")}{time.Minutes}";
            }
        }

        public List<WayStep> WaySteps { get; private set; }
            
        public Way()
        {
            WaySteps = new List<WayStep>();
        }

        public void SetSteps(IEnumerable<WayStep> steps)
        {
            WaySteps = steps.OrderBy(i => i.Step.Order).ToList();
        }

        public Way Load(Guid id)
        {
            return Load(Container.TPManager.TransportPublicTourneeOperation.GetById(id));
        }

        public Way Load(TOURNEE data)
        {
            Id = data.ID;
            SetSteps(Container.TPManager.TransportPublicTourneeEtapeOperation.GetByWay(Id).Select(i => new WayStep().Load(i)));
            return this;
        }

        public Way Load(IEnumerable<TOURNEE_ETAPE> steps)
        {
            Id = steps.First().ID_TOURNEE;
            SetSteps(steps.Select(i => new WayStep().Load(i)));
            return this;
        }
    }
}