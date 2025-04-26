using Model;

namespace BattleshipGame.Services;

public class NormalGameService : BaseGameService
{
    public NormalGameService() { }

    public override void InitializeGame(int gridSize, Dictionary<int, int> shipConfig)
    {
        GridRows = gridSize;
        GridColumns = gridSize;
        ShipConfiguration = shipConfig;
        ShipLengths = ConvertShipConfigToArray(shipConfig);

        var playerFleetBuilder = new FleetBuilder(GridRows, GridColumns, ShipLengths);
        PlayerFleet = playerFleetBuilder.CreateFleet();

        var aiFleetBuilder = new FleetBuilder(GridRows, GridColumns, ShipLengths);
        AIFleet = aiFleetBuilder.CreateFleet();

        AIGunnery = new Gunnery(GridRows, GridColumns, ShipLengths);

        PlayerHits.Clear();
        AIHits.Clear();
        IsPlayerTurn = true;
    }

    public override HitResult PlayerShoot(int row, int col)
    {
        if (!IsPlayerTurn || IsGameOver)
            return HitResult.Missed;

        var result = AIFleet.Hit(row, col);

        var squareState = result switch
        {
            HitResult.Hit => SquareState.Hit,
            HitResult.Sunken => SquareState.Sunken,
            _ => SquareState.Missed
        };

        PlayerHits.Add((row, col, squareState));

        IsPlayerTurn = false;

        return result;
    }

    public override HitResult AIShoot()
    {
        if (IsPlayerTurn || IsGameOver)
            return HitResult.Missed;

        var target = AIGunnery.Next();
        var result = PlayerFleet.Hit(target.Row, target.Column);
        AIGunnery.ProcessHit(result);

        var squareState = result switch
        {
            HitResult.Hit => SquareState.Hit,
            HitResult.Sunken => SquareState.Sunken,
            _ => SquareState.Missed
        };

        AIHits.Add((target.Row, target.Column, squareState));

        IsPlayerTurn = true;

        return result;
    }
}