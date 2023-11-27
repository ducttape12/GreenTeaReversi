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

        //public IList<Coordinate> MovesForCurrentPlayer()
        //{
        //    var possibleMoves = new List<Coordinate>(board.SquareCount);

        //    for (byte row = 0; row < board.RowLength; row++)
        //    {
        //        for (byte column = 0; column < board.ColumnLength; column++)
        //        {
        //            var currentCoordinate = new Coordinate(row, column);

        //            var colorAtCoordinate = board.GetPlayerColor(currentCoordinate);

        //            // Only consider coordinates without pieces
        //            if (colorAtCoordinate != null)
        //            {
        //                continue;
        //            }

        //            // Start walking in direction.  If first step is other player, countine.  Keep walking.  If find player then
        //            // true otherwise if run out of runway or null then false.
        //            // CheckForLine(startColumn, startRow, columnDelta, rowDelta);
        //        }
        //    }

        //}

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



        // 1. All moves that touch another piece of opponent color
        // 2. Keep going in that direction until the player color is found
        // 3. If found, then a valid move and move on
        // 4. if not found, go back to 1.
        // 5. If all directions exhausted in 1, then no move found.  Move on to next empty square.
    }
}
