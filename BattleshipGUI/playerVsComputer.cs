using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using modelNmspc = Vsite.Oom.Battleship.Model;


namespace BattleshipGUI
{
    public partial class playerVsComputer : Form
    {
        private int color = 1;
        private List<List<Button>> playerButtons = new List<List<Button>>();
        private modelNmspc.Grid gr = new modelNmspc.Grid(10, 10);
        private modelNmspc.fleet fl = new modelNmspc.fleet();
        private List<modelNmspc.Square> currentShipMaking = new List<modelNmspc.Square>();
        private int currentPlacingShipLength = 0;
        private modelNmspc.Square shipHead = null;
        private modelNmspc.squareTerminator terminator = new modelNmspc.squareTerminator(10, 10);
        public playerVsComputer()
        {
            InitializeComponent();
        }

        private void playerVsComputer_Load(object sender, EventArgs e)
        {   //playerStuff       
            timer1.Interval = 500;
            timer1.Start();
            timer1.Enabled = true;
            DrawButtonPlayerGrid();
            //computerStuff
            //DrawButtonComputerGrid();
        }

        //private void DrawButtonComputerGrid()
        //{
        //    throw new NotImplementedException();
        //}

        private void DrawButtonPlayerGrid()  // draw 10x10 button grid for player (player side grid)
        {

            for (int i = 50; i != 550; i += 50)
            {
                List<Button> innerButtons = new List<Button>();
                for (int j = 100; j != 600; j += 50)
                {
                    Button button = new Button
                    {
                        Left = i,
                        Top = j,
                        Height = 49,
                        Width = 49
                    };
                    innerButtons.Add(button);
                    button.Click += (s, e) => { preGameStateButtonsHandler(button); };
                    button.BackColor = Color.DarkGreen;
                    Controls.Add(button);
                }
                playerButtons.Add(innerButtons);
            }
        }

        private void preGameStateButtonsHandler(Button clickedButton)    // event handler for buttons in pre game state (player choosing ships positions)
        {
            int x = 0, y = 0;
            foreach (var list in playerButtons)
            {
                if (list.IndexOf(clickedButton) != -1)       // get index of clicked button
                {
                    x = playerButtons.IndexOf(list);
                    y = list.IndexOf(clickedButton);
                }
            }
            if (shipHead == null)                   // if button that's clicked is first square of ship
            {
                clickedButton.BackColor = Color.Blue;
                getCurrentPlacingShipLength();
                shipHead = new modelNmspc.Square(x, y);
                currentShipMaking.Add(shipHead);
                List<modelNmspc.Square> s = checkForAvailableSquaresAfterSquareIsChosen(new modelNmspc.Square(x, y));
                if (s.Count() == 0)
                {
                    clickedButton.BackColor = Color.Green;
                    shipHead = null;
                    currentShipMaking.Clear();
                }
                else
                {
                    ChangeColorOfAvailableSquares(s);
                }
            }
            else
            {
                if (currentShipMaking[0].row < x)
                {
                    for (int i = 1; i < currentPlacingShipLength; i++)
                    {
                        currentShipMaking.Add(new modelNmspc.Square(currentShipMaking[0].row + i, currentShipMaking[0].column));
                    }
                    foreach (var s in currentShipMaking)
                    {
                        playerButtons[s.row][s.column].BackColor = Color.Blue;
                        playerButtons[s.row][s.column].Enabled = false;
                    }
                }
                if (currentShipMaking[0].row > x)
                {
                    for (int i = 1; i < currentPlacingShipLength; i++)
                    {
                        currentShipMaking.Add(new modelNmspc.Square(currentShipMaking[0].row - i, currentShipMaking[0].column));
                    }
                    foreach (var s in currentShipMaking)
                    {
                        playerButtons[s.row][s.column].BackColor = Color.Blue;
                        playerButtons[s.row][s.column].Enabled = false;
                    }
                    currentShipMaking.Reverse();
                }
                if (currentShipMaking[0].column < y)
                {
                    for (int i = 1; i < currentPlacingShipLength; i++)
                    {
                        currentShipMaking.Add(new modelNmspc.Square(currentShipMaking[0].row, currentShipMaking[0].column + i));
                    }
                    foreach (var s in currentShipMaking)
                    {
                        playerButtons[s.row][s.column].BackColor = Color.Blue;
                        playerButtons[s.row][s.column].Enabled = false;
                    }
                }
                if (currentShipMaking[0].column > y)
                {
                    for (int i = 1; i < currentPlacingShipLength; i++)
                    {
                        currentShipMaking.Add(new modelNmspc.Square(currentShipMaking[0].row, currentShipMaking[0].column - i));
                    }
                    foreach (var s in currentShipMaking)
                    {
                        playerButtons[s.row][s.column].BackColor = Color.Blue;
                        playerButtons[s.row][s.column].Enabled = false;
                    }
                    currentShipMaking.Reverse();
                }
                shipHead = null;
                fl.addShip(currentShipMaking);
                var toElim = terminator.ToEliminate(currentShipMaking);
                gr.eliminateSquares(toElim);
                var avaSquares = gr.GetAvailablePlacements(1);
                foreach (var list in playerButtons)
                {
                    foreach (var button in list)
                    {
                        if (button.BackColor == Color.Green)
                        {
                            button.BackColor = Color.Gray;
                            button.Enabled = false;
                        }
                    }
                }
                foreach (var item in avaSquares)
                {
                    foreach (var square in item)
                    {
                        playerButtons[square.row][square.column].BackColor = Color.Green;
                        playerButtons[square.row][square.column].Enabled = true;
                    }
                }
                currentShipMaking.Clear();
                changeLabel();
                if (fl.getNumberOfShips() == 10)
                {
                    button_WOC1.Enabled = true;
                    button_WOC1.ButtonColor = Color.DarkRed;

                }
            }
        }

        private void changeLabel()
        {
            string temp = string.Empty;
            string label = label2.Text;
            int val = 0;

            for (int i = 0; i < label.Length; i++)
            {
                if (Char.IsDigit(label[i]))
                    temp += label[i];
            }

            if (temp.Length > 0)
                val = int.Parse(temp);

            if (val - 1 == 0)
            {
                label1.Text = "Place the ship of length (" + (currentPlacingShipLength - 1) + ")" + " ...";
                getCurrentPlacingShipLength();
                switch (currentPlacingShipLength)
                {
                    case 4:
                        label2.Text = "2 ships to place ...";
                        break;
                    case 3:
                        label2.Text = "3 ships to place ...";
                        break;
                    case 2:
                        label2.Text = "4 ships to place ...";
                        break;
                    default:
                        changeColorOfRemainingSquares();
                        label2.Hide();
                        label1.Text = "All ships placed, waiting for confirmation ...";
                        break;
                }
            }
            else
            {
                if (val - 1 > 1)
                {
                    label2.Text = (val - 1) + " ships to place ...";
                }
                else
                {
                    label2.Text = (val - 1) + " ship to place ...";
                }
            }
        }

        private void changeColorOfRemainingSquares()
        {
            foreach (var list in playerButtons)
            {
                foreach (var button in list)
                {
                    if (button.BackColor == Color.Green)
                    {
                        button.BackColor = Color.Gray;
                        button.Enabled = false;
                    }
                }
            }
        }

        private void ChangeColorOfAvailableSquares(List<modelNmspc.Square> horizontalAndVertical)
        {
            var avaPlaces = gr.GetAvailablePlacements(1);
            List<modelNmspc.Square> avaSquares = new List<modelNmspc.Square>();
            foreach (var s in avaPlaces)
            {
                foreach (var square in s)
                {
                    avaSquares.Add(square);
                }
            }
            modelNmspc.Square sq = null;
            modelNmspc.Square sq2 = null;
            foreach (var list in playerButtons)
            {
                foreach (var button in list)
                {
                    sq = new modelNmspc.Square(list.IndexOf(button), playerButtons.IndexOf(list));
                    sq2 = new modelNmspc.Square(playerButtons.IndexOf(list), list.IndexOf(button));
                    if (!horizontalAndVertical.Contains(sq) && avaSquares.Contains(sq2))
                    {
                        button.BackColor = Color.Gray;
                        button.Enabled = false;
                    }
                }
            }
            foreach (var square in currentShipMaking)
            {
                playerButtons[square.row][square.column].BackColor = Color.Blue;
                playerButtons[square.row][square.column].Enabled = false;
            }
            foreach (var i in horizontalAndVertical)
            {
                playerButtons[i.column][i.row].BackColor = Color.Green;
            }
        }

        private List<modelNmspc.Square> checkForAvailableSquaresAfterSquareIsChosen(modelNmspc.Square b)
        {
            List<modelNmspc.Square> horizontalAndVerticalSquares = new List<modelNmspc.Square>();
            var aPlaces = gr.GetAvailablePlacements(1);
            List<modelNmspc.Square> aSquares = new List<modelNmspc.Square>();
            List<modelNmspc.Square> temp = new List<modelNmspc.Square>();
            int y = b.column;
            int x = b.row - 1;
            int j = 0;
            foreach (var item in aPlaces)
            {
                foreach (var square in item)
                {
                    aSquares.Add(new modelNmspc.Square(square.column, square.row));
                }
            }
            while (x >= 0)
            {
                temp.Add(new modelNmspc.Square(y, x));
                if (temp.Count == currentPlacingShipLength - 1)
                {
                    foreach (var sq in temp)
                    {
                        if (aSquares.Contains(sq))
                            j++;
                    }
                    if (j == currentPlacingShipLength - 1)
                    {
                        for (int i = 0; i < currentPlacingShipLength - 1; ++i)
                        {
                            horizontalAndVerticalSquares.Add(temp[i]);
                        }
                    }
                    break;

                }
                x -= 1;
            }
            temp.Clear();
            x = b.row + 1;
            j = 0;
            while (x <= 9)
            {
                temp.Add(new modelNmspc.Square(y, x));
                if (temp.Count == currentPlacingShipLength - 1)
                {
                    foreach (var sq in temp)
                    {
                        if (aSquares.Contains(sq))
                            j++;
                    }
                    if (j == currentPlacingShipLength - 1)
                    {
                        for (int i = 0; i < currentPlacingShipLength - 1; ++i)
                        {
                            horizontalAndVerticalSquares.Add(temp[i]);
                        }
                    }
                    break;

                }
                x += 1;
            }
            temp.Clear();
            y = b.column - 1;
            x = b.row;
            j = 0;
            while (y >= 0)
            {
                temp.Add(new modelNmspc.Square(y, x));
                if (temp.Count == currentPlacingShipLength - 1)
                {
                    foreach (var sq in temp)
                    {
                        if (aSquares.Contains(sq))
                            j++;
                    }
                    if (j == currentPlacingShipLength - 1)
                    {
                        for (int i = 0; i < currentPlacingShipLength - 1; ++i)
                        {
                            horizontalAndVerticalSquares.Add(temp[i]);
                        }
                    }
                    break;
                }
                y -= 1;
            }
            temp.Clear();
            y = b.column + 1;
            j = 0;
            while (y <= 9)
            {
                temp.Add(new modelNmspc.Square(y, x));
                if (temp.Count == currentPlacingShipLength - 1)
                {
                    foreach (var sq in temp)
                    {
                        if (aSquares.Contains(sq))
                            j++;
                    }
                    if (j == currentPlacingShipLength - 1)
                    {
                        for (int i = 0; i < currentPlacingShipLength - 1; ++i)
                        {
                            horizontalAndVerticalSquares.Add(temp[i]);
                        }
                    }
                    break;
                }
                y += 1;
            }
            horizontalAndVerticalSquares = horizontalAndVerticalSquares.Distinct().ToList();
            return horizontalAndVerticalSquares;

        }

        private void getCurrentPlacingShipLength()  // parse length of current placing ship from label
        {
            string temp = string.Empty;
            string label = label1.Text;
            int val = 0;

            for (int i = 0; i < label.Length; i++)
            {
                if (Char.IsDigit(label[i]))
                    temp += label[i];
            }

            if (temp.Length > 0)
                val = int.Parse(temp);

            currentPlacingShipLength = val;
        }

        private void button_WOC2_Click(object sender, EventArgs e) //reset fleet button
        {
            resetButtonsColor();
            gr = new modelNmspc.Grid(10, 10);
            fl = new modelNmspc.fleet();
            currentShipMaking = new List<modelNmspc.Square>();
            currentPlacingShipLength = 0;
            shipHead = null;
            terminator = new modelNmspc.squareTerminator(10, 10);
            resetLabels();
            button_WOC1.Enabled = false;
            button_WOC1.ButtonColor = Color.Black;


        }

        private void resetLabels()
        {
            label2.Text = "1 ship(s) to place...";
            label1.Text = "Place the ship of length(5)...";
            label1.Show();
            label2.Show();
        }

        private void resetButtonsColor()
        {
            foreach (var item in playerButtons)
            {
                foreach (var button in item)
                {
                    button.BackColor = Color.Green;
                    button.Enabled = true;
                }
            }
        }
        private void button_WOC1_Click(object sender, EventArgs e)
        {
            button_WOC1.Hide();
            button_WOC2.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (color % 2 == 0)
            {
                if (button_WOC1.Enabled)
                {
                    button_WOC1.BorderColor = Color.DarkGreen;
                    color--;
                }
            }
            else
            {
                if (button_WOC1.Enabled)
                {
                    button_WOC1.BorderColor = Color.LightGreen;
                    color++;
                }
            }
        }
    }
}
