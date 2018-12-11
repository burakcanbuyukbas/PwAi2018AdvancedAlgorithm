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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxWells_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxConstant.Text) && !String.IsNullOrEmpty(textBoxWells.Text))
            {
                int constant = Int32.Parse(textBoxConstant.Text);
                int wells = Int32.Parse(textBoxWells.Text);
                textBoxHouses.Text = (wells * constant).ToString();
            }
        }

        private void textBoxWells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void textBoxConstant_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxConstant.Text) && !String.IsNullOrEmpty(textBoxWells.Text))
            {
                int constant = Int32.Parse(textBoxConstant.Text);
                int wells = Int32.Parse(textBoxWells.Text);
                textBoxHouses.Text = (wells * constant).ToString();
            }
        }
        private void textBoxConstant_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void buttonRandomLocations_Click(object sender, EventArgs e)
        {
            //requires validation
            int wells = Int32.Parse(textBoxWells.Text);
            int constant = Int32.Parse(textBoxConstant.Text);
            int houses = wells * constant;
            Random rnd = new Random();

            //generate and populate a list of wells
            var wellList = new List<Well>();
            for (int i = 1; i <= wells; i++)
            {
                var well = new Well();
                well.Id = i;
                well.Name = "Well" + i;
                well.x = rnd.Next(0, 99);
                well.y = rnd.Next(0, 99);
                wellList.Add(well);
            }

            //generate and populate a list of houses
            var houseList = new List<House>();
            for (int i = 1; i <= houses; i++)
            {
                var house = new House();
                house.Id = i;
                house.Name = "House" + i;
                house.x = rnd.Next(0, 99);
                house.y = rnd.Next(0, 99);
                houseList.Add(house);
            }


            var form = new Form2(houseList, wellList);

            
            form.Location = this.Location;
            form.StartPosition = FormStartPosition.Manual;
            form.FormClosing += delegate { this.Show(); };
            form.Show();
            this.Hide();
        }
    }
}
