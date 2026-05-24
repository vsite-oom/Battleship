namespace Battleship.Model;

/// <summary>Defines the contract for a targeting strategy that selects the next square to fire at.</summary>
public interface ITargetSelector
{
    /// <summary>Returns the next square to target.</summary>
    /// <returns>The next target square.</returns>
    Square Next();
}