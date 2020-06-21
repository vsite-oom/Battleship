using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI
{
    public partial class BattleshipsGUI : Form
    {
        public BattleshipsGUI()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            playerFleetGrid.Controls.Clear();
            enemyFleetGrid.Controls.Clear();
            if ((new Random()).Next() % 2 == 0)
            {
                playerFleetGrid.Turn = true;
            }

            if (!playerFleetGrid.Turn)
            {
                enemyFleetGrid.Turn = true;
            }

            playerFleetGrid.Init();
            enemyFleetGrid.Init();
            enemyGunner = new Gunner(RulesSingleton.Instance.Rows, RulesSingleton.Instance.Columns, RulesSingleton.Instance.ShipLengths);
            newGameStarted = true;
            if (enemyFleetGrid.Turn)
            {
                EnemyShoot();
            }
        }

        private void PlayerShoot(int index)
        {
            Square square = new Square((index / 10) % 10, index % 10);
            HitResult hitResult = enemyFleetGrid.Fleet.Hit(square);
            switch (hitResult)
            {
                case HitResult.Missed:
                    enemyFleetGrid.buttons.ElementAt(index).BackColor = Color.AliceBlue;
                    break;
                case HitResult.Hit:
                    enemyFleetGrid.buttons.ElementAt(index).BackColor = Color.Orange;
                    break;
                case HitResult.Sunken:
                    SunkenShips(enemyFleetGrid);
                    if (enemyFleetGrid.Fleet.Ships.Count() == 0)
                    {
                        MessageBox.Show("YOU WON !!!");
                        newGameStarted = false;
                    }

                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            enemyFleetGrid.Turn = true;
            playerFleetGrid.Turn = false;

            EnemyShoot();
        }

        private void EnemyShoot()
        {
            Square square = enemyGunner.NextTarget();
            int buttonNumber = square.Row * 10 + square.Column;
            HitResult hitResult = playerFleetGrid.Fleet.Hit(square);
            switch (hitResult)
            {
                case HitResult.Missed:
                    playerFleetGrid.buttons.ElementAt(buttonNumber).BackColor = Color.AliceBlue;
                    break;
                case HitResult.Hit:
                    playerFleetGrid.buttons.ElementAt(buttonNumber).BackColor = Color.Orange;
                    break;
                case HitResult.Sunken:
                    SunkenShips(playerFleetGrid);
                    if (playerFleetGrid.Fleet.Ships.Count() == 0)
                    {
                        MessageBox.Show("You lose :(");
                        newGameStarted = false;
                    }
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            enemyGunner.ProcessHitResult(hitResult);
            
            playerFleetGrid.Turn = true;
            enemyFleetGrid.Turn = false;

        }

        public void SunkenShips(DrawFleetGrid flow)
        {
            foreach (var ships in flow.Fleet.Ships)
            {
                foreach (var shipSquare in ships.Squares)
                {
                    if(shipSquare.SquareState == SquareState.Sunken)
                    {
                        flow.buttons.ElementAt(shipSquare.Row*10 + shipSquare.Column).BackColor = Color.Red;
                    }
                }
            }
        }

        private bool newGameStarted;
        private Gunner friendlyGunner;
        private Gunner enemyGunner;


        private void enemyFleetGrid_Click(object sender, EventArgs e)
        {
            if (newGameStarted)
            {
                if (((Control) sender).Parent is DrawFleetGrid)
                {
                    MessageBox.Show("IT IS !");
                }

                if (playerFleetGrid.Turn)
                {
                    PlayerShoot(enemyFleetGrid.buttonClicked);
                }

                if (enemyFleetGrid.Turn)
                {
                    EnemyShoot();
                }

            }
            else
            {
                MessageBox.Show("Start new game please.");
            }
        }
    }
}