using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        Player currentPlayer = Player.Player;
        Gunner gunner;
        Fleet playerFleet;
        Fleet computerFleet;
        Grid playerEvidenceGrid=new Grid(10,10);
        List<Square> hitList=new List<Square>();
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
            button5.Enabled = false;
            button6.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            playerFleet = null;
            Invalidate();
        }
        private void StartNewGame()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            WhoFirst();
        }

        private void WhoFirst()
        {
            if (MessageBox.Show("Do you wish to start first?", "Start new game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                currentPlayer = Player.Player;
                return;
            }
            currentPlayer = Player.Computer;
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
            var shipBuilder = new Shipwright(10, 10);
            try
            {
                playerFleet = shipBuilder.CreateFleet(ships);
            }
            catch
            {
                playerFleet = shipBuilder.CreateFleet(ships);
            }

            Invalidate();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //delete player playerFleet
            playerFleet = null;
            Invalidate();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //start game
            if (playerFleet == null)
            {
                MessageBox.Show("No fleet!");
                return;
            }
            gunner = new Gunner(10, 10, ships);
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
        private void button5_Click(object sender, EventArgs e)
        {
            //From textbox1 mark a specific cube as hit/sunken
            try
            {
                //Translator(textBox1.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Wrong input please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox1.Text = "";
            Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Square square = new Square(0, 0);
            try
            {
               // square=Translator(textBox2.Text);
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Wrong input please try again","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            Shoot(square);
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
            int column = char.ToUpper(target[0]) - 'A';

            target.Trim();
            if (column < 0 || column >= 10)
                throw new ArgumentOutOfRangeException();
            int i = 1;
            while (!char.IsDigit(target[i]))
                ++i;
            int row = int.Parse(target.Substring(i)) - 1;
            if (row >= 10)
                throw new ArgumentOutOfRangeException();
            return new Square(row, column);

        }
        private void Shoot(Square square)
        {
            //Notify grid/square/fleet that the field is shot
            //call paint to paint it

            square = gunner.NextTarget();
            HitResult hitResult = playerFleet.Hit(square);
            gunner.ProcessHitResult(hitResult);
            playerEvidenceGrid.MarkHitResult(square, hitResult);
            textBox2.Text = "";
            textBox1.Text = square.Column.ToString() + square.Row.ToString();
            if (FleetIsSunken(playerFleet))
            {
                DisplayDefeat();
            }   
        }
        //Drawing
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPlayerGrid(e.Graphics, e);
            if (!GameIsStarted())
            {
                DrawGrids(e.Graphics);
            }
            
        }

        private void DrawPlayerGrid(Graphics graphics,PaintEventArgs e)
        {
            graphics = panel1.CreateGraphics();
            Pen myPen = new Pen(Brushes.Black, 1);
            Font myFont = new Font("Arial", 10);
            int lines = 10;
            float xspace = panel1.Height / lines;
            float yspace = panel1.Width / lines - 1;
            if (playerEvidenceGrid == null)
            {
                throw new NullReferenceException();
            }
            if (playerFleet == null)
            {
                DrawEmptyField(graphics, xspace, yspace);
                return;
            }
            foreach (var square in playerEvidenceGrid.GetSquares())
            {
                var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column* yspace) + 1, (int)xspace - 1, (int)yspace - 1);

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
                    ColorShipWithSquare(square, xspace, yspace, graphics,playerFleet);
                    DrawGrids(e.Graphics);
                }
            }
            if (!GameIsStarted())
            {
                playerEvidenceGrid = new Grid(10, 10);
                foreach (var square in playerEvidenceGrid.GetSquares())
                {
                    var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                    graphics.FillRectangle(Brushes.LightSteelBlue, rect);
                }
                    DrawShips(graphics, xspace, yspace);

            }
        }

        private void DrawShips(Graphics graphics, float xspace, float yspace)
        {
            foreach (var ship in playerFleet.Ships)
            {
                    ColorEntireship(graphics, xspace, yspace, ship, ship.Squares.First(), Brushes.LightSteelBlue, Brushes.BlueViolet);
            }
        }

        private void DrawEmptyField(Graphics graphics, float xspace, float yspace)
        {
            foreach (var square in playerEvidenceGrid.GetSquares())
            {
                var rect = new Rectangle((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                graphics.FillRectangle(Brushes.LightSteelBlue, rect);

            }
            return;
        }

        private bool GameIsStarted()
        {
            return !button1.Enabled;
        }

        private static void ColorEntireship(Graphics graphics, float xspace, float yspace, Ship ship, Square square,Brush bottom,Brush top)
        {
            RectangleF rectf = new RectangleF((int)(square.Row * xspace) + 1, (int)(square.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
            foreach (var squares in ship.Squares)
            {

                var rect = new RectangleF((int)(squares.Row * xspace) + 1, (int)(squares.Column * yspace) + 1, (int)xspace - 1, (int)yspace - 1);
                System.Drawing.RectangleF.Union(rect, rectf);
                //graphics.FillRectangle(Brushes.Yellow, rect);
                rectf = RectangleF.Union(rectf, rect);
            }
            Rectangle unionRect = Rectangle.Truncate(rectf);
            graphics.FillRectangle(bottom, rectf);
            graphics.FillEllipse(top, rectf);
        }

        private void ColorShipWithSquare(Square square, float xspace, float yspace,Graphics graphics,Fleet fleet)
        {
            foreach(var ship in fleet.Ships)
            {
                if (ship.ContainsSquare(square))
                {
                    ColorEntireship(graphics, xspace, yspace, ship, square, Brushes.Crimson, Brushes.DarkOrange);
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
            Font myfont = new Font("Arial", 10);
            int lines = 10;
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
    }
}
