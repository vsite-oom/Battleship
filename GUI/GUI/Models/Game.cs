using System.Collections.Generic;
using System.Linq;
using Vsite.Oom.Battleship.Model;

namespace Vsite.Oom.Battleship.GUI.Models;

public enum GameDifficulty
{
    Easy,
    Normal,
    Hard
}

public class Game(SquareEliminator eliminator)
{
    private readonly SquareEliminator _eliminator = eliminator;
    
    public GameDifficulty Difficulty { get; set; } = GameDifficulty.Normal;

    public int Rows { get; set; } = 10;
    public int Columns { get; set; } = 10;

    public int ShipLength2 { get; set; } = 1;
    public int ShipLength3 { get; set; } = 2;
    public int ShipLength4 { get; set; } = 1;
    public int ShipLength5 { get; set; } = 1;

    public int[] ShipLengths
    {
        get
        {
            var lengths = new List<int>();
            for (int i = 0; i < ShipLength2; i++)
            {
                lengths.Add(2);
            }

            for (int i = 0; i < ShipLength3; i++)
            {
                lengths.Add(3);
            }

            for (int i = 0; i < ShipLength4; i++)
            {
                lengths.Add(4);
            }

            for (int i = 0; i < ShipLength5; i++)
            {
                lengths.Add(5);
            }

            return lengths.ToArray();
        }
    }

    public Player Player { get; } = new();
    private Player Opponent { get; } = new();

    public void NewGame()
    {
        Player.Initialize(Rows, Columns, ShipLengths);
        Opponent.Initialize(Rows, Columns, ShipLengths);
    }

    public void PlayerAttack(DisplaySquare target)
    {
        Player.Gunnery.SetTarget(target.Row, target.Column);
        
        var shipSquare = Opponent.Fleet.Ships
            .SelectMany(s => s.Squares)
            .FirstOrDefault(s => s.Row == target.Row && s.Column == target.Column);

        if (shipSquare is not null)
        {
            ShipIsHit(target, shipSquare, Player, Opponent);
            
            var ship = Opponent.Fleet.Ships.FirstOrDefault(s => s.Squares.Contains(shipSquare));
            if (ship is null) return;
            
            CheckIfShipSunken(ship, true);
        }
        else
        {
            ShipIsMissed(target, Player, Opponent);
        }
    }

    public void OpponentAttack()
    {
        var target = Opponent.Gunnery.Next();

        var shipSquare = Player.Fleet.Ships
            .SelectMany(s => s.Squares)
            .FirstOrDefault(s => s.Row == target.Row && s.Column == target.Column);

        if (shipSquare != null)
        {
            ShipIsHit(target, shipSquare, Opponent, Player);

            var ship = Player.Fleet.Ships.FirstOrDefault(s => s.Squares.Contains(shipSquare));
            if (ship is null) return;

            CheckIfShipSunken(ship, false);
        }
        else
        {
            ShipIsMissed(target, Opponent, Player);
        }
    }
    
    private void ShipIsHit(Square target, Square shipSquare, Player attacker, Player defender)
    {
        shipSquare.ChangeState(SquareState.Hit);
        attacker.ShotsBoard[target.Row][target.Column].ChangeState(SquareState.Hit);
        
        attacker.Gunnery.ProcessHit(HitResult.Hit);
        defender.FleetBoard[target.Row][target.Column].ChangeState(SquareState.Hit);
    }
    
    private void ShipIsMissed(Square target, Player attacker, Player defender)
    {
        attacker.Gunnery.ProcessHit(HitResult.Missed);
        attacker.ShotsBoard[target.Row][target.Column].ChangeState(SquareState.Missed);
        defender.FleetBoard[target.Row][target.Column].ChangeState(SquareState.Missed);
    }
    
    private void CheckIfShipSunken(Ship ship, bool isPlayer)
    {
        if (ship.Squares.Any(square => square.SquareState == SquareState.Intact))
        {
            return;
        }

        if (isPlayer)
        {
            if (Difficulty == GameDifficulty.Easy)
            {
                var toEliminate = _eliminator.ToEliminate(ship.Squares, Rows, Columns);
                foreach (var square in toEliminate)
                {
                    Player.ShotsBoard[square.Row][square.Column].ChangeState(SquareState.Eliminated);
                }
            }

            if (Difficulty != GameDifficulty.Hard)
            {
                foreach (var square in ship.Squares)
                {
                    Player.ShotsBoard[square.Row][square.Column].ChangeState(SquareState.Sunken);
                }
            }
        }
        else
        {
            Opponent.Gunnery.ProcessHit(HitResult.Sunken);
        }
    }
    
    public bool IsGameOver(bool isPlayer)
    {
        return isPlayer
            ? Opponent.Fleet.Ships.SelectMany(s => s.Squares).All(ship => ship.SquareState != SquareState.Intact)
            : Player.Fleet.Ships.SelectMany(s => s.Squares).All(ship => ship.SquareState != SquareState.Intact);
    }
}