using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    using System.Threading;
    using Vsite.Oom.Battleship.Model;


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset = 1;
            LengthToShoot = 0;

            Buttons[0].Add(PlayerButton0x0);
            Buttons[0].Add(PlayerButton0x1);
            Buttons[0].Add(PlayerButton0x2);
            Buttons[0].Add(PlayerButton0x3);
            Buttons[0].Add(PlayerButton0x4);
            Buttons[0].Add(PlayerButton0x5);
            Buttons[0].Add(PlayerButton0x6);
            Buttons[0].Add(PlayerButton0x7);
            Buttons[0].Add(PlayerButton0x8);
            Buttons[0].Add(PlayerButton0x9);
            Buttons[1].Add(PlayerButton1x0);
            Buttons[1].Add(PlayerButton1x1);
            Buttons[1].Add(PlayerButton1x2);
            Buttons[1].Add(PlayerButton1x3);
            Buttons[1].Add(PlayerButton1x4);
            Buttons[1].Add(PlayerButton1x5);
            Buttons[1].Add(PlayerButton1x6);
            Buttons[1].Add(PlayerButton1x7);
            Buttons[1].Add(PlayerButton1x8);
            Buttons[1].Add(PlayerButton1x9);
            Buttons[2].Add(PlayerButton2x0);
            Buttons[2].Add(PlayerButton2x1);
            Buttons[2].Add(PlayerButton2x2);
            Buttons[2].Add(PlayerButton2x3);
            Buttons[2].Add(PlayerButton2x4);
            Buttons[2].Add(PlayerButton2x5);
            Buttons[2].Add(PlayerButton2x6);
            Buttons[2].Add(PlayerButton2x7);
            Buttons[2].Add(PlayerButton2x8);
            Buttons[2].Add(PlayerButton2x9);
            Buttons[3].Add(PlayerButton3x0);
            Buttons[3].Add(PlayerButton3x1);
            Buttons[3].Add(PlayerButton3x2);
            Buttons[3].Add(PlayerButton3x3);
            Buttons[3].Add(PlayerButton3x4);
            Buttons[3].Add(PlayerButton3x5);
            Buttons[3].Add(PlayerButton3x6);
            Buttons[3].Add(PlayerButton3x7);
            Buttons[3].Add(PlayerButton3x8);
            Buttons[3].Add(PlayerButton3x9);
            Buttons[4].Add(PlayerButton4x0);
            Buttons[4].Add(PlayerButton4x1);
            Buttons[4].Add(PlayerButton4x2);
            Buttons[4].Add(PlayerButton4x3);
            Buttons[4].Add(PlayerButton4x4);
            Buttons[4].Add(PlayerButton4x5);
            Buttons[4].Add(PlayerButton4x6);
            Buttons[4].Add(PlayerButton4x7);
            Buttons[4].Add(PlayerButton4x8);
            Buttons[4].Add(PlayerButton4x9);
            Buttons[5].Add(PlayerButton5x0);
            Buttons[5].Add(PlayerButton5x1);
            Buttons[5].Add(PlayerButton5x2);
            Buttons[5].Add(PlayerButton5x3);
            Buttons[5].Add(PlayerButton5x4);
            Buttons[5].Add(PlayerButton5x5);
            Buttons[5].Add(PlayerButton5x6);
            Buttons[5].Add(PlayerButton5x7);
            Buttons[5].Add(PlayerButton5x8);
            Buttons[5].Add(PlayerButton5x9);
            Buttons[6].Add(PlayerButton6x0);
            Buttons[6].Add(PlayerButton6x1);
            Buttons[6].Add(PlayerButton6x2);
            Buttons[6].Add(PlayerButton6x3);
            Buttons[6].Add(PlayerButton6x4);
            Buttons[6].Add(PlayerButton6x5);
            Buttons[6].Add(PlayerButton6x6);
            Buttons[6].Add(PlayerButton6x7);
            Buttons[6].Add(PlayerButton6x8);
            Buttons[6].Add(PlayerButton6x9);
            Buttons[7].Add(PlayerButton7x0);
            Buttons[7].Add(PlayerButton7x1);
            Buttons[7].Add(PlayerButton7x2);
            Buttons[7].Add(PlayerButton7x3);
            Buttons[7].Add(PlayerButton7x4);
            Buttons[7].Add(PlayerButton7x5);
            Buttons[7].Add(PlayerButton7x6);
            Buttons[7].Add(PlayerButton7x7);
            Buttons[7].Add(PlayerButton7x8);
            Buttons[7].Add(PlayerButton7x9);
            Buttons[8].Add(PlayerButton8x0);
            Buttons[8].Add(PlayerButton8x1);
            Buttons[8].Add(PlayerButton8x2);
            Buttons[8].Add(PlayerButton8x3);
            Buttons[8].Add(PlayerButton8x4);
            Buttons[8].Add(PlayerButton8x5);
            Buttons[8].Add(PlayerButton8x6);
            Buttons[8].Add(PlayerButton8x7);
            Buttons[8].Add(PlayerButton8x8);
            Buttons[8].Add(PlayerButton8x9);
            Buttons[9].Add(PlayerButton9x0);
            Buttons[9].Add(PlayerButton9x1);
            Buttons[9].Add(PlayerButton9x2);
            Buttons[9].Add(PlayerButton9x3);
            Buttons[9].Add(PlayerButton9x4);
            Buttons[9].Add(PlayerButton9x5);
            Buttons[9].Add(PlayerButton9x6);
            Buttons[9].Add(PlayerButton9x7);
            Buttons[9].Add(PlayerButton9x8);
            Buttons[9].Add(PlayerButton9x9);

            EnemyButtons[0].Add(AiButton0x0);
            EnemyButtons[0].Add(AiButton0x1);
            EnemyButtons[0].Add(AiButton0x2);
            EnemyButtons[0].Add(AiButton0x3);
            EnemyButtons[0].Add(AiButton0x4);
            EnemyButtons[0].Add(AiButton0x5);
            EnemyButtons[0].Add(AiButton0x6);
            EnemyButtons[0].Add(AiButton0x7);
            EnemyButtons[0].Add(AiButton0x8);
            EnemyButtons[0].Add(AiButton0x9);
            EnemyButtons[1].Add(AiButton1x0);
            EnemyButtons[1].Add(AiButton1x1);
            EnemyButtons[1].Add(AiButton1x2);
            EnemyButtons[1].Add(AiButton1x3);
            EnemyButtons[1].Add(AiButton1x4);
            EnemyButtons[1].Add(AiButton1x5);
            EnemyButtons[1].Add(AiButton1x6);
            EnemyButtons[1].Add(AiButton1x7);
            EnemyButtons[1].Add(AiButton1x8);
            EnemyButtons[1].Add(AiButton1x9);
            EnemyButtons[2].Add(AiButton2x0);
            EnemyButtons[2].Add(AiButton2x1);
            EnemyButtons[2].Add(AiButton2x2);
            EnemyButtons[2].Add(AiButton2x3);
            EnemyButtons[2].Add(AiButton2x4);
            EnemyButtons[2].Add(AiButton2x5);
            EnemyButtons[2].Add(AiButton2x6);
            EnemyButtons[2].Add(AiButton2x7);
            EnemyButtons[2].Add(AiButton2x8);
            EnemyButtons[2].Add(AiButton2x9);
            EnemyButtons[3].Add(AiButton3x0);
            EnemyButtons[3].Add(AiButton3x1);
            EnemyButtons[3].Add(AiButton3x2);
            EnemyButtons[3].Add(AiButton3x3);
            EnemyButtons[3].Add(AiButton3x4);
            EnemyButtons[3].Add(AiButton3x5);
            EnemyButtons[3].Add(AiButton3x6);
            EnemyButtons[3].Add(AiButton3x7);
            EnemyButtons[3].Add(AiButton3x8);
            EnemyButtons[3].Add(AiButton3x9);
            EnemyButtons[4].Add(AiButton4x0);
            EnemyButtons[4].Add(AiButton4x1);
            EnemyButtons[4].Add(AiButton4x2);
            EnemyButtons[4].Add(AiButton4x3);
            EnemyButtons[4].Add(AiButton4x4);
            EnemyButtons[4].Add(AiButton4x5);
            EnemyButtons[4].Add(AiButton4x6);
            EnemyButtons[4].Add(AiButton4x7);
            EnemyButtons[4].Add(AiButton4x8);
            EnemyButtons[4].Add(AiButton4x9);
            EnemyButtons[5].Add(AiButton5x0);
            EnemyButtons[5].Add(AiButton5x1);
            EnemyButtons[5].Add(AiButton5x2);
            EnemyButtons[5].Add(AiButton5x3);
            EnemyButtons[5].Add(AiButton5x4);
            EnemyButtons[5].Add(AiButton5x5);
            EnemyButtons[5].Add(AiButton5x6);
            EnemyButtons[5].Add(AiButton5x7);
            EnemyButtons[5].Add(AiButton5x8);
            EnemyButtons[5].Add(AiButton5x9);
            EnemyButtons[6].Add(AiButton6x0);
            EnemyButtons[6].Add(AiButton6x1);
            EnemyButtons[6].Add(AiButton6x2);
            EnemyButtons[6].Add(AiButton6x3);
            EnemyButtons[6].Add(AiButton6x4);
            EnemyButtons[6].Add(AiButton6x5);
            EnemyButtons[6].Add(AiButton6x6);
            EnemyButtons[6].Add(AiButton6x7);
            EnemyButtons[6].Add(AiButton6x8);
            EnemyButtons[6].Add(AiButton6x9);
            EnemyButtons[7].Add(AiButton7x0);
            EnemyButtons[7].Add(AiButton7x1);
            EnemyButtons[7].Add(AiButton7x2);
            EnemyButtons[7].Add(AiButton7x3);
            EnemyButtons[7].Add(AiButton7x4);
            EnemyButtons[7].Add(AiButton7x5);
            EnemyButtons[7].Add(AiButton7x6);
            EnemyButtons[7].Add(AiButton7x7);
            EnemyButtons[7].Add(AiButton7x8);
            EnemyButtons[7].Add(AiButton7x9);
            EnemyButtons[8].Add(AiButton8x0);
            EnemyButtons[8].Add(AiButton8x1);
            EnemyButtons[8].Add(AiButton8x2);
            EnemyButtons[8].Add(AiButton8x3);
            EnemyButtons[8].Add(AiButton8x4);
            EnemyButtons[8].Add(AiButton8x5);
            EnemyButtons[8].Add(AiButton8x6);
            EnemyButtons[8].Add(AiButton8x7);
            EnemyButtons[8].Add(AiButton8x8);
            EnemyButtons[8].Add(AiButton8x9);
            EnemyButtons[9].Add(AiButton9x0);
            EnemyButtons[9].Add(AiButton9x1);
            EnemyButtons[9].Add(AiButton9x2);
            EnemyButtons[9].Add(AiButton9x3);
            EnemyButtons[9].Add(AiButton9x4);
            EnemyButtons[9].Add(AiButton9x5);
            EnemyButtons[9].Add(AiButton9x6);
            EnemyButtons[9].Add(AiButton9x7);
            EnemyButtons[9].Add(AiButton9x8);
            EnemyButtons[9].Add(AiButton9x9);

            foreach(List<Button> liste in Buttons)
            {
                foreach(Button button in liste)
                {
                    button.BackColor = Color.LightGray;
                    button.Text = "";
                }
            }

            foreach (List<Button> liste in EnemyButtons)
            {
                foreach (Button button in liste)
                {
                    button.BackColor = Color.LightGray;
                    button.Text = "";
                    button.Enabled = true;
                }
            }

            PlayerFleet = new Fleet();
            AiFleet = new Fleet();

            grid = new Grid(10, 10);
            
            
            SurroundingSquaresEliminator eliminator = new SurroundingSquaresEliminator(10, 10);

            DrawShip(5, grid, eliminator);
            DrawShip(5, grid, eliminator);
            DrawShip(4, grid, eliminator);
            DrawShip(3, grid, eliminator);
            DrawShip(3, grid, eliminator);
            DrawShip(2, grid, eliminator);
            DrawShip(2, grid, eliminator);

            EnemyGrid = new Grid(10, 10);

            SurroundingSquaresEliminator EnemyEliminator = new SurroundingSquaresEliminator(10, 10);

            CreateEnemyFleet(5, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(5, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(4, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(3, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(3, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(2, EnemyGrid, EnemyEliminator);
            CreateEnemyFleet(2, EnemyGrid, EnemyEliminator);

            LengthToShoot = 5 + 5 + 4 + 3 + 3 + 2 + 2;
            LengthToShootByEnemy = 5 + 5 + 4 + 3 + 3 + 2 + 2;

            gunnery = new Gunnery(10, 10, new List<int>() { 5, 5, 4, 3, 3, 2, 2 });
        }

        private void DrawShip(int shipLength, Grid grid, SurroundingSquaresEliminator eliminator)
        {
            Random random = new Random();
            IEnumerable<IEnumerable<Square>> placements = grid.GetAvailablePlacements(shipLength);
            Thread.Sleep(50);
            IEnumerable<Square> Odabrani = placements.ElementAt(random.Next(0, placements.Count()));
            PlayerFleet.CreateShip(Odabrani);
            foreach (Square square in Odabrani)
            {
                Buttons[square.Row][square.Column].BackColor = Color.Blue;
            }
            IEnumerable<Square> ToRemove = eliminator.ToEliminate(Odabrani);
            grid.Eliminate(ToRemove);
            foreach (Square square in ToRemove)
            {
                square.SetSquareState(HitResult.Missed);
            }
        }

        private void CreateEnemyFleet(int shipLength, Grid grid, SurroundingSquaresEliminator eliminator)
        {
            Random random = new Random();
            IEnumerable<IEnumerable<Square>> placements = grid.GetAvailablePlacements(shipLength);
            Thread.Sleep(50);
            IEnumerable<Square> Odabrani = placements.ElementAt(random.Next(0, placements.Count()));
            AiFleet.CreateShip(Odabrani);
            foreach (Square square in Odabrani)
            {
                //EnemyButtons[square.Row][square.Column].BackColor = Color.Yellow;     SLUZI SAMO ZA PROVJERU
                EnemyButtons[square.Row][square.Column].Text = " ";
            }
            IEnumerable<Square> ToRemove = eliminator.ToEliminate(Odabrani);
            grid.Eliminate(ToRemove);
            foreach (Square square in ToRemove)
            {
                square.SetSquareState(HitResult.Missed);
            }
        }

        private List<List<Button>> Buttons = new List<List<Button>>()
        {
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
        };

        private List<List<Button>> EnemyButtons = new List<List<Button>>()
        {
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
            new List<Button> {},
        };

        private Gunnery gunnery;
        private Fleet PlayerFleet;
        private Fleet AiFleet;
        private int Reset = 0;
        private Grid grid;
        private Grid EnemyGrid;
        private int LengthToShoot;
        private int LengthToShootByEnemy;


        private void ButtonClickedFunction(Button button)
        {
            if (Reset == 0)
                return;
            string text = " ";
            
            if (button.Text == text) // znaci da je pogodeno
            {
                string ime = button.Name;
                int x = (int)Char.GetNumericValue(ime[8]);
                int y = (int)Char.GetNumericValue(ime[10]);
                Square square = new Square(x, y);
                Ship shipThatWasShotByPlayer = null;
                bool found = false;

                foreach (Ship ship in AiFleet.Ships)
                {
                    foreach (Square Candidate in ship.Squares)
                    {
                        if (Candidate.Row == square.Row && Candidate.Column == square.Column)
                        {
                            shipThatWasShotByPlayer = ship;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }


                HitResult result = shipThatWasShotByPlayer.Hit(square);
                if (result == HitResult.Hit)
                {
                    button.BackColor = Color.Red;
                    button.Enabled = false;
                }

                else
                {
                    
                    foreach(Square Sunken in shipThatWasShotByPlayer.Squares)
                    {
                        if (Sunken.SquareState == SquareState.Sunken)
                        {
                            EnemyButtons[Sunken.Row][Sunken.Column].BackColor = Color.Black;
                            EnemyButtons[Sunken.Row][Sunken.Column].Enabled = false;
                            LengthToShoot -= 1;
                        }
                    }
                }
                
            }
            else
            {
                button.BackColor = Color.Green;
                button.Enabled = false;
            }
            

            
            Square Target = gunnery.NextTarget();
            HitResult ShootingResult = HitResult.Missed;
            bool Contains = false;
            Ship shipThatWasShot = null;


            foreach (Ship ship in PlayerFleet.Ships)
            {
                foreach(Square square in ship.Squares)
                {
                    if (Target.Row == square.Row && Target.Column == square.Column)
                    {
                        Contains = true;
                        shipThatWasShot = ship;
                        break;
                    }
                }
                if (Contains)
                    break;
            }


            if (Contains)
            {
                ShootingResult = shipThatWasShot.Hit(Target);
                if (ShootingResult == HitResult.Hit)
                {
                    Buttons[Target.Row][Target.Column].BackColor = Color.Red;
                }
                else if(ShootingResult == HitResult.Sunken)
                {
                    foreach(Square square in shipThatWasShot.Squares)
                    {
                        Buttons[square.Row][square.Column].BackColor = Color.Black;
                        LengthToShootByEnemy -= 1;
                    }
                }
            }

            else
            {
                Buttons[Target.Row][Target.Column].BackColor = Color.Green;
                ShootingResult = HitResult.Missed;
            }


            /*
            if (Buttons[Target.Row][Target.Column].BackColor == Color.Blue)
            {
                foreach (Ship ship in PlayerFleet.Ships)
                {
                    ShootingResult = ship.Hit(Target);

                    if (ShootingResult == HitResult.Missed)
                    {
                        Buttons[Target.Row][Target.Column].BackColor = Color.Purple;
                    }
                    if (ShootingResult == HitResult.Hit)
                    {
                        Buttons[Target.Row][Target.Column].BackColor = Color.Red;
                        break;
                    }
                    else
                    {
                        foreach (Square Sunken in ship.Squares)
                        {
                            if (Sunken.SquareState == SquareState.Sunken)
                            {
                                Buttons[Sunken.Row][Sunken.Column].BackColor = Color.Black;
                            }
                        }
                        break;
                    }

                }
            }
            else
            {
                Buttons[Target.Row][Target.Column].BackColor = Color.Green;
            }
            */


            /* IMPLEMENTACIJA v1
            if (Buttons[Target.Row][Target.Column].BackColor == Color.Blue)
            {
                Buttons[Target.Row][Target.Column].BackColor = Color.Red;
                ShootingResult = HitResult.Hit;

                

                
                int brojCrvenih = 0;
                int brojPlavih = 0;
                if (Target.Row - 1 >= 0)
                {
                    if (Buttons[Target.Row - 1][Target.Column].BackColor == Color.Blue)
                        brojPlavih += 1;
                }
                else
                    brojPlavih += 1;
                if (Target.Row + 1 <= 9)
                {
                    if(Buttons[Target.Row + 1][Target.Column].BackColor == Color.Blue)
                    brojPlavih += 1;
                }
                else
                    brojPlavih += 1;
                if (Target.Column - 1 >= 0)
                {
                    if (Buttons[Target.Row][Target.Column - 1].BackColor == Color.Blue)
                        brojPlavih += 1;
                }
                else
                    brojPlavih += 1;
                if (Target.Column + 1 <= 9)
                {
                    if (Buttons[Target.Row][Target.Column + 1].BackColor == Color.Blue)
                        brojPlavih += 1;
                }
                else
                    brojPlavih += 1;
                if (Target.Row - 1 >= 0 && Target.Row + 1 <= 9 && Target.Column - 1 >= 0 && Target.Column + 1 <= 9)
                {
                    if (Buttons[Target.Row - 1][Target.Column].BackColor == Color.Red || Buttons[Target.Row + 1][Target.Column].BackColor == Color.Red ||
                    Buttons[Target.Row][Target.Column - 1].BackColor == Color.Red || Buttons[Target.Row][Target.Column + 1].BackColor == Color.Red)
                        brojCrvenih += 1;
                }
                
                if (brojCrvenih == 1 && brojPlavih == 0)
                {
                    ShootingResult = HitResult.Sunken;
                    Buttons[Target.Row][Target.Column].BackColor = Color.Black;
                    int row = Target.Row;
                    int column = Target.Column;
                    while (Buttons[row - 1][column].BackColor == Color.Red || Buttons[row + 1][column].BackColor == Color.Red ||
                        Buttons[row][column - 1].BackColor == Color.Red || Buttons[row][column + 1].BackColor == Color.Red)
                    {
                        if (Buttons[row - 1][column].BackColor == Color.Red)
                            row -= 1;
                        if (Buttons[row + 1][column].BackColor == Color.Red)
                            row += 1;
                        if (Buttons[row][column - 1].BackColor == Color.Red)
                            column -= 1;
                        if (Buttons[row][column + 1].BackColor == Color.Red)
                            column += 1;
                    }
                }
                

            }

            else
            {
                Buttons[Target.Row][Target.Column].BackColor = Color.Green;
                ShootingResult = HitResult.Missed;
            }
               */

            if (LengthToShoot == 0)
            {
                Console.WriteLine("You Win");
                var oneDimension = EnemyButtons.SelectMany(s => s);
                foreach (Button buttonToDisable in oneDimension)
                {
                    buttonToDisable.Enabled = false;
                }
                return;
            }

            if (LengthToShootByEnemy != 0)
                gunnery.RecordShooting(ShootingResult);
            else
            {
                Console.WriteLine("You Lose");
                var oneDimension = EnemyButtons.SelectMany(s => s);
                foreach(Button buttonToDisable in oneDimension)
                {
                    buttonToDisable.Enabled = false;
                }
                return;
            }



        }

        private void AiButton0x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x0);
        }

        private void AiButton0x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x1);
        }

        private void AiButton0x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x2);
        }

        private void AiButton0x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x3);
        }

        private void AiButton0x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x4);
        }

        private void AiButton0x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x5);
        }

        private void AiButton0x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x6);
        }

        private void AiButton0x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x7);
        }

        private void AiButton0x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x8);
        }

        private void AiButton0x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton0x9);
        }

        private void AiButton1x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x0);
        }

        private void AiButton1x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x1);
        }

        private void AiButton1x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x2);
        }

        private void AiButton1x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x3);
        }

        private void AiButton1x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x4);
        }

        private void AiButton1x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x5);
        }

        private void AiButton1x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x6);
        }

        private void AiButton1x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x7);
        }

        private void AiButton1x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x8);
        }

        private void AiButton1x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton1x9);
        }

        private void AiButton2x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x0);
        }

        private void AiButton2x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x1);
        }

        private void AiButton2x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x2);
        }

        private void AiButton2x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x3);
        }

        private void AiButton2x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x4);
        }

        private void AiButton2x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x5);
        }

        private void AiButton2x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x6);
        }

        private void AiButton2x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x7);
        }

        private void AiButton2x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x8);
        }

        private void AiButton2x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton2x9);
        }

        private void AiButton3x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x0);
        }

        private void AiButton3x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x1);
        }

        private void AiButton3x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x2);
        }

        private void AiButton3x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x3);
        }

        private void AiButton3x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x4);
        }

        private void AiButton3x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x5);
        }

        private void AiButton3x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x6);
        }

        private void AiButton3x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x7);
        }

        private void AiButton3x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x8);
        }

        private void AiButton3x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton3x9);
        }

        private void AiButton4x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x0);
        }

        private void AiButton4x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x1);
        }

        private void AiButton4x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x2);
        }

        private void AiButton4x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x3);
        }

        private void AiButton4x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x4);
        }

        private void AiButton4x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x5);
        }

        private void AiButton4x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x6);
        }

        private void AiButton4x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x7);
        }

        private void AiButton4x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x8);
        }

        private void AiButton4x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton4x9);
        }

        private void AiButton5x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x0);
        }

        private void AiButton5x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x1);
        }

        private void AiButton5x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x2);
        }

        private void AiButton5x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x3);
        }

        private void AiButton5x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x4);
        }

        private void AiButton5x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x5);
        }

        private void AiButton5x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x6);
        }

        private void AiButton5x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x7);
        }

        private void AiButton5x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x8);
        }

        private void AiButton5x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton5x9);
        }

        private void AiButton6x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x0);
        }

        private void AiButton6x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x1);
        }

        private void AiButton6x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x2);
        }

        private void AiButton6x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x3);
        }

        private void AiButton6x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x4);
        }

        private void AiButton6x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x5);
        }

        private void AiButton6x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x6);
        }

        private void AiButton6x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x7);
        }

        private void AiButton6x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x8);
        }

        private void AiButton6x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton6x9);
        }

        private void AiButton7x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x0);
        }

        private void AiButton7x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x1);
        }

        private void AiButton7x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x2);
        }

        private void AiButton7x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x3);
        }

        private void AiButton7x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x4);
        }

        private void AiButton7x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x5);
        }

        private void AiButton7x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x6);
        }

        private void AiButton7x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x7);
        }

        private void AiButton7x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x8);
        }

        private void AiButton7x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton7x9);
        }

        private void AiButton8x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x0);
        }

        private void AiButton8x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x1);
        }

        private void AiButton8x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x2);
        }

        private void AiButton8x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x3);
        }

        private void AiButton8x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x4);
        }

        private void AiButton8x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x5);
        }

        private void AiButton8x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x6);
        }

        private void AiButton8x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x7);
        }

        private void AiButton8x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x8);
        }

        private void AiButton8x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton8x9);
        }

        private void AiButton9x0_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x0);
        }

        private void AiButton9x1_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x1);
        }

        private void AiButton9x2_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x2);
        }

        private void AiButton9x3_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x3);
        }

        private void AiButton9x4_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x4);
        }

        private void AiButton9x5_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x5);
        }

        private void AiButton9x6_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x6);
        }

        private void AiButton9x7_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x7);
        }

        private void AiButton9x8_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x8);
        }

        private void AiButton9x9_Click(object sender, EventArgs e)
        {
            ButtonClickedFunction(AiButton9x9);
        }
    }
}
