using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tripatlux.Business;
using Tripatlux.Business.Service;
using Tripatlux.Helpers;
using Tripatlux.Models.DataUser;

namespace Tripatlux
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SHManager.Init();
            new BatchManager().Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] == "::1")
                 SessionHelper.CurrentUser = new User().Load(new Guid("4506DBEC-AA38-4828-91D9-D173469BE0F3"));
                //SessionHelper.CurrentUser = new User().Load(new Guid("0257BEE4-7234-4CA5-917A-FC31F39D57F0"));
            else
                SessionHelper.CurrentUser = new User().Load(new Guid("824FC130-0F8D-4DF6-8A2D-9C987FB177AC"));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
        }
    }
}
