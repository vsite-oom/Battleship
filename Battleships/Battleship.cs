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
using System.Threading;

namespace Battleships
{
    public partial class Battleship : Form
    {
        public Battleship()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            PCFleetControl.ButtonClick += on_click;
        }
        
        private void OnGameEndEvent(object sender, EventArgs args)
        {
            Invoke((MethodInvoker) delegate () {
                var result = MessageBox.Show("Game has come to an end, do you want to repeat?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    Close();
                }
                else
                {
                    StartGame_Click(this, EventArgs.Empty);
                }
            });
        }

        private void on_click(object sender, EventArgs e)
        {
            if (!IsGameStarted)
            {
                return;
            }

            var button = (ShipButton)sender;

            if (button == null)
            {
                return;
            }

            if (button.IsSunken)
            {
                return;
            }

            PlayerManager.EnqueuePlayerNotification(button);
        }


        private void OnApplicationExit(object sender, EventArgs args)
        {
            try
            {
                if (PlayerManager != null)
                {
                    PlayerManager.Cleanup();
                }
            }
            catch { }
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            if (PlayerManager != null)
            {
                PlayerManager = null;
            }

            PCFleetControl.Initialize();
            PlayerFleetControl.Initialize();

            IsGameStarted = true;
            PlayerManager = new PlayerManager(PCFleetControl, PlayerFleetControl);
            PlayerManager.EndGameEvent += OnGameEndEvent;
        }

        private PlayerManager PlayerManager;
        private bool IsGameStarted = false;

    }
}
