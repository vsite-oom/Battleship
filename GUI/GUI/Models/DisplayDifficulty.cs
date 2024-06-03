using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vsite.Oom.Battleship.GUI.Models;

public class DisplayDifficulty: INotifyPropertyChanged
{
    private bool isSelected;
    
    public GameDifficulty Difficulty { get; set; }
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            if (value == isSelected) return;
            isSelected = value;
            OnPropertyChanged();
        }
    }
    
    public string Description => Difficulty switch
    {
        GameDifficulty.Easy => "Eliminated and sunken squares are shown on the board.",
        GameDifficulty.Normal => "Sunken squares are shown on the board.",
        GameDifficulty.Hard => "No hints are shown.",
        _ => throw new ArgumentOutOfRangeException(nameof(Difficulty))
    };
    

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}