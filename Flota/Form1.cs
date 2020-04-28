using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flota
{
    public partial class Form1 : Form
    {
        const int sz = 40;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.LightGray);
            SolidBrush sb = new SolidBrush(Color.White);
            Rectangle[] rects = new Rectangle[100];
            for (int y = 0; y < 10; ++y)
            {
                for (int x = 0; x < 10; ++x)
                {
                    rects[y * 10 + x] = new Rectangle(x * sz, y * sz, sz, sz);
                }
            }
            g.FillRectangle(sb, 0, 0, 401, 401);
            g.DrawRectangles(p, rects);
        }

        private void btnSetFleet_Click(object sender, EventArgs e)
        {

        }
    }
}
