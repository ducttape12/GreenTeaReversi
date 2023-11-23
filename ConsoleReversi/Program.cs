using GreenTeaReversi;

var board = new Board(8);

board.SetDisk(Disk.White, Row.D, Column.Four);
board.SetDisk(Disk.Black, Row.E, Column.Four);
board.SetDisk(Disk.Black, Row.D, Column.Five);
board.SetDisk(Disk.White, Row.E, Column.Five);

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
            Disk.Black => "B",
            Disk.White => "W",
            null => "_"
        };
        Console.Write(display);
    }

    Console.WriteLine();
}