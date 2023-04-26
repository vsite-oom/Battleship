using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ModelTests
{
    [TestClass]
    public class GunneryTests
    {
        [TestMethod]
        public void CurrentShootingTacticsIsInitiallyRandom()
        {
            var gunnery = new Gunnery(new GameRules(), new Fleet());
            Assert.AreEqual(CurrentShootingTactics.Random, gunnery.currentShootingTactics);
        }
    }
}