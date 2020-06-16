using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vsite.Oom.Battleship.Model
{
    public partial class FormGB : Form
    {
        public FormGB()
        {
            
            InitializeComponent();
            this.shipwright = new Shipwright(10, 10);
            pcsFleet = shipwright.CreateFleet(shipLengths);
            pcsGunner = new Gunner(10, 10, shipLengths);
            this.myEvidenceGrid = new Grid(10, 10);
            this.pcsEvidenceGrid = new Grid(10, 10);
            myEvidenceSquares = myEvidenceGrid.GetAvailablePlacements(1);
            pcsEvidenceSquares = pcsEvidenceGrid.GetAvailablePlacements(1);
            this.rects = new Rectangle[100];
            CreateRects();
            MessageBox.Show("\n1.  Click the \"Set Fleet\" button to arrange " +
                "your fleet to your liking\n2.  You play first: select your target by clicking on a PC's Fleet square of your choice\n" +
                "3.  The PC will follow...","***  HOW TO PLAY ***");
        }

        private void CreateRects()
        {
            for (int y = 0; y < 10; ++y)
            {
                for (int x = 0; x < 10; ++x)
                {
                    rects[y * 10 + x] = new Rectangle(x * rectSize, y * rectSize, rectSize, rectSize);
                }
            }
        }

        private void btnSetFleet_Click(object sender, EventArgs e)
        {
            myFleet = shipwright.CreateFleet(shipLengths);
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.SlateGray);
            SolidBrush sb = new SolidBrush(Color.Silver);
            g.FillRectangle(sb, 0, 0, 401, 401);

            foreach (IEnumerable<Square> isq in pcsEvidenceSquares)
            {
                foreach (Square square in isq)
                {
                    sb = GetBrush(square.SquareState);
                    g.FillRectangle(sb, rects[square.Column + square.Row * 10]);
                }
            }
            if (myFleet!=null)
            {
                foreach (Ship ship in myFleet.Ships)
                {
                    foreach (Square square in ship.Squares)
                    {
                        if (square.SquareState == SquareState.None)
                            sb.Color = Color.Blue;
                        else
                            sb = GetBrush(square.SquareState);
                        g.FillRectangle(sb, rects[square.Column + square.Row * 10]);
                    }
                }
            }
            g.DrawRectangles(p, rects);
            sb.Dispose();
            p.Dispose();
        }
        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel2.CreateGraphics();
            Pen p = new Pen(Color.SlateGray);
            SolidBrush sb = new SolidBrush(Color.Silver);
            g.FillRectangle(sb, 0, 0, 401, 401);

            foreach (IEnumerable<Square> isq in myEvidenceSquares)
            {
                foreach(Square square in isq)
                {
                    sb = GetBrush(square.SquareState);
                    g.FillRectangle(sb, rects[square.Column + square.Row * 10]);
                }
            }

            if (pcsFleet != null)
            {
                foreach (Ship ship in pcsFleet.Ships)
                {
                    foreach (Square square in ship.Squares)
                    {
                        if (square.SquareState == SquareState.Sunken)
                        {
                            sb = GetBrush(square.SquareState);
                            g.FillRectangle(sb, rects[square.Column + square.Row * 10]);
                        }
                    }
                }
            }
            g.DrawRectangles(p, rects);
            sb.Dispose();
            p.Dispose();
        }
        
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (myFleet == null)
            {
                MessageBox.Show("First you must set your fleet up!", "Hey!");
                return;
            }
            btnSetFleet.Enabled = false;
            int index;
            index = e.Y / rectSize * 10 + e.X / rectSize;
            Square mytarget = new Square(e.Y/rectSize,e.X/rectSize);
            HitResult hr = pcsFleet.Hit(mytarget);
            myEvidenceSquares.ElementAt(index).ElementAt(0).SetState(hr);
            Square pcsTarget = pcsGunner.NextTarget();
            hr = myFleet.Hit(pcsTarget);
            pcsGunner.ProcessHitResult(hr);
            if(hr==HitResult.Hit)
            {
                foreach (Ship ship in myFleet.Ships)
                {
                    foreach (Square square in ship.Squares)
                    {
                        if (square.Column == pcsTarget.Column && square.Row == pcsTarget.Row)
                            square.SetState(HitResult.Hit);
                    }
                }
            }
            pcsEvidenceSquares.ElementAt(pcsTarget.Column+pcsTarget.Row*10).ElementAt(0).SetState(hr);
            panel2.Invalidate();
            panel1.Invalidate();
        }
        
        private SolidBrush GetBrush(SquareState st)
        {
            Color color;
            switch (st)
            {
                case SquareState.Missed:
                    color = Color.FromName("White");
                    break;
                case SquareState.Hit:
                    color = Color.FromName("Red");
                    break;
                case SquareState.Sunken:
                    color = Color.FromName("Brown");
                    break;
                default:
                    color = Color.FromName("Silver");
                    break;
            }
            SolidBrush sb = new SolidBrush(color);
            return sb;
        }
        const int rectSize = 40;
        private Rectangle[] rects;
        private Grid myEvidenceGrid;
        private Grid pcsEvidenceGrid;
        private IEnumerable<IEnumerable<Square>> myEvidenceSquares;
        private IEnumerable<IEnumerable<Square>> pcsEvidenceSquares;
        private Fleet myFleet;
        private Fleet pcsFleet;
        private Shipwright shipwright;
        private int[] shipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        private Gunner pcsGunner;
    }
}
