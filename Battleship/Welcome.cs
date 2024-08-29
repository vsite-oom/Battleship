using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class Welcome : Form
    {
        private BattleGame newGame = new BattleGame();
        private SoundPlayer soundPlayer;
        private readonly string fileShipName = "battleships.wav";
        private readonly string filePathName = @"c:\oom\Battleship\music\";

        public Welcome()
        {
            InitializeComponent();
            PlayIntroMusic();
        }

        private void PlayIntroMusic()
        {
            soundPlayer = new SoundPlayer(Path.Combine(filePathName, fileShipName));
            soundPlayer.PlayLooping();
        }

        private void ToBattle_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            Hide();
            newGame.Show();
        }
    }
}
