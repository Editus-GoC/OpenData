using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tripatlux.Models.DataUser
{
    public class Avis
    {
        public User User { get; set; }
        public string Comment { get; set; }

        public Avis() { }
        public Avis(Guid idUser, string comment)
        {
            User = new User().Load(idUser);
            Comment = comment;
        }
    }
}