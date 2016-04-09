using System;
using System.Web.Mvc;
using Tripatlux.Business.Service;
using Tripatlux.Models.DataTrip;
using Tripatlux.Models.ViewModels;
using Tripatlux.ServiceHackathon;

namespace Tripatlux.Controllers
{
    public class CreatedController : BaseController
    {
        // GET: Created
        public ActionResult Index()
        {
            /*var model = new TripViewModel();
            model.AddressStart = "4 route du village Crusnes";
            model.AddressEnd = "208 rue de Noertzange Kayl";
            model.Date = new DateTime(2016, 04, 09);
            model.Hours = DateTime.MinValue.AddHours(10);
            model.Trip = new Models.DataTrip.Trip();
            model.Trip.NbPassagers = 4;
            model.Trip.DefaultCost = 25;
            model.RoundTrip = "on";
            model.Steps.Add("Ottange");
            model.HoursReturn = DateTime.MinValue.AddHours(18);*/

            return View(new TripViewModel());
        }

        [HttpPost]
        public ActionResult Index(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = model.Save();
            if (!res.IsOk)
            {
                ModelState.AddModelError("Model", new Exception("Impossible de sauvegarde votre trajet"));
            }
            return RedirectToAction("Success", new { id = res.Id });
        }

        public ActionResult Success(Guid id)
        {
            return View(new Trip().Load(id));
        }

        #region Ajax

        public JsonResult VerifyIsCompany(string address)
        {
            return Json(SHManager.GetCompanyByAddress(address), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}