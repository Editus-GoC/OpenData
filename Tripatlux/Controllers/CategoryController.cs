using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tripatlux.Business.Service;
using Tripatlux.Models;

namespace Tripatlux.Controllers
{
    public class CategoryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies()
        {
            var res = SHManager.GetFilms();
            return View(res);
        }

        public ActionResult Movie(int id)
        {
            var res = SHManager.GetFilm(id);
            return View(res);
        }

        public ActionResult Salle(int id)
        {
            var res = SHManager.GetSalle(id);
            return View(res);
        }


        public ActionResult Events()
        {
            var res = SHManager.GetEvenements();
            return View(res);
        }

        public ActionResult Bars()
        {
            var res = SHManager.GetBars();
            return View(res);
        }

        public ActionResult NightClubs()
        {
            var res = SHManager.GetNightClubs();
            return View(res);
        }

        public ActionResult Restaurants()
        {
            var res = SHManager.GetRestaurants();
            return View(res);
        }
    }
}