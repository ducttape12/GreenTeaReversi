namespace GreenTeaReversi
{
    public class Board
    {
        private readonly PlayerColor?[,] grid;

        public int ColumnLength => grid.GetLength(1);
        public int RowLength => grid.GetLength(0);
        public int SquareCount => grid.Length;

        public Board(int size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            grid = new PlayerColor?[size, size];
        }

        public PlayerColor?[,] GetGrid()
        {
            var newGrid = new PlayerColor?[grid.GetLength(0), grid.GetLength(1)];
            Array.Copy(grid, newGrid, grid.Length);
            return newGrid;
        }

        public void SetDisk(PlayerColor playerColor, Coordinate coordinate)
        {
            ValidateRowAndColumn(coordinate);

            grid[coordinate.Row, coordinate.Column] = playerColor;
        }

        public PlayerColor? GetPlayerColor(Coordinate coordinate)
        {
            ValidateRowAndColumn(coordinate);

            return grid[coordinate.Row, coordinate.Column];
        }

        private void ValidateRowAndColumn(Coordinate coordinate)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(coordinate.Row, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(coordinate.Row, RowLength);
            ArgumentOutOfRangeException.ThrowIfLessThan(coordinate.Column, 0);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(coordinate.Column, ColumnLength);
        }
    }
}
