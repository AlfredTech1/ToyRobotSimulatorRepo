using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ToyRobotSimulator.BusinessLayer.Tests
{
    [TestFixture]
    public class TableSurfaceTest
    {
        /// <summary>
        /// Test valid location 
        /// </summary>
        [Test]
        public void IsValidLocation_ValidTableSurfaceLocation_True()
        {
            // arrange
            TableSurface ts = new TableSurface(5, 5);
            Location location = new Location(1, 4, Location.Direction.NORTH);

            // act
            var result = ts.IsValidLocation(location);

            // assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Put the toy robot outside of the table surface
        /// </summary>
        [Test]
        public void IsValidLocation_InvalidTableSurfaceLocation_False()
        {
            // arrange
            TableSurface ts = new TableSurface(5, 5);
            Location location = new Location(10, 10, Location.Direction.NORTH);

            // act
            var result = ts.IsValidLocation(location);

            // assert
            Assert.IsFalse(result);
        }
    }
}
