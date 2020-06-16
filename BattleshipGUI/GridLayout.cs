using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;


namespace BattleshipGUI
{
    enum Player
    {
        Player,Computer
    }
    public partial class GridLayout : Form
    {
        Gunner gunner;
        static int size = 10;
        Fleet playerFleet;
        Fleet computerFleet;
        List<Square> playerhitList = new List<Square>();
        List<Square> computerhitList = new List<Square>();
        Grid playerEvidenceGrid=new Grid(size,size);
        Grid playerShootingGrid=new Grid(size,size);
        Pen mypen = new Pen(Brushes.Black, 1);
        readonly int[] ships = new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
        public GridLayout()
        {
            InitializeComponent();
        }
        //Functionality

        private void GameOver()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = false;
            button6.Enabled = false;
            textBox2.Enabled = false;
            textBox2.Text = "";
            playerFleet = null;
            computerFleet = null;
            playerEvidenceGrid = new Grid(10, 10);
            playerShootingGrid = new Grid(10, 10);
            playerhitList = new List<Square>();
            computerhitList = new List<Square>();
            Invalidate();
        }
        private void StartNewGame()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button6.Enabled = true;
            textBox2.Enabled = true;
            
            
            WhoFirst();
        }

        private void WhoFirst()
        {
            if (MessageBox.Show("Do you wish to start first?", "Start new game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return;
            }
            ShootComputer();
        }

        private void DisplayDefeat()
        {
            DialogResult dr=MessageBox.Show("You lose. Play again?","Defeat",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            PlayAgain(dr);
        }
        
        private void DisplayVictory()
        {
            DialogResult dr = MessageBox.Show("Congratulations,you win. Play again?", "Victory!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            PlayAgain(dr);
        }

        private void PlayAgain(DialogResult dr)
        {
            if (dr == DialogResult.Yes)
            {
                GameOver();
            }
            else
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //draw player playerFleet
            var shipBuilder = new Shipwright(size, size);
            try
            {
                playerFleet = shipBuilder.CreateFleet(ships);
            }
            catch
            {
                playerFleet = shipBuilder.CreateFleet(ships);
            }
            try
            {
                computerFleet = shipBuilder.CreateFleet(ships);
            }
            catch
            {
                computerFleet = shipBuilder.CreateFleet(ships);
            }
            InvalidatePlayer();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //delete player playerFleet
            playerFleet = null;
            InvalidatePlayer();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //start game
            if (playerFleet == null)
            {
                MessageBox.Show("No fleet!");
                return;
            }
            gunner = new Gunner(size, size, ships);
            StartNewGame();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //surrender
            DialogResult dr = MessageBox.Show("Are you sure you want to surrender?", "Surrender", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DisplayDefeat();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(ShootPlayer())
                ShootComputer();
            CheckEndGame();
        }

        private void ShootComputer()
        {
            Square square;
            HitResult hitResult;
            square = gunner.NextTarget();
            hitResult = playerFleet.Hit(square);
            gunner.ProcessHitResult(hitResult);
            playerEvidenceGrid.MarkHitResult(square, hitResult);
            if (hitResult == HitResult.Sunken)
            {
                MarkSurrounding(square, playerEvidenceGrid, playerFleet,computerhitList);
            }
            InvalidatePlayer();
            
            
        }

        private bool ShootPlayer()
        {
            Square square=new Square(-1,-1);
            HitResult hitResult;
            try
            {
                square = Translator(textBox2.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Invalid input.");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Invalid input.");
            }

            if (square.Row == -1 || square.Column == -1)
                return false;
            if (playerhitList.Contains(square))
            {
                textBox2.Text = "";
                MessageBox.Show("That field is already hit");
                return false;
            }
            hitResult = computerFleet.Hit(square);
            playerShootingGrid.MarkHitResult(square, computerFleet.Hit(square));
            textBox2.Text = "";
            if (hitResult == HitResult.Sunken)
            {
                MarkSurrounding(square, playerShootingGrid, computerFleet,playerhitList);
            }
            InvalidateComputer();
            Thread.Sleep(1000);
            
            playerhitList.Add(square);
            return true;
        }
        private bool FleetIsSunken(Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                if (!ShipIsSunken(ship))
                    return false;
            }
            return true;
        }

        private bool ShipIsSunken(Ship ship)
        {

            if (ship.Squares.Last().SquareState == SquareState.Sunken)
            {
                return true;
            }
            return false;
        }

        private Square Translator(string target)
        {
            if (target.Equals(""))
            {
                return new Square(-1, -1);
            }
            int column = char.ToUpper(target[0]) - 'A';

            target.Trim();
            if (column < 0 || column >= size)
                throw new ArgumentOutOfRangeException();
            int i = 1;
            while (!char.IsDigit(target[i]))
                ++i;
            int row = int.Parse(target.Substring(i)) - 1;
            if (row >= size)
                throw new ArgumentOutOfRangeException();
            return new Square(column, row);

        }

        private void CheckEndGame()
        {
            if (FleetIsSunken(computerFleet))
            {
                DisplayVictory();
                return;
            }
            else if (FleetIsSunken(playerFleet))
            {
                DisplayDefeat();
                return;
            }
        }

        private void InvalidateComputer()
        {
            Invalidate(panel2.Region);
        }

        private void InvalidatePlayer()
        {
            Invalidate(panel1.Region);
        }

        //Drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

                PaintPlayerGrid(e.Graphics, e);
                PaintComputerGrid(e.Graphics, e);

                DrawGrids(e.Graphics);

        }

        private void PaintComputerGrid(Graphics graphics, PaintEventArgs e)
        {
            graphics = panel2.CreateGraphics();
            float xspace = panel1.Height / size;
            float yspace = panel1.Width / size - 1;
            if (playerShootingGrid == null)
            {
                throw new NullReferenceException();
            }
            foreach (var square in playerShootingGrid.GetSquares())
            {
                var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                if (square.SquareState == SquareState.None)
                {
                    graphics.FillRectangle(Brushes.LightSteelBlue, rect);
                }
                if (square.SquareState == SquareState.Missed)
                {
                    graphics.FillRectangle(Brushes.LightSlateGray, rect);

                }
                if (square.SquareState == SquareState.Sunken || square.SquareState == SquareState.Hit)
                {
                    graphics.FillRectangle(Brushes.Red, rect);
                }
            }
        }

        private void PaintPlayerGrid(Graphics graphics,PaintEventArgs e)
        {
            graphics = panel1.CreateGraphics();
            float xspace = panel1.Height / size;
            float yspace = panel1.Width / size - 1;
            if (playerEvidenceGrid == null)
            {
                throw new NullReferenceException();
            }
            foreach (var square in playerEvidenceGrid.GetSquares())
            {
                var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column* yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                if (square.SquareState == SquareState.None)
                {
                    graphics.FillRectangle(Brushes.LightSteelBlue, rect);
                }
                if (playerFleet != null&& square.SquareState == SquareState.None&&FleetHasSquare(playerFleet,square)) {
                    graphics.FillRectangle(Brushes.Navy, rect);
                }
                if (square.SquareState == SquareState.Missed)
                {
                    graphics.FillRectangle(Brushes.LightSlateGray, rect);
                }
                if (square.SquareState == SquareState.Sunken || square.SquareState==SquareState.Hit)
                {
                    graphics.FillRectangle(Brushes.Red, rect);  
                }
            }
            
        }

        private bool FleetHasSquare(Fleet playerFleet, Square square)
        {
            foreach(Ship ship in playerFleet.Ships)
            {
                if (ship.ContainsSquare(square))
                    return true;
            }
            return false;
        }

        private Ship GiveShipWithSquare(Square square,Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                if (ship.ContainsSquare(square))
                {
                    return ship;
                }
            }
            return null;
        }
        public void MarkSurrounding(Square square,Grid grid,Fleet fleet,List<Square>hitList)
        {
            SquareTerminator terminator = new SquareTerminator(size, size);
            Ship ship=new Ship(null);
            foreach (var ships in fleet.Ships)
            {
                if (ships.ContainsSquare(square))
                {
                    ship = ships;
                    break;
                }
            }
            List<Square> squares = new List<Square>();
            squares = terminator.ToEliminate(ship.Squares).ToList();
            squares=squares.Except(ship.Squares).ToList();
            squares=grid.GetSquares().Intersect(squares).ToList();
            foreach(var squaree in squares)
            {
                squaree.SetState(HitResult.Missed);
                hitList.Add(squaree);
            }
            return;
        }
        private bool GameIsStarted()
        {
            return !button1.Enabled;
        }
        private void DrawGrids(Graphics graphics)
        {
            graphics = panel1.CreateGraphics();
            DrawGrid(graphics);
            graphics = panel2.CreateGraphics();
            DrawGrid(graphics);
        }

        private void DrawGrid(Graphics graphics)
        {
            float x = 0;
            float y = 0;
            float xspace = panel1.Height / size;
            float yspace = panel1.Width / size - 1;
            for (int i = 0; i <= size; ++i)
            {
                graphics.DrawLine(mypen, x, y, x, size * yspace);
                x += xspace;
            }
            x = 0;
            for (int i = 0; i <= size; ++i)
            {
                graphics.DrawLine(mypen, x, y, size * xspace, y);
                y += yspace;
            }
        }

        private void textBox2_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
