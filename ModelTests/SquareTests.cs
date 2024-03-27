using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class SquareTests
    {
        [TestMethod]
        public void ConstructorCreatesSquarewithRowColumn()
        {
            int row = 4;
            int col = 8;
            var square = new Square(row, col);
            Assert.AreEqual(row, square.Row);
            Assert.AreEqual(col, square.Col);
        }
    }
}
