using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tripatlux.Core.Bll;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;
using Tripatlux.Models;
using Tripatlux.Models.DataTrip;
using Tripatlux.Models.DataUser;

namespace Tripatlux.Models.ViewModels
{
    public class ReservationViewModel
    {
        public Trip Trip { get; set; }

        public float Price
        {
            get
            {
                return Container.RechercheManager.RechercheOperation.GetPrixVoyage(Trip.Id, Trip.FirstStep.Id, Trip.LastStep.Id);
            }
        }

        public SaveResult Save()
        {
           return Trip.Save();
        }
    }
}