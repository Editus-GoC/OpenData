using System;
using Tripatlux.Business;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataTrip
{
    public class Step
    {
        public Guid Id { get; set; }
        public TypeStep Type { get; set; }
        public string Address { get; set; }
        public int Order { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Step() { }

        public Step(string address, TypeStep type, int order)
        {
            Address = address;
            var coor = Container.RechercheManager.RechercheOperation.GetCoordinates(Address);
            Longitude = coor.Item1;
            Latitude = coor.Item2;
            Order = order;
            Type = type;
        }

        private string _city;
        public string GetCity()
        {
            return _city == null ? _city = GoogleApi.GetCity(Latitude, Longitude) : _city;
        }

        public Step Load(Guid id)
        {
            return Load(Container.Manager.EtapeOperation.GetById(id));
        }

        public Step Load(VOYAGE_ETAPE step)
        {
            Id = step.ID;
            Type = Type.GetEnum<TypeStep>(step.ID_TYPE_ETAPE);
            Address = step.ADRESSE;
            Order = step.ORDRE;
            Latitude = (double)step.COORD_Y;
            Longitude = (double)step.COORD_X;
            return this;
        }

        internal SaveResult Save(Trip trip)
        {
            try
            {
                var data = new VOYAGE_ETAPE();
                data.ID_VOYAGE = trip.Id;
                data.ID_TYPE_ETAPE = Type.GetGuid().Value;
                data.ADRESSE = Address;
                data.ORDRE = Order;
                data.COORD_Y = (decimal)Latitude;
                data.COORD_X = (decimal)Longitude;

                Container.Manager.EtapeOperation.Add(data);
                Container.Manager.Save();
                Id = data.ID;
                return new SaveResult(true, Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Passager.Save()");
                return new SaveResult(ex);
            }
        }
    }
}