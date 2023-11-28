namespace GreenTeaReversi
{
    public class ReversiGame
    {
        private const int DefaultBoardSize = 8;
        private readonly Board board = new(DefaultBoardSize);
        public PlayerColor CurrentPlayerColor { get; private set; }
        public PlayerColor OpponentColor => CurrentPlayerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;

        public ReversiGame()
        {
            InitializeBoard();
        }

        public ReversiGame(Board board, PlayerColor currentPlayer)
        {
            this.board = board;
            CurrentPlayerColor = currentPlayer;
        }

        private void InitializeBoard()
        {
            board.SetDisk(PlayerColor.White, new Coordinate(3, 3));
            board.SetDisk(PlayerColor.Black, new Coordinate(3, 4));
            board.SetDisk(PlayerColor.Black, new Coordinate(4, 3));
            board.SetDisk(PlayerColor.White, new Coordinate(4, 4));

            CurrentPlayerColor = PlayerColor.Black;
        }

        public IList<Coordinate> ValidMovesForCurrentPlayer()
        {
            var possibleMoves = new List<Coordinate>(board.SquareCount);

            // Check all squares to determine if it's a valid move
            for (var row = 0; row < board.RowLength; row++)
            {
                for (var column = 0; column < board.ColumnLength; column++)
                {
                    var currentCoordinate = new Coordinate(row, column);

                    var colorAtCoordinate = board.GetPlayerColor(currentCoordinate);

                    // Only consider coordinates without pieces
                    if (colorAtCoordinate != null)
                    {
                        continue;
                    }

                    // Check all directions to see if placing a piece here makes a valid chain
                    var directions = new List<(int, int)>()
                    {
                        (0, 1),   // East
                        (-1, 1),  // Northeast
                        (-1, 0),  // North
                        (-1, -1), // Northwest
                        (0, -1),  // West
                        (1, -1),  // Southwest
                        (1, 0),   // South
                        (1, 1)    // Southeast
                    };

                    foreach (var direction in directions)
                    {
                        (var rowDelta, var columnDelta) = direction;

                        if (DirectionHasCurrentPlayerChain(currentCoordinate, rowDelta, columnDelta))
                        {
                            possibleMoves.Add(currentCoordinate);
                            break;
                        }
                    }
                }
            }

            return possibleMoves;
        }

        public bool DirectionHasCurrentPlayerChain(Coordinate startCoordinate, int rowDelta, int columnDelta)
        {
            var opponentColorCount = 0;

            var inspectRow = startCoordinate.Row + rowDelta;
            var inspectColumn = startCoordinate.Column + columnDelta;

            // Scenarios to handle (assuming current player white and walking east, # is start coordinate):
            // 1. #BW| => True
            // 2. #W_| => False
            // 3. #B_| => False
            // 4. #__| => False
            // 5. __#| => False

            while (inspectRow >= 0 && inspectRow < board.RowLength &&
                inspectColumn >= 0 && inspectColumn < board.ColumnLength)
            {
                var inspectCoordinate = new Coordinate(inspectRow, inspectColumn);

                var colorAtInspectCoordinate = board.GetPlayerColor(inspectCoordinate);

                // Scenario 3 or 4
                if (!colorAtInspectCoordinate.HasValue)
                {
                    return false;
                }
                // Scenario 1 or 2
                else if (colorAtInspectCoordinate.Value == CurrentPlayerColor)
                {
                    return opponentColorCount > 0;
                }
                // Counting opponent color; unsure of scenario at this point
                else if (colorAtInspectCoordinate.Value == OpponentColor)
                {
                    opponentColorCount++;
                }

                inspectRow += rowDelta;
                inspectColumn += columnDelta;
            }

            // Scenario 5
            return false;
        }
    }
}
