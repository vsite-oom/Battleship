namespace Model.Model;

public abstract class GridBase
{

    public IEnumerable<IEnumerable<Square>> GetAvailablePlacements(int length)
    {
        return GetHorizontalAvailablePlacements(length).Concat(GetVerticalAvailablePlacements(length));
    }

    private object GetVerticalAvailablePlacements(int length)
    {
        throw new NotImplementedException();
    }

    private object GetHorizontalAvailablePlacements(int length)
    {
        throw new NotImplementedException();
    }


}