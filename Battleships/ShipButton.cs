using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Battleships
{
    public class ShipButton : Button
    {
        public ShipButton(int row, int column) : base()
        {
            Row = row;
            Column = column;

            int width = 35;
            int height = 35;
            int offsetX = 30;
            int offsetY = 50;
            Size = new Size(width, height);
            Location = new Point((width * Row) + offsetX, (height * Column) + offsetY);

            isSunken = false;

            BackColor = Color.Gray;
            ForeColor = Color.Gray;

            // Border

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;
            FlatAppearance.BorderSize = 1;

            // Focus border ehnancement is not needed
            TabStop = false;
            NotifyDefault(false);

            Show();
        }

        public bool IsSunken{get {return isSunken;} }

        public Task Sunk()
        {
            var currentColor = Color.Red;
            return Task.Run(() => SunkAnimation(Color.Purple, currentColor));
        }

        public void SetColor(Color foreColor, Color backColor)
        {
            ForeColor = foreColor;
            BackColor = backColor;
        }
        
        public void Disable()
        {
            Enabled = false;
        }

        private void SunkAnimation(Color cOne, Color cTwo)
        {
            for (int i = 0; i < 3; ++i)
            {
                BackColor = cOne;
                Thread.Sleep(200);
                BackColor = cTwo;
                Thread.Sleep(200);
            }

            BackColor = cOne;
            isSunken = true;
        }


        public readonly int Row;
        public readonly int Column;
        private bool isSunken;
    }
}
