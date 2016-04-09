using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;
using Tripatlux.Models.DataUser;

namespace Tripatlux.Models.DataTrip
{
    public class Passager
    {
        public User User { get; set; }
        public float Cost { get; set; }
        public string PayToken { get; set; }
        public bool PaySend { get; set; }
        public short? NotePassager { get; set; }
        public short NoteDriver { get; set; }
        public string CommentPassager { get; set; }
        public string CommentDriver { get; set; }
        public bool? Validation { get; set; }

        public Passager() { }

        public Passager(User user, float cost)
        {
            User = user;
            Cost = cost;
        }

        public Passager Load(Guid id)
        {
            return Load(Container.Manager.VoyagePassagerOperation.GetById(id));
        }

        public Passager Load(VOYAGE_PASSAGER pass)
        {
            User = new User().Load(pass.ID_UTILISATEUR);
            Cost = pass.COUT_A_PAYER;
            PayToken = pass.TOKEN_PAIEMENT;
            PaySend = pass.PAIEMENT_EFFECTUE;
            CommentPassager = pass.COMMENTAIRE_CONDUCTEUR;
            CommentDriver = pass.COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR;
            NotePassager = pass.NOTE_PASSAGER_PAR_CONDUCTEUR;
            Validation = pass.VALIDATION;
            return this;
        }

        internal SaveResult Save(Trip trip)
        {
            try
            {
                var data = new VOYAGE_PASSAGER();
                data.ID_UTILISATEUR = User.Id;
                data.ID_VOYAGE = trip.Id;
                data.COUT_A_PAYER = Cost;
                data.TOKEN_PAIEMENT = GenerateToken();
                data.PAIEMENT_EFFECTUE = PaySend;
                data.COMMENTAIRE_CONDUCTEUR = CommentDriver;
                data.COMMENTAIRE_PASSAGER_PAR_CONDUCTEUR = CommentPassager;
                data.NOTE_PASSAGER_PAR_CONDUCTEUR = NotePassager;
                data.NOTE_CONDUCTEUR = NoteDriver;
                data.VALIDATION = false;
                Container.Manager.VoyagePassagerOperation.Add(data);

                Container.Manager.Save();
                return new SaveResult(true);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Passager.Save()");
                return new SaveResult(ex);
            }
        }

        internal SaveResult PayExecute(Trip trip)
        {
            var data = Container.Manager.VoyagePassagerOperation.GetAll().Single(i => i.ID_UTILISATEUR == User.Id && i.ID_VOYAGE == trip.Id);
            data.PAIEMENT_EFFECTUE = PaySend;
            Container.Manager.Save();
            return new SaveResult(true);
        }

        public void Valid(bool valid, Trip trip)
        {
            var pass = Container.Manager.VoyagePassagerOperation.GetAll().Single(i => i.ID_VOYAGE == trip.Id && i.ID_UTILISATEUR == User.Id);
            pass.VALIDATION = valid;
            Container.Manager.Save();

            var message = "Le conducteur de votre trajet à valider votre inscription. Merci à vous";
            Container.SmsManager.SmsOperation.SendSms(User.SmsNotif, message);

        }

        private static string GenerateToken()
        {
            var random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(8);
            for (int i = 0; i < 8; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString().ToUpper();
        }
    }
}