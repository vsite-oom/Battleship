using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests;

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
