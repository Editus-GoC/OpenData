using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;

namespace Tripatlux.Core.Bll.Operation
{
    public class VoyageGuidageOperation : BaseOperation<VOYAGE_GUIDAGE>
    {
        public VoyageGuidageOperation(TRIP_AT_LUXContext context) : base(context) { }

        public void Add(VOYAGE voyage, string donnee)
        {
            var parcoursVoyage = JsonConvert.DeserializeObject<Result>(donnee);

            int index = 1;
            int indexVoyage = 1;
            var voyageEtape = new EtapeOperation(_context);
            foreach (Legs legs in parcoursVoyage.Routes.First().listeLegs)
            {
                var etapeDe = voyageEtape.GetAll().SingleOrDefault(p => p.ID_VOYAGE == voyage.ID && p.ORDRE == index);
                var etapeA = voyageEtape.GetAll().SingleOrDefault(p => p.ID_VOYAGE == voyage.ID && p.ORDRE == index + 1);
                if (etapeA == null)
                    etapeA = voyageEtape.GetAll().SingleOrDefault(p => p.ID_VOYAGE == voyage.ID && p.ORDRE == 99999);

                int indexStep = 1;
                foreach (var step in legs.ListeSteps)
                {
                    _context.VOYAGE_GUIDAGE.Add(new VOYAGE_GUIDAGE()
                    {
                        ID_VOYAGE_ETAPE_DE = etapeDe.ID,
                        ID_VOYAGE_ETAPE_A = etapeA.ID,
                        ORDRE_ETAPE = indexStep,
                        ORDRE_VOYAGE = indexVoyage,
                        COORD_X = step.StartLocation.CoordX,
                        COORD_Y = step.StartLocation.CoordY,
                        DURATION = step.Duration.Text,
                        DURATION_SEC = step.Duration.DureeEnSec,
                        DIRECTION = step.Instructions,
                        DISTANCE = step.Distance.Text,
                        DISTANCE_M = step.Distance.DureeEnSec
                    });
                    indexStep++;
                    indexVoyage++;
                }
                index++;
            }
        }

        public int GetKm(VOYAGE_ETAPE De, VOYAGE_ETAPE A)
        {
            var guidageDe = GetAll().Where(l => l.ID_VOYAGE_ETAPE_DE == De.ID).OrderBy(p => p.ORDRE_VOYAGE).First();
            var guidateA = GetAll().Where(l => l.ID_VOYAGE_ETAPE_A == A.ID).OrderByDescending(p => p.ORDRE_VOYAGE).First();

            //return (GetAll().Where(l => l.ORDRE_VOYAGE >= guidageDe.ORDRE_VOYAGE && l.ORDRE_VOYAGE <= guidateA.ORDRE_VOYAGE)
            //                .Sum(l => l.DISTANCE_M)) / 1000;

            var voyage = De.VOYAGE;
            return (from guidage in _context.VOYAGE_GUIDAGE
                    join etape in _context.VOYAGE_ETAPE on guidage.ID_VOYAGE_ETAPE_DE equals etape.ID
                    where etape.ID_VOYAGE == voyage.ID && guidage.ORDRE_VOYAGE >= guidageDe.ORDRE_VOYAGE && guidage.ORDRE_VOYAGE <= guidateA.ORDRE_VOYAGE
                    select new { Guidage = guidage, Etape = etape }).Sum(g => g.Guidage.DISTANCE_M) / 1000;
        }

        public int GetKm(VOYAGE voyage)
        {
            return (from guidage in _context.VOYAGE_GUIDAGE
                     join etape in _context.VOYAGE_ETAPE on guidage.ID_VOYAGE_ETAPE_DE equals etape.ID
                     where etape.ID_VOYAGE == voyage.ID
                     select new { Guidage = guidage, Etape = etape }).Sum(g => g.Guidage.DISTANCE_M) / 1000;
        }
    }

    public class Result
    {
        public List<Routes> Routes { get; set; }
    }


    [JsonObject(Title = "routes")]
    public class Routes
    {
        [JsonProperty(PropertyName = "legs")]
        public List<Legs> listeLegs { get; set; }

        public string copyrights { get; set; }

    }

    [JsonObject(Title = "legs")]
    public class Legs
    {
        [JsonProperty(PropertyName = "end_address")]
        public string EndAddress { get; set; }

        [JsonProperty(PropertyName = "end_location")]
        public Location EndLocation { get; set; }

        [JsonProperty(PropertyName = "start_address")]
        public string StartAddress { get; set; }

        [JsonProperty(PropertyName = "start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty(PropertyName = "steps")]
        public List<Steps> ListeSteps { get; set; }
    }

    [JsonObject(Title = "steps")]
    public class Steps
    {
        [JsonProperty(PropertyName = "start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public Duration Duration { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public Duration Distance { get; set; }

        [JsonProperty(PropertyName = "html_instructions")]
        public string Instructions { get; set; }
    }

    [JsonObject(Title = "start_location")]
    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public decimal CoordX { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public decimal CoordY { get; set; }
    }

    [JsonObject(Title = "duration")]
    public class Duration
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "value")]
        public int DureeEnSec { get; set; }
    }
}
