using GreenTeaReversi;

internal class Program
{
    private static void Main(string[] args)
    {
        var game = new ReversiGame();

        do
        {
            DisplayBoard(game);

            Console.Write($"{game.CurrentPlayerColor}, enter your move (e.g. 'A1'): ");
            var moveInput = Console.ReadLine();

            var move = ParseCoordinate(moveInput);

            if(!move.HasValue)
            {
                continue;
            }

            var results = game.PlaceCurrentPlayerDisk(move.Value);

            foreach(var result in results)
            {
                Console.WriteLine(result);
            }

        } while (true);
    }

    private static void DisplayBoard(ReversiGame game)
    {
        var grid = game.GetGrid();
        var possibleMoves = game.GetValidMovesForCurrentPlayer();

        Console.WriteLine(" ABCDEFGH");
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            Console.Write($"{row + 1}");

            for (var column = 0; column < grid.GetLength(1); column++)
            {
                var coordinate = new Coordinate(row, column);

                var validMove = possibleMoves.Contains(coordinate);

                var disk = grid[row, column];
                var display = disk switch
                {
                    PlayerColor.Black => "B",
                    PlayerColor.White => "W",
                    null => validMove ? "." : "_",
                    _ => throw new NotImplementedException($"Unknown PlayerColor {disk}")
                }; ;
                Console.Write(display);
            }

            Console.WriteLine();
        }
    }

    private static Coordinate? ParseCoordinate(string? source)
    {
        if(source == null)
        {
            return null;
        }

        var cleaned = source.Trim().ToUpperInvariant();

        if(cleaned.Length != 2)
        {
            return null;
        }

        var column = cleaned[0];
        var row = cleaned[1];

        var columnIndex = -1;
        switch(column)
        {
            case 'A':
                columnIndex = 0;
                break;
            case 'B':
                columnIndex = 1;
                break;
            case 'C':
                columnIndex = 2;
                break;
            case 'D':
                columnIndex = 3;
                break;
            case 'E':
                columnIndex = 4;
                break;
            case 'F':
                columnIndex = 5;
                break;
            case 'G':
                columnIndex = 6;
                break;
            case 'H':
                columnIndex = 7;
                break;
            default:
                return null;
        }

        if(!int.TryParse(row.ToString(), out var rowIndex))
        {
            return null;
        }

        return new Coordinate(rowIndex - 1, columnIndex);
    }
}