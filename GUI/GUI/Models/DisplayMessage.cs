using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Vsite.Oom.Battleship.GUI.Models;

public class DisplayMessage : INotifyPropertyChanged
{
    private bool isVisible;
    public string Title { get; set; }
    public string Content { get; set; }

    public bool IsVisible
    {
        get => isVisible;
        set
        {
            if (value == isVisible) return;
            isVisible = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}