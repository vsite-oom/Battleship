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
                    var playerButton = CreateGridButton(false);
                    playerButtons[row, col] = playerButton;
                    PlayerGrid.Children.Add(playerButton);

                    int r = row, c = col; // Capture for lambda
                    var computerButton = CreateGridButton(true);
                    computerButton.Click += (s, e) => ComputerGrid_Click(r, c);
                    computerButtons[row, col] = computerButton;
                    ComputerGrid.Children.Add(computerButton);
                }
            }
        }

        private Button CreateGridButton(bool isComputer)
        {
            return new Button
            {
                Width = 35,
                Height = 35,
                Margin = new Thickness(1),
                Background = isComputer ? Brushes.LightGray : Brushes.LightBlue,
                BorderBrush = Brushes.DarkBlue,
                BorderThickness = new Thickness(1),
                FontSize = 14,
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
            StatusLabel.Text = "Your turn - click on enemy grid to fire!";
            
            // Reset and enable buttons
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    computerButtons[row, col].IsEnabled = true;
                    computerButtons[row, col].Background = Brushes.LightGray;
                    computerButtons[row, col].Content = "";
                    
                    playerButtons[row, col].Background = Brushes.LightBlue;
                    playerButtons[row, col].Content = "";
                }
            }
            
            // Show player ships
            UpdatePlayerGrid();
        }

        private void UpdatePlayerGrid()
        {
            foreach (var ship in playerFleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    playerButtons[square.Row, square.Column].Background = Brushes.Navy;
                    playerButtons[square.Row, square.Column].Content = "⬛";
                    playerButtons[square.Row, square.Column].Foreground = Brushes.DarkGray;
                    playerButtons[square.Row, square.Column].FontSize = 24;
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
                    computerButtons[row, col].Background = Brushes.White;
                    computerButtons[row, col].Content = "○";
                    computerButtons[row, col].Foreground = Brushes.Blue;
                    computerButtons[row, col].FontSize = 24;
                    break;
                case HitResult.Hit:
                    computerButtons[row, col].Background = Brushes.Orange;
                    computerButtons[row, col].Content = "⬛";
                    computerButtons[row, col].Foreground = Brushes.Black;
                    computerButtons[row, col].FontSize = 24;
                    break;
                case HitResult.Sunk:
                    computerButtons[row, col].Background = Brushes.Red;
                    computerButtons[row, col].Content = "⬛";
                    computerButtons[row, col].Foreground = Brushes.White;
                    computerButtons[row, col].FontSize = 24;
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
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1000) };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    ComputerTurn();
                };
                timer.Start();
            }
        }

        private void ComputerTurn()
        {
            var target = computerGunnery.NextTarget();
            var result = playerFleet.Fire(target);
            computerGunnery.ProcessHitResult(result);
            playerGrid.MarkSquare(target.Row, target.Column, result);

            // Update player grid button
            switch (result)
            {
                case HitResult.Missed:
                    playerButtons[target.Row, target.Column].Background = Brushes.White;
                    playerButtons[target.Row, target.Column].Content = "○";
                    playerButtons[target.Row, target.Column].Foreground = Brushes.Blue;
                    playerButtons[target.Row, target.Column].FontSize = 24;
                    break;
                case HitResult.Hit:
                    playerButtons[target.Row, target.Column].Background = Brushes.Orange;
                    playerButtons[target.Row, target.Column].Content = "⬛";
                    playerButtons[target.Row, target.Column].Foreground = Brushes.Black;
                    playerButtons[target.Row, target.Column].FontSize = 24;
                    break;
                case HitResult.Sunk:
                    playerButtons[target.Row, target.Column].Background = Brushes.Red;
                    playerButtons[target.Row, target.Column].Content = "⬛";
                    playerButtons[target.Row, target.Column].Foreground = Brushes.White;
                    playerButtons[target.Row, target.Column].FontSize = 24;
                    break;
            }

            // Check for game over
            if (IsFleetDestroyed(playerFleet))
            {
                StatusLabel.Text = "💥 Computer won! Better luck next time! 💥";
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
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1500) };
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    ComputerTurn();
                };
                timer.Start();
            }
        }

        private void MarkSunkShip(Square lastHit)
        {
            foreach (var ship in computerFleet.Ships)
            {
                if (ship.Squares.Any(s => s.Row == lastHit.Row && s.Column == lastHit.Column))
                {
                    foreach (var square in ship.Squares)
                    {
                        computerButtons[square.Row, square.Column].Background = Brushes.Red;
                        computerButtons[square.Row, square.Column].Content = "X";
                        computerButtons[square.Row, square.Column].Foreground = Brushes.White;
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
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    computerButtons[row, col].IsEnabled = false;
                }
            }
        }
    }
}