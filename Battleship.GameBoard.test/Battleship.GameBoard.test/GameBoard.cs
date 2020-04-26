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

namespace Battleship.GameBoard
{
    public partial class GameBoard : Form
    {
        public GameBoard()
        {
            InitializeComponent();
        }

        private void CreateBattlefield_Click(object sender, EventArgs e)
        {
            try 
            {
                GenerateBattlefield(Int32.Parse(textBoxColumn.Text), Int32.Parse(textBoxRow.Text));

                Battlefield.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

               // FillBattlefield(Int32.Parse(textBoxColumn.Text), Int32.Parse(textBoxRow.Text));
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid column or row count!");
            }
        }

        private void createFleet_Click(object sender, EventArgs e)
        {
            Battlefield.Controls.Clear();
            CreateFleet(Int32.Parse(textBoxColumn.Text), Int32.Parse(textBoxRow.Text));
        }

        private void GenerateBattlefield(int columnCount, int rowCount)
        {
            //columnCount++;
            //rowCount++;

            Battlefield.Controls.Clear();

            Battlefield.ColumnStyles.Clear();
            Battlefield.RowStyles.Clear();

            Battlefield.ColumnCount = columnCount;
            Battlefield.RowCount = rowCount;

            for (int x = 0; x < columnCount; x++)
            {
                ColumnStyle columnStyle = new ColumnStyle(SizeType.Percent);
                columnStyle.Width = CellPercentSize(columnCount);

                Battlefield.ColumnStyles.Add(columnStyle);

                for (int y = 0; y < rowCount; y++)
                {
                    if (x == 0)
                    {
                        RowStyle rowStyle = new RowStyle(SizeType.Percent);
                        rowStyle.Height = CellPercentSize(rowCount);
                       
                        Battlefield.RowStyles.Add(rowStyle);
                    }
                }
            }
        }
        float CellPercentSize(int count)
        {
            return 100 / count;
        }

        #region Battlefieled marks
        public void MarkBattlefield(int columnCount, int rowCount)
        {
            columnCount++;
            rowCount++;
            char letter = 'A';

            for (int x = 1; x < columnCount; x++)
            {
                Label columnLabel = new Label();
                columnLabel.Text = letter++.ToString();
                LabelsStyle(columnLabel);
                Battlefield.Controls.Add(columnLabel, x, 0);
            }

            for (int y = 1; y < rowCount; y++)
            {
                Label rowLabel = new Label();
                rowLabel.Text = y.ToString();
                LabelsStyle(rowLabel);
                Battlefield.Controls.Add(rowLabel, 0, y);
            }

        }
        public void LabelsStyle(Label label)
        {
            label.Anchor = AnchorStyles.Top;
            label.Anchor = AnchorStyles.Left;
            label.Anchor = AnchorStyles.Bottom;
            label.Anchor = AnchorStyles.Right;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
        #endregion

        private void CreateFleet(int columnCount, int rowCount)
        {
            List<int> ships = new List<int> { 2, 2, 2, 2, 3, 3, 3, 4, 4, 5 };
            Shipwright shipWrighter = new Shipwright(columnCount, rowCount);
 
            Fleet fleet = shipWrighter.CreateFleet(ships);

                foreach (var ship in fleet.Ships)
                {
                    foreach (Square square in ship.Squares)
                    {
                               Panel sq = new Panel();
                               sq.BackColor = Color.Navy;
                               Battlefield.Controls.Add(sq, square.Column, square.Row);                    
                    }
                }   
        }
    }
}
