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
    public partial class Form1 : Form
    {
        
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
            for (int y = 0; y < 10; ++y)
            {
                for (int x = 0; x < 10; ++x)
                {
                    rects[y * 10 + x] = new Rectangle(x * sz, y * sz, sz, sz);
                }
            }
            g.FillRectangle(sb, 0, 0, 401, 401);
            g.DrawRectangles(p, rects);
            sb.Dispose();
            p.Dispose();
        }

        private void btnSetFleet_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.LightGray);
            SolidBrush sb = new SolidBrush(Color.White);
            g.FillRectangles(sb, rects);
            SolidBrush sb1 = new SolidBrush(Color.Blue);
            Shipwright sw = new Shipwright(10, 10);
            Fleet fleet = sw.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            
            foreach (Ship ship in fleet.Ships )
            {
                foreach (Square square in ship.Squares)
                {
                    g.FillRectangle(sb1, rects[square.Column * 10 + square.Row]);
                }
            }
            g.DrawRectangles(p, rects);
        }
        const int sz = 40;
        private Rectangle[] rects = new Rectangle[100];
    }
}
