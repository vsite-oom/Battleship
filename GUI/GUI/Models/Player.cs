using System.Collections.ObjectModel;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI.Models;

public class Player
{
    public ObservableCollection<ObservableCollection<DisplaySquare>> FleetBoard { get; } = new();
    public ObservableCollection<ObservableCollection<DisplaySquare>> ShotsBoard { get; } = new();
    public Gunnery Gunnery { get; set; } = null!;
    public Fleet Fleet { get; set; } = null!;
    
    
    public void Initialize(int rows, int columns, int[] shipLengths)
    {
        FleetBoard.Clear();
        ShotsBoard.Clear();
        FleetBuilder fleetBuilder = new(rows, columns, shipLengths);
        Fleet = fleetBuilder.CreateFleet();
        Gunnery = new Gunnery(rows, columns, shipLengths);
        
        GenerateBoards(rows, columns);
    }
    
    private void GenerateBoards(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            FleetBoard.Add(new ObservableCollection<DisplaySquare>());

            for (int j = 0; j < columns; j++)
            {
                FleetBoard[i].Add(new DisplaySquare(i, j));
            }
        }

        foreach (var ship in Fleet.Ships)
        {
            foreach (var square in ship.Squares)
            {
                FleetBoard[square.Row][square.Column].IsShip = true;
            }
        }

        for (int i = 0; i < rows; i++)
        {
            ShotsBoard.Add(new ObservableCollection<DisplaySquare>());

            for (int j = 0; j < columns; j++)
            {
                ShotsBoard[i].Add(new DisplaySquare(i, j));
            }
        }
    }
}