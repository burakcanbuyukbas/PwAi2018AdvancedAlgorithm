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


        public Form2(List<House> houseList, List<Well> wellList)
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;



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
            dataGridView1.Columns["connectedHousesString"].Visible = false;

            wells = wellList;
            houses = houseList;

        }

        private void buttonOrganize_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Visible = false;
            label2.Visible = false;
            dataGridView1.Width = dataGridView1.Width * 2;
            dataGridView1.Columns["connectedHousesString"].Visible = true;
            List<Well> sortedWells = new List<Well>();
            List<House> connectedHouseList = new List<House>();
            try
            {
                foreach (Well well in wells)
                {
                    connectedHouseList.Clear();
                    foreach (House house in houses)
                    {
                        
                        house.distanceFromWell = Math.Sqrt(((well.x - house.x) * (well.x - house.x) + (well.y - house.y) * (well.y - house.y)));

                    }
                    foreach (House house in houses.OrderBy(x => x.distanceFromWell).Take(3))//k number should replace with 3  BUG!
                    {
                        house.connectedWell = well;
                        connectedHouseList.Add(house);
                        houses.Remove(house);
                    }
                    well.connectedHouses = connectedHouseList;
                    sortedWells.Add(well);//this fking list always get same result  BUG!
                }
                var bindingListForSortedWells = new BindingList<Well>(sortedWells);
                dataGridView1.DataSource = bindingListForSortedWells;

            }
            catch (Exception ex)
            {

                Console.WriteLine("**********EXCEPTION: {0}", e.ToString());
                throw;
            }

        }
    }
}

