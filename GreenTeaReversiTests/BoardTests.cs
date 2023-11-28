using GreenTeaReversi;

namespace GreenTeaReversiTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void GivenEmptyBoard_WhenWhiteDiskPlaced_ThenWhiteDiskCountIncremented()
        {
            // Arrange
            var board = new Board(8);

            // Act
            board.SetDisk(PlayerColor.White, new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(1, board.WhiteDiskCount);
            Assert.AreEqual(0, board.BlackDiskCount);
        }

        [TestMethod]
        public void GivenEmptyBoard_WhenBlackDiskPlaced_ThenBlackDiskCountIncremented()
        {
            // Arrange
            var board = new Board(8);

            // Act
            board.SetDisk(PlayerColor.Black, new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(0, board.WhiteDiskCount);
            Assert.AreEqual(1, board.BlackDiskCount);
        }

        [TestMethod]
        public void GivenBoardWithWhiteSquare_WhenBlackDiskReplaces_ThenWhiteAndBlackCountsCorrect()
        {
            // Arrange
            var board = new Board(8);
            board.SetDisk(PlayerColor.White, new Coordinate(0, 0));

            // Act
            board.SetDisk(PlayerColor.Black, new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(0, board.WhiteDiskCount);
            Assert.AreEqual(1, board.BlackDiskCount);
        }

        [TestMethod]
        public void GivenBoardWithBlackSquare_WhenWhiteDiskReplaces_ThenWhiteAndBlackCountsCorrect()
        {
            // Arrange
            var board = new Board(8);
            board.SetDisk(PlayerColor.Black, new Coordinate(0, 0));

            // Act
            board.SetDisk(PlayerColor.White, new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(1, board.WhiteDiskCount);
            Assert.AreEqual(0, board.BlackDiskCount);
        }

        [TestMethod]
        public void GivenEmptyBoard_WhenWhiteAndBlackDisksPlaced_ThenFreeSquareCountIsCorrect()
        {
            // Arrange
            var board = new Board(8);
            board.SetDisk(PlayerColor.Black, new Coordinate(0, 0));
            board.SetDisk(PlayerColor.White, new Coordinate(0, 1));

            // Act
            board.SetDisk(PlayerColor.Black, new Coordinate(0, 0));
            board.SetDisk(PlayerColor.White, new Coordinate(0, 1));

            // Assert
            Assert.AreEqual(62, board.FreeSquaresCount);
        }
    }
}
