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

        private int _matchingIndex { get; set; }

        public string Name { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public Well connectedWell { get; set; }

        public string Coordinates { get { return "(" + x + "," + y + ")"; } }

        public List<Well> Prefs { get; set; }


        public House(string name)
        {
            Name = name;
            Prefs = null;
            connectedWell = null;
            _matchingIndex = 0;
        }

        public bool Prefers(Well h)
        {
            return Prefs.FindIndex(o => o == h) < Prefs.FindIndex(o => o == connectedWell);
        }



        public Well NextCandidateNotYetProposedTo()
        {
            if (_matchingIndex >= Prefs.Count) return null;
            return Prefs[_matchingIndex++];
        }

        public void EngageTo(Well w)
        {
            if (w.connectedHouse != null) w.connectedHouse.connectedWell = null;
            w.connectedHouse = this;
            if (connectedWell != null) connectedWell.connectedHouse = null;
            connectedWell = w;
        }

    }
}
