using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vste.oom.battleship.model.tests
{
	[TestClass]
	public class FleetBuilderTests
	{
		[TestMethod]
		public void CreateFleetBuildsFleetWithNumberOfShips()
		{
			int rows = 10;
			int columns = 10;
			int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];

			var builder = new FleetBuilder(rows, columns, shipLengths);
			var fleet= builder.CreateFleet();

			Assert.AreEqual(10, fleet.Ships.Count());
		}
		
		[TestMethod]
		public void CreateFleetBuildsFleetWithShipsOfLengthProvidedFor2()
		{
			int rows = 10;
			int columns = 10;
			int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];

			var builder = new FleetBuilder(rows, columns, shipLengths);
			var fleet= builder.CreateFleet();

			Assert.AreEqual(4, fleet.Ships.Count(s => s.Squares.Count() == 2));
		}

		[TestMethod]
		public void CreateFleetBuildsFleetWithShipsOfLengthProvidedFor3()
		{
			int rows = 10;
			int columns = 10;
			int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];

			var builder = new FleetBuilder(rows, columns, shipLengths);
			var fleet = builder.CreateFleet();

			Assert.AreEqual(3, fleet.Ships.Count(s => s.Squares.Count() == 3));
		}

		[TestMethod]
		public void CreateFleetBuildsFleetWithShipsOfLengthProvidedFor4()
		{
			int rows = 10;
			int columns = 10;
			int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];

			var builder = new FleetBuilder(rows, columns, shipLengths);
			var fleet = builder.CreateFleet();

			Assert.AreEqual(2, fleet.Ships.Count(s => s.Squares.Count() == 4));
		}
		
		[TestMethod]
		public void CreateFleetBuildsFleetWithShipsOfLengthProvidedFor5()
		{
			int rows = 10;
			int columns = 10;
			int[] shipLengths = [2, 2, 2, 2, 3, 3, 3, 4, 4, 5];

			var builder = new FleetBuilder(rows, columns, shipLengths);
			var fleet = builder.CreateFleet();

			Assert.AreEqual(1, fleet.Ships.Count(s => s.Squares.Count() == 5));
		}


	}
}