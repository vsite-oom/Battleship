using System;
using System.Linq;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Game
{
    public partial class MainForm : Form
    {
        Fleet playerFleet;
        Fleet opponentFleet;
        FleetBuilder fleetBuilder;
        Gunnery playerGunnery;
        Gunnery opponentGunnery;

        public MainForm()
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
            try
            {
                fleetBuilder = new FleetBuilder(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

                // Provjerite je li moguće postaviti sve brodove prije nego što stvorite flote
                var fleetGrid = fleetBuilder.GetType()
                    .GetField("fleetGrid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.GetValue(fleetBuilder) as FleetGrid;

                if (fleetGrid == null)
                {
                    MessageBox.Show("Nije moguće inicijalizirati mrežu flote.");
                    return;
                }

                foreach (var length in new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 })
                {
                    var candidates = fleetGrid.GetAvailablePlacements(length);

                    if (!candidates.Any())
                    {
                        MessageBox.Show($"Nema dovoljno mjesta za postavljanje broda duljine {length}. Pokušajte ponovno.");
                        return;
                    }
                }

                playerFleet = fleetBuilder.CreateFleet();
                opponentFleet = fleetBuilder.CreateFleet();

                playerGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });
                opponentGunnery = new Gunnery(10, 10, new[] { 5, 4, 4, 3, 3, 2, 2, 2, 2 });

                RenderFleet(strateskiGrid, playerFleet);
                statusLabel.Text = "Status: Igra je počela!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
            }
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
