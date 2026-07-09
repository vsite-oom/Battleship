using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Model;

namespace BattleshipGUI;

public partial class PlacementScreen : UserControl
{
    private readonly MainWindow _main;
    private readonly BoardControl _board;
    private bool _horizontal = true;
    private int _currentShipIndex = 0;
    private readonly int[] _shipLengths = { 5, 4, 3, 3, 2 };
    private readonly string[] _shipNames = { "CARRIER", "BATTLESHIP", "CRUISER", "SUBMARINE", "DESTROYER" };

    private readonly List<List<(int Row, int Col)>> _placedShips = new();
    private readonly bool[,] _occupied = new bool[10, 10];
    private readonly List<(int Row, int Col)> _previewCells = new();

    public PlacementScreen(MainWindow main)
    {
        InitializeComponent();
        _main = main;
        _board = new BoardControl(32);
        BoardHost.Child = _board;

        _board.CellClicked += OnCellClicked;
        _board.CellHovered += OnCellHovered;
        _board.CellLeft += OnCellLeft;

        UpdateUI();
        AutoDeploy();

        Loaded += (_, _) => Focus();
    }

    private void UpdateUI()
    {
        RotationLabel.Text = $"ROT: {(_horizontal ? "HORIZ" : "VERT")}  [R]";
        RotateButton.Content = $"ROTATE: {(_horizontal ? "HORIZONTAL" : "VERTICAL")}";
        CommenceButton.IsEnabled = _placedShips.Count == _shipLengths.Length;

        if (_currentShipIndex < _shipLengths.Length)
            StatusText.Text = $"▶ PLACING {_shipNames[_currentShipIndex]} ({_shipLengths[_currentShipIndex]})  —  [R] ROTATE";
        else
            StatusText.Text = "▶ ALL SHIPS DEPLOYED — COMMENCE BATTLE";

        UpdateDock();
    }

    private void UpdateDock()
    {
        ShipDock.Children.Clear();
        for (int i = 0; i < _shipLengths.Length; i++)
        {
            bool placed = i < _placedShips.Count;
            bool active = i == _currentShipIndex;

            var panel = new StackPanel { Margin = new Thickness(0, 2, 0, 2) };

            var header = new DockPanel();
            var nameBlock = new TextBlock
            {
                Text = _shipNames[i],
                FontFamily = new FontFamily("Consolas"),
                FontSize = 10,
                Foreground = active ? (Brush)FindResource("CrtBrightBrush") : (Brush)FindResource("CrtFgBrush")
            };
            var statusBlock = new TextBlock
            {
                Text = placed ? $"DEPLOYED · LEN {_shipLengths[i]}" : $"— UNASSIGNED — · LEN {_shipLengths[i]}",
                FontFamily = new FontFamily("Consolas"),
                FontSize = 10,
                Foreground = (Brush)FindResource("CrtDimBrush")
            };

            header.Children.Add(nameBlock);
            panel.Children.Add(header);
            panel.Children.Add(statusBlock);

            var shipRow = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 3, 0, 0) };
            for (int s = 0; s < _shipLengths[i]; s++)
            {
                var cell = new Border
                {
                    Width = 16, Height = 16,
                    Margin = new Thickness(1, 0, 1, 0),
                    BorderThickness = new Thickness(1),
                    BorderBrush = placed ? (Brush)FindResource("CrtFgBrush") : (Brush)FindResource("CrtDimBrush"),
                    Background = placed
                        ? new SolidColorBrush(Color.FromArgb(90, 92, 255, 138))
                        : new SolidColorBrush(Color.FromArgb(30, 92, 255, 138))
                };
                shipRow.Children.Add(cell);
            }
            panel.Children.Add(shipRow);

            ShipDock.Children.Add(panel);
        }
    }

    private bool CanPlace(int row, int col, int length, bool horizontal)
    {
        for (int i = 0; i < length; i++)
        {
            int r = horizontal ? row : row + i;
            int c = horizontal ? col + i : col;
            if (r < 0 || r >= 10 || c < 0 || c >= 10) return false;

            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    int nr = r + dr;
                    int nc = c + dc;
                    if (nr >= 0 && nr < 10 && nc >= 0 && nc < 10 && _occupied[nr, nc])
                        return false;
                }
            }
        }
        return true;
    }

    private void PlaceShipAt(int row, int col, int length, bool horizontal)
    {
        var cells = new List<(int, int)>();
        var shipBrush = new SolidColorBrush(Color.FromArgb(90, 92, 255, 138));
        var borderBrush = (SolidColorBrush)FindResource("CrtFgBrush");

        for (int i = 0; i < length; i++)
        {
            int r = horizontal ? row : row + i;
            int c = horizontal ? col + i : col;
            _occupied[r, c] = true;
            cells.Add((r, c));
            _board.SetCellColor(r, c, shipBrush, borderBrush);
        }
        _placedShips.Add(cells);
        _currentShipIndex = _placedShips.Count;
        UpdateUI();
    }

    private void OnCellClicked(int row, int col)
    {
        if (_currentShipIndex >= _shipLengths.Length) return;
        int length = _shipLengths[_currentShipIndex];
        if (!CanPlace(row, col, length, _horizontal)) return;
        ClearPreview();
        PlaceShipAt(row, col, length, _horizontal);
    }

    private void OnCellHovered(int row, int col)
    {
        ClearPreview();
        if (_currentShipIndex >= _shipLengths.Length) return;

        int length = _shipLengths[_currentShipIndex];
        bool valid = CanPlace(row, col, length, _horizontal);

        var brush = valid
            ? new SolidColorBrush(Color.FromArgb(50, 92, 255, 138))
            : new SolidColorBrush(Color.FromArgb(50, 255, 92, 92));
        var border = valid
            ? new SolidColorBrush(Color.FromArgb(120, 92, 255, 138))
            : new SolidColorBrush(Color.FromArgb(120, 255, 92, 92));

        for (int i = 0; i < length; i++)
        {
            int r = _horizontal ? row : row + i;
            int c = _horizontal ? col + i : col;
            if (r >= 0 && r < 10 && c >= 0 && c < 10 && !_occupied[r, c])
            {
                _board.SetCellColor(r, c, brush, border);
                _previewCells.Add((r, c));
            }
        }
    }

    private void OnCellLeft()
    {
        ClearPreview();
    }

    private void ClearPreview()
    {
        foreach (var (r, c) in _previewCells)
        {
            if (!_occupied[r, c])
                _board.ClearCell(r, c);
        }
        _previewCells.Clear();
    }

    private void ClearAllPlacements()
    {
        _placedShips.Clear();
        Array.Clear(_occupied, 0, _occupied.Length);
        _currentShipIndex = 0;
        _board.ClearAll();
        UpdateUI();
    }

    private void AutoDeploy()
    {
        ClearAllPlacements();
        var rng = new Random();

        for (int i = 0; i < _shipLengths.Length; i++)
        {
            int length = _shipLengths[i];
            int attempts = 0;
            while (attempts < 2000)
            {
                bool horiz = rng.Next(2) == 0;
                int r = rng.Next(10);
                int c = rng.Next(10);
                if (CanPlace(r, c, length, horiz))
                {
                    PlaceShipAt(r, c, length, horiz);
                    break;
                }
                attempts++;
            }
        }
    }

    private void AutoDeploy_Click(object sender, RoutedEventArgs e) => AutoDeploy();
    private void ClearAll_Click(object sender, RoutedEventArgs e) => ClearAllPlacements();

    private void Rotate_Click(object sender, RoutedEventArgs e)
    {
        _horizontal = !_horizontal;
        UpdateUI();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.R)
        {
            _horizontal = !_horizontal;
            UpdateUI();
        }
    }

    private void Commence_Click(object sender, RoutedEventArgs e)
    {
        var playerFleet = new Fleet();
        var playerSquares = new List<List<Square>>();
        foreach (var ship in _placedShips)
        {
            var squares = ship.Select(p => new Square(p.Row, p.Col)).ToList();
            playerFleet.CreateShip(squares);
            playerSquares.Add(squares);
        }

        _main.ShowScreen(new GameScreen(_main, playerFleet, playerSquares, _occupied));
    }
}
