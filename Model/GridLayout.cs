using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model
{
    public partial class GridLayout : Form
    {
        public GridLayout()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = panel1.CreateGraphics();
            Pen mypen = new Pen(new Color(), 2);
            int linesx = Convert.ToInt32(textBoxX.Text);
            int linesy = Convert.ToInt32(textBoxY.Text);
            int x = 0;
            int y = 0;
            int xspace = panel1.Size.Height / (linesx + 1);
            int yspace = panel1.Size.Width / (linesy + 1);


            for (int i = 0; i < linesx; ++i)
            {
                graphics.DrawLine(mypen, x, y, x, yspace * linesy);
                x += xspace;
            }

            for (int i = 0; i < linesy; ++i)
            {

            }
        }
    }
}
