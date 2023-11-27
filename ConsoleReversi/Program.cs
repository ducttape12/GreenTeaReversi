using GreenTeaReversi;

var board = new Board(8);

board.SetDisk(PlayerColor.White, new Coordinate(3, 3));
board.SetDisk(PlayerColor.Black, new Coordinate(3, 4));
board.SetDisk(PlayerColor.Black, new Coordinate(4, 3));
board.SetDisk(PlayerColor.White, new Coordinate(4, 4));

var grid = board.GetGrid();

Console.WriteLine(" ABCDEFGH");
for (var columnIndex = 0; columnIndex < grid.GetLength(1); columnIndex++)
{
    Console.Write($"{columnIndex + 1}");

    for (var rowIndex = 0; rowIndex < grid.GetLength(0); rowIndex++)
    {
        var disk = grid[rowIndex, columnIndex];
        var display = disk switch
        {
            PlayerColor.Black => "B",
            PlayerColor.White => "W",
            null => "_"
        };
        Console.Write(display);
    }

    Console.WriteLine();
}