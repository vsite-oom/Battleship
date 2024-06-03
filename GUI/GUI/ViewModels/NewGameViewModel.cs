using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Vsite.Oom.Battleship.GUI.Models;

namespace Vsite.Oom.Battleship.GUI.ViewModels;

public partial class NewGameViewModel : ViewModelBase
{
    [ObservableProperty] private Game _game;

    public ObservableCollection<DisplayDifficulty> Difficulties { get; } =
    [
        new DisplayDifficulty { Difficulty = GameDifficulty.Easy },
        new DisplayDifficulty { Difficulty = GameDifficulty.Normal },
        new DisplayDifficulty { Difficulty = GameDifficulty.Hard }
    ];

    public NewGameViewModel() : this(null!)
    {
    }
    public NewGameViewModel(Game game)
    {
        _game = game;
        
        Difficulties.First(x => x.Difficulty == Game.Difficulty).IsSelected = true;
    }

    [RelayCommand]
    private void SetDifficulty(GameDifficulty difficulty)
    {
        Game.Difficulty = difficulty;
    }
}