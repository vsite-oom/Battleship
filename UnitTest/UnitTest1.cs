using NUnit.Framework;

namespace UnitTest
{
    public class Tests
    {
        [Test]
       
            public void SquareConstructorCreatesSquareWithGivenPosition()
            {
                Model.Square s = new Model.Square(1, 8);
                Assert.AreEqual(1, s.Row);
                Assert.AreEqual(8, s.Column);
            }
        }
    }
