using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Tripatlux.ServiceHackathon;

namespace Tripatlux.Business.Service
{
    public static class SHManager
    {
        private static List<FilmFicheDetail> _movies;
        private static List<Event> _events;
        private static FicheResultatMobile _clubs;
        private static FicheResultatMobile _bars;
        private static FicheResultatMobile _restaurant;

        public static void Init()

        {
           Task.Run(delegate
            {
                GetFilms();
                GetRestaurants();
                GetEvenements();
                GetNightClubs();
                GetBars();
            });
        }


        public static List<FilmFicheDetail> GetFilms()
        {
            try
            {
                if (_movies == null)
                {
                    using (var service = new HackathonServiceClient())
                    {
                        var data = service.GetFilms();
                        var list = new List<FilmFicheDetail>();
                        foreach (var m in data)
                        {
                            if (!list.Exists(i => i.Titre_Film == m.Titre_Film))
                                list.Add(m);
                        }
                        _movies = list;
                    }
                }
                return _movies;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetCinemas");
                throw ex;
            }
        }

        public static List<Event> GetEvenements()
        {
            try
            {
                if (_events == null)
                {
                    using (var service = new HackathonServiceClient())
                    {
                        var data = service.GetEvenements();
                        var list = new List<Event>();
                        foreach (var m in data)
                        {
                            if (!list.Exists(i => i.Name == m.Name))
                                list.Add(m);
                        }
                        _events = list;
                    }
                }
                return _events;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetEvenements");
                throw ex;
            }
        }

        public static FicheResultatMobile GetNightClubs()
        {
            try
            {
                if (_clubs == null)
                {
                    using (var service = new HackathonServiceClient())
                    {
                        var data = service.GetNightClubs();
                        _clubs = data;
                    }
                }
                return _clubs;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetNightClubs");
                throw ex;
            }
        }

        public static FicheResultatMobile GetBars()
        {
            try
            {
                if (_bars == null)
                {
                    using (var service = new HackathonServiceClient())
                    {
                        var data = service.GetBars();
                        _bars = data;
                    }
                }
                return _bars;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetBars");
                throw ex;
            }
        }

        public static FicheResultatMobile GetRestaurants()
        {
            try
            {
                if (_restaurant == null)
                {
                    using (var service = new HackathonServiceClient())
                    {
                        var data = service.GetRestaurants();
                        _restaurant = data;
                    }
                }
                return _restaurant;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetRestaurants");
                throw ex;
            }
        }

        public static FilmFicheDetail GetFilm(int id)
        {
            try
            {

                using (var service = new HackathonServiceClient())
                {
                    var data = service.GetFilm(id);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetFilm");
                throw ex;
            }
        }

        public static Salle GetSalle(int id)
        {
            try
            {
                using (var service = new HackathonServiceClient())
                {
                    var data = service.GetSalle(id);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetSalle");
                throw ex;
            }
        }

        public static FicheInfoMobile GetCompany(int id)
        {
            try
            {
                using (var service = new HackathonServiceClient())
                {
                    var data = service.GetTiers(id);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetCompany");
                throw ex;
            }
        }

        public static FicheResultatMobile GetCompanyByAddress(string address)
        {
            try
            {
                using (var service = new HackathonServiceClient())
                {
                    var data = service.GetTiersByAddress(address);
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "SHManager.GetCompanyByAddress");
                throw ex;
            }
        }
    }
}