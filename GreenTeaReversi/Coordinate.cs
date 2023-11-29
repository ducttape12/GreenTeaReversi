using System.Diagnostics.CodeAnalysis;

namespace GreenTeaReversi
{
    public struct Coordinate(int row, int column)
    {
        public readonly int Row => row;
        public readonly int Column => column;

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            if(obj is not Coordinate)
            {
                return false;
            }

            var comparison = (Coordinate)obj;

            return Row == comparison.Row &&
                Column == comparison.Column;
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }
    }
}
