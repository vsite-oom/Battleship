using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Model;

namespace Battleship.GUI
{
    public partial class Form1 : Form
    {
        private const int Size = 10;
        private const int Cell = 32;

        // Fleet composition per game rules: 1x5, 2x4, 3x3, 4x2 = 10 ships (see README / FleetBuilderTests)
        private static readonly (string Name, int Length)[] ShipDefinitions =
        {
            ("Nosac zrakoplova", 5),
            ("Bojni brod", 4),
            ("Bojni brod", 4),
            ("Razarac", 3),
            ("Razarac", 3),
            ("Razarac", 3),
            ("Podmornica", 2),
            ("Podmornica", 2),
            ("Podmornica", 2),
            ("Podmornica", 2),
        };
        private static int[] ShipLengths => ShipDefinitions.Select(s => s.Length).ToArray();

        private enum GameMode { Singleplayer, Multiplayer }
        private GameMode mode;

        // ----- panels -----
        private Panel pnlSetup;
        private Panel pnlPlacement;
        private Panel pnlPassDevice;
        private Panel pnlBattle;
        private Panel pnlGameOver;

        // ----- setup controls -----
        private RadioButton rbSingle;
        private RadioButton rbMulti;

        // ----- placement state/controls -----
        private Button[,] placementButtons;
        private ListBox lstShipsToPlace;
        private Button btnRotate;
        private Button btnConfirmPlacement;
        private Label lblPlacementTitle;
        private bool isHorizontal = true;

        private FleetGrid placementGrid;
        private List<(string Name, int Length)> shipsQueue;
        private Fleet fleetBeingBuilt;
        private Action onPlacementComplete;

        // ----- pass-device controls -----
        private Label lblPassMessage;
        private Button btnPassContinue;
        private Action onPassContinue;

        // ----- battle controls -----
        private Button[,] ownBoardButtons;
        private Button[,] trackBoardButtons;
        private Label lblOwnTitle;
        private Label lblEnemyTitle;
        private Label lblStatus;
        private Label lblGameOverMessage;
        private readonly System.Windows.Forms.Timer aiTimer;

        // ----- singleplayer state -----
        private Fleet playerFleet;
        private Fleet computerFleet;
        private Gunnery computerGunnery;
        private ShotsGrid playerShotsOnComputer;
        private bool isPlayerTurn;

        // ----- multiplayer state -----
        private Fleet p1Fleet;
        private Fleet p2Fleet;
        private ShotsGrid p1ShotsOnP2;
        private ShotsGrid p2ShotsOnP1;
        private int currentPlayer;

        private bool gameOver;

        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += Form1_KeyDown;

            aiTimer = new System.Windows.Forms.Timer { Interval = 650 };
            aiTimer.Tick += AiTimer_Tick;

            BuildSetupPanel();
            BuildPlacementPanel();
            BuildPassDevicePanel();
            BuildBattlePanel();
            BuildGameOverPanel();

            ShowPanel(pnlSetup);
        }

        private void ShowPanel(Panel panel)
        {
            pnlSetup.Visible = false;
            pnlPlacement.Visible = false;
            pnlPassDevice.Visible = false;
            pnlBattle.Visible = false;
            pnlGameOver.Visible = false;
            panel.Visible = true;
            panel.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R && pnlPlacement.Visible) ToggleRotation();
        }

        private void AddBoardLabels(Panel parent, int boardLeft, int boardTop)
        {
            for (int c = 0; c < Size; c++)
            {
                parent.Controls.Add(new Label
                {
                    Text = ((char)('A' + c)).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(Cell, 18),
                    Location = new Point(boardLeft + c * Cell, boardTop - 18),
                    Font = new Font("Arial", 8, FontStyle.Bold)
                });
            }
            for (int r = 0; r < Size; r++)
            {
                parent.Controls.Add(new Label
                {
                    Text = (r + 1).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(18, Cell),
                    Location = new Point(boardLeft - 20, boardTop + r * Cell),
                    Font = new Font("Arial", 8, FontStyle.Bold)
                });
            }
        }

        // ==================== SETUP ====================

        private void BuildSetupPanel()
        {
            pnlSetup = new Panel { Dock = DockStyle.Fill };
            Controls.Add(pnlSetup);

            var lblTitle = new Label { Text = "BATTLESHIP", Font = new Font("Arial", 28, FontStyle.Bold), AutoSize = true, Location = new Point(40, 30) };
            var lblSubtitle = new Label
            {
                Text = "Flota: 1x Nosac zrakoplova (5), 2x Bojni brod (4), 3x Razarac (3), 4x Podmornica (2) - ploca 10x10",
                AutoSize = true,
                Location = new Point(40, 90),
                ForeColor = Color.DimGray
            };

            var lblMode = new Label { Text = "Odaberi nacin igre:", Font = new Font("Arial", 11, FontStyle.Bold), AutoSize = true, Location = new Point(40, 140) };
            rbSingle = new RadioButton { Text = "Igra protiv racunala", Checked = true, AutoSize = true, Location = new Point(60, 170) };
            rbMulti = new RadioButton { Text = "Dva igraca (lokalno, naizmjenicno)", AutoSize = true, Location = new Point(60, 200) };

            var btnStart = new Button { Text = "Zapocni igru", Location = new Point(40, 250), Width = 200, Height = 42, BackColor = Color.LightGreen };
            btnStart.Click += (s, e) => StartSetup();

            pnlSetup.Controls.AddRange(new Control[] { lblTitle, lblSubtitle, lblMode, rbSingle, rbMulti, btnStart });
        }

        private void StartSetup()
        {
            mode = rbSingle.Checked ? GameMode.Singleplayer : GameMode.Multiplayer;
            gameOver = false;
            aiTimer.Stop();

            if (mode == GameMode.Singleplayer)
            {
                computerFleet = new FleetBuilder(Size, Size, ShipLengths).CreateFleet();
                computerGunnery = new Gunnery(Size, Size, ShipLengths);
                playerShotsOnComputer = new ShotsGrid(Size, Size);
                isPlayerTurn = true;

                BeginPlacement("Postavi svoju flotu", () =>
                {
                    playerFleet = fleetBeingBuilt;
                    RenderBattleForCurrentPlayer();
                    ShowPanel(pnlBattle);
                });
            }
            else
            {
                currentPlayer = 1;
                BeginPlacement("Igrac 1 - postavi svoju flotu", () =>
                {
                    p1Fleet = fleetBeingBuilt;
                    ShowPassDevice("Igrac 1 je spreman. Predaj uredjaj Igracu 2.", () =>
                    {
                        BeginPlacement("Igrac 2 - postavi svoju flotu", () =>
                        {
                            p2Fleet = fleetBeingBuilt;
                            p1ShotsOnP2 = new ShotsGrid(Size, Size);
                            p2ShotsOnP1 = new ShotsGrid(Size, Size);
                            currentPlayer = 1;
                            ShowPassDevice("Obje flote su postavljene. Predaj uredjaj Igracu 1 - igra pocinje!", () =>
                            {
                                RenderBattleForCurrentPlayer();
                                ShowPanel(pnlBattle);
                            });
                        });
                    });
                });
            }
        }

        // ==================== PLACEMENT ====================

        private void BuildPlacementPanel()
        {
            pnlPlacement = new Panel { Dock = DockStyle.Fill };
            Controls.Add(pnlPlacement);

            lblPlacementTitle = new Label { Font = new Font("Arial", 14, FontStyle.Bold), AutoSize = true, Location = new Point(40, 15) };

            const int boardLeft = 40;
            const int boardTop = 70;

            placementButtons = new Button[Size, Size];
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var btn = new Button
                    {
                        Size = new Size(Cell, Cell),
                        Location = new Point(boardLeft + c * Cell, boardTop + r * Cell),
                        BackColor = Color.AliceBlue,
                        FlatStyle = FlatStyle.Flat,
                        Tag = new SquareCoordinate(r, c),
                    };
                    btn.MouseEnter += PlacementButton_MouseEnter;
                    btn.MouseLeave += PlacementButton_MouseLeave;
                    btn.Click += PlacementButton_Click;
                    placementButtons[r, c] = btn;
                    pnlPlacement.Controls.Add(btn);
                }
            }
            AddBoardLabels(pnlPlacement, boardLeft, boardTop);

            int sideLeft = boardLeft + Size * Cell + 40;

            var lblShipsHeader = new Label { Text = "Preostali brodovi:", Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true, Location = new Point(sideLeft, boardTop) };
            lstShipsToPlace = new ListBox { Location = new Point(sideLeft, boardTop + 25), Width = 220, Height = 160 };

            btnRotate = new Button { Text = "Rotiraj: Horizontalno (R)", Location = new Point(sideLeft, boardTop + 195), Width = 220, Height = 32, BackColor = Color.LightYellow };
            btnRotate.Click += (s, e) => ToggleRotation();

            var btnRandomPlace = new Button { Text = "Nasumicno postavi flotu", Location = new Point(sideLeft, boardTop + 235), Width = 220, Height = 32 };
            btnRandomPlace.Click += (s, e) => RandomPlaceRemaining();

            var btnResetPlacement = new Button { Text = "Resetiraj raspored", Location = new Point(sideLeft, boardTop + 275), Width = 220, Height = 32 };
            btnResetPlacement.Click += (s, e) => ResetPlacement();

            btnConfirmPlacement = new Button { Text = "Nastavi ->", Location = new Point(sideLeft, boardTop + 325), Width = 220, Height = 42, BackColor = Color.LightGreen, Enabled = false };
            btnConfirmPlacement.Click += (s, e) => onPlacementComplete?.Invoke();

            var lblHelp = new Label
            {
                Text = "Klikni polje za postavljanje odabranog broda.\nPritisni 'R' ili gumb za rotaciju.\nBrodovi se ne smiju dodirivati.",
                Location = new Point(sideLeft, boardTop + 375),
                Size = new Size(220, 80),
                ForeColor = Color.DimGray
            };

            pnlPlacement.Controls.AddRange(new Control[] { lblPlacementTitle, lblShipsHeader, lstShipsToPlace, btnRotate, btnRandomPlace, btnResetPlacement, btnConfirmPlacement, lblHelp });
        }

        private void BeginPlacement(string title, Action onComplete)
        {
            lblPlacementTitle.Text = title;
            onPlacementComplete = onComplete;
            ResetPlacement();
            ShowPanel(pnlPlacement);
        }

        private void ResetPlacement()
        {
            placementGrid = new FleetGrid(Size, Size);
            shipsQueue = new List<(string, int)>(ShipDefinitions);
            fleetBeingBuilt = new Fleet();
            isHorizontal = true;
            btnRotate.Text = "Rotiraj: Horizontalno (R)";
            btnConfirmPlacement.Enabled = false;
            RefreshShipsList();
            RenderPlacementBoard();
        }

        private void RefreshShipsList()
        {
            lstShipsToPlace.Items.Clear();
            foreach (var ship in shipsQueue)
            {
                lstShipsToPlace.Items.Add($"{ship.Name} ({ship.Length})");
            }
            if (lstShipsToPlace.Items.Count > 0) lstShipsToPlace.SelectedIndex = 0;
        }

        private void RenderPlacementBoard()
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    bool occupied = fleetBeingBuilt.Ships.Any(s => s.Contains(r, c));
                    placementButtons[r, c].BackColor = occupied ? Color.DimGray : Color.AliceBlue;
                    placementButtons[r, c].Text = occupied ? "#" : "";
                    placementButtons[r, c].ForeColor = Color.White;
                    placementButtons[r, c].Enabled = shipsQueue.Count > 0;
                }
            }
        }

        private void ToggleRotation()
        {
            isHorizontal = !isHorizontal;
            btnRotate.Text = isHorizontal ? "Rotiraj: Horizontalno (R)" : "Rotiraj: Vertikalno (R)";
        }

        private bool IsPlacementValid(int row, int col, int length)
        {
            for (int i = 0; i < length; i++)
            {
                int r = isHorizontal ? row : row + i;
                int c = isHorizontal ? col + i : col;
                if (r >= Size || c >= Size) return false;
                if (!placementGrid.Squares.Any(s => s.Row == r && s.Column == c)) return false;
            }
            return true;
        }

        private void PlacementButton_MouseEnter(object sender, EventArgs e)
        {
            if (shipsQueue.Count == 0) return;
            var coord = (SquareCoordinate)((Button)sender).Tag;
            int length = shipsQueue[0].Length;
            bool valid = IsPlacementValid(coord.Row, coord.Column, length);
            HighlightPreview(coord.Row, coord.Column, length, valid ? Color.LightGreen : Color.LightSalmon);
        }

        private void PlacementButton_MouseLeave(object sender, EventArgs e)
        {
            RenderPlacementBoard();
        }

        private void HighlightPreview(int row, int col, int length, Color color)
        {
            for (int i = 0; i < length; i++)
            {
                int r = isHorizontal ? row : row + i;
                int c = isHorizontal ? col + i : col;
                if (r < Size && c < Size && !fleetBeingBuilt.Ships.Any(s => s.Contains(r, c)))
                {
                    placementButtons[r, c].BackColor = color;
                }
            }
        }

        private void PlacementButton_Click(object sender, EventArgs e)
        {
            if (shipsQueue.Count == 0) return;
            var coord = (SquareCoordinate)((Button)sender).Tag;
            var ship = shipsQueue[0];

            if (!IsPlacementValid(coord.Row, coord.Column, ship.Length))
            {
                MessageBox.Show("Brod se ne moze ovdje postaviti (izvan ploce ili prislonjen uz drugi brod).", "Nevaljan potez", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var squares = new List<Square>();
            for (int i = 0; i < ship.Length; i++)
            {
                int r = isHorizontal ? coord.Row : coord.Row + i;
                int c = isHorizontal ? coord.Column + i : coord.Column;
                squares.Add(new Square(r, c));
            }

            fleetBeingBuilt.CreateShip(squares);

            var eliminator = new SquareEliminator();
            foreach (var toRemove in eliminator.ToEliminate(squares, Size, Size))
            {
                placementGrid.EliminateSquare(toRemove.Row, toRemove.Column);
            }

            shipsQueue.RemoveAt(0);
            RefreshShipsList();
            RenderPlacementBoard();

            if (shipsQueue.Count == 0)
            {
                btnConfirmPlacement.Enabled = true;
            }
        }

        private void RandomPlaceRemaining()
        {
            fleetBeingBuilt = new FleetBuilder(Size, Size, ShipLengths).CreateFleet();
            shipsQueue.Clear();
            RefreshShipsList();
            RenderPlacementBoard();
            btnConfirmPlacement.Enabled = true;
        }

        // ==================== PASS DEVICE ====================

        private void BuildPassDevicePanel()
        {
            pnlPassDevice = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            Controls.Add(pnlPassDevice);

            lblPassMessage = new Label
            {
                Font = new Font("Arial", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(50, 180),
                Size = new Size(800, 120),
            };

            btnPassContinue = new Button
            {
                Text = "Nastavi",
                Location = new Point(360, 320),
                Width = 180,
                Height = 44,
                BackColor = Color.LightGreen
            };
            btnPassContinue.Click += (s, e) => onPassContinue?.Invoke();

            pnlPassDevice.Controls.Add(lblPassMessage);
            pnlPassDevice.Controls.Add(btnPassContinue);
        }

        private void ShowPassDevice(string message, Action continueAction)
        {
            lblPassMessage.Text = message;
            onPassContinue = continueAction;
            ShowPanel(pnlPassDevice);
        }

        // ==================== BATTLE ====================

        private void BuildBattlePanel()
        {
            pnlBattle = new Panel { Dock = DockStyle.Fill };
            Controls.Add(pnlBattle);

            lblStatus = new Label { Font = new Font("Arial", 13, FontStyle.Bold), AutoSize = true, Location = new Point(40, 10) };

            const int ownLeft = 40;
            const int trackLeft = 410;
            const int boardTop = 90;

            lblOwnTitle = new Label { Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true, Location = new Point(ownLeft, 45) };
            lblEnemyTitle = new Label { Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true, Location = new Point(trackLeft, 45) };

            ownBoardButtons = new Button[Size, Size];
            trackBoardButtons = new Button[Size, Size];

            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var ownBtn = new Button
                    {
                        Size = new Size(Cell, Cell),
                        Location = new Point(ownLeft + c * Cell, boardTop + r * Cell),
                        Enabled = false,
                        FlatStyle = FlatStyle.Flat,
                    };
                    ownBoardButtons[r, c] = ownBtn;
                    pnlBattle.Controls.Add(ownBtn);

                    var trackBtn = new Button
                    {
                        Size = new Size(Cell, Cell),
                        Location = new Point(trackLeft + c * Cell, boardTop + r * Cell),
                        Tag = new SquareCoordinate(r, c),
                        FlatStyle = FlatStyle.Flat,
                    };
                    trackBtn.Click += TrackBoardButton_Click;
                    trackBoardButtons[r, c] = trackBtn;
                    pnlBattle.Controls.Add(trackBtn);
                }
            }

            AddBoardLabels(pnlBattle, ownLeft, boardTop);
            AddBoardLabels(pnlBattle, trackLeft, boardTop);

            pnlBattle.Controls.Add(lblStatus);
            pnlBattle.Controls.Add(lblOwnTitle);
            pnlBattle.Controls.Add(lblEnemyTitle);
        }

        private void TrackBoardButton_Click(object sender, EventArgs e)
        {
            if (gameOver) return;
            var coord = (SquareCoordinate)((Button)sender).Tag;

            if (mode == GameMode.Singleplayer)
            {
                if (!isPlayerTurn) return;
                HandlePlayerShot(coord.Row, coord.Column);
            }
            else
            {
                HandleMultiplayerShot(coord.Row, coord.Column);
            }
        }

        private HitResult ApplyShot(Fleet targetFleet, ShotsGrid trackingGrid, int row, int col)
        {
            var result = targetFleet.Hit(row, col);
            switch (result)
            {
                case HitResult.Missed:
                    trackingGrid.GetSquare(row, col).ChangeState(SquareState.Missed);
                    break;
                case HitResult.Hit:
                    trackingGrid.GetSquare(row, col).ChangeState(SquareState.Hit);
                    break;
                case HitResult.Sunken:
                    var ship = targetFleet.Ships.First(s => s.Contains(row, col));
                    foreach (var sq in ship.Squares)
                    {
                        trackingGrid.GetSquare(sq.Row, sq.Column).ChangeState(SquareState.Sunken);
                    }
                    var eliminator = new SquareEliminator();
                    foreach (var elCoord in eliminator.ToEliminate(ship.Squares, Size, Size))
                    {
                        trackingGrid.GetSquare(elCoord.Row, elCoord.Column).ChangeState(SquareState.Eliminated);
                    }
                    break;
            }
            return result;
        }

        private static bool IsFleetDestroyed(Fleet fleet)
        {
            return fleet.Ships.All(s => s.Squares.All(sq => sq.IsHit));
        }

        private void HandlePlayerShot(int row, int col)
        {
            if (playerShotsOnComputer.GetSquare(row, col).SquareState != SquareState.Intact) return;

            var result = ApplyShot(computerFleet, playerShotsOnComputer, row, col);
            RenderTrackBoard(trackBoardButtons, playerShotsOnComputer, isPlayerTurn);

            if (IsFleetDestroyed(computerFleet))
            {
                EndGame("Cestitamo, pobijedio si! Potopio si cijelu neprijateljsku flotu.");
                return;
            }

            if (result == HitResult.Missed)
            {
                isPlayerTurn = false;
                lblStatus.Text = "Promasaj! Racunalo je na potezu...";
                RenderTrackBoard(trackBoardButtons, playerShotsOnComputer, false);
                aiTimer.Start();
            }
            else
            {
                lblStatus.Text = result == HitResult.Sunken ? "Potopio si brod! Pucaj ponovno." : "Pogodak! Pucaj ponovno.";
            }
        }

        private void AiTimer_Tick(object sender, EventArgs e)
        {
            aiTimer.Stop();

            var target = computerGunnery.Next();
            var result = playerFleet.Hit(target.Row, target.Column);
            computerGunnery.ProcessHitResult(result);

            RenderOwnBoard(ownBoardButtons, playerFleet, computerGunnery.RecordGrid);

            if (IsFleetDestroyed(playerFleet))
            {
                EndGame("Racunalo je pobijedilo. Tvoja flota je potopljena.");
                return;
            }

            if (result == HitResult.Missed)
            {
                isPlayerTurn = true;
                lblStatus.Text = "Tvoj potez!";
                RenderTrackBoard(trackBoardButtons, playerShotsOnComputer, true);
            }
            else
            {
                lblStatus.Text = result == HitResult.Sunken ? "Racunalo je potopilo tvoj brod!" : "Racunalo te pogodilo!";
                aiTimer.Start();
            }
        }

        private void HandleMultiplayerShot(int row, int col)
        {
            Fleet targetFleet = currentPlayer == 1 ? p2Fleet : p1Fleet;
            ShotsGrid myShots = currentPlayer == 1 ? p1ShotsOnP2 : p2ShotsOnP1;

            if (myShots.GetSquare(row, col).SquareState != SquareState.Intact) return;

            var result = ApplyShot(targetFleet, myShots, row, col);
            RenderTrackBoard(trackBoardButtons, myShots, true);

            if (IsFleetDestroyed(targetFleet))
            {
                EndGame($"Igrac {currentPlayer} je pobijedio!");
                return;
            }

            if (result == HitResult.Missed)
            {
                currentPlayer = currentPlayer == 1 ? 2 : 1;
                int nextPlayer = currentPlayer;
                ShowPassDevice($"Promasaj! Predaj uredjaj Igracu {nextPlayer}.", () =>
                {
                    RenderBattleForCurrentPlayer();
                    ShowPanel(pnlBattle);
                });
            }
            else
            {
                lblStatus.Text = result == HitResult.Sunken ? $"Igrac {currentPlayer} je potopio brod! Puca ponovno." : $"Igrac {currentPlayer} je pogodio! Puca ponovno.";
            }
        }

        private void RenderTrackBoard(Button[,] buttons, ShotsGrid myShots, bool clickable)
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var state = myShots.GetSquare(r, c).SquareState;
                    var btn = buttons[r, c];
                    switch (state)
                    {
                        case SquareState.Intact:
                            btn.Text = "";
                            btn.BackColor = Color.LightGray;
                            btn.Enabled = clickable && !gameOver;
                            break;
                        case SquareState.Eliminated:
                            btn.Text = ".";
                            btn.ForeColor = Color.DarkGray;
                            btn.BackColor = Color.Gainsboro;
                            btn.Enabled = false;
                            break;
                        case SquareState.Missed:
                            btn.Text = "O";
                            btn.ForeColor = Color.White;
                            btn.BackColor = Color.RoyalBlue;
                            btn.Enabled = false;
                            break;
                        case SquareState.Hit:
                            btn.Text = "X";
                            btn.ForeColor = Color.White;
                            btn.BackColor = Color.OrangeRed;
                            btn.Enabled = false;
                            break;
                        case SquareState.Sunken:
                            btn.Text = "X";
                            btn.ForeColor = Color.White;
                            btn.BackColor = Color.DarkRed;
                            btn.Enabled = false;
                            break;
                    }
                }
            }
        }

        private void RenderOwnBoard(Button[,] buttons, Fleet myFleet, ShotsGrid incomingShots)
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    var btn = buttons[r, c];
                    var incomingState = incomingShots.GetSquare(r, c).SquareState;
                    var ship = myFleet.Ships.FirstOrDefault(s => s.Contains(r, c));

                    if (incomingState == SquareState.Missed)
                    {
                        btn.Text = "O";
                        btn.BackColor = Color.LightSkyBlue;
                        btn.ForeColor = Color.White;
                    }
                    else if (ship != null)
                    {
                        bool sunk = ship.Squares.All(sq => sq.IsHit);
                        var mySquare = ship.Squares.First(sq => sq.Row == r && sq.Column == c);
                        if (sunk)
                        {
                            btn.Text = "X";
                            btn.BackColor = Color.Black;
                            btn.ForeColor = Color.White;
                        }
                        else if (mySquare.IsHit)
                        {
                            btn.Text = "X";
                            btn.BackColor = Color.OrangeRed;
                            btn.ForeColor = Color.White;
                        }
                        else
                        {
                            btn.Text = "#";
                            btn.BackColor = Color.DimGray;
                            btn.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        btn.Text = "";
                        btn.BackColor = Color.AliceBlue;
                    }
                }
            }
        }

        private void RenderBattleForCurrentPlayer()
        {
            if (mode == GameMode.Singleplayer)
            {
                lblOwnTitle.Text = "Tvoja flota";
                lblEnemyTitle.Text = "Protivnik (racunalo)";
                RenderOwnBoard(ownBoardButtons, playerFleet, computerGunnery.RecordGrid);
                RenderTrackBoard(trackBoardButtons, playerShotsOnComputer, isPlayerTurn);
                lblStatus.Text = isPlayerTurn ? "Tvoj potez!" : "Racunalo je na potezu...";
            }
            else
            {
                lblOwnTitle.Text = $"Igrac {currentPlayer} - tvoja flota";
                lblEnemyTitle.Text = $"Igrac {currentPlayer} - protivnicka ploca";
                if (currentPlayer == 1)
                {
                    RenderOwnBoard(ownBoardButtons, p1Fleet, p2ShotsOnP1);
                    RenderTrackBoard(trackBoardButtons, p1ShotsOnP2, true);
                }
                else
                {
                    RenderOwnBoard(ownBoardButtons, p2Fleet, p1ShotsOnP2);
                    RenderTrackBoard(trackBoardButtons, p2ShotsOnP1, true);
                }
                lblStatus.Text = $"Na potezu: Igrac {currentPlayer}";
            }
        }

        // ==================== GAME OVER ====================

        private void BuildGameOverPanel()
        {
            pnlGameOver = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            Controls.Add(pnlGameOver);

            lblGameOverMessage = new Label
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(50, 180),
                Size = new Size(800, 120),
            };

            var btnNewGame = new Button
            {
                Text = "Nova igra",
                Location = new Point(360, 320),
                Width = 180,
                Height = 44,
                BackColor = Color.LightGreen
            };
            btnNewGame.Click += (s, e) => ShowPanel(pnlSetup);

            pnlGameOver.Controls.Add(lblGameOverMessage);
            pnlGameOver.Controls.Add(btnNewGame);
        }

        private void EndGame(string message)
        {
            gameOver = true;
            aiTimer.Stop();
            lblGameOverMessage.Text = message;
            ShowPanel(pnlGameOver);
        }
    }
}
