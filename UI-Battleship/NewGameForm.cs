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
        private static int[] shipLengths = { 5, 4, 3, 3, 3, 2, 2, 2, 2}; // Example ship lengths

        private Fleet playerFleet;
        private Fleet pcFleet;

        private Gunnery shipGunnery;


        private Button[,] playerButtonsGrid = new Button[10, 10];
        private Button[,] pcButtonsGrid = new Button[10, 10];


        private Point? selectedStart = null;
        private int currentShipSize;
        private List<Point> availablePlacements = new List<Point>();

        private FleetGrid playerFleetGrid;
        private Queue<int> playerShipSizes = new Queue<int>(shipLengths);

        private  FleetBuilder pcFleetBuilder;
        private FleetBuilder playerFleetBuilder;

        private FleetGrid pcFleetGrid;

        public NewGameForm()
        {
            InitializeComponent();

            InitializeFleetsAndGridUI();

        }

        private void InitializeFleetsAndGridUI()
        {
            pcFleetBuilder = new FleetBuilder(GridSize, GridSize, shipLengths); //also has FleetGrid
            pcFleet = pcFleetBuilder.CreateFleet(); //Positions of Ships automatically added
            
            playerFleetBuilder = new FleetBuilder(GridSize, GridSize, shipLengths); //also has FleetGrid
            playerFleet = playerFleetBuilder.CreateFleet(); //Empty player fleet, need to add Ships with Square coordinates

            InitializeGridsUI();

        }

        private void InitializeGridsUI()
        {
            Panel playerGrid = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(300, 300) 
            };

            Panel pcGrid = new Panel
            {
                Location = new Point(330, 10),
                Size = new Size(300, 300)
            };

            Controls.Add(playerGrid); // Add the panel to the form's controls
            Controls.Add(pcGrid);

            //player grid
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
                    if(pcFleet.Ships.FirstOrDefault(x => x.Contains(row, column)) != null)
                        button.BackColor = Color.Red;

                    // Assign an event handler for the Click event if needed
                    button.Click += Player_GridButton_Click;
                    playerButtonsGrid[row, column] = button;
                    playerGrid.Controls.Add(button);
                }
            }
            //pc grid
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

                    //TODO: Remove this so opponent can't see
                    if (playerFleet.Ships.FirstOrDefault(x => x.Contains(row, column)) != null)
                        button.BackColor = Color.Red;
                    // Assign an event handler for the Click event if needed
                    button.Click += PC_GridButton_Click;
                    pcButtonsGrid[row, column] = button;
                    pcGrid.Controls.Add(button);
                }
            }
        }

        private void Player_GridButton_Click(object? sender, EventArgs e)
        {
            


        }

        private void PC_GridButton_Click(object? sender, EventArgs e)
        {


        }
    }
}
