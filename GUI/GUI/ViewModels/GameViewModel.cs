using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Vsite.Oom.Battleship.GUI.Messages;
using Vsite.Oom.Battleship.GUI.Models;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI.ViewModels;

public partial class GameViewModel : ViewModelBase
{
    [ObservableProperty] private Game _game;

    [ObservableProperty] private string _message = "";

    public GameViewModel() : this(null!) { }
    public GameViewModel(Game game)
    {
        _game = game;

        _game.NewGame();
    }

    
    [RelayCommand(CanExecute = nameof(CanShoot))]
    private void Shoot(DisplaySquare target)
    {
        Game.PlayerAttack(target);

        if (Game.IsGameOver(true))
        {
            Message = "You won!";
            return;
        }

        OpponentShoot();
    }
    private bool CanShoot(DisplaySquare? square)
    {
        return square?.SquareState == SquareState.Intact;
    }

    private void OpponentShoot()
    {
        Game.OpponentAttack();

        if (Game.IsGameOver(false))
        {
            Message = "You lost!";
        }
    }
    
    // Start new game with the same settings
    [RelayCommand]
    private void NewGame()
    {
        Message = "";
        Game.NewGame();
    }

    // Change game settings
    [RelayCommand]
    private void ChangeSettings()
    {
        Message = "";
        WeakReferenceMessenger.Default.Send(new ChangeSettingsMessage());
    }
}