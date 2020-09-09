using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.oom.Battleship.Model;

namespace GUI
{
    public partial class Battlefield : Form
    {
        public Battlefield()
        {
            InitializeComponent();

            CreateBattlefields();
        }

        private void cell_Click(object sender, MouseEventArgs e)
        {
            if (player.shipsSunken != player.fleet.Ships.Count() && com.shipsSunken != com.fleet.Ships.Count())
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        PersonPlays(sender, e);
                        if (com.shipsSunken != com.fleet.Ships.Count())
                            ComPlays();
                        break;
                    case MouseButtons.Right:
                        PersonPlays(sender, e);
                        break;
                }
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NovaIgra();
        }

        private void gameOutcome_Click(object sender, EventArgs e)
        {
            NovaIgra();
        }

        private void CreateBattlefields()
        {
            for (int i = 0; i <= rules.Rows; ++i)
            {
                for (int j = 0; j <= rules.Columns; ++j)
                {
                    if (i == 0 && j == 0)
                        continue;
                    else if (i == 0 ^ j == 0)
                    {
                        Label lb = BattleshipLabel(i, j, 20, 40);
                        this.Controls.Add(lb);

                        lb = BattleshipLabel(i, j, 670, 40);
                        this.Controls.Add(lb);
                    }
                    else
                    {
                        CheckBox cb = BattleshipCheckBox(0, i, j, 20, 40);
                        player.Grid.Add(cb);
                        this.Controls.Add(cb);

                        cb = BattleshipCheckBox(1, i, j, 670, 40);
                        com.Grid.Add(cb);
                        this.Controls.Add(cb);
                    }
                }
            }
        }

        private Label BattleshipLabel(int i, int j, int offSetX, int offSetY)
        {
            Label lb = new Label();
            lb.Location = new System.Drawing.Point(offSetX + j * 50, offSetY + i * 50);
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.Font = new System.Drawing.Font(lb.Font.Name, 15);
            lb.Size = new System.Drawing.Size(50, 50);
            if (i == 0)
                lb.Text = Char.ToString((char)(j + 64));
            else if (j == 0)
                lb.Text = i.ToString();

            return lb;
        }

        private CheckBox BattleshipCheckBox(int type, int i, int j, int offSetX, int offSetY)
        {
            CheckBox cb = new CheckBox();

            cb.Appearance = Appearance.Button;
            cb.Location = new System.Drawing.Point(offSetX + j * 50, offSetY + i * 50);
            cb.Size = new System.Drawing.Size(50, 50);
            cb.Enabled = false;

            if (type == 1)
            {
                cb.AutoCheck = false;
                cb.Name = (j - 1).ToString() + "," + (i - 1).ToString();
                cb.MouseDown += new MouseEventHandler(this.cell_Click);
                cb.TabStop = false;
            }

            return cb;
        }

        private void NovaIgra()
        {
            Shipwright sw = new Shipwright(rules.Rows, rules.Columns);
            player.fleet = sw.CreateFleet(rules.ShipLengths);
            com.fleet = sw.CreateFleet(rules.ShipLengths);
            gunner = new Gunner(rules.Rows, rules.Columns, rules.ShipLengths);
            int position;

            for (int i = 0; i < rules.Rows; ++i)
            {
                for (int j = 0; j < rules.Columns; ++j)
                {
                    position = i * 10 + j;

                    com.Grid[position].BackColor = System.Drawing.Color.White;
                    com.Grid[position].Enabled = true;

                    foreach (Ship ship in player.fleet.Ships)
                    {
                        if (ship.Squares.Contains(new Square(i, j)))
                        {
                            player.Grid[position].BackColor = System.Drawing.Color.Blue;
                            break;
                        }
                        else
                            player.Grid[position].BackColor = System.Drawing.Color.White;
                    }
                }
            }

            gameOutcome.Text = "";
            gameOutcome.Visible = false;
            player.shipsSunken = 0;
            com.shipsSunken = 0;
        }

        private void PersonPlays(object sender, MouseEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (e.Button == MouseButtons.Left)
            {
                Int32.TryParse(cb.Name.Split(new Char[] { ',' })[0], out int column);
                Int32.TryParse(cb.Name.Substring(cb.Name.IndexOf(',') + 1), out int row);

                HitResult result = com.fleet.Hit(new Square(row, column));
                cb.Enabled = false;

                if (result == HitResult.Missed)
                    cb.BackColor = System.Drawing.Color.DarkGray;
                else if (result == HitResult.Hit)
                    cb.BackColor = System.Drawing.Color.Red;
                else if (result == HitResult.Sunken)
                {
                    foreach (Ship ship in com.fleet.Ships)
                    {
                        if (ship.Squares.Contains(new Square(row, column)))
                        {
                            foreach (Square square in ship.Squares)
                            {
                                com.Grid[square.Rows * 10 + square.Columns].BackColor = System.Drawing.Color.DarkRed;
                            }
                            break;
                        }
                    }

                    ++com.shipsSunken;
                    if (com.shipsSunken == com.fleet.Ships.Count())
                    {
                        gameOutcome.Text = "POBJEDA :)";
                        gameOutcome.Visible = true;
                    }
                }
            }
            else
            {
                if (cb.BackColor == System.Drawing.Color.White)
                    cb.BackColor = System.Drawing.Color.DarkGray;
                else
                    cb.BackColor = System.Drawing.Color.White;
            }
        }

        private void ComPlays()
        {
            Square sq = gunner.NextTarget();
            HitResult result = player.fleet.Hit(sq);

            gunner.ProcessHitResult(result);

            if (result == HitResult.Missed)
                player.Grid[sq.Rows * 10 + sq.Columns].BackColor = System.Drawing.Color.DarkGray;
            else if (result == HitResult.Hit)
                player.Grid[sq.Rows * 10 + sq.Columns].BackColor = System.Drawing.Color.Red;
            else if (result == HitResult.Sunken)
            {
                foreach (Ship ship in player.fleet.Ships)
                {
                    if (ship.Squares.Contains(sq))
                    {
                        foreach (Square square in ship.Squares)
                        {
                            player.Grid[square.Rows * 10 + square.Columns].BackColor = System.Drawing.Color.Purple;
                        }
                        break;
                    }
                }

                ++player.shipsSunken;
                if (player.shipsSunken == player.fleet.Ships.Count())
                {
                    gameOutcome.Text = "PORAZ :(";
                    gameOutcome.Visible = true;
                    Reveal();
                }
            }
        }

        private void Reveal()
        {
            foreach (Ship ship in com.fleet.Ships)
            {
                foreach (Square square in ship.Squares)
                {
                    if (com.Grid[square.Rows * 10 + square.Columns].Enabled == true)
                        com.Grid[square.Rows * 10 + square.Columns].BackColor = System.Drawing.Color.Green;
                }
            }
        }

        RulesSingleton rules = RulesSingleton.Instance;
        Player player = new Player();
        Player com = new Player();
        Gunner gunner;

        private void Battlefield_Load(object sender, EventArgs e)
        {

        }
    }
}
