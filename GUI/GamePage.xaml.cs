using Battleship.GUI.Models;

namespace GUI;

public partial class GamePage : ContentPage
{
	private GameModel friendlyGrid;
	private GameModel enemyGrid;
	public GamePage(int rows, int columns)
	{
		InitializeComponent();
	}

	private void LoadGameData(int rows, int columns)
	{
		friendlyGrid = new GameModel(rows, columns);
		enemyGrid = new GameModel(rows, columns);
	}
}