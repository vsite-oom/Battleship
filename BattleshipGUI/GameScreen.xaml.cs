using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Model;

namespace BattleshipGUI;

public partial class GameScreen : UserControl
{
    private readonly MainWindow _main;
    private readonly Fleet _playerFleet;
    private readonly Fleet _enemyFleet;
    private readonly BoardControl _enemyBoard;
    private readonly BoardControl _playerBoard;
    private readonly Gunnery _cpuGunnery;

    private readonly bool[,] _playerOccupied;
    private readonly bool[,] _enemyOccupied = new bool[10, 10];

    private readonly string?[,] _playerShots = new string?[10, 10];
    private readonly string?[,] _enemyShots = new string?[10, 10];

    private bool _playerTurn = true;
    private bool _gameOver = false;
    private int _shotsFired = 0;
    private int _hitsLanded = 0;

    private readonly string[] _shipNames = { "CARRIER", "BATTLESHIP", "CRUISER", "SUBMARINE", "DESTROYER" };
    private readonly int[] _shipLengths = { 5, 4, 3, 3, 2 };

    private readonly int[] _playerShipHits;
    private readonly int[] _enemyShipHits;
    private readonly bool[] _playerShipSunk;
    private readonly bool[] _enemyShipSunk;

    private readonly List<List<(int Row, int Col)>> _enemyShipPositions = new();
    private readonly List<List<(int Row, int Col)>> _playerShipPositions;

    private static readonly SolidColorBrush MissBrush = new(Color.FromArgb(80, 92, 255, 138));
    private static readonly SolidColorBrush HitBrush = new(Color.FromArgb(180, 255, 92, 92));
    private static readonly SolidColorBrush SunkBrush = new(Color.FromArgb(220, 255, 60, 60));
    private static readonly SolidColorBrush ShipBrush = new(Color.FromArgb(90, 92, 255, 138));

    public GameScreen(MainWindow main, Fleet playerFleet, List<List<Square>> playerShipSquares, bool[,] playerOccupied)
    {
        InitializeComponent();
        _main = main;
        _playerFleet = playerFleet;
        _playerOccupied = playerOccupied;

        _playerShipPositions = playerShipSquares
            .Select(s => s.Select(sq => (sq.Position.Row, sq.Position.Column)).ToList())
            .ToList();

        _playerShipHits = new int[_shipLengths.Length];
        _enemyShipHits = new int[_shipLengths.Length];
        _playerShipSunk = new bool[_shipLengths.Length];
        _enemyShipSunk = new bool[_shipLengths.Length];

        BuildEnemyFleet();

        _cpuGunnery = new Gunnery(10, 10, _shipLengths);

        _enemyBoard = new BoardControl(32);
        _playerBoard = new BoardControl(20);

        EnemyBoardHost.Child = _enemyBoard;
        PlayerBoardHost.Child = _playerBoard;

        _enemyBoard.CellClicked += OnEnemyCellClicked;
        _enemyBoard.CellHovered += OnEnemyCellHovered;
        _enemyBoard.CellLeft += () =>
        {
            TargetLabel.Text = "TGT ▶ [——]";
            InstructionLabel.Text = _playerTurn ? "CLICK TO FIRE TORPEDO" : "STAND BY — INCOMING";
        };

        DrawPlayerShips();

        AddLog("> ENEMY FLEET DEPLOYED");
        AddLog("> SONAR ARRAY ENGAGED");
        AddLog("> AWAITING ORDERS, ADMIRAL");

        UpdateHUD();
        UpdateFleetStatus();
        UpdateEnemyIntel();
    }

    private void BuildEnemyFleet()
    {
        _enemyShipPositions.Clear();
        Array.Clear(_enemyOccupied, 0, _enemyOccupied.Length);

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
                bool valid = true;

                var cells = new List<(int, int)>();
                for (int j = 0; j < length; j++)
                {
                    int rr = horiz ? r : r + j;
                    int cc = horiz ? c + j : c;
                    if (rr >= 10 || cc >= 10 || _enemyOccupied[rr, cc])
                    {
                        valid = false;
                        break;
                    }
                    cells.Add((rr, cc));
                }

                if (valid)
                {
                    foreach (var (row, col) in cells)
                        _enemyOccupied[row, col] = true;
                    _enemyShipPositions.Add(cells);
                    break;
                }
                attempts++;
            }
        }
    }

    private void DrawPlayerShips()
    {
        var borderBrush = (SolidColorBrush)FindResource("CrtFgBrush");
        foreach (var ship in _playerShipPositions)
        {
            foreach (var (r, c) in ship)
                _playerBoard.SetCellColor(r, c, ShipBrush, borderBrush);
        }
    }

    private void OnEnemyCellClicked(int row, int col)
    {
        if (!_playerTurn || _gameOver) return;
        if (_playerShots[row, col] != null) return;

        _shotsFired++;
        bool hit = _enemyOccupied[row, col];
        string coord = $"{(char)('A' + col)}{row + 1}";

        if (hit)
        {
            _playerShots[row, col] = "hit";
            _hitsLanded++;
            _enemyBoard.SetCellColor(row, col, HitBrush);
            _enemyBoard.SetCellContent(row, col, "X", Brushes.White);

            int shipIdx = FindEnemyShip(row, col);
            if (shipIdx >= 0)
            {
                _enemyShipHits[shipIdx]++;
                if (_enemyShipHits[shipIdx] == _shipLengths[shipIdx])
                {
                    _enemyShipSunk[shipIdx] = true;
                    MarkEnemyShipSunk(shipIdx);
                    AddLog($"> [{coord}] ▶ DIRECT HIT");
                    AddLog($"> *** ENEMY {_shipNames[shipIdx]} SUNK ***");
                }
                else
                {
                    AddLog($"> [{coord}] ▶ DIRECT HIT");
                }
            }
        }
        else
        {
            _playerShots[row, col] = "miss";
            _enemyBoard.SetCellColor(row, col, MissBrush);
            _enemyBoard.SetCellContent(row, col, "·", (SolidColorBrush)FindResource("CrtDimBrush"));
            AddLog($"> [{coord}] ▶ SPLASH");
        }

        UpdateHUD();
        UpdateEnemyIntel();

        if (_enemyShipSunk.All(s => s))
        {
            _gameOver = true;
            AddLog("> ★★★  ENEMY FLEET ANNIHILATED  ★★★");
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                _main.ShowScreen(new EndScreen(_main, true, _shotsFired, _hitsLanded));
            };
            timer.Start();
            return;
        }

        if (!hit)
        {
            _playerTurn = false;
            UpdateHUD();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(800) };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                CpuTurn();
            };
            timer.Start();
        }
    }

    private void CpuTurn()
    {
        if (_gameOver) return;

        var target = _cpuGunnery.Next();
        int r = target.Position.Row;
        int c = target.Position.Column;
        string coord = $"{(char)('A' + c)}{r + 1}";

        bool hit = _playerOccupied[r, c];
        HitResult result;

        if (hit)
        {
            int shipIdx = FindPlayerShip(r, c);
            _playerShipHits[shipIdx]++;
            bool sunk = _playerShipHits[shipIdx] == _shipLengths[shipIdx];

            if (sunk)
            {
                _playerShipSunk[shipIdx] = true;
                result = HitResult.Sunken;
                MarkPlayerShipSunk(shipIdx);
                AddLog($"> CPU [{coord}] ▶ HIT ON FLEET");
                AddLog($"> *** YOUR {_shipNames[shipIdx]} SUNK ***");
            }
            else
            {
                result = HitResult.Hit;
                AddLog($"> CPU [{coord}] ▶ HIT ON FLEET");
            }

            _enemyShots[r, c] = "hit";
            _playerBoard.SetCellColor(r, c, HitBrush);
        }
        else
        {
            result = HitResult.Missed;
            _enemyShots[r, c] = "miss";
            _playerBoard.SetCellColor(r, c, MissBrush);
            AddLog($"> CPU [{coord}] ▶ MISS");
        }

        _cpuGunnery.ProcessHitResult(result);
        UpdateFleetStatus();

        if (_playerShipSunk.All(s => s))
        {
            _gameOver = true;
            AddLog("> ☠☠☠  YOUR FLEET IS LOST  ☠☠☠");
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5) };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                _main.ShowScreen(new EndScreen(_main, false, _shotsFired, _hitsLanded));
            };
            timer.Start();
            return;
        }

        if (hit)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(800) };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                CpuTurn();
            };
            timer.Start();
        }
        else
        {
            _playerTurn = true;
            UpdateHUD();
        }
    }

    private int FindEnemyShip(int row, int col)
    {
        for (int i = 0; i < _enemyShipPositions.Count; i++)
            if (_enemyShipPositions[i].Any(p => p.Row == row && p.Col == col))
                return i;
        return -1;
    }

    private int FindPlayerShip(int row, int col)
    {
        for (int i = 0; i < _playerShipPositions.Count; i++)
            if (_playerShipPositions[i].Any(p => p.Row == row && p.Col == col))
                return i;
        return -1;
    }

    private void MarkEnemyShipSunk(int shipIdx)
    {
        foreach (var (r, c) in _enemyShipPositions[shipIdx])
        {
            _enemyBoard.SetCellColor(r, c, SunkBrush);
            _enemyBoard.SetCellContent(r, c, "X", Brushes.White);
        }
    }

    private void MarkPlayerShipSunk(int shipIdx)
    {
        foreach (var (r, c) in _playerShipPositions[shipIdx])
            _playerBoard.SetCellColor(r, c, SunkBrush);
    }

    private void OnEnemyCellHovered(int row, int col)
    {
        string coord = $"{(char)('A' + col)}{row + 1}";
        TargetLabel.Text = $"TGT ▶ [{coord}]";
        InstructionLabel.Text = _playerTurn ? "CLICK TO FIRE TORPEDO" : "STAND BY — INCOMING";
    }

    private void UpdateHUD()
    {
        int acc = _shotsFired > 0 ? (int)Math.Round(100.0 * _hitsLanded / _shotsFired) : 0;
        StatsLabel.Text = $"SHOTS {_shotsFired}  ·  HITS {_hitsLanded}  ·  ACC {acc}%";
        TurnLabel.Text = _playerTurn ? "YOUR MOVE" : "CPU FIRING...";
        TargetLabel.Text = "TGT ▶ [——]";
        InstructionLabel.Text = _playerTurn ? "CLICK TO FIRE TORPEDO" : "STAND BY — INCOMING";
    }

    private void UpdateFleetStatus()
    {
        FleetStatusPanel.Children.Clear();
        for (int i = 0; i < _shipLengths.Length; i++)
        {
            var row = new DockPanel { Margin = new Thickness(0, 2, 0, 2) };
            var name = new TextBlock
            {
                Text = _shipNames[i],
                FontFamily = new FontFamily("Consolas"),
                FontSize = 9,
                Foreground = _playerShipSunk[i]
                    ? (Brush)FindResource("CrtRedBrush")
                    : (Brush)FindResource("CrtFgBrush")
            };
            var status = new TextBlock
            {
                Text = _playerShipSunk[i]
                    ? "DESTROYED"
                    : $"{_shipLengths[i] - _playerShipHits[i]}/{_shipLengths[i]} OK",
                FontFamily = new FontFamily("Consolas"),
                FontSize = 9,
                Foreground = _playerShipSunk[i]
                    ? (Brush)FindResource("CrtRedBrush")
                    : (Brush)FindResource("CrtDimBrush"),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            DockPanel.SetDock(status, Dock.Right);
            row.Children.Add(status);
            row.Children.Add(name);
            FleetStatusPanel.Children.Add(row);
        }
    }

    private void UpdateEnemyIntel()
    {
        EnemyIntelPanel.Children.Clear();
        for (int i = 0; i < _shipLengths.Length; i++)
        {
            var row = new DockPanel { Margin = new Thickness(0, 2, 0, 2) };
            string label = _enemyShipSunk[i] ? $"{_shipNames[i]} ✕"
                : _enemyShipHits[i] > 0 ? $"{_shipNames[i]} ⚠"
                : $"{_shipNames[i]} ?";
            string info = _enemyShipSunk[i] ? "CONFIRMED SUNK"
                : _enemyShipHits[i] > 0 ? $"{_enemyShipHits[i]}/{_shipLengths[i]} HITS"
                : "POSITION UNKNOWN";

            Brush fg = _enemyShipSunk[i] ? (Brush)FindResource("CrtRedBrush")
                : _enemyShipHits[i] > 0 ? (Brush)FindResource("CrtBrightBrush")
                : (Brush)FindResource("CrtDimBrush");

            var name = new TextBlock
            {
                Text = label,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 9,
                Foreground = fg
            };
            var status = new TextBlock
            {
                Text = info,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 9,
                Foreground = (Brush)FindResource("CrtDimBrush"),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            DockPanel.SetDock(status, Dock.Right);
            row.Children.Add(status);
            row.Children.Add(name);
            EnemyIntelPanel.Children.Add(row);
        }
    }

    private void AddLog(string message)
    {
        Brush fg = (Brush)FindResource("CrtFgBrush");
        if (message.Contains("SUNK")) fg = new SolidColorBrush(Color.FromRgb(255, 184, 184));
        else if (message.Contains("HIT")) fg = (Brush)FindResource("CrtBrightBrush");

        LogPanel.Children.Add(new TextBlock
        {
            Text = message,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 11,
            Foreground = fg,
            Margin = new Thickness(0, 1, 0, 1)
        });
        LogScroller.ScrollToEnd();
    }

    private void Abort_Click(object sender, RoutedEventArgs e)
    {
        _main.ShowScreen(new MenuScreen(_main));
    }
}
