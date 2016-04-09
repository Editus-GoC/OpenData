using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Helpers;
using Tripatlux.Models.DataTrip;

namespace Tripatlux.Models.ViewModels
{
    public class PaiementViewModel
    {
        public Guid IdTrip { get; set; }
        public int NbPlace { get; set; }

        public Trip Trip => new Trip().Load(IdTrip);

        public bool PayByPaypal(float cost)
        {
            //SIMU SUCCESS
            var success = true;
            if (success)
            {
                var res = Trip.AddPassager(new Passager(SessionHelper.CurrentUser, cost));
                if (res.IsOk)
                {
                    //SendMail();
                    SendSms();
                }
            }
            return success;
        }

        private bool SendMail()
        {
            if (string.IsNullOrEmpty(SessionHelper.CurrentUser.EmailNotif))
                return true;
            var message = "Paiement accepter, en attente de validation par l'autre con";
            var subject = "Validation de paiement";
            return new MailMessage(SessionHelper.CurrentUser.EmailNotif, subject, message).Send();
        }

        private bool SendSms()
        {
            try
            {
                if (string.IsNullOrEmpty(SessionHelper.CurrentUser.SmsNotif))
                    return true;
                var message = "Paiement accepté, le conducteur dois encore acceter votre réservation.";
                Container.SmsManager.SmsOperation.SendSms(SessionHelper.CurrentUser.SmsNotif, message);

                message = "Un passager souaite monter à bord, veuillez valider sont inscriptions à partir de votre compte.";
                Container.SmsManager.SmsOperation.SendSms(Trip.Driver.SmsNotif, message);
                return true;
            }
           catch (Exception ex)
            {
                Logger.Error(ex, "SendSms()");
                return false;
            }
        }
    }
}