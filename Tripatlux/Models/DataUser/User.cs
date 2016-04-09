using System;
using System.Linq;
using System.Collections.Generic;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;

namespace Tripatlux.Models.DataUser
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string EmailLogin { get; set; }
        public string EmailNotif { get; set; }
        public string SmsNotif { get; set; }
        public byte[] Avatar { get; set; }
        public DateTime DateBirthday { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime DateCreation { get; set; }
        //public List<Car> Cars { get; set; }

        public int NbTripDone => Container.Manager.UtilisateurOperation.GetNbTripFinished(Id);
        public double Note => Container.Manager.UtilisateurOperation.GetNote(Id);

        private IEnumerable<Avis> _avis;
        public IEnumerable<Avis> Avis => _avis == null ? _avis = GetAvis() : _avis;

        public User()
        {
            //Cars = new List<Car>();
        }



        public User Load(Guid id)
        {
            var data = Container.Manager.UtilisateurOperation.GetById(id);
            Id = data.ID;
            Name = data.NOM;
            FirstName = data.PRENOM;
            Password = data.MOT_DE_PASSE;
            EmailLogin = data.EMAIL_LOGIN;
            EmailNotif = data.EMAIL_NOTIFICATION;
            SmsNotif = data.SMS_NOTIFICATION;
            Avatar = data.PHOTO;
            LastConnection = data.DERNIERE_CONNEXION.Value;
            DateBirthday = data.DATE_DE_NAISSANCE.Value;
            DateCreation = data.CREATION;

            /*var carsData = Container.Manager.VehiculeOperation.GetByUser(id);
            foreach(var item in carsData)
            {
                Cars.Add(new Car().Load(item));
            }*/
            return this;
        }

        public SaveResult Save()
        {
            try
            {
                var user = new UTILISATEUR();
                bool isNew = true;
                if (Id != Guid.Empty)
                {
                    user = Container.Manager.UtilisateurOperation.GetById(Id);
                    isNew = false;
                }

                user.MOT_DE_PASSE = Password;
                user.EMAIL_NOTIFICATION = EmailNotif;
                user.SMS_NOTIFICATION = SmsNotif;
                user.PHOTO = Avatar;

                if (isNew)
                {
                    user.NOM = Name;
                    user.PRENOM = FirstName;
                    user.EMAIL_LOGIN = EmailLogin;
                    user.CREATION = DateTime.Now;
                   // user.DATE_NAISSANCE = DateBirthday;
                }

                if (isNew)
                    Container.Manager.UtilisateurOperation.Add(user);
                Container.Manager.Save();
                Id = user.ID;
                return new SaveResult(true, Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "User.Save()");
                return new SaveResult(ex);
            }
        }

        private IEnumerable<Avis> GetAvis()
        {
            return Container.Manager.UtilisateurOperation.GetAvis(Id).Select(i => new Avis(i.Item1, i.Item2));
        }
    }
}