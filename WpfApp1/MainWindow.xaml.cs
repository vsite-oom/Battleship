using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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