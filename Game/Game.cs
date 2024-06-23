using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<Button> buttonsHostHit = new List<Button>();
        private List<int> shipsToShoot;

        public MainForm()
        {
            InitializeComponent();
            InitializeGrids();
        }

        void InitializeGrids()
        {
            InitializeGrid(panel_Host);
            InitializeGrid(panel_Enemy);
        }

        void InitializeGrid(Panel grid)
        {
            grid.Controls.Clear();
            int buttonSize = grid.Width / 10;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(buttonSize, buttonSize),
                        Location = new Point(col * buttonSize, row * buttonSize),
                        Tag = new ButtonInfo(row, col)
                    };
                    btn.Click += GridButton_Click;
                    grid.Controls.Add(btn);
                }
            }
        }

        void button_StartStop_Click(object sender, EventArgs e)
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

                RenderFleet(panel_Host, playerFleet);
                statusLabel.Text = "Status: Igra je počela!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
            }
        }

        void GridButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            var info = btn.Tag as ButtonInfo;
            if (info == null) return;

            int row = info.Row;
            int col = info.Col;

            var result = opponentFleet.Hit(row, col);
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

        void RenderFleet(Panel grid, Fleet fleet)
        {
            foreach (var ship in fleet.Ships)
            {
                foreach (var square in ship.Squares)
                {
                    var btn = grid.Controls.OfType<Button>().FirstOrDefault(b =>
                    {
                        var info = b.Tag as ButtonInfo;
                        return info.Row == square.Row && info.Col == square.Column;
                    });

                    if (btn != null)
                    {
                        btn.BackColor = Color.Gray;
                    }
                }
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

        void panel_Host_SizeChanged(object sender, EventArgs e)
        {
            InitializeGrid(panel_Host);
        }

        void panel_Enemy_SizeChanged(object sender, EventArgs e)
        {
            InitializeGrid(panel_Enemy);
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            InitializeGrids();
        }

        private class ButtonInfo
        {
            public int Row { get; }
            public int Col { get; }

            public ButtonInfo(int row, int col)
            {
                Row = row;
                Col = col;
            }
        }
    }
}
