using System.Collections.Immutable;

namespace GreenTeaReversi
{
    public static class Directions
    {
        public static Direction East { get; } = new(0, 1);
        public static Direction Northeast { get; } = new (-1, 1);
        public static Direction North { get; } = new(-1, 0);
        public static Direction Northwest { get; } = new(-1, -1);
        public static Direction West { get; } = new(0, -1);
        public static Direction Southwest { get; } = new(1, -1);
        public static Direction South { get; } = new(1, 0);
        public static Direction Southeast { get; } = new(1, 1);

        public static IList<Direction> AllDirections { get; } = ImmutableList.Create(East,
                                                                                     Northeast,
                                                                                     North,
                                                                                     Northwest,
                                                                                     West,
                                                                                     Southwest,
                                                                                     South,
                                                                                     Southeast);
    }
}
