using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousesAndWells
{
    public class House
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public Well connectedWell { get; set; }

        public string Coordinates { get { return "(" + x + "," + y + ")"; } }

        public double distanceFromWell { get; set; }


    }
}
