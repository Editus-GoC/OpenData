using System;
using System.Linq;
using System.Web.Mvc;
using Tripatlux.Helpers;
using Tripatlux.Models.DataBus;
using Tripatlux.Models.DataTrip;
using Tripatlux.Models.Search;
using Tripatlux.Models.ViewModels;

namespace Tripatlux.Controllers
{
    public class FindController : BaseController
    {
        // GET: Created
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result(SearchRequest request)
        {
            //TODO
           /* request.StartAddress = "4 route du village 54680 Crusnes";
            request.EndAddress = "208 rue de noertzange Kayl";
            request.Date_Submit = DateTime.Today;*/

            var res = Container.RechercheManager.RechercheOperation.Search(request.StartAddress, request.EndAddress, request.Date_Submit);

            var result = new SearchResult(res);
            result.Request = request;
            SessionHelper.LastSearchResult = result;
            return View(result);
        }

        public ActionResult InfoBus(Guid id)
        {
            return View(new Way().Load(id));
        }

        public ActionResult InfoCo(Guid id)
        {
            return View(new Trip().Load(id));
        }

        public ActionResult Reservation(Guid idTrip)
        {
            var model = new ReservationViewModel();
            model.Trip = new Trip().Load(idTrip);
            return View(model);
        }

        public ActionResult Paiement(PaiementViewModel model)
        {
            return View(model);
        }

        public ActionResult PaiementResult(PaiementViewModel model)
        {
            var adrStart = SessionHelper.LastSearchResult.Request.LocationStartAddress;
            var adrEnd = SessionHelper.LastSearchResult.Request.LocationEndAddress;
            var stepstart = Container.Manager.EtapeOperation.GetByCoordinates(model.IdTrip, (double) adrStart.CoordX, (double)adrEnd.CoordY, true);
            var stepend = Container.Manager.EtapeOperation.GetByCoordinates(model.IdTrip, (double)adrStart.CoordX, (double)adrEnd.CoordY, false);
            // var cost = Container.RechercheManager.RechercheOperation.GetPrixVoyage(Container.Manager.VoyageOperation.GetById(model.Trip.Id), stepstart, stepend);
            var cost = model.NbPlace * model.Trip.DefaultCost;
            var res = model.PayByPaypal(cost);
            return View(res);
        }

        #region ajax

        public string Next()
        {
            return RenderPartialViewToString("_RefreshItems", SessionHelper.LastSearchResult.GetPage(SessionHelper.LastSearchResult.CurrentPage + 1).ToList());
        }

        #endregion
    }
}