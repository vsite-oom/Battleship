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
	public struct GameManager
	{
		public Fleet fleet;
		public Shipwright shipwright;
		public Gunner gunner;

		public GameManager(int row, int column, List<int> ships)
		{
			fleet = new Fleet();
			shipwright = new Shipwright(row, column);
			gunner = new Gunner(row, column, ships);
			fleet = shipwright.CreateFleet(ships);
		}

	}
	public partial class Form1 : Form
	{
		Field[,] playerFields = new Field[10, 10];
		Field[,] enemyFields = new Field[10, 10];

		GameManager playerManager;
		GameManager enemyManager;

		public Form1()
		{
			InitializeComponent();
			spawnFields();
			playerManager = new GameManager(10, 10, new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
			enemyManager = new GameManager(10, 10, new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
		}

		private void CreateFleetButton_Click(object sender, EventArgs e)
		{
			RefreshMap();

			AcceptFleetButton.Enabled = true;

			playerManager.fleet = playerManager.shipwright.CreateFleet(new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });
			enemyManager.fleet = enemyManager.shipwright.CreateFleet(new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });

			drawPlayerShips(playerManager);

		}

		private void drawPlayerShips(GameManager gameManager)
		{

			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
					foreach (var ship in gameManager.fleet.Ships)
					{
						if (ship.Squares.Contains(new Square(r, c)))
						{
							playerFields[r, c].BackColor = Color.Blue;
						}
					}
		}

		private void spawnFields()
		{
			char letter = 'A';
			for (int r = 0; r < 10; r++,letter++)
				for (int c = 0; c < 10; c++)
				{
					string fieldName = letter + " " + (c + 1).ToString();
					playerFields[r, c] = spawnField(new Point((r * 40) + 10, (c * 40) + 5), PlayerGameBoard, fieldName, false);
					enemyFields[r, c] = spawnField(new Point((r * 40) + 10, (c * 40) + 5), EnemyGameBoard, fieldName, true);
				}
		}

		private Field spawnField(Point location, Panel parent, string fieldName, bool isEnabled)
		{

			Field field = new Field();
			Controls.Add(field);
			field.Name = fieldName;
			field.MouseHover += fieldTarget;
			field.Parent = parent;
			field.Location = location;
			field.Size = new Size(40, 40);
			field.BackColor = Color.White;

			if (isEnabled == false)
				field.Enabled = false;

			return field;

		}

		private void fieldTarget(object sender, EventArgs arg)
		{
			Field field= (Field)sender;
			TargetFieldLabel.Text = field.Name;
		}

		private void RefreshMap()
		{

			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
					playerFields[r, c].BackColor = Color.White;

		}
		private void AcceptFleetButton_Click(object sender, EventArgs e)
		{
			CreateFleetButton.Enabled = false;
			AcceptFleetButton.Enabled = false;
		}
	}	
}
