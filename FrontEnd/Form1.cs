using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd
{
    public partial class Form1 : Form
    {
        private const int gridSize = 10; // Making the grid size a constant for easy changes
        private Panel panel;

        public Form1()
        {
            InitializeComponent();
            panel = new Panel();
            panel.Dock = DockStyle.Fill;
            this.Controls.Add(panel);
            this.Resize += new EventHandler(Form1_Resize); // Hook up the Resize event
            InitializeGrid(AnchorStyles.Top | AnchorStyles.Left);
            InitializeGrid(AnchorStyles.Top | AnchorStyles.Right);
            Form1_Resize(this, null); // Manually call the Resize event
        }

        private void InitializeGrid(AnchorStyles anchor)
        {
            Button[,] buttons = new Button[gridSize, gridSize];

            // Initialize TableLayoutPanel
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Anchor = anchor;
            tableLayoutPanel.RowCount = gridSize;
            tableLayoutPanel.ColumnCount = gridSize;

            // Set all rows and columns to be equally sized
            for (int i = 0; i < gridSize; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / gridSize));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / gridSize));
            }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    buttons[i, j] = new Button();

                    // Set button properties
                    buttons[i, j].Dock = DockStyle.Fill;
                    buttons[i, j].Click += Button_Click;

                    // Add button to TableLayoutPanel
                    tableLayoutPanel.Controls.Add(buttons[i, j], j, i);
                }
            }

            // Add TableLayoutPanel to Panel
            panel.Controls.Add(tableLayoutPanel);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackColor = Color.Red;
            button.Enabled = false;
        }

        // Resize event handler
        private void Form1_Resize(object sender, EventArgs e)
        {
            int panelWidth = (int)(panel.Width * 0.45);
            int panelHeight = (int)(panel.Height * 0.8);

            // Ensure the size of the TableLayoutPanel is a multiple of the gridSize
            int width = (panelWidth / gridSize) * gridSize;
            int height = (panelHeight / gridSize) * gridSize;

            foreach (Control control in panel.Controls)
            {
                if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tableLayoutPanel = (TableLayoutPanel)control;
                    tableLayoutPanel.Size = new Size(width, height);
                    if (tableLayoutPanel.Anchor.HasFlag(AnchorStyles.Right))
                    {
                        tableLayoutPanel.Location = new Point(panel.Width - width, 0);
                    }
                    else
                    {
                        tableLayoutPanel.Location = new Point(0, 0);
                    }
                }
            }
        }
    }

}
