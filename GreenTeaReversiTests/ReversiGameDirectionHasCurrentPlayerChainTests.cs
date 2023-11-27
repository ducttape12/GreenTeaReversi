using GreenTeaReversi;

namespace GreenTeaReversiTests
{
    [TestClass]
    public class ReversiGameDirectionHasCurrentPlayerChainTests
    {
        private const int BoardSize = 8;

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithEast_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(3, 7),
                             rowDelta: 0,
                             columnDelta: 1);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithNorthEast_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(0, 7),
                             rowDelta: -1,
                             columnDelta: 1);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithNorth_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(0, 3),
                             rowDelta: -1,
                             columnDelta: 0);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithNorthWest_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(0, 0),
                             rowDelta: -1,
                             columnDelta: -1);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithWest_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(3, 0),
                             rowDelta: 0,
                             columnDelta: -1);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithSouthWest_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(7, 0),
                             rowDelta: 1,
                             columnDelta: -1);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithSouth_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(7, 3),
                             rowDelta: 1,
                             columnDelta: 0);
        }

        [TestMethod]
        public void GivenCurrentPlayerWhite_WhenCalledWithSouthEast_ThenReturnsExpectedValues()
        {
            TestAllScenarios(startCoordinate: new Coordinate(3, 3),
                             edgeCoordinate: new Coordinate(7, 7),
                             rowDelta: 1,
                             columnDelta: 1);
        }

        public void TestAllScenarios(Coordinate startCoordinate, Coordinate edgeCoordinate, int rowDelta, int columnDelta)
        {
            var playerColors = new List<PlayerColor>() { PlayerColor.White, PlayerColor.Black };

            foreach (var currentPlayer in playerColors)
            {
                GivenBoardForOpponentCurrent_WhenCalled_ThenReturnsTrue(currentPlayer, startCoordinate, rowDelta, columnDelta);
                GivenBoardForCurrent_WhenCalled_ThenReturnsFalse(currentPlayer, startCoordinate, rowDelta, columnDelta);
                GivenBoardForOpponent_WhenCalled_ThenReturnsFalse(currentPlayer, startCoordinate, rowDelta, columnDelta);
                GivenEmptyBoard_WhenCalled_ThenReturnsFalse(currentPlayer, startCoordinate, rowDelta, columnDelta);
                GivenPlayerAtEnd_WhenCalled_ThenReturnsFalse(currentPlayer, edgeCoordinate, rowDelta, columnDelta);
            }
        }

        public void GivenBoardForOpponentCurrent_WhenCalled_ThenReturnsTrue(PlayerColor currentPlayer,
                                                                            Coordinate startCoordinate, int rowDelta,
                                                                            int columnDelta)
        {
            // Arrange
            var board = new Board(BoardSize);
            board.SetDisk(OpponentColor(currentPlayer),
                          new Coordinate(startCoordinate.Row + rowDelta,
                                         startCoordinate.Column + columnDelta));
            board.SetDisk(currentPlayer,
                          new Coordinate(startCoordinate.Row + rowDelta + rowDelta,
                                         startCoordinate.Column + columnDelta + columnDelta));
            var game = new ReversiGame(board, currentPlayer);

            // Act
            var hasChain = game.DirectionHasCurrentPlayerChain(startCoordinate, rowDelta, columnDelta);

            // Assert
            Assert.IsTrue(hasChain);
        }

        public void GivenBoardForCurrent_WhenCalled_ThenReturnsFalse(PlayerColor currentPlayer,
                                                                     Coordinate startCoordinate, int rowDelta,
                                                                     int columnDelta)
        {
            // Arrange
            var board = new Board(BoardSize);
            board.SetDisk(currentPlayer,
                          new Coordinate(startCoordinate.Row + rowDelta, startCoordinate.Column + columnDelta));
            var game = new ReversiGame(board, currentPlayer);

            // Act
            var hasChain = game.DirectionHasCurrentPlayerChain(startCoordinate, rowDelta, columnDelta);

            // Assert
            Assert.IsFalse(hasChain);
        }

        public void GivenBoardForOpponent_WhenCalled_ThenReturnsFalse(PlayerColor currentPlayer,
                                                                      Coordinate startCoordinate, int rowDelta,
                                                                      int columnDelta)
        {
            // Arrange
            var board = new Board(BoardSize);
            board.SetDisk(OpponentColor(currentPlayer),
                          new Coordinate(startCoordinate.Row + rowDelta, startCoordinate.Column + columnDelta));
            var game = new ReversiGame(board, PlayerColor.White);

            // Act
            var hasChain = game.DirectionHasCurrentPlayerChain(startCoordinate, rowDelta, columnDelta);

            // Assert
            Assert.IsFalse(hasChain);
        }

        public void GivenEmptyBoard_WhenCalled_ThenReturnsFalse(PlayerColor currentPlayer, Coordinate startCoordinate,
                                                                int rowDelta, int columnDelta)
        {
            // Arrange
            var board = new Board(BoardSize);
            var game = new ReversiGame(board, currentPlayer);

            // Act
            var hasChain = game.DirectionHasCurrentPlayerChain(startCoordinate, rowDelta, columnDelta);

            // Assert
            Assert.IsFalse(hasChain);
        }

        public void GivenPlayerAtEnd_WhenCalled_ThenReturnsFalse(PlayerColor currentPlayer, Coordinate startCoordinate,
                                                                 int rowDelta, int columnDelta)
        {
            // Arrange
            var board = new Board(BoardSize);
            var game = new ReversiGame(board, currentPlayer);

            // Act
            var hasChain = game.DirectionHasCurrentPlayerChain(startCoordinate, rowDelta, columnDelta);

            // Assert
            Assert.IsFalse(hasChain);
        }

        private static PlayerColor OpponentColor(PlayerColor currentPlayer)
        {
            return currentPlayer == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }
    }
}