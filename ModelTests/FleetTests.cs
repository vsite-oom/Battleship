using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Vsite.Oom.Battleship.Model.Tests
{
    [TestClass]
    public class FleetTests
    {
        [TestMethod]
        public void ConstructorCreatesEmptyFleet()
        {
            var fleet = new Fleet();

            Assert.AreEqual(0, fleet.Ships.Count());
        }
        [TestMethod]
        public void CreateShipAddsNewShipToFleet()
        {
            var fleet = new Fleet();
            var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            fleet.CreateShip(squares);
            Assert.AreEqual(1, fleet.Ships.Count());
        }
        [TestMethod]
        public void HitMethodReturnsMissedForSquareNotInAnyShip()
        {
            var fleet = new Fleet();
            var squares = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            fleet.CreateShip(squares);
            
            
        }
        [TestMethod]
        public void HitMethodReturnsSunkenAfterLastSquareInFirstShipIsHit()
        {
            var fleet = CreateFleet();
           
            Assert.AreEqual(HitResult.Hit, fleet.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, fleet.Hit(1, 4));
            Assert.AreEqual(HitResult.Sunken, fleet.Hit(1, 5));


            
            
        }[TestMethod]
        public void HitMethodReturnsSunkenAfterLastSquareInSecondShipIsHit()
        {
            var fleet = CreateFleet();
           
            Assert.AreEqual(HitResult.Hit, fleet.Hit(1, 3));
            Assert.AreEqual(HitResult.Hit, fleet.Hit(1, 4));
            Assert.AreEqual(HitResult.Sunken, fleet.Hit(1, 5));

            Assert.AreEqual(HitResult.Hit, fleet.Hit(8, 5));
            Assert.AreEqual(HitResult.Sunken, fleet.Hit(8, 4));


            
            
        }


        private Fleet CreateFleet()
        {
            var fleet = new Fleet();
            var squares1 = new List<Square> { new Square(1, 3), new Square(1, 4), new Square(1, 5) };
            fleet.CreateShip(squares1);
            var squares2 = new List<Square> { new Square(8, 4), new Square(8, 5) };
            fleet.CreateShip(squares2);
            return fleet;
        }
    }
}