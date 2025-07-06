using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipBlazor.Services
{
    public class GameService
    {
        public int GridRows { get; private set; }
        public int GridColumns { get; private set; }
        public int[] ShipLengths { get; private set; }

        public Fleet PlayerFleet { get; private set; }
        public Fleet AIFleet { get; private set; }

        public Gunnery AIGunnery { get; private set; }

        public virtual bool IsGameOver => PlayerHits.Count(h => (h.state == SquareState.Sunken || h.state == SquareState.Hit || h.state == SquareState.Eliminated)) == ShipLengths.Sum() ||
                                     AIHits.Count(h => (h.state == SquareState.Sunken || h.state == SquareState.Hit || h.state == SquareState.Eliminated)) == ShipLengths.Sum();

        public bool IsPlayerTurn { get; private set; } = true;

        public List<(int row, int col, SquareState state)> PlayerHits { get; } = new();
        public List<(int row, int col, SquareState state)> AIHits { get; } = new();

        public GameService()
        {
        }

        public void InitializeGame(int gridSize, int[] shipLengths)
        {
            GridRows = gridSize;
            GridColumns = gridSize;
            ShipLengths = shipLengths;
            Console.WriteLine(shipLengths);

            var playerFleetBuilder = new FleetBuilder(GridRows, GridColumns, ShipLengths);
            PlayerFleet = playerFleetBuilder.CreateFleet();

            var aiFleetBuilder = new FleetBuilder(GridRows, GridColumns, ShipLengths);
            AIFleet = aiFleetBuilder.CreateFleet();

            AIGunnery = new Gunnery(GridRows, GridColumns, ShipLengths);

            PlayerHits.Clear();
            AIHits.Clear();

            IsPlayerTurn = true;
        }

        public HitResult PlayerShoot(int row, int col)
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

        public HitResult AIShoot()
        {
            if (IsPlayerTurn || IsGameOver)
                return HitResult.Missed;

            var target = AIGunnery.Next();
            var result = PlayerFleet.Hit(target.Row, target.Column);

            AIGunnery.ProcessHitResult(result);

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
    }
}