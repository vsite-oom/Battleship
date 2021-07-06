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

namespace Battleships
{
    public partial class FleetControl : UserControl
    {
        public event EventHandler ButtonClick;

        public FleetControl()
        {
            InitializeComponent();

            InternalInitialization();
            GuiInitialization(10, 10);
        }

        public void Initialize()
        {
            ClearColor();
            SetRemainingShipsNumber();
        }

        public void Miss(Square square)
        {
            ships[square.Row, square.Column].Invoke((MethodInvoker)delegate ()
            {
                ships[square.Row, square.Column].SetColor(Color.Black, Color.Black);
                ships[square.Row, square.Column].Disable();
            });
        }

        public void Hit(Square square)
        {
            ships[square.Row, square.Column].SetColor(Color.Red, Color.Red);
        }

        public void SunkShip(IEnumerable<Square> ship)
        {
            Task t = null;
            foreach (var square in ship)
            {
                t = ships[square.Row, square.Column].Sunk(); // get only the last one and block until he is done
            }

            t.Wait();
        }

        public void ClearColor()
        {
            foreach (var button in ships)
            {
                button.BackColor = Color.WhiteSmoke;
                button.ForeColor = Color.WhiteSmoke;
            }
        }

        public void PlaceShips(Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    ships[square.Row, square.Column].SetColor(Color.Blue, Color.Blue);
                }
            }
        }
        
        public void InvalidateShipCount()
        {
            var number = int.Parse(RemainingShipsNumber.Text);
            --number;

            DecreaseShipCount(number.ToString());
        }

        protected void DecreaseShipCount(string text)
        {
            Invoke((MethodInvoker)delegate ()
            {
                RemainingShipsNumber.Text = text;
            });
        }

        private void SetRemainingShipsNumber()
        {
            RemainingShipsNumber.Text = "10";
        }

        private void Button_click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(sender, e);
        }

        private void GuiInitialization(int row, int column)
        {

            RemainingShips = new Label {
                Text = "Remaining ships: ",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(100, 30),
                Location = new Point(40, 2)
            };

            RemainingShipsNumber = new Label
            {
                Text = "10",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(30, 30),
                Location = new Point(140, 2)
            };

            Controls.Add(RemainingShips);
            Controls.Add(RemainingShipsNumber);
            InitializeButtons(row, column);
            InitializeText(row, column);
        }

        private void InternalInitialization()
        {
            verticalLabels = new Label[10];
            horisontalLabels = new Label[10];
            ships = new ShipButton[10, 10];
        }

        private void InitializeButtons(int rows, int columns)
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    var button = new ShipButton(r, c);
                    button.Click += Button_click;

                    ships[r, c] = button;
                    Controls.Add(button);
                }
            }
        }

        private void InitializeText(int rows, int columns)
        {
            InitializeHorisontalLabels(columns);
            InitializeVerticalLabel(rows);
        }

        private void InitializeHorisontalLabels(int columns)
        {
            int width = 35;
            int height = 35;
            int offsetX = 30;
            int yPos = 10;

            for (int c = 0; c < columns; ++c)
            {
                Label l = new Label {
                    Text = ((char)(c + 'A')).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(width, height),
                    Location = new Point(((width * c) + offsetX), (height + (yPos * 2)) - width)
                };
                horisontalLabels[c] = l;

                Controls.Add(l);
            }
        }

        private void InitializeVerticalLabel(int rows)
        {
            int width = 35;
            int height = 35;
            int xPos = -10;
            int offsetY = 30;

            for (int r = 0; r < rows; ++r)
            {
                Label l = new Label { 
                    Text = r.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(width, height),
                    Location = new Point(xPos, ((height * r) + height + (offsetY / 2))) 
                };

                verticalLabels[r] = l;
                Controls.Add(l);
            }
        }

        Label[] verticalLabels;
        Label[] horisontalLabels;

        Label RemainingShips;
        Label RemainingShipsNumber;
        private ShipButton[,] ships;
    }
}
