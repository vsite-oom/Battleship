using System.Windows;
using System.Windows.Controls;

namespace BattleshipGUI;

public partial class MenuScreen : UserControl
{
    private readonly MainWindow _main;

    public MenuScreen(MainWindow main)
    {
        InitializeComponent();
        _main = main;
    }

    private void NewGame_Click(object sender, RoutedEventArgs e)
    {
        _main.ShowScreen(new PlacementScreen(_main));
    }
}
