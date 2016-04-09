using System;
using System.Linq;
using System.Collections.Generic;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;
using Tripatlux.Models.DataUser;
using Tripatlux.Core.Bll.Operation;

namespace Tripatlux.Models.DataTrip
{
    public class Trip : Item
    {
        public Guid Id { get; set; }
        public User Driver { get; set; }
        public Car Car { get; set; }
        public TypeBagage TypeBagage { get; set; }
        public short NbPassagers { get; set; }
        public bool IsValid { get; set; }
        public bool IsFinish { get; set; }
        public float DefaultCost { get; set; }
        public bool RoundTrip { get; set; }
        public TimeSpan Duration { get; set; }
        public override DateTime DateStart { get; set; }
        public bool Recurrence { get; set; }
        public string Comment { get; set; }

        public Step FirstStep => Steps?.Single(i => i.Type == TypeStep.Start);
        public Step LastStep => Steps?.Single(i => i.Type == TypeStep.End);

        public List<Step> Steps { get; set; }
        public List<Caracteristic> Caracteristics { get; set; }
        public List<Passager> Passagers { get; set; }

        public DateTime DateEnd => DateStart + Duration;
        public int NbFreePlace => NbPassagers - Passagers.Count;

        public string DurationStr
        {
            get
            {
                if (Duration.Hours == 0)
                    return Duration.Minutes + "min";
                return $"{(Duration.Hours < 10 ? "0" : "")}{Duration.Hours}h{(Duration.Minutes < 10 ? "0" : "")}{Duration.Minutes}";
            }
        }

        public Trip()
        {
            Steps = new List<Step>();
            Caracteristics = new List<Caracteristic>();
            Passagers = new List<Passager>();
        }

        public int GetKm(Location adrStart, Location adrEnd)
        {
            var stepstart = Container.Manager.EtapeOperation.GetByCoordinates(Id, (double)adrStart.CoordX, (double)adrEnd.CoordY, true);
            var stepend = Container.Manager.EtapeOperation.GetByCoordinates(Id, (double)adrStart.CoordX, (double)adrEnd.CoordY, false);
            var res = Container.Manager.VoyageGuidageOperation.GetKm(stepstart, stepend);
            return res;
        }

        public Trip Load(Guid id)
        {
            return Load(Container.Manager.VoyageOperation.GetById(id));
        }

        public Trip Load(VOYAGE data)
        {
            Id = data.ID;
            Driver = new User().Load(data.ID_UTILISATEUR_CONDUCTEUR);
            Car = new Car().Load(data.ID_VEHICULE);
            TypeBagage = TypeBagage.GetEnum<TypeBagage>(data.ID_VEHICULE);
            NbPassagers = data.NOMBRE_DE_PASSAGER;
            IsValid = data.EST_VALIDE;
            IsFinish = data.EST_TERMINE;
            DefaultCost = data.COUT_AU_KM;
            RoundTrip = data.RETOUR_PRIS_EN_CHARGE;
            Duration = data.TEMPS_PREVU;
            DateStart = data.HEURE_DEPART;
            Recurrence = data.RECURENCE ?? false;
            Comment = data.COMMENTAIRE;

            var dataSteps = Container.Manager.EtapeOperation.GetByTrip(Id);
            foreach (var item in dataSteps)
                Steps.Add(new Step().Load(item));

            var dataCara = Container.Manager.VoyageCaracteristiqueOperation.GetByTrip(Id);
            foreach (var item in dataCara)
                Caracteristics.Add(new Caracteristic().Load(item));

            var dataPass = Container.Manager.VoyagePassagerOperation.GetByTrip(Id);
            foreach (var item in dataPass)
                Passagers.Add(new Passager().Load(item));

            return this;
                
        }

        public SaveResult Save()
        {
            try
            {
                var trip = new VOYAGE();
                trip.ID_UTILISATEUR_CONDUCTEUR = Driver.Id;
                trip.ID_VEHICULE = new Guid("5B6FE2AA-D329-4388-B93F-0EF754E23C17"); //Forcé pour ce Hackathon
                trip.ID_TYPE_BAGAGE = TypeBagage.GetGuid().Value;
                trip.NOMBRE_DE_PASSAGER = NbPassagers;
                trip.COUT_AU_KM = DefaultCost;
                trip.RETOUR_PRIS_EN_CHARGE = RoundTrip;
                trip.TEMPS_PREVU = Duration;
                trip.HEURE_DEPART = DateStart;
                trip.HEURE_RETOUR = DateEnd == DateTime.MinValue ? (DateTime?)null : DateEnd;
                trip.RECURENCE = Recurrence;
                trip.EST_VALIDE = true;
                trip.COMMENTAIRE = Comment;
                trip = Container.Manager.VoyageOperation.Add(trip);
                Container.Manager.Save();
                Id = trip.ID;

               /* foreach (var item in Caracteristics)
                {
                    var res = item.Save(this);
                    if (!res.IsOk)
                        throw res.Exception;
                }*/ //Pas le temps for this

                foreach (var item in Steps)
                {
                    var res = item.Save(this);
                    if (!res.IsOk)
                        throw res.Exception;
                }
                return new SaveResult(true, Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Trip.Save()");
                return new SaveResult(ex);
            }
        }

        public SaveResult AddPassager(Passager item)
        {
            return item.Save(this);
        }
    }
}