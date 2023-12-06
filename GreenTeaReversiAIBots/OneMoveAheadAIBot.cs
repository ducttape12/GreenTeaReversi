using GreenTeaReversi;

namespace GreenTeaReversiAIBots
{
    public class OneMoveAheadAIBot : IAIBot
    {
        public Coordinate GetMove(ReversiGame game)
        {
            var possibleMoves = game.GetValidMovesForCurrentPlayer();
            var moveResults = new List<MoveResult>(possibleMoves.Count);

            foreach(var coordinate in possibleMoves)
            {
                var gameCopy = new ReversiGame(game);

                var results = gameCopy.PlaceCurrentPlayerDisk(coordinate);

                moveResults.Add(new MoveResult(optimizeForColor: game.CurrentPlayerColor,
                                               coordinate: coordinate,
                                               whiteDiskCount: gameCopy.WhiteDiskCount,
                                               blackDiskCount: gameCopy.BlackDiskCount,
                                               lastResult: results.Last()));
            }

            moveResults.Sort();

            return moveResults.First().Coordinate;
        }
    }
}
