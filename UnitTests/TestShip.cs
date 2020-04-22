using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Vsite.Oom.BattleShip.Model.UnitTests
{
    [TestClass]
    public class TestShip
    {
        [TestMethod]
        public void ConstructorCreatesShipsFromSquaresProvided()
        {
            Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
            Assert.IsTrue(ship.Squares.Contains(new Square(1, 4)));
            Assert.IsTrue(ship.Squares.Contains(new Square(1, 5)));
            Assert.IsTrue(ship.Squares.Contains(new Square(1, 6)));

        }

        [TestMethod]
        public void HitMethodOnFirstSquareReturnsHitResultHit()
        {
            {
                Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
                var result = ship.IsHit(new Square(1, 5));
                Assert.AreEqual(HitResult.Hit, result);
            }

            {
                Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
                var result = ship.IsHit(new Square(1, 4));
                Assert.AreEqual(HitResult.Hit, result);
            }

            {
                Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
                var result = ship.IsHit(new Square(1, 6));
                Assert.AreEqual(HitResult.Hit, result);
            }

        }

        [TestMethod]
        public void HitMethodOnMissedSquareReturnsHitResultMissed()
        {
            {
                Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
                var result = ship.IsHit(new Square(2, 5));
                Assert.AreEqual(HitResult.Missed, result);
            }

           

        }

        [TestMethod]
        public void HitMethodOnLastSquareReturnsHitResultSunk()
        {
            {
                Ship ship = new Ship(new List<Square> { new Square(1, 4), new Square(1, 5), new Square(1, 6) });
                var result = ship.IsHit(new Square(1, 5));
                result = ship.IsHit(new Square(1, 4));
                result = ship.IsHit(new Square(1, 6));
                Assert.AreEqual(HitResult.Sunk, result);
            }



        }
    }
}
