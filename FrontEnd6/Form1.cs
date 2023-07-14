using Vsite.Oom.Battleship.Model;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace FrontEnd6
{
    public partial class Form1 : Form
    {
        public class GridButton : Button
        {
            public Point GridCoordinates { get; set; }

            public GridButton(int x, int y)
            {
                GridCoordinates = new Point(x, y);
            }

            public void ChangeColor(Color color)
            {
                this.BackColor = color;
            }
        }

        private const int gridSize = 10;
        private TableLayoutPanel mainTableLayoutPanel;
        public GridButton[,] buttonsClient = new GridButton[gridSize, gridSize];
        public GridButton[,] buttonsEnemy = new GridButton[gridSize, gridSize];
        private TextBox console;

        public GameRules rules;
        public Gunnery AIGunnery;
        
        public FleetBuilder builder;
        public Fleet fleetEnemy;
        public Fleet fleetClient;
        public int shipCountClient;
        public int shipCountEnemy;
        public Button button1;
        public Button button2;

        public Form1()
        {
            InitializeComponent();

            mainTableLayoutPanel = new TableLayoutPanel();
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.ColumnCount = 3;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80f));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));

            this.Controls.Add(mainTableLayoutPanel);

            InitializeGrid(buttonsClient);
            InitializeConsole();
            InitializeGrid(buttonsEnemy);

            TableLayoutPanel buttonTableLayoutPanel = new TableLayoutPanel();
            buttonTableLayoutPanel.Dock = DockStyle.Bottom;
            buttonTableLayoutPanel.ColumnCount = 2;
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            buttonTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45f));
            buttonTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            buttonTableLayoutPanel.Height = 50;  // You can adjust this height as necessary


            button1 = new Button();
            button1.Text = "Shuffle";
            button1.Dock = DockStyle.Fill;
            button1.Click += button1_Click;  // Link to existing button1_Click event handler
            buttonTableLayoutPanel.Controls.Add(button1, 0, 0);

            button2 = new Button();
            button2.Text = "Restart Game";
            button2.Dock = DockStyle.Fill;
            button2.Click += button2_Click;  // Link to existing button2_Click event handler
            buttonTableLayoutPanel.Controls.Add(button2, 1, 0);

            this.Controls.Add(buttonTableLayoutPanel);

            this.Resize += new EventHandler(Form1_Resize);
            Form1_Resize(this, null);
            RestartGame();
        }

        private void InitializeGrid(GridButton[,] buttons)
        {
            TableLayoutPanel gridPanel = new TableLayoutPanel();
            gridPanel.RowCount = gridSize;
            gridPanel.ColumnCount = gridSize;
            for (int i = 0; i < gridSize; i++)
            {
                gridPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / gridSize));
                gridPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / gridSize));
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buttons[i, j] = new GridButton(i, j);
                    buttons[i, j].Dock = DockStyle.Fill;
                    buttons[i, j].Click += Button_Click;
                    gridPanel.Controls.Add(buttons[i, j], j, i);
                }
            }

            mainTableLayoutPanel.Controls.Add(gridPanel);
        }

        private void InitializeConsole()
        {
            console = new TextBox();
            console.Multiline = true;
            console.ReadOnly = true;
            console.Dock = DockStyle.Fill;

            mainTableLayoutPanel.Controls.Add(console);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            GridButton button = sender as GridButton;
            //button.ChangeColor(Color.Gray);
            button.Enabled = false;

            
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    
                    if (buttonsEnemy[i, j] == button)
                    {
                        console.AppendText($"client clicked at {button.GridCoordinates}.\r\n");
                        FireOnGrid(i, j, "client");
                    }
                }
            }
        }

        private void FireOnGrid(int i, int j, string gridType)
        {
            if (gridType == "client")
            {
                HitResult r = fleetEnemy.Fire(new Square(i, j));
                ProcessHitResult(i, j, buttonsEnemy, r, fleetEnemy, ref shipCountEnemy, "\r\nClient wins\r\n");

                var targetSquare = AIGunnery.NextTarget();
                var rx = fleetClient.Fire(targetSquare);
                AIGunnery.ProcessHitResult(rx);
                console.AppendText($"enemy clicked at {{X = {targetSquare.Row},Y = {targetSquare.Column}}}.\r\n");

                ProcessHitResult(targetSquare.Row, targetSquare.Column, buttonsClient, rx, fleetClient, ref shipCountClient, "\r\nEnemy wins\r\n");
            }
        }

        private void ProcessHitResult(int i, int j, GridButton[,] buttons, HitResult hitResult, Fleet fleet, ref int shipCount, string victoryMessage)
        {
           
            //"client clicked at { X = {i},Y = {j}}.";

            if (hitResult == HitResult.Hit)
            {
                buttons[i, j].ChangeColor(Color.Red);
            }
            if (hitResult == HitResult.Missed)
            {
                buttons[i, j].ChangeColor(Color.Gray);
            }
            if (hitResult == HitResult.Sunk)
            {
                shipCount--;
                foreach (Ship s in fleet.Ships)
                {
                    foreach (Square square in s.Squares)
                    {
                        if (square.SquareState == SquareState.Sunk)
                        {
                            buttons[square.Row, square.Column].ChangeColor(Color.DarkRed);
                        }
                    }
                }
                if (shipCount <= 0)
                {
                    foreach (GridButton button in buttonsEnemy)
                    {
                        if (button.Enabled)
                        {
                            console.AppendText($"{victoryMessage}");
                            DisableAllButtons();
                        }
                    }
                    
                }
            }
        }
        private void RestartGame()
        {
            button1.Enabled = true;
            rules = new GameRules();
            shipCountClient = rules.ShipLengths.Count();
            shipCountEnemy = rules.ShipLengths.Count();
            AIGunnery = new Gunnery(rules);
            builder = new FleetBuilder(rules);
            shuffelBord();

            foreach (GridButton gb in buttonsEnemy)
            {
                gb.ChangeColor(Color.White);
                gb.Enabled = true;
            }
            console.Clear();
            shipCountClient = rules.ShipLengths.Count();
            shipCountEnemy = rules.ShipLengths.Count();
        }

        private void DisableAllButtons()
        {
            foreach (GridButton bs in buttonsEnemy)
            {
                bs.Enabled = false;
            }
            foreach (GridButton bs in buttonsClient)
            {
                bs.Enabled = false;
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int width = (int)(this.Width * 0.45);
            int height = (int)(this.Height * 0.8);

            foreach (Control control in mainTableLayoutPanel.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)control;
                    tableLayoutPanel.Size = new Size(width, height);
                }
            }

            foreach (Control control in this.Controls)
            {
                if (control is TableLayoutPanel buttonTableLayoutPanel)
                {
                    foreach (Control buttonControl in buttonTableLayoutPanel.Controls)
                    {
                        if (buttonControl is Button)
                        {
                            Button button = (Button)buttonControl;
                            button.Width = (int)(width * 0.45);
                            button.Height = (int)(this.Height * 0.2);
                        }
                    }
                }
            }
        }


        private void shuffelBord()
        {
            fleetEnemy = builder.CreateFleet();
            fleetClient = builder.CreateFleet();

            foreach (GridButton gb in buttonsClient)
            {
                gb.ChangeColor(Color.White);
            }
            foreach (Ship s in fleetClient.Ships)
            {
                foreach (Square square in s.Squares)
                {
                    buttonsClient[square.Row, square.Column].ChangeColor(Color.Green);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Enabled = false;
            try
            {
                shuffelBord();
            }
            catch (Exception)
            {
                shuffelBord();
            }
            button.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Enabled = false;
            try
            {
                RestartGame();
            }
            catch (Exception)
            {
                RestartGame();
            }
            button.Enabled = true;
        }
    }
}
