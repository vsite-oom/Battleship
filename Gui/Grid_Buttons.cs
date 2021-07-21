using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipGUI {
    internal class Grid_Buttons : System.Windows.Forms.Button {
        public int grid_button_row, grid_button_column;

        public Grid_Buttons(int row, int column) {
            this.grid_button_row = row;
            this.grid_button_column = column;
        }

        private void animate(Color c1, Color c2) {
            for (int i = 0; i <= 3; i++) {

                BackColor = c1;
                Thread.Sleep(200);
                BackColor = c2;
                Thread.Sleep(200);
            }
            BackColor = c1;

        


        }

        public void animate_button(Color c1) {
            var oldColor = BackColor;
            Task.Run(() => animate(c1, oldColor));
        }
    }
}