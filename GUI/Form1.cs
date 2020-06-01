using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace GUI
{
	public partial class Form1 : Form
	{
		Field[,] playerFields = new Field[10, 10];
		Field[,] enemyFields = new Field[10, 10];

		public Form1()
		{
			InitializeComponent();
			spawnFields();
		}

		private void CreateFleetButton_Click(object sender, EventArgs e)
		{
			RefreshMap();

			List<int> ships = new List<int>
			{
				2,2,2,2,3,3,3,4,4,5
			};
			Fleet fleet = new Fleet();
			Shipwright shipwright = new Shipwright(10, 10);
			fleet = shipwright.CreateFleet(ships);

			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
					foreach (var ship in fleet.Ships)
					{
						if (ship.Squares.Contains(new Square(r, c)))
							playerFields[r, c].BackColor = Color.Blue;
					}

		}
		private void spawnFields()
		{
			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
				{
					playerFields[r, c] = new Field();
					Controls.Add(playerFields[r, c]);
					playerFields[r, c].Parent = PlayerGameBoard;
					playerFields[r, c].Location = new Point((r * 40) + 10, (c * 40) + 5);
					playerFields[r, c].Size = new Size(40, 40);
					playerFields[r, c].BackColor = Color.White;
				}
		}
		private void RefreshMap()
		{

			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
					playerFields[r, c].BackColor = Color.White;

		}

		private void PlayerGameBoard_Click(object sender, EventArgs e)
		{
			MouseEventArgs e2 = (MouseEventArgs)e;
			//label1.Text = e2.
		}
	}	
}
