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
					grid[i, j].Click += OnClickSquares;
					this.Controls.Add(grid[i, j]);
				}
			}
		}
		private void Arrange_Click(object sender, EventArgs e)
		{
			ClearFleet(myFleet);
			ClearFleet(opponent);
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
		private void ClearFleet(Button[,] fleet)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < cols; j++)
				{
					fleet[i, j].BackColor = Color.FromArgb(255,207,255);
				}
			}
		}
		
		private void OnClickSquares(object sender, EventArgs e)
		{
			if (check == false)
				return;
			Btn button = sender as Btn;
			Square point = new Square(button.x, button.y);
			HitResult hitResult = pcFleetToPlayWith.Hit(point);
			switch (hitResult)
			{
				case HitResult.Hit:
					{
						button.BackColor = Color.FromArgb(204, 0, 0);
						break;
					}
				case HitResult.Missed:
					{
						button.BackColor = Color.FromArgb(255,0,255);
						NextTurn();
						break;
					}
				case HitResult.Sunken:
					{
						foreach (var sunken in pcFleetToPlayWith.Ships.Where(s => s.Squares.Contains(point)).SelectMany(s => s.Squares))
						{
							opponent[sunken.Row, sunken.Col].BackColor = Color.FromArgb(255, 51, 51);
						}
						countSunkenShipsMe++;
						if (countSunkenShipsMe == 10)
						{
							string message = "You Win!";
							string title = "You Win!";
							MessageBox.Show(message, title);
							Application.Exit();
						}
						break;
					}
			}

		}

		private void NextTurn()
		{
			Square point = gunner.NextTarget();
			HitResult hitResult = myFleetToPlayWith.Hit(point);
			gunner.ProcessHitResult(hitResult);
			switch (hitResult)
			{
				case HitResult.Hit:
					{
						myFleet[point.Row, point.Col].BackColor = Color.FromArgb(204,0,0);
						NextTurn();
						break;
					}
				case HitResult.Missed:
					{
						myFleet[point.Row, point.Col].BackColor = Color.FromArgb(255,0,255);
						break;
					}
				case HitResult.Sunken:
					{
						foreach (var sunken in myFleetToPlayWith.Ships.Where(s => s.Squares.Contains(point)).SelectMany(s => s.Squares))
						{
							myFleet[sunken.Row, sunken.Col].BackColor = Color.FromArgb(255,51,51);
						}
						countSunkenShipsPc++;
						if (countSunkenShipsPc == 10)
						{
							string message = "You Lose!";
							string title = "You Lose!";
							MessageBox.Show(message, title);
							Application.Exit();
						}
						NextTurn();
						break;
					}
			}

		}
		private void Play_Click(object sender, EventArgs e)
		{
			Arrange.Visible = false;
			check = true;
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
		int countSunkenShipsPc = 0;
		int countSunkenShipsMe = 0;

		private void Quit_Game(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
