using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Models;
using Tripatlux.Core.Models.TransportPublic;

namespace Tripatlux.Core.Bll.Operation
{
    public class RechercheOperation
    {
        private TRIP_AT_LUXContext _contextTrip;
        private TRANSPORT_PUBLICContext _contextTransport;

        public RechercheOperation(TRIP_AT_LUXContext contextTrip, TRANSPORT_PUBLICContext contextTransport)
        {
            _contextTransport = contextTransport;
            _contextTrip = contextTrip;
        }

        public SearchResult Search(string De, string A, DateTime quand)
        {
            SearchResult returnedResult = new SearchResult();

            var geocodingDe = GetCoordinates(De);
            var geocodingA = GetCoordinates(A);

            // Bus
            TransportPublicArretOperation transportPublicArretOperation = new TransportPublicArretOperation(_contextTransport);
            TransportPublicLigneOperation transportPublicLigneOperation = new TransportPublicLigneOperation(_contextTransport);
            TransportPublicTourneeEtapeOperation transportPublicTourneeEtapeOperation = new TransportPublicTourneeEtapeOperation(_contextTransport);
            var arretDe = transportPublicArretOperation.GetArretPlusProche(geocodingDe.Item1, geocodingDe.Item2);
            var arretA = transportPublicArretOperation.GetArretPlusProche(geocodingA.Item1, geocodingA.Item2);

            foreach (var de in arretDe)
            {
                foreach (var a in arretA)
                {
                    var ligneTemp = transportPublicLigneOperation.Search(de, a);
                    if (ligneTemp != null && ligneTemp.Count > 0)
                    {
                        returnedResult.TourneeEtapes = transportPublicTourneeEtapeOperation.GetEtapes(ligneTemp.First(), de, a);
                    }
                }
            }
            //while (ligne == null || ligne.Count == 0)
            //{
            //    transportPublicLigneOperation.Search(arretDe, arretA);
            //}

            //if (ligne != null && ligne.Count > 0)
            //    returnedResult.TourneeEtapes = transportPublicTourneeEtapeOperation.GetEtapes(ligne.First(), arretDe, arretA);

            // Voyage
            VoyageOperation voyageOperation = new VoyageOperation(_contextTrip);
            returnedResult.Voyages = voyageOperation.GetVoyagePlusProche(geocodingDe.Item1, geocodingDe.Item2, geocodingA.Item1, geocodingA.Item2, 5000);

            return returnedResult;
        }

        public float GetPrixVoyage(VOYAGE voyage, string De, string A)
        {
            var geocodingDe = GetCoordinates(De);
            var geocodingA = GetCoordinates(A);

            EtapeOperation etapeOperation = new EtapeOperation(_contextTrip);
            var etapeDe = etapeOperation.GetByCoordinates(voyage.ID, geocodingDe.Item1, geocodingDe.Item2, true);
            var etapeA = etapeOperation.GetByCoordinates(voyage.ID, geocodingA.Item1, geocodingA.Item2, false);

            return GetPrixVoyage(voyage, etapeDe, etapeA);
        }

        public float GetPrixVoyage(VOYAGE voyage, VOYAGE_ETAPE etapeDe, VOYAGE_ETAPE etapeA)
        {
            VoyageGuidageOperation voyageGuidageOperation = new VoyageGuidageOperation(_contextTrip);
            return voyage.COUT_AU_KM / voyageGuidageOperation.GetKm(voyage) * voyageGuidageOperation.GetKm(etapeDe, etapeA);
        }

        public float GetPrixVoyage(Guid idVoyage, Guid idEtapeDe, Guid idEtapeA)
        {
            using (var manager = new TripAtLuxManager())
            {
                var voyage = manager.VoyageOperation.GetById(idVoyage);
                var etapeDe = manager.EtapeOperation.GetById(idEtapeDe);
                var etapeA = manager.EtapeOperation.GetById(idEtapeA);
                VoyageGuidageOperation voyageGuidageOperation = new VoyageGuidageOperation(_contextTrip);
                return voyage.COUT_AU_KM * voyageGuidageOperation.GetKm(etapeDe, etapeA);
            }
        }

        public string GetVille(double coordX, double coordY)
        {
            var urlGoogle = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={coordY.ToString().Replace(",",".")},{coordX.ToString().Replace(",", ".")}&key=AIzaSyCta3Rfy6fLAfzzb8OrqQKNfISBSefH8Rk";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlGoogle);

            request.Method = WebRequestMethods.Http.Get;
            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string data;
                // Get the stream associated with the response.
                using (Stream receiveStream = response.GetResponseStream())
                {
                    // Pipes the stream to a higher level stream reader with the required encoding format. 

                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        Console.WriteLine("Response stream received.");
                        data = readStream.ReadToEnd();
                        Console.WriteLine(data);
                    }

                }
            }

            return string.Empty;
        }

        public Tuple<double, double> GetCoordinates(string location)
        {
            var urlGoogle = $"https://maps.googleapis.com/maps/api/geocode/json?address={location.Replace(" ", "+")}&key=AIzaSyCta3Rfy6fLAfzzb8OrqQKNfISBSefH8Rk";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlGoogle);

            request.Method = WebRequestMethods.Http.Get;
            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string data;
                // Get the stream associated with the response.
                using (Stream receiveStream = response.GetResponseStream())
                {
                    // Pipes the stream to a higher level stream reader with the required encoding format. 

                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        Console.WriteLine("Response stream received.");
                        data = readStream.ReadToEnd();
                        Console.WriteLine(data);
                    }

                }

                var geocoding = JsonConvert.DeserializeObject<GeoResponse>(data);
                if (geocoding != null && geocoding.Status == "OK")
                    return new Tuple<double, double>(geocoding.Results.First().Geometry.Location.Lng, geocoding.Results.First().Geometry.Location.Lat);
            }
            return null;
        }
    }

    public class GeoLocation
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GeoGeometry
    {
        public GeoLocation Location { get; set; }
    }

    public class GeoResult
    {
        //public GeoAdressComponents Address_Components { get; set; }
        public GeoGeometry Geometry { get; set; }
    }

    //public class GeoAdressComponents
    //{

    //}

    //public 

    public class GeoResponse
    {
        public string Status { get; set; }
        public GeoResult[] Results { get; set; }
    }

    public class SearchResult
    {
        public List<List<TOURNEE_ETAPE>> TourneeEtapes { get; set; }
        public List<VOYAGE> Voyages { get; set; }
    }
}
