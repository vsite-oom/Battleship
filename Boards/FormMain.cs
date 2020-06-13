using System;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Boards
{
    enum ActiveGunner
    {
        Person,
        Computer
    }
    public partial class FormMain : Form
    {

        private Opponent opponent;

        public FormMain()
        {

            InitializeComponent();
        }


        private void buttonResetClick(object sender, EventArgs e)
        {
            //if fleet has been arranged - toggle button to Reset 
            Button btn = (Button)sender;
            if (btn.Text == "New game?")
                Application.Restart();
            if (btn.Text == "Arrange fleet")
                btn.Text = "Reset";
            
            
            fleetDisplay.CreateFleet();
            evidenceGrid.Reset();

            ShootStatusPersonLabel.Text = "";
            ShootStatusPcLabel.Text = "";

            buttonStartGame.Enabled = true;
            // wire-up evidence grid with buttons
            evidenceGrid.RaiseButtonSelectedEvent += ButtonSelectedEvent;


        }
        
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            //  toggle buttonStartGame 
            Button btn = (Button)sender;
            if (btn.Enabled)
                btn.Enabled = false;
            buttonReset.Text = "New game?";


            opponent = Opponent.getNewOpponent();
            opponent.GameOverEvent += OnGameOver;
            fleetDisplay.GameOverEvent += OnGameOver;


            Play(SelectFirstPlayer());


        }

        private void OnGameOver(object sender, string e)
        {
            Controls.Remove(actionButton);
            Controls.Remove(buttonStartGame);
            ShootStatusPersonLabel.Text = e;
            ShootStatusPcLabel.Text = e;
            
        }

        private void LabelShootStatus(object sender, ShootingCompletedEventArgs e)
        {
            //prints last target info on ShootStatusPersonLabel
            
            ShootStatusPersonLabel.Visible = true;
            ShootStatusPersonLabel.Text = "Last Hit: " + ((char)(e.column + 'A')).ToString() + " " + (e.row + 1).ToString() + "\n" + "Result: " + Enum.GetName(typeof(HitResult), e.hitResult);
            Play(SwitchPlayers(ActiveGunner.Person));
        }

        private void LabelPcShootStatus(object sender, ShootingCompletedEventArgs e)
        {

            //prints pc's last target info on ShootStatusPcLabel
            ShootStatusPcLabel.Visible = true;
            ShootStatusPcLabel.Text = "PC Last Hit: " + ((char)(e.column + 'A')).ToString() + " " + (e.row + 1).ToString() + "\n" + "Result: " + Enum.GetName(typeof(HitResult), e.hitResult);

        }

        private void ButtonSelectedEvent(object sender, string e)
        {
            //gets activated on player picking a field on his EvidenceGrid.
            actionButton.Enabled = true;

            if (e.Length == 0)
            {
                actionButton.Enabled = false;
                return;
            }
            else
            {
                actionButton.Text = "Shoot " + e + " ?";
                actionButton.Visible = true;
                actionButton.Enabled = true;
            }
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            ShootStatusPersonLabel.Enabled = true;
            ShootStatusPersonLabel.Visible = true;
            actionButton.Enabled = false;
        }
        private void Play(ActiveGunner active)
        {
            if (active == ActiveGunner.Person)
            {
                //enable evidenceGrid and action button while Person is on the move.
                evidenceGrid.Enabled = true;
                actionButton.Visible = true;

                //Person wire-up on events
                actionButton.Click += opponent.ShootIncomingRequest; //forward your opponent ShootIncomingRequest upon clicking action button.
                opponent.ShootingCompleted += evidenceGrid.OnShootingCompleted; //update evidenceGrid upon successfull shooting. 
                opponent.ShootingCompleted += LabelShootStatus; //update LabelShootPcStatus upon sucessfull shooting.

            }
            else
            {
                if (ShootStatusPcLabel.Text == "You win!" || ShootStatusPcLabel.Text == "You lost!")
                    return;
                //disable evidenceGrid while opponent (pc) is on the move.
                evidenceGrid.Enabled = false;
                //Computer wire-up on events
                fleetDisplay.ShootingResponse += opponent.ShootResponse;
                opponent.ShootingCompleted += fleetDisplay.Play;
                fleetDisplay.ShootingResponse += LabelPcShootStatus;

                //start opponent's move (pc).
                opponent.Play();


                //switch to Person after finished.
                Play(SwitchPlayers(ActiveGunner.Computer));
            }
        }

        private ActiveGunner SelectFirstPlayer()
        {
            Random random = new Random();
            int i = random.Next(0, 2);
            var active = (ActiveGunner)i;
            return active;
        }
        private ActiveGunner SwitchPlayers(ActiveGunner active)
        {
            if (active == ActiveGunner.Person)
            {
                actionButton.Click -= opponent.ShootIncomingRequest;
                opponent.ShootingCompleted -= evidenceGrid.OnShootingCompleted;
                opponent.ShootingCompleted -= LabelShootStatus;
                return ActiveGunner.Computer;
            }
            else
            {
                fleetDisplay.ShootingResponse -= opponent.ShootResponse;
                fleetDisplay.ShootingResponse -= LabelPcShootStatus;
                opponent.ShootingCompleted -= fleetDisplay.Play;
                return ActiveGunner.Person;

            }
        }
    }
}

