namespace GreenTeaReversi
{
    public class ReversiGame
    {
        private const int DefaultBoardSize = 8;
        private readonly Board board = new(DefaultBoardSize);
        public PlayerColor CurrentPlayerColor { get; private set; }
        public PlayerColor OpponentColor => CurrentPlayerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        public int WhiteDiskCount => board.WhiteDiskCount;
        public int BlackDiskCount => board.BlackDiskCount;
        public int FreeSquareCount => board.FreeSquaresCount;
        public bool GameInProgress { get; private set; } = true;

        public ReversiGame()
        {
            InitializeBoard();
        }

        public ReversiGame(ReversiGame original)
        {
            board = new Board(original.board);
            CurrentPlayerColor = original.CurrentPlayerColor;
            GameInProgress = original.GameInProgress;
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

        public ISet<Coordinate> GetValidMovesForCurrentPlayer()
        {
            var possibleMoves = new HashSet<Coordinate>(board.SquareCount);

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

                    if (IsCurrentPlayerValidMove(currentCoordinate))
                    {
                        possibleMoves.Add(currentCoordinate);
                    }
                }
            }

            return possibleMoves;
        }

        private bool IsCurrentPlayerValidMove(Coordinate coordinate)
        {
            if(coordinate.Row < 0 || coordinate.Row >= board.RowLength ||
                coordinate.Column < 0 || coordinate.Column >= board.ColumnLength)
            {
                return false;
            }

            var colorAtCurrentLocation = board.GetPlayerColor(coordinate);

            if (colorAtCurrentLocation.HasValue)
            {
                return false;
            }

            // Check all directions to see if placing a piece here makes a valid chain
            foreach (var direction in Directions.AllDirections)
            {
                if (DirectionHasCurrentPlayerChain(coordinate, direction))
                {
                    return true;
                }
            }

            return false;
        }

        public IList<ActionResult> PlaceCurrentPlayerDisk(Coordinate coordinate)
        {
            var results = new List<ActionResult>();
            var chainFlipped = false;

            if (!IsCurrentPlayerValidMove(coordinate))
            {
                return new List<ActionResult>() { ActionResult.InvalidMove };
            }

            // If possible, flip chains
            foreach (var direction in Directions.AllDirections)
            {
                chainFlipped |= FlipOpponentDisks(coordinate, direction);
            }

            // If no chains flipped, then this was an invalid move
            if (!chainFlipped)
            {
                return new List<ActionResult>() { ActionResult.InvalidMove };
            }

            // Switch to the other player
            results.Add(ActionResult.ValidMove);
            CurrentPlayerColor = OpponentColor;

            // If there are no valid moves for the current player, then switch back to the previous player
            if (!GetValidMovesForCurrentPlayer().Any())
            {
                var possibleActionResult = CurrentPlayerColor == PlayerColor.White ? ActionResult.WhiteNoValidMoves : ActionResult.BlackNoValidMoves;

                CurrentPlayerColor = OpponentColor;

                // If there are valid moves for the first player, then just mark it as such
                if (GetValidMovesForCurrentPlayer().Any())
                {
                    results.Add(possibleActionResult);
                }
                // Otherwise it's game over
                else
                {
                    if (board.WhiteDiskCount > board.BlackDiskCount)
                    {
                        results.Add(ActionResult.GameOverWhiteWins);
                        GameInProgress = false;
                    }
                    else if (board.BlackDiskCount > board.WhiteDiskCount)
                    {
                        results.Add(ActionResult.GameOverBlackWins);
                        GameInProgress = false;
                    }
                    else
                    {
                        results.Add(ActionResult.GameOverTie);
                        GameInProgress = false;
                    }
                }
            }

            return results;
        }

        public bool DirectionHasCurrentPlayerChain(Coordinate startCoordinate, Direction direction)
        {
            var opponentColorCount = 0;

            var inspectRow = startCoordinate.Row + direction.RowDelta;
            var inspectColumn = startCoordinate.Column + direction.ColumnDelta;

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

                inspectRow += direction.RowDelta;
                inspectColumn += direction.ColumnDelta;
            }

            // Scenario 5
            return false;
        }

        private bool FlipOpponentDisks(Coordinate startCoordinate, Direction direction)
        {
            if (!DirectionHasCurrentPlayerChain(startCoordinate, direction))
            {
                return false;
            }

            board.SetDisk(CurrentPlayerColor, startCoordinate);

            var inspectRow = startCoordinate.Row + direction.RowDelta;
            var inspectColumn = startCoordinate.Column + direction.ColumnDelta;

            while (inspectRow >= 0 && inspectRow < board.RowLength &&
                inspectColumn >= 0 && inspectColumn < board.ColumnLength)
            {
                var inspectCoordinate = new Coordinate(inspectRow, inspectColumn);

                var colorAtInspectCoordinate = board.GetPlayerColor(inspectCoordinate);

                if (colorAtInspectCoordinate == CurrentPlayerColor)
                {
                    break;
                }

                board.SetDisk(CurrentPlayerColor, inspectCoordinate);

                inspectRow += direction.RowDelta;
                inspectColumn += direction.ColumnDelta;
            }

            return true;
        }

        public PlayerColor?[,] GetGrid()
        {
            return board.GetGrid();
        }
    }
}
