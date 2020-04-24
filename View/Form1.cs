using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public partial class FormMain : Form
    {

        public delegate void PaintRed(Button btn);

        public FormMain()
        {
            InitializeComponent();



            GridCtrl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;


            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.BackColor = Color.White;
                    btn.Enter += Btn_Enter;
                    btn.Leave += Btn_Leave;

                    GridCtrl.Controls.Add( btn  , i, j);

                }
            }


            TableLayoutColumnStyleCollection ColumnStyles = GridCtrl.ColumnStyles;
            foreach (ColumnStyle style in ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 10;
            }

            TableLayoutRowStyleCollection RowStyles = GridCtrl.RowStyles;
            foreach (RowStyle style in RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 10;
            }

        }

        private void Btn_Leave(object sender, EventArgs e)
        {
            Button me = (Button)sender;
            me.BackColor = Color.White;
        }

        private void Btn_Enter(object sender, EventArgs e)
        {
            Button me = (Button)sender;
            me.BackColor = Color.Red;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
