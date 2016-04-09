using System.Web;

namespace Tripatlux.Business
{
    public static class CacheManager
    {
        public static void Set(string key, object obj)
        {
            HttpContext.Current.Cache[key] = obj;
        }

        public static T Get<T>(string key) where T : class
        {
            var data = HttpContext.Current.Cache[key];
            return data as T;
        }

        public static bool Exist(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }
    }
}