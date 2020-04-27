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

namespace Battleships_GUI
{
    public partial class Form1 : Form
    {
        public int Rows { get; } = 10;
        public int Columns { get; } = 10;

        readonly int buttonSize = 42;
        CheckBox[,] buttons;

        public Form1()
        {
            InitializeComponent();
            AddAllLabels();
            CreateAllButtons();        
        }

        private void AddAllLabels()
        {
            // postavlja slova 
            int startLeft = panelMain.Left;
            for (int c = 0; c < Columns; ++c)
            {
                Controls.Add(new Label
                {
                    Top = panelMain.Top - buttonSize,
                    Left = startLeft,
                    Width = buttonSize,
                    Height = buttonSize,
                    Text = ((char)(c + 'A')).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                });
                startLeft += buttonSize;
            }
            //postavlja brojeve
            int startTop = panelMain.Top;
            for (int r = 0; r < Rows; ++r)
            {
                Controls.Add(new Label
                {
                    Top = startTop,
                    Left = panelMain.Left - buttonSize,
                    Width = buttonSize,
                    Height = buttonSize,
                    Text = (r + 1).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                });
                startTop += buttonSize;
            }
        }

        private void CreateAllButtons()
        {    
            buttons = new CheckBox[Rows, Columns];   // 10x10 checkbox
                                   
            int startTop = 0;
            for (int r = 0; r < Rows; ++r)
            {
                int startLeft = 0;
                for (int c = 0; c < Columns; ++c)
                {
                    buttons[r,c] = new CheckBox   //stvara "gumb"
                    {
                        Top = startTop,
                        Left = startLeft,
                        Width = buttonSize,
                        Height = buttonSize,
                        Appearance = Appearance.Button                     
                    };
                    panelMain.Controls.Add(buttons[r, c]);    //dodaje "gumb" u određeni red i kolonu          
                    startLeft += buttonSize;
                }
                startTop += buttonSize;
            }  
        }
    
        readonly Color shipColor = Color.Gray;
        readonly Color water = Color.LightBlue;       
        Fleet fleet = new Fleet();

        private void ResetAllButtons()
        {
            //sve gumbe vraća na unchecked state i default boju (voda)
            for (int r = 0; r < Rows; ++r)
            {
                for (int c = 0; c < Columns; ++c)
                {
                    buttons[r, c].Checked = false;    
                    buttons[r, c].BackColor = water;
                }
            }       
            //sve Squares od Ships u checked stanje i boju broda
            List<Ship> ships = new List<Ship>(fleet.Ships); 
        
            for (int i = 0; i < ships.Count(); i++)
            {
                foreach (var button in from square in ships[i].Squares
                                       let button = buttons[square.Row, square.Column]
                                       select button)
                {
                    button.Checked = true;
                    button.BackColor = shipColor;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shipwright ante = new Shipwright(Rows, Columns);
            fleet = ante.CreateFleet(new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 });
            ResetAllButtons();
        }
    }
}