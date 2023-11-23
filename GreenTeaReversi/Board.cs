namespace GreenTeaReversi
{
    public class Board
    {
        private readonly Disk?[,] grid = new Disk?[8, 8];

        public Board(int size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            grid = new Disk?[size, size];
        }

        public Disk?[,] GetGrid()
        {
            var newGrid = new Disk?[grid.GetLength(0), grid.GetLength(1)];
            Array.Copy(grid, newGrid, grid.Length);
            return newGrid;
        }

        public void SetDisk(Disk disk, Row row, Column column)
        {
            var rowIndex = RowToIndex(row);
            var columnIndex = ColumnToIndex(column);

            grid[rowIndex, columnIndex] = disk;
        }

        private static int RowToIndex(Row row)
        {
            return row switch
            {
                Row.A => 0,
                Row.B => 1,
                Row.C => 2,
                Row.D => 3,
                Row.E => 4,
                Row.F => 5,
                Row.G => 6,
                Row.H => 7,
                _ => throw new NotImplementedException($"Unknown Row {row}")
            };
        }

        private static int ColumnToIndex(Column column)
        {
            return column switch
            {
                Column.One => 0,
                Column.Two => 1,
                Column.Three => 2,
                Column.Four => 3,
                Column.Five => 4,
                Column.Six => 5,
                Column.Seven => 6,
                Column.Eight => 7,
                _ => throw new NotImplementedException($"Unknown Column {column}")
            };
        }
    }
}
