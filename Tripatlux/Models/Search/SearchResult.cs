using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tripatlux.Models.DataBus;
using Tripatlux.Models.DataTrip;

namespace Tripatlux.Models.Search
{
    public class SearchResult
    {
        public SearchRequest Request { get; set; }
        public List<Item> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get { return Items.Count; } }

        public SearchResult()
        {
            CurrentPage = 1;
            Items = new List<Item>();
        }

        public SearchResult(Core.Bll.Operation.SearchResult res) : this()
        {
            if (res.Voyages != null)
            {
                foreach (var item in res.Voyages)
                {
                    Items.Add(new Trip().Load(item));
                }
            }

            if (res.TourneeEtapes != null)
            {
                foreach (var item in res.TourneeEtapes)
                {
                    Items.Add(new Way().Load(item));
                }
            }
            Items = Items.OrderBy(i => i.DateStart).ToList();
        }

        public IEnumerable<Item> GetPage(int page)
        {
            CurrentPage = page;
            return Items.Skip(10 * (CurrentPage - 1)).Take(10);
        }
    }
}