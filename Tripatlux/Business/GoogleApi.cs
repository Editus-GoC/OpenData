using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Tripatlux.Business
{
    public static class GoogleApi
    {
        private const string KEY = "AIzaSyCta3Rfy6fLAfzzb8OrqQKNfISBSefH8Rk";

        public static string GetCity(double latitude, double longitude)
        {
            var adr = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}&result_type=sublocality|locality&key={KEY}";

            var request = (HttpWebRequest)WebRequest.Create(adr);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var res = JsonConvert.DeserializeObject<dynamic>(streamReader.ReadToEnd());
                return res.results[0].address_components[0].short_name;
            }
        }

        public static string GetGoogleDirection(string adrStart, string adrEnd, List<string> adrSteps)
        {
            var origin = adrStart.Replace(" ", "+");
            var dest = adrEnd.Replace(" ", "+");
            var steps = adrSteps == null ? "" : string.Join("|", adrSteps.Select(i => i.Replace(" ", "+")));

            var adr = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={dest}&waypoints={steps}&mode=driving&key={KEY}";

            var request = (HttpWebRequest)WebRequest.Create(adr);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}