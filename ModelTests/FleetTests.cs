using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vste.oom.battleship.model.tests
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
	}
}