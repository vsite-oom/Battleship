namespace Vsite.Oom.Battleship.Model;

public class InlineTargetSelector : ITargetSelector
{
    private readonly ShotsGrid shotsGrid;
    private readonly IEnumerable<Square> squaresHit;
    private readonly int shipLength;
    private Random random = new Random();

    public InlineTargetSelector(ShotsGrid shotsGrid, IEnumerable<Square> squaresHit, int shipLength)
    {
        this.shotsGrid = shotsGrid;
        this.squaresHit = squaresHit;
        this.shipLength = shipLength;
    }

    public Square Next()
    {
        var sorted = squaresHit.OrderBy(sq => sq.Row + sq.Column);
        var directionCandidates = new List<IEnumerable<Square>>();
        if(sorted.First().Row == sorted.Last().Row)
        {
            //Horizontal
            var left = shotsGrid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Left);
            if(left.Any())
                directionCandidates.Add(left);

            var right = shotsGrid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Right);
            if(right.Any())
                directionCandidates.Add(right);
        }
        else 
        {
            //Vertical
            var up = shotsGrid.GetSquaresInDirection(sorted.First().Row, sorted.First().Column, Direction.Upwards);
            if (up.Any())
                directionCandidates.Add(up);

            var down = shotsGrid.GetSquaresInDirection(sorted.Last().Row, sorted.Last().Column, Direction.Downwards);
            if (down.Any())
                directionCandidates.Add(down);
        }

        var groupByLength = directionCandidates.GroupBy(lsq => lsq.Count());
        var sortedByLength = groupByLength.OrderByDescending(g => g.Key);
        var longestDirections = sortedByLength.First();
        var candidates = longestDirections.Count();
        if (candidates == 1)
            return longestDirections.First().First();

        int selectedIndex = random.Next(0, candidates);
        return longestDirections.ElementAt(selectedIndex).First();
    
    }
}