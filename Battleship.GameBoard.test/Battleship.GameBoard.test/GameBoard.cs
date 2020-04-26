using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                GenerateTable(Int32.Parse(textBoxColumn.Text), Int32.Parse(textBoxRow.Text));

                Battlefield.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;

                FillBattlefield(Int32.Parse(textBoxColumn.Text), Int32.Parse(textBoxRow.Text));
            }
            catch (SystemException)
            {
                MessageBox.Show("Invalid column or row count!");
            }
        }

        private void GenerateTable(int columnCount, int rowCount)
        {
            columnCount++;
            rowCount++;

            //generate new table layout
            Battlefield.Controls.Clear();

            //clear out the existing row and column styles
            Battlefield.ColumnStyles.Clear();
            Battlefield.RowStyles.Clear();

            //set up the row and column counts first
            Battlefield.ColumnCount = columnCount;
            Battlefield.RowCount = rowCount;

            for (int x = 0; x < columnCount; x++)
            {
                ColumnStyle columnStyle = new ColumnStyle(SizeType.Percent);
                columnStyle.Width = CellPercentSize(columnCount);

                Battlefield.ColumnStyles.Add(columnStyle);

                for (int y = 0; y < rowCount; y++)
                {
                    //add a row
                    if (x == 0)
                    {
                        RowStyle rowStyle = new RowStyle(SizeType.Percent);
                        rowStyle.Height = CellPercentSize(rowCount);
                       
                        Battlefield.RowStyles.Add(rowStyle);
                    }
                }
            }
        }

        public void FillBattlefield(int columnCount, int rowCount)
        {
            columnCount++;
            rowCount++;
            char letter = 'A';

            for (int x = 1; x < columnCount; x++)
            {
                Label columnLabel = new Label();
                columnLabel.Text = letter++.ToString();
                ColumnsAndRowsStyle(columnLabel);
                Battlefield.Controls.Add(columnLabel, x, 0);
            }

            for (int y = 1; y < rowCount; y++)
            {
                Label rowLabel = new Label();
                rowLabel.Text = y.ToString();
                ColumnsAndRowsStyle(rowLabel);
                Battlefield.Controls.Add(rowLabel, 0, y);
            }

            for (int x = 1; x < columnCount; x++)
            {
                for (int y = 1; y < rowCount; y++)
                {
                    Panel square = new Panel();
                    //square.BackColor = Color.Red;
                    Battlefield.Controls.Add(square, x, y);
                }
            }

        }
        public void ColumnsAndRowsStyle(Label label)
        {
            label.Anchor = AnchorStyles.Top;
            label.Anchor = AnchorStyles.Left;
            label.Anchor = AnchorStyles.Bottom;
            label.Anchor = AnchorStyles.Right;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        float CellPercentSize(int count)
        {
            return 100 / count;
        }
    }
}
