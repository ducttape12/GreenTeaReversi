using GreenTeaReversi;

namespace GreenTeaReversiAIBots
{
    internal class MoveResult(PlayerColor optimizeForColor,
                              Coordinate coordinate,
                              int whiteDiskCount,
                              int blackDiskCount,
                              ActionResult lastResult) : IComparable<MoveResult>
    {
        internal PlayerColor MyColor => optimizeForColor;
        internal Coordinate Coordinate => coordinate;
        internal int WhiteDiskCount => whiteDiskCount;
        internal int BlackDiskCount => blackDiskCount;
        internal ActionResult LastResult => lastResult;

        internal ActionResult WinIdentifier => MyColor == PlayerColor.White ?
            ActionResult.GameOverWhiteWins : ActionResult.GameOverBlackWins;
        internal ActionResult LoseIdentifier => MyColor == PlayerColor.White ?
            ActionResult.GameOverBlackWins : ActionResult.GameOverWhiteWins;
        internal ActionResult OpponentSkipMove => MyColor == PlayerColor.White ?
            ActionResult.BlackNoValidMoves : ActionResult.WhiteNoValidMoves;
        internal ActionResult MySkipMove => MyColor == PlayerColor.White ?
            ActionResult.WhiteNoValidMoves : ActionResult.BlackNoValidMoves;
        internal int MyDiskCount => MyColor == PlayerColor.White ?
            WhiteDiskCount : BlackDiskCount;
        internal int OpponentDiskCount => MyColor == PlayerColor.White ?
            WhiteDiskCount : BlackDiskCount;
        internal int MyDiskCountDelta => MyDiskCount - OpponentDiskCount;

        public int CompareTo(MoveResult? other)
        {
            if(other == null)
            {
                return -1;
            }

            // Prefer options in the following order

            // Prefer winning
            if(LastResult == WinIdentifier && other.LastResult != WinIdentifier)
            {
                return -1;
            }
            else if(LastResult == WinIdentifier && other.LastResult == WinIdentifier)
            {
                return 0;
            }
            else if(LastResult != WinIdentifier && other.LastResult == WinIdentifier)
            {
                return 1;
            }

            // Prefer not losing
            if(LastResult == LoseIdentifier && other.LastResult != LoseIdentifier)
            {
                return 1;
            }
            else if (LastResult == LoseIdentifier && other.LastResult == LoseIdentifier)
            {
                return 0;
            }
            else if (LastResult != LoseIdentifier && other.LastResult == LoseIdentifier)
            {
                return -1;
            }
            
            // Prefer not to tie
            if(LastResult == ActionResult.GameOverTie && other.LastResult != ActionResult.GameOverTie)
            {
                return 1;
            }
            else if (LastResult == ActionResult.GameOverTie && other.LastResult == ActionResult.GameOverTie)
            {
                return 0;
            }
            else if (LastResult != ActionResult.GameOverTie && other.LastResult == ActionResult.GameOverTie)
            {
                return -1;
            }

            // Prefer making the opponent skip a move
            if(LastResult == OpponentSkipMove && other.LastResult != OpponentSkipMove)
            {
                return -1;
            }
            else if (LastResult == OpponentSkipMove && other.LastResult == OpponentSkipMove)
            {
                return 0;
            }
            else if (LastResult != OpponentSkipMove && other.LastResult == OpponentSkipMove)
            {
                return 1;
            }

            // Prefer the move that gives me the most number of colored disks
            return MyDiskCount.CompareTo(other.MyDiskCount);
        }

        public static bool operator <(MoveResult left, MoveResult right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(MoveResult left, MoveResult right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(MoveResult left, MoveResult right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(MoveResult left, MoveResult right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
