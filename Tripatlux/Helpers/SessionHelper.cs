using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Models;
using Tripatlux.Models.DataTrip;
using Tripatlux.Models.DataUser;
using Tripatlux.Models.Search;

namespace Tripatlux.Helpers
{
    public static class SessionHelper
    {
        public static User CurrentUser
        {
            get { return HttpContext.Current.Session["CurrentUser"] as User; }
            set { HttpContext.Current.Session["CurrentUser"] = value; }
        }

        public static SearchResult LastSearchResult
        {
            get { return HttpContext.Current.Session["LastSearchResult"] as SearchResult; }
            set { HttpContext.Current.Session["LastSearchResult"] = value; }
        }
    }
}