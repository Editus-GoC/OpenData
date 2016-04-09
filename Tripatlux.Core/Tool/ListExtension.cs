using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Tool
{
    public static class ListExtension
    {
        public static bool SafeAny<T>(this IQueryable<T> liste) where T : class
        {
            return liste != null && liste.Any();
        }
    }
}
