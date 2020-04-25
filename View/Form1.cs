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

namespace View
{
    public partial class FormMain : Form
    {

        Shipwright sw;
         

        public FormMain()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            GridCtrl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            FormatLayoutTable(GridCtrl, 10);
            FillTableWithPanels(GridCtrl);

            
        }

        // Helper methods //

        private void FormatLayoutTable(TableLayoutPanel tbl, int percent)
        {
            TableLayoutColumnStyleCollection ColumnStyles = tbl.ColumnStyles;
            foreach (ColumnStyle style in ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = percent;
            }

            TableLayoutRowStyleCollection RowStyles = tbl.RowStyles;
            foreach (RowStyle style in RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = percent;
            }

        }
        private void PaintTableCells(TableLayoutPanel tbl, Color clr)
        {
            foreach (Panel panel in tbl.Controls)
            {
                panel.BackColor = clr;
            }
        }
        private void PaintSingleTableCell(TableLayoutPanel tbl, Color clr, int column, int row)
        {
            var pnl = tbl.GetControlFromPosition(column, row);
            pnl.BackColor = clr;
        }
        private void FillTableWithPanels(TableLayoutPanel tbl)
        {
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                for (int j = 0; j < tbl.RowCount; j++)
                {
                    Panel nwpanel = new Panel();
                    nwpanel.Dock = DockStyle.Fill;
                    nwpanel.Margin = new Padding(0);
                    nwpanel.BackColor = Color.White;
                    //nwpanel.MouseHover += Nwpanel_MouseHover;
                    nwpanel.MouseClick += PaintCell_MouseClick;


                    tbl.Controls.Add(nwpanel);
                }
            }
        }
        private void PaintCell_MouseClick(object sender, EventArgs e)
        {
            Panel me = (Panel)sender;
            
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                me.BackColor = Color.White;
            }
            else if (me.BackColor == Color.MidnightBlue)
            {
                me.BackColor = Color.DarkViolet;
            }
            else
            {
                me.BackColor = Color.Red;
            }
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GridCtrl_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void NewGame()
        {
            try
            {
                PaintTableCells(GridCtrl, Color.White);
                sw = new Shipwright(10, 10);
                List<int> shipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
                Fleet flt = sw.CreateFleet(shipLengths);

                foreach (var ship in flt.Ships)
                {
                    foreach (Square square in ship.Squares)
                    {
                        PaintSingleTableCell(GridCtrl, Color.MidnightBlue, square.Column, square.Row);
                    }
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                NewGame();
            }

        }
    }
}
