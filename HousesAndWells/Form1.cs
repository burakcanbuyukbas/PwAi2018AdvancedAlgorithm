using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                var well = new Well("Well" + i);
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
                var house = new House("House" + i);
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

        private void fileInputButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // only txt files allowed
            dialog.Multiselect = false; // no multi select
            int counter = 0;
            int wellCount = 0;
            int constant = 0;
            List<Well> wellList = new List<Well>();
            List<House> houseList = new List<House>();

            string line;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String path = dialog.FileName;
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding()))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (counter == 0)
                        {
                            Int32.TryParse(line.Split(null)[0], out wellCount);
                            Int32.TryParse(line.Split(null)[1], out constant);
                        }
                        else if(counter >= 1 && counter <= wellCount)
                        {
                            int x,y = 0;
                            
                            var matches = Regex.Matches(line, @"-?\d*\.{0,1}\d").Cast<Match>().Select(m => m.Value).ToArray();
                            Int32.TryParse(matches[1], out x);
                            Int32.TryParse(matches[2], out y);
                            Well well = new Well("Well" + counter);
                            well.Id = counter - 1;
                            well.x = x;
                            well.y = y;
                            wellList.Add(well);
                        }

                        else if(counter > wellCount && counter <=  wellCount + wellCount * constant)
                        {
                            int x, y = 0;

                            var matches = Regex.Matches(line, @"-?\d*\.{0,1}\d").Cast<Match>().Select(m => m.Value).ToArray();
                            Int32.TryParse(matches[1], out x);
                            Int32.TryParse(matches[2], out y);
                            House house = new House("House" + (counter - wellCount));
                            house.Id = counter - 1 - wellCount;
                            house.x = x;
                            house.y = y;
                            houseList.Add(house);
                        }
                        counter++;
                    }
                    reader.Close();

                    var form = new Form2(houseList, wellList);


                    form.Location = this.Location;
                    form.StartPosition = FormStartPosition.Manual;
                    form.FormClosing += delegate { this.Show(); };
                    form.Show();
                    this.Hide();
                }
            }
        }
    }
}
