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

        private int _matchingIndex;

        public string Name { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public House connectedHouse { get; set; }

        public string Coordinates { get { return "(" + x + "," + y + ")"; } }

        public List<House> Prefs { get; set; }

        public Well(string name)
        {
            Name = name;
            Prefs = null;
            connectedHouse = null;
            _matchingIndex = 0;
        }

        public bool Prefers(House h)
        {
            return Prefs.FindIndex(o => o == h) < Prefs.FindIndex(o => o == connectedHouse);
        }



        public House NextCandidateNotYetProposedTo()
        {
            if (_matchingIndex >= Prefs.Count) return null;
            return Prefs[_matchingIndex++];
        }

        public void EngageTo(House h)
        {
            if (h.connectedWell != null) h.connectedWell.connectedHouse = null;
            h.connectedWell = this;
            if (connectedHouse != null) connectedHouse.connectedWell = null;
            connectedHouse = h;
        }


        public string connectedHouseString
        {
            get
            {
                //if (connectedHouse)
                //{
                //    return connectedHouse.Name + " " + connectedHouse.Coordinates;
                //}
                //else
                //{
                return "";
                //}
            }
        }

    }
}
