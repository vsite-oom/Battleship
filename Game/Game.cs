using System;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Game
{
    public partial class Game : Form
    {
        Fleet playerFleet;
        Fleet opponentFleet;
        FleetBuilder fleetBuilder;
        Gunnery playerGunnery;
        Gunnery opponentGunnery;

        public Game()
        {
            InitializeComponent();
            InitializeGrids();
        }

        void InitializeGrids()
        {
            InitializeGrid(strateskiGrid);
            InitializeGrid(taktickiGrid);
        }

        void InitializeGrid(DataGridView grid)
        {
            grid.ColumnCount = 10;
            grid.RowCount = 10;

            // Postavljanje zaglavlja stupaca od A do J
            for (int i = 0; i < 10; i++)
            {
                grid.Columns[i].Name = ((char)('A' + i)).ToString();
                grid.Columns[i].Width = 30; // prilagođena veličina ćelija
            }

            // Postavljanje zaglavlja redova od 1 do 10
            for (int i = 0; i < 10; i++)
            {
                grid.Rows[i].HeaderCell.Value = (i + 1).ToString();
                grid.Rows[i].Height = 30;   // prilagođena veličina ćelija
            }
        }

        void pocetakButton_Click(object sender, EventArgs e)
        {
            fleetBuilder = new FleetBuilder(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });
            playerFleet = fleetBuilder.CreateFleet();
            opponentFleet = fleetBuilder.CreateFleet();

            playerGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });
            opponentGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

            RenderFleet(strateskiGrid, playerFleet);
            statusLabel.Text = "Status: Igra je počela!";
        }

        void RenderFleet(DataGridView grid, Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    grid.Rows[square.Row].Cells[square.Column].Style.BackColor = System.Drawing.Color.Gray;
                }
            }
        }

        void strateskiGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var result = opponentFleet.Hit(e.RowIndex, e.ColumnIndex);
            playerGunnery.ProcessHitResult(result);
            UpdateGrids();
            if (result == HitResult.Sunken && opponentFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("Pobijedili ste!");
                statusLabel.Text = "Status: Pobijedili ste!";
            }
            else
            {
                ProtivnikovPotez();
            }
        }

        void ProtivnikovPotez()
        {
            var target = opponentGunnery.Next();
            var result = playerFleet.Hit(target.Row, target.Column);
            opponentGunnery.ProcessHitResult(result);
            UpdateGrids();
            if (result == HitResult.Sunken && playerFleet.Ships.All(ship => ship.Squares.All(sq => sq.IsHit)))
            {
                MessageBox.Show("Protivnik je pobijedio!");
                statusLabel.Text = "Status: Protivnik je pobijedio!";
            }
        }

        void UpdateGrids()
        {
            // Ažuriraj prikaz strateškog i taktičkog grida na temelju trenutnog stanja igre
        }
    }
}
