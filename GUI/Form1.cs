using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
		int totalShips;

		public GameManager(int row, int column, List<int> ships)
		{
			fleet = new Fleet();
			shipwright = new Shipwright(row, column);
			gunner = new Gunner(row, column, ships);
			fleet = shipwright.CreateFleet(ships);
			totalShips = ships.Count();
		}

		public bool isDestroyed()
		{
			int i = fleet.Ships.Count(s => s.Squares.All(sq => sq.SquareState == SquareState.Sunken));
			return i == totalShips;
		}

	}

	public enum CurrentPlayer
	{
		Player,
		Enemy
	}
	public partial class Form1 : Form
	{
		Field[,] playerFields = new Field[10, 10];
		Field[,] enemyFields = new Field[10, 10];

		List<int> ships = new List<int>() { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

		GameManager playerManager;
		GameManager enemyManager;
		CurrentPlayer currentPlayer;

		Random random = new Random();

		public Form1()
		{
			InitializeComponent();
			spawnFields();
			playerManager = new GameManager(10, 10, ships);
			enemyManager = new GameManager(10, 10, ships);

			InfoBoardTextBox.AppendText("Chose your starting fleet.");

		}

		private void CreateFleetButton_Click(object sender, EventArgs e)
		{
			RefreshMap();

			AcceptFleetButton.Enabled = true;

			playerManager.fleet = playerManager.shipwright.CreateFleet(ships);
			enemyManager.fleet = enemyManager.shipwright.CreateFleet(ships);

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
					playerFields[r, c] = spawnField(new Point((r * 40) + 10, (c * 40) + 5), PlayerGameBoard, fieldName, false,r,c);
					enemyFields[r, c] = spawnField(new Point((r * 40) + 10, (c * 40) + 5), EnemyGameBoard, fieldName, true, r, c);
				}
		}

		private Field spawnField(Point location, Panel parent, string fieldName, bool isEnabled, int row, int column)
		{

			Field field = new Field(row,column);
			Controls.Add(field);
			field.Name = fieldName;
			field.MouseHover += fieldTarget;
			field.MouseClick += confirmTarget;
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

		private void confirmTarget(object sender, EventArgs arg)
		{
			if (isValid() == false)
				return;

			HitResult hitResult;

			Field field = (Field)sender;
			hitResult = enemyManager.fleet.Hit(field.Position());

			MarkResult(field.Position(), hitResult, enemyFields);

			if (enemyManager.isDestroyed())
				InfoBoardTextBox.AppendText("YOU WIN!");

			if (playerManager.isDestroyed())
				InfoBoardTextBox.AppendText("YOU LOSE!");

			currentPlayer = CurrentPlayer.Enemy;
			EnemyTurn();

		}

		private void EnemyTurn()
		{
			InfoBoardTextBox.AppendText("\r\nEnemy turn!");
			InfoBoardTextBox.AppendText("\r\nEnemy is preparing to fire");

			EnemyAI.Start();

		}

		private void MarkResult(Square target, HitResult hitResult, Field[,] field)
		{
			switch (hitResult)
			{
				case HitResult.Missed:
					field[target.Row, target.Column].BackColor = Color.Gray;
					InfoBoardTextBox.AppendText("\r\nMiss!");
					break;
				case HitResult.Hit:
					field[target.Row, target.Column].BackColor = Color.Red;
					InfoBoardTextBox.AppendText("\r\nHit!");
					break;
				case HitResult.Sunk:
					field[target.Row, target.Column].BackColor = Color.DarkRed;
					InfoBoardTextBox.AppendText("\r\nDestroyed");
					break;
				default:
					Debug.Assert(false);
					break;
			}
		}

		private void RefreshMap()
		{

			for (int r = 0; r < 10; r++)
				for (int c = 0; c < 10; c++)
					playerFields[r, c].BackColor = Color.White;

		}

		private bool isValid()
		{
			return CurrentPlayer.Player == currentPlayer;
		}
		private void AcceptFleetButton_Click(object sender, EventArgs e)
		{
			CreateFleetButton.Enabled = false;
			AcceptFleetButton.Enabled = false;

			InfoBoardTextBox.AppendText("\r\nRandomly choosing which player goes first...");

			var values = Enum.GetValues(typeof(CurrentPlayer));
			currentPlayer = (CurrentPlayer)values.GetValue(random.Next(values.Length));

			switch (currentPlayer)
			{
				case CurrentPlayer.Player:
					InfoBoardTextBox.AppendText("\r\nYou go first, chose field to fire at!");
					break;
				case CurrentPlayer.Enemy:
					EnemyTurn();
					break;
				default:
					Debug.Assert(false);
					break;
			}

		}

		private void EnemyAI_Tick(object sender, EventArgs e)
		{
			HitResult hitResult;
			Square target = enemyManager.gunner.NextTarget();
			hitResult = playerManager.fleet.Hit(target);
			enemyManager.gunner.ProcessHitResult(hitResult);
			InfoBoardTextBox.AppendText("\r\nEnemy is firing at " + (char)(target.Row + 'A') + (target.Column + 1));

			MarkResult(target, hitResult, playerFields);

			if (enemyManager.isDestroyed())
				InfoBoardTextBox.AppendText("YOU WIN!");

			if (playerManager.isDestroyed())
				InfoBoardTextBox.AppendText("YOU LOSE!");


			currentPlayer = CurrentPlayer.Player;

			EnemyAI.Stop();
		}
	}	
}
