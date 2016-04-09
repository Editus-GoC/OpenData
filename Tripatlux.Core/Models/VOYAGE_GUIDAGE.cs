using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Models
{
    public class VOYAGE_GUIDAGE
    {
        public Guid ID_VOYAGE_ETAPE_DE { get; set; }
        public Guid ID_VOYAGE_ETAPE_A { get; set; }
        public int ORDRE_ETAPE { get; set; }
        public int ORDRE_VOYAGE { get; set; }
        public decimal COORD_X { get; set; }
        public decimal COORD_Y { get; set; }
        public string DIRECTION { get; set; }
        public string DURATION { get; set; }
        public int DURATION_SEC { get; set; }
        public string DISTANCE { get; set; }
        public int DISTANCE_M { get; set; }
        public string VILLE { get; set; }
    }
}
