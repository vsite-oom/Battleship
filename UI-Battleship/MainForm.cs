using System.Windows.Forms.VisualStyles;

namespace UI_Battleship
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btNewGame_Click(object sender, EventArgs e)
        {
            NewGameForm gameWindow = new NewGameForm();
            gameWindow.ShowDialog();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
