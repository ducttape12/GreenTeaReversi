using GreenTeaReversi;

namespace GreenTeaReversiTests
{
    [TestClass]
    public class ReversiGameTests
    {
        [TestMethod]
        public void GivenNewReversiGame_WhenValidMovesForCurrentPlayerCalled_ThenReturnsExpectedMoves()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var validMoves = game.GetValidMovesForCurrentPlayer();

            // Assert
            Assert.AreEqual(4, validMoves.Count);
            Assert.IsTrue(validMoves.Contains(new Coordinate(2, 3)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(3, 2)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(4, 5)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(5, 4)));
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlacingDiskInInvalidLocation_ThenReturnsInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(-1, -1));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlacingDiskInNoChainLocation_ThenReturnsInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
        }
    }
}
