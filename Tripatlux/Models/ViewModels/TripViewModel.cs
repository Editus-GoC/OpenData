using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Tripatlux.Business;
using Tripatlux.Core.Bll;
using Tripatlux.Core.Models;
using Tripatlux.Helpers;
using Tripatlux.Models;
using Tripatlux.Models.DataTrip;
using Tripatlux.Models.DataUser;

namespace Tripatlux.Models.ViewModels
{
    public class TripViewModel
    {
        public string AddressStart { get; set; }
        public string AddressEnd { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hours { get; set; }
        public DateTime HoursReturn { get; set; }
        public List<string> Steps { get; set; }
        public string RoundTrip { get; set; }
        public int Recurrence { get; set; }
        public int Duration { get; set; }
        public int IdTiers { get; set; }
        public Trip Trip { get; set; }

        public TripViewModel()
        {
            Date = DateTime.Today;
            Hours = DateTime.MinValue.AddHours(10);
            HoursReturn = DateTime.MinValue.AddHours(18);
            Trip = new Trip();
            Steps = new List<string>();
        }

        public SaveResult Save()
        {
            try
            {
                int count = 1;
                Trip.Steps.Add(new Step(AddressStart, TypeStep.Start, count++));
                if (Steps != null)
                {
                    foreach (var item in Steps)
                        Trip.Steps.Add(new Step(item, TypeStep.Step, count++));
                }
                Trip.Steps.Add(new Step(AddressEnd, TypeStep.End, count++));

                Trip.Driver = SessionHelper.CurrentUser;
                Trip.DateStart = Date.AddHours(Hours.Hour).AddMinutes(Hours.Minute);
                Trip.RoundTrip = RoundTrip == "on";
                Trip.Duration = new TimeSpan(0, 0, 0, Duration);
                var res = Trip.Save();
                var json = GoogleApi.GetGoogleDirection(AddressStart, AddressEnd, Steps);
                Container.Manager.VoyageGuidageOperation.Add(Container.Manager.VoyageOperation.GetById(res.Id), json);
                Container.Manager.Save();
                return new SaveResult(true, Trip.Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "TripViewModel.Save()");
                return new SaveResult(ex);
            }
        }
    }
}