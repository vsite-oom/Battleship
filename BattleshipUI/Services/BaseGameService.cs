using Model;

namespace BattleshipGame.Services;

public abstract class BaseGameService
{
    public int GridRows { get; protected set; }
    public int GridColumns { get; protected set; }
    public int[] ShipLengths { get; protected set; }
    public Dictionary<int, int> ShipConfiguration { get; protected set; }
    public Fleet PlayerFleet { get; protected set; }
    public Fleet AIFleet { get; protected set; }
    public Gunnery AIGunnery { get; protected set; }

    public virtual bool IsGameOver => PlayerHits.Count(h => (h.state == SquareState.Sunken || h.state == SquareState.Hit || h.state == SquareState.Eliminated)) == ShipLengths.Sum() ||
                                     AIHits.Count(h => (h.state == SquareState.Sunken || h.state == SquareState.Hit || h.state == SquareState.Eliminated)) == ShipLengths.Sum();

    public bool IsPlayerTurn { get; protected set; } = true;
    public List<(int row, int col, SquareState state)> PlayerHits { get; } = new();
    public List<(int row, int col, SquareState state)> AIHits { get; } = new();

    public abstract void InitializeGame(int gridSize, Dictionary<int, int> shipConfig);
    public abstract HitResult PlayerShoot(int row, int col);
    public abstract HitResult AIShoot();

    public bool HasPlayerHit(int row, int col)
    {
        return PlayerHits.Any(h => h.row == row && h.col == col);
    }

    public SquareState GetPlayerHitState(int row, int col)
    {
        return PlayerHits.FirstOrDefault(h => h.row == row && h.col == col).state;
    }

    public bool HasAIHit(int row, int col)
    {
        return AIHits.Any(h => h.row == row && h.col == col);
    }

    public SquareState GetAIHitState(int row, int col)
    {
        return AIHits.FirstOrDefault(h => h.row == row && h.col == col).state;
    }

    public bool IsPlayerShipAt(int row, int col)
    {
        return PlayerFleet.Ships.Any(ship => ship.Contains(row, col));
    }

    public bool IsAIShipAt(int row, int col)
    {
        return AIFleet.Ships.Any(ship => ship.Contains(row, col));
    }
    protected int[] ConvertShipConfigToArray(Dictionary<int, int> shipConfig)
    {
        var result = new List<int>();
        foreach (var kvp in shipConfig)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                result.Add(kvp.Key);
            }
        }
        return result.ToArray();
    }
}
