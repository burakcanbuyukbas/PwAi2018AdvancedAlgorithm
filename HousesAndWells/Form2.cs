using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HousesAndWells
{
    public partial class Form2 : Form
    {
        List<Well> wells;
        List<House> houses;
        List<Well> kwells;
        decimal totalDistance;

        public Form2(List<House> houseList, List<Well> wellList)
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            buttonOutput.Visible = false;
            labelTotalDistance.Visible = false;

            //GUI related work
            var bindingListForWells = new BindingList<Well>(wellList);
            var wellSource = new BindingSource(bindingListForWells, null);
            dataGridView1.DataSource = wellSource;
            dataGridView1.Columns["x"].Visible = false;
            dataGridView1.Columns["y"].Visible = false;
            dataGridView1.Columns["connectedHouse"].Visible = false;
            var bindingListForHouses = new BindingList<House>(houseList);
            var houseSource = new BindingSource(bindingListForHouses, null);
            dataGridView2.DataSource = houseSource;
            dataGridView2.Columns["connectedWell"].Visible = false;
            dataGridView2.Columns["x"].Visible = false;
            dataGridView2.Columns["y"].Visible = false;
            dataGridView2.Columns["connectedWellName"].Visible = false;
            dataGridView1.Columns["connectedHouseString"].Visible = false;

            wells = wellList;
            houses = houseList;
            kwells = new List<Well>();

        }

        private void buttonOrganize_Click(object sender, EventArgs e)
        {
            //GUI related
            dataGridView2.DataSource = null;
            dataGridView2.Visible = false;
            label2.Visible = false;
            dataGridView1.Width = dataGridView1.Width * 2;
            dataGridView1.Columns["connectedHouseString"].Visible = true;
            buttonOrganize.Visible = false;
            buttonOutput.Visible = true;

            //clone wells to get both sets with same size
            int constant = houses.Count / wells.Count;
            for (int i = 0; i < constant; i++)
            {
                foreach (Well well in wells.ToList())
                {
                    Well cloneWell = new Well(well.Name);
                    cloneWell.Id = wells.Max(x => x.Id) + 1;
                    cloneWell.Name = well.Name;
                    cloneWell.x = well.x;
                    cloneWell.y = well.y;
                    kwells.Add(cloneWell);
                }
            }
            //fill preference list of wells
            foreach (Well well in kwells)
            {
                well.Prefs = houses.OrderBy(house => ((well.x - house.x) * (well.x - house.x) + (well.y - house.y) * (well.y - house.y))).ToList();
            }

            //fill preference list of houses
            foreach (House house in houses)
            {
                house.Prefs = kwells.OrderBy(well => ((house.x - well.x) * (house.x - well.x) + (house.y - well.y) * (house.y - well.y))).ToList();
            }

            ////Commented out to implement new matching algorithm


            //int freeHouseCount = houses.Count;
            //while (freeHouseCount > 0)
            //{
            //    foreach (House house in houses)
            //    {
            //        if (house.connectedWell == null)
            //        {
            //            Well well = house.NextCandidateNotYetProposedTo();
            //            if (well.connectedHouse == null)
            //            {
            //                house.EngageTo(well);
            //                freeHouseCount--;
            //            }
            //            else if (well.Prefers(house))
            //            {
            //                house.EngageTo(well);
            //            }
            //        }
            //    }
            //}


            RunMatching(houses, kwells); //algorithm v2
            var bindingListForMatchedHouses = new BindingList<House>(houses);
            dataGridView1.DataSource = bindingListForMatchedHouses;
            dataGridView1.Columns["x"].Visible = false;
            dataGridView1.Columns["y"].Visible = false;
            dataGridView1.Columns["connectedWell"].Visible = false;
            dataGridView1.Columns["connectedWellName"].Visible = true;
            foreach (var well in kwells)
            {
                totalDistance += (decimal)(Math.Sqrt((well.x - well.connectedHouse.x) * (well.x - well.connectedHouse.x)
                    + (well.y - well.connectedHouse.y) * (well.y - well.connectedHouse.y)));
            }
            labelTotalDistance.Visible = true;
            labelTotalDistance.Text = "Total Distance: " + totalDistance.ToString();

        }

        private void buttonOutput_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "MatchingResults.txt";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.OpenFile());

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    writer.WriteLine(dataGridView1.Rows[row].Cells[0].Value.ToString() + ". "
                        + dataGridView1.Rows[row].Cells[1].Value.ToString() + ": ("
                        + dataGridView1.Rows[row].Cells[2].Value.ToString() + ","
                        + dataGridView1.Rows[row].Cells[3].Value.ToString() + ") --> "
                        + dataGridView1.Rows[row].Cells[5].Value.ToString());
                }
                writer.WriteLine("Total Distance: " + totalDistance.ToString());
                writer.Dispose();
                writer.Close();

            }
        }

        //Matching Algorithm v2
        private static void RunMatching(List<House> wells, List<Well> houses)
        {
            foreach (var well in wells)
            {
                var matchings = new List<Well>();
                while (well.connectedWell == null)
                {
                    var foundMatch = false;
                    Well match = null;
                    while (!foundMatch)
                    {
                        match = houses.Find(x => x == well.Prefs.First());
                        if (!matchings.Contains(match))
                            foundMatch = true;
                        else
                            // --Move the matched house to the back of well's preferences in order to grab the next one which might not be matched
                            MoveFirstElementToEnd(well.Prefs);
                    }

                    if (match.connectedHouse == null)
                        MarkAsMatched(well, match);

                    else
                    {
                        // --Compare preference of current match and new match, cancelling the current match out if the potential match is a higher preference
                        var currentMatchIndex = match.Prefs.IndexOf(match.connectedHouse);
                        var potentialMatchIndex = match.Prefs.IndexOf(well);

                        if (potentialMatchIndex < currentMatchIndex)
                        {
                            var currentMatch = wells.Find(m => m == match.connectedHouse);
                            currentMatch.connectedWell = null;
                            MarkAsMatched(well, match);
                        }
                        else
                            matchings.Add(match);
                    }
                }
            }
            // --Recursively run through the program if not all of the proposing group is matched (i.e. if someone had their match taken away from them)
            if (wells.Any(m => m.connectedWell == null))
                RunMatching(wells, houses);
        }
        private static void MoveFirstElementToEnd<T>(List<T> list) where T : class
        {
            var temp = list[0];
            list.Remove(temp);
            list.Add(temp);
        }
        private static void MarkAsMatched(House house, Well well)
        {
            house.connectedWell = well;
            well.connectedHouse = house;
        }

    }

}

