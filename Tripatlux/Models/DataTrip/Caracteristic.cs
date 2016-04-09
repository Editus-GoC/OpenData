using System;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;
using Tripatlux.Models.DataUser;

namespace Tripatlux.Models.DataTrip
{
    public class Caracteristic
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string Label { get; set; }
        public bool CanSelected { get; set; }

        public Caracteristic() { }

        public Caracteristic Load(Guid id)
        {
            return Load(Container.Manager.VoyageCaracteristiqueOperation.GetById(id));
        }

        public Caracteristic Load(VOYAGE_CARACTERISTIQUE cara)
        {
            var data = Container.Manager.NomenclatureCaracteristiqueVoyageOperation.GetById(cara.ID_CARACTERISTIQUE);

            Id = data.ID;
            Label = data.TITRE;
            CanSelected = data.SELECTION_UTILISATEUR;
            User = cara.ID_UTILISATEUR == null ? null : new User().Load(cara.ID_UTILISATEUR.Value);
            return this;
        }

        internal SaveResult Save(Trip trip)
        {
            try
            {
                var cara = new VOYAGE_CARACTERISTIQUE();
                cara.ID_UTILISATEUR = User.Id;
                cara.ID_VOYAGE = trip.Id;
                cara.ID_CARACTERISTIQUE = Id;
                Container.Manager.VoyageCaracteristiqueOperation.Add(cara);

                Container.Manager.Save();
                Id = cara.ID_CARACTERISTIQUE;
                return new SaveResult(true, Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Caracteristic.Save()");
                return new SaveResult(ex);
            }
        }
    }
}