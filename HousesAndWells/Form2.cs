using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public Form2(List<House> houseList, List<Well> wellList)
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;


            //GUI related work
            var bindingListForWells = new BindingList<Well>(wellList);
            var wellSource = new BindingSource(bindingListForWells, null);
            dataGridView1.DataSource = wellSource;
            dataGridView1.Columns["x"].Visible = false;
            dataGridView1.Columns["y"].Visible = false;
            var bindingListForHouses = new BindingList<House>(houseList);
            var houseSource = new BindingSource(bindingListForHouses, null);
            dataGridView2.DataSource = houseSource;
            dataGridView2.Columns["connectedWell"].Visible = false;
            dataGridView2.Columns["x"].Visible = false;
            dataGridView2.Columns["y"].Visible = false;
            dataGridView2.Columns["distanceFromWell"].Visible = false;
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

        }
    }
}

