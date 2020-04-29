
namespace Vsite.Oom.Battleship.Model
{
    public class RulesSingleton
    {
        private RulesSingleton()
        {
            Rows = 10;
            Columns = 10;
            ShipLengths = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
        }

        public static RulesSingleton Instance { get; } = new RulesSingleton();
        public readonly int Rows;
        public readonly int Columns;
        public readonly int[] ShipLengths;
    }
}
