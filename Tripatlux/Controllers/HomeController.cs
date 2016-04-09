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
    public class HomeController : BaseController
    {
        public ActionResult Index(string addressTo)
        {
            ViewData["AddressTo"] = addressTo;
            return View();
        }
    }
}