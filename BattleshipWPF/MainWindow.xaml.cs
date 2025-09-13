using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Vsite.Oom.Battleship.Model;

namespace BattleshipWPF
{
    public partial class MainWindow : Window
    {
        private GameRules gameRules;
        private Fleet playerFleet;
        private Fleet computerFleet;
        private Gunnery computerGunnery;
        private RecordGrid playerGrid;
        
        private Button[,] playerButtons;
        private Button[,] computerButtons;
        private bool gameStarted = false;
        private bool playerTurn = true;
        private DispatcherTimer computerTimer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            CreateGridButtons();
        }

        private void InitializeGame()
        {
            gameRules = new GameRules();
            playerGrid = new RecordGrid(gameRules.GridRows, gameRules.GridColumns);
        }

        private void CreateGridButtons()
        {
            playerButtons = new Button[10, 10];
            computerButtons = new Button[10, 10];

            // Create player grid buttons
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var playerButton = CreateGridButton();
                    playerButtons[row, col] = playerButton;
                    PlayerGrid.Children.Add(playerButton);

                    int r = row, c = col; // Capture for lambda
                    var computerButton = CreateGridButton();
                    computerButton.Click += (s, e) => ComputerGrid_Click(r, c);
                    computerButtons[row, col] = computerButton;
                    ComputerGrid.Children.Add(computerButton);
                }
            }
        }

        private Button CreateGridButton()
        {
            var backgroundBrush = new SolidColorBrush(Color.FromRgb(244, 244, 244));
            var borderBrush = new SolidColorBrush(Color.FromRgb(173, 178, 181));

			return new Button
            {
                Width = 35,
                Height = 35,
                Margin = new Thickness(1),
                Background = backgroundBrush,
				BorderBrush = borderBrush,
				BorderThickness = new Thickness(1),
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                IsEnabled = false,
                Style = CreateButtonStyle(),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
        }

        private Style CreateButtonStyle()
        {
            var style = new Style(typeof(Button));
            
            // Hover effect
            var trigger = new Trigger { Property = IsMouseOverProperty, Value = true };
            trigger.Setters.Add(new Setter(OpacityProperty, 0.8));
            style.Triggers.Add(trigger);
            
            return style;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StopComputerTimer();
            
            // Create fleets
            var fleetBuilder = new FleetBuilder(gameRules);
            playerFleet = fleetBuilder.CreateFleet();
            computerFleet = fleetBuilder.CreateFleet();
            
            // Initialize computer gunnery
            computerGunnery = new Gunnery(gameRules);
            
            // Reset grids
            playerGrid = new RecordGrid(gameRules.GridRows, gameRules.GridColumns);
            
            gameStarted = true;
            playerTurn = true;
            StatusLabel.Text = "Your turn - click on your opponent's grid to fire!";
            
            // Reset and enable buttons
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var grayBrush = new SolidColorBrush(Color.FromRgb(244, 244, 244));

                    computerButtons[row, col].IsEnabled = true;
                    computerButtons[row, col].Background = grayBrush;
                    computerButtons[row, col].Content = "";
        
                    playerButtons[row, col].Background = grayBrush;
                    playerButtons[row, col].Content = "";
                }
            }
            
            // Show player ships
            UpdatePlayerGrid();
            UpdateGridBorders();
        }

        private void UpdatePlayerGrid()
        {
            foreach (var ship in playerFleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    playerButtons[square.Row, square.Column].Background = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                    playerButtons[square.Row, square.Column].Content = "⬛";
                    playerButtons[square.Row, square.Column].Foreground = Brushes.DimGray;
                }
            }
        }

        private void ComputerGrid_Click(int row, int col)
        {
            if (!gameStarted || !playerTurn)
                return;

            var target = new Square(row, col);
            var result = computerFleet.Fire(target);

            // Update button appearance
            computerButtons[row, col].IsEnabled = false;
            switch (result)
            {
                case HitResult.Missed:
                    computerButtons[row, col].Content = "◉";
                    computerButtons[row, col].Foreground = Brushes.SteelBlue;
                    break;
                case HitResult.Hit:
                    computerButtons[row, col].Content = "⬛";
                    computerButtons[row, col].Foreground = Brushes.Black;
                    break;
                case HitResult.Sunk:
                    MarkSunkShip(target);
                    break;
            }

            // Check for game over
            if (IsFleetDestroyed(computerFleet))
            {
                StatusLabel.Text = "🎉 You won! Congratulations! 🎉";
                StatusLabel.Foreground = Brushes.Gold;
                DisableAllButtons();
                return;
            }

            StatusLabel.Text = result == HitResult.Missed ? "Miss! Computer's turn..." : 
                              result == HitResult.Hit ? "Hit! Continue firing..." :
                              "Ship sunk! Continue firing...";
            StatusLabel.Foreground = Brushes.White;

            if (result == HitResult.Missed)
            {
                playerTurn = false;
                StartComputerTimer(1500);
            }

            UpdateGridBorders();
        }

        private void ComputerTurn()
        {
            if (!gameStarted || playerTurn)
                return;

            var target = computerGunnery.NextTarget();
            var result = playerFleet.Fire(target);
            computerGunnery.ProcessHitResult(result);
            playerGrid.MarkSquare(target.Row, target.Column, result);

            // Update player grid button
            switch (result)
            {
                case HitResult.Missed:
                    playerButtons[target.Row, target.Column].Content = "◉";
                    playerButtons[target.Row, target.Column].Foreground = Brushes.SteelBlue;
                    break;
                case HitResult.Hit:
                    playerButtons[target.Row, target.Column].Content = "⬛";
                    playerButtons[target.Row, target.Column].Foreground = Brushes.Black;
                    break;
                case HitResult.Sunk:
                    MarkSunkShip(target, true);
                    break;
            }

            // Check for game over
            if (IsFleetDestroyed(playerFleet))
            {
                StatusLabel.Text = "💥 You lost! Better luck next time! 💥";
                StatusLabel.Foreground = Brushes.Red;
                DisableAllButtons();
                return;
            }

            StatusLabel.Text = result == HitResult.Missed ? "Computer missed! Your turn..." :
                              result == HitResult.Hit ? "Computer hit! Computer continues..." :
                              "Computer sunk your ship! Computer continues...";

            if (result == HitResult.Missed)
            {
                playerTurn = true;
            }
            else
            {
                StartComputerTimer(1500);
            }

            UpdateGridBorders();
        }

        private void StartComputerTimer(int milliseconds)
        {
            StopComputerTimer();
            
            computerTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(milliseconds) };
            computerTimer.Tick += (s, e) =>
            {
                computerTimer.Stop();
                computerTimer = null;
                ComputerTurn();
            };
            computerTimer.Start();
        }

        private void StopComputerTimer()
        {
            if (computerTimer != null)
            {
                computerTimer.Stop();
                computerTimer = null;
            }
        }

        private void MarkSunkShip(Square lastHit, bool isPlayerShip = false)
        {
            var fleet = isPlayerShip ? playerFleet : computerFleet;
            var buttons = isPlayerShip ? playerButtons : computerButtons;
            
            foreach (var ship in fleet.Ships)
            {
                if (ship.Squares.Any(s => s.Row == lastHit.Row && s.Column == lastHit.Column))
                {
                    foreach (var square in ship.Squares)
                    {
                        buttons[square.Row, square.Column].Content = "X";
                        buttons[square.Row, square.Column].Foreground = Brushes.IndianRed;
                    }
                    break;
                }
            }
        }

        private bool IsFleetDestroyed(Fleet fleet)
        {
            return fleet.Ships.All(ship => ship.Squares.All(square => 
                square.SquareState == SquareState.Hit || square.SquareState == SquareState.Sunk));
        }

        private void DisableAllButtons()
        {
            gameStarted = false;
            StopComputerTimer();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    computerButtons[row, col].IsEnabled = false;
                }
            }
        }

        private void UpdateGridBorders()
        {
            if (playerTurn)
            {
                PlayerGridBorder.BorderBrush = Brushes.Gray;
                ComputerGridBorder.BorderBrush = Brushes.ForestGreen;
            }
            else
            {
                PlayerGridBorder.BorderBrush = Brushes.Red;
                ComputerGridBorder.BorderBrush = Brushes.Gray;
            }
        }
    }
}