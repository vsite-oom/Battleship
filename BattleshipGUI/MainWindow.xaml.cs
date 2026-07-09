using System.Windows;
using System.Windows.Controls;

namespace BattleshipGUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ShowScreen(new MenuScreen(this));
    }

    public void ShowScreen(UserControl screen)
    {
        RootGrid.Children.Clear();
        RootGrid.Children.Add(screen);
    }
}
