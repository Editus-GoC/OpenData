using System;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataUser
{
    public class Car
    {
        public Guid Id { get; set; }
        public TypeCar TypeCar { get; set; }
        public string Color { get; set; }
        public string Immatriculation { get; set; }
        public string Marque { get; set; }
        public string Commentaire { get; set; }
        public int NbMaxPassager { get; set; }

        public Car() { }

        public Car Load(Guid id)
        {
            return Load(Container.Manager.VehiculeOperation.GetById(id));
        }

        public Car Load(VEHICULE data)
        {
            Id = data.ID;
            TypeCar = TypeCar.GetEnum<TypeCar>(data.ID_TYPE_VEHICULE);
            Color = data.COULEUR;
            Immatriculation = data.PLAQUE_IMMATRICULATION;
            Marque = data.MARQUE;
            Commentaire = data.COMMENTAIRE;
            NbMaxPassager = (int)data.NBRE_MAX_PASSAGER;
            return this;
        }
    }
}