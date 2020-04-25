using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.BattleShip.Model;

namespace Battleship_GUI
{
    public partial class Form1 : Form
    {
        Rectangle[,] grid;
        Graphics graphics;
        Brush brush;
        Pen pen = new Pen(Color.Gray);
        int width_height = 30;
        

        public Form1()
        {
            InitializeComponent();
            grid = new Rectangle[10, 10];
            graphics = panel1.CreateGraphics();
        }

        
       
        private void Form1_Load(object sender, EventArgs e)
        {
            //ignore
        }


        private void CreateGrid(object sender, PaintEventArgs e)
        {
            

            Rectangle rect = new Rectangle();
            rect.Width = width_height;
            rect.Height = width_height;
            rect.Y = 30;

            for(int row = 0; row < 10; ++row)
            {
                rect.X = 0;
                for(int column = 0; column < 10; ++column)
                {
                    graphics.DrawRectangle(pen, rect);
                    grid[row, column] = rect;
                    rect.X += 30;
                }
                rect.Y += 30;
            }
        }

        private void ClearFleet()
        {
            Rectangle rect = new Rectangle();
            brush = new SolidBrush(Color.Gray);
            
            

            for (int rows = 0; rows < 10; ++rows)
            {
                
                for (int columns = 0; columns < 10; ++columns)
                {
                    rect = grid[rows, columns];
                    rect.Y += 2;
                    rect.X += 2;
                    rect.Width = width_height - 2;
                    rect.Height = width_height - 2;
                    graphics.FillRectangle(brush, rect);
                    
                }
               
            }
        }


        //Posloži flotu
        private void button1_Click(object sender, EventArgs e)
        {
            ClearFleet();

            Rectangle rect = new Rectangle();
            brush = new SolidBrush(Color.Blue);
            Shipwright shipwright = new Shipwright(10, 10);
            Fleet fleet = new Fleet();
            List<int> ship_lengths = new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };

            fleet = shipwright.CreateFleet(ship_lengths);

            for(int rows = 0; rows < 10; ++rows)
            {
                for(int columns = 0; columns < 10; ++columns)
                {

                    foreach(Ship ship in fleet.Ships) {
                        if(ship.Squares.Contains(new Square(rows, columns)))
                        {
                            rect = grid[rows, columns];
                            rect.Y += 2;
                            rect.X += 2;
                            rect.Width = width_height - 2;
                            rect.Height = width_height - 2;
                            graphics.FillRectangle(brush, rect);
                        }
                    }
                }
            }
        }
    }
}
