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
        List<Square> hitList = new List<Square>();
        Grid playerEvidenceGrid=new Grid(size,size);
        Grid playerShootingGrid=new Grid(size,size);
        
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
            InvalidatePlayer();
            if (hitResult == HitResult.Sunken)
            {
                MarkSurrounding(square, playerEvidenceGrid, playerFleet);
            }
            
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
            if (hitList.Contains(square))
            {
                textBox2.Text = "";
                return false;
            }
            hitResult = computerFleet.Hit(square);
            playerShootingGrid.MarkHitResult(square, computerFleet.Hit(square));
            textBox2.Text = "";
            InvalidateComputer();
            Thread.Sleep(1000);
            if (hitResult == HitResult.Sunken)
            {
                MarkSurrounding(square, playerShootingGrid, computerFleet);
            }
            hitList.Add(square);
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
            Pen myPen = new Pen(Brushes.Black, 1);
            Font myFont = new Font("Arial", size);
            int lines = size;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
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
                if (square.SquareState == SquareState.Hit && square.SquareState != SquareState.Sunken)
                {
                    graphics.FillRectangle(Brushes.Red, rect);
                }
                if (square.SquareState == SquareState.Sunken)
                {
                    ColorShipWithSquare(square, xspace, yspace, graphics, computerFleet,Brushes.Crimson);
                    DrawGrids(e.Graphics);
                }
            }
        }

        private void PaintPlayerGrid(Graphics graphics,PaintEventArgs e)
        {
            graphics = panel1.CreateGraphics();
            Pen myPen = new Pen(Brushes.Black, 1);
            Font myFont = new Font("Arial", size);
            int lines = size;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
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
                if (playerFleet != null&& square.SquareState == SquareState.None) {
                    ColorShipWithSquare(square, xspace, yspace, graphics, playerFleet,Brushes.Navy);
                }
                if (square.SquareState == SquareState.Missed)
                {
                    graphics.FillRectangle(Brushes.LightSlateGray, rect);
                }
                if (square.SquareState == SquareState.Hit&&square.SquareState!=SquareState.Sunken)
                {
                    graphics.FillRectangle(Brushes.Red, rect);  
                }
                if (square.SquareState == SquareState.Sunken)
                {
                    ColorShipWithSquare(square, xspace, yspace, graphics,playerFleet,Brushes.Crimson);
                    DrawGrids(e.Graphics);
                }
            }
            
        }

        public void MarkSurrounding(Square square,Grid grid,Fleet fleet)
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
            }
            return;
        }



        private void DrawEmptyField(Graphics graphics, float xspace, float yspace,Grid grid)
        {
            foreach (var square in grid.GetSquares())
            {
                var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);

            }
            return;
        }

        private bool GameIsStarted()
        {
            return !button1.Enabled;
        }

        private static void ColorEntireship(Graphics graphics, float xspace, float yspace, Ship ship, Square square,Brush bottom)
        {
            RectangleF rectf = new RectangleF((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
            foreach (var squares in ship.Squares)
            {

                var rect = new RectangleF((int)(squares.Row * xspace) + 1, (int)(squares.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                RectangleF.Union(rect, rectf);
                rectf = RectangleF.Union(rectf, rect);
            }
            Rectangle unionRect = Rectangle.Truncate(rectf);
            graphics.FillRectangle(bottom, rectf);
        }

        private void ColorShipWithSquare(Square square, float xspace, float yspace,Graphics graphics,Fleet fleet,Brush Brush1)
        {
            foreach(var ship in fleet.Ships)
            {
                if (ship.ContainsSquare(square))
                {
                    ColorEntireship(graphics, xspace, yspace, ship, square, Brush1);
                } 
            }
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
            Pen mypen = new Pen(Brushes.Black, 1);
            Font myfont = new Font("Arial", size);
            int lines = size;
            float x = 0;
            float y = 0;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
            for (int i = 0; i <= lines; ++i)
            {
                graphics.DrawLine(mypen, x, y, x, lines * yspace);
                x += xspace;
            }
            x = 0;
            for (int i = 0; i <= lines; ++i)
            {
                graphics.DrawLine(mypen, x, y, lines * xspace, y);
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
