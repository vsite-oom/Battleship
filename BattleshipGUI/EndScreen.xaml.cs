using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BattleshipGUI;

public partial class EndScreen : UserControl
{
    private readonly MainWindow _main;

    public EndScreen(MainWindow main, bool victory, int shotsFired, int hitsLanded)
    {
        InitializeComponent();
        _main = main;

        if (victory)
        {
            OutcomeText.Text = "VICTORY";
            OutcomeText.Foreground = (Brush)FindResource("CrtBrightBrush");
            OutcomeSubtext.Text = "ENEMY FLEET DESTROYED";
        }
        else
        {
            OutcomeText.Text = "DEFEAT";
            OutcomeText.Foreground = (Brush)FindResource("CrtRedBrush");
            OutcomeSubtext.Text = "YOUR FLEET HAS BEEN LOST AT SEA";
        }

        ShotsLabel.Text = shotsFired.ToString();
        HitsLabel.Text = hitsLanded.ToString();
        int acc = shotsFired > 0 ? (int)Math.Round(100.0 * hitsLanded / shotsFired) : 0;
        AccuracyLabel.Text = $"{acc}%";
    }

    private void NewBattle_Click(object sender, RoutedEventArgs e)
    {
        _main.ShowScreen(new PlacementScreen(_main));
    }

    private void MainMenu_Click(object sender, RoutedEventArgs e)
    {
        _main.ShowScreen(new MenuScreen(_main));
    }
}
