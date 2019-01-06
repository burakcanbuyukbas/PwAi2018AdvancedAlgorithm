﻿using System;
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

        public Form2(List<House> houseList, List<Well> wellList)
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            buttonOutput.Visible = false;


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
            //dataGridView2.Columns["distanceFromWell"].Visible = false;
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


            int freeHouseCount = houses.Count;
            while (freeHouseCount > 0)
            {
                foreach (House house in houses)
                {
                    if (house.connectedWell == null)
                    {
                        Well well = house.NextCandidateNotYetProposedTo();
                        if (well.connectedHouse == null)
                        {
                            house.EngageTo(well);
                            freeHouseCount--;
                        }
                        else if (well.Prefers(house))
                        {
                            house.EngageTo(well);
                        }
                    }
                }
            }
            var bindingListForMatchedHouses = new BindingList<House>(houses);
            dataGridView1.DataSource = bindingListForMatchedHouses;
            dataGridView1.Columns["x"].Visible = false;
            dataGridView1.Columns["y"].Visible = false;
            dataGridView1.Columns["connectedWell"].Visible = false;
            dataGridView1.Columns["connectedWellName"].Visible = true;



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
                        +  dataGridView1.Rows[row].Cells[2].Value.ToString() + ","
                        + dataGridView1.Rows[row].Cells[3].Value.ToString() + ") --> "
                        + dataGridView1.Rows[row].Cells[5].Value.ToString());
                }

                writer.Dispose();
                writer.Close();

            }
        }
    }
}

