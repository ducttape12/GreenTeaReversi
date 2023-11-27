namespace GreenTeaReversi
{
    public struct Coordinate(int row, int column)
    {
        public readonly int Row => row;
        public readonly int Column => column;
    }
}
