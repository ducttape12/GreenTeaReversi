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
            var validMoves = game.ValidMovesForCurrentPlayer();

            // Assert
            Assert.AreEqual(4, validMoves.Count);
            Assert.IsTrue(validMoves.Contains(new Coordinate(2, 3)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(3, 2)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(4, 5)));
            Assert.IsTrue(validMoves.Contains(new Coordinate(5, 4)));
        }
    }
}
