using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Boards
{
    class EvidenceGrid : GridDisplay
    {

        public event EventHandler<string> RaiseButtonSelectedEvent;
        public EvidenceGrid()
        {
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    buttons[r, c].Click += ButtonClicked;

                }
            }

        }//constructor

        public void Reset()
        {
            ResetButtons();
        }

        private void ButtonClicked(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < columns; ++c)
                {
                    if (buttons[r, c] == button)
                    {
                        if (button.BackColor == buttonColor)                        
                            RaiseButtonSelectedEvent?.Invoke(button, ((char)(c + 'A')).ToString() + " " + (r + 1).ToString());
                        else                       
                            RaiseButtonSelectedEvent?.Invoke(button, "");
                    }
                }
            }
            
        }

        internal void OnShootingCompleted(object sender, ShootingCompletedEventArgs e)//update buttons on shooting completed.
        {
            if (e.hitResult == HitResult.Missed)
            {
                buttons[e.row, e.column].BackColor = missedColor;
                buttons[e.row, e.column].Enabled = false;
            }
            else if (e.hitResult == HitResult.Hit)
            {
                buttons[e.row, e.column].BackColor = hitColor;
                buttons[e.row, e.column].Enabled = false;
            }
            else if (e.hitResult == HitResult.Sunken)
            {
                
                buttons[e.row, e.column].BackColor = sunkenColor;
                buttons[e.row, e.column].Enabled = false;
            }


        }

        private static Color hitColor = Color.Red;
        private static Color sunkenColor = Color.Brown;
        private static Color missedColor = Color.White;
    }
}