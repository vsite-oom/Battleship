using System;
using Avalonia.Controls;
using Avalonia.Layout;


namespace Vsite.Oom.Battleship.GUI.Views;

public partial class GameView : UserControl
{
    public GameView()
    {
        InitializeComponent();
    }

    private void Control_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        var orientation = Bounds.Width > Bounds.Height ? Orientation.Horizontal : Orientation.Vertical;
        
        if(orientation == Orientation.Vertical)
        {
            ShotsBoard.SetValue(Grid.RowProperty, 1);
            ShotsBoard.SetValue(Grid.ColumnProperty, 0);
            
            var vm = DataContext as ViewModels.GameViewModel;
            
            var cellSize = (Bounds.Width - 150) / vm!.Game.Columns;
            
            cellSize = cellSize < 15 ? 15 : cellSize;
            cellSize = cellSize > 30 ? 30 : cellSize;
            
            BoardGrid.VerticalAlignment = VerticalAlignment.Top;
            
            FleetBoard.CellSize = cellSize;
            ShotsBoard.CellSize = cellSize;
        }
        else
        {
            ShotsBoard.SetValue(Grid.RowProperty, 0);
            ShotsBoard.SetValue(Grid.ColumnProperty, 1);
            FleetBoard.CellSize = 30;
            ShotsBoard.CellSize = 30;
            
            BoardGrid.VerticalAlignment = VerticalAlignment.Center;
        }

        if (OperatingSystem.IsAndroid())
        {
            BoardGrid.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}