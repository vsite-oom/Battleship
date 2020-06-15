using System;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;
using System.Drawing;
using System.Linq;

namespace FleetView
{
	public partial class FleetForm : Form
	{
		public FleetForm()
		{
			InitializeComponent();
			GridDrawing(myFleet, 20);
			GridDrawing(opponent, 480);
			check = false;
		}
		private void GridDrawing(Btn[,] grid, int position)
		{
		
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					grid[i, j] = new Btn();
					grid[i, j].x = i;
					grid[i, j].y = j;
					grid[i, j].BackColor = Color.FromArgb(255,207,255);
					grid[i, j].Location = new System.Drawing.Point(position + i * 40, 80 + j * 40);
					grid[i, j].Size = new System.Drawing.Size(40, 40);
					this.Controls.Add(grid[i, j]);
				}
			}
		}
		private void Arrange_Click(object sender, EventArgs e)
		{
			Play.Visible = true;
			Shipwright ship = new Shipwright(rows, cols);
			var fleetOfUser= ship.CreateFleet(sizeOfShip);
			var fleetOfOpponent = ship.CreateFleet(sizeOfShip);
			myFleetToPlayWith = fleetOfUser;
			pcFleetToPlayWith = fleetOfOpponent;
			gunner = new Gunner(rows, cols, sizeOfShip);

			foreach (Ship ships in fleetOfUser.Ships)
			{
				foreach (Square fleet in ships.Squares)
				{
					myFleet[fleet.Row, fleet.Col].BackColor = Color.FromArgb(0, 120, 0);
				}
			}
		}
		

		private int rows = 10;
		private int cols = 10;
		private Btn[,] myFleet = new Btn[10, 10];
		private Btn[,] opponent = new Btn[10, 10];
		private Fleet myFleetToPlayWith;
		private Fleet pcFleetToPlayWith;
		private Gunner gunner;
		private int[] sizeOfShip = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
		bool check = false;

	}
}
