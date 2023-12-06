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

            // Winning is better
            var winComparison = CompareLastResultPreferred(WinIdentifier, other.LastResult);
            if(winComparison.HasValue)
            {
                return winComparison.Value;
            }

            // Not Losing is Better
            var loseComparison = CompareLastResultNotPreferred(LoseIdentifier, other.LastResult);
            if(loseComparison.HasValue)
            {
                return loseComparison.Value;
            }

            // Not Tieing is better
            var tieComparison = CompareLastResultNotPreferred(ActionResult.GameOverTie, other.LastResult);
            if(tieComparison.HasValue)
            {
                return tieComparison.Value;
            }

            // Opponent skipping their next move is better
            var opponentSkipComparison = CompareLastResultPreferred(OpponentSkipMove, other.LastResult);
            if(opponentSkipComparison.HasValue)
            {
                return opponentSkipComparison.Value;
            }

            // Move that gives the most disks is better
            if(MyDiskCount > other.MyDiskCount)
            {
                return -1;
            }
            else if (MyDiskCount < other.MyDiskCount)
            {
                return 1;
            }

            return 0;
        }

        private int? CompareLastResultPreferred(ActionResult preferredResult, ActionResult compareToResult)
        {
            if (LastResult == preferredResult && compareToResult != preferredResult)
            {
                return -1;
            }
            else if (LastResult == preferredResult && compareToResult == preferredResult)
            {
                return 0;
            }
            else if (LastResult != preferredResult && compareToResult == preferredResult)
            {
                return 1;
            }

            return null;
        }

        private int? CompareLastResultNotPreferred(ActionResult notPreferredResult, ActionResult compareToResult)
        {
            if (LastResult == notPreferredResult && compareToResult != notPreferredResult)
            {
                return -1;
            }
            else if (LastResult == notPreferredResult && compareToResult == notPreferredResult)
            {
                return 0;
            }
            else if (LastResult != notPreferredResult && compareToResult == notPreferredResult)
            {
                return 1;
            }

            return null;
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
