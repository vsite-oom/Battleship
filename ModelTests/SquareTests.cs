using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests;
[TestClass]
class SquareTests
{
    [TestMethod]

public void ConstructorCreateSquareWithRowAndColumnProvided()
{
    int row = 4;
    int column = 8;

    var square = new Square(row, column);

    Assert.AreEqual(row, square.Row);
    Assert.AreEqual(column, square.Column);

}

}