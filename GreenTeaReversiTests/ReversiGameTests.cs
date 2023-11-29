using GreenTeaReversi;
using System.Text;

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
        public void GivenReversiGame_WhenPlacingDiskInInvalidLocationUnder_ThenReturnsInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(-1, -1));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "___WB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlacingDiskInInvalidLocationOver_ThenReturnsInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(8, 8));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "___WB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
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
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "___WB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGameWithIncompleteChain_WhenPlacingDiskForChain_ThenChainFlips()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(3, 2));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "__BBB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlaceDiskOverCurrentPlayerDisk_ThenInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(4, 3));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "___WB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlaceDiskOverOpponentDisk_ThenInvalidMove()
        {
            // Arrange
            var game = new ReversiGame();

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(4, 4));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "___WB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGame_WhenPlacingDiskOverExistingPieceAndCompleteChain_ThenInvalidMove()
        {// Arrange
            var layout = "________" +
                         "________" +
                         "____WB__" +
                         "__BBB___" +
                         "___BW___" +
                         "________" +
                         "________" +
                         "________";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(2, 4));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.InvalidMove, results.First());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "____WB__" +
                "__BBB___" +
                "___BW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiGameWithIncompleteChains_WhenPlacingDiskForMultipleChains_ThenChainFlips()
        {
            // Arrange
            var layout = "________" +
                         "________" +
                         "____WB__" +
                         "__BBB___" +
                         "___BW___" +
                         "________" +
                         "________" +
                         "________";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(4, 2));

            // Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "____WB__" +
                "__BWB___" +
                "__WWW___" +
                "________" +
                "________" +
                "________", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenBlackMovePreventsWhite_ThenBlackMovesAgain()
        {
            // Arrange
            var layout = "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "_______W" +
                         "______WB";
            var game = InitializeReversiGame(layout, PlayerColor.Black);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(5, 7));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.WhiteNoValidMoves, results.Last());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "________" +
                "________" +
                "_______B" +
                "_______B" +
                "______WB", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenWhiteMovePreventsBlack_ThenWhiteMovesAgain()
        {
            // Arrange
            var layout = "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "_______B" +
                         "______BW";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(5, 7));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.BlackNoValidMoves, results.Last());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "________" +
                "________" +
                "_______W" +
                "_______W" +
                "______BW", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenBlackMovesAndNoWhiteMovesRemaining_ThenGameOver()
        {
            // Arrange
            var layout = "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "_______W" +
                         "_______B";
            var game = InitializeReversiGame(layout, PlayerColor.Black);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(5, 7));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.GameOverBlackWins, results.Last());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "________" +
                "________" +
                "_______B" +
                "_______B" +
                "_______B", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenWhiteMovesAndNoBlackMovesRemaining_ThenGameOver()
        {
            // Arrange
            var layout = "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "________" +
                         "_______B" +
                         "_______W";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(5, 7));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.GameOverWhiteWins, results.Last());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "________" +
                "________" +
                "________" +
                "________" +
                "________" +
                "_______W" +
                "_______W" +
                "_______W", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenBlackMovesAndNoSpacesLeft_ThenGameOver()
        {
            // Arrange
            var layout = "_WBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB";
            var game = InitializeReversiGame(layout, PlayerColor.Black);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.GameOverBlackWins, results.Last());
            Assert.AreEqual(PlayerColor.Black, game.CurrentPlayerColor);
            AssertBoardLayout(
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenWhiteMovesAndSpacesRemaining_ThenGameOver()
        {
            // Arrange
            var layout = "_BWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW" +
                         "WWWWWWWW";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(0, 0));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.GameOverWhiteWins, results.Last());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW", game);
        }

        [TestMethod]
        public void GivenReversiBoard_WhenWhiteMovesAndEqualSpacesCovered_ThenGameOverTie()
        {
            // Arrange
            var layout = "WWWWWWWW" +
                         "WWWWWWWB" +
                         "WWWWWWW_" +
                         "WWWWWWWW" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB" +
                         "BBBBBBBB";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            // Act
            var results = game.PlaceCurrentPlayerDisk(new Coordinate(2, 7));

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(ActionResult.ValidMove, results.First());
            Assert.AreEqual(ActionResult.GameOverTie, results.Last());
            Assert.AreEqual(PlayerColor.White, game.CurrentPlayerColor);
            AssertBoardLayout(
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "WWWWWWWW" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB" +
                "BBBBBBBB", game);
        }

        [TestMethod]
        public void ConfirmInitializeBoardAndAssertBoardLayoutAreWorking()
        {
            var layout = "________" +
                         "________" +
                         "________" +
                         "__BBB___" +
                         "___BW___" +
                         "________" +
                         "________" +
                         "________";
            var game = InitializeReversiGame(layout, PlayerColor.White);

            AssertBoardLayout(layout, game);
        }

        private static ReversiGame InitializeReversiGame(string layout, PlayerColor startPlayer)
        {
            var boardSize = (int)Math.Sqrt(layout.Length);
            var board = new Board(boardSize);

            for(var i = 0; i < layout.Length; i++)
            {
                var row = i / boardSize;
                var column = i % boardSize;
                var coordinate = new Coordinate(row, column);

                var disk = layout[i];

                // Empty space
                if(disk == '_')
                {
                    continue;
                }

                var color = disk == 'W' ? PlayerColor.White : PlayerColor.Black;

                board.SetDisk(color, coordinate);
            }

            return new ReversiGame(board, startPlayer);
        }

        private static void AssertBoardLayout(string expected, ReversiGame actual)
        {
            var grid = actual.GetGrid();
            var actualString = new StringBuilder();

            foreach(var disk in grid)
            {
                var spaceString = disk switch
                {
                    null => "_",
                    PlayerColor.White => "W",
                    PlayerColor.Black => "B",
                    _ => throw new NotImplementedException($"Unknown disk {disk}")
                };

                actualString.Append(spaceString);
            }

            Assert.AreEqual(expected, actualString.ToString());
        }
    }
}
