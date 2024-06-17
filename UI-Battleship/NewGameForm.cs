using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace UI_Battleship
{
    public partial class NewGameForm : Form
    {
        private const int GridSize = 10; // Assuming a 10x10 grid
        private static int[] shipLengths = { 5, 4, 3, 3, 2 }; // Example ship lengths

        private bool isHorizontal = true; // Ship placement orientation

        private Button[,] playerButtons = new Button[10, 10];
        private Point? selectedStart = null;
        private int currentShipSize;
        private List<Point> availablePlacements = new List<Point>();

        private FleetGrid playerFleetGrid;
        private Fleet playerFleet;
        private Queue<int> playerShipSizes = new Queue<int>(shipLengths);

        private  FleetBuilder pcFleetBuilder;
        private Fleet pcFleet;

        enum GamePhase { Initialization, ShipPlacement, Gameplay}
        private GamePhase currentGamePhase;

        public NewGameForm()
        {
            InitializeComponent();

            InitializeFleetsAndGridUI();

            //PlayerPlacementPhase();
        }

        private void InitializeFleetsAndGridUI()
        {
            pcFleetBuilder = new FleetBuilder(GridSize, GridSize, shipLengths); //also has FleetGrid
            pcFleet = pcFleetBuilder.CreateFleet(); //Positions of Ships automatically added

            playerFleetGrid = new FleetGrid(GridSize, GridSize); // Empty FleetGrid
            playerFleet = new Fleet(); //Empty player fleet, need to add Ships with Square coordinates

            InitializeGridsUI();

            currentGamePhase = GamePhase.ShipPlacement;
        }

        private void InitializeGridsUI()
        {
            Panel gridPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(300, 300) 
            };

            Controls.Add(gridPanel); // Add the panel to the form's controls


            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    Button button = new Button
                    {
                        Width = 30,
                        Height = 30,
                        Location = new Point(column * 30, row * 30),
                        Tag = new Point(row, column) 
                        // Store the row and column as a Point in the Tag property for later use
                    };

                    // Assign an event handler for the Click event if needed
                    button.Click += GridButton_Click;
                    playerButtons[row, column] = button;
                    gridPanel.Controls.Add(button);
                }
            }
        }

        private void GridButton_Click(object? sender, EventArgs e)
        {
            if (currentGamePhase == GamePhase.ShipPlacement)
            {
                if (playerShipSizes.Count > 0)
                {
                    Button clickedButton = sender as Button;
                    Point location = (Point)clickedButton.Tag;

                    if (selectedStart == null)
                    {
                        // First click selects the start of the ship
                        selectedStart = location;
                        HighlightAvailablePlacements(location);
                    }
                    else
                    {
                        // Second click selects the direction and places the ship
                        // PlaceShip(location);
                        selectedStart = null;

                        // Kreiramo List<Square> ship = za sve koji su u direction prvi element do zadnjeg
                        // playerFleet.CreateShip(ship);
                        // playerFleetGrid.EliminateSquares(svi okolo broda...)


                    }
                }
                else
                {
                    currentGamePhase = GamePhase.Gameplay;
                }

            }
            else if (currentGamePhase == GamePhase.Gameplay)
            {

            }



        }

        private void PlayerPlacementPhase()
        {
            Queue<int> shipSizes = new Queue<int>(shipLengths);

            while (shipSizes.Count > 0)
            {
                var availablePlacements = playerFleetGrid.GetAvailablePlacements(shipSizes.First());
                foreach (var placements in availablePlacements)
                {
                    //HighlightAvailablePlacements();
                }
            }
        }

        private void HighlightAvailablePlacements(Point start)
        {
            // Clear previous highlights
            foreach (var point in availablePlacements)
            {
                playerButtons[point.X, point.Y].BackColor = SystemColors.Control;
            }

            // Get available placements for current ship size
            // Assuming fleetGrid is an instance of FleetGrid with implemented GetAvailablePlacements method
            availablePlacements.Clear();
    
            foreach (var placement in playerFleetGrid.GetAvailablePlacements(5))
            {
                if (placement.First().Row == start.X && placement.First().Column == start.Y || placement.Last().Row == start.X && placement.Last().Column == start.Y)
                {
                    foreach (var square in placement)
                    {
                        var point = new Point(square.Row, square.Column);
                        availablePlacements.Add(point);
                        playerButtons[point.X, point.Y].BackColor = Color.LightBlue;
                    }
                }
            }
        }



    }
}
