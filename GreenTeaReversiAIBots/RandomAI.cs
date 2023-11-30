using GreenTeaReversi;

namespace GreenTeaReversiAIBots
{
    public class RandomAI : IAIBot
    {
        public Coordinate GetMove(ReversiGame game)
        {
            var possibleMoves = game.GetValidMovesForCurrentPlayer().ToList();

            var index = Random.Shared.Next(0, possibleMoves.Count);

            return possibleMoves[index];
        }
    }
}
