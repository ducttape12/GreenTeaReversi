using GreenTeaReversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenTeaReversiTests
{
    [TestClass]
    public class CoordinateTests
    {
        [TestMethod]
        public void GivenCoordinate_WhenEqualsCalledWithNull_ThenReturnsFalse()
        {
            var source = new Coordinate(0, 0);
            Coordinate? comparison = null;

            var isEqual = source.Equals(comparison);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void GivenCoordinate_WhenEqualsCalledWithOnlyRowEqual_ThenReturnsFalse()
        {
            var source = new Coordinate(0, 0);
            var comparison = new Coordinate(0, 1);

            var isEqual = source.Equals(comparison);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void GivenCoordinate_WhenEqualsCalledWithOnlyColumnEqual_ThenReturnsFalse()
        {
            var source = new Coordinate(0, 0);
            var comparison = new Coordinate(1, 0);

            var isEqual = source.Equals(comparison);

            Assert.IsFalse(isEqual);
        }

        [TestMethod]
        public void GivenCoordinate_WhenEqualsCalledWithRowAndColumnEqual_ThenReturnsTrue()
        {
            var source = new Coordinate(0, 0);
            var comparison = new Coordinate(0, 0);

            var isEqual = source.Equals(comparison);

            Assert.IsTrue(isEqual);
        }
    }
}
