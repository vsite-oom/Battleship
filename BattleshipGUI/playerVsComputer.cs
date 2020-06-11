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
        //defines
        private static Color shipSunkColor = Color.DarkOrange;
        private static Color shipHitColor = Color.Red;
        private static Color shipMissedColor = Color.Black;
        private static Color shipPlacedOnGridColor = Color.Blue;



        //player stuff
        private int color = 1;
        private List<List<Button>> playerButtons = new List<List<Button>>();
        private List<List<Button>> computerButtons = new List<List<Button>>();
        private modelNmspc.Grid playerGrid = new modelNmspc.Grid(10, 10);
        private modelNmspc.fleet playerFleet = new modelNmspc.fleet();
        private List<modelNmspc.Square> currentShipMaking = new List<modelNmspc.Square>();
        private int currentPlacingShipLength = 0;
        private modelNmspc.Square shipHead = null;
        private modelNmspc.squareTerminator terminator = new modelNmspc.squareTerminator(10, 10);
        private Random rand = new Random();

        //computer stuff
        private modelNmspc.Grid computerGrid = new modelNmspc.Grid(10, 10);
        private modelNmspc.fleet computerFleet = new modelNmspc.fleet();
        private modelNmspc.Gunner gun = new modelNmspc.Gunner(10, 10, new int[] { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 });

        public playerVsComputer()
        {
            InitializeComponent();
        }

        private void playerVsComputer_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
            timer1.Enabled = true;
            DrawButtonPlayerGrid(1);
            DrawButtonPlayerGrid(2);
        }

        private void DrawButtonPlayerGrid(int x)  // draw 10x10 button grid for player (player side grid)
        {
            if (x == 1)
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
                        button.Click += (s, e) => { playersButtonGridClickHandler(button); };
                        button.BackColor = Color.Green;
                        Controls.Add(button);
                    }
                    playerButtons.Add(innerButtons);
                }
            }
            else if (x == 2)
            {
                for (int i = 630; i != 1130; i += 50)
                {
                    List<Button> innerButtons = new List<Button>();
                    for (int j = 100; j != 600; j += 50)
                    {
                        Button button = new Button
                        {
                            Left = i,
                            Top = j,
                            Height = 49,
                            Width = 49,
                            Enabled = false
                        };
                        innerButtons.Add(button);
                        button.Click += (s, e) => { computerButtonGridClickHandler(button); };
                        button.BackColor = Color.Gray;
                        Controls.Add(button);
                    }
                    computerButtons.Add(innerButtons);
                }


            }
        }

        private void computerButtonGridClickHandler(Button clickedButton)
        {
            int x = 0, y = 0;
            foreach (var list in computerButtons)
            {
                if (list.IndexOf(clickedButton) != -1)
                {
                    x = computerButtons.IndexOf(list);
                    y = list.IndexOf(clickedButton);
                }
            }
            if (modelNmspc.HitResult.Hit == computerFleet.Hit(new modelNmspc.Square(x, y)))
            {
                clickedButton.BackColor = shipHitColor;
                clickedButton.Enabled = false;
                label3.Text = "Hit !";
                return;
            }
            if (modelNmspc.HitResult.Sunken == computerFleet.Hit(new modelNmspc.Square(x, y)))
            {
                clickedButton.BackColor = shipSunkColor;
                clickedButton.Enabled = false;
                label3.Text = "You have sunk a ship!";
                if (checkIfPlayerWon() == 1) { return; };
                disableComputerGridButtons();
                displayMessageAndWaitForXSeconds(1);
                return;
            }
            clickedButton.BackColor = shipMissedColor;
            clickedButton.Enabled = false;
            label3.Text = "You missed :(";
            disableComputerGridButtons();
            displayMessageAndWaitForXSeconds(0);
        }

        private int checkIfComputerWon()
        {
            foreach (var item in playerButtons)
            {
                foreach (var button in item)
                {
                    if (button.BackColor == shipPlacedOnGridColor)
                    {
                        return 0;
                    }
                }
            }
            disableComputerGridButtons();
            label7.Text = "YOU LOST! :(";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Show();
            return 2;
        }

        private int checkIfPlayerWon()
        {
            int counter = 0;
            foreach (var item in computerButtons)
            {
                foreach (var button in item)
                {
                    if (button.BackColor == shipHitColor || button.BackColor == shipSunkColor)
                    {
                        counter++;
                    }
                }
            }
            if (counter == 30)
            {
                disableComputerGridButtons();
                label7.TextAlign = ContentAlignment.MiddleCenter;
                label7.Show();
                return 1;

            }
            return 0;
        }

        private void enableComputerGrid()
        {
            foreach (var items in computerButtons)
            {
                foreach (var button in items)
                {
                    if (button.BackColor != shipSunkColor && button.BackColor != shipHitColor)
                    {
                        button.Enabled = true;
                    }
                }
            }
        }

        private void displayMessageAndWaitForXSeconds(int x)
        {
            if (x == 1)
            {
                label3.Text = "You have sunk a ship ! Enemy shooting ...";
            }
            else
            {
                label3.Text = "Enemy shooting ...";
            }
            timer2.Interval = 2000;
            timer2.Start();
            timer2.Enabled = true;
        }

        private int computerTurnToShoot()
        {
            int counter = 0;
            var square = gun.NextTarget();
            var sq = new modelNmspc.Square(square.column, square.row);
            foreach (var ship in playerFleet.Ships)
            {
                if (ship.squares.Contains(sq))
                {
                    foreach (var x in ship.squares)
                    {
                        if (x == sq)
                        {
                            x.SetState(modelNmspc.HitResult.Hit);
                        }
                    }
                    foreach (var x in ship.squares)
                    {
                        if (x.SquareState == modelNmspc.SquareState.Hit)
                        {
                            ++counter;
                        }
                    }
                    if (counter == ship.squares.Count())
                    {
                        playerButtons[square.column][square.row].BackColor = shipSunkColor;
                        gun.ProcessHitResult(modelNmspc.HitResult.Sunken);
                        if (checkIfComputerWon() == 2) { return 4; }
                        return 3;
                    }
                    else
                    {
                        playerButtons[square.column][square.row].BackColor = shipHitColor;
                        gun.ProcessHitResult(modelNmspc.HitResult.Hit);
                        return 2;
                    }
                }
            }
            playerButtons[square.column][square.row].BackColor = shipMissedColor;
            gun.ProcessHitResult(modelNmspc.HitResult.Missed);
            return 1;
        }


        private void disableComputerGridButtons()
        {
            foreach (var items in computerButtons)
            {
                foreach (var button in items)
                {
                    if (button.BackColor != shipHitColor && button.BackColor != shipSunkColor)
                    {
                        button.Enabled = false;
                    }
                }
            }
        }

        private void playersButtonGridClickHandler(Button clickedButton)    // event handler for buttons in pre game state (player choosing ships positions)
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
                clickedButton.BackColor = shipPlacedOnGridColor;
                getCurrentPlacingShipLength();
                shipHead = new modelNmspc.Square(x, y);
                currentShipMaking = new List<modelNmspc.Square>
                {
                    shipHead
                };
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
                        playerButtons[s.row][s.column].BackColor = shipPlacedOnGridColor;
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
                        playerButtons[s.row][s.column].BackColor = shipPlacedOnGridColor;
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
                        playerButtons[s.row][s.column].BackColor = shipPlacedOnGridColor;
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
                        playerButtons[s.row][s.column].BackColor = shipPlacedOnGridColor;
                        playerButtons[s.row][s.column].Enabled = false;
                    }
                    currentShipMaking.Reverse();
                }
                shipHead = null;
                playerFleet.addShip(currentShipMaking);
                var toElim = terminator.ToEliminate(currentShipMaking);
                playerGrid.eliminateSquares(toElim);
                var avaSquares = playerGrid.GetAvailablePlacements(1);
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
                currentShipMaking = null;
                changeLabel();
                if (playerFleet.getNumberOfShips() == 10)
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
            var avaPlaces = playerGrid.GetAvailablePlacements(1);
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
            var aPlaces = playerGrid.GetAvailablePlacements(1);
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
            playerGrid = new modelNmspc.Grid(10, 10);
            playerFleet = new modelNmspc.fleet();
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
            label2.Text = "1 ship to place...";
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
            label1.Hide();
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Show();
            computerGrid = new modelNmspc.Grid(10, 10);
            computerFleet = new modelNmspc.fleet();
            while (generateComputerGrid() == 404)
            {
                computerGrid = new modelNmspc.Grid(10, 10);
                computerFleet = new modelNmspc.fleet();
            }
            foreach (var i in computerButtons)
            {
                foreach (var button in i)
                {
                    button.Enabled = true;
                }
            }
            //var x = computerFleet.Ships;                 //debugging purpose only
            //foreach (var t in x)                         //draws computer fleet
            //{
            //    foreach (var z in t.squares)
            //    {
            //        computerButtons[z.row][z.column].BackColor = Color.Pink;
            //    }
            //}

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
            label4.ForeColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            label5.ForeColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            label7.ForeColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        private int generateComputerGrid()
        {
            int trigger = 1;
            int shipLength = 5;
            var availableSquares = computerGrid.GetAvailablePlacements(shipLength);
            if (ChooseRandomShipPositions_AddToFleet_Eliminate(availableSquares) == 404) { return 404; };
            for (int i = 0; i < 9; ++i)
            {
                if (trigger < 3) { shipLength = 4; }
                if (trigger < 6 && trigger >= 3) { shipLength = 3; }
                if (trigger <= 10 && trigger >= 6) { shipLength = 2; }


                availableSquares = computerGrid.GetAvailablePlacements(shipLength);
                if (ChooseRandomShipPositions_AddToFleet_Eliminate(availableSquares) == 404) { return 404; };
                ++trigger;
            }
            return 1;
        }

        private int ChooseRandomShipPositions_AddToFleet_Eliminate(IEnumerable<IEnumerable<modelNmspc.Square>> availablePositions)
        {

            int result = availablePositions.Count();
            int randomPosition = rand.Next(0, result);
            int counter = 0;
            List<modelNmspc.Square> squaresToAddAndElim = null;
            IEnumerable<modelNmspc.Square> ship = null;
            using (var sequenceEnum = availablePositions.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    if (counter == randomPosition)
                    {
                        ship = sequenceEnum.Current;
                    }
                    ++counter;
                }
            }
            int initCounter = 0;
            if (ship == null)
            {
                return 404;
            }

            using (var sequenceEnum = ship.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    if (initCounter == 0)
                    {
                        var square = sequenceEnum.Current;
                        squaresToAddAndElim = new List<modelNmspc.Square> { new modelNmspc.Square(square.row, square.column) };
                        initCounter = 1;
                    }
                    else
                    {
                        var square = sequenceEnum.Current;
                        squaresToAddAndElim.Add(square);
                    }

                }
            }
            computerFleet.addShip(squaresToAddAndElim);
            modelNmspc.squareTerminator terminator = new modelNmspc.squareTerminator(computerGrid.Rw, computerGrid.Cl);
            var toElim = terminator.ToEliminate(squaresToAddAndElim);
            computerGrid.eliminateSquares(toElim);

            return 1;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var x = computerTurnToShoot();
            if (x == 1)
            {
                timer2.Stop();
                label3.Text = "Enemy missed ... It's your turn !";
                enableComputerGrid();
            }
            else if (x == 2)
            {
                string message1 = "Enemy hit your ship !";
                string message2 = "Enemy strikes again !";
                string message3 = "Enemy is on fire !";
                string message4 = "Enemy is unstoppable !";
                if (label3.Text == "Enemy shooting ...")
                {
                    label3.Text = message1;
                    return;
                }
                if (label3.Text == message1)
                {
                    label3.Text = message2;
                    return;
                }
                if (label3.Text == message2)
                {
                    label3.Text = message3;
                    return;
                }
                if (label3.Text == message3)
                {
                    label3.Text = message4;
                    return;
                }
            }
            else if (x == 3)
            {
                timer2.Stop();
                enableComputerGrid();
                label3.Text = "Enemy sunk your ship ! It's your turn now !";
            }
            else if (x == 4)
            {
                timer2.Stop();
            }
        }
    }
}
