namespace GreenTeaReversi
{
    public readonly struct Direction(int rowDelta, int columnDelta)
    {
        public int RowDelta => rowDelta;
        public int ColumnDelta => columnDelta;
    }
}
