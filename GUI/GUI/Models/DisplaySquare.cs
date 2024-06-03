using System.ComponentModel;
using System.Runtime.CompilerServices;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI.Models;

public class DisplaySquare(int row, int column) : Square(row, column), INotifyPropertyChanged
{

    private SquareState _squareState;
    
    public override SquareState SquareState
    {
        get => _squareState;
        set
        {
            if (_squareState == value) return;
            _squareState = value;
            OnPropertyChanged();
        }
    }
    
    public bool IsShip { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}