namespace Battleship.Model;

/// <summary>Selects a target square along the axis of a partially sunken ship.</summary>
/// <remarks>This selector is used after the ship axis has been established by two confirmed hits in line.</remarks>
public class InlineTargetSelector : ITargetSelector
{
    /// <inheritdoc/>
    public Square Next()
    {
        throw new NotImplementedException();
    }
}