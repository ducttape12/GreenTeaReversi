namespace GreenTeaReversi
{
    public class Board
    {
        private readonly PlayerColor?[,] grid;

        public int ColumnLength => grid.GetLength(1);
        public int RowLength => grid.GetLength(0);
        public int SquareCount => grid.Length;

        public int BlackDiskCount { get; private set; } = 0;
        public int WhiteDiskCount { get; private set; } = 0;
        public int FreeSquaresCount => SquareCount - BlackDiskCount - WhiteDiskCount;


        public Board(int size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            grid = new PlayerColor?[size, size];
        }

        public Board(Board original)
        {
            grid = original.GetGrid();

            foreach(var square in grid)
            {
                if(square.HasValue)
                {
                    if(square.Value == PlayerColor.White)
                    {
                        WhiteDiskCount++;
                    }
                    else
                    {
                        BlackDiskCount++;
                    }
                }
            }
        }

        public PlayerColor?[,] GetGrid()
        {
            var newGrid = new PlayerColor?[grid.GetLength(0), grid.GetLength(1)];
            Array.Copy(grid, newGrid, grid.Length);
            return newGrid;
        }

        public void SetDisk(PlayerColor diskColor, Coordinate coordinate)
        {
            ValidateRowAndColumn(coordinate);

            var colorAtCoordinate = grid[coordinate.Row, coordinate.Column];

            // Instead of recalculating these counts on demand, update them here for performance purposes
            if(colorAtCoordinate.HasValue)
            {
                if(colorAtCoordinate.Value == PlayerColor.White)
                {
                    WhiteDiskCount--;
                }
                else
                {
                    BlackDiskCount--;
                }
            }

            grid[coordinate.Row, coordinate.Column] = diskColor;

            if(diskColor == PlayerColor.White)
            {
                WhiteDiskCount++;
            }
            else
            {
                BlackDiskCount++;
            }
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
