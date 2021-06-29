using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipGUI
{
    internal class GridButton : System.Windows.Forms.Button
    {
        public int Row, Column;

        public GridButton(int row, int column)
        {
            Row = row;
            Column = column;
        }

        private void Animation(Color c1, Color c2)
        {
            for (int i = 0; i < 3; i++)
            {
                BackColor = c1;
                Thread.Sleep(200);
                BackColor = c2;
                Thread.Sleep(200);
            }
            BackColor = c1;
        }

        public void AnimateButtonColor(Color c1)
        {
            var oldColor = BackColor;
            Task.Run(() => Animation(c1, oldColor));
        }
    }
}