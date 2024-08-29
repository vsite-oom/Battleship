using System.Media;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.Game
{
    public partial class BattleGame : Form
    {
        private SoundPlayer soundPlayer;
        private List<int> ShipLengths = new List<int> { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        private Fleet playerFleet;
        private Fleet enemyFleet;
        private Gunnery shipGunnery;
        private SquareEliminator squareEliminator;
        private List<Button> hitSquares = new List<Button>();
        private List<int> shipsToShoot;
        private int playerHitCounter;
        private int opponentHitCounter;
        private int scoreBooster = 5;
        private int totalTime = 0;

        private const int gridRow = 10;
        private const int gridColumn = 10;
        private readonly string gridColumnLetters = "ABCDEFGHIJ";
        private readonly string mojiBodoviText = "BODOVI";
        private readonly string aiBodoviText = "AI BODOVI";
        private readonly string startText = "START";
        private readonly string restartText = "RESTART";
        private readonly string baseAudioPath = @"C:\Users\Korisnik\Desktop\Battleship-bgajski\Audio\";
        private readonly string windowsAudioPath = @"C:\Windows\Media\";
        private readonly string singleGunshot = "single-gunshot.wav";
        private readonly string singleHit = "single-shot-hit.wav";
        private readonly string startReset = "chimes.wav";
        private readonly string igrac = "Igrač";
        private readonly string neprijatelj = "neprijatelj";
        private readonly string win = "WIN";
        private readonly string poraz = "PORAZ";
        private readonly string gameOver = "Game Over!";

        public BattleGame()
        {
            InitializeComponent();
        }
    }
}
