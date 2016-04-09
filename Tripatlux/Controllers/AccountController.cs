using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Tripatlux.Models;
using Tripatlux.Models.DataUser;
using Tripatlux.Helpers;
using Tripatlux.Models.DataTrip;

namespace Tripatlux.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            var trips = Container.Manager.VoyageOperation.GetAll().Where(i => i.ID_UTILISATEUR_CONDUCTEUR == SessionHelper.CurrentUser.Id).ToList();
            return View(trips.Select(i => new Trip().Load(i)));
        }

        public ActionResult Trip(Guid Id)
        {
            var trip = new Trip().Load(Id);
            return View(trip);
        }

        [HttpPost]
        public ActionResult Code(Guid idUser, Guid idTrip, string code)
        {
            var trip = new Trip().Load(idTrip);
            var passager = trip.Passagers.Single(i => i.User.Id == idUser);

            if (passager.PayToken == code)
            {
                passager.PaySend = true;
                passager.PayExecute(trip);
            }

            return RedirectToAction("Trip", new { Id = idTrip });
        }

        [HttpGet]
        public ActionResult ValidOk(Guid idUser, Guid idTrip)
        {
            var trip = new Trip().Load(idTrip);
            var passager = trip.Passagers.Single(i => i.User.Id == idUser);
            passager.Valid(true, trip);
            return RedirectToAction("Trip", new { Id = idTrip });
        }

        [HttpGet]
        public ActionResult ValidKo(Guid idUser, Guid idTrip)
        {
            var trip = new Trip().Load(idTrip);
            var passager = trip.Passagers.Single(i => i.User.Id == idUser);
            passager.Valid(false, trip);
            return RedirectToAction("Trip", new { Id = idTrip });
        }

        [HttpGet]
        public ActionResult Avatar(Guid idUser)
        {
            byte[] imageData = new User().Load(idUser).Avatar;
            return imageData == null ? null : File(imageData, "image/png");
        }
    }
}