using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripatlux.Core.Bll;
using Tripatlux.Core.Models.TransportPublic;
using Tripatlux.Core.Tool;

namespace TripAtLux.Console
{
    class Program
    {
        private static byte[] ConvertImageFile(string completeFileName)
        {
            byte[] tempByte = null;
            FileInfo fileInfo = new FileInfo(completeFileName);
            long numByte = fileInfo.Length;
            FileStream fileStream = new FileStream(completeFileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            tempByte = binaryReader.ReadBytes(Convert.ToInt32(numByte));
            return tempByte;
        }

        static void Main(string[] args)
        {
            //using (TripAtLuxManager mgr = new TripAtLuxManager())
            //{
            //    var voyage = mgr.VoyageOperation.GetById(new Guid("FF466965-8594-42C7-A5E7-F397279992E7"));
            //    var etape1 = mgr.EtapeOperation.GetById(new Guid("9B6B735C-3141-4043-8C1E-B5A551023475"));
            //    var etape2 = mgr.EtapeOperation.GetById(new Guid("A0C4F891-C854-4215-8463-CB0029623C21"));

            using (RechercheManager mgrRecherche = new RechercheManager())
            {
                var test = mgrRecherche.RechercheOperation.Search("route du village Crusnes", "208 rue de Noertzange Kayl", DateTime.Now);
            }

            //}

            //using (RechercheManager mgr = new RechercheManager())
            //{
            //    using (TripAtLuxManager mgrTrip = new TripAtLuxManager())
            //    {

            //var test = mgr.RechercheOperation.GetPrixVoyage(mgrTrip.VoyageOperation.GetById(new Guid("f4d144e8-5d58-46ce-b6b9-913c61f664e1")), string.Empty, string.Empty);

            //mgr.RechercheOperation.Search("cinema elange france", "utopolis kirchberg", DateTime.Now);
            //var test = mgr.RechercheOperation.Search("80 route du Buchel 57100 Thionville", "12 rue Antoine de Saint-Exupéry 1923 Luxembourg", DateTime.Now);

            //foreach (var t in test.TourneeEtapes)
            //{
            //    System.Console.WriteLine("-------------");
            //    System.Console.WriteLine($"Ligne : {t.First().PARCOURS_ETAPE.PARCOURS.LIGNE.NOM}");
            //    foreach (var a in t.OrderBy(ta => ta.PARCOURS_ETAPE.ORDRE))
            //    {
            //        System.Console.WriteLine($"{a.PARCOURS_ETAPE.ARRET.NOM} à {a.HEURE.Substring(0, 2)}:{a.HEURE.Substring(2)} ({a.PARCOURS_ETAPE.ORDRE.ToString()})");
            //    }
            //}
            //    }
            //}


            //using (TripAtLuxManager mgr = new TripAtLuxManager())
            //{
            //    mgr.VoyageGuidageOperation.Add(mgr.VoyageOperation.GetById(new Guid("f4d144e8-5d58-46ce-b6b9-913c61f664e1")), File.ReadAllText(@"C:\Users\alexandre.schmitt\Source\Workspaces\TripatLux\Tripatlux\Tripatlux.Core\FichiersAnnexes\json_directions_gmap.txt"));
            //    mgr.Save();
            //}


            //InitArretDeBus();
            //InitBus();
            //Init();

            //using (TransportPublicManager mgr = new TransportPublicManager())
            //{
            //    var test123 = mgr.TransportPublicArretOperation.GetArretPlusProche(6.13f, 50.28f);

            //    var arretDepart = mgr.TransportPublicArretOperation.GetByName("Elange, Cinéma");
            //    var arretArrivee = mgr.TransportPublicArretOperation.GetByName("Kirchberg, Antoine de St Exupéry");
            //    var horaire = new DateTime(2016, 06, 01, 08, 11, 00);

            //    var ligne = mgr.TransportPublicLigneOperation.Search(arretDepart, arretArrivee);
            //    var test =  mgr.TransportPublicTourneeEtapeOperation.GetEtapes(ligne.First(), arretDepart, arretArrivee);

            //    foreach(var t in test)
            //    {
            //        System.Console.WriteLine("-------------");
            //        System.Console.WriteLine($"Ligne : {t.First().PARCOURS_ETAPE.PARCOURS.LIGNE.NOM}");
            //        foreach (var a in t.OrderBy(ta => ta.PARCOURS_ETAPE.ORDRE))
            //        {
            //            System.Console.WriteLine($"{a.PARCOURS_ETAPE.ARRET.NOM} à {a.HEURE.Substring(0,2)}:{a.HEURE.Substring(2)} ({a.PARCOURS_ETAPE.ORDRE.ToString()})");
            //        }
            //    }
            //}
        }

        private static void InitBus()
        {

            for (int i = 300; i <= 301; i++)
            {
                //var completeFileName = $@"C:\Users\alexandre.schmitt\Desktop\mobiliteit-lu-horaires-arrets\data\AVL---_{i}.LIN";
                var prefixe = "RGM";
                var completeFileName = $@"C:\Users\alexandre.schmitt\Desktop\mobiliteit-lu-horaires-arrets\data\{prefixe}---_{i}.LIN";

                using (TransportPublicManager mgr = new TransportPublicManager())
                {
                    LIGNE ligne;
                    PARCOURS parcours = null;
                    TOURNEE tournee = null;
                    PARCOURS_ETAPE parcoursEtape;
                    short index = 1;

                    foreach (var ligneFichier in File.ReadAllLines(completeFileName))
                    {
                        string[] infoLigneBus;
                        if (ligneFichier.StartsWith("*L"))
                        {
                            tournee = null;
                            parcours = null;

                            infoLigneBus = ligneFichier.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var numeroLigneBus = Int16.Parse(infoLigneBus[1]);
                            var arretDepart = mgr.TransportPublicArretOperation.GetByReference(infoLigneBus[2]);
                            var arretTerminus = mgr.TransportPublicArretOperation.GetByReference(infoLigneBus[3]);
                            ligne = mgr.TransportPublicLigneOperation.GetByNumber(numeroLigneBus);
                            if (ligne == null)
                            {
                                ligne = new LIGNE()
                                {
                                    NUMERO = Int16.Parse(infoLigneBus[1]),
                                    NOM = $"{prefixe} - Ligne {infoLigneBus[1]}"
                                };
                                ligne = mgr.TransportPublicLigneOperation.Add(ligne);
                                mgr.Save();
                            }

                            parcours = mgr.TransportPublicParcoursOperation.GetByLigneEtArrets(ligne, arretDepart, arretTerminus);
                            if (parcours == null)
                            {
                                parcours = new PARCOURS()
                                {
                                    ID_LIGNE = ligne.ID,
                                    ID_ARRET_DEPART = arretDepart.ID,
                                    ID_ARRET_TERMINUS = arretTerminus.ID,
                                    REFERENCE = $"{ligne.NOM} - {arretDepart.NOM} - {arretTerminus.NOM}"
                                };
                                parcours = mgr.TransportPublicParcoursOperation.Add(parcours);

                                mgr.Save();
                            }
                        }
                        else if (!ligneFichier.StartsWith("*"))
                        {
                            if (tournee == null)
                            {
                                tournee = new TOURNEE();
                                tournee = mgr.TransportPublicTourneeOperation.Add(tournee);
                                index = 1;
                            }

                            if (tournee != null)
                            {
                                var infoTournee = ligneFichier.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                var arret = mgr.TransportPublicArretOperation.GetByReference(infoTournee[0]);
                                parcoursEtape = mgr.TransportPublicParcoursEtapeOperation.GetByParcoursArret(parcours, arret);
                                if (parcoursEtape == null)
                                {
                                    parcoursEtape = new PARCOURS_ETAPE()
                                    {
                                        ID_ARRET = arret.ID,
                                        ID_PARCOURS = parcours.ID,
                                        ORDRE = index
                                    };
                                    parcoursEtape = mgr.TransportPublicParcoursEtapeOperation.Add(parcoursEtape);
                                    mgr.Save();
                                    index++;
                                }

                                if (parcoursEtape != null)
                                {
                                    mgr.TransportPublicTourneeEtapeOperation.Add(new TOURNEE_ETAPE()
                                    {
                                        ID_PARCOURS_ETAPE = parcoursEtape.ID,
                                        ID_TOURNEE = tournee.ID,
                                        HEURE = infoTournee.First(p => p.StartsWith("0")).Substring(1)
                                    });
                                    mgr.Save();
                                }
                            }
                        }
                    }
                }
            }
        }


        private static void InitArretDeBus()
        {
            ConcurrentBag<ARRET> arrets = new ConcurrentBag<ARRET>();
            File.ReadAllLines(@"C:\Users\alexandre.schmitt\Desktop\mobiliteit-lu-horaires-arrets\data\ARRET_ALS.txt").AsParallel().ForAll(line =>
            {
                var information = line.Split('@');
                arrets.Add(new ARRET()
                {
                    NOM = information[1].Substring(2),
                    REFERENCE = information[5].Substring(2),
                    COORD_X = Decimal.Parse(information[2].Substring(2)),
                    COORD_Y = Decimal.Parse(information[3].Substring(2))
                });
            });

            using (TransportPublicManager mgr = new TransportPublicManager())
            {
                mgr.TransportPublicArretOperation.AddRange(arrets.ToList());
                mgr.Save();
            }
        }

        private static void Init()
        {
            using (TransportPublicManager manager = new TransportPublicManager())
            {
                // Arrêt
                for (int i = 1; i <= 20; i++)
                {
                    manager.TransportPublicArretOperation.Add(new Tripatlux.Core.Models.TransportPublic.ARRET() { NOM = $"Arret {i}" });
                }
                // Ligne
                for (short i = 1; i <= 5; i++)
                {
                    manager.TransportPublicLigneOperation.Add(new Tripatlux.Core.Models.TransportPublic.LIGNE() { NUMERO = i, NOM = $"Ligne {i}" });
                }
                // Tournée
                for (short i = 1; i <= 5; i++)
                {
                    manager.TransportPublicTourneeOperation.Add(new Tripatlux.Core.Models.TransportPublic.TOURNEE() { REFERENCE = i.ToString(), LUNDI = true, MARDI = true, MERCREDI = true, JEUDI = true, VENDREDI = true });
                }
                // Enregistrement
                manager.Save();

                // Parcours
                for (int i = 1; i <= 5; i++)
                {
                    manager.TransportPublicParcoursOperation.Add(new Tripatlux.Core.Models.TransportPublic.PARCOURS()
                    {
                        LUNDI = true,
                        MARDI = true,
                        MERCREDI = true,
                        JEUDI = true,
                        VENDREDI = true,
                        ID_LIGNE = manager.TransportPublicLigneOperation.GetByNumber(i).ID,
                        REFERENCE = i.ToString()
                    });
                }
                for (int i = 6; i <= 10; i++)
                {
                    manager.TransportPublicParcoursOperation.Add(new Tripatlux.Core.Models.TransportPublic.PARCOURS()
                    {
                        SAMEDI = true,
                        DIMANCHE = true,
                        ID_LIGNE = manager.TransportPublicLigneOperation.GetByNumber(i - 5).ID,
                        REFERENCE = i.ToString()
                    });
                }
                // Enregistrement
                manager.Save();

                // parcours étape
                InsertParcoursEtape(manager, manager.TransportPublicParcoursOperation.GetByReference("1"), new List<string> { "Arret 2", "Arret 5", "Arret 10", "Arret 18", "Arret 3" });
                InsertParcoursEtape(manager, manager.TransportPublicParcoursOperation.GetByReference("2"), new List<string> { "Arret 3", "Arret 17", "Arret 5", "Arret 4", "Arret 11", "Arret 2", "Arret 10", "Arret 9" });
                InsertParcoursEtape(manager, manager.TransportPublicParcoursOperation.GetByReference("3"), new List<string> { "Arret 12", "Arret 14", "Arret 18", "Arret 5", "Arret 7", "Arret 9" });
                InsertParcoursEtape(manager, manager.TransportPublicParcoursOperation.GetByReference("4"), new List<string> { "Arret 1", "Arret 2", "Arret 3", "Arret 5", "Arret 7", "Arret 9", "Arret 19", "Arret 20" });
                InsertParcoursEtape(manager, manager.TransportPublicParcoursOperation.GetByReference("5"), new List<string> { "Arret 1", "Arret 7", "Arret 20", "Arret 15" });
                manager.Save();

                // Tournée etape
                InsertTourneeEtape(manager,
                                    manager.TransportPublicParcoursOperation.GetByReference("1"),
                                    manager.TransportPublicTourneeOperation.GetByReference("1"),
                                    new TimeSpan(8, 15, 00),
                                    new TimeSpan(0, 0, 2));
                InsertTourneeEtape(manager,
                                    manager.TransportPublicParcoursOperation.GetByReference("1"),
                                    manager.TransportPublicTourneeOperation.GetByReference("2"),
                                    new TimeSpan(9, 00, 00),
                                    new TimeSpan(0, 0, 4));
                InsertTourneeEtape(manager,
                                    manager.TransportPublicParcoursOperation.GetByReference("2"),
                                    manager.TransportPublicTourneeOperation.GetByReference("3"),
                                    new TimeSpan(8, 42, 00),
                                    new TimeSpan(0, 0, 5));
                manager.Save();
            }
        }


        private static void InsertTourneeEtape(TransportPublicManager manager,
                                                Tripatlux.Core.Models.TransportPublic.PARCOURS parcours,
                                                Tripatlux.Core.Models.TransportPublic.TOURNEE tournee,
                                                TimeSpan heureDépart,
                                                TimeSpan tempsEntreDeuxArrets)
        {
            var parcoursEtapes = manager.TransportPublicParcoursEtapeOperation.GetByParcours(parcours);
            var index = 0;
            foreach (var parcoursEtape in parcoursEtapes.OrderBy(pe => pe.ORDRE))
            {
                //System.Console.WriteLine($"{parcoursEtape.ORDRE} : {parcoursEtape.ARRET.NOM} à {heureDépart + tempsEntreDeuxArrets.Multiply(index)}");
                manager.TransportPublicTourneeEtapeOperation.Add(new Tripatlux.Core.Models.TransportPublic.TOURNEE_ETAPE()
                {
                    ID_PARCOURS_ETAPE = parcoursEtape.ID,
                    ID_TOURNEE = tournee.ID
                    //HEURE = heureDépart + tempsEntreDeuxArrets.Multiply(index)
                });
                index++;
            }
        }

        private static void InsertParcoursEtape(TransportPublicManager manager, Tripatlux.Core.Models.TransportPublic.PARCOURS parcours, List<string> nomArrets)
        {
            short index = 1;
            foreach (string arret in nomArrets)
            {
                manager.TransportPublicParcoursEtapeOperation.Add(new Tripatlux.Core.Models.TransportPublic.PARCOURS_ETAPE()
                {
                    ID_PARCOURS = parcours.ID,
                    ID_ARRET = manager.TransportPublicArretOperation.GetByName(arret).ID,
                    ORDRE = index
                });
                index++;
            }
        }

        private static void RemoveAll()
        {
            using (TransportPublicManager manager = new TransportPublicManager())
            {
                manager.TransportPublicParcoursEtapeOperation.RemoveAll();
                manager.TransportPublicParcoursOperation.RemoveAll();
                manager.TransportPublicLigneOperation.RemoveAll();
                manager.TransportPublicArretOperation.RemoveAll();
                manager.TransportPublicTourneeOperation.RemoveAll();
                manager.TransportPublicTourneeEtapeOperation.RemoveAll();

                manager.Save();
            }
        }
    }
}
