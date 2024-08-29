using System.Media;

namespace Vsite.Oom.Battleship.Game
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

        private void toBattle_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            Hide();
            newGame.Show();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {

        }
    }
}
