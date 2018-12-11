using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousesAndWells
{
    public class Well
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public List<House> connectedHouses { get; set; }
        public string Coordinates { get { return "(" + x + "," + y + ")"; } }
        public string connectedHousesString { get
            {
                return String.Join(", ", connectedHouses.Select(x => x.Name + "(" + x.Coordinates + ")")); ;
            }
        }

    }
}
